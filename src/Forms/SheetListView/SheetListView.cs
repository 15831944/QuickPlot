using QuickPrint.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualWget;

namespace QuickPrint
{
    public class SheetsListView : ListView
    {
        public SheetsListView()
        {
            DoubleBuffered = true;
        }

        public void UpdateInfo()
        {
            UpdateInfo(new Rectangle(0, 0, 0, 0));
        }

        internal void AddSheet(Sheet sheet)
        {
            this.Items.Add(NewListViewItem(sheet));
        }


        ListViewItem NewListViewItem(Sheet sheet)
        {
            int imageIndex = (int)sheet.Id;
            string[] subitems = { sheet.Id.ToString(), sheet.Name,"Ready","","", sheet.Path };
            ListViewItem lvi = new ListViewItem(subitems, imageIndex);
            lvi.Tag = sheet;
            return lvi;
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

                    Sheet job = (Sheet)lvi.Tag;
                    //JobStatus status = job.GetStatus();

                    //int imgInd = (int)status;

                    //if (lvi.ImageIndex != imgInd)
                    //{
                    //    lvi.ImageIndex = imgInd;
                    //}

                    //string name = job.Name;

                    //if (lvi.SubItems[0].Text != name)
                    //{
                    //    lvi.SubItems[0].Text = name;
                    //}

                    //string numText = job.Num.ToString();

                    //if (lvi.SubItems[1].Text != numText)
                    //{
                    //    lvi.SubItems[1].Text = numText;
                    //}

                    //string sizeText = job.SizeText;

                    //if (lvi.SubItems[2].Text != sizeText)
                    //{
                    //    lvi.SubItems[2].Text = sizeText;
                    //}

                    //string doneText = job.DoneText;

                    //if (lvi.SubItems[3].Text != doneText)
                    //{
                    //    lvi.SubItems[3].Text = doneText;
                    //}

                    //string statusText;

                    //if (status == JobStatus.Ready)
                    //{
                    //    statusText = Util.translationList["000155"];
                    //}
                    //else if (status == JobStatus.Queued)
                    //{
                    //    statusText = Util.translationList["000156"];
                    //}
                    //else if (status == JobStatus.Running)
                    //{
                    //    statusText = Util.translationList["000157"];
                    //}
                    //else if (status == JobStatus.Retrieving)
                    //{
                    //    statusText = Util.translationList["000158"];
                    //}
                    //else if (status == JobStatus.Stopped)
                    //{
                    //    statusText = Util.translationList["000159"];
                    //}
                    //else if (status == JobStatus.Finished)
                    //{
                    //    statusText = Util.translationList["000160"];
                    //}
                    //else
                    //{
                    //    Debug.Assert(false);
                    //    statusText = "";
                    //}

                    //statusText += ((status == JobStatus.Stopped && job.RetryCount > 0) ? string.Format(" ({0})", job.RetryCount) : "");

                    //if (lvi.SubItems[4].Text != statusText)
                    //{
                    //    lvi.SubItems[4].Text = statusText;
                    //}

                    //if (status == JobStatus.Retrieving)
                    //{
                    //    string speedText = job.SpeedText;

                    //    if (lvi.SubItems[5].Text != speedText)
                    //    {
                    //        lvi.SubItems[5].Text = speedText;
                    //    }

                    //    string etaText = job.EtaText;

                    //    if (lvi.SubItems[6].Text != etaText)
                    //    {
                    //        lvi.SubItems[6].Text = etaText;
                    //    }
                    //}
                    //else
                    //{
                    //    if (job.Speed != -1 || job.Eta != -1)
                    //    {
                    //        job.ClearSpeed();
                    //    }

                    //    if (lvi.SubItems[5].Text != "")
                    //    {
                    //        lvi.SubItems[5].Text = "";
                    //    }

                    //    if (lvi.SubItems[6].Text != "")
                    //    {
                    //        lvi.SubItems[6].Text = "";
                    //    }
                    //}

                    //string noteText = job.NoteText;

                    //if (lvi.SubItems[7].Text != noteText)
                    //{
                    //    lvi.SubItems[7].Text = noteText;
                    //}
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
               // if (this.View == View.Details && this.Columns.Count > 0)
               //     this.Columns[this.Columns.Count - 1].Width = -2;
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
}
