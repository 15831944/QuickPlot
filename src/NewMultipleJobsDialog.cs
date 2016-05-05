using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VisualWget
{
    public partial class NewMultipleJobsDialog : Form
    {
        List<string> urlsList;

        public NewMultipleJobsDialog()
        {
            InitializeComponent();

            urlsList = new List<string>();
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            urlsList.AddRange(urlsTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            DialogResult = DialogResult.OK;
            Close();
        }

        public string[] UrlsList
        {
            get
            {
                return urlsList.ToArray();
            }
        }

        private void NewMultipleJobsDialog_Load(object sender, EventArgs e)
        {
            Translate();
            noticeLabel.SendToBack();
            Util.SetToolTip(toolTip1, Controls);
            this.SetInterFaceFont();
        }

        void Translate()
        {
            Text = Util.translationList["000066"];
            urlsLabel.Text = Util.translationList["000067"];
            noticeLabel.Text = Util.translationList["000068"];
            okButton.Text = Util.translationList["000069"];
            cancelButton.Text = Util.translationList["000070"];
        }

        private void SetInterFaceFont()
        {
            this.Font = Util.GetInterfaceFont();
        }
    }
}
