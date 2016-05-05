using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;

namespace VisualWget
{
    public partial class PreferencesDialog : Form
    {
        public PreferencesDialog()
        {
            InitializeComponent();
        }

        class Language
        {
            public int index;
            public string lang;
            public string langText;

            public Language(string lang, string langText)
            {
                index = -1;
                this.lang = lang;
                this.langText = langText;
            }
        }

        List<Language> languages;

        int CompareLangText(Language x, Language y)
        {
            return x.langText.CompareTo(y.langText);
        }

        class LangComparer : IComparer<Language>
        {
            string lang;

            public LangComparer(string lang)
            {
                this.lang = lang;
            }

            public int Compare(Language x, Language y)
            {
                if (x.lang == "")
                {
                    if (y.lang == "")
                    {
                        return 0;
                    }
                    else
                    {
                        return lang.CompareTo(y.lang);
                    }
                }
                else
                {
                    if (y.lang == "")
                    {
                        return x.lang.CompareTo(lang);
                    }
                    else
                    {
                        return x.lang.CompareTo(y.lang);
                    }
                }
            }
        }

        private void PreferencesDialog_Load(object sender, System.EventArgs e)
        {
            prefCatListBox.SelectedIndex = 0;

            speedLimitNumericUpDown.Value = decimal.Parse(Util.GetSetting("SpeedLimit"));
            maxRunningJobsNumericUpDown.Value = decimal.Parse(Util.GetSetting("MaxRunningJobs"));
            retryCheckBox.Checked = bool.Parse(Util.GetSetting("Retry"));
            retryAttemptsNumericUpDown.Value = decimal.Parse(Util.GetSetting("RetryAttempts"));
            timeBetweenRetryAttemptsComboBox.Items.AddRange(new string[] {
                "1 " + Util.translationList["000094"],
                "3 " + Util.translationList["000095"],
                "5 " + Util.translationList["000095"],
                "10 " + Util.translationList["000095"],
                "30 " + Util.translationList["000095"],
                "1 " + Util.translationList["000096"],
                "5 " + Util.translationList["000097"],
                "15 " + Util.translationList["000097"],
                "30 " + Util.translationList["000097"],
                "1 " + Util.translationList["000098"]
            });
            timeBetweenRetryAttemptsComboBox.SelectedIndex = int.Parse(Util.GetSetting("TimeBetweenRetryAttempts"));
            retryAttemptsLabel.Enabled = retryAttemptsNumericUpDown.Enabled = timeBetweenRetryAttemptsLabel.Enabled = timeBetweenRetryAttemptsComboBox.Enabled = retryCheckBox.Checked;
            checkForUpdatesCheckBox.Checked = bool.Parse(Util.GetSetting("CheckForUpdates"));
            checkForUpdatesComboBox.Items.AddRange(new string[] {
                Util.translationList["000101"],
                Util.translationList["000102"]
            });
            checkForUpdatesComboBox.SelectedIndex = int.Parse(Util.GetSetting("CheckForUpdatesInterval"));
            checkForUpdatesComboBox.Enabled = checkForUpdatesCheckBox.Checked;
            closeToTrayCheckBox.Checked = bool.Parse(Util.GetSetting("CloseToTray"));
            minimizeToTrayCheckBox.Checked = bool.Parse(Util.GetSetting("MinimizeToTray"));
            showBalloonTipCheckBox.Checked = bool.Parse(Util.GetSetting("ShowBalloonTip"));
            alwaysShowTrayIconCheckBox.Checked = bool.Parse(Util.GetSetting("AlwaysShowTrayIcon"));
            hideConsolesCheckBox.Checked = bool.Parse(Util.GetSetting("HideConsole"));

            if (hideConsolesCheckBox.Checked)
            {
                redirectOutputsCheckBox.Checked = bool.Parse(Util.GetSetting("RedirectOutput"));
            }
            else
            {
                redirectOutputsCheckBox.Checked = redirectOutputsCheckBox.Enabled = false;
            }

            listViewDoubleClickOpenCheckBox.Checked = bool.Parse(Util.GetSetting("ListViewDoubleClickOpen"));
            promptWhenOpenFileTypesTextBox.Text = Util.GetSetting("PromptWhenOpenFileTypes");

            string sizeUnit = Util.GetSetting("SizeUnit");
            if (sizeUnit == "B") { sizeBytesRadioButton.Checked = true; }
            else if (sizeUnit == "KB") { sizeKbRadioButton.Checked = true; }
            else if (sizeUnit == "MB") { sizeMbRadioButton.Checked = true; }
            else if (sizeUnit == "GB") { sizeGbRadioButton.Checked = true; }
            else if (sizeUnit == "TB") { sizeTbRadioButton.Checked = true; }
            else { sizeAutoRadioButton.Checked = true; }

            runWhenLogOnCheckBox.Checked = Util.IsRunWhenLogOn();

            languages = new List<Language>();

            string[] langFiles = Directory.GetFiles(Util.LangFolder);

            for (int i = 0; i < langFiles.Length; i++)
            {
                if (Path.GetExtension(langFiles[i]).ToLower() != ".txt")
                {
                    continue;
                }

                string langText = "";
                string[] lines = File.ReadAllLines(langFiles[i]);

                for (int j = 0; j < lines.Length; j++)
                {
                    string line = lines[j];

                    if (line.StartsWith("#") || line == "")
                    {
                        continue;
                    }

                    string[] cols = line.Split('=');
                    int id = int.Parse(cols[0].Trim());

                    cols[1] = cols[1].Trim();

                    if (id == 0)
                    {
                        langText = cols[1];

                        break;
                    }
                }

                if (langText != "")
                {
                    languages.Add(new Language(Path.GetFileNameWithoutExtension(langFiles[i]), langText));
                }
            }

            LangComparer lc = new LangComparer(Util.GetSetting("Lang"));

            languages.Sort(lc);

            for (int i = 0; i < languages.Count; i++)
            {
                languages[i].index = i;
            }

            languages.Sort(CompareLangText);

            List<int> indexes = new List<int>();

            for (int i = 0; i < languages.Count; i++)
            {
                languageComboBox.Items.Add(languages[i].langText);
                indexes.Add(languages[i].index);
            }

            languageComboBox.Tag = indexes;
            languages.Sort(lc);

            int index = languages.BinarySearch(new Language("", ""), lc);

            if (languages.Count > 0 && index >= 0)
            {
                int i;

                for (i = 0; i < indexes.Count; i++)
                {
                    if (indexes[i] == index)
                    {
                        break;
                    }
                }

                languageComboBox.SelectedIndex = i;
            }

            httpProxyTextBox.Text = Util.GetSetting("HttpProxy");
            httpsProxyTextBox.Text = Util.GetSetting("HttpsProxy");
            ftpProxyTextBox.Text = Util.GetSetting("FtpProxy");
            noProxyTextBox.Text = Util.GetSetting("NoProxy");
            noPromptOnNewJobCheckBox.Checked = bool.Parse(Util.GetSetting("NoPromptOnNewJob"));

            downloadFinishedSoundCheckBox.Checked = bool.Parse(Util.GetSetting("PlayDownloadFinishedSound"));
            allDownloadsFinishedSoundCheckBox.Checked = bool.Parse(Util.GetSetting("PlayAllDownloadsFinishedSound"));

            Translate();

            speedLimitLabel1.SendToBack();
            maxRunningJobsLabel.SendToBack();
            retryAttemptsLabel.SendToBack();
            timeBetweenRetryAttemptsLabel.SendToBack();
            checkForUpdatesCheckBox.SendToBack();
            languageLabel.SendToBack();
            closeToTrayCheckBox.SendToBack();
            showBalloonTipCheckBox.SendToBack();
            hideConsolesCheckBox.SendToBack();
            httpProxyLabel.SendToBack();
            httpsProxyLabel.SendToBack();
            ftpProxyLabel.SendToBack();
            noProxyLabel.SendToBack();
            Util.SetToolTip(toolTip1, Controls);

            this.SetInterFaceFont();
        }

        void Translate()
        {
            Text = Util.translationList["000082"];
            prefCatListBox.Items[0] = Util.translationList["000083"];
            speedLimitGroupBox.Text = Util.translationList["000084"];
            speedLimitLabel1.Text = Util.translationList["000085"];
            speedLimitLabel2.Text = Util.translationList["000086"];
            queueGroupBox.Text = Util.translationList["000087"];
            maxRunningJobsLabel.Text = Util.translationList["000088"];
            retryingGroupBox.Text = Util.translationList["000089"];
            retryNoticeLabel.Text = Util.translationList["000090"];
            retryCheckBox.Text = Util.translationList["000091"];
            retryAttemptsLabel.Text = Util.translationList["000092"];
            timeBetweenRetryAttemptsLabel.Text = Util.translationList["000093"];
            updatesGroupBox.Text = Util.translationList["000099"];
            checkForUpdatesCheckBox.Text = Util.translationList["000100"];
            prefCatListBox.Items[1] = Util.translationList["000103"];
            translationsGroupBox.Text = Util.translationList["000104"];
            languageLabel.Text = Util.translationList["000105"];
            systemTrayGroupBox.Text = Util.translationList["000106"];
            closeToTrayCheckBox.Text = Util.translationList["000107"];
            minimizeToTrayCheckBox.Text = Util.translationList["000108"];
            showBalloonTipCheckBox.Text = Util.translationList["000109"];
            alwaysShowTrayIconCheckBox.Text = Util.translationList["000110"];
            wgetConsolesGroupBox.Text = Util.translationList["000111"];
            hideConsolesCheckBox.Text = Util.translationList["000112"];
            redirectOutputsCheckBox.Text = Util.translationList["000113"];
            listViewGroupBox.Text = Util.translationList["000198"];
            listViewDoubleClickOpenCheckBox.Text = Util.translationList["000199"];
            promptWhenOpenFileTypesLabel.Text = Util.translationList["000200"];

            sizeUnitGroupBox.Text = Util.translationList["000230"];
            sizeAutoRadioButton.Text = Util.translationList["000229"];
            sizeBytesRadioButton.Text = Util.translationList["000150"];
            sizeKbRadioButton.Text = Util.translationList["000151"];
            sizeMbRadioButton.Text = Util.translationList["000152"];
            sizeGbRadioButton.Text = Util.translationList["000153"];
            sizeTbRadioButton.Text = Util.translationList["000154"];

            prefCatListBox.Items[2] = Util.translationList["000242"];
            systemSoundsGroupBox.Text = Util.translationList["000243"];
            downloadFinishedSoundCheckBox.Text = Util.translationList["000244"];
            allDownloadsFinishedSoundCheckBox.Text = Util.translationList["000245"];

            fontGroupBox.Text = Util.translationList["000201"];
            setInterfaceFontButton.Text = Util.translationList["000202"];
            useDefaultFontButton.Text = Util.translationList["000203"];
            logOnGroupBox.Text = Util.translationList["000204"];
            runWhenLogOnCheckBox.Text = Util.translationList["000205"];
            newJobGroupBox.Text = Util.translationList["000206"];
            noPromptOnNewJobCheckBox.Text = Util.translationList["000207"];
            prefCatListBox.Items[3] = Util.translationList["000114"];
            proxiesGroupBox.Text = Util.translationList["000115"];
            httpProxyLabel.Text = Util.translationList["000116"];
            httpsProxyLabel.Text = Util.translationList["000195"];
            ftpProxyLabel.Text = Util.translationList["000117"];
            noProxyLabel.Text = Util.translationList["000118"];
            okButton.Text = Util.translationList["000119"];
            cancelButton.Text = Util.translationList["000120"];
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            Util.PutSetting("SpeedLimit", speedLimitNumericUpDown.Value.ToString());
            Util.PutSetting("MaxRunningJobs", maxRunningJobsNumericUpDown.Value.ToString());
            Util.PutSetting("Retry", retryCheckBox.Checked.ToString());
            Util.PutSetting("RetryAttempts", retryAttemptsNumericUpDown.Value.ToString());
            Util.PutSetting("TimeBetweenRetryAttempts", timeBetweenRetryAttemptsComboBox.SelectedIndex.ToString());
            Util.PutSetting("CheckForUpdates", checkForUpdatesCheckBox.Checked.ToString());
            Util.PutSetting("CheckForUpdatesInterval", checkForUpdatesComboBox.SelectedIndex.ToString());
            Util.PutSetting("CloseToTray", closeToTrayCheckBox.Checked.ToString());
            Util.PutSetting("MinimizeToTray", minimizeToTrayCheckBox.Checked.ToString());
            Util.PutSetting("ShowBalloonTip", showBalloonTipCheckBox.Checked.ToString());
            Util.PutSetting("AlwaysShowTrayIcon", alwaysShowTrayIconCheckBox.Checked.ToString());
            Util.PutSetting("HideConsole", hideConsolesCheckBox.Checked.ToString());
            Util.PutSetting("RedirectOutput", redirectOutputsCheckBox.Checked.ToString());
            Util.PutSetting("ListViewDoubleClickOpen", listViewDoubleClickOpenCheckBox.Checked.ToString());
            Util.PutSetting("PromptWhenOpenFileTypes", promptWhenOpenFileTypesTextBox.Text);

            string sizeUnit = "Auto";
            if (sizeBytesRadioButton.Checked) { sizeUnit = "B"; }
            else if (sizeKbRadioButton.Checked) { sizeUnit = "KB"; }
            else if (sizeMbRadioButton.Checked) { sizeUnit = "MB"; }
            else if (sizeGbRadioButton.Checked) { sizeUnit = "GB"; }
            else if (sizeTbRadioButton.Checked) { sizeUnit = "TB"; }
            Util.PutSetting("SizeUnit", sizeUnit);

            if (runWhenLogOnCheckBox.Checked)
            {
                Registry.SetValue(
                    "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                    "QuickPrint",
                    string.Format("\"{0}\" --start-in-tray", Path.Combine(Util.AppDir, "QuickPrint.exe"))
                    );
            }
            else
            {
                Registry.CurrentUser.OpenSubKey(
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                    true
                    ).DeleteValue("QuickPrint", false);
            }

            if (languageComboBox.SelectedIndex >= 0)
            {
                string lang = Util.GetSetting("Lang");

                for (int i = 0; i < languages.Count; i++)
                {
                    Language language = languages[i];
                    List<int> indexes = (List<int>)languageComboBox.Tag;

                    if (language.index == indexes[languageComboBox.SelectedIndex])
                    {
                        lang = language.lang;

                        break;
                    }
                }

                Util.PutSetting("Lang", lang);
                Util.LoadLang();
                Util.MainForm.Translate();
            }

            string httpProxy = httpProxyTextBox.Text.Trim();
            string httpsProxy = httpsProxyTextBox.Text.Trim();
            string ftpProxy = ftpProxyTextBox.Text.Trim();
            string noProxy = noProxyTextBox.Text.Trim();

            Util.PutSetting("HttpProxy", httpProxy);
            Util.PutSetting("HttpsProxy", httpsProxy);
            Util.PutSetting("FtpProxy", ftpProxy);
            Util.PutSetting("NoProxy", noProxy);
            Environment.SetEnvironmentVariable("http_proxy", httpProxy);
            Environment.SetEnvironmentVariable("https_proxy", httpsProxy);
            Environment.SetEnvironmentVariable("ftp_proxy", ftpProxy);
            Environment.SetEnvironmentVariable("no_proxy", noProxy);

            Util.PutSetting("NoPromptOnNewJob", noPromptOnNewJobCheckBox.Checked.ToString());

            Util.PutSetting("PlayDownloadFinishedSound", downloadFinishedSoundCheckBox.Checked.ToString());
            Util.PutSetting("PlayAllDownloadsFinishedSound", allDownloadsFinishedSoundCheckBox.Checked.ToString());
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void retryCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            retryAttemptsLabel.Enabled = retryAttemptsNumericUpDown.Enabled = timeBetweenRetryAttemptsLabel.Enabled = timeBetweenRetryAttemptsComboBox.Enabled = retryCheckBox.Checked;
        }

        private void checkForUpdatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForUpdatesComboBox.Enabled = checkForUpdatesCheckBox.Checked;
        }

        private void hideConsoleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hideConsolesCheckBox.Checked)
            {
                redirectOutputsCheckBox.Enabled = true;
            }
            else
            {
                redirectOutputsCheckBox.Checked = redirectOutputsCheckBox.Enabled = false;
            }
        }

        private void prefCatListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prefCatListBox.SelectedIndex == 0)
            {
                DisableAllPanels();
                this.generalPanel.Enabled = true;
                this.generalPanel.BringToFront();
            }
            else if (prefCatListBox.SelectedIndex == 1)
            {
                DisableAllPanels();
                this.interfacePanel.Enabled = true;
                this.interfacePanel.BringToFront();
            }
            else if (prefCatListBox.SelectedIndex == 2)
            {
                DisableAllPanels();
                this.soundsPanel.Enabled = true;
                this.soundsPanel.BringToFront();
            }
            else if (prefCatListBox.SelectedIndex == 3)
            {
                DisableAllPanels();
                this.othersPanel.Enabled = true;
                this.othersPanel.BringToFront();
            }
        }

        private void DisableAllPanels()
        {
            this.generalPanel.Enabled = false;
            this.interfacePanel.Enabled = false;
            this.soundsPanel.Enabled = false;
            this.othersPanel.Enabled = false;
        }

        private void setInterfaceFontButton_Click(object sender, EventArgs e)
        {
            string fontName = Util.GetSetting("FontName");
            float fontSize = float.Parse(Util.GetSetting("FontSize"));
            FontStyle fontStyle = (FontStyle)(Enum.Parse(typeof(FontStyle), Util.GetSetting("FontStyle")));
            byte fontGdiCharSet = byte.Parse(Util.GetSetting("FontGdiCharSet"));

            FontDialog fd = new FontDialog();
            fd.Font = new Font(fontName, fontSize, fontStyle, GraphicsUnit.Point, fontGdiCharSet);

            if (fd.ShowDialog() == DialogResult.OK)
            {
                Util.PutSetting("FontName", fd.Font.Name);
                Util.PutSetting("FontSize", fd.Font.Size.ToString());
                Util.PutSetting("FontStyle", fd.Font.Style.ToString());
                Util.PutSetting("FontGdiCharSet", fd.Font.GdiCharSet.ToString());
                Util.MainForm.SetInterFaceFont();
                this.SetInterFaceFont();
            }
        }

        private void useDefaultFontButton_Click(object sender, EventArgs e)
        {
            Util.PutSetting("FontName", "Tahoma");
            Util.PutSetting("FontSize", (8.25F).ToString());
            Util.PutSetting("FontStyle", "Regular");
            Util.PutSetting("FontGdiCharSet", "0");
            Util.MainForm.SetInterFaceFont();
            this.SetInterFaceFont();
        }

        private void SetInterFaceFont()
        {
            this.Font = Util.GetInterfaceFont();
            this.useDefaultFontButton.Enabled = !Util.IsDefaultFont();
        }
    }
}