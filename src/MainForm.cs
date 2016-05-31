using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Media;
using QuickPrint;

#region AutoCAD
using Autodesk.AutoCAD;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using QuickPrint.AutoCAD;
using Ionic.Zlib;
using QuickPrint.CTB;
using System.Text.RegularExpressions;
using System.Globalization;
using PiaNO.Plot;
#endregion

namespace VisualWget
{
    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct NMHDR
    {
        public IntPtr hwndFrom;
        public int idFrom;
        public int code;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct NMCUSTOMDRAW
    {
        public NMHDR hdr;
        public int dwDrawStage;
        public IntPtr hdc;
        public RECT rc;
        public int dwItemSpec;
        public int uItemState;
        public int lItemlParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct NMLVCUSTOMDRAW
    {
        public NMCUSTOMDRAW nmcd;
        public uint clrText;
        public uint clrTextBk;
        public int iSubItem;
    }

    [Flags]
    enum DrawstageFlags
    {
        CDDS_PREPAINT = 0x00000001,
        CDDS_POSTPAINT = 0x00000002,
        CDDS_PREERASE = 0x00000003,
        CDDS_POSTERASE = 0x00000004,
        CDDS_ITEM = 0x00010000,
        CDDS_ITEMPREPAINT = (CDDS_ITEM | CDDS_PREPAINT),
        CDDS_ITEMPOSTPAINT = (CDDS_ITEM | CDDS_POSTPAINT),
        CDDS_ITEMPREERASE = (CDDS_ITEM | CDDS_PREERASE),
        CDDS_ITEMPOSTERASE = (CDDS_ITEM | CDDS_POSTERASE),
        CDDS_SUBITEM = 0x00020000
    }

    [Flags]
    enum CustomDrawReturnFlags
    {
        CDRF_DODEFAULT = 0x00000000,
        CDRF_NEWFONT = 0x00000002,
        CDRF_SKIPDEFAULT = 0x00000004,
        CDRF_NOTIFYPOSTPAINT = 0x00000010,
        CDRF_NOTIFYITEMDRAW = 0x00000020,
        CDRF_NOTIFYSUBITEMDRAW = 0x00000020,
        CDRF_NOTIFYPOSTERASE = 0x00000040
    }

    public partial class MainForm : Form
    {
        bool firstTimeRunning;
        string windowState;
        bool formLoaded;
        bool formClosing;
        List<Job> jobs;
        JobsListView jobsListView;
        ColumnHeader nameColumnHeader;
        ColumnHeader numColumnHeader;
        ColumnHeader sizeColumnHeader;
        ColumnHeader doneColumnHeader;
        ColumnHeader statusColumnHeader;
        ColumnHeader speedColumnHeader;
        ColumnHeader etaColumnHeader;
        ColumnHeader noteColumnHeader;
        int sortCol;
        bool sortAsc;
        BackgroundWorker listenServer;
        AutoShutdownAction autoShutdownAction;
        bool downloadsCompleted;
        decimal maxRunningJobs;
        bool showBalloonTip;
        bool retry;
        decimal retryAttempts;
        int timeBetweenRetryAttempts;
        List<Job> selectedJobs;
        int cx;
        int cy;
        bool computeHashDone;
        bool settingSaved;
        int logPos;
        Job selectedJob;
        bool updateLog;
        ImageList toolBar1ImageList;
        ImageList jobsListViewImageList;
        int timerHalfSecCount;

        class JobsListView : ListView
        {
            public JobsListView()
            {
                DoubleBuffered = true;
            }

            public void UpdateInfo()
            {
                UpdateInfo(new Rectangle(0, 0, 0, 0));
            }

            public void UpdateInfo(Rectangle updateRect)
            {
                if (TopItem != null)
                {
                    for (int i = TopItem.Index; i < Items.Count; i++)
                    {
                        ListViewItem lvi = Items[i];

                        if (lvi.Bounds.Top > DisplayRectangle.Bottom)
                        {
                            break;
                        }

                        if (!updateRect.IsEmpty
                            && !updateRect.Contains(lvi.Bounds))
                        {
                            if (lvi.Bounds.Bottom > DisplayRectangle.Bottom)
                            {
                                if (!updateRect.Contains(new Rectangle(lvi.Bounds.Left, lvi.Bounds.Top, lvi.Bounds.Width, DisplayRectangle.Bottom - lvi.Bounds.Top)))
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }

                        Job job = (Job)lvi.Tag;
                        JobStatus status = job.GetStatus();

                        int imgInd = (int)status;

                        if (lvi.ImageIndex != imgInd)
                        {
                            lvi.ImageIndex = imgInd;
                        }

                        string name = job.Name;

                        if (lvi.SubItems[0].Text != name)
                        {
                            lvi.SubItems[0].Text = name;
                        }

                        string numText = job.Num.ToString();

                        if (lvi.SubItems[1].Text != numText)
                        {
                            lvi.SubItems[1].Text = numText;
                        }

                        string sizeText = job.SizeText;

                        if (lvi.SubItems[2].Text != sizeText)
                        {
                            lvi.SubItems[2].Text = sizeText;
                        }

                        string doneText = job.DoneText;

                        if (lvi.SubItems[3].Text != doneText)
                        {
                            lvi.SubItems[3].Text = doneText;
                        }

                        string statusText;

                        if (status == JobStatus.Ready)
                        {
                            statusText = Util.translationList["000155"];
                        }
                        else if (status == JobStatus.Queued)
                        {
                            statusText = Util.translationList["000156"];
                        }
                        else if (status == JobStatus.Running)
                        {
                            statusText = Util.translationList["000157"];
                        }
                        else if (status == JobStatus.Retrieving)
                        {
                            statusText = Util.translationList["000158"];
                        }
                        else if (status == JobStatus.Stopped)
                        {
                            statusText = Util.translationList["000159"];
                        }
                        else if (status == JobStatus.Finished)
                        {
                            statusText = Util.translationList["000160"];
                        }
                        else
                        {
                            Debug.Assert(false);
                            statusText = "";
                        }

                        statusText += ((status == JobStatus.Stopped && job.RetryCount > 0) ? string.Format(" ({0})", job.RetryCount) : "");

                        if (lvi.SubItems[4].Text != statusText)
                        {
                            lvi.SubItems[4].Text = statusText;
                        }

                        if (status == JobStatus.Retrieving)
                        {
                            string speedText = job.SpeedText;

                            if (lvi.SubItems[5].Text != speedText)
                            {
                                lvi.SubItems[5].Text = speedText;
                            }

                            string etaText = job.EtaText;

                            if (lvi.SubItems[6].Text != etaText)
                            {
                                lvi.SubItems[6].Text = etaText;
                            }
                        }
                        else
                        {
                            if (job.Speed != -1 || job.Eta != -1)
                            {
                                job.ClearSpeed();
                            }

                            if (lvi.SubItems[5].Text != "")
                            {
                                lvi.SubItems[5].Text = "";
                            }

                            if (lvi.SubItems[6].Text != "")
                            {
                                lvi.SubItems[6].Text = "";
                            }
                        }

                        string noteText = job.NoteText;

                        if (lvi.SubItems[7].Text != noteText)
                        {
                            lvi.SubItems[7].Text = noteText;
                        }
                    }
                }
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == ((0x0400 + 0x1c00) + 0x004E) /* OCM_NOTIFY */)
                {
                    NMHDR hdr = (NMHDR)m.GetLParam(typeof(NMHDR));

                    if (hdr.code == (0 - 12) /* NM_CUSTOMDRAW */)
                    {
                        m.Result = (IntPtr)OnCustomDraw((NMLVCUSTOMDRAW)m.GetLParam(typeof(NMLVCUSTOMDRAW)));

                        return;
                    }
                }
                else if (m.Msg == 0x000F /* WM_PAINT */)
                {
                    RECT rect = new RECT();

                    if (Util.GetUpdateRect(Handle, out rect, false))
                    {
                        UpdateInfo(new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top));
                    }
                }

                base.WndProc(ref m);
            }

            CustomDrawReturnFlags OnCustomDraw(NMLVCUSTOMDRAW lvcd)
            {
                DrawstageFlags df = (DrawstageFlags)lvcd.nmcd.dwDrawStage;

                if (df == DrawstageFlags.CDDS_PREPAINT)
                {
                    return CustomDrawReturnFlags.CDRF_NOTIFYITEMDRAW;
                }

                if (df == DrawstageFlags.CDDS_ITEMPREPAINT)
                {
                    return CustomDrawReturnFlags.CDRF_NOTIFYSUBITEMDRAW;
                }

                if (df == (DrawstageFlags.CDDS_SUBITEM | DrawstageFlags.CDDS_ITEMPREPAINT))
                {
                    return CustomDrawReturnFlags.CDRF_NOTIFYPOSTPAINT;
                }

                if (df == (DrawstageFlags.CDDS_SUBITEM | DrawstageFlags.CDDS_ITEMPOSTPAINT))
                {
                    if (lvcd.iSubItem == 3) // Done
                    {
                        ListViewItem lvi = Items[lvcd.nmcd.dwItemSpec];

                        if (lvi.SubItems[3].Text != "" && lvi.ListView.Columns[3].Width != 0)
                        {
                            float value = float.Parse(lvi.SubItems[3].Text.Split(' ')[0]);
                            Rectangle bounds = lvi.SubItems[3].Bounds;

                            if (value < 0)
                            {
                                value = 0;
                            }
                            else if (value > 100)
                            {
                                value = 100;
                            }

                            bounds.Inflate(-2, -2);

                            using (Graphics g = Graphics.FromHdc(lvcd.nmcd.hdc))
                            {
                                RectangleF rcItem = new RectangleF(bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2);
                                RectangleF rcLeft = new RectangleF(bounds.X + 1, bounds.Y + 1, (bounds.Width - 2) * value / 100, bounds.Height - 2);
                                RectangleF rcRight = new RectangleF(bounds.X + 1 + (bounds.Width - 2) * value / 100, bounds.Y + 1, bounds.Width - 2 - ((bounds.Width - 2) * value / 100), bounds.Height - 2);
                                StringFormat sf = new StringFormat();

                                g.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)), rcLeft);
                                g.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Window)), rcRight);

                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Center;
                                sf.FormatFlags = StringFormatFlags.NoWrap;

                                g.SetClip(rcLeft);
                                g.DrawString(string.Format("{0:F} %", value), Util.GetInterfaceFont(), new SolidBrush(Color.FromKnownColor(KnownColor.HighlightText)), rcItem, sf);

                                g.SetClip(rcRight);
                                g.DrawString(string.Format("{0:F} %", value), Util.GetInterfaceFont(), new SolidBrush(Color.FromKnownColor(KnownColor.WindowText)), rcItem, sf);

                                g.SetClip(bounds);
                                g.DrawRectangle(new Pen(Color.FromKnownColor(KnownColor.Control)), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
                            }
                        }
                    }

                    return CustomDrawReturnFlags.CDRF_DODEFAULT;
                }

                return CustomDrawReturnFlags.CDRF_DODEFAULT;
            }
        }

        void jobsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (sortCol != e.Column)
            {
                sortCol = e.Column;
                sortAsc = true;
            }
            else
            {
                sortAsc = !sortAsc;
            }

            Util.SetListViewHeaderArrow(jobsListView, sortCol, sortAsc);
            jobsListView.ListViewItemSorter = new ListViewItemComparer(sortCol, sortAsc);
        }

        class ListViewItemComparer : IComparer
        {
            int col;
            bool asc;

            public ListViewItemComparer(int col, bool asc)
            {
                this.col = col;
                this.asc = asc;
            }

            public int Compare(object x, object y)
            {
                ListViewItem lvi_x = (ListViewItem)x;
                ListViewItem lvi_y = (ListViewItem)y;
                Job j_x = (Job)lvi_x.Tag;
                Job j_y = (Job)lvi_y.Tag;
                int returnVal = 0;

                if (col == 1) // #
                {
                    int X = j_x.Num;
                    int Y = j_y.Num;

                    if (X < Y)
                    {
                        returnVal = -1;
                    }
                    else if (X > Y)
                    {
                        returnVal = 1;
                    }
                }
                else if (col == 2) // Size
                {
                    long X = j_x.Size;
                    long Y = j_y.Size;

                    if (X < Y)
                    {
                        returnVal = -1;
                    }
                    else if (X > Y)
                    {
                        returnVal = 1;
                    }
                }
                else if (col == 3) // Done
                {
                    long X1 = j_x.Size;
                    long X2 = j_x.Done;

                    long Y1 = j_y.Size;
                    long Y2 = j_y.Done;

                    double X, Y;

                    if (X1 == -1 || X2 == -1)
                    {
                        X = -1;
                    }
                    else
                    {
                        X = X2 * 100.0 / X1;
                    }

                    if (Y1 == -1 || Y2 == -1)
                    {
                        Y = -1;
                    }
                    else
                    {
                        Y = Y2 * 100.0 / Y1;
                    }

                    if (X < Y)
                    {
                        returnVal = -1;
                    }
                    else if (X > Y)
                    {
                        returnVal = 1;
                    }
                }
                else if (col == 4) // Status
                {
                    int X = (int)j_x.GetStatus();
                    int Y = (int)j_y.GetStatus();

                    if (X < Y)
                    {
                        returnVal = -1;
                    }
                    else if (X > Y)
                    {
                        returnVal = 1;
                    }
                }
                else if (col == 5) // Speed
                {
                    long X = j_x.Speed;
                    long Y = j_y.Speed;

                    if (X < Y)
                    {
                        returnVal = -1;
                    }
                    else if (X > Y)
                    {
                        returnVal = 1;
                    }
                }
                else if (col == 6) // Eta
                {
                    long X = j_x.Eta;
                    long Y = j_y.Eta;

                    if (X < Y)
                    {
                        returnVal = -1;
                    }
                    else if (X > Y)
                    {
                        returnVal = 1;
                    }
                }
                else if (col == 7) // Note
                {
                    returnVal = string.Compare(j_x.NoteText, j_y.NoteText);
                }

                if (returnVal == 0) // Name
                {
                    returnVal = string.Compare(j_x.Name, j_y.Name);
                }

                if (returnVal == 0) // #
                {
                    int X = j_x.Num;
                    int Y = j_y.Num;

                    if (X < Y)
                    {
                        returnVal = -1;
                    }
                    else if (X > Y)
                    {
                        returnVal = 1;
                    }
                }

                return asc ? returnVal : returnVal * -1;
            }
        }

        protected override void WndProc(ref Message m)
        {
            Rectangle rc = Screen.FromPoint(MousePosition).WorkingArea;

            if (m.Msg == 0x0214 /* WM_SIZING */)
            {
                RECT rect = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));

                if (Math.Abs(rect.left - rc.Left) <= 10)
                {
                    rect.left = rc.Left;
                }

                if (Math.Abs(rect.top - rc.Top) <= 10)
                {
                    rect.top = rc.Top;
                }

                if (Math.Abs(rc.Right - rect.right) <= 10)
                {
                    rect.right = rc.Right;
                }

                if (Math.Abs(rc.Bottom - rect.bottom) <= 10)
                {
                    rect.bottom = rc.Bottom;
                }

                Marshal.StructureToPtr(rect, m.LParam, true);
                m.Result = (IntPtr)1;

                return;
            }
            else if (m.Msg == 0x0216 /* WM_MOVING */)
            {
                RECT rect = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));

                rect.right += MousePosition.X - rect.left - cx;
                rect.left = MousePosition.X - cx;
                rect.bottom += MousePosition.Y - rect.top - cy;
                rect.top = MousePosition.Y - cy;

                if (Math.Abs(rc.Right - rect.right) <= 10)
                {
                    rect.left += (rc.Right - rect.right);
                    rect.right = rc.Right;
                }

                if (Math.Abs(rc.Bottom - rect.bottom) <= 10)
                {
                    rect.top += (rc.Bottom - rect.bottom);
                    rect.bottom = rc.Bottom;
                }

                if (Math.Abs(rect.left - rc.Left) <= 10)
                {
                    rect.right -= (rect.left - rc.Left);
                    rect.left = rc.Left;
                }

                if (Math.Abs(rect.top - rc.Top) <= 10)
                {
                    rect.bottom -= (rect.top - rc.Top);
                    rect.top = rc.Top;
                }

                Marshal.StructureToPtr(rect, m.LParam, true);
                m.Result = (IntPtr)1;

                return;
            }
            else if (m.Msg == 0x0231 /* WM_ENTERSIZEMOVE */)
            {
                cx = MousePosition.X - Left;
                cy = MousePosition.Y - Top;
                m.Result = (IntPtr)0;

                return;
            }

            base.WndProc(ref m);
        }

        public MainForm()
        {
            Util.LoadSettings();

            firstTimeRunning = bool.Parse(Util.GetSetting("FirstTimeRunning"));
            windowState = Util.GetSetting("WindowState");
            formLoaded = false;
            formClosing = false;
            jobs = new List<Job>();
            sortCol = 1;
            sortAsc = true;

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 0);

            server.Start();
            Util.ListeningPort = ((IPEndPoint)server.LocalEndpoint).Port;
            listenServer = new BackgroundWorker();
            listenServer.DoWork += new DoWorkEventHandler(listenServer_DoWork);
            listenServer.WorkerReportsProgress = true;
            listenServer.ProgressChanged += new ProgressChangedEventHandler(listenServer_ProgressChanged);
            listenServer.RunWorkerAsync(server);

            autoShutdownAction = AutoShutdownAction.None;
            downloadsCompleted = true;
            maxRunningJobs = decimal.Parse(Util.GetSetting("MaxRunningJobs"));
            showBalloonTip = bool.Parse(Util.GetSetting("ShowBalloonTip"));
            retry = bool.Parse(Util.GetSetting("Retry"));
            retryAttempts = decimal.Parse(Util.GetSetting("RetryAttempts"));
            timeBetweenRetryAttempts = Util.GetTimeBetweenRetryAttempts();
            selectedJobs = new List<Job>();
            cx = 0;
            cy = 0;
            computeHashDone = true;
            settingSaved = true;
            logPos = 0;
            selectedJob = null;
            //updateLog = true;

            toolBar1ImageList = new ImageList();
            toolBar1ImageList.ColorDepth = ColorDepth.Depth32Bit;
            toolBar1ImageList.Images.AddRange(new Image[] {
                    QuickPrint.Properties.Resources.MainToolbar_New,
                    QuickPrint.Properties.Resources.MainToolbar_Edit,
                    QuickPrint.Properties.Resources.MainToolbar_Remove,
                    QuickPrint.Properties.Resources.MainToolbar_Start,
                    QuickPrint.Properties.Resources.MainToolbar_Stop,
                    QuickPrint.Properties.Resources.MainToolbar_OpenContainingFolder,
                    QuickPrint.Properties.Resources.MainToolbar_MoveUp,
                    QuickPrint.Properties.Resources.MainToolbar_MoveDown,
                    QuickPrint.Properties.Resources.MainToolbar_DefaultDownloadOptions,
                    QuickPrint.Properties.Resources.MainToolbar_Preferences,
                    QuickPrint.Properties.Resources.MainToolbar_New_Multiple,
                    QuickPrint.Properties.Resources.MainToolbar_Speed_Limit,
                    QuickPrint.Properties.Resources.refresh16,
                });

            jobsListViewImageList = new ImageList();
            jobsListViewImageList.ColorDepth = ColorDepth.Depth32Bit;
            jobsListViewImageList.Images.AddRange(new Image[] {
                    QuickPrint.Properties.Resources.JobsListView_Ready,
                    QuickPrint.Properties.Resources.JobsListView_Queued,
                    QuickPrint.Properties.Resources.JobsListView_Running,
                    QuickPrint.Properties.Resources.JobsListView_Retrieving,
                    QuickPrint.Properties.Resources.JobsListView_Stopped,
                    QuickPrint.Properties.Resources.JobsListView_Finished
                });

            timerHalfSecCount = 0;

            jobsListView = new JobsListView();

            nameColumnHeader = new ColumnHeader();
            nameColumnHeader.Name = "nameColumnHeader";
            nameColumnHeader.Text = "Name";

            numColumnHeader = new ColumnHeader();
            numColumnHeader.Name = "numColumnHeader";
            numColumnHeader.Text = "#";
            numColumnHeader.TextAlign = HorizontalAlignment.Right;

            sizeColumnHeader = new ColumnHeader();
            sizeColumnHeader.Name = "sizeColumnHeader";
            sizeColumnHeader.Text = "Size";
            sizeColumnHeader.TextAlign = HorizontalAlignment.Right;

            doneColumnHeader = new ColumnHeader();
            doneColumnHeader.Name = "doneColumnHeader";
            doneColumnHeader.Text = "Done";
            doneColumnHeader.TextAlign = HorizontalAlignment.Right;

            statusColumnHeader = new ColumnHeader();
            statusColumnHeader.Name = "statusColumnHeader";
            statusColumnHeader.Text = "Status";

            speedColumnHeader = new ColumnHeader();
            speedColumnHeader.Name = "speedColumnHeader";
            speedColumnHeader.Text = "Speed";
            speedColumnHeader.TextAlign = HorizontalAlignment.Right;

            etaColumnHeader = new ColumnHeader();
            etaColumnHeader.Name = "etaColumnHeader";
            etaColumnHeader.Text = "ETA";

            noteColumnHeader = new ColumnHeader();
            noteColumnHeader.Name = "noteColumnHeader";
            noteColumnHeader.Text = "Note";

            jobsListView.Columns.AddRange(new ColumnHeader[] {
                    nameColumnHeader,
                    numColumnHeader,
                    sizeColumnHeader,
                    doneColumnHeader,
                    statusColumnHeader,
                    speedColumnHeader,
                    etaColumnHeader,
                    noteColumnHeader
                });

            jobsListView.Dock = DockStyle.Fill;
            jobsListView.FullRowSelect = true;
            jobsListView.HideSelection = false;
            jobsListView.Name = "jobsListView";
            jobsListView.TabIndex = 0;
            jobsListView.View = View.Details;
            jobsListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            jobsListView.SmallImageList = jobsListViewImageList;
            jobsListView.DoubleClick += new EventHandler(jobsListView_DoubleClick);
            jobsListView.ColumnClick += new ColumnClickEventHandler(jobsListView_ColumnClick);
            jobsListView.KeyDown += new KeyEventHandler(jobsListView_KeyDown);
            jobsListView.AllowColumnReorder = true;
            jobsListView.ColumnReordered += new ColumnReorderedEventHandler(jobsListView_ColumnReordered);
            jobsListView.SelectedIndexChanged += new EventHandler(jobsListView_SelectedIndexChanged);
            jobsListView.ShowItemToolTips = true;

            InitializeComponent();

            toolBar1.ImageList = toolBar1ImageList;
            jobsListView.ContextMenu = jobsListViewContextMenu;
            panel2.Controls.Add(jobsListView);

            notifyIcon1.ContextMenu = trayContextMenu;

            Icon = QuickPrint.Properties.Resources.App;
            notifyIcon1.Icon = QuickPrint.Properties.Resources.Tray;

            LoadForm();

            if (Util.StartInTray)
            {
                notifyIcon1.Visible = true;
                timer1.Interval = 200;
                Util.TrimCurrentProcessWorkingSet();
            }

            //
            //this.TopMost = true;
            AddTest();
        }

        #region Test
        void AddTest()
        {
            jobsListView.Items.Clear();

            for (int i = 1; i < 10; i++)
            {
                Job job = new Job("Girder "+ i);
                //JobDialog jobDialog = new JobDialog(job.Urls, job.Opts);

                job.Num = i;// jobs.Count + 1;

                DateTime now = DateTime.Now;

                job.LastStartedTime = (new DateTime(now.Year, now.Month, now.Day)).Ticks;
                //job.AutoStartNumDays = jobDialog.AutoStartNumDays;
                job.LastStartedTimeActual = now.Ticks;
                //job.AutoStartNumHours = jobDialog.AutoStartNumHours;
               // job.NoteText = jobDialog.NoteText;
                jobs.Add(job);

                ListViewItem lvi = NewListViewItem(job);

                jobsListView.Items.Add(lvi);
            }
            
        }
        #endregion

        void listenServer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0) // VisualWget is already running.
            {
                if (!Visible)
                {
                    Show();
                }

                if (WindowState == FormWindowState.Minimized)
                {
                    Util.ShowWindow(Handle, 9 /* SW_RESTORE */);
                }

                SetAsForegroundWindow();
            }
            else if (e.ProgressPercentage == 1) // Add a new download.
            {
                AddNewDownload(e.UserState.ToString());
            }
        }

        private void AddNewDownload(string cmdLineArgs)
        {
            bool noPrompt = bool.Parse(Util.GetSetting("NoPromptOnNewJob"));

            if (noPrompt)
            {
                Job j = NewJob(cmdLineArgs, true);

                notifyIcon1.Tag = null;
                notifyIcon1.ShowBalloonTip(30000, Util.translationList["000231"], String.Format("{0}", j.GetNotifyJobName()), ToolTipIcon.Info);
            }
            else if (Handle == Util.GetLastActivePopup(Handle))
            {
                if (!Visible)
                {
                    Show();
                }

                if (WindowState == FormWindowState.Minimized)
                {
                    Util.ShowWindow(Handle, 9 /* SW_RESTORE */);
                }

                SetAsForegroundWindow();
                NewJob(cmdLineArgs);
            }
        }

        void jobsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedJobs.Count > 0)
            {
                selectedJobs.Clear();
            }
        }

        void jobsListView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            if (e.NewDisplayIndex == 0 || e.OldDisplayIndex == 0)
            {
                e.Cancel = true;
            }
        }

        void jobsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                for (int i = 0; i < jobsListView.Items.Count; i++)
                {
                    jobsListView.Items[i].Selected = true;
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                RemoveJobs();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                EditJob();
            }
        }

        bool IsWaitRetry(Job j)
        {
            if (j.IsStopped()
                && retry
                && !j.Opts.ContainsKey("recursive")
                && j.Opts.ContainsKey("continue")
                && (retryAttempts == 0
                    || j.RetryCount < retryAttempts))
            {
                return true;
            }

            return false;
        }

        bool IsFullyStopped(Job j)
        {
            if (j.IsRunning() || j.Queued || IsWaitRetry(j))
            {
                return false;
            }

            return true;
        }

        void EnableDisableToolbarButtonsAndMenuItems()
        {
            if (Visible)
            {
                if (selectedJobs.Count == 0)
                {
                    foreach (int i in jobsListView.SelectedIndices)
                    {
                        selectedJobs.Add((Job)jobsListView.Items[i].Tag);
                    }
                }

                if (MouseButtons == MouseButtons.Left)
                {
                    if (isToolBarButtonPressing)
                    {
                        return;
                    }
                }
                else
                {
                    isToolBarButtonPressing = false;
                }

                bool canEdit = false;

                if (jobsListView.SelectedIndices.Count == 1
                    && IsFullyStopped((Job)jobsListView.Items[jobsListView.SelectedIndices[0]].Tag))
                {
                    canEdit = true;
                }

                jobsEditMenuItem.Enabled = editToolBarButton.Enabled = jlv_j_editMenuItem.Enabled = canEdit;

                bool canEditMultiple = true;

                foreach (int i in jobsListView.SelectedIndices)
                {
                    Job j = (Job)jobsListView.Items[i].Tag;
                    if (!IsFullyStopped(j))
                    {
                        canEditMultiple = false;
                        break;
                    }
                }

                jlv_j_editMultipleMenuItem.Enabled = canEditMultiple;

                bool canRemove = (jobsListView.SelectedIndices.Count != 0);

                jobsRemoveMenuItem.Enabled = removeToolBarButton.Enabled = jlv_j_removeMenuItem.Enabled = canRemove;

                bool canCleanup = false;

                foreach (Job j in jobs)
                {
                    if (j.GetStatus() == JobStatus.Finished)
                    {
                        canCleanup = true;

                        break;
                    }
                }

                jobsCleanupMenuItem.Enabled = canCleanup;

                bool canStart = false;

                foreach (Job j in selectedJobs)
                {
                    if (IsFullyStopped(j))
                    {
                        canStart = true;

                        break;
                    }
                }

                jobsStartMenuItem.Enabled = startToolBarButton.Enabled = jlv_j_startMenuItem.Enabled = canStart;

                bool canStop = false;

                foreach (Job j in selectedJobs)
                {
                    if (!IsFullyStopped(j))
                    {
                        canStop = true;

                        break;
                    }
                }

                jobsStopMenuItem.Enabled = stopToolBarButton.Enabled = jlv_j_stopMenuItem.Enabled = canStop;

                bool canOpenFolder = false;

                if (jobsListView.SelectedIndices.Count == 1)
                {
                    canOpenFolder = TestOpenContainingFolder((Job)jobsListView.Items[jobsListView.SelectedIndices[0]].Tag);
                }

                jobsOpenContainingFolderMenuItem.Enabled = openContainingFolderToolBarButton.Enabled = jlv_j_openContainingFolderMenuItem.Enabled = canOpenFolder;

                List<int> selectedNums = new List<int>();

                foreach (Job j in selectedJobs)
                {
                    selectedNums.Add(j.Num);
                }

                selectedNums.Sort();

                bool canMoveUp = false;

                for (int i = 0; i < selectedNums.Count; i++)
                {
                    if (selectedNums[i] != i + 1)
                    {
                        canMoveUp = true;

                        break;
                    }
                }

                jobsMoveUpMenuItem.Enabled = moveUpToolBarButton.Enabled = jlv_j_moveUpMenuItem.Enabled = canMoveUp;

                bool canMoveDown = false;

                for (int i = selectedNums.Count - 1; i >= 0; i--)
                {
                    if (selectedNums[i] != jobsListView.Items.Count + i - selectedNums.Count + 1)
                    {
                        canMoveDown = true;

                        break;
                    }
                }

                jobsMoveDownMenuItem.Enabled = moveDownToolBarButton.Enabled = jlv_j_moveDownMenuItem.Enabled = canMoveDown;

                bool canExportWgetBatchFile = (jobsListView.SelectedIndices.Count != 0);

                jobsExportWgetBatchFileMenuItem.Enabled = jlv_j_exportWgetBatchFileMenuItem.Enabled = canExportWgetBatchFile;
            }

#if false // remove
            bool canStartAll = false;

            foreach (Job j in jobs) {
                if (IsFullyStopped(j)) {
                    canStartAll = true;

                    break;
                }
            }

            bool canStopAll = false;

            foreach (Job j in jobs)
            {
                if (!IsFullyStopped(j))
                {
                    canStopAll = true;

                    break;
                }
            }
#endif
        }

        struct Connection
        {
            public TcpClient client;
            public NetworkStream stream;
            public int type;
            public uint procId;
            public Job job;
            public StringBuilder data;

            public Connection(TcpClient client, NetworkStream stream)
            {
                this.client = client;
                this.stream = stream;
                type = -1;
                procId = 0;
                job = null;
                data = new StringBuilder();
            }
        }

        void listenServer_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            TcpListener server = (TcpListener)e.Argument;
            List<Connection> connections = new List<Connection>();

            try
            {
                while (true)
                {
                    while (server.Pending())
                    {
                        TcpClient client = server.AcceptTcpClient();
                        NetworkStream stream = client.GetStream();

                        connections.Add(new Connection(client, stream));
                    }

                    int index = 0;

                    while (index < connections.Count)
                    {
                        Connection c = connections[index];
                        string[] lines;

                        if (c.type == -1)
                        {
                            if (c.stream.CanRead)
                            {
                                try
                                {
                                    while (c.stream.DataAvailable)
                                    {
                                        Byte[] bytes = new Byte[256];
                                        int i = c.stream.Read(bytes, 0, bytes.Length);

                                        c.data.Append(Encoding.Default.GetString(bytes, 0, i));
                                    }

                                    lines = c.data.ToString().Split(new char[] { '\n' }, 2);

                                    if (lines.Length == 2)
                                    {
                                        c.type = int.Parse(lines[0]);
                                        c.data.Remove(0, c.data.Length);
                                        c.data.Append(lines[1]);
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        else if (c.type == 0) // Wget
                        {
                            if (c.job == null)
                            {
                                if (c.procId == 0 && c.stream.CanRead)
                                {
                                    try
                                    {
                                        while (c.stream.DataAvailable)
                                        {
                                            Byte[] bytes = new Byte[256];
                                            int i = c.stream.Read(bytes, 0, bytes.Length);

                                            c.data.Append(Encoding.Default.GetString(bytes, 0, i));
                                        }

                                        lines = c.data.ToString().Split(new char[] { '\n' }, 2);

                                        if (lines.Length == 2)
                                        {
                                            c.procId = uint.Parse(lines[0]);
                                            c.data.Remove(0, c.data.Length);
                                            c.data.Append(lines[1]);
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }

                                if (c.procId != 0)
                                {
                                    foreach (Job j in jobs)
                                    {
                                        if (j.IsRunning() && j.ProcId == c.procId)
                                        {
                                            c.job = j;

                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (c.stream.CanRead)
                                {
                                    try
                                    {
                                        while (c.stream.DataAvailable)
                                        {
                                            Byte[] bytes = new Byte[256];
                                            int i = c.stream.Read(bytes, 0, bytes.Length);

                                            c.data.Append(Encoding.Default.GetString(bytes, 0, i));
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }

                                lines = c.data.ToString().Split(new char[] { '\n' }, 2);

                                while (lines.Length == 2)
                                {
                                    c.job.DispatchMsg(string.Format("{0} {1}", lines[0], DateTime.Now.Ticks));
                                    c.data.Remove(0, c.data.Length);
                                    c.data.Append(lines[1]);
                                    lines = c.data.ToString().Split(new char[] { '\n' }, 2);
                                }

                                if (!c.job.IsRunning() || c.data.ToString() == "$")
                                {
                                    c.stream.Close();
                                    c.client.Close();
                                    connections.RemoveAt(index);

                                    continue;
                                }

                                if (c.stream.CanWrite)
                                {
                                    if (c.job.LocalFileChecked)
                                    {
                                        try
                                        {
                                            byte[] buffer = Encoding.Default.GetBytes(string.Format("0 {0}\n", c.job.LocalFileChecker));

                                            c.stream.WriteTimeout = 1;
                                            c.stream.Write(buffer, 0, buffer.Length);
                                            c.job.LocalFileChecked = false;
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    if (c.job.LimitRateChanged)
                                    {
                                        Debug.Assert(c.job.LimitRate >= 0);

                                        try
                                        {
                                            byte[] buffer = Encoding.Default.GetBytes(string.Format("1 {0}\n", c.job.LimitRate));

                                            c.stream.WriteTimeout = 1;
                                            c.stream.Write(buffer, 0, buffer.Length);
                                            c.job.LimitRateChanged = false;
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                        else if (c.type == 1)
                        {
                            if (c.stream.CanRead)
                            {
                                try
                                {
                                    while (c.stream.DataAvailable)
                                    {
                                        Byte[] bytes = new Byte[256];
                                        int i = c.stream.Read(bytes, 0, bytes.Length);

                                        c.data.Append(Encoding.Default.GetString(bytes, 0, i));
                                    }
                                }
                                catch
                                {
                                }
                            }

                            lines = c.data.ToString().Split(new char[] { '\n' }, 2);

                            while (lines.Length == 2)
                            {
                                string[] segments = Util.CmdLineArgsToArgs(lines[0]);

                                Debug.Assert(segments.Length > 0);

                                int id = int.Parse(segments[0]);

                                if (id == 0)
                                {
                                    Debug.Assert(segments.Length == 1);
                                    bw.ReportProgress(0);
                                }
                                else if (id == 1)
                                {
                                    Debug.Assert(segments.Length == 2);
                                    bw.ReportProgress(1, segments[1]);
                                }

                                c.data.Remove(0, c.data.Length);
                                c.data.Append(lines[1]);
                                lines = c.data.ToString().Split(new char[] { '\n' }, 2);
                            }

                            if (c.data.ToString() == "$")
                            {
                                c.stream.Close();
                                c.client.Close();
                                connections.RemoveAt(index);

                                continue;
                            }
                        }

                        connections[index] = c;
                        index++;
                    }

                    Thread.Sleep(1);
                }
            }
            finally
            {
                for (int index = 0; index < connections.Count; index++)
                {
                    Connection c = connections[index];

                    c.stream.Close();
                    c.client.Close();
                }

                server.Stop();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing
                && bool.Parse(Util.GetSetting("CloseToTray")))
            {
                Hide();
                e.Cancel = true;

                return;
            }

            if ((e.CloseReason == CloseReason.UserClosing
                    || e.CloseReason == CloseReason.ApplicationExitCall)
                && !downloadsCompleted
                && Util.MsgBox(Util.translationList["000191"],
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.No)
            {
                e.Cancel = true;

                return;
            }

            notifyIcon1.Visible = false;
            formClosing = true;
            Hide();

            Util.PutSetting("FirstTimeRunning", bool.FalseString);

            if (WindowState == FormWindowState.Normal)
            {
                Util.PutSetting("X", Location.X.ToString());
                Util.PutSetting("Y", Location.Y.ToString());
                Util.PutSetting("Width", Size.Width.ToString());
                Util.PutSetting("Height", Size.Height.ToString());
            }
            else
            {
                Util.PutSetting("X", RestoreBounds.X.ToString());
                Util.PutSetting("Y", RestoreBounds.Y.ToString());
                Util.PutSetting("Width", RestoreBounds.Width.ToString());
                Util.PutSetting("Height", RestoreBounds.Height.ToString());
            }

            Util.PutSetting("WindowState", windowState);
            Util.PutSetting("JobsListViewSortCol", sortCol.ToString());
            Util.PutSetting("JobsListViewSortAsc", sortAsc.ToString());

            string columnWidthArrayString = "";

            for (int i = 0; i < jobsListView.Columns.Count; i++)
            {
                if (columnWidthArrayString != "")
                {
                    columnWidthArrayString += ",";
                }

                columnWidthArrayString += jobsListView.Columns[i].Width.ToString();
            }

            Util.PutSetting("JobsListViewColumnWidthArray", columnWidthArrayString);

            int[] columnOrderArray;
            bool success = Util.GetListViewColumnOrder(jobsListView, out columnOrderArray);

            if (success)
            {
                string columnOrderArrayString = "";

                for (int i = 0; i < columnOrderArray.Length; i++)
                {
                    if (columnOrderArrayString != "")
                    {
                        columnOrderArrayString += ",";
                    }

                    columnOrderArrayString += columnOrderArray[i].ToString();
                }

                Util.PutSetting("JobsListViewColumnOrderArray", columnOrderArrayString);
            }

            Util.PutSetting("ShowToolbar", viewToolbarMenuItem.Checked.ToString());
            Util.PutSetting("ShowLog", viewLogMenuItem.Checked.ToString());
            Util.PutSetting("ShowStatusBar", viewStatusBarMenuItem.Checked.ToString());
            Util.PutSetting("LogHeight", logTextBox.Height.ToString());
            Util.PutSetting("ListeningPort", "-1");
            Util.SaveSettings();

            foreach (Job j in jobs)
            {
                StopJob(j);
            }

            SaveJobs();

            if (Util.CheckForUpdatesTemp != null)
            {
                try
                {
                    File.Delete(Util.CheckForUpdatesTemp);
                }
                catch
                {
                }
            }
        }

        void SaveJobs()
        {
            List<string> lines = new List<string>();

            foreach (Job j in jobs)
            {
                bool r = j.Opts.ContainsKey("recursive");

                lines.Add(Util.AddQuotes(j.GetCmdLineArgs())
                    + " " + Util.AddQuotes(r ? "" : j.LocalFile)
                    + " " + Util.AddQuotes(r ? "-1" : j.Size.ToString())
                    + " " + Util.AddQuotes(r ? "-1" : j.Done.ToString())
                    + " " + Util.AddQuotes(j.GetStatus().ToString())
                    + " " + Util.AddQuotes(j.LastStartedTime.ToString())
                    + " " + Util.AddQuotes(j.AutoStartNumDays.ToString())
                    + " " + Util.AddQuotes(j.LastStartedTimeActual.ToString())
                    + " " + Util.AddQuotes(j.AutoStartNumHours.ToString())
                    + " " + Util.AddQuotes(j.NoteText)
                    );
            }

            string dir;

            if (Util.IsPortableApp())
            {
                dir = Util.AppDir;
            }
            else
            {
                dir = Util.AppDataDir;
                Directory.CreateDirectory(dir);
            }

            try
            {
                File.WriteAllLines(Path.Combine(dir, "DownloadList.cfg"), lines.ToArray());
            }
            catch (Exception ex)
            {
                Util.MsgBox(string.Format(Util.translationList["000189"], ex.Message), MessageBoxIcon.Error);
            }
        }

        ListViewItem NewListViewItem(Job j)
        {
            int imageIndex = (int)j.SavedStatus;
            string[] subitems = { j.Name, j.Num.ToString(), j.SizeText, j.DoneText, j.SavedStatus.ToString(), "", "", j.NoteText };
            ListViewItem lvi = new ListViewItem(subitems, imageIndex);

            lvi.Tag = j;

            return lvi;
        }

        void LoadJobs()
        {
            string[] lines;

            try
            {
                string dir;

                if (Util.IsPortableApp())
                {
                    dir = Util.AppDir;
                }
                else
                {
                    dir = Util.AppDataDir;
                }

                lines = File.ReadAllLines(Path.Combine(dir, "DownloadList.cfg"));
            }
            catch (FileNotFoundException)
            {
                return;
            }
            catch (DirectoryNotFoundException)
            {
                return;
            }
            catch (Exception ex)
            {
                Util.MsgBox(ex.Message, MessageBoxIcon.Error);

                return;
            }

            foreach (Job j in jobs)
            {
                StopJob(j);
            }

            jobs.Clear();
            jobsListView.BeginUpdate();
            jobsListView.Items.Clear();

            List<ListViewItem> addToJobList = new List<ListViewItem>();

            foreach (string line in lines)
            {
                string[] args = Util.CmdLineArgsToArgs(line);

                if (args.Length > 0)
                {
                    Job job = new Job(args[0]);

                    job.Num = jobs.Count + 1;
                    jobs.Add(job);

                    if (args.Length > 4)
                    {
                        job.LocalFile = args[1];
                        job.Size = long.Parse(args[2]);
                        job.Done = long.Parse(args[3]);
                        job.SavedStatus = (JobStatus)(Enum.Parse(typeof(JobStatus), args[4]));

                        if (job.SavedStatus == JobStatus.Queued
                            || job.SavedStatus == JobStatus.Running
                            || job.SavedStatus == JobStatus.Retrieving)
                        {
                            job.SavedStatus = JobStatus.Stopped;
                        }

                        if (args.Length > 6)
                        {
                            job.LastStartedTime = long.Parse(args[5]);
                            job.AutoStartNumDays = int.Parse(args[6]);
                        }

                        if (args.Length > 8)
                        {
                            job.LastStartedTimeActual = long.Parse(args[7]);
                            job.AutoStartNumHours = int.Parse(args[8]);
                        }

                        if (args.Length > 9)
                        {
                            job.NoteText = args[9];
                        }
                    }

                    addToJobList.Add(NewListViewItem(job));
                }
            }

            if (addToJobList.Count > 0)
            {
                jobsListView.Items.AddRange(addToJobList.ToArray());
            }

            jobsListView.EndUpdate();
        }

        void LoadForm()
        {
            if (!Util.InitOpts())
            {
                Util.MsgBox(Util.translationList["000188"], MessageBoxIcon.Error);
                Application.Exit();

                return;
            }

            Environment.SetEnvironmentVariable("http_proxy", Util.GetSetting("HttpProxy"));
            Environment.SetEnvironmentVariable("https_proxy", Util.GetSetting("HttpsProxy"));
            Environment.SetEnvironmentVariable("ftp_proxy", Util.GetSetting("FtpProxy"));
            Environment.SetEnvironmentVariable("no_proxy", Util.GetSetting("NoProxy"));
            LoadJobs();
            sortCol = int.Parse(Util.GetSetting("JobsListViewSortCol"));
            sortAsc = bool.Parse(Util.GetSetting("JobsListViewSortAsc"));
            Util.SetListViewHeaderArrow(jobsListView, sortCol, sortAsc);
            jobsListView.ListViewItemSorter = new ListViewItemComparer(sortCol, sortAsc);
            EnableDisableToolbarButtonsAndMenuItems();
            toolBar1.Height = 25;
            toolBar1.Visible = viewToolbarMenuItem.Checked = bool.Parse(Util.GetSetting("ShowToolbar"));
            splitter1.Visible = logTextBox.Visible = viewLogMenuItem.Checked = bool.Parse(Util.GetSetting("ShowLog"));
            statusBar1.Visible = viewStatusBarMenuItem.Checked = bool.Parse(Util.GetSetting("ShowStatusBar"));
            logTextBox.Height = int.Parse(Util.GetSetting("LogHeight"));
            toolsAutoRemoveFinishedJobMenuItem.Checked = bool.Parse(Util.GetSetting("AutoRemoveFinishedJob"));
            t_detectClipboardMenuItem.Checked = bool.Parse(Util.GetSetting("DetectClipboard"));
            
            toolsWhenDownloadsFinishedFreezeMenuItem.Checked = bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze"));
            toolsWhenDownloadsFinishedCancelableMenuItem.Checked = bool.Parse(Util.GetSetting("WhenDownloadsFinishedCancelable"));

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                autoShutdownAction = (AutoShutdownAction)(Enum.Parse(typeof(AutoShutdownAction), Util.GetSetting("WhenDownloadsFinishedFreezedValue")));
            }

            string columnWidthArrayString = Util.GetSetting("JobsListViewColumnWidthArray");

            if (columnWidthArrayString != "")
            {
                string[] values = columnWidthArrayString.Split(',');
                int[] columnWidthArray = new int[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    columnWidthArray[i] = int.Parse(values[i]);
                }

                for (int i = 0; i < jobsListView.Columns.Count && i < columnWidthArray.Length; i++)
                {
                    jobsListView.Columns[i].Width = columnWidthArray[i];
                }
            }

            string columnOrderArrayString = Util.GetSetting("JobsListViewColumnOrderArray");

            if (columnOrderArrayString != "")
            {
                string[] values = columnOrderArrayString.Split(',');
                int[] columnOrderArray = new int[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    columnOrderArray[i] = int.Parse(values[i]);
                }

                Util.SetListViewColumnOrder(jobsListView, columnOrderArray);
            }

            Translate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Size = new Size(int.Parse(Util.GetSetting("Width")), int.Parse(Util.GetSetting("Height")));

            if (firstTimeRunning)
            {
                CenterToScreen();
            }
            else
            {
                Point pt = new Point(int.Parse(Util.GetSetting("X")), int.Parse(Util.GetSetting("Y")));
                Rectangle rc = new Rectangle(pt, Size);

                if (Screen.FromPoint(MousePosition).WorkingArea.Contains(rc))
                {
                    Location = pt;
                }
                else
                {
                    CenterToScreen();
                }
            }

            if (WindowState == FormWindowState.Normal)
            {
                WindowState = (FormWindowState)(Enum.Parse(typeof(FormWindowState), windowState));
            }

            this.SetInterFaceFont();

            formLoaded = true;
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            if (formLoaded && (WindowState != FormWindowState.Minimized))
            {
                windowState = WindowState.ToString();
            }
        }

        void NewJob(string cmdLineArgs)
        {
            NewJob(cmdLineArgs, false);
        }

        Job NewJob(string cmdLineArgs, bool noPrompt)
        {
            StringCollection urls;
            StringDictionary opts;

            Util.GetOpt(cmdLineArgs, out urls, out opts);

            if (opts.Count == 0)
            {
                cmdLineArgs = Util.GetSetting("DefDlOptions") + " " + cmdLineArgs;
                cmdLineArgs = cmdLineArgs.Trim();
            }

            Job job = new Job(cmdLineArgs);
            JobDialog jobDialog = new JobDialog(job.Urls, job.Opts);

            jobDialog.Text = "New Job";
            jobDialog.AutoStartNumDays = int.Parse(Util.GetSetting("DefDlAutoStartNumDays"));
            jobDialog.AutoStartNumHours = int.Parse(Util.GetSetting("DefDlAutoStartNumHours"));

            if (noPrompt || jobDialog.ShowDialog(this) == DialogResult.OK)
            {
                job.Num = jobs.Count + 1;

                DateTime now = DateTime.Now;

                job.LastStartedTime = (new DateTime(now.Year, now.Month, now.Day)).Ticks;
                job.AutoStartNumDays = jobDialog.AutoStartNumDays;
                job.LastStartedTimeActual = now.Ticks;
                job.AutoStartNumHours = jobDialog.AutoStartNumHours;
                job.NoteText = jobDialog.NoteText;
                jobs.Add(job);

                ListViewItem lvi = NewListViewItem(job);

                jobsListView.Items.Add(lvi);
                jobsListView.SelectedIndices.Clear();
                lvi.Focused = true;
                lvi.Selected = true;
                lvi.EnsureVisible();
                jobsListView.Focus();

                if (noPrompt || jobDialog.StartOnOk)
                {
                    job.Queued = true;
                    downloadsCompleted = false;
                }

                SaveJobsAsync();
            }

            jobDialog.Dispose();

            return job;
        }

        void EditJob()
        {
            if (jobsListView.SelectedIndices.Count != 1)
            {
                return;
            }

            Job j = (Job)jobsListView.Items[jobsListView.SelectedIndices[0]].Tag;

            if (!IsFullyStopped(j))
            {
                return;
            }

            StringCollection urls = new StringCollection();
            StringDictionary opts = new StringDictionary();

            foreach (string value in j.Urls)
            {
                urls.Add(value);
            }

            foreach (string key in j.Opts.Keys)
            {
                opts.Add(key, j.Opts[key]);
            }

            JobDialog jobDialog = new JobDialog(urls, opts);

            jobDialog.Text = "Edit Job";
            jobDialog.AutoStartNumDays = j.AutoStartNumDays;
            jobDialog.AutoStartNumHours = j.AutoStartNumHours;
            jobDialog.NoteText = j.NoteText;
            jobDialog.ShowStartOnOk = false;

            if (jobDialog.ShowDialog(this) == DialogResult.OK)
            {
                //
                // check again if job was started by scheduler
                //
                if (!IsFullyStopped(j))
                {
                    Util.MsgBox(Util.translationList["000217"], MessageBoxIcon.Error);
                    jobDialog.Dispose();
                    return;
                }

                j.Urls = urls;
                j.Opts = opts;
                j.Close();
                j.SavedStatus = JobStatus.Ready;
                j.LocalFile = "";
                j.Size = -1;
                j.Done = -1;
                j.AutoStartNumDays = jobDialog.AutoStartNumDays;
                j.AutoStartNumHours = jobDialog.AutoStartNumHours;
                j.NoteText = jobDialog.NoteText;

                SaveJobsAsync();
            }

            jobDialog.Dispose();
        }

        void RemoveJobs()
        {
            if (jobsListView.SelectedIndices.Count > 0)
            {
                if (Util.MsgBox(Util.translationList["000171"],
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {
                    Cursor temp = Cursor.Current;

                    Cursor.Current = Cursors.WaitCursor;

                    List<int> deleteFromJobList = new List<int>();

                    foreach (int i in jobsListView.SelectedIndices)
                    {
                        deleteFromJobList.Add(i);
                    }

                    jobsListView.SelectedIndices.Clear();
                    deleteFromJobList.Sort();

                    int num = 0;

                    jobsListView.BeginUpdate();

                    for (int i = deleteFromJobList.Count - 1; i >= 0; i--)
                    {
                        Job j = (Job)((ListViewItem)jobsListView.Items[deleteFromJobList[i]]).Tag;

                        if (num == 0 || j.Num < num)
                        {
                            num = j.Num;
                        }

                        StopJob(j);
                        jobs.Remove(j);
                        jobsListView.Items.RemoveAt(deleteFromJobList[i]);
                    }

                    if (num > 0)
                    {
                        for (int k = num - 1; k < jobs.Count; k++)
                        {
                            jobs[k].Num = k + 1;
                        }

                        SaveJobsAsync();
                    }

                    jobsListView.EndUpdate();
                    Cursor.Current = temp;
                }
            }
        }

        private void jobsListView_DoubleClick(object sender, EventArgs e)
        {
            if (bool.Parse(Util.GetSetting("ListViewDoubleClickOpen")))
            {
                OpenDownloadedFile();
            }
            else
            {
                EditJob();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                #region determine half second interval

                bool isHalfSecond = (timerHalfSecCount == 0);

                timerHalfSecCount++;

                if (timerHalfSecCount == 5)
                {
                    timerHalfSecCount = 0;
                }
                #endregion

                //
                // 0 = No sound
                // 1 = Exclamation (each download completed)
                // 2 = Asterisk (all downloads completed)
                //
                int notificationSound = 0;

                #region update the log pane

                if (updateLog)
                {
                    if (jobsListView.SelectedIndices.Count == 1)
                    {
                        Job j = (Job)jobsListView.Items[jobsListView.SelectedIndices[0]].Tag;

                        if (selectedJob != j)
                        {
                            logTextBox.Clear();
                            logPos = 0;
                            selectedJob = j;
                        }
                        else if (j.LogBegin)
                        {
                            logTextBox.Clear();
                            logPos = 0;
                            j.LogBegin = false;
                        }

                        if (logPos < j.LogLength)
                        {
                            StringBuilder sb = new StringBuilder(j.Log);

                            sb.Remove(0, logPos);

                            // Limit appending to no more than 64K at a time to prevent halt.
                            if (sb.Length > 65536)
                            {
                                string s = sb.Remove(65536, sb.Length - 65536).ToString();
                                int lastNewLine = s.LastIndexOf(Environment.NewLine);

                                if (lastNewLine > 0)
                                {
                                    s = s.Substring(0, lastNewLine);
                                }

                                logTextBox.AppendText(s);
                                logPos += s.Length;
                            }
                            else
                            {
                                logTextBox.AppendText(sb.ToString());
                                logPos += sb.Length;
                            }
                        }
                    }
                    else
                    {
                        logTextBox.Clear();
                        logPos = 0;
                        selectedJob = null;
                    }
                }
                #endregion

                #region start/stop jobs, update speed

                int runningCount = 0;

                for (int i = 0; i < jobs.Count; i++)
                {
                    Job j = jobs[i];

                    if (maxRunningJobs == 0 || runningCount < maxRunningJobs)
                    {
                        if (j.Queued)
                        {
                            if (IsWaitRetry(j))
                            {
                                j.RetryCount++;
                            }

                            j.Start();
                            j.Queued = false;
                            runningCount++;
                            downloadsCompleted = false;
                        }
                        else if (j.IsRunning() || IsWaitRetry(j))
                        {
                            runningCount++;
                        }
                    }
                    else if (j.IsRunning())
                    {
                        j.Stop();
                        j.Queued = true;
                    }
                    else if (IsWaitRetry(j))
                    {
                        j.Kill();
                        j.Queued = true;
                    }

                    if (isHalfSecond)
                    {
                        j.UpdateSpeed();
                    }
                }
                #endregion

                #region Prevent downloading same file at the same time.

                for (int i = 0; i < jobs.Count; i++)
                {
                    Job j = jobs[i];

                    if (j.IsRunning()
                        && j.LocalFileChecker != ""
                        && j.LocalFileCheckerChanged)
                    {
                        int index;

                        for (index = 0; index < jobs.Count; index++)
                        {
                            if (index == i)
                            {
                                continue;
                            }

                            Job job = jobs[index];

                            if (job.IsRunning()
                                && job.LocalFileChecker == j.LocalFileChecker)
                            {
                                break;
                            }
                        }

                        if (index < jobs.Count)
                        {
                            string errorMessage = "Downloading same file at the same time is not permitted!";

                            if (index < i)
                            {
                                j.Stop(errorMessage);
                            }
                            else
                            {
                                jobs[index].Stop(errorMessage);
                            }
                        }
                        else
                        {
                            j.LocalFileChecked = true;
                            j.LocalFileCheckerChanged = false;
                        }
                    }
                }
                #endregion

                EnableDisableToolbarButtonsAndMenuItems();

                if (isHalfSecond)
                {
                    jobsListView.UpdateInfo();
                }

                runningCount = 0;

                #region count jobs, queue jobs, prepare for speed limit

                int queuedCount = 0;
                int finishedCount = 0;
                int stoppedCount = 0;
                int waitRetryCount = 0;
                long totalSpeed = 0;
                long totalSize = 0;
                int totalSizeCount = 0;
                List<Job> speedLimitJobList = new List<Job>();
                List<Job> runningJobs = new List<Job>();
                List<Job> toBeRemoved = new List<Job>();

                for (int i = 0; i < jobs.Count; i++)
                {
                    Job j = jobs[i];
                    JobStatus js = j.GetStatus();

                    if (j.Size > 0)
                    {
                        totalSize += j.Size;
                        totalSizeCount++;
                    }

                    if (js == JobStatus.Queued)
                    {
                        queuedCount++;
                    }
                    else if (js == JobStatus.Running)
                    {
                        runningCount++;
                        runningJobs.Add(j);
                    }
                    else if (js == JobStatus.Retrieving)
                    {
                        runningCount++;
                        runningJobs.Add(j);

                        long l = j.Speed;

                        if (l > 0)
                        {
                            totalSpeed += l;
                            speedLimitJobList.Add(j);
                        }
                    }
                    else if (js == JobStatus.Stopped)
                    {
                        stoppedCount++;

                        if (IsWaitRetry(j))
                        {
                            waitRetryCount++;

                            if (j.LastStoppedTime == -1)
                            {
                                j.LastStoppedTime = DateTime.Now.Ticks;
                            }
                            else if (DateTime.Now.Ticks - j.LastStoppedTime
                                    > timeBetweenRetryAttempts * 10000000)
                            {
                                j.Queued = true;
                            }
                        }
                        else if (j.IsScheduled)
                        {
                            j.Queued = true;
                        }
                    }
                    else if (js == JobStatus.Finished)
                    {
                        finishedCount++;
                        
                        if (!j.DownloadComplete)
                        {
                            notificationSound = 1;
                            j.DownloadComplete = true;
                        }

                        if (j.ShowBalloon)
                        {
                            if (showBalloonTip)
                            {
                                if (j.Urls.Count > 0)
                                {
                                    notifyIcon1.Tag = j;
                                    notifyIcon1.ShowBalloonTip(30000, Util.translationList["000236"], String.Format(Util.translationList["000237"], j.GetNotifyJobName()), ToolTipIcon.Info);
                                }
                            }

                            j.ShowBalloon = false;
                        }

                        if (j.IsScheduled)
                        {
                            j.Queued = true;
                        }

                        if (toolsAutoRemoveFinishedJobMenuItem.Checked)
                        {
                            toBeRemoved.Add(j);
                        }
                    }
                    else // Ready
                    {
                        if (j.IsScheduled)
                        {
                            j.Queued = true;
                        }
                    }
                }

                if (toBeRemoved.Count > 0)
                {
                    for (int i = 0; i < toBeRemoved.Count; i++)
                    {
                        Job j = toBeRemoved[i];
                        int num = j.Num;

                        jobs.Remove(j);

                        int index = -1;

                        foreach (ListViewItem lvi in jobsListView.Items)
                        {
                            if (lvi.Tag == j)
                            {
                                index = lvi.Index;

                                break;
                            }
                        }

                        if (index >= 0)
                        {
                            jobsListView.Items.RemoveAt(index);
                        }

                        for (int k = num - 1; k < jobs.Count; k++)
                        {
                            jobs[k].Num = k + 1;
                        }
                    }

                    SaveJobsAsync();
                }
                #endregion

                #region status bar, tray icon text

                string statusBarText = string.Format("{8} {9} ({10}) :: {0} {1} :: {2} {3} :: {6} {7} :: {4} {5}",
                    runningCount, Util.translationList["000157"],
                    queuedCount, Util.translationList["000156"],
                    finishedCount, Util.translationList["000160"],
                    stoppedCount, Util.translationList["000159"],
                    jobs.Count, Util.translationList["000227"],
                    Util.GetSizeText(totalSize)
                    );

                if (statusBarPanel1.Text != statusBarText)
                {
                    statusBarPanel1.Text = statusBarText;
                }

                if (isHalfSecond)
                {
                    string speedText = Util.GetSpeedText(totalSpeed);

                    if (statusBarPanel2.Text != speedText)
                    {
                        statusBarPanel2.Text = speedText;
                        notifyIcon1.Text = Application.ProductName + Environment.NewLine + speedText;
                    }
                }
                #endregion

                #region speed limit

                long speedLimit = (long)Math.Floor(float.Parse(Util.GetSetting("SpeedLimit")) * 1024);

                if (speedLimit > 0)
                {
                    if (runningCount > 0)
                    {
                        long diff = speedLimit - totalSpeed;

                        if (diff < 0)
                        {
                            diff = 0;
                        }

                        List<Job> initList = new List<Job>();

                        for (int i = 0; i < runningJobs.Count; i++)
                        {
                            Job j = runningJobs[i];

                            if (j.LimitRate == 0)
                            {
                                initList.Add(j);
                            }
                        }

                        if (initList.Count > 0)
                        {
                            int r = Util.Rand(initList.Count);
                            long x = diff / initList.Count;
                            long y = diff % initList.Count;

                            for (int i = 0; i < initList.Count; i++)
                            {
                                Job j = initList[i];

                                j.LimitRate = (diff == 0 ? 1 : x + (i == r ? y : 0));
                                j.LimitRateChanged = true;
                            }
                        }
                    }

                    if (speedLimitJobList.Count > 0)
                    {
                        int r = Util.Rand(speedLimitJobList.Count);
                        long x = (totalSpeed - speedLimit) / speedLimitJobList.Count;
                        long y = (totalSpeed - speedLimit) % speedLimitJobList.Count;

                        for (int i = 0; i < speedLimitJobList.Count; i++)
                        {
                            Job j = speedLimitJobList[i];
                            long adjust = x + (i == r ? y : 0);
                            long rate = j.Speed - (adjust > j.Speed - 1 ? j.Speed - 1 : adjust);

                            if (j.LimitRate != rate)
                            {
                                j.LimitRate = rate;
                                j.LimitRateChanged = true;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < speedLimitJobList.Count; i++)
                    {
                        Job j = speedLimitJobList[i];

                        if (j.LimitRate != 0)
                        {
                            j.LimitRate = 0;
                            j.LimitRateChanged = true;
                        }
                    }
                }
                #endregion

                #region download complete

                if (runningCount + queuedCount + waitRetryCount == 0)
                {
                    if (downloadsCompleted == false)
                    {
                        downloadsCompleted = true;

                        if (bool.Parse(Util.GetSetting("PlayAllDownloadsFinishedSound")))
                        {
                            notificationSound = 2;
                        }

                        //
                        // play sound
                        //
                        if (notificationSound > 0)
                        {
                            if (notificationSound == 1
                                && bool.Parse(Util.GetSetting("PlayDownloadFinishedSound")))
                            {
                                SystemSounds.Exclamation.Play();
                            }
                            else if (notificationSound == 2
                                && bool.Parse(Util.GetSetting("PlayAllDownloadsFinishedSound")))
                            {
                                SystemSounds.Asterisk.Play();
                            }

                            notificationSound = 0;
                        }

                        if (autoShutdownAction != AutoShutdownAction.None)
                        {
                            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedCancelable")))
                            {
                                if (!Visible)
                                {
                                    Show();
                                }

                                if (WindowState == FormWindowState.Minimized)
                                {
                                    Util.ShowWindow(Handle, 9 /* SW_RESTORE */);
                                }

                                SetAsForegroundWindow();

                                AutoShutdownPrompt autoShutdownPrompt = new AutoShutdownPrompt(autoShutdownAction);

                                autoShutdownPrompt.ShowDialog();
                                autoShutdownPrompt.Dispose();
                            }
                            else
                            {
                                Util.DoAutoShutdown(autoShutdownAction);
                            }
                        }
                    }
                }
                else if (downloadsCompleted)
                {
                    downloadsCompleted = false;
                }
                #endregion

                #region clipboard detection

                if (bool.Parse(Util.GetSetting("DetectClipboard")))
                {
                    string clipboardText = Util.GetTextFromClipboard();

                    if (Util.IsDownloadable(clipboardText)
                        && clipboardText != Util.CurrentClipboardText)
                    {
                        Util.CurrentClipboardText = clipboardText;
                        AddNewDownload(Util.ArgsToCmdLineArgs(new string[] { clipboardText }));
                    }
                }
                #endregion

                #region play sound

                if (notificationSound > 0)
                {
                    if (notificationSound == 1
                        && bool.Parse(Util.GetSetting("PlayDownloadFinishedSound")))
                    {
                        SystemSounds.Exclamation.Play();
                    }
                    else if (notificationSound == 2
                        && bool.Parse(Util.GetSetting("PlayAllDownloadsFinishedSound")))
                    {
                        SystemSounds.Asterisk.Play();
                    }

                    notificationSound = 0;
                }
                #endregion

                #region save settings

                if (settingSaved)
                {
                    BackgroundWorker settingWorker = new BackgroundWorker();

                    settingSaved = false;
                    settingWorker.DoWork += new DoWorkEventHandler(settingWorker_DoWork);
                    settingWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(settingWorker_RunWorkerCompleted);
                    settingWorker.RunWorkerAsync();
                }
                #endregion
            }
            catch
            {
            }
        }

        void SaveJobsAsync()
        {
            BackgroundWorker jobListWorker = new BackgroundWorker();
            jobListWorker.DoWork += new DoWorkEventHandler(jobListWorker_DoWork);
            jobListWorker.RunWorkerAsync();
        }

        void jobListWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SaveJobs();
        }

        void settingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            settingSaved = true;
        }

        void settingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.SaveSettings();
        }

        void StartJobs()
        {
            foreach (int i in jobsListView.SelectedIndices)
            {
                Job j = (Job)jobsListView.Items[i].Tag;

                if (IsFullyStopped(j))
                {
                    j.Kill();
                    j.Queued = true;
                    j.RetryCount = 0;
                }
            }
        }

        void StopJob(Job j)
        {
            if (j.IsRunning())
            {
                j.Stop();
            }
            else if (j.Queued)
            {
                j.Queued = false;
            }

            j.Kill();
        }

        void StopJobs()
        {
            foreach (int i in jobsListView.SelectedIndices)
            {
                StopJob((Job)jobsListView.Items[i].Tag);
            }
        }

        void OpenDownloadedFile()
        {
            if (jobsListView.SelectedIndices.Count != 1)
            {
                return;
            }

            Job j = (Job)jobsListView.Items[jobsListView.SelectedIndices[0]].Tag;

            if (!IsFullyStopped(j))
            {
                return;
            }

            string locf = j.LocalFile;

            if (locf != "")
            {
                if (!Path.IsPathRooted(locf))
                {
                    locf = Path.Combine(Path.GetDirectoryName(Util.WgetPath), j.LocalFile);
                }
            }

            if (locf != "" && File.Exists(locf))
            {
                try
                {
                    string[] types = Util.GetSetting("PromptWhenOpenFileTypes").Split(new char[] { ',' });
                    bool prompt = false;

                    for (int i = 0; i < types.Length; i++)
                    {
                        if (locf.EndsWith("." + types[i].Trim()))
                        {
                            prompt = true;
                            break;
                        }

                    }

                    if (prompt)
                    {
                        if (Util.MsgBox(Util.translationList["000211"]
                                , MessageBoxButtons.YesNo
                                , MessageBoxIcon.Warning
                                , MessageBoxDefaultButton.Button2)
                            == DialogResult.Yes)
                        {
                            Process.Start(Util.AddQuotes(locf));
                        }
                    }
                    else
                    {
                        Process.Start(Util.AddQuotes(locf));
                    }
                }
                catch (Exception ex)
                {
                    Util.MsgBox(ex.Message);
                }
            }
            else
            {
                Util.MsgBox(Util.translationList["000212"]);
            }
        }

        void OpenContainingFolder(Job j, ref bool test)
        {
            string locf = j.LocalFile;

            if (locf != "")
            {
                if (!Path.IsPathRooted(locf))
                {
                    locf = Path.Combine(Path.GetDirectoryName(Util.WgetPath), j.LocalFile);
                }
            }

            if (locf != "" && File.Exists(locf))
            {
                if (test)
                {
                    return;
                }

                Process.Start("Explorer", "/n,/select," + Util.AddQuotes(locf));
            }
            else
            {
                try
                {
                    string path;

                    if (j.Opts.ContainsKey("directory-prefix"))
                    {
                        path = j.Opts["directory-prefix"];

                        if (!Path.IsPathRooted(path))
                        {
                            path = Path.Combine(Path.GetDirectoryName(Util.WgetPath), path);
                        }
                    }
                    else
                    {
                        path = Path.GetDirectoryName(Util.WgetPath);
                    }

                    DirectoryInfo di = new DirectoryInfo(path);

                    if (di.Exists)
                    {
                        if (test)
                        {
                            return;
                        }

                        Process.Start("Explorer", Util.AddQuotes(di.FullName));
                    }
                    else
                    {
                        if (test)
                        {
                            test = false;

                            return;
                        }

                        Util.MsgBox(String.Format(Util.translationList["000194"], di.FullName), MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    if (test)
                    {
                        test = false;

                        return;
                    }

                    Util.MsgBox(ex.Message, MessageBoxIcon.Error);
                }
            }
        }

        bool TestOpenContainingFolder(Job j)
        {
            bool test = true;

            OpenContainingFolder(j, ref test);

            return test;
        }

        void OpenContainingFolder(Job j)
        {
            bool test = false;

            OpenContainingFolder(j, ref test);
        }

        void OpenContainingFolder()
        {
            if (jobsListView.SelectedIndices.Count == 1)
            {
                OpenContainingFolder((Job)jobsListView.Items[jobsListView.SelectedIndices[0]].Tag);
            }
        }

        void MoveJobsUp()
        {
            if (jobsListView.SelectedIndices.Count > 0)
            {
                Cursor temp = Cursor.Current;

                Cursor.Current = Cursors.WaitCursor;

                List<int> selectedNums = new List<int>();

                foreach (int i in jobsListView.SelectedIndices)
                {
                    selectedNums.Add(((Job)jobsListView.Items[i].Tag).Num);
                }

                for (int i = 1; i < jobs.Count; i++)
                {
                    if (!selectedNums.Contains(i + 1))
                    {
                        continue;
                    }

                    bool done = false;

                    foreach (int j in selectedNums)
                    {
                        if (j - 1 == i)
                        {
                            continue;
                        }

                        if (j - 1 == i - 1)
                        {
                            done = true;

                            break;
                        }
                    }

                    if (!done)
                    {
                        Job tmp = jobs[i];

                        jobs[i] = jobs[i - 1];
                        jobs[i - 1] = tmp;
                        jobs[i].Num = i + 1;
                        jobs[i - 1].Num = i;

                        selectedNums.Remove(i + 1);
                        selectedNums.Add(i);
                    }
                }

                SaveJobsAsync();

                jobsListView.UpdateInfo();
                jobsListView.Sort();
                Cursor.Current = temp;
            }
        }

        void MoveJobsDown()
        {
            if (jobsListView.SelectedIndices.Count > 0)
            {
                Cursor temp = Cursor.Current;

                Cursor.Current = Cursors.WaitCursor;

                List<int> selectedNums = new List<int>();

                foreach (int i in jobsListView.SelectedIndices)
                {
                    selectedNums.Add(((Job)jobsListView.Items[i].Tag).Num);
                }

                for (int i = jobs.Count - 2; i >= 0; i--)
                {
                    if (!selectedNums.Contains(i + 1))
                    {
                        continue;
                    }

                    bool done = false;

                    foreach (int j in selectedNums)
                    {
                        if (j - 1 == i)
                        {
                            continue;
                        }

                        if (j - 1 == i + 1)
                        {
                            done = true;

                            break;
                        }
                    }

                    if (!done)
                    {
                        Job tmp = jobs[i];

                        jobs[i] = jobs[i + 1];
                        jobs[i + 1] = tmp;
                        jobs[i].Num = i + 1;
                        jobs[i + 1].Num = i + 2;

                        selectedNums.Remove(i + 1);
                        selectedNums.Add(i + 2);
                    }
                }

                SaveJobsAsync();

                jobsListView.UpdateInfo();
                jobsListView.Sort();
                Cursor.Current = temp;
            }
        }

        void SetDefaultDownloadOptions()
        {
            string cmdLineArgs = Util.GetSetting("DefDlOptions");

            cmdLineArgs = cmdLineArgs.Trim();

            StringCollection urls;
            StringDictionary opts;

            Util.GetOpt(cmdLineArgs, out urls, out opts);

            JobDialog jobDialog = new JobDialog(urls, opts);

            jobDialog.Text = "Default Download Properties";

            jobDialog.ShowStartOnOk = false;
            jobDialog.ShowOutputDocument = false;
            jobDialog.ShowNote = false;
            jobDialog.EnableUrls = false;
            jobDialog.ShowReferer = false;

            jobDialog.AutoStartNumDays = int.Parse(Util.GetSetting("DefDlAutoStartNumDays"));
            jobDialog.AutoStartNumHours = int.Parse(Util.GetSetting("DefDlAutoStartNumHours"));

            if (jobDialog.ShowDialog(this) == DialogResult.OK)
            {
                Util.PutSetting("DefDlOptions", jobDialog.GetOptsString());
                Util.PutSetting("DefDlAutoStartNumDays", jobDialog.AutoStartNumDays.ToString());
                Util.PutSetting("DefDlAutoStartNumHours", jobDialog.AutoStartNumHours.ToString());
            }

            jobDialog.Dispose();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized
                && bool.Parse(Util.GetSetting("MinimizeToTray")))
            {
                if (formLoaded)
                {
                    Hide();
                }
            }
        }

        private void jobsNewMenuItem_Click(object sender, EventArgs e)
        {
           // NewJob("");
            SheetDialog sheetDlg = new SheetDialog();
            sheetDlg.StartPosition = FormStartPosition.CenterParent;
            sheetDlg.ShowDialog();
        }

        private void jobsEditMenuItem_Click(object sender, EventArgs e)
        {
            EditJob();
        }

        private void jobsRemoveMenuItem_Click(object sender, EventArgs e)
        {
            RemoveJobs();
        }

        private void jobsStartMenuItem_Click(object sender, EventArgs e)
        {
            StartJobs();
        }

        private void jobsStopMenuItem_Click(object sender, EventArgs e)
        {
            StopJobs();
        }

        private void jobsOpenContainingFolderMenuItem_Click(object sender, EventArgs e)
        {
            OpenContainingFolder();
        }

        private void jobsMoveUpMenuItem_Click(object sender, EventArgs e)
        {
            MoveJobsUp();
        }

        private void jobsMoveDownMenuItem_Click(object sender, EventArgs e)
        {
            MoveJobsDown();
        }

        private void jobsQuitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewToolbarMenuItem_Click(object sender, EventArgs e)
        {
            viewToolbarMenuItem.Checked = !viewToolbarMenuItem.Checked;
            toolBar1.Visible = viewToolbarMenuItem.Checked;
        }

        private void viewStatusBarMenuItem_Click(object sender, EventArgs e)
        {
            viewStatusBarMenuItem.Checked = !viewStatusBarMenuItem.Checked;
            statusBar1.Visible = viewStatusBarMenuItem.Checked;
        }

        void SetPreferences()
        {
            PreferencesDialog preferencesDialog = new PreferencesDialog();

            preferencesDialog.ShowDialog(this);
            preferencesDialog.Dispose();

            for (int i = 0; i < this.jobs.Count; i++)
            {
                this.jobs[i].Size = this.jobs[i].Size;
            }

            maxRunningJobs = decimal.Parse(Util.GetSetting("MaxRunningJobs"));
            showBalloonTip = bool.Parse(Util.GetSetting("ShowBalloonTip"));
            retry = bool.Parse(Util.GetSetting("Retry"));
            retryAttempts = decimal.Parse(Util.GetSetting("RetryAttempts"));
            timeBetweenRetryAttempts = Util.GetTimeBetweenRetryAttempts();

            if (!Util.InitOpts())
            {
                Util.MsgBox(Util.translationList["000188"], MessageBoxIcon.Error);
                Application.Exit();

                return;
            }
        }

        private void toolsPreferencesMenuItem_Click(object sender, EventArgs e)
        {
            SetPreferences();
        }

        private void toolsDefaultDownloadOptionsMenuItem_Click(object sender, EventArgs e)
        {
            SetDefaultDownloadOptions();
        }

        private void toolsWhenDownloadsFinishedMenuItem_Popup(object sender, EventArgs e)
        {
            toolsWhenDownloadsFinishedDisabledMenuItem.Checked = false;
            toolsWhenDownloadsFinishedQuitMenuItem.Checked = false;
            toolsWhenDownloadsFinishedStandByMenuItem.Checked = false;
            toolsWhenDownloadsFinishedHibernateMenuItem.Checked = false;
            toolsWhenDownloadsFinishedRestartMenuItem.Checked = false;
            toolsWhenDownloadsFinishedTurnOffMenuItem.Checked = false;
            toolsWhenDownloadsFinishedLogOffMenuItem.Checked = false;
            toolsWhenDownloadsFinishedLockComputerMenuItem.Checked = false;

            if (autoShutdownAction == AutoShutdownAction.None)
            {
                toolsWhenDownloadsFinishedDisabledMenuItem.Checked = true;
            }
            else if (autoShutdownAction == AutoShutdownAction.Quit)
            {
                toolsWhenDownloadsFinishedQuitMenuItem.Checked = true;
            }
            else if (autoShutdownAction == AutoShutdownAction.StandBy)
            {
                toolsWhenDownloadsFinishedStandByMenuItem.Checked = true;
            }
            else if (autoShutdownAction == AutoShutdownAction.Hibernate)
            {
                toolsWhenDownloadsFinishedHibernateMenuItem.Checked = true;
            }
            else if (autoShutdownAction == AutoShutdownAction.TurnOff)
            {
                toolsWhenDownloadsFinishedTurnOffMenuItem.Checked = true;
            }
            else if (autoShutdownAction == AutoShutdownAction.Restart)
            {
                toolsWhenDownloadsFinishedRestartMenuItem.Checked = true;
            }
            else if (autoShutdownAction == AutoShutdownAction.LogOff)
            {
                toolsWhenDownloadsFinishedLogOffMenuItem.Checked = true;
            }
            else if (autoShutdownAction == AutoShutdownAction.LockComputer)
            {
                toolsWhenDownloadsFinishedLockComputerMenuItem.Checked = true;
            }
        }

        private void toolsWhenDownloadsFinishedDisabledMenuItem_Click(object sender, EventArgs e)
        {
            autoShutdownAction = AutoShutdownAction.None;

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
        }

        private void toolsWhenDownloadsFinishedQuitMenuItem_Click(object sender, EventArgs e)
        {
            autoShutdownAction = AutoShutdownAction.Quit;

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
        }

        private void toolsWhenDownloadsFinishedStandByMenuItem_Click(object sender, EventArgs e)
        {
            autoShutdownAction = AutoShutdownAction.StandBy;

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
        }

        private void toolsWhenDownloadsFinishedHibernateMenuItem_Click(object sender, EventArgs e)
        {
            autoShutdownAction = AutoShutdownAction.Hibernate;

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
        }

        private void toolsWhenDownloadsFinishedTurnOffMenuItem_Click(object sender, EventArgs e)
        {
            autoShutdownAction = AutoShutdownAction.TurnOff;

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
        }

        private void toolsWhenDownloadsFinishedRestartMenuItem_Click(object sender, EventArgs e)
        {
            autoShutdownAction = AutoShutdownAction.Restart;

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
        }

        private void toolsWhenDownloadsFinishedLogOffMenuItem_Click(object sender, EventArgs e)
        {
            autoShutdownAction = AutoShutdownAction.LogOff;

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
        }

        private void toolsWhenDownloadsFinishedLockComputerMenuItem_Click(object sender, EventArgs e)
        {
            autoShutdownAction = AutoShutdownAction.LockComputer;

            if (bool.Parse(Util.GetSetting("WhenDownloadsFinishedFreeze")))
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
        }

        private void helpAboutVisualWgetMenuItem_Click(object sender, EventArgs e)
        {
            Util.MsgBox(Util.GetAssemblyTitle() + " Version " + Application.ProductVersion + "\n" +
                Util.GetAssemblyCopyright() + "\n" +
                "© 2016 Enesy, Engineering Education System"
                );
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            string s = e.Button.Name;

            if (s == "newToolBarButton")
            {
                //NewJob("");
                SheetDialog sheetDlg = new SheetDialog();
                sheetDlg.StartPosition = FormStartPosition.CenterParent;
                sheetDlg.ShowDialog();
            }
            else if (s == "newMultipleToolBarButton")
            {
                //NewMultipleJobs();
                SheetDialog sheetDlg = new SheetDialog();
                sheetDlg.StartPosition = FormStartPosition.CenterParent;
                sheetDlg.ShowDialog();
            }
            else if (s == "editToolBarButton")
            {
                EditJob();
            }
            else if (s == "removeToolBarButton")
            {
                RemoveJobs();
            }
            else if (s == "startToolBarButton")
            {
                StartJobs();
            }
            else if (s == "stopToolBarButton")
            {
                StopJobs();
            }
            else if (s == "openContainingFolderToolBarButton")
            {
                OpenContainingFolder();
            }
            else if (s == "moveUpToolBarButton")
            {
                MoveJobsUp();
            }
            else if (s == "moveDownToolBarButton")
            {
                MoveJobsDown();
            }
            else if (s == "defaultDownloadOptionsToolBarButton")
            {
                SetDefaultDownloadOptions();
            }
            else if (s == "preferencesToolBarButton")
            {
                SetPreferences();
            }
            else if (s == "speedLimitToolBarButton")
            {
                List<MenuItem> menuItems = GetSpeedLimitMenuItems(true);
                ContextMenu contextMenu = new ContextMenu(menuItems.ToArray());

                contextMenu.Show(e.Button.Parent, new Point(e.Button.Rectangle.X, e.Button.Rectangle.Y + 22));
            }
        }

#if false // removed
        private void t_stopAllJobsMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < jobsListView.Items.Count; i++)
            {
                StopJob((Job)jobsListView.Items[i].Tag);
            }
        }

        private void t_exitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }
#endif

        bool isToolBarButtonPressing = false;

        private void toolBar1_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                isToolBarButtonPressing = false;

                foreach (ToolBarButton tbb in toolBar1.Buttons)
                {
                    if (tbb.Style != ToolBarButtonStyle.Separator && tbb.Enabled && tbb.Rectangle.Contains(toolBar1.PointToClient(MousePosition)))
                    {
                        isToolBarButtonPressing = true;

                        break;
                    }
                }
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (notifyIcon1.Tag != null)
            {
                OpenContainingFolder((Job)notifyIcon1.Tag);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Util.TrimCurrentProcessWorkingSet();

            if (bool.Parse(Util.GetSetting("AlwaysShowTrayIcon")))
            {
                notifyIcon1.Visible = true;
            }
            else
            {
                notifyIcon1.Visible = !Visible;
            }

            if (bool.Parse(Util.GetSetting("CheckForUpdates")))
            {
                bool b = false;
                long l = long.Parse(Util.GetSetting("CheckForUpdatesTime"));

                if (l == -1)
                {
                    b = true;
                }
                else
                {
                    long now = DateTime.Now.Ticks;

                    Debug.Assert(now - l > 0);

                    TimeSpan ts = new TimeSpan(now - l);
                    int i = int.Parse(Util.GetSetting("CheckForUpdatesInterval"));

                    if (i == 0)
                    {
                        if (ts.TotalDays >= 1)
                        {
                            b = true;
                        }
                    }
                    else if (i == 1)
                    {
                        if (ts.TotalDays >= 7)
                        {
                            b = true;
                        }
                    }
                    else
                    {
                        Debug.Assert(false);
                    }
                }

                if (b)
                {
                    if (CheckForUpdates(true))
                    {
                        DateTime now = DateTime.Now;

                        Util.PutSetting("CheckForUpdatesTime", (new DateTime(now.Year, now.Month, now.Day)).Ticks.ToString(), true);
                    }
                }
            }

            if (Util.CmdLineArgs != "")
            {
                NewJob(Util.CmdLineArgs, bool.Parse(Util.GetSetting("NoPromptOnNewJob")));
            }
        }

        bool CheckForUpdates(bool auto)
        {
            string link = QuickPrint.Properties.Resources.updinfoLink;
            int count = 0;

            while (true)
            {
                string tempPath = Path.GetTempPath();

                do
                {
                    Util.CheckForUpdatesTemp = Path.Combine(tempPath, Guid.NewGuid().ToString() + ".tmp");

                } while (File.Exists(Util.CheckForUpdatesTemp));

                Job j = new Job(string.Format("--no-cache --output-document=\"{0}\" {1}", Util.CheckForUpdatesTemp, link));
                Cursor tmp = Cursor;

                Cursor = Cursors.WaitCursor;
                j.Start(false, true, false);

                if (!j.WaitForExit(5000))
                {
                    j.Stop();
                }

                Cursor = tmp;

                string updinfo;

                try
                {
                    updinfo = File.ReadAllText(Util.CheckForUpdatesTemp);
                }
                catch
                {
                    if (!auto)
                    {
                        Util.MsgBox(Util.translationList["000173"], MessageBoxIcon.Exclamation);
                    }

                    return false;
                }

                string[] segments = Util.CmdLineArgsToArgs(updinfo);

                if (segments.Length < 2)
                {
                    if (!auto)
                    {
                        Util.MsgBox(Util.translationList["000173"], MessageBoxIcon.Exclamation);
                    }

                    return false;
                }

                Version version;

                try
                {
                    version = new Version(segments[0]);
                }
                catch
                {
                    if (!auto)
                    {
                        Util.MsgBox(Util.translationList["000173"], MessageBoxIcon.Exclamation);
                    }

                    return false;
                }

                link = segments[1];

                if (version.Major == 0 && version.Minor == 0)
                {
                    count++;

                    if (count == 2)
                    {
                        if (!auto)
                        {
                            Util.MsgBox(Util.translationList["000173"], MessageBoxIcon.Exclamation);
                        }

                        return false;
                    }

                    continue;
                }

                if ((new Version(Application.ProductVersion)).CompareTo(version) < 0)
                {
                    if (Util.MsgBox(Util.translationList["000174"],
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        Process.Start(link);
                    }
                }
                else if (!auto)
                {
                    Util.MsgBox(Util.translationList["000175"]);
                }

                return true;
            }
        }

        bool CheckForUpdates()
        {
            return CheckForUpdates(false);
        }

        private void helpCheckForUpdatesMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckForUpdates())
            {
                DateTime now = DateTime.Now;

                Util.PutSetting("CheckForUpdatesTime", (new DateTime(now.Year, now.Month, now.Day)).Ticks.ToString(), true);
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            // *** Fix ***
            // The exception, "ContextMenu cannot be shown on an invisible control.",
            // can get thrown when using MSVDM.
            if (!Visible) Show();

            if (bool.Parse(Util.GetSetting("AlwaysShowTrayIcon")))
            {
                notifyIcon1.Visible = true;
            }
            else
            {
                notifyIcon1.Visible = !Visible;
            }
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!formClosing)
            {
                if (bool.Parse(Util.GetSetting("AlwaysShowTrayIcon")))
                {
                    notifyIcon1.Visible = true;
                }
                else
                {
                    notifyIcon1.Visible = !Visible;
                }
            }

            if (Visible)
            {
                if (WindowState == FormWindowState.Normal
                    && !Screen.FromPoint(MousePosition).WorkingArea.Contains(Bounds))
                {
                    CenterToScreen();
                }

                timer1.Interval = 100;
            }
            else
            {
                timer1.Interval = 200;
                Util.TrimCurrentProcessWorkingSet();
            }
        }

        private void jobsListViewContextMenu_Popup(object sender, EventArgs e)
        {
            Point pt = jobsListView.PointToClient(MousePosition);

            if (jobsListView.SelectedIndices.Count > 0
                && jobsListView.TopItem != null
                && (jobsListView.TopItem.Bounds.Top <= pt.Y || pt.Y < 0))
            {
                foreach (MenuItem mi in jobsListViewContextMenu.MenuItems)
                {
                    mi.Visible = (mi.Tag != null && mi.Tag.ToString() == "j");
                }
            }
            else
            {
                int top;

                if (jobsListView.TopItem == null)
                {
                    jobsListView.Items.Add("");
                    top = jobsListView.TopItem.Bounds.Top;
                    jobsListView.Items.RemoveAt(0);
                }
                else
                {
                    top = jobsListView.TopItem.Bounds.Top;
                }

                if (0 <= pt.Y && pt.Y < top)
                {
                    foreach (MenuItem mi in jobsListViewContextMenu.MenuItems)
                    {
                        mi.Visible = (mi.Tag != null && mi.Tag.ToString() == "h");
                    }

                    jlv_h_nameMenuItem.Checked = (nameColumnHeader.Width != 0);
                    jlv_h_numMenuItem.Checked = (numColumnHeader.Width != 0);
                    jlv_h_sizeMenuItem.Checked = (sizeColumnHeader.Width != 0);
                    jlv_h_doneMenuItem.Checked = (doneColumnHeader.Width != 0);
                    jlv_h_statusMenuItem.Checked = (statusColumnHeader.Width != 0);
                    jlv_h_speedMenuItem.Checked = (speedColumnHeader.Width != 0);
                    jlv_h_etaMenuItem.Checked = (etaColumnHeader.Width != 0);
                    jlv_h_noteMenuItem.Checked = (noteColumnHeader.Width != 0);
                }
                else
                {
                    foreach (MenuItem mi in jobsListViewContextMenu.MenuItems)
                    {
                        mi.Visible = false;
                    }
                }
            }
        }

        private void jlv_j_openContainingFolderMenuItem_Click(object sender, EventArgs e)
        {
            OpenContainingFolder();
        }

        private void jlv_j_startMenuItem_Click(object sender, EventArgs e)
        {
            StartJobs();
        }

        private void jlv_j_stopMenuItem_Click(object sender, EventArgs e)
        {
            StopJobs();
        }

        private void jlv_j_moveUpMenuItem_Click(object sender, EventArgs e)
        {
            MoveJobsUp();
        }

        private void jlv_j_moveDownMenuItem_Click(object sender, EventArgs e)
        {
            MoveJobsDown();
        }

        private void jlv_j_removeMenuItem_Click(object sender, EventArgs e)
        {
            RemoveJobs();
        }

        private void jlv_j_editMenuItem_Click(object sender, EventArgs e)
        {
            EditJob();
        }

        private void toolsComputeMD5HashMenuItem_Click(object sender, EventArgs e)
        {
            if (computeHashDone)
            {
                openFileDialog1.Filter = "All Files (*.*)|*.*";

                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    BackgroundWorker computeMD5HashWorker = new BackgroundWorker();

                    computeHashDone = toolsComputeSHA1HashMenuItem.Enabled = toolsComputeMD5HashMenuItem.Enabled = false;
                    computeMD5HashWorker.DoWork += new DoWorkEventHandler(computeMD5HashWorker_DoWork);
                    computeMD5HashWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(computeMD5HashWorker_RunWorkerCompleted);
                    computeMD5HashWorker.RunWorkerAsync(openFileDialog1.FileName);
                }
            }
        }

        void computeMD5HashWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string s = e.Result.ToString();

            if (s.StartsWith(Util.translationList["000176"]))
            {
                Util.MsgBox(s, MessageBoxIcon.Error);
            }
            else
            {
                Util.MsgBox(string.Format(Util.translationList["000178"], s));
            }

            computeHashDone = toolsComputeSHA1HashMenuItem.Enabled = toolsComputeMD5HashMenuItem.Enabled = true;
        }

        void computeMD5HashWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = string.Format("MD5 Hash: {0}\n\n{1}", Util.ComputeMD5Hash(e.Argument.ToString()), e.Argument.ToString());
            }
            catch
            {
                e.Result = string.Format("{0}\n\n{1}", Util.translationList["000176"], e.Argument.ToString());
            }
        }

        private void toolsComputeSHA1HashMenuItem_Click(object sender, EventArgs e)
        {
            if (computeHashDone)
            {
                openFileDialog1.Filter = "All Files (*.*)|*.*";

                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    BackgroundWorker computeSHA1HashWorker = new BackgroundWorker();

                    computeHashDone = toolsComputeSHA1HashMenuItem.Enabled = toolsComputeMD5HashMenuItem.Enabled = false;
                    computeSHA1HashWorker.DoWork += new DoWorkEventHandler(computeSHA1HashWorker_DoWork);
                    computeSHA1HashWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(computeSHA1HashWorker_RunWorkerCompleted);
                    computeSHA1HashWorker.RunWorkerAsync(openFileDialog1.FileName);
                }
            }
        }

        void computeSHA1HashWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string s = e.Result.ToString();

            if (s.StartsWith(Util.translationList["000177"]))
            {
                Util.MsgBox(s, MessageBoxIcon.Error);
            }
            else
            {
                Util.MsgBox(string.Format(Util.translationList["000178"], s));
            }

            computeHashDone = toolsComputeSHA1HashMenuItem.Enabled = toolsComputeMD5HashMenuItem.Enabled = true;
        }

        void computeSHA1HashWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = string.Format("SHA1 Hash: {0}\n\n{1}", Util.ComputeSHA1Hash(e.Argument.ToString()), e.Argument.ToString());
            }
            catch
            {
                e.Result = string.Format("{0}\n\n{1}", Util.translationList["000177"], e.Argument.ToString());
            }
        }

        private void viewLogMenuItem_Click(object sender, EventArgs e)
        {
            viewLogMenuItem.Checked = !viewLogMenuItem.Checked;
            splitter1.Visible = logTextBox.Visible = viewLogMenuItem.Checked;
        }

        private void logTextBox_Enter(object sender, EventArgs e)
        {
            updateLog = false;
            logTextBox.BackColor = Color.FromKnownColor(KnownColor.Control);
            logTextBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
        }

        private void logTextBox_Leave(object sender, EventArgs e)
        {
            updateLog = true;
            logTextBox.BackColor = Color.FromKnownColor(KnownColor.Info);
            logTextBox.ForeColor = Color.FromKnownColor(KnownColor.InfoText);
        }

        public void SetAsForegroundWindow()
        {
            IntPtr lastActivePopup = Util.GetLastActivePopup(Handle);

            if (Util.GetForegroundWindow() == lastActivePopup)
            {
                return;
            }

            long t = DateTime.Now.Ticks;

            while (t + 0.5 * 10000000 > DateTime.Now.Ticks
                && !Util.SetForegroundWindow(lastActivePopup))
            {
                Thread.Sleep(10);
            }
        }

        void ExportJobsAsWgetBatchFile()
        {
            if (jobsListView.SelectedIndices.Count > 0)
            {
                saveFileDialog1.Filter = "Batch Files (*.bat)|*.bat";

                if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    List<string> lines = new List<string>();

                    lines.Add("@echo off");
                    lines.Add("rem Wget executable must be either a) in PATH, or b) in the same directory as this batch file.");

                    List<int> exportFromJobList = new List<int>();

                    foreach (int i in jobsListView.SelectedIndices)
                    {
                        exportFromJobList.Add(i);
                    }

                    exportFromJobList.Sort();

                    for (int i = 0; i < exportFromJobList.Count; i++)
                    {
                        Job j = (Job)((ListViewItem)jobsListView.Items[exportFromJobList[i]]).Tag;

                        lines.Add("wget " + j.GetCmdLineArgs().Replace("%", "%%"));
                    }

                    lines.Add("pause");

                    File.WriteAllLines(saveFileDialog1.FileName, lines.ToArray(), Encoding.Default);
                }
            }
        }

        private void jobsCleanupMenuItem_Click(object sender, EventArgs e)
        {
            CleanupJobs();
        }

        void CleanupJobs()
        {
            if (Util.MsgBox(Util.translationList["000172"],
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
            {
                Cursor temp = Cursor.Current;

                Cursor.Current = Cursors.WaitCursor;

                List<int> deleteFromJobList = new List<int>();

                for (int i = 0; i < jobsListView.Items.Count; i++)
                {
                    Job j = (Job)((ListViewItem)jobsListView.Items[i]).Tag;

                    if (j.GetStatus() == JobStatus.Finished)
                    {
                        deleteFromJobList.Add(i);
                    }
                }

                deleteFromJobList.Sort();

                int num = 0;

                jobsListView.BeginUpdate();

                for (int i = deleteFromJobList.Count - 1; i >= 0; i--)
                {
                    Job j = (Job)((ListViewItem)jobsListView.Items[deleteFromJobList[i]]).Tag;

                    if (num == 0 || j.Num < num)
                    {
                        num = j.Num;
                    }

                    StopJob(j);
                    jobs.Remove(j);
                    jobsListView.Items.RemoveAt(deleteFromJobList[i]);
                }

                if (num > 0)
                {
                    for (int k = num - 1; k < jobs.Count; k++)
                    {
                        jobs[k].Num = k + 1;
                    }

                    SaveJobsAsync();
                }

                jobsListView.EndUpdate();
                Cursor.Current = temp;
            }
        }

        private void jobsNewMultipleMenuItem_Click(object sender, EventArgs e)
        {
            NewMultipleJobs();
        }

        private void NewMultipleJobs()
        {

            NewMultipleJobsDialog newMultipleJobsDialog = new NewMultipleJobsDialog();

            if (newMultipleJobsDialog.ShowDialog(this) == DialogResult.OK)
            {
                newMultipleJobsDialog.Hide();
                Refresh();

                Cursor temp = Cursor.Current;

                Cursor.Current = Cursors.WaitCursor;

                List<ListViewItem> addToJobList = new List<ListViewItem>();

                if (newMultipleJobsDialog.UrlsList.Length > 0)
                {
                    jobsListView.SelectedIndices.Clear();
                }

                foreach (string urls in newMultipleJobsDialog.UrlsList)
                {
                    string cmdLineArgs = urls.Trim();

                    if (cmdLineArgs == "")
                    {
                        continue;
                    }

                    cmdLineArgs = Util.GetSetting("DefDlOptions") + " " + cmdLineArgs;
                    cmdLineArgs = cmdLineArgs.Trim();

                    Job job = new Job(cmdLineArgs);

                    job.Num = jobs.Count + 1;

                    DateTime now = DateTime.Now;

                    job.LastStartedTime = (new DateTime(now.Year, now.Month, now.Day)).Ticks;
                    job.AutoStartNumDays = int.Parse(Util.GetSetting("DefDlAutoStartNumDays"));
                    job.LastStartedTimeActual = now.Ticks;
                    job.AutoStartNumHours = int.Parse(Util.GetSetting("DefDlAutoStartNumHours"));
                    jobs.Add(job);
                    addToJobList.Add(NewListViewItem(job));
                }

                if (newMultipleJobsDialog.UrlsList.Length > 0)
                {
                    jobsListView.Items.AddRange(addToJobList.ToArray());

                    for (int i = 0; i < addToJobList.Count; i++)
                    {
                        ListViewItem lvi = addToJobList[i];

                        lvi.Selected = true;

                        if (i == addToJobList.Count - 1)
                        {
                            lvi.Focused = true;
                            lvi.EnsureVisible();
                        }
                    }

                    jobsListView.Focus();
                    SaveJobsAsync();
                }

                Cursor.Current = temp;
            }

            newMultipleJobsDialog.Dispose();
        }

        private void jlv_h_nameMenuItem_Click(object sender, EventArgs e)
        {
            if (nameColumnHeader.Width == 0)
            {
                nameColumnHeader.Width = 170;
            }
            else
            {
                nameColumnHeader.Width = 0;
            }
        }

        private void jlv_h_numMenuItem_Click(object sender, EventArgs e)
        {
            if (numColumnHeader.Width == 0)
            {
                numColumnHeader.Width = 35;
            }
            else
            {
                numColumnHeader.Width = 0;
            }
        }

        private void jlv_h_sizeMenuItem_Click(object sender, EventArgs e)
        {
            if (sizeColumnHeader.Width == 0)
            {
                sizeColumnHeader.Width = 75;
            }
            else
            {
                sizeColumnHeader.Width = 0;
            }
        }

        private void jlv_h_doneMenuItem_Click(object sender, EventArgs e)
        {
            if (doneColumnHeader.Width == 0)
            {
                doneColumnHeader.Width = 75;
            }
            else
            {
                doneColumnHeader.Width = 0;
            }
        }

        private void jlv_h_statusMenuItem_Click(object sender, EventArgs e)
        {
            if (statusColumnHeader.Width == 0)
            {
                statusColumnHeader.Width = 80;
            }
            else
            {
                statusColumnHeader.Width = 0;
            }
        }

        private void jlv_h_speedMenuItem_Click(object sender, EventArgs e)
        {
            if (speedColumnHeader.Width == 0)
            {
                speedColumnHeader.Width = 80;
            }
            else
            {
                speedColumnHeader.Width = 0;
            }
        }

        private void jlv_h_etaMenuItem_Click(object sender, EventArgs e)
        {
            if (etaColumnHeader.Width == 0)
            {
                etaColumnHeader.Width = 75;
            }
            else
            {
                etaColumnHeader.Width = 0;
            }
        }

        private void toolsAutoRemoveFinishedJobMenuItem_Click(object sender, EventArgs e)
        {
            toolsAutoRemoveFinishedJobMenuItem.Checked = !toolsAutoRemoveFinishedJobMenuItem.Checked;
            Util.PutSetting("AutoRemoveFinishedJob", toolsAutoRemoveFinishedJobMenuItem.Checked.ToString());
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            int h = panel1.Height - splitter1.Height - 25;

            if (logTextBox.Height > h && h >= 25)
            {
                logTextBox.Height = h;
            }
        }

        public void Translate()
        {
            jobsMenuItem.Text = Util.translationList["000001"];
            jobsNewMenuItem.Text = Util.translationList["000002"];
            jobsNewMultipleMenuItem.Text = Util.translationList["000003"];
            jobsEditMenuItem.Text = Util.translationList["000004"];
            jobsRemoveMenuItem.Text = Util.translationList["000005"];
            jobsCleanupMenuItem.Text = Util.translationList["000006"];
            jobsStartMenuItem.Text = Util.translationList["000007"];
            jobsStopMenuItem.Text = Util.translationList["000008"];
            jobsOpenContainingFolderMenuItem.Text = Util.translationList["000009"];
            jobsMoveUpMenuItem.Text = Util.translationList["000010"];
            jobsMoveDownMenuItem.Text = Util.translationList["000011"];
            //jobsExportMenuItem.Text = Util.translationList["000012"];
            jobsExportMenuItem.Text = Util.translationList["000260"];
            jobsExportWgetBatchFileMenuItem.Text = Util.translationList["000261"];
            jobsQuitMenuItem.Text = Util.translationList["000013"];
            viewMenuItem.Text = Util.translationList["000014"];
            viewToolbarMenuItem.Text = Util.translationList["000015"];
            viewLogMenuItem.Text = Util.translationList["000016"];
            viewStatusBarMenuItem.Text = Util.translationList["000017"];
            toolsMenuItem.Text = Util.translationList["000018"];
            toolsPreferencesMenuItem.Text = Util.translationList["000019"];
            //toolsEnableIEExtensionMenuItem.Text = Util.translationList["000020"];
            toolsDefaultDownloadOptionsMenuItem.Text = Util.translationList["000021"];
            toolsComputeMD5HashMenuItem.Text = Util.translationList["000022"];
            toolsComputeSHA1HashMenuItem.Text = Util.translationList["000023"];
            toolsAutoRemoveFinishedJobMenuItem.Text = Util.translationList["000024"];
            toolsWhenDownloadsFinishedMenuItem.Text = Util.translationList["000025"];
            toolsWhenDownloadsFinishedDisabledMenuItem.Text = Util.translationList["000026"];
            toolsWhenDownloadsFinishedQuitMenuItem.Text = Util.translationList["000027"];
            toolsWhenDownloadsFinishedStandByMenuItem.Text = Util.translationList["000028"];
            toolsWhenDownloadsFinishedHibernateMenuItem.Text = Util.translationList["000029"];
            toolsWhenDownloadsFinishedTurnOffMenuItem.Text = Util.translationList["000030"];
            toolsWhenDownloadsFinishedRestartMenuItem.Text = Util.translationList["000031"];
            toolsWhenDownloadsFinishedLogOffMenuItem.Text = Util.translationList["000032"];
            toolsWhenDownloadsFinishedLockComputerMenuItem.Text = Util.translationList["000033"];
            toolsWhenDownloadsFinishedFreezeMenuItem.Text = Util.translationList["000238"];
            toolsWhenDownloadsFinishedCancelableMenuItem.Text = Util.translationList["000239"];
            helpMenuItem.Text = Util.translationList["000034"];
            helpDiscussionGroupMenuItem.Text = Util.translationList["000196"];
            helpWebsiteMenuItem.Text = Util.translationList["000197"];
            helpCheckForUpdatesMenuItem.Text = Util.translationList["000035"];
            helpAboutVisualWgetMenuItem.Text = Util.translationList["000036"];

            newToolBarButton.ToolTipText = Util.translationList["000071"];
            newMultipleToolBarButton.ToolTipText = Util.translationList["000246"];
            editToolBarButton.ToolTipText = Util.translationList["000072"];
            removeToolBarButton.ToolTipText = Util.translationList["000073"];
            startToolBarButton.ToolTipText = Util.translationList["000074"];
            stopToolBarButton.ToolTipText = Util.translationList["000075"];
            openContainingFolderToolBarButton.ToolTipText = Util.translationList["000076"];
            moveUpToolBarButton.ToolTipText = Util.translationList["000077"];
            moveDownToolBarButton.ToolTipText = Util.translationList["000078"];
            //enableIEExtensionToolBarButton.ToolTipText = Util.translationList["000079"];
            defaultDownloadOptionsToolBarButton.ToolTipText = Util.translationList["000080"];
            preferencesToolBarButton.ToolTipText = Util.translationList["000081"];
            speedLimitToolBarButton.ToolTipText = Util.translationList["000240"];

            //tb1_iee_unregisterIEExtensionMenuItem.Text = Util.translationList["000134"];

            nameColumnHeader.Text = Util.translationList["000135"];
            numColumnHeader.Text = Util.translationList["000136"];
            sizeColumnHeader.Text = Util.translationList["000137"];
            doneColumnHeader.Text = Util.translationList["000138"];
            statusColumnHeader.Text = Util.translationList["000139"];
            speedColumnHeader.Text = Util.translationList["000140"];
            etaColumnHeader.Text = Util.translationList["000141"];
            noteColumnHeader.Text = Util.translationList["000213"];
            jlv_h_nameMenuItem.Text = Util.translationList["000142"];
            jlv_h_numMenuItem.Text = Util.translationList["000143"];
            jlv_h_sizeMenuItem.Text = Util.translationList["000144"];
            jlv_h_doneMenuItem.Text = Util.translationList["000145"];
            jlv_h_statusMenuItem.Text = Util.translationList["000146"];
            jlv_h_speedMenuItem.Text = Util.translationList["000147"];
            jlv_h_etaMenuItem.Text = Util.translationList["000148"];
            jlv_j_editMenuItem.Text = Util.translationList["000004"];
            jlv_j_removeMenuItem.Text = Util.translationList["000005"];
            jlv_j_startMenuItem.Text = Util.translationList["000007"];
            jlv_j_stopMenuItem.Text = Util.translationList["000008"];
            jlv_j_openContainingFolderMenuItem.Text = Util.translationList["000009"];
            jlv_j_moveUpMenuItem.Text = Util.translationList["000010"];
            jlv_j_moveDownMenuItem.Text = Util.translationList["000011"];
            //jlv_j_exportMenuItem.Text = Util.translationList["000012"];
            jlv_j_exportMenuItem.Text = Util.translationList["000260"];
            jlv_j_exportWgetBatchFileMenuItem.Text = Util.translationList["000261"];
            jlv_j_editMultipleMenuItem.Text = Util.translationList["000248"];
            jlv_j_setDownloadDirectoryMenuItem.Text = Util.translationList["000249"];
            jlv_j_setHttpRefererMenuItem.Text = Util.translationList["000250"];

            t_quitMenuItem.Text = Util.translationList["000013"];
            t_speedLimitMenuItem.Text = Util.translationList["000240"];
            t_detectClipboardMenuItem.Text = Util.translationList["000241"];
        }

        void SpeedLimitMenuItems_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            if (menuItem.Text == Util.translationList["000232"])
            {
                if (!Visible)
                {
                    Show();
                }

                if (WindowState == FormWindowState.Minimized)
                {
                    Util.ShowWindow(Handle, 9 /* SW_RESTORE */);
                }

                SetAsForegroundWindow();

                string input = "?????";// Interaction.InputBox(Util.translationList["000233"], Util.translationList["000234"], Util.GetSetting("SpeedLimitList"), Left + 40, Top + 30);

                if (input.Length > 0)
                {
                    if (input.Contains("default"))
                    {
                        Util.PutSetting("SpeedLimitList", Util.GetDefaultSetting("SpeedLimitList"));
                    }
                    else
                    {
                        Util.PutSetting("SpeedLimitList", input);
                    }
                }
            }
            else
            {
                float speed = 0;

                if (menuItem.Text != Util.translationList["000235"])
                {
                    speed = float.Parse(menuItem.Text.Split(' ')[0]);
                }

                Util.PutSetting("SpeedLimit", speed.ToString("F1"));
            }
        }

        private void statusBar1_PanelClick(object sender, StatusBarPanelClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right
                && e.StatusBarPanel.Name == "statusBarPanel2")
            {
                List<MenuItem> menuItems = GetSpeedLimitMenuItems();
                ContextMenu contextMenu = new ContextMenu(menuItems.ToArray());

                contextMenu.Show(e.StatusBarPanel.Parent, new Point(e.X, e.Y));
            }
        }

        private List<MenuItem> GetSpeedLimitMenuItems(bool reverse = false)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            if (reverse)
            {
                MenuItem unlimitedItem = new MenuItem(Util.translationList["000235"], new EventHandler(SpeedLimitMenuItems_Click));

                menuItems.Add(unlimitedItem);
                menuItems.Add(new MenuItem("-"));

                float current = float.Parse(Util.GetSetting("SpeedLimit"));
                string[] speedLimitItems = Util.GetSetting("SpeedLimitList").Split(',');

                bool currentAdded = false;

                for (int i = speedLimitItems.Length - 1; i >= 0; i--)
                {
                    string s = speedLimitItems[i].Trim();
                    float f;

                    if (float.TryParse(s, out f))
                    {
                        MenuItem mi = new MenuItem(string.Format("{0:F1} " + Util.translationList["000162"], f), new EventHandler(SpeedLimitMenuItems_Click));

                        if (string.Format("{0:F1}", f) == string.Format("{0:F1}", current))
                        {
                            mi.Checked = true;
                            mi.RadioCheck = true;
                            currentAdded = true;
                        }
                        else if (!currentAdded && current > 0 && float.Parse(string.Format("{0:F1}", f)) > float.Parse(string.Format("{0:F1}", current)))
                        {
                            MenuItem currentItem = new MenuItem(string.Format("{0:F1} " + Util.translationList["000162"], current), new EventHandler(SpeedLimitMenuItems_Click));

                            currentItem.Checked = true;
                            currentItem.RadioCheck = true;
                            menuItems.Add(currentItem);
                            currentAdded = true;
                        }

                        menuItems.Add(mi);
                    }
                    else
                    {
                        continue;
                    }
                }

                menuItems.Add(new MenuItem("-"));

                if (current == 0)
                {
                    unlimitedItem.Checked = true;
                    unlimitedItem.RadioCheck = true;
                }

                menuItems.Add(new MenuItem(Util.translationList["000232"], new EventHandler(SpeedLimitMenuItems_Click)));
            }
            else
            {
                menuItems.Add(new MenuItem(Util.translationList["000232"], new EventHandler(SpeedLimitMenuItems_Click)));
                menuItems.Add(new MenuItem("-"));

                float current = float.Parse(Util.GetSetting("SpeedLimit"));
                string[] speedLimitItems = Util.GetSetting("SpeedLimitList").Split(',');

                bool currentAdded = false;

                for (int i = 0; i < speedLimitItems.Length; i++)
                {
                    string s = speedLimitItems[i].Trim();
                    float f;

                    if (float.TryParse(s, out f))
                    {
                        MenuItem mi = new MenuItem(string.Format("{0:F1} " + Util.translationList["000162"], f), new EventHandler(SpeedLimitMenuItems_Click));

                        if (string.Format("{0:F1}", f) == string.Format("{0:F1}", current))
                        {
                            mi.Checked = true;
                            mi.RadioCheck = true;
                            currentAdded = true;
                        }
                        else if (!currentAdded && float.Parse(string.Format("{0:F1}", f)) < float.Parse(string.Format("{0:F1}", current)))
                        {
                            MenuItem currentItem = new MenuItem(string.Format("{0:F1} " + Util.translationList["000162"], current), new EventHandler(SpeedLimitMenuItems_Click));

                            currentItem.Checked = true;
                            currentItem.RadioCheck = true;
                            menuItems.Add(currentItem);
                            currentAdded = true;
                        }

                        menuItems.Add(mi);
                    }
                    else
                    {
                        continue;
                    }
                }

                menuItems.Add(new MenuItem("-"));

                MenuItem unlimitedItem = new MenuItem(Util.translationList["000235"], new EventHandler(SpeedLimitMenuItems_Click));

                if (current == 0)
                {
                    unlimitedItem.Checked = true;
                    unlimitedItem.RadioCheck = true;
                }

                menuItems.Add(unlimitedItem);
            }

            return menuItems;
        }

        public void SetInterFaceFont()
        {
            this.Font = Util.GetInterfaceFont();
        }

        private void helpWebsiteMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(QuickPrint.Properties.Resources.websiteLink);
        }

        private void helpDiscussionGroupMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(QuickPrint.Properties.Resources.discussionGroupLink);
        }

        private void jlv_h_noteMenuItem_Click(object sender, EventArgs e)
        {
            if (noteColumnHeader.Width == 0)
            {
                noteColumnHeader.Width = 170;
            }
            else
            {
                noteColumnHeader.Width = 0;
            }
        }

        private void jlv_j_setHttpRefererMenuItem_Click(object sender, EventArgs e)
        {
            string input = "????";//Interaction.InputBox("Please enter the HTTP Referer URL.\nEnter 'clear' to clear the referer.", "Set HTTP Referer", "", Left + 40, Top + 30);

            if (input.Length > 0)
            {
                if (input.Contains("clear"))
                {
                    //
                    // do not allow editing if some jobs are currently running
                    //
                    foreach (int i in jobsListView.SelectedIndices)
                    {
                        Job j = (Job)jobsListView.Items[i].Tag;
                        if (!IsFullyStopped(j))
                        {
                            Util.MsgBox(Util.translationList["000218"], MessageBoxIcon.Error);
                            return;
                        }
                    }

                    //
                    // clear http referer for all selected job
                    //
                    foreach (int i in jobsListView.SelectedIndices)
                    {
                        Job j = (Job)jobsListView.Items[i].Tag;
                        j.Opts.Remove("referer");
                    }

                    SaveJobsAsync();
                    Util.MsgBox(Util.translationList["000222"]);
                }
                else
                {
                    //
                    // do not allow editing if some jobs are currently running
                    //
                    foreach (int i in jobsListView.SelectedIndices)
                    {
                        Job j = (Job)jobsListView.Items[i].Tag;
                        if (!IsFullyStopped(j))
                        {
                            Util.MsgBox(Util.translationList["000218"], MessageBoxIcon.Error);
                            return;
                        }
                    }

                    //
                    // set http referer for all selected job
                    //
                    foreach (int i in jobsListView.SelectedIndices)
                    {
                        Job j = (Job)jobsListView.Items[i].Tag;
                        j.Opts["referer"] = input;
                    }

                    SaveJobsAsync();
                    Util.MsgBox(Util.translationList["000221"]);
                }
            }
        }

        private void jlv_j_setDownloadDirectoryMenuItem_Click(object sender, EventArgs e)
        {
            SelectFolderInfo selectFolderInfo = new SelectFolderInfo("", Util.translationList["000225"], Util.translationList["000226"]);
            SelectFolderDialog selectFolderDialog = new SelectFolderDialog(selectFolderInfo);

            if (selectFolderDialog.ShowDialog(this) == DialogResult.OK)
            {
                string folderPath = selectFolderDialog.FolderPath;

                //
                // do not allow editing if some jobs are currently running
                //
                foreach (int i in jobsListView.SelectedIndices)
                {
                    Job j = (Job)jobsListView.Items[i].Tag;
                    if (!IsFullyStopped(j))
                    {
                        Util.MsgBox(Util.translationList["000218"], MessageBoxIcon.Error);
                        return;
                    }
                }

                //
                // set download directory for all selected job
                //
                foreach (int i in jobsListView.SelectedIndices)
                {
                    Job j = (Job)jobsListView.Items[i].Tag;
                    j.Opts["directory-prefix"] = folderPath;
                }

                SaveJobsAsync();
                Util.MsgBox(Util.translationList["000224"]);
            }

            selectFolderDialog.Dispose();
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Visible)
                {
                    if (Handle == Util.GetLastActivePopup(Handle))
                    {
                        Hide();
                    }
                    else
                    {
                        SetAsForegroundWindow();
                    }
                }
                else
                {
                    Show();

                    if (WindowState == FormWindowState.Minimized)
                    {
                        Util.ShowWindow(Handle, 9 /* SW_RESTORE */);
                    }

                    SetAsForegroundWindow();
                }
            }
        }

        private void trayContextMenu_Popup(object sender, EventArgs e)
        {
            List<MenuItem> menuItems = GetSpeedLimitMenuItems();

            t_speedLimitMenuItem.MenuItems.Clear();
            t_speedLimitMenuItem.MenuItems.AddRange(menuItems.ToArray());
        }

        private void t_detectClipboardMenuItem_Click(object sender, EventArgs e)
        {
            t_detectClipboardMenuItem.Checked = !t_detectClipboardMenuItem.Checked;
            Util.PutSetting("DetectClipboard", t_detectClipboardMenuItem.Checked.ToString());
        }

        private void toolsWhenDownloadsFinishedFreezeMenuItem_Click(object sender, EventArgs e)
        {
            toolsWhenDownloadsFinishedFreezeMenuItem.Checked = !toolsWhenDownloadsFinishedFreezeMenuItem.Checked;
            Util.PutSetting("WhenDownloadsFinishedFreeze", toolsWhenDownloadsFinishedFreezeMenuItem.Checked.ToString());

            if (toolsWhenDownloadsFinishedFreezeMenuItem.Checked)
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", autoShutdownAction.ToString());
            }
            else
            {
                Util.PutSetting("WhenDownloadsFinishedFreezedValue", AutoShutdownAction.None.ToString());
            }
        }

        private void toolsWhenDownloadsFinishedCancelableMenuItem_Click(object sender, EventArgs e)
        {
            toolsWhenDownloadsFinishedCancelableMenuItem.Checked = !toolsWhenDownloadsFinishedCancelableMenuItem.Checked;
            Util.PutSetting("WhenDownloadsFinishedCancelable", toolsWhenDownloadsFinishedCancelableMenuItem.Checked.ToString());
        }

        private void t_quitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void jobsExportWgetBatchFileMenuItem_Click(object sender, EventArgs e)
        {
            ExportJobsAsWgetBatchFile();
        }

        private void jlv_j_exportWgetBatchFileMenuItem_Click(object sender, EventArgs e)
        {
            ExportJobsAsWgetBatchFile();
        }

        #region Test
        public static AcadApplication gbl_app;
        public static AcadDocument gbl_doc;
        private void menuItem2_Click(object sender, EventArgs e)
        {
           // this.TopMost = false;
            try
            {
                object obj = Marshal.GetActiveObject("AutoCAD.Application.17");
                if (obj != null)
                {
                    gbl_app = obj as AcadApplication;
                    try
                    {
                        //gbl_app.ActiveDocument.ActiveLayout.ConfigName = "DWF6 ePlot.pc3";
                       // AcadPreferencesOutput ACADPref = gbl_app.ActiveDocument.Application.Preferences.Output;
                        string newFile = "monochrome.ctb";
                        //ACADPref.DefaultPlotStyleTable = newFile;
                        
                        //
                        AcadPlotConfigurations PtConfigs;
                        AcadPlotConfiguration PlotConfig;
                        AcadPlot PtObj;
                        var BackPlot = gbl_app.ActiveDocument.GetVariable("BACKGROUNDPLOT");

                        //Create a new plot configuration with all needed parameters
                        PtObj = gbl_app.ActiveDocument.Plot;
                        PtConfigs = gbl_app.ActiveDocument.PlotConfigurations;

                        //'Add a new plot configuration
                        PtConfigs.Add("PDF", false);

                        //'The plot config you created become active
                        PlotConfig = PtConfigs.Item("PDF");

                        //Use this method to set the scale
                        PlotConfig.StandardScale = AcPlotScale.acScaleToFit;


                        PlotConfig.ConfigName = "DWG To PDF.pc3";
                        

                        //Specifies whether or not to plot using the plot styles
                        PlotConfig.PlotWithPlotStyles = true;

                        //'You can select the plot style table here
                        PlotConfig.StyleSheet = newFile;

                        //Updates the plot
                        PlotConfig.RefreshPlotDeviceInfo();
                        //'Here you specify the pc3 file you want to use
                        
                        //If you are going to create pdf files in a batch mode,
                        //'I would recommend to turn off the BACKGROUNDPLOT system variable,
                        //'so autocad will not continue to do anything until finishes
                        //'the pdf creation
                        gbl_app.ActiveDocument.SetVariable("BACKGROUNDPLOT", 0);

                        //'Updates the plot
                        PlotConfig.RefreshPlotDeviceInfo();

                        //gbl_app.ActiveDocument.Plot.DisplayPlotPreview(AcPreviewMode.acPartialPreview);

                        //////'Now you can use the PlotTofile method
                        ////if (PtObj.PlotToFile(gbl_app.ActiveDocument.Name.Replace("dwg", "pdf"), PlotConfig.ConfigName))
                        ////{
                        ////    MessageBox.Show("PDF was created!");
                        ////}
                        ////else
                        ////    MessageBox.Show("PDF creation unsuccessful!");
                        //////'If you wish you can delete th plot configuration you created
                        //////'programmatically, and set the 'BACKGROUNDPLOT' system variable
                        //////'to its original status.

                        ////PtConfigs.Item("PDF").Delete();
                        ////PlotConfig = null;
                        ////gbl_app.ActiveDocument.SetVariable("BACKGROUNDPLOT", BackPlot);

                        gbl_app.ActiveDocument.Plot.DisplayPlotPreview(AcPreviewMode.acPartialPreview);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
                }
            }
            catch
            {
                logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
            }
        }
        
        private void menuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = Marshal.GetActiveObject("AutoCAD.Application.17");
                if (obj != null)
                {
                    gbl_app = obj as AcadApplication;
                    try
                    {
                        gbl_app.Visible = !gbl_app.Visible;
                        menuItem5.Checked = gbl_app.Visible == true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
                }
            }
            catch
            {
                logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
            }
            
        }
        #endregion

        private void menuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = Marshal.GetActiveObject("AutoCAD.Application.17");
                if (obj != null)
                {
                    gbl_app = obj as AcadApplication;
                    try
                    {
                        string ssName = "ss";
                        for (int i = 0; i< gbl_app.ActiveDocument.SelectionSets.Count; i++)
                        {
                            if ( gbl_app.ActiveDocument.SelectionSets.Item(i).Name == ssName)
                            {
                                gbl_app.ActiveDocument.SelectionSets.Item(i).Delete();
                                break;
                            }
                        }

                        AcadSelectionSet ss1 = gbl_app.ActiveDocument.SelectionSets.Add(ssName);
                        //
                        ss1.SelectOnScreen();
                        foreach (AcadEntity ent in ss1)
                        {
                            ent.Highlight(true);
                            ent.Update();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
                }
            }
            catch
            {
                logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
            }
            
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = Marshal.GetActiveObject("AutoCAD.Application.17");
                if (obj != null)
                {
                    gbl_app = obj as AcadApplication;
                    try
                    {
                        var point1 = gbl_app.ActiveDocument.Utility.GetPoint();
                        gbl_app.ActiveDocument.Utility.GetCorner(point1);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
                }
            }
            catch
            {
                logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
            }
            
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            PF.CheckLayerExisting("0");
        }

        #region CTB
        
        private Dictionary<int, string> pensdict = new Dictionary<int, string>();
        private string[] LineTypes = new string[32]
    {
      "Solid",
      "Dashed",
      "Dotted",
      "Dash Dot",
      "Short Dash",
      "Medium Dash",
      "Long Dash",
      "Short Dah X2",
      "Medium Dash X2",
      "Long Dash X2",
      "Medium Long Dash",
      "Medium Dash Short Dash Short Dash",
      "Long Dash Short Dash",
      "Long Dash Dot Dot",
      "Long Dash Dot Dot",
      "Medium Dash Dot Short Dash Dot",
      "Sparse Dot",
      "ISO Dash",
      "ISO Dash Space",
      "ISO Long Dash Dot",
      "ISO Long Dash Double Dot",
      "ISO Long Dash Triple Dot",
      "ISO Dot",
      "ISO Long Dash Short Dash",
      "ISO Long Dash Double Short Dash",
      "ISO Dash Dot",
      "ISO Double Dash Dot",
      "ISO Dash Double Dot",
      "ISO Double Dash Double Dot",
      "ISO Dash Triple Dot",
      "ISO Double Dash Triple Dot",
      "(Object)"
    };
        private string[] FillStyle = new string[10]
    {
      "Solid",
      "Checkerboard",
      "Crosshatch",
      "Diamonds",
      "Hor. bars",
      "Slant left",
      "Slant right",
      "Square dots",
      "Vert. bars",
      "(Object)"
    };
        private string[] EndStyle = new string[5]
    {
      "Butt",
      "Square",
      "Round",
      "Diamond",
      "(Object)"
    };
        private string[] JoinStyle = new string[6]
    {
      "Miter",
      "Bevel",
      "Round",
      "Diamond",
      "Dummy",
      "(Object)"
    };

        private void menuItem11_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            this.logTextBox.Clear();
            this.Decompress(this.openFileDialog1.FileName);
        }
        private void Decompress(string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<string> stringList1 = new List<string>();
            List<string> stringList2 = new List<string>();
            List<CtbRec> ctbRecList = new List<CtbRec>();
            using (FileStream fileStream = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (ZlibStream zlibStream = new ZlibStream((Stream)fileStream, CompressionMode.Decompress, false))
                {
                    fileStream.Seek(60L, SeekOrigin.Begin);
                    int num;
                    do
                    {
                        byte[] numArray = new byte[4096];
                        num = zlibStream.Read(numArray, 0, 4096);
                        stringBuilder.Append(Encoding.Default.GetString(numArray));
                    }
                    while (num > 0);
                }
            }
            string string1 = stringBuilder.ToString();
            if (!string1.StartsWith("description="))
            {
                int num1 = (int)MessageBox.Show("Not a valid ctb/stb file");
            }
            else
            {
                this.logTextBox.Text = string1;
                //string[] strArray1 = string1.Split(Environment.NewLine.ToCharArray());
                //if (strArray1.Length < 50)
                //{
                //    int num2 = (int)MessageBox.Show("Not a valid ctb/stb file");
                //}
                //else
                //{
                //    string str1 = "";
                //    string str2 = "";
                //    int num3 = 0;
                //    for (int index = 0; index < strArray1.Length; ++index)
                //    {
                //        string TheLine = strArray1[index];
                //        if (TheLine.StartsWith("custom_lineweight_display_units"))
                //        {
                //            string str3 = this.RightSide(strArray1[index]);
                //            if (str3 == "0")
                //                str3 = "mm";
                //            if (str3 == "1")
                //                str3 = "inch";
                //            //this.toolStripStatusLabel1.Text = "Units: " + str3;
                //        }
                //        if (TheLine.StartsWith("scale_factor"))
                //            str2 = this.RightSide(TheLine).Split('(')[0].Trim();
                //        if (TheLine.StartsWith("apply_factor"))
                //        {
                //            if (this.RightSide(TheLine).Contains("TRUE"))
                //                this.toolStripStatusLabel2.Text = "LW Scale by: " + str2;
                //            else
                //                this.toolStripStatusLabel2.Text = "";
                //        }
                //        if (TheLine.StartsWith("custom_lineweight_table{"))
                //            break;
                //    }
                //    str1 = "";
                //    num3 = 0;
                //    int index1 = 0;
                //    while (index1 < strArray1.Length && !strArray1[index1].StartsWith("custom_lineweight_table{"))
                //        ++index1;
                //    for (int index2 = index1 + 1; index2 < strArray1.Length - 1 && !strArray1[index2].Equals("}"); ++index2)
                //        stringList1.Add(this.RightSide(strArray1[index2]));
                //    int startIndex = string1.IndexOf("plot_style{") + 11;
                //    int num4 = string1.IndexOf("}\n}") + 1;
                //    string[] strArray2 = string1.Substring(startIndex, num4 - startIndex).Split(new char[2] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
                //    List<List<string>> stringListList = new List<List<string>>();
                //    string[] strArray3 = new string[strArray2.Length / 2 - 1];
                //    string[] strArray4 = strArray2[1].Trim(' ', '"').Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                //    for (int index2 = 0; index2 < strArray4.Length; ++index2)
                //        this.dt.Columns.Add("col" + index2.ToString(), UppercaseFirst(this.LeftSide(strArray4[index2]).Trim()));
                //    int index3 = 1;
                //    while (index3 < strArray2.Length)
                //    {
                //        int index2 = -1;
                //        int argb = 0;
                //        string[] items = strArray2[index3].Trim(' ', '"').Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                //        int index4;
                //        for (int index5 = 0; index5 < items.Length; ++index5)
                //        {
                //            switch (this.LeftSide(items[index5]))
                //            {
                //                case "name":
                //                    string str3 = this.RightSide(items[index5]);
                //                    string str4 = "000";
                //                    if (str3.StartsWith("Color_"))
                //                    {
                //                        str4 = Convert.ToInt32(str3.Split('_')[1]).ToString("D3");
                //                        str3 = str3.Split('_')[0];
                //                    }
                //                    items[index5] = str3 + "_" + str4;
                //                    break;
                //                case "localized_name":
                //                    items[index5] = this.RightSide(items[index5]);
                //                    break;
                //                case "color":
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        int int32 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        if (int32 == -1006632961 || int32 == -1)
                //                        {
                //                            items[index5] = "(Object)";
                //                        }
                //                        else
                //                        {
                //                            string s = int32.ToString("X8").Substring(2);
                //                            if (s.Equals("FFFFFF"))
                //                            {
                //                                items[index5] = "(Object)";
                //                            }
                //                            else
                //                            {
                //                                int key = int.Parse(s, NumberStyles.HexNumber);
                //                                items[index5] = !this.pensdict.ContainsKey(key) ? "TrueColor" : this.pensdict[key];
                //                            }
                //                        }
                //                        break;
                //                    }
                //                    break;
                //                case "mode_color":
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        int int32 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        if (int32 != 0)
                //                        {
                //                            if (items[index5 - 1].Equals("(Object)"))
                //                            {
                //                                items[index5] = "(Object)";
                //                            }
                //                            else
                //                            {
                //                                items[index5] = this.IntToColor(int32);
                //                                index2 = index5;
                //                                argb = int32;
                //                            }
                //                        }
                //                        else
                //                            items[index5] = "(Object)";
                //                        break;
                //                    }
                //                    break;
                //                case "color_policy":
                //                    index4 = 0;
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        index4 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        switch (index4)
                //                        {
                //                            case 0:
                //                                items[index5] = "none";
                //                                break;
                //                            case 1:
                //                                items[index5] = "dith";
                //                                break;
                //                            case 2:
                //                                items[index5] = "gray";
                //                                break;
                //                            case 3:
                //                                items[index5] = "dith, gray";
                //                                break;
                //                            case 4:
                //                                items[index5] = "none";
                //                                break;
                //                            case 5:
                //                                items[index5] = "dith";
                //                                break;
                //                        }
                //                        break;
                //                    }
                //                    break;
                //                case "physical_pen_number":
                //                    index4 = 0;
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        index4 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        items[index5] = index4 != 0 ? index4.ToString() : "Auto";
                //                        break;
                //                    }
                //                    break;
                //                case "virtual_pen_number":
                //                    index4 = 0;
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        index4 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        items[index5] = index4 != 0 ? index4.ToString() : "Auto";
                //                        break;
                //                    }
                //                    break;
                //                case "linetype":
                //                    index4 = 0;
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        index4 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        items[index5] = this.LineTypes[index4];
                //                        break;
                //                    }
                //                    items[index5] = "unknown";
                //                    break;
                //                case "lineweight":
                //                    index4 = 0;
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        index4 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        items[index5] = index4 != 0 ? stringList1[index4 - 1] : "(Object)";
                //                        break;
                //                    }
                //                    break;
                //                case "fill_style":
                //                    index4 = 0;
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        index4 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        items[index5] = this.FillStyle[index4 - 64];
                //                        break;
                //                    }
                //                    break;
                //                case "end_style":
                //                    index4 = 0;
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        index4 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        items[index5] = this.EndStyle[index4];
                //                        break;
                //                    }
                //                    break;
                //                case "join_style":
                //                    index4 = 0;
                //                    if (this.IsNumber(this.RightSide(items[index5])))
                //                    {
                //                        index4 = Convert.ToInt32(this.RightSide(items[index5]));
                //                        items[index5] = this.JoinStyle[index4];
                //                        break;
                //                    }
                //                    break;
                //                default:
                //                    items[index5] = this.RightSide(items[index5]);
                //                    break;
                //            }
                //        }
                //        ListViewItem listViewItem = new ListViewItem(items);
                //        if (index2 >= 0)
                //        {
                //            string text = listViewItem.SubItems[index2].Text;
                //            listViewItem.SubItems[index2].BackColor = Color.FromArgb(argb);
                //        }
                //        this.dt.Rows.Add((object[])items);
                //        index3 += 2;
                //    }
                //    int index6 = 0;
                //    while (index6 < this.dt.Columns.Count - 1 && !this.dt.Columns[index6].HeaderText.ToLower().Equals("mode_color"))
                //        ++index6;
                //    for (int index2 = 0; index2 < this.dt.Rows.Count; ++index2)
                //    {
                //        if (this.dt[index6, index2].Value != null)
                //        {
                //            string string2 = this.dt[index6, index2].Value.ToString();
                //            if (string2.Contains(","))
                //            {
                //                int int32_1 = Convert.ToInt32(string2.Split(',')[0]);
                //                int int32_2 = Convert.ToInt32(string2.Split(',')[1]);
                //                int int32_3 = Convert.ToInt32(string2.Split(',')[2]);
                //                this.dt.Rows[index2].Cells[index6].Style.BackColor = Color.FromArgb(int32_1, int32_2, int32_3);
                //            }
                //        }
                //    }
                //    if (this.radioButton1.Checked)
                //        this.dt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
                //    else if (this.radioButton2.Checked)
                //        this.dt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //    this.dt.ColumnHeadersDefaultCellStyle.Font = new Font(this.dt.Font, FontStyle.Bold);
                //}
            }
        }
        private static string ITBS(string thepath)
        {
            return !thepath.EndsWith("\\") ? thepath + "\\" : thepath;
        }

        private static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            return ((int)char.ToUpper(s[0])).ToString() + s.Substring(1);
        }

        public bool IsNumber(string strNumber)
        {
            Regex regex1 = new Regex("[^0-9.-]");
            Regex regex2 = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex regex3 = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            Regex regex4 = new Regex("(" + "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$" + ")|(" + "^([-]|[0-9])[0-9]*$" + ")");
            return !regex1.IsMatch(strNumber) && !regex2.IsMatch(strNumber) && !regex3.IsMatch(strNumber) && regex4.IsMatch(strNumber);
        }

        private string LeftSide(string TheLine)
        {
            string str = "";
            if (TheLine.Contains("="))
                str = TheLine.Split('=')[0].Trim(' ');
            return str;
        }

        private string RightSide(string TheLine)
        {
            if (!TheLine.Contains("="))
                return "";
            return TheLine.Split('=')[1].Trim('"');
        }

        private string IntToColor(int colornumber)
        {
            long num1 = ((long)colornumber & 4278190080L) >> 24;
            long num2 = (long)((colornumber & 16711680) >> 16);
            long num3 = (long)((colornumber & 65280) >> 8);
            long num4 = (long)(colornumber & (int)byte.MaxValue);
            string str;
            if (colornumber == -1 || colornumber == 16777215)
                str = "(Object)";
            else
                str = num2.ToString() + "," + num3.ToString() + "," + num4.ToString();
            return str;
        }
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[2000];
            int count;
            while ((count = input.Read(buffer, 0, 2000)) > 0)
                output.Write(buffer, 0, count);
            output.Flush();
        }

        public static int GetCompressedSize(string inFile, bool IsFile)
        {
            FileStream fileStream = new FileStream(inFile, FileMode.Open, FileAccess.Read);
            byte[] buffer;
            try
            {
                int length = (int)fileStream.Length;
                buffer = new byte[length];
                int offset = 0;
                int num;
                while ((num = fileStream.Read(buffer, offset, length - offset)) > 0)
                    offset += num;
            }
            finally
            {
                fileStream.Close();
            }
            byte[] array;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (ZlibStream zlibStream = new ZlibStream((Stream)memoryStream, CompressionMode.Compress))
                {
                    using (Stream input = (Stream)new MemoryStream(buffer))
                    {
                        CopyStream(input, (Stream)zlibStream);
                        zlibStream.Close();
                        array = memoryStream.ToArray();
                    }
                }
            }
            return array.Length;
        }

        public static int GetCompressedSize(string ctbcontent)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(ctbcontent);
            MemoryStream memoryStream1 = new MemoryStream(bytes);
            MemoryStream memoryStream2 = new MemoryStream(bytes);
            byte[] buffer;
            try
            {
                int length = (int)memoryStream2.Length;
                buffer = new byte[length];
                int offset = 0;
                int num;
                while ((num = memoryStream2.Read(buffer, offset, length - offset)) > 0)
                    offset += num;
            }
            finally
            {
                memoryStream2.Close();
            }
            byte[] array;
            using (MemoryStream memoryStream3 = new MemoryStream())
            {
                using (ZlibStream zlibStream = new ZlibStream((Stream)memoryStream3, CompressionMode.Compress))
                {
                    using (Stream input = (Stream)new MemoryStream(buffer))
                    {
                        CopyStream(input, (Stream)zlibStream);
                        zlibStream.Close();
                        array = memoryStream3.ToArray();
                    }
                }
            }
            return array.Length;
        }

        public static void MakeHeader(Stream input, Stream output)
        {
            byte[] buffer = new byte[60];
            int count;
            while ((count = input.Read(buffer, 0, 60)) > 0)
                output.Write(buffer, 0, count);
            output.Flush();
        }

        public void compressFile(string inFile, string outFile)
        {
            byte[] buffer = new byte[60];
            string str1 = "PIAFILEVERSION_2.0,CTBVER1,compress\r\npmzlibcodec";
            int length = str1.Length;
            for (int index = 0; index < length; ++index)
                buffer[index] = Convert.ToByte(str1[index]);
            for (int index = length; index < 55; ++index)
                buffer[index] = byte.MaxValue;
            buffer[55] = (byte)0;
            int compressedSize = GetCompressedSize(inFile, true);
            string str2 = string.Format("{0:X8}", (object)compressedSize);
            string str3 = str2.Substring(0, 2);
            string str4 = str2.Substring(2, 2);
            string str5 = str2.Substring(4, 2);
            string str6 = str2.Substring(6, 2);
            buffer[56] = Convert.ToByte(str6, 16);
            buffer[57] = Convert.ToByte(str5, 16);
            buffer[58] = Convert.ToByte(str4, 16);
            buffer[59] = Convert.ToByte(str3, 16);
            FileStream fileStream1 = new FileStream(outFile, FileMode.Create);
            ZlibStream zlibStream = new ZlibStream((Stream)fileStream1, CompressionMode.Compress);
            FileStream fileStream2 = new FileStream(inFile, FileMode.Open);
            Stream input = (Stream)new MemoryStream(buffer);
            try
            {
                MakeHeader(input, (Stream)fileStream1);
                CopyStream((Stream)fileStream2, (Stream)zlibStream);
            }
            finally
            {
                //this.label1.Text = compressedSize.ToString();
                zlibStream.Close();
                fileStream1.Close();
                fileStream2.Close();
            }
        }

        public void compressFile(string ctbcontent, bool IsContent, string outFile)
        {
            byte[] buffer = new byte[60];
            string str1 = "PIAFILEVERSION_2.0,CTBVER1,compress\r\npmzlibcodec";
            int length = str1.Length;
            for (int index = 0; index < length; ++index)
                buffer[index] = Convert.ToByte(str1[index]);
            for (int index = length; index < 55; ++index)
                buffer[index] = byte.MaxValue;
            buffer[55] = (byte)0;
            int compressedSize = GetCompressedSize(ctbcontent);
            string str2 = string.Format("{0:X8}", (object)compressedSize);
            string str3 = str2.Substring(0, 2);
            string str4 = str2.Substring(2, 2);
            string str5 = str2.Substring(4, 2);
            string str6 = str2.Substring(6, 2);
            buffer[56] = Convert.ToByte(str6, 16);
            buffer[57] = Convert.ToByte(str5, 16);
            buffer[58] = Convert.ToByte(str4, 16);
            buffer[59] = Convert.ToByte(str3, 16);
            FileStream fileStream = new FileStream(outFile, FileMode.Create);
            ZlibStream zlibStream = new ZlibStream((Stream)fileStream, CompressionMode.Compress);
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(ctbcontent));
            Stream input = (Stream)new MemoryStream(buffer);
            try
            {
                MakeHeader(input, (Stream)fileStream);
                CopyStream((Stream)memoryStream, (Stream)zlibStream);
            }
            finally
            {
                //this.label1.Text = compressedSize.ToString();
                zlibStream.Close();
                fileStream.Close();
                memoryStream.Close();
            }
        }
        #endregion

        private void menuItem12_Click(object sender, EventArgs e)
        {
            if (this.logTextBox.Text.Length < 0) return;
            this.saveFileDialog1.Filter = "CTB Files|*.ctb|STB Files|*.stb";
           // this.saveFileDialog1.FileName = Path.GetDirectoryName(this.openFileDialog1.FileName) + "\\_" + Path.GetFileName(this.openFileDialog1.FileName);
            this.saveFileDialog1.DefaultExt = "." + Path.GetExtension(this.openFileDialog1.FileName);
            switch (this.saveFileDialog1.DefaultExt)
            {
                case ".ctb":
                    this.saveFileDialog1.FilterIndex = 1;
                    break;
                case ".stb":
                    this.saveFileDialog1.FilterIndex = 2;
                    break;
            }
            if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (this.saveFileDialog1.FileName.Equals(this.openFileDialog1.FileName))
                this.saveFileDialog1.FileName = Path.GetDirectoryName(this.saveFileDialog1.FileName) + "\\~" + Path.GetFileName(this.saveFileDialog1.FileName);
            this.compressFile(this.logTextBox.Text, true, this.saveFileDialog1.FileName);
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            string supportPath = @"C:\Users\CongNV\AppData\Roaming\Autodesk\AutoCAD 2007\R17.0\enu\Plotters";
            string configName = Path.Combine(supportPath, "DWG To PDF.pc3");
            var pdfConfig = new PlotterConfiguration(configName);

            pdfConfig.TruetypeAsText = true;
            pdfConfig.SetCustomValue("Include_Layer", false);
            pdfConfig.SetCustomValue("Create_Bookmarks", false);

            pdfConfig.Write(Path.Combine(supportPath, "DWG To PDF - NoLayersOrBookmarks.pc3"));
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = Marshal.GetActiveObject("AutoCAD.Application.17");
                if (obj != null)
                {
                    gbl_app = obj as AcadApplication;
                    try
                    {
                        gbl_app.ActiveDocument.ActiveLayout.ConfigName = "DWF6 ePlot.pc3";
                        // AcadPreferencesOutput ACADPref = gbl_app.ActiveDocument.Application.Preferences.Output;
                        string newFile = @"C:\Users\CongNV\AppData\Roaming\Autodesk\AutoCAD 2007\R17.0\enu\Plot Styles\monochrome.ctb";
                        //ACADPref.DefaultPlotStyleTable = newFile;

                        gbl_app.ActiveDocument.Plot.PlotToDevice();
                        gbl_app.ActiveDocument.Plot.DisplayPlotPreview(AcPreviewMode.acPartialPreview);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
                }
            }
            catch
            {
                logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
            }
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            CTBColor clo1 = new CTBColor(0);
            this.logTextBox.Text = clo1.Content.ToString();
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            CTBFile ctbFile = new CTBFile();
            ctbFile.Colors[10].Screen = 50;
            ctbFile.UpdateContent();
            this.logTextBox.Text = ctbFile.Content.ToString();
        }

        private void menuItem18_Click(object sender, EventArgs e)
        {
            NewCTBFileDialog nCtbDialg = new NewCTBFileDialog();
            nCtbDialg.ShowDialog();
        }

        private void menuItem19_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = Marshal.GetActiveObject("AutoCAD.Application.17");
                if (obj != null)
                {
                    gbl_app = obj as AcadApplication;
                    try
                    {
                        AcadApp.SetFocus(gbl_app.HWND);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
                }
            }
            catch
            {
                logTextBox.Text = "AutoCAD chưa khởi động hoặc không đúng phiên bản!";
            }
        }

        private void menuItem20_Click(object sender, EventArgs e)
        {
            SheetDialog sheetdialog = new SheetDialog();
            sheetdialog.StartPosition = FormStartPosition.CenterParent;
            sheetdialog.ShowDialog();
        }

    }
}
