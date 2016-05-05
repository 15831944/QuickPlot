using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VisualWget
{
    struct Preset
    {
        public string name;
        public string value;

        public Preset(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }

    public enum JobOptionCategory
    {
        General = 0,
        Http,
        Advanced,
    }

    public partial class JobDialog : Form
    {
        List<Preset> presets;
        bool savePresets;
        StringCollection urls;
        StringDictionary opts;

        public JobDialog(StringCollection urls, StringDictionary opts)
        {
            InitializeComponent();

            presets = new List<Preset>();
            savePresets = false;
            this.urls = urls;
            this.opts = opts;
        }

        public string GetUrlsString()
        {
            string s = "";

            for (int i = 0; i < urls.Count; i++)
                s += " " + Util.AddQuotes(urls[i]);

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

        public bool StartOnOk
        {
            get
            {
                return startOnOkCheckBox.Checked;
            }
        }

        public bool ShowStartOnOk
        {
            set
            {
                startOnOkCheckBox.Visible = value;
            }
        }

        public bool ShowOutputDocument
        {
            set
            {
                outputDocumentLabel.Visible = value;
                outputDocumentTextBox.Visible = value;
                outputDocumentButton.Visible = value;
            }
        }

        public bool ShowNote
        {
            set
            {
                noteLabel.Visible = value;
                noteTextBox.Visible = value;
            }
        }

        public bool EnableUrls
        {
            set
            {
                urlsLabel.Enabled = value;
                urlsTextBox.Enabled = value;
            }
        }

        public bool ShowReferer
        {
            set
            {
                refererLabel.Visible = value;
                refererTextBox.Visible = value;
            }
        }

        public string UrlsLabelText
        {
            set
            {
                urlsLabel.Text = value;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            urls.Clear();
            urls.AddRange(Util.CmdLineArgsToArgs(urlsTextBox.Text.Trim()));

            string s = directoryPrefixComboBox.Text;

            Util.PutSetting("DirPrefix0", s);

            int i = directoryPrefixComboBox.FindStringExact(s);

            if (i == -1)
            {
                i = directoryPrefixComboBox.Items.Count;

                if (i == directoryPrefixComboBox.MaxDropDownItems)
                {
                    i--;
                }
            }

            for (int j = 0; j < i; j++)
            {
                Util.PutSetting("DirPrefix" + (j + 1).ToString(), directoryPrefixComboBox.Items[j].ToString());
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        void LoadPresets()
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

                lines = File.ReadAllLines(Path.Combine(dir, "OptionPresets.cfg"));
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

            foreach (string line in lines)
            {
                string[] args = Util.CmdLineArgsToArgs(line);

                if (args.Length > 1)
                {
                    presets.Add(new Preset(args[0], args[1]));
                }
            }
        }

        private void JobDialog_Load(object sender, EventArgs e)
        {
            this.jobCatListBox.SelectedIndex = 0;

            string[] userAgentLines = File.ReadAllLines(Path.Combine(Util.AppDir, "UserAgents.cfg"));

            for (int i = 0; i < userAgentLines.Length; i++)
            {
                string userAgentLine = userAgentLines[i];

                if (userAgentLine.Contains("="))
                {
                    string[] userAgentParts = userAgentLine.Split('=');
                    string userAgentName = userAgentParts[0].Trim();
                    string userAgentValue = userAgentParts[1].Trim();
                    UserAgentItem userAgentItem = new UserAgentItem(userAgentName, userAgentValue);

                    this.userAgentComboBox.Items.Add(userAgentItem);
                }
            }

            SyncHttpOpts();

            urlsTextBox.Text = GetUrlsString();

            if (Text == "New Job" && urlsTextBox.Text == "")
            {
                string clipboardText = Util.GetTextFromClipboard();

                if (Util.IsDownloadable(clipboardText))
                {
                    urlsTextBox.Text = "\"" + clipboardText + "\"";
                }
            }

            SyncGeneralOpts();
            autoStartNumDaysNumericUpDown.Enabled = autoStartCheckBox.Checked;
            autoStartNumHoursNumericUpDown.Enabled = (autoStartCheckBox.Checked && (autoStartNumDaysNumericUpDown.Value == 0));
            LoadPresets();
            UpdateOptionsPresetComboBox();
            optionsCategoryListBox.Items.AddRange(Util.GetArrayOfOptCats());
            optionsCategoryListBox.SelectedIndex = 0;
            startOnOkCheckBox.Checked = bool.Parse(Util.GetSetting("JobDialogStartOnOk"));

            for (int i = 0; i < directoryPrefixComboBox.MaxDropDownItems; i++)
            {
                string dirPrefix = Util.GetSetting("DirPrefix" + i.ToString());

                if (dirPrefix == "")
                {
                    break;
                }

                if (directoryPrefixComboBox.FindStringExact(dirPrefix) == -1)
                {
                    directoryPrefixComboBox.Items.Add(dirPrefix);
                }
            }

            Translate();
            autoStartCheckBox.SendToBack();
            startOnOkCheckBox.SendToBack();
            Util.SetToolTip(toolTip1, Controls);
            this.SetInterFaceFont();
        }

        void Translate()
        {
            if (Text == "New Job")
            {
                Text = Util.translationList["000037"];
                urlsLabel.Text = Util.translationList["000041"];
            }
            else if (Text == "Edit Job")
            {
                Text = Util.translationList["000038"];
                urlsLabel.Text = Util.translationList["000041"];
            }
            else if (Text == "Default Download Properties")
            {
                Text = Util.translationList["000039"];
                urlsLabel.Text = Util.translationList["000041"];
            }

            jobCatListBox.Items[(int)JobOptionCategory.General] = Util.translationList["000040"];
            directoryPrefixLabel.Text = Util.translationList["000043"];
            resumingGroupBox.Text = Util.translationList["000247"];
            continueCheckBox.Text = Util.translationList["000044"];
            timestampingCheckBox.Text = Util.translationList["000045"];
            //recursiveCheckBox.Text = Util.translationList["000046"];
            //noClobberCheckBox.Text = Util.translationList["000047"];
            schedulerGroupBox.Text = Util.translationList["000048"];
            autoStartCheckBox.Text = Util.translationList["000049"];
            autoStartDaysLabel.Text = Util.translationList["000050"];
            autoStartHoursLabel.Text = Util.translationList["000208"];
            outputDocumentLabel.Text = Util.translationList["000051"];
            noteLabel.Text = Util.translationList["000210"];
            jobCatListBox.Items[(int)JobOptionCategory.Advanced] = Util.translationList["000052"];
            presetsGroupBox.Text = Util.translationList["000053"];
            //loadButton.Text = Util.translationList["000054"];
            //saveButton.Text = Util.translationList["000055"];
            loadEmptyButton.Text = Util.translationList["000209"];
            saveAsButton.Text = Util.translationList["000056"];
            deleteButton.Text = Util.translationList["000057"];
            //resetLinkLabel.Text = Util.translationList["000058"];
            //resetLinkLabel.Location = new Point(panel4.Width - resetLinkLabel.Width - 3, panel4.Height - resetLinkLabel.Height - 3);
            startOnOkCheckBox.Text = Util.translationList["000059"];
            okButton.Text = Util.translationList["000060"];
            cancelButton.Text = Util.translationList["000061"];
            jobCatListBox.Items[(int)JobOptionCategory.Http] = Util.translationList["000214"];
            userAgentLabel.Text = Util.translationList["000215"];
            refererLabel.Text = Util.translationList["000216"];
        }

        private void JobDialog_Shown(object sender, EventArgs e)
        {
            urlsTextBox.Focus();
            urlsTextBox.SelectAll();
        }

        private void UpdatePreset(Preset p)
        {
            for (int i = 0; i < presets.Count; i++)
            {
                if (string.Compare(presets[i].name, p.name, true) == 0)
                {
                    presets[i] = p;

                    break;
                }
            }
        }

        /*private void saveButton_Click(object sender, EventArgs e)
        {
            Debug.Assert(optionsPresetComboBox.SelectedItem != null);

            Preset p;

            p.name = optionsPresetComboBox.SelectedItem.ToString();
            p.value = GetOptsString();

            int i = SearchPresetsValue(p.value);

            if (i != -1)
            {
                if (Util.MsgBox(string.Format(Util.translationList["000179"], presets[i].name),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.No)
                {
                    return;
                }

                presets.RemoveAt(i);
            }

            UpdatePreset(p);
            UpdateOptionsPresetComboBox();
            savePresets = true;
        }*/

        private void DeletePreset(string presetName)
        {
            for (int i = 0; i < presets.Count; i++)
            {
                if (string.Compare(presets[i].name, presetName, true) == 0)
                {
                    presets.RemoveAt(i);

                    break;
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Debug.Assert(optionsPresetComboBox.SelectedItem != null);

            DeletePreset(optionsPresetComboBox.SelectedItem.ToString());
            UpdateOptionsPresetComboBox();
            savePresets = true;
        }

        void SavePresets()
        {
            List<string> lines = new List<string>();

            foreach (Preset p in presets)
            {
                lines.Add(Util.AddQuotes(p.name) + " " + Util.AddQuotes(p.value));
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
                File.WriteAllLines(Path.Combine(dir, "OptionPresets.cfg"), lines.ToArray());
            }
            catch (Exception ex)
            {
                Util.MsgBox(string.Format(Util.translationList["000182"], ex.Message), MessageBoxIcon.Error);
            }
        }

        void UpdateOptionsPresetComboBox()
        {
            optionsPresetComboBox.Items.Clear();

            foreach (Preset p in presets)
            {
                optionsPresetComboBox.Items.Add(p.name);
            }

            int i = SearchPresetsValue(GetOptsString());

            if (i != -1)
            {
                string s = presets[i].name;

                Debug.Assert(s != "");

                int j = optionsPresetComboBox.FindStringExact(s);

                if (j != -1)
                {
                    optionsPresetComboBox.SelectedIndexChanged -= new EventHandler(optionsPresetComboBox_SelectedIndexChanged);
                    optionsPresetComboBox.SelectedIndex = j;
                    optionsPresetComboBox.SelectedIndexChanged += new EventHandler(optionsPresetComboBox_SelectedIndexChanged);
                }
            }

            EnableDisableButtons();
        }

        void SetChecked()
        {
            int i = optionsCategoryListBox.SelectedIndex;

            for (int j = 0; j < optionsListView.Items.Count; j++)
            {
                int index = Util.GetOptIndex(i, j);

                if (index == -1)
                {
                    Debug.Assert(false, string.Format("Opt not found, i={0}, j={1}.", i, j));

                    continue;
                }

                optionsListView.ItemCheck -= new ItemCheckEventHandler(optionsListView_ItemCheck);
                optionsListView.Items[j].Checked = opts.ContainsKey(Util.GetOptByIndex(index).name);
                optionsListView.ItemCheck += new ItemCheckEventHandler(optionsListView_ItemCheck);
            }
        }

        void SetText()
        {
            int i = optionsCategoryListBox.SelectedIndex;

            if (optionsListView.SelectedIndices.Count == 0)
            {
                return;
            }

            int j = optionsListView.SelectedIndices[0];
            int index = Util.GetOptIndex(i, j);

            if (index == -1)
            {
                Debug.Assert(false, string.Format("Opt not found, i={0}, j={1}.", i, j));

                return;
            }

            if (Util.GetOptByIndex(index).needArg)
            {
                argumentTextBox.TextChanged -= new EventHandler(argumentTextBox_TextChanged);

                if (opts.ContainsKey(Util.GetOptByIndex(index).name))
                {
                    argumentTextBox.Text = opts[Util.GetOptByIndex(index).name];
                }
                else
                {
                    argumentTextBox.Text = "";
                }

                argumentTextBox.TextChanged += new EventHandler(argumentTextBox_TextChanged);
            }
        }

        void UpdateOpts(string optsString)
        {
            StringCollection tmpUrls;
            StringDictionary tmpOpts;

            Util.GetOpt(optsString, out tmpUrls, out tmpOpts);
            opts.Clear();

            foreach (string optName in tmpOpts.Keys)
            {
                opts.Add(optName, tmpOpts[optName]);
            }
        }

        void HideArgumentControls()
        {
            argumentLabel.Hide();
            argumentTextBox.Hide();
            argumentButton.Hide();
        }

        private void optionsCategoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            optionsListView.Items.Clear();

            foreach (string s in Util.GetArrayOfOpts(optionsCategoryListBox.SelectedIndex))
            {
                optionsListView.Items.Add(s);
            }

            optionsListView.SelectedIndices.Clear();
            HideArgumentControls();
            descriptionLabel.Text = "";
            SetChecked();
        }

        private void argumentTextBox_TextChanged(object sender, EventArgs e)
        {
            int i = optionsCategoryListBox.SelectedIndex;

            if (optionsListView.SelectedIndices.Count == 0)
            {
                return;
            }

            int j = optionsListView.SelectedIndices[0];
            int index = Util.GetOptIndex(i, j);

            if (index == -1)
            {
                Debug.Assert(false, string.Format("Opt not found, i={0}, j={1}.", i, j));

                return;
            }

            Debug.Assert(Util.GetOptByIndex(index).needArg);

            if (!optionsListView.CheckedIndices.Contains(j))
            {
                optionsListView.Items[j].Checked = true;
            }

            if (!opts.ContainsKey(Util.GetOptByIndex(index).name))
            {
                opts.Add(Util.GetOptByIndex(index).name, argumentTextBox.Text);
            }
            else
            {
                opts[Util.GetOptByIndex(index).name] = argumentTextBox.Text;
            }

            EnableDisableButtons();
        }

        void ShowDir()
        {
            folderBrowserDialog1.Description = "Please select a folder.";

            if (Directory.Exists(argumentTextBox.Text))
            {
                folderBrowserDialog1.SelectedPath = argumentTextBox.Text;
            }

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                argumentTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        void ShowOpen()
        {
            openFileDialog1.Filter = "All Files (*.*)|*.*";

            if (File.Exists(argumentTextBox.Text))
            {
                openFileDialog1.FileName = Path.GetFileName(argumentTextBox.Text);
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(argumentTextBox.Text);
            }

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                argumentTextBox.Text = openFileDialog1.FileName;
            }
        }

        void ShowSave()
        {
            saveFileDialog1.Filter = "All Files (*.*)|*.*";

            if (File.Exists(argumentTextBox.Text))
            {
                saveFileDialog1.FileName = Path.GetFileName(argumentTextBox.Text);
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(argumentTextBox.Text);
            }

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                argumentTextBox.Text = saveFileDialog1.FileName;
            }
        }

        private void argumentButton_Click(object sender, EventArgs e)
        {
            int i = optionsCategoryListBox.SelectedIndex;

            if (optionsListView.SelectedIndices.Count == 0)
            {
                return;
            }

            int j = optionsListView.SelectedIndices[0];
            int index = Util.GetOptIndex(i, j);

            if (index == -1)
            {
                Debug.Assert(false, string.Format("Opt not found, i={0}, j={1}.", i, j));

                return;
            }

            Debug.Assert(Util.GetOptByIndex(index).needArg);

            string action = Util.GetOptByIndex(index).action;

            if (action == "ShowDir")
            {
                ShowDir();
            }
            else if (action == "ShowOpen")
            {
                ShowOpen();
            }
            else if (action == "ShowSave")
            {
                ShowSave();
            }
        }

        private void optionsListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i = optionsCategoryListBox.SelectedIndex;
            int j = e.Index;
            int index = Util.GetOptIndex(i, j);

            if (index == -1)
            {
                Debug.Assert(false, string.Format("Opt not found, i={0}, j={1}.", i, j));

                return;
            }

            if (optionsListView.CheckedIndices.Contains(j))
            {
                if (opts.ContainsKey(Util.GetOptByIndex(index).name))
                {
                    opts.Remove(Util.GetOptByIndex(index).name);
                    SetText();
                    EnableDisableButtons();
                }
            }
            else
            {
                if (!opts.ContainsKey(Util.GetOptByIndex(index).name))
                {
                    string optName = Util.GetOptByIndex(index).name;
                    string optArg = null;

                    if (Util.GetOptByIndex(index).needArg)
                    {
                        optArg = "";
                    }

                    opts.Add(optName, optArg);
                    EnableDisableButtons();
                }
            }
        }

        private void optionsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = optionsCategoryListBox.SelectedIndex;

            if (optionsListView.SelectedIndices.Count == 0)
            {
                HideArgumentControls();
                descriptionLabel.Text = "";

                return;
            }

            int j = optionsListView.SelectedIndices[0];
            int index = Util.GetOptIndex(i, j);

            if (index == -1)
            {
                Debug.Assert(false, string.Format("Opt not found, i={0}, j={1}.", i, j));

                return;
            }

            descriptionLabel.Text = Util.GetOptByIndex(index).description;
            HideArgumentControls();

            if (Util.GetOptByIndex(index).needArg)
            {
                argumentLabel.Text = Util.GetOptByIndex(index).argumentLabel + ":";
                argumentLabel.Show();
                argumentTextBox.Show();

                string action = Util.GetOptByIndex(index).action;

                if (action == "ShowDir" || action == "ShowOpen" || action == "ShowSave")
                {
                    argumentButton.Show();
                }
            }

            SetText();
        }

        private void startOnOkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Util.PutSetting("JobDialogStartOnOk", startOnOkCheckBox.Checked.ToString());
        }

        private string GetPresetValue(string presetName)
        {
            int i = SearchPresets(presetName);

            return (i != -1) ? presets[i].value : null;
        }

        void SyncGeneralOpts()
        {
            directoryPrefixComboBox.TextChanged -= new EventHandler(directoryPrefixComboBox_TextChanged);
            directoryPrefixComboBox.Text = opts.ContainsKey("directory-prefix") ? opts["directory-prefix"] : "";
            directoryPrefixComboBox.TextChanged += new EventHandler(directoryPrefixComboBox_TextChanged);
            continueCheckBox.CheckedChanged -= new EventHandler(continueCheckBox_CheckedChanged);
            continueCheckBox.Checked = opts.ContainsKey("continue");
            continueCheckBox.CheckedChanged += new EventHandler(continueCheckBox_CheckedChanged);
            timestampingCheckBox.CheckedChanged -= new EventHandler(timestampingCheckBox_CheckedChanged);
            timestampingCheckBox.Checked = opts.ContainsKey("timestamping");
            timestampingCheckBox.CheckedChanged += new EventHandler(timestampingCheckBox_CheckedChanged);
            outputDocumentTextBox.TextChanged -= new EventHandler(outputDocumentTextBox_TextChanged);
            outputDocumentTextBox.Text = opts.ContainsKey("output-document") ? opts["output-document"] : "";
            outputDocumentTextBox.TextChanged += new EventHandler(outputDocumentTextBox_TextChanged);
        }

        void EnableDisableButtons()
        {
            if (optionsPresetComboBox.SelectedItem == null)
            {
                deleteButton.Enabled = false;
            }
            else
            {
                string presetValue = GetPresetValue(optionsPresetComboBox.SelectedItem.ToString());

                Debug.Assert(presetValue != null);

                deleteButton.Enabled = true;
            }
        }

        private void optionsPresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Assert(optionsPresetComboBox.SelectedItem != null);

            string presetValue = GetPresetValue(optionsPresetComboBox.SelectedItem.ToString());

            Debug.Assert(presetValue != null);
            UpdateOpts(presetValue);
            SetChecked();
            SetText();
            EnableDisableButtons();
        }

        private int SearchPresets(string presetName)
        {
            for (int i = 0; i < presets.Count; i++)
            {
                if (string.Compare(presets[i].name, presetName, true) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        private int SearchPresetsValue(string presetValue)
        {
            for (int i = 0; i < presets.Count; i++)
            {
                if (string.Compare(presets[i].value, presetValue, false) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            string presetValue = GetOptsString();
            int i = SearchPresetsValue(presetValue);

            if (i != -1)
            {
                if (Util.MsgBox(string.Format(Util.translationList["000179"], presets[i].name),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.No)
                {
                    return;
                }
            }

            SavePresetInfo savePresetInfo = new SavePresetInfo("", optionsPresetComboBox);
            SavePresetDialog savePresetDialog = new SavePresetDialog(savePresetInfo);

            if (savePresetDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (i != -1)
                {
                    presets.RemoveAt(i);
                }

                i = SearchPresets(savePresetDialog.PresetName);

                if (i != -1)
                {
                    presets.RemoveAt(i);
                }

                Preset p;

                p.name = savePresetDialog.PresetName;
                p.value = presetValue;
                presets.Add(p);
                UpdateOptionsPresetComboBox();
                savePresets = true;
            }

            savePresetDialog.Dispose();
        }

        private void directoryPrefixButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Please select a folder.";

            if (Directory.Exists(directoryPrefixComboBox.Text))
            {
                folderBrowserDialog1.SelectedPath = directoryPrefixComboBox.Text;
            }

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                directoryPrefixComboBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void continueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!continueCheckBox.Checked)
            {
                if (opts.ContainsKey("continue"))
                {
                    opts.Remove("continue");
                }
            }
            else
            {
                if (!opts.ContainsKey("continue"))
                {
                    opts.Add("continue", null);
                }
            }
        }

        private void timestampingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!timestampingCheckBox.Checked)
            {
                if (opts.ContainsKey("timestamping"))
                {
                    opts.Remove("timestamping");
                }
            }
            else
            {
                if (!opts.ContainsKey("timestamping"))
                {
                    opts.Add("timestamping", null);
                }
            }
        }

        private void resetLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateOpts("");
            SetChecked();
            SetText();
            EnableDisableButtons();
        }

        private void directoryPrefixComboBox_TextChanged(object sender, EventArgs e)
        {
            if (directoryPrefixComboBox.Text == "")
            {
                opts.Remove("directory-prefix");
            }
            else
            {
                opts["directory-prefix"] = directoryPrefixComboBox.Text;
            }
        }

        private void JobDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savePresets)
            {
                SavePresets();
            }
        }

        private void outputDocumentTextBox_TextChanged(object sender, EventArgs e)
        {
            if (outputDocumentTextBox.Text == "")
            {
                opts.Remove("output-document");
            }
            else
            {
                opts["output-document"] = outputDocumentTextBox.Text;
            }
        }

        private void outputDocumentButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "All Files (*.*)|*.*";

            if (File.Exists(outputDocumentTextBox.Text))
            {
                saveFileDialog1.FileName = Path.GetFileName(outputDocumentTextBox.Text);
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(outputDocumentTextBox.Text);
            }

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                outputDocumentTextBox.Text = saveFileDialog1.FileName;
            }
        }

        private void scheduleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            autoStartNumDaysNumericUpDown.Enabled = autoStartCheckBox.Checked;
            autoStartNumHoursNumericUpDown.Enabled = (autoStartCheckBox.Checked && (autoStartNumDaysNumericUpDown.Value == 0));
        }

        public int AutoStartNumDays
        {
            get
            {
                if (autoStartCheckBox.Checked)
                {
                    return Convert.ToInt32(autoStartNumDaysNumericUpDown.Value);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value >= 1 && value <= 365)
                {
                    // Please set AutoStartNumHours after AutoStartNumDays to enable the checkbox
                    //autoStartCheckBox.Checked = true;
                    autoStartNumDaysNumericUpDown.Value = Convert.ToDecimal(value);
                }
                else
                {
                    // Please set AutoStartNumHours after AutoStartNumDays to disble the checkbox
                    //autoStartCheckBox.Checked = false;
                }
            }
        }

        public int AutoStartNumHours
        {
            get
            {
                if (autoStartCheckBox.Checked)
                {
                    return Convert.ToInt32(autoStartNumHoursNumericUpDown.Value);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value >= 1 && value <= 168)
                {
                    autoStartCheckBox.Checked = true;
                    autoStartNumHoursNumericUpDown.Value = Convert.ToDecimal(value);
                }
                else
                {
                    int daysValue = Convert.ToInt32(autoStartNumDaysNumericUpDown.Value);

                    if (daysValue >= 1 && daysValue <= 365)
                    {
                        autoStartCheckBox.Checked = true;
                    }
                    else
                    {
                        autoStartCheckBox.Checked = false;
                    }
                }
            }
        }

        public string NoteText
        {
            get
            {
                return noteTextBox.Text;
            }
            set
            {
                noteTextBox.Text = value;
            }
        }

        private void autoStartNumDaysNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            autoStartNumHoursNumericUpDown.Enabled = (autoStartNumDaysNumericUpDown.Value == 0);
        }

        private void SetInterFaceFont()
        {
            this.Font = Util.GetInterfaceFont();
        }

        private void jobCatListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.jobCatListBox.SelectedIndex == (int)JobOptionCategory.General)
            {
                DisableAllPanels();
                this.generalPanel.Enabled = true;
                this.generalPanel.BringToFront();
                SyncGeneralOpts();

            }
            else if (this.jobCatListBox.SelectedIndex == (int)JobOptionCategory.Http)
            {
                DisableAllPanels();
                this.httpPanel.Enabled = true;
                this.httpPanel.BringToFront();
                SyncHttpOpts();

            }
            else if (this.jobCatListBox.SelectedIndex == (int)JobOptionCategory.Advanced)
            {
                DisableAllPanels();
                this.advancedPanel.Enabled = true;
                this.advancedPanel.BringToFront();
                EnableDisableButtons();
                SetChecked();
                SetText();
            }
        }

        private void SyncHttpOpts()
        {
            userAgentComboBox.SelectedIndexChanged -= new EventHandler(userAgentComboBox_SelectedIndexChanged);
            int i;
            for (i = 0; i < userAgentComboBox.Items.Count; i++)
            {
                UserAgentItem userAgentItem = (UserAgentItem)userAgentComboBox.Items[i];

                if (opts.ContainsKey("user-agent") && opts["user-agent"] == userAgentItem.value)
                {
                    userAgentComboBox.SelectedItem = userAgentItem;
                    break;
                }
            }
            if (i == userAgentComboBox.Items.Count)
            {
                userAgentComboBox.SelectedIndex = -1;
            }
            userAgentComboBox.SelectedIndexChanged += new EventHandler(userAgentComboBox_SelectedIndexChanged);

            refererTextBox.TextChanged -= new EventHandler(refererTextBox_TextChanged);
            refererTextBox.Text = opts.ContainsKey("referer") ? opts["referer"] : "";
            refererTextBox.TextChanged += new EventHandler(refererTextBox_TextChanged);
        }

        private void DisableAllPanels()
        {
            this.generalPanel.Enabled = false;
            this.httpPanel.Enabled = false;
            this.advancedPanel.Enabled = false;
        }

        private void loadEmptyButton_Click(object sender, EventArgs e)
        {
            UpdateOpts("");
            SetChecked();
            SetText();
            EnableDisableButtons();
        }

        private void outputDocumentTextBox_Leave(object sender, EventArgs e)
        {
            string directoryPrefixText = directoryPrefixComboBox.Text.Trim();
            string outputDocumentText = outputDocumentTextBox.Text.Trim();

            if (directoryPrefixText.Length > 0 && outputDocumentText.Length > 0)
            {
                if (Path.GetDirectoryName(outputDocumentText).Length == 0)
                {
                    outputDocumentTextBox.Text = Path.Combine(directoryPrefixText, outputDocumentText);
                }
            }
        }

        private void userAgentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userAgentComboBox.SelectedIndex == -1)
            {
                opts.Remove("user-agent");
            }
            else
            {
                opts["user-agent"] = ((UserAgentItem)userAgentComboBox.SelectedItem).value;
            }
        }

        private void refererTextBox_TextChanged(object sender, EventArgs e)
        {
            if (refererTextBox.Text == "")
            {
                opts.Remove("referer");
            }
            else
            {
                opts["referer"] = refererTextBox.Text;
            }
        }
    }
}