using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;

namespace VisualWget
{
    public class UserAgentItem
    {
        public string name;
        public string value;

        public UserAgentItem(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return this.name;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct HDITEM
    {
        public uint mask;
        public int cxy;
        public string pszText;
        public IntPtr hbm;
        public int cchTextMax;
        public int fmt;
        public uint lParam;
        public int iImage;
        public int iOrder;
        public uint type;
        public IntPtr pvFilter;
    }

    struct OptCat
    {
        public string name;

        public OptCat(string name)
        {
            this.name = name;
        }
    }

    struct Opt
    {
        public int i;
        public int j;
        public string name;
        public bool needArg;
        public string argumentLabel;
        public string action;
        public string description;

        public Opt(int i, int j, string name, bool needArg, string argumentLabel, string action, string description)
        {
            this.i = i;
            this.j = j;
            this.name = name;
            this.needArg = needArg;
            this.argumentLabel = argumentLabel;
            this.action = action;
            this.description = description;
        }
    }

    class WindowWrapper : IWin32Window
    {
        IntPtr handle;

        public WindowWrapper(IntPtr handle)
        {
            this.handle = handle;
        }

        public IntPtr Handle
        {
            get
            {
                return handle;
            }
        }
    }

    static class Util
    {
        static MainForm mainForm = null;

        public static MainForm MainForm
        {
            get
            {
                return mainForm;
            }
            set
            {
                mainForm = value;
            }
        }

        static int listeningPort = -1;

        public static int ListeningPort
        {
            get
            {
                return listeningPort;
            }
            set
            {
                listeningPort = value;
                PutSetting("ListeningPort", listeningPort.ToString(), true);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool AllowSetForegroundWindow(uint dwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        static string appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KhomsanPh\\VisualWget\\2.0");

        public static string AppDataDir
        {
            get
            {
                return appDataDir;
            }
        }

        const string browserExtGuid = "{4b31ac95-6066-4d20-be12-c947eacafe2e}";
        static List<OptCat> optCats = new List<OptCat>();
        static List<Opt> opts = new List<Opt>();

        static string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string AppDir
        {
            get
            {
                return appDir;
            }
        }

        static string wgetPath = Path.Combine(appDir, "wget.exe");

        public static string WgetPath
        {
            get
            {
                return wgetPath;
            }
        }

        public static bool InitOpts()
        {
            List<OptCat> optCatsTemp = new List<OptCat>();
            List<Opt> optsTemp = new List<Opt>();

            string optFile = Path.Combine(langFolder, string.Format("wget_{0}.opt", Util.GetSetting("Lang")));

            if (!File.Exists(optFile))
            {
                optFile = Path.Combine(appDir, "wget.opt");
            }

            string[] optLines;

            try
            {
                optLines = File.ReadAllLines(optFile);
            }
            catch (Exception ex)
            {
                MsgBox(ex.Message, MessageBoxIcon.Error);

                return false;
            }

            int i = -2;
            int j = -1;

            for (int l = 0; l < optLines.Length; l++)
            {
                string optLine = optLines[l];

                optLine = optLine.Trim();

                if (optLine.StartsWith("#"))
                {
                    continue;
                }

                if (optLine == "")
                {
                    i++;
                    j = 0;

                    continue;
                }

                string[] optCols = optLine.Split('|');

                for (int c = 0; c < optCols.Length; c++)
                {
                    optCols[c] = optCols[c].Replace("{p}", "|");
                    optCols[c] = optCols[c].Trim();
                }

                if (optCols.Length == 1 && i == -1)
                {
                    optCatsTemp.Add(new OptCat(optCols[0]));
                    j++;
                }
                else if (optCols.Length == 5)
                {
                    optsTemp.Add(new Opt(
                        i,
                        j,
                        optCols[0],
                        bool.Parse(optCols[1]),
                        optCols[2],
                        optCols[3],
                        optCols[4]));
                    j++;
                }
                else
                {
                    MsgBox(string.Format(Util.translationList["000187"], optFile), MessageBoxIcon.Error);

                    return false;
                }
            }

            optCats = optCatsTemp;
            opts = optsTemp;

            return true;
        }

        public static OptCat GetOptCatByIndex(int i)
        {
            Debug.Assert(i < optCats.Count);

            return optCats[i];
        }

        public static Opt GetOptByIndex(int i)
        {
            Debug.Assert(i < opts.Count);

            return opts[i];
        }

        public static Opt GetOptByName(string name)
        {
            for (int index = 0; index < opts.Count; index++)
            {
                if (GetOptByIndex(index).name == name)
                {
                    return opts[index];
                }
            }

            return new Opt(-1, -1, "", false, "", "", "");
        }

        public static int GetOptIndex(int i, int j)
        {
            for (int index = 0; index < opts.Count; index++)
            {
                if (GetOptByIndex(index).i == i && GetOptByIndex(index).j == j)
                {
                    return index;
                }
            }

            return -1;
        }

        public static string[] GetArrayOfOpts(int i)
        {
            List<string> list = new List<string>();

            for (int index = 0; index < opts.Count; index++)
            {
                if (GetOptByIndex(index).i == i)
                {
                    string s = "--" + GetOptByIndex(index).name;

                    if (GetOptByIndex(index).needArg)
                    {
                        s += "=" + GetOptByIndex(index).argumentLabel;
                    }

                    list.Add(s);
                }
            }

            return list.ToArray();
        }

        public static string[] GetArrayOfOptCats()
        {
            List<string> list = new List<string>();

            for (int index = 0; index < optCats.Count; index++)
            {
                list.Add(GetOptCatByIndex(index).name);
            }

            return list.ToArray();
        }

        public static bool IsDownloadUIEnabled()
        {
            object value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer", "DownloadUI", "");

            return (value == null) ? false : (value.ToString() == browserExtGuid);
        }

        static bool extIsEnabling = false;

        public static bool ExtIsEnabling
        {
            get
            {
                return extIsEnabling;
            }
        }

#if false // removed
        public static void EnableDownloadUI(bool enable)
        {
            extIsEnabling = true;

            if (enable)
            {
                if (!IsBrowserExtRegistered(custDMExtPath))
                {
                    if (!RegisterBrowserExt())
                    {
                        MsgBox(Util.translationList["000183"], MessageBoxIcon.Error);

                        return;
                    }
                }
            }

            RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer", true);

            if (rk != null)
            {
                object value = rk.GetValue("DownloadUI");

                if (value != null)
                {
                    if (value.ToString() == browserExtGuid)
                    {
                        if (!enable)
                        {
                            rk.DeleteValue("DownloadUI");
                        }
                    }
                    else
                    {
                        if (enable)
                        {
                            if (Util.MsgBox(Util.translationList["000193"],
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                == DialogResult.Yes)
                            {
                                rk.SetValue("DownloadUI", browserExtGuid);
                            }
                        }
                    }
                }
                else
                {
                    if (enable)
                    {
                        rk.SetValue("DownloadUI", browserExtGuid);
                    }
                }

                rk.Close();
            }

            extIsEnabling = false;
        }
#endif

        public static void GetOpt(string cmdLineArgs, out StringCollection urls, out StringDictionary opts)
        {
            urls = new StringCollection();
            opts = new StringDictionary();

            string[] args = Util.CmdLineArgsToArgs(cmdLineArgs);

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if (arg.StartsWith("--"))
                {
                    string optName, optArg;
                    int index = arg.IndexOf("=");

                    if (index != -1)
                    {
                        optName = arg.Substring(2, index - 2);
                        optArg = arg.Substring(index + 1);
                    }
                    else
                    {
                        optName = arg.Substring(2);
                        optArg = null;
                    }

                    Opt opt = GetOptByName(optName);

                    if (opt.i == -1
                        || opt.needArg && optArg == null
                        || !opt.needArg && optArg != null)
                    {
                        continue;
                    }

                    opts.Add(optName, optArg);
                }
                else
                {
                    urls.Add(arg);
                }
            }
        }

        public static string GetFileName(string path)
        {
            if (path == null || path == "")
            {
                return path;
            }

            int nLast = path.LastIndexOf(Path.DirectorySeparatorChar);

            if (nLast >= 0)
            {
                return path.Substring(nLast + 1);
            }

            return path;
        }

        public static string GetAssemblyTitle()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

            if (attributes.Length == 0)
            {
                return "";
            }

            return ((AssemblyTitleAttribute)attributes[0]).Title;
        }

        public static string GetAssemblyCopyright()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

            if (attributes.Length == 0)
            {
                return "";
            }

            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }

        public static DialogResult MsgBox(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            if (mainForm != null)
            {
                if (!mainForm.Visible)
                {
                    mainForm.Show();
                }

                if (mainForm.WindowState == FormWindowState.Minimized)
                {
                    ShowWindow(mainForm.Handle, 9 /* SW_RESTORE */);
                }

                mainForm.SetAsForegroundWindow();

                return MessageBox.Show(new WindowWrapper(GetLastActivePopup(mainForm.Handle)), text, Application.ProductName, buttons, icon, defaultButton);
            }
            else
            {
                return MessageBox.Show(text, Application.ProductName, buttons, icon, defaultButton);
            }
        }

        public static DialogResult MsgBox(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MsgBox(text, buttons, icon, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult MsgBox(string text, MessageBoxIcon icon)
        {
            return MsgBox(text, MessageBoxButtons.OK, icon);
        }

        public static DialogResult MsgBox(string text)
        {
            return MsgBox(text, MessageBoxIcon.Information);
        }

#if false // removed
        static string appVer = "2.4a3u1";

        public static string AppVer
        {
            get
            {
                return appVer;
            }
        }

        static string releaseDate = "July 29, 2010";

        public static string ReleaseDate
        {
            get
            {
                return releaseDate;
            }
        }
#endif

        public static int GetTimeBetweenRetryAttempts()
        {
            int[] times = { 1, 3, 5, 10, 30, 60, 300, 900, 1800, 3600 };
            int index = int.Parse(Util.GetSetting("TimeBetweenRetryAttempts"));

            Debug.Assert(index >= 0 && index < 10);

            if (index >= 0 && index < 10)
            {
                return times[index];
            }

            return times[2];
        }

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, ref HDITEM lParam);
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, IntPtr lParam);

        public static void SetListViewHeaderArrow(ListView listView, int columnIndex, bool isAscending)
        {
            IntPtr header = (IntPtr)SendMessage(listView.Handle, 0x1000 + 31 /* LVM_GETHEADER */, 0, 0);

            for (int i = 0; i < listView.Columns.Count; i++)
            {
                HDITEM hdi = new HDITEM();

                hdi.mask = 0x0004 /* HDI_FORMAT */;
                SendMessage(header, 0x1200 + 11 /* HDM_GETITEM */, (uint)i, ref hdi);
                hdi.fmt &= ~(0x0200 /* HDF_SORTDOWN */ | 0x0400 /* HDF_SORTUP */);

                if (i == columnIndex)
                {
                    hdi.fmt |= isAscending ? 0x0400 /* HDF_SORTUP */ : 0x0200 /* HDF_SORTDOWN */;
                }

                SendMessage(header, 0x1200 + 12 /* HDM_SETITEM */, (uint)i, ref hdi);
            }

            SendMessage(listView.Handle, 0x1000 + 140 /* LVM_SETSELECTEDCOLUMN */, (uint)columnIndex, 0);
        }

        public static bool SetListViewColumnOrder(ListView listView, int[] columnOrderArray)
        {
            IntPtr buffer = Marshal.AllocHGlobal(sizeof(Int32) * columnOrderArray.Length);

            for (int i = 0; i < columnOrderArray.Length; i++)
            {
                Marshal.WriteInt32(buffer, i * sizeof(Int32), columnOrderArray[i]);
            }

            bool success = (SendMessage(listView.Handle, 0x1000 + 58 /* LVM_SETCOLUMNORDERARRAY */, (uint)listView.Columns.Count, buffer) != 0);
            Marshal.FreeHGlobal(buffer);

            return success;
        }

        public static bool GetListViewColumnOrder(ListView listView, out int[] columnOrderArray)
        {
            List<int> columnOrderList = new List<int>();
            IntPtr buffer = Marshal.AllocHGlobal(sizeof(Int32) * listView.Columns.Count);

            bool success = (SendMessage(listView.Handle, 0x1000 + 59 /* LVM_GETCOLUMNORDERARRAY */, (uint)listView.Columns.Count, buffer) != 0);

            if (success)
            {
                for (int i = 0; i < listView.Columns.Count; i++)
                {
                    columnOrderList.Add(Marshal.ReadInt32(buffer, i * sizeof(Int32)));
                }

                columnOrderArray = columnOrderList.ToArray();
            }
            else
            {
                columnOrderArray = null;
            }

            Marshal.FreeHGlobal(buffer);

            return success;
        }

        public static string GetSizeText(long size)
        {
            string sizeUnit = Util.GetSetting("SizeUnit");
            string text;

            if (sizeUnit == "B")
            {
                if (size < 0)
                {
                    text = "";
                }
                else if (size == 0)
                {
                    text = Util.translationList["000149"];
                }
                else
                {
                    text = string.Format("{0} {1}", size, Util.translationList["000150"]);
                }
            }
            else if (sizeUnit == "KB")
            {
                if (size < 0)
                {
                    text = "";
                }
                else if (size == 0)
                {
                    text = Util.translationList["000149"];
                }
                else
                {
                    double value = size / 1024.0;

                    text = string.Format("{0:F0} {1}", value, Util.translationList["000151"]);
                }
            }
            else if (sizeUnit == "MB")
            {
                if (size < 0)
                {
                    text = "";
                }
                else if (size == 0)
                {
                    text = Util.translationList["000149"];
                }
                else
                {
                    double value = size / (1024.0 * 1024.0);

                    text = string.Format("{0:F0} {1}", value, Util.translationList["000152"]);
                }
            }
            else if (sizeUnit == "GB")
            {
                if (size < 0)
                {
                    text = "";
                }
                else if (size == 0)
                {
                    text = Util.translationList["000149"];
                }
                else
                {
                    double value = size / (1024.0 * 1024.0 * 1024.0);

                    text = string.Format("{0:F0} {1}", value, Util.translationList["000153"]);
                }
            }
            else if (sizeUnit == "TB")
            {
                if (size < 0)
                {
                    text = "";
                }
                else if (size == 0)
                {
                    text = Util.translationList["000149"];
                }
                else
                {
                    double value = size / (1024.0 * 1024.0 * 1024.0 * 1024.0);

                    text = string.Format("{0:F0} {1}", value, Util.translationList["000154"]);
                }
            }
            else
            {
                if (size < 0)
                {
                    text = "";
                }
                else if (size == 0)
                {
                    text = Util.translationList["000149"];
                }
                else if (size < 1024)
                {
                    text = string.Format("{0} {1}", size, Util.translationList["000150"]);
                }
                else if (size < 1024 * 1024)
                {
                    double value = size / 1024.0;

                    if (value < 9.995)
                    {
                        text = string.Format("{0:F} {1}", value, Util.translationList["000151"]);
                    }
                    else if (value < 99.95)
                    {
                        text = string.Format("{0:F1} {1}", value, Util.translationList["000151"]);
                    }
                    else
                    {
                        text = string.Format("{0:F0} {1}", value, Util.translationList["000151"]);
                    }
                }
                else if (size < 1024 * 1024 * 1024)
                {
                    double value = size / (1024.0 * 1024.0);

                    if (value < 9.995)
                    {
                        text = string.Format("{0:F} {1}", value, Util.translationList["000152"]);
                    }
                    else if (value < 99.95)
                    {
                        text = string.Format("{0:F1} {1}", value, Util.translationList["000152"]);
                    }
                    else
                    {
                        text = string.Format("{0:F0} {1}", value, Util.translationList["000152"]);
                    }
                }
                else if (size < 1024.0 * 1024 * 1024 * 1024)
                {
                    double value = size / (1024.0 * 1024.0 * 1024.0);

                    if (value < 9.995)
                    {
                        text = string.Format("{0:F} {1}", value, Util.translationList["000153"]);
                    }
                    else if (value < 99.95)
                    {
                        text = string.Format("{0:F1} {1}", value, Util.translationList["000153"]);
                    }
                    else
                    {
                        text = string.Format("{0:F0} {1}", value, Util.translationList["000153"]);
                    }
                }
                else
                {
                    double value = size / (1024.0 * 1024.0 * 1024.0 * 1024.0);

                    if (value < 9.995)
                    {
                        text = string.Format("{0:F} {1}", value, Util.translationList["000154"]);
                    }
                    else if (value < 99.95)
                    {
                        text = string.Format("{0:F1} {1}", value, Util.translationList["000154"]);
                    }
                    else
                    {
                        text = string.Format("{0:F0} {1}", value, Util.translationList["000154"]);
                    }
                }
            }

            return text;
        }

        public static string GetSpeedText(long speed)
        {
            string text;

            if (speed < 0)
            {
                text = "";
            }
            else if (speed < 1024)
            {
                text = string.Format("{0} {1}", speed, Util.translationList["000161"]);
            }
            else if (speed < 1024 * 1024)
            {
                double value = speed / 1024.0;

                if (value < 9.995)
                {
                    text = string.Format("{0:F} {1}", value, Util.translationList["000162"]);
                }
                else if (value < 99.95)
                {
                    text = string.Format("{0:F1} {1}", value, Util.translationList["000162"]);
                }
                else
                {
                    text = string.Format("{0:F0} {1}", value, Util.translationList["000162"]);
                }
            }
            else if (speed < 1024 * 1024 * 1024)
            {
                double value = speed / (1024.0 * 1024.0);

                if (value < 9.995)
                {
                    text = string.Format("{0:F} {1}", value, Util.translationList["000163"]);
                }
                else if (value < 99.95)
                {
                    text = string.Format("{0:F1} {1}", value, Util.translationList["000163"]);
                }
                else
                {
                    text = string.Format("{0:F0} {1}", value, Util.translationList["000163"]);
                }
            }
            else
            {
                double value = speed / (1024.0 * 1024.0 * 1024.0);

                if (value < 9.995)
                {
                    text = string.Format("{0:F} {1}", value, Util.translationList["000164"]);
                }
                else if (value < 99.95)
                {
                    text = string.Format("{0:F1} {1}", value, Util.translationList["000164"]);
                }
                else
                {
                    text = string.Format("{0:F0} {1}", value, Util.translationList["000164"]);
                }
            }

            return text;
        }

        public static string GetHexString(byte[] bytes)
        {
            string hashString = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                hashString += string.Format("{0:x2}", bytes[i]);
            }

            return hashString;
        }

        public static string ComputeMD5Hash(string path)
        {
            return GetHexString((new MD5CryptoServiceProvider()).ComputeHash(File.OpenRead(path)));
        }

        public static string ComputeSHA1Hash(string path)
        {
            return GetHexString((new SHA1CryptoServiceProvider()).ComputeHash(File.OpenRead(path)));
        }

        static string regSubKeyName = "Software\\KhomsanPh\\VisualWget\\2.0";
        static string regKeyName = "HKEY_CURRENT_USER\\" + regSubKeyName;
        static string vwgetCfgPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "VisualWget.cfg");

        struct Setting
        {
            public string name;
            public string value;
            public string defaultValue;

            public Setting(string name, string defaultValue)
            {
                this.name = name;
                value = null;
                this.defaultValue = defaultValue;
            }
        }

        static bool settingsNeedToSave = false;

        static Setting[] settings =
        {
            new Setting("AlwaysShowTrayIcon", bool.TrueString),
            new Setting("AutoRemoveFinishedJob", bool.FalseString),
            new Setting("CheckForUpdates", bool.FalseString),
            new Setting("CheckForUpdatesInterval", "1"),
            new Setting("CheckForUpdatesTime", "-1"),
            new Setting("CloseToTray", bool.FalseString),
            new Setting("DefDlAutoStartNumDays", "0"),
            new Setting("DefDlAutoStartNumHours", "0"),
            new Setting("DefDlOptions", "--continue --directory-prefix=" + AddQuotes(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)) + " --timestamping"),
            new Setting("DetectClipboard", bool.FalseString),
            new Setting("DirPrefix0", ""),
            new Setting("DirPrefix1", ""),
            new Setting("DirPrefix2", ""),
            new Setting("DirPrefix3", ""),
            new Setting("DirPrefix4", ""),
            new Setting("DirPrefix5", ""),
            new Setting("DirPrefix6", ""),
            new Setting("DirPrefix7", ""),
            new Setting("FirstTimeRunning", bool.TrueString),
            new Setting("FtpProxy", ""),
            new Setting("FontName", "Tahoma"),
            new Setting("FontSize", (8.25F).ToString()),
            new Setting("FontStyle", "Regular"),
            new Setting("FontGdiCharSet", "0"),
            new Setting("Lang", "en"),
            new Setting("ListeningPort", "-1"),
            new Setting("LogHeight", "172"),
            new Setting("Height", "600"),
            new Setting("HideConsole", bool.TrueString),
            new Setting("HttpProxy", ""),
            new Setting("HttpsProxy", ""),
            new Setting("JobDialogStartOnOk", bool.TrueString),
            new Setting("JobsListViewColumnOrderArray", "0,1,2,3,4,5,6,7"),
            new Setting("JobsListViewColumnWidthArray", "170,35,75,75,80,80,75,170"),
            new Setting("JobsListViewSortAsc", bool.TrueString),
            new Setting("JobsListViewSortCol", "1"),
            new Setting("ListViewDoubleClickOpen", bool.TrueString),
            new Setting("MaxRunningJobs", "5"),
            new Setting("MinimizeToTray", bool.FalseString),
            new Setting("NoPromptOnNewJob", bool.FalseString),
            new Setting("NoProxy", ""),
            new Setting("PlayDownloadFinishedSound", bool.FalseString),
            new Setting("PlayAllDownloadsFinishedSound", bool.FalseString),
            new Setting("PromptWhenOpenFileTypes", "exe,com,pif,bat,scr"),
            new Setting("RedirectOutput", bool.TrueString),
            new Setting("Retry", bool.TrueString),
            new Setting("RetryAttempts", "30"),
            new Setting("ShowBalloonTip", bool.TrueString),
            new Setting("ShowLog", bool.TrueString),
            new Setting("ShowStatusBar", bool.TrueString),
            new Setting("ShowToolbar", bool.TrueString),
            new Setting("SizeUnit", "Auto"),
            new Setting("SpeedLimit", "0"),
            new Setting("SpeedLimitList", "4096,2048,1024,512,256,128,64,32,16,8,4,2"),
            new Setting("TimeBetweenRetryAttempts", "2"),
            new Setting("WhenDownloadsFinishedFreeze", bool.FalseString),
            new Setting("WhenDownloadsFinishedFreezedValue", AutoShutdownAction.None.ToString()),
            new Setting("WhenDownloadsFinishedCancelable", bool.TrueString),
            new Setting("Width", "800"),
            new Setting("WindowState", "Normal"),
            new Setting("X", "20"),
            new Setting("Y", "15")
        };

        static string LoadCfgSetting(string settingName, string defaultValue, string cfgPath)
        {
            string[] lines = File.ReadAllLines(cfgPath);

            foreach (string line in lines)
            {
                string[] segments = line.Split(new char[] { '=' }, 2);

                if (segments.Length == 2)
                {
                    string name = segments[0].Trim();

                    if (string.Compare(name, settingName, true) == 0)
                    {
                        string value = segments[1].Trim();

                        return value;
                    }
                }
            }

            return defaultValue;
        }

        static string LoadVisualWgetCfgSetting(string settingName, string defaultValue)
        {
            return LoadCfgSetting(settingName, defaultValue, vwgetCfgPath);
        }

        static void LoadVisualWgetCfg()
        {
            StringDictionary sd = new StringDictionary();
            string[] lines = File.ReadAllLines(vwgetCfgPath);

            foreach (string line in lines)
            {
                string[] segments = line.Split(new char[] { '=' }, 2);

                if (segments.Length == 2)
                {
                    string name = segments[0].Trim();
                    string value = segments[1].Trim();

                    sd.Add(name, value);
                }
            }

            for (int i = 0; i < settings.Length; i++)
            {
                Setting setting = settings[i];

                if (sd.ContainsKey(setting.name))
                {
                    setting.value = sd[setting.name];
                }
                else
                {
                    setting.value = setting.defaultValue;
                }

                settings[i] = setting;
            }
        }

        static void SaveVisualWgetCfg()
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < settings.Length; i++)
            {
                Setting setting = settings[i];

                if (setting.value != null
                    && setting.value != setting.defaultValue)
                {
                    lines.Add(setting.name + " = " + setting.value);
                }
            }

            File.WriteAllLines(vwgetCfgPath, lines.ToArray());
        }

        public static void LoadSettings()
        {
            if (IsPortableApp())
            {
                LoadVisualWgetCfg();
            }
            else
            {
                foreach (Setting setting in settings)
                {
                    GetSetting(setting.name);
                }
            }
        }

        public static string GetDefaultSetting(string settingName)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                Setting setting = settings[i];

                if (string.Compare(setting.name, settingName, true) == 0)
                {
                    return setting.defaultValue;
                }
            }

            return "";
        }

        public static string GetSetting(string settingName, bool load)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                Setting setting = settings[i];

                if (string.Compare(setting.name, settingName, true) == 0)
                {
                    if (setting.value != null && !load)
                    {
                        return setting.value;
                    }

                    if (IsPortableApp())
                    {
                        setting.value = LoadVisualWgetCfgSetting(setting.name, setting.defaultValue);
                    }
                    else
                    {
                        object value = Registry.GetValue(regKeyName, settingName, setting.defaultValue);

                        setting.value = ((value == null) ? setting.defaultValue : value.ToString());
                    }

                    settings[i] = setting;

                    return setting.value;
                }
            }

            Debug.Assert(false);

            return "";
        }

        public static string GetSetting(string settingName)
        {
            return GetSetting(settingName, false);
        }

        public static void PutSetting(string settingName, string settingValue, bool save)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                Setting setting = settings[i];

                if (setting.name == settingName)
                {
                    setting.value = settingValue;
                    settings[i] = setting;

                    if (save)
                    {
                        if (IsPortableApp())
                        {
                            try
                            {
                                SaveVisualWgetCfg();
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            Registry.SetValue(regKeyName, settingName, settingValue);
                        }
                    }
                    else
                    {
                        settingsNeedToSave = true;
                    }

                    break;
                }
            }
        }

        public static void PutSetting(string settingName, string settingValue)
        {
            //Console.Write(settingName);
            //Console.Write("=");
            //Console.WriteLine(settingValue);
            PutSetting(settingName, settingValue, false);
        }

        public static void SaveSettings()
        {
            if (settingsNeedToSave == false) return;
            settingsNeedToSave = false;
            //Console.WriteLine("SaveSettings");

            if (IsPortableApp())
            {
                try
                {
                    SaveVisualWgetCfg();
                }
                catch (Exception ex)
                {
                    Util.MsgBox(string.Format(Util.translationList["000190"], ex.Message), MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    Registry.CurrentUser.DeleteSubKeyTree(regSubKeyName);
                }
                catch
                {
                }

                for (int i = 0; i < settings.Length; i++)
                {
                    Setting setting = settings[i];

                    if (setting.value != null
                        && setting.value != setting.defaultValue)
                    {
                        Registry.SetValue(regKeyName, setting.name, setting.value);
                    }
                }
            }
        }

        public static bool IsPortableApp()
        {
            return true;
            //return File.Exists(vwgetCfgPath);
        }

        public static string AddQuotes(string s)
        {
            string result = "\"";
            string t = s;
            int i = t.IndexOf("\"");
            int j;

            while (i != -1)
            {
                result += t.Substring(0, i);

                for (j = i - 1; j >= 0; j--)
                {
                    if (t[j] == '\\')
                    {
                        result += "\\";
                    }
                    else
                    {
                        break;
                    }
                }

                result += "\\\"";

                t = t.Substring(i + 1);

                if (t == "")
                {
                    break;
                }

                i = t.IndexOf("\"");
            }

            result += t;

            for (j = t.Length - 1; j >= 0; j--)
            {
                if (t[j] == '\\')
                {
                    result += "\\";
                }
                else
                {
                    break;
                }
            }

            result += "\"";

            return result;
        }

        [DllImport("shell32.dll")]
        static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static string[] CmdLineArgsToArgs(string cmdLineArgs)
        {
            string executableName;

            return CommandLineToArgs("executableName " + cmdLineArgs, out executableName);
        }

        static string[] CommandLineToArgs(string commandLine, out string executableName)
        {
            int argCount;
            IntPtr result = CommandLineToArgvW(commandLine, out argCount);

            if (result == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            else
            {
                try
                {
                    executableName = Marshal.PtrToStringUni(Marshal.ReadIntPtr(result, 0 * IntPtr.Size));

                    string[] args = new string[argCount - 1];

                    for (int i = 0; i < args.Length; i++)
                    {
                        args[i] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(result, (i + 1) * IntPtr.Size));
                    }

                    return args;
                }
                finally
                {
                    Marshal.FreeHGlobal(result);
                }
            }
        }

        public static string ArgsToCmdLineArgs(string[] args)
        {
            string s = "";

            for (int i = 0; i < args.Length; i++)
            {
                s += AddQuotes(args[i]) + " ";
            }

            return s.Trim();
        }

        [DllImport("kernel32.dll")]
        static extern bool SetProcessWorkingSetSize(IntPtr hProcess, uint dwMinimumWorkingSetSize, uint dwMaximumWorkingSetSize);

        public static bool TrimCurrentProcessWorkingSet()
        {
            return SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, unchecked((uint)-1), unchecked((uint)-1));
        }

        static Random rand = new Random();

        public static int Rand(int max)
        {
            return rand.Next(max);
        }

        static string cmdLineArgs = "";

        public static string CmdLineArgs
        {
            get
            {
                return cmdLineArgs;
            }
            set
            {
                cmdLineArgs = value;
            }
        }

        static bool startInTray = false;

        public static bool StartInTray
        {
            get
            {
                return startInTray;
            }
            set
            {
                startInTray = value;
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetUpdateRect(IntPtr hWnd, out RECT lpRect, bool bErase);

        [DllImport("wtsapi32.dll")]
        public static extern bool WTSShutdownSystem(IntPtr hServer, uint ShutdownFlag);

        [DllImport("wtsapi32.dll")]
        public static extern bool WTSLogoffSession(IntPtr hServer, uint SessionId, bool bWait);

        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

        public static StringDictionary translationList = new StringDictionary();

        static string langFolder = Path.Combine(appDir, "Lang");

        public static string LangFolder
        {
            get
            {
                return langFolder;
            }
        }

        public static void LoadLang()
        {
            Util.translationList.Clear();

            string lang = Util.GetSetting("Lang");
            string path = Path.Combine(Util.LangFolder, lang + ".txt");

            if (lang != "en" && !File.Exists(path))
            {
                lang = "en";
                path = Path.Combine(Util.LangFolder, lang + ".txt");
            }

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line.StartsWith("#") || line == "")
                {
                    continue;
                }

                string[] cols = line.Split(new char[] { '=' }, 2);

                Util.translationList.Add(cols[0].Trim(), cols[1].Trim().Replace("\\n", "\n"));
            }
        }

        static string checkForUpdatesTemp = null;

        public static string CheckForUpdatesTemp
        {
            get
            {
                return checkForUpdatesTemp;
            }
            set
            {
                checkForUpdatesTemp = value;
            }
        }

        static string currentClipboardText = null;

        public static string CurrentClipboardText
        {
            get
            {
                return currentClipboardText;
            }
            set
            {
                currentClipboardText = value;
            }
        }

        public static void SetToolTip(ToolTip tt, Control.ControlCollection col)
        {
            //
            // not used now
            //
#if false
            foreach (Control ctl in col) {
                if (ctl.HasChildren) {
                    SetToolTip(tt, ctl.Controls);
                }

                //Console.WriteLine(ctl.GetType().ToString());

                if (ctl is Label || ctl is CheckBox || ctl is Button) {
                    StringBuilder sb = new StringBuilder(ctl.Text);

                    for (int i = 0; i < sb.Length; i++) {
                        if (sb[i] == '&') {
                            sb.Remove(i, 1);
                        }
                    }

                    tt.SetToolTip(ctl, sb.ToString());
                }
            }
#endif
        }

        public static bool IsRunWhenLogOn()
        {
            object temp = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                false
                ).GetValue("QuickPrint");

            if (temp == null)
            {
                return false;
            }

            string value1 = temp.ToString();
            string value2 = string.Format("\"{0}\" --start-in-tray", Path.Combine(Util.AppDir, "QuickPrint.exe"));

            return (string.Compare(value1, value2, true) == 0);
        }

        public static Font GetInterfaceFont()
        {
            string fontName = Util.GetSetting("FontName");
            float fontSize = float.Parse(Util.GetSetting("FontSize"));
            FontStyle fontStyle = (FontStyle)(Enum.Parse(typeof(FontStyle), Util.GetSetting("FontStyle")));
            byte fontGdiCharSet = byte.Parse(Util.GetSetting("FontGdiCharSet"));

            return new Font(fontName, fontSize, fontStyle, GraphicsUnit.Point, fontGdiCharSet);
        }

        public static bool IsDefaultFont()
        {
            string fontName = Util.GetSetting("FontName");
            float fontSize = float.Parse(Util.GetSetting("FontSize"));
            FontStyle fontStyle = (FontStyle)(Enum.Parse(typeof(FontStyle), Util.GetSetting("FontStyle")));
            byte fontGdiCharSet = byte.Parse(Util.GetSetting("FontGdiCharSet"));

            if (fontName != "Tahoma")
            {
                return false;
            }
            if (fontSize != 8.25F)
            {
                return false;
            }
            if (fontStyle != FontStyle.Regular)
            {
                return false;
            }
            if (fontGdiCharSet.ToString() != "0")
            {
                return false;
            }

            return true;
        }

        public static string GetTextFromClipboard()
        {
            //
            // http://msdn.microsoft.com/en-us/library/system.windows.forms.clipboard.aspx
            //
            string clipboardText = "";

            // Declares an IDataObject to hold the data returned from the clipboard.
            // Retrieves the data from the clipboard.
            IDataObject iData = Clipboard.GetDataObject();

            // Determines whether the data is in a format you can use.
            if (iData.GetDataPresent(DataFormats.Text))
            {
                // Yes it is.
                clipboardText = (String)iData.GetData(DataFormats.Text);
            }
            else
            {
                // No it is not.
            }

            return clipboardText;
        }

        public static bool IsDownloadable(string text)
        {
            if (text.StartsWith("http://"))
            {
                return true;
            }
            if (text.StartsWith("https://"))
            {
                return true;
            }
            if (text.StartsWith("ftp://"))
            {
                return true;
            }
            return false;
        }

        public static void DoAutoShutdown(AutoShutdownAction action)
        {
            if (action == AutoShutdownAction.Quit)
            {
                Application.Exit();
            }
            else if (action == AutoShutdownAction.StandBy)
            {
                Application.SetSuspendState(PowerState.Suspend, false, false);
            }
            else if (action == AutoShutdownAction.Hibernate)
            {
                Application.SetSuspendState(PowerState.Hibernate, false, false);
            }
            else if (action == AutoShutdownAction.TurnOff)
            {
                Util.WTSShutdownSystem(IntPtr.Zero, 0x00000008 /* WTS_WSD_POWEROFF */);
            }
            else if (action == AutoShutdownAction.Restart)
            {
                Util.WTSShutdownSystem(IntPtr.Zero, 0x00000004 /* WTS_WSD_REBOOT */);
            }
            else if (action == AutoShutdownAction.LogOff)
            {
                Util.WTSLogoffSession(IntPtr.Zero, unchecked((uint)-1), false);
            }
            else if (action == AutoShutdownAction.LockComputer)
            {
                Util.LockWorkStation();
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}
