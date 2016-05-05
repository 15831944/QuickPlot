using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VisualWget
{
    class VWgetAppContext : ApplicationContext
    {
        public VWgetAppContext(MainForm mainForm)
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            mainForm.FormClosed += new FormClosedEventHandler(OnFormClosed);

            if (!Util.StartInTray)
            {
                mainForm.Show();
            }
        }

        void OnApplicationExit(object sender, EventArgs e)
        {
        }

        void OnFormClosed(object sender, EventArgs e)
        {
            ExitThread();
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Util.LoadLang();

            string name = string.Format("{0} 2.0", Application.ProductName);

            if (Util.IsPortableApp())
            {
                name = "Global\\" + Util.GetHexString((new MD5CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(Assembly.GetExecutingAssembly().CodeBase)));
            }

            bool createdNew;
            Mutex mutex;

            try
            {
                mutex = new Mutex(true, name, out createdNew);
            }
            catch (Exception ex)
            {
                Util.MsgBox(string.Format(Util.translationList["000185"], ex.Message), MessageBoxIcon.Error);

                return;
            }

            if (createdNew)
            {
                if (args.Length == 1 && args[0].CompareTo("--start-in-tray") == 0)
                {
                    Util.StartInTray = true;
                }
                else if (args.Length > 0)
                {
                    Util.CmdLineArgs = Util.ArgsToCmdLineArgs(args);
                }

                Util.MainForm = new MainForm();
                Application.Run(new VWgetAppContext(Util.MainForm));
                mutex.ReleaseMutex();
            }
            else
            {
                Process currentProcess = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcesses();
                bool found = false;

                for (int i = 0; i < processes.Length; i++)
                {
                    Process p = processes[i];

                    if (p.Id == currentProcess.Id || p.ProcessName != "QuickPrint")
                    {
                        continue;
                    }

                    if (string.Compare(p.MainModule.FileName, currentProcess.MainModule.FileName, true) == 0)
                    {
                        Util.AllowSetForegroundWindow((uint)p.Id);
                        found = true;

                        break;
                    }
                }

                if (found)
                {
                    int port = int.Parse(Util.GetSetting("ListeningPort", true));

                    if (port != -1)
                    {
                        TcpClient client = new TcpClient("127.0.0.1", port);
                        NetworkStream stream = client.GetStream();

                        try
                        {
                            byte[] buffer = Encoding.Default.GetBytes("1\n");

                            stream.WriteTimeout = 1000;
                            stream.Write(buffer, 0, buffer.Length);

                            if (args.Length == 1 && args[0].CompareTo("--start-in-tray") == 0)
                            {
                            }
                            else
                            {
                                if (args.Length == 0 || !bool.Parse(Util.GetSetting("NoPromptOnNewJob")))
                                {
                                    buffer = Encoding.Default.GetBytes("0\n");
                                    stream.Write(buffer, 0, buffer.Length);
                                }

                                if (args.Length > 0)
                                {
                                    buffer = Encoding.Default.GetBytes(string.Format("1 {0}\n", Util.AddQuotes(Util.ArgsToCmdLineArgs(args))));
                                    stream.Write(buffer, 0, buffer.Length);
                                }
                            }

                            buffer = Encoding.Default.GetBytes("$");
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        catch
                        {
                        }

                        stream.Close();
                        client.Close();
                    }
                }
                else
                {
                    Util.MsgBox(Util.translationList["000186"], MessageBoxIcon.Exclamation);
                }
            }

            GC.KeepAlive(mutex);
        }
    }
}