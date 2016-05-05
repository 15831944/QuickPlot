using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VisualWget
{
    public partial class SelectFolderDialog : Form
    {
        SelectFolderInfo sfi;

        public SelectFolderDialog(SelectFolderInfo sfi)
        {
            InitializeComponent();

            this.sfi = sfi;
        }

        private void SelectFolderDialog_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < selectFolderComboBox.MaxDropDownItems; i++)
            {
                string dirPrefix = Util.GetSetting("DirPrefix" + i.ToString());

                if (dirPrefix == "")
                {
                    break;
                }

                if (selectFolderComboBox.FindStringExact(dirPrefix) == -1)
                {
                    selectFolderComboBox.Items.Add(dirPrefix);
                }
            }

            Translate();
            this.SetInterFaceFont();
        }

        private void SetInterFaceFont()
        {
            this.Font = Util.GetInterfaceFont();
        }

        void Translate()
        {
            this.Text = this.sfi.title;
            this.selectFolderLabel.Text = this.sfi.label;

            okButton.Text = Util.translationList["000219"];
            cancelButton.Text = Util.translationList["000220"];
        }

        public string FolderPath
        {
            get
            {
                return sfi.folderPath;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string folderPath = selectFolderComboBox.Text.Trim();

            if (!Directory.Exists(folderPath))
            {
                Util.MsgBox(Util.translationList["000223"], MessageBoxIcon.Error);
                return;
            }

            sfi.folderPath = folderPath;

            string s = selectFolderComboBox.Text;

            Util.PutSetting("DirPrefix0", s);

            int i = selectFolderComboBox.FindStringExact(s);

            if (i == -1)
            {
                i = selectFolderComboBox.Items.Count;

                if (i == selectFolderComboBox.MaxDropDownItems)
                {
                    i--;
                }
            }

            for (int j = 0; j < i; j++)
            {
                Util.PutSetting("DirPrefix" + (j + 1).ToString(), selectFolderComboBox.Items[j].ToString());
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Please select a folder.";

            if (Directory.Exists(selectFolderComboBox.Text))
            {
                folderBrowserDialog1.SelectedPath = selectFolderComboBox.Text;
            }

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                selectFolderComboBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }

    public struct SelectFolderInfo
    {
        public string folderPath;
        public string title;
        public string label;

        public SelectFolderInfo(string folderPath, string title, string label)
        {
            this.folderPath = folderPath;
            this.title = title;
            this.label = label;
        }
    }
}
