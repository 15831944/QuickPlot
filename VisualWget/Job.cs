using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace VisualWget
{
    enum JobStatus
    {
        Ready = 0,
        Queued = 1,
        Running = 2,
        Retrieving = 3,
        Stopped = 4,
        Finished = 5
    }

    class Job
    {

        #region Enesy
        string drawing;
        
        #endregion

        bool queued;
        string url;
        string locf;
        string name;
        int num;
        long startPos;
        long toRead;
        long size;
        string sizeText;
        int bytesRead;
        long sumRead;
        long done;
        string doneText;
        long beginTime;

        struct SpeedHistoryItem
        {
            public long sumRead;
            public long elapsedTime;
            public long time;

            public SpeedHistoryItem(long sumRead, long elapsedTime, long time)
            {
                this.sumRead = sumRead;
                this.elapsedTime = elapsedTime;
                this.time = time;
            }
        }

        List<SpeedHistoryItem> speedHistory;
        long speed;
        string speedText;
        long eta;
        string etaText;

        StringCollection urls;
        StringDictionary opts;
        Process proc;
        int procId;
        bool isStarted;
        bool showBalloon;
        bool downloadComplete;
        JobStatus savedStatus;
        bool killed;
        long lastStoppedTime;
        int retryCount;
        long limitRate;
        bool limitRateChanged;
        string localFileChecker;
        bool localFileCheckerChanged;
        bool localFileChecked;
        BackgroundWorker stdOutReader, stdErrReader;
        StringBuilder log;
        bool logBegin;
        bool outputRedirected;
        long lastStartedTime;
        long lastStartedTimeActual;
        int autoStartNumDays;
        int autoStartNumHours;
        string noteText;

        public Job(string cmdLineArgs)
        {
            queued = false;
            url = "";
            locf = "";
            name = null;
            num = -1;
            startPos = -1;
            toRead = -1;
            size = -1;
            sizeText = "";
            bytesRead = -1;
            sumRead = 0;
            done = -1;
            doneText = "";
            beginTime = -1;
            speedHistory = new List<SpeedHistoryItem>();
            speed = -1;
            speedText = "";
            eta = -1;
            etaText = "";

            Util.GetOpt(cmdLineArgs, out urls, out opts);
            proc = null;
            procId = 0;
            isStarted = false;
            showBalloon = false;
            downloadComplete = true;
            savedStatus = JobStatus.Ready;
            killed = false;
            lastStoppedTime = -1;
            retryCount = 0;
            limitRate = 0;
            limitRateChanged = false;
            localFileChecker = "";
            localFileCheckerChanged = false;
            localFileChecked = false;
            stdOutReader = stdErrReader = null;
            log = new StringBuilder();
            logBegin = true;
            outputRedirected = false;
            lastStartedTime = -1;
            lastStartedTimeActual = -1;
            autoStartNumDays = 0;
            autoStartNumHours = 0;
            noteText = "";
        }

        public bool OutputRedirected
        {
            get
            {
                return outputRedirected;
            }
        }

        public int LogLength
        {
            get
            {
                return log.Length;
            }
        }

        public string Log
        {
            get
            {
                return log.ToString();
            }
        }

        public bool LogBegin
        {
            get
            {
                return logBegin;
            }
            set
            {
                logBegin = value;
            }
        }

        public bool LocalFileCheckerChanged
        {
            get
            {
                return localFileCheckerChanged;
            }
            set
            {
                localFileCheckerChanged = value;
            }
        }

        public string LocalFileChecker
        {
            get
            {
                return localFileChecker;
            }
        }

        public bool LocalFileChecked
        {
            get
            {
                return localFileChecked;
            }
            set
            {
                localFileChecked = value;
            }
        }

        public bool LimitRateChanged
        {
            get
            {
                return limitRateChanged;
            }
            set
            {
                limitRateChanged = value;
            }
        }

        public long LimitRate
        {
            get
            {
                return limitRate;
            }
            set
            {
                limitRate = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }
        }

        public int RetryCount
        {
            get
            {
                return retryCount;
            }
            set
            {
                retryCount = value;
            }
        }

        public long LastStoppedTime
        {
            get
            {
                return lastStoppedTime;
            }
            set
            {
                lastStoppedTime = value;
            }
        }

        public JobStatus SavedStatus
        {
            get
            {
                return savedStatus;
            }
            set
            {
                savedStatus = value;
            }
        }

        public bool ShowBalloon
        {
            get
            {
                return showBalloon;
            }
            set
            {
                showBalloon = value;
            }
        }

        public bool DownloadComplete
        {
            get
            {
                return downloadComplete;
            }
            set
            {
                downloadComplete = value;
            }
        }

        public string LocalFile
        {
            get
            {
                return locf;
            }
            set
            {
                locf = value;

                if (locf != "")
                {
                    name = Util.GetFileName(locf);
                }
                else
                {
                    name = null;
                }
            }
        }

        public bool Queued
        {
            get
            {
                return queued;
            }
            set
            {
                queued = value;
            }
        }

        public void DispatchMsg(string msg)
        {
            string[] segments = Util.CmdLineArgsToArgs(msg);

            Debug.Assert(segments.Length > 0);

            int msgId = int.Parse(segments[0]);

            if (msgId == 0)
            {
                Debug.Assert(segments.Length == 4);
                url = segments[1];
                localFileChecker = segments[2];
                localFileCheckerChanged = true;
                locf = segments[2].Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                name = Util.GetFileName(locf);

                if (opts.ContainsKey("recursive"))
                {
                    size = -1;
                    sizeText = Util.GetSizeText(size);
                    done = -1;
                    doneText = GetDoneText();
                }

                // segments[3]
            }
            else if (msgId == 1)
            {
                Debug.Assert(segments.Length == 4);
                startPos = long.Parse(segments[1]);
                toRead = long.Parse(segments[2]);

                if (toRead == 0)
                {
                    size = 0;
                }
                else
                {
                    size = startPos + toRead;
                }

                sizeText = Util.GetSizeText(size);
                beginTime = long.Parse(segments[3]);
                sumRead = 0;
                done = startPos + sumRead;
                doneText = GetDoneText();
                speedHistory.Clear();
                ClearSpeed();
            }
            else if (msgId == 2)
            {
                Debug.Assert(segments.Length == 4);
                bytesRead = (int)(long.Parse(segments[1]) - sumRead);
                sumRead += bytesRead;
                done = startPos + sumRead;
                doneText = GetDoneText();
                speedHistory.Add(new SpeedHistoryItem(sumRead, long.Parse(segments[2]), long.Parse(segments[3])));
            }
        }

        public int ProcId
        {
            get
            {
                return procId;
            }
        }

        public string Name
        {
            get
            {
                if (name == null)
                {
                    name = "";

                    for (int j = 0; j < urls.Count; j++)
                    {
                        if (name != "")
                        {
                            name += ", ";
                        }

                        string url = urls[j];
                        Uri uri;

                        try
                        {
                            uri = new Uri(url, UriKind.Absolute);
                        }
                        catch
                        {
                            name += url;

                            continue;
                        }

                        if (uri.Segments.Length > 0)
                        {
                            string lastSegment = uri.Segments[uri.Segments.Length - 1];
                            string fileName = Path.GetFileName(lastSegment);

                            if (Path.GetExtension(lastSegment) != "")
                            {
                                name += Path.GetFileName(lastSegment);

                                continue;
                            }
                        }

                        name += url;
                    }
                }

                return name;
            }
        }

        public int Num
        {
            get
            {
                return num;
            }
            set
            {
                num = value;
            }
        }

        public long Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                sizeText = Util.GetSizeText(size);
            }
        }

        public string SizeText
        {
            get
            {
                return sizeText;
            }
        }

        public long Done
        {
            get
            {
                return done;
            }
            set
            {
                done = value;
                doneText = GetDoneText();
            }
        }

        string GetDoneText()
        {
            string text;

            if (done < 0 || size <= 0 || done > size)
            {
                text = "";
            }
            else
            {
                text = string.Format("{0:F} %", done * 100.0 / size);
            }

            return text;
        }

        public string DoneText
        {
            get
            {
                return doneText;
            }
        }

        public JobStatus GetStatus()
        {
            if (queued)
            {
                return JobStatus.Queued;
            }

            if (!isStarted)
            {
                if (savedStatus == JobStatus.Stopped
                    || savedStatus == JobStatus.Finished)
                {
                    return savedStatus;
                }

                return JobStatus.Ready;
            }

            if (!proc.HasExited)
            {
                long lastPreviousTime = GetLastPreviousTime();

                if (lastPreviousTime != -1
                    && lastPreviousTime + 2.5 * 10000000 > DateTime.Now.Ticks)
                {
                    return JobStatus.Retrieving;
                }

                return JobStatus.Running;
            }

            if (proc.ExitCode == 0)
            {
                return JobStatus.Finished;
            }

            return JobStatus.Stopped;
        }

        public long GetLastPreviousTime()
        {
            return (speedHistory.Count > 0) ? speedHistory[speedHistory.Count - 1].time : -1;
        }

        public void UpdateSpeed()
        {
            ComputeSpeed();
            speedText = Util.GetSpeedText(speed);
            etaText = GetEtaText();
        }

        void ComputeSpeed()
        {
            int bytes = 0;
            double sec = 0;
            int i;

            for (i = speedHistory.Count - 2; i >= 0 && sec < 2.5; i--)
            {
                SpeedHistoryItem shi1 = speedHistory[i];
                SpeedHistoryItem shi2 = speedHistory[i + 1];

                bytes += (int)(shi2.sumRead - shi1.sumRead);
                sec += (shi2.elapsedTime - shi1.elapsedTime) / 1000.0;
            }

            while (i >= 0)
            {
                speedHistory.RemoveAt(i);
                i--;
            }

            if (sec > 0)
            {
                speed = (long)(bytes / sec);
            }
            else
            {
                speed = -1;
            }

            if (toRead == -1 || speed == -1)
            {
                eta = -1;
            }
            else if (speed == 0)
            {
                eta = -2;
            }
            else
            {
                eta = ((toRead - sumRead) * 1000) / speed;
            }
        }

        public void ClearSpeed()
        {
            speed = -1;
            speedText = "";
            eta = -1;
            etaText = "";
        }

        public long Speed
        {
            get
            {
                return speed;
            }
        }

        public string SpeedText
        {
            get
            {
                return speedText;
            }
        }

        public long Eta
        {
            get
            {
                return eta;
            }
        }

        string GetEtaText()
        {
            string text;

            if (eta == -2)
            {
                text = Util.translationList["000165"];
            }
            else if (eta < 0)
            {
                text = "";
            }
            else
            {
                long value = eta;
                int w, d, h, m, s, ms;

                w = (int)(value / (1000 * 60 * 60 * 24 * 7));
                value %= 1000 * 60 * 60 * 24 * 7;
                d = (int)(value / (1000 * 60 * 60 * 24));
                value %= 1000 * 60 * 60 * 24;
                h = (int)(value / (1000 * 60 * 60));
                value %= 1000 * 60 * 60;
                m = (int)(value / (1000 * 60));
                value %= 1000 * 60;
                s = (int)(value / 1000);
                value %= 1000;
                ms = (int)value;

                if (w > 0)
                {
                    text = string.Format("{0}{1} {2}{3}",
                        w, Util.translationList["000166"],
                        d, Util.translationList["000167"]);
                }
                else if (d > 0)
                {
                    text = string.Format("{0}{1} {2}{3}",
                        d, Util.translationList["000167"],
                        h, Util.translationList["000168"]);
                }
                else if (h > 0)
                {
                    text = string.Format("{0}{1} {2}{3}",
                        h, Util.translationList["000168"],
                        m, Util.translationList["000169"]);
                }
                else if (m > 0)
                {
                    text = string.Format("{0}{1} {2}{3}",
                        m, Util.translationList["000169"],
                        s, Util.translationList["000170"]);
                }
                else if (s > 0)
                {
                    text = string.Format("{0}{1}",
                        s, Util.translationList["000170"]);
                }
                else
                {
                    text = string.Format(".{0}s", ms / 100);
                }
            }

            return text;
        }

        public string EtaText
        {
            get
            {
                return etaText;
            }
        }

        public StringCollection Urls
        {
            get
            {
                return urls;
            }
            set
            {
                urls = value;
            }
        }

        public StringDictionary Opts
        {
            get
            {
                return opts;
            }
            set
            {
                opts = value;
            }
        }

        public string GetUrlsString()
        {
            string s = "";

            for (int i = 0; i < urls.Count; i++)
            {
                s += " " + Util.AddQuotes(urls[i]);
            }

            return s.Trim();
        }

        public string GetOptsString()
        {
            string s = "";
            string[] keys = new string[opts.Keys.Count];

            opts.Keys.CopyTo(keys, 0);
            Array.Sort(keys);

            foreach (string t in keys)
            {
                s += "--" + t;

                if (opts[t] != null)
                {
                    s += "=" + Util.AddQuotes(opts[t]);
                }

                s += " ";
            }

            return s.Trim();
        }

        public string GetCmdLineArgs()
        {
            string cmdLineArgs = GetOptsString() + " " + GetUrlsString();

            return cmdLineArgs.Trim();
        }

        public bool IsRunning()
        {
            if (!isStarted)
            {
                return false;
            }

            return !proc.HasExited;
        }

        public void Start()
        {
            Start(true, false, false);
        }

        public void Start(bool listen, bool forceHideConsole, bool forceRedirectOutput)
        {
            if (!IsRunning())
            {
                Close();

                bool background = opts.ContainsKey("background");

                if (listen && !background)
                {
                    opts.Add("visualwget-listening-port", Util.ListeningPort.ToString());
                }

                string arguments = GetCmdLineArgs();

                if (listen && !background)
                {
                    opts.Remove("visualwget-listening-port");
                }

                if (opts.ContainsKey("execute"))
                {
                    string s = "--execute=" + Util.AddQuotes(opts["execute"]);
                    string t = "";
                    string[] cmds = Util.CmdLineArgsToArgs(opts["execute"]);

                    for (int i = 0; i < cmds.Length; i++)
                    {
                        t += " --execute=" + Util.AddQuotes(cmds[i]);
                    }

                    t = t.Trim();
                    Debug.Assert(arguments.Contains(s));
                    arguments = arguments.Replace(s, t);
                }

                proc = new Process();
                proc.StartInfo.FileName = Util.WgetPath;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(Util.WgetPath);

                if (forceHideConsole
                    || bool.Parse(Util.GetSetting("HideConsole")))
                {
                    proc.StartInfo.CreateNoWindow = true;

                    if (forceRedirectOutput
                        || bool.Parse(Util.GetSetting("RedirectOutput")))
                    {
                        proc.StartInfo.RedirectStandardOutput = true;
                        proc.StartInfo.RedirectStandardError = true;

                        // We've to create a new BackgroundWorker each time the process start since
                        // BackgroundWorker.CancelAsync() does not set BackgroundWorker.IsBusy to false
                        // and we can not run the BackgroundWorker while it is busy

                        stdOutReader = new BackgroundWorker();
                        stdOutReader.DoWork += new DoWorkEventHandler(stdOutReader_DoWork);
                        stdOutReader.ProgressChanged += new ProgressChangedEventHandler(stdOutReader_ProgressChanged);
                        stdOutReader.WorkerReportsProgress = true;
                        stdOutReader.WorkerSupportsCancellation = true;

                        stdErrReader = new BackgroundWorker();
                        stdErrReader.DoWork += new DoWorkEventHandler(stdErrReader_DoWork);
                        stdErrReader.ProgressChanged += new ProgressChangedEventHandler(stdErrReader_ProgressChanged);
                        stdErrReader.WorkerReportsProgress = true;
                        stdErrReader.WorkerSupportsCancellation = true;

                        outputRedirected = true;
                    }
                }

                proc.Start();
                procId = proc.Id;

                if (stdOutReader != null)
                {
                    stdOutReader.RunWorkerAsync(proc.StandardOutput);
                }

                if (stdErrReader != null)
                {
                    stdErrReader.RunWorkerAsync(proc.StandardError);
                }

                if (!outputRedirected)
                {
                    log.Append("Output not redirected!");
                }

                isStarted = true;
                showBalloon = true;
                downloadComplete = false;

                DateTime now = DateTime.Now;

                lastStartedTime = (new DateTime(now.Year, now.Month, now.Day)).Ticks;
                lastStartedTimeActual = now.Ticks;
            }
        }

        void CancelLog()
        {
            if (outputRedirected)
            {
                if (stdOutReader != null)
                {
                    if (stdOutReader.IsBusy)
                    {
                        stdOutReader.CancelAsync();
                    }

                    stdOutReader.ProgressChanged -= new ProgressChangedEventHandler(stdOutReader_ProgressChanged);
                    stdOutReader = null;
                }

                if (stdErrReader != null)
                {
                    if (stdErrReader.IsBusy)
                    {
                        stdErrReader.CancelAsync();
                    }

                    stdErrReader.ProgressChanged -= new ProgressChangedEventHandler(stdErrReader_ProgressChanged);
                    stdErrReader = null;
                }

                outputRedirected = false;
            }
        }

        void LogDoWork(BackgroundWorker bw, StreamReader sr)
        {
            while (!sr.EndOfStream && !bw.CancellationPending)
            {
                StringBuilder sb = new StringBuilder();

                while (sr.Peek() != -1)
                {
                    sb.Append((char)sr.Read());
                }

                bw.ReportProgress(0, sb.ToString());
                Thread.Sleep(1);
            }
        }

        void stdOutReader_DoWork(object sender, DoWorkEventArgs e)
        {
            LogDoWork((BackgroundWorker)sender, (StreamReader)e.Argument);
        }

        void stdErrReader_DoWork(object sender, DoWorkEventArgs e)
        {
            LogDoWork((BackgroundWorker)sender, (StreamReader)e.Argument);
        }

        void stdOutReader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            log.Append(e.UserState.ToString());
        }

        void stdErrReader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            log.Append(e.UserState.ToString());
        }

        public void Stop()
        {
            Stop(null);
        }

        public void Stop(string errorMessage)
        {
            if (IsRunning() && !killed)
            {
                try
                {
                    proc.Kill();
                }
                catch
                {
                }

                killed = true;

                if (errorMessage != null)
                {
                    CancelLog();
                    log.Remove(0, log.Length);
                    logBegin = true;
                    log.Append(errorMessage);
                }
            }
        }

        public void Close()
        {
            if (isStarted)
            {
                Debug.Assert(!IsRunning());
                proc.Close();
                proc.Dispose();
                proc = null;
                procId = 0;
                isStarted = false;
                killed = false;
                lastStoppedTime = -1;
                speedHistory.Clear();
                ClearSpeed();
                limitRate = 0;
                limitRateChanged = false;
                localFileChecker = "";
                localFileCheckerChanged = false;
                localFileChecked = false;
                CancelLog();
                log.Remove(0, log.Length);
                logBegin = true;
            }
        }

        public bool IsStopped()
        {
            return (isStarted && proc.HasExited && proc.ExitCode != 0 && !killed);
        }

        public void Kill()
        {
            if (isStarted)
            {
                killed = true;
            }
        }

        public bool IsStarted
        {
            get
            {
                return isStarted;
            }
        }

        public bool WaitForExit(int msec)
        {
            if (IsRunning())
            {
                return proc.WaitForExit(msec);
            }

            return true;
        }

        public long LastStartedTime
        {
            get
            {
                return lastStartedTime;
            }
            set
            {
                lastStartedTime = value;
            }
        }

        public long LastStartedTimeActual
        {
            get
            {
                return lastStartedTimeActual;
            }
            set
            {
                lastStartedTimeActual = value;
            }
        }

        public int AutoStartNumDays
        {
            get
            {
                return autoStartNumDays;
            }
            set
            {
                autoStartNumDays = value;
            }
        }

        public int AutoStartNumHours
        {
            get
            {
                return autoStartNumHours;
            }
            set
            {
                autoStartNumHours = value;
            }
        }

        public string NoteText
        {
            get
            {
                return noteText;
            }
            set
            {
                noteText = value;
            }
        }

        public bool IsScheduled
        {
            get
            {
                bool b = false;

                if (autoStartNumDays >= 1 && autoStartNumDays <= 365)
                {
                    if (lastStartedTime == -1)
                    {
                        b = true;
                    }
                    else
                    {
                        long now = DateTime.Now.Ticks;

                        Debug.Assert(now - lastStartedTime > 0);

                        TimeSpan ts = new TimeSpan(now - lastStartedTime);

                        if (ts.TotalDays >= autoStartNumDays)
                        {
                            b = true;
                        }
                    }
                }
                else if (autoStartNumHours >= 1 && autoStartNumHours <= 168)
                {
                    if (lastStartedTimeActual == -1)
                    {
                        b = true;
                    }
                    else
                    {
                        long now = DateTime.Now.Ticks;

                        Debug.Assert(now - lastStartedTimeActual > 0);

                        TimeSpan ts = new TimeSpan(now - lastStartedTimeActual);

                        if (ts.TotalHours >= autoStartNumHours)
                        {
                            b = true;
                        }
                    }
                }

                return b;
            }
        }

        public string GetNotifyJobName()
        {
            string notifyName;

            if (opts.ContainsKey("recursive"))
            {
                if (urls.Count > 1)
                {
                    notifyName = "..., " + urls[urls.Count - 1];
                }
                else
                {
                    notifyName = urls[0];
                }
            }
            else
            {
                if (urls.Count > 1)
                {
                    notifyName = "..., " + this.Name;
                }
                else
                {
                    notifyName = this.Name;
                }
            }

            if (notifyName == "")
            {
                notifyName = "N/A";
            }

            return notifyName;
        }
    }
}
