using QuickPrint.AutoCAD;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace VisualWget
{


    public partial class SheetDialog : Form
    {
        List<Preset> presets;
        bool savePresets;
        StringCollection urls;
        StringDictionary opts;

        public SheetDialog()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
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



        

        void ShowDir()
        {
            folderBrowserDialog1.Description = "Please select a folder.";

            if (Directory.Exists("??"))
            {
                folderBrowserDialog1.SelectedPath = "??";
            }

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //"??" = folderBrowserDialog1.SelectedPath;
            }
        }

        void ShowOpen()
        {
            //openfiledialog1.filter = "all files (*.*)|*.*";

            //if (file.exists(argumenttextbox.text))
            //{
            //    openfiledialog1.filename = path.getfilename(argumenttextbox.text);
            //    openfiledialog1.initialdirectory = path.getdirectoryname(argumenttextbox.text);
            //}

            //if (openfiledialog1.showdialog(this) == dialogresult.ok)
            //{
            //    argumenttextbox.text = openfiledialog1.filename;
            //}
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "DWG files (*.dwg)|*.DWG|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string shortFileName = Path.GetFileName(openFileDialog1.FileName);
                try
                {
                    string[] layouts = Utils.LayoutsFromDWGFile(fileName).ToArray();
                    foreach (string s in layouts)
                    {
                        ListViewItem li = new ListViewItem(new[] { shortFileName, s, "Available" });
                        this.listDrawings.Items.Add(li);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}