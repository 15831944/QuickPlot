using System;
using System.Windows.Forms;

namespace VisualWget
{
    public partial class SavePresetDialog : Form
    {
        SavePresetInfo spi;

        public SavePresetDialog(SavePresetInfo spi)
        {
            InitializeComponent();

            this.spi = spi;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                Util.MsgBox(Util.translationList["000180"], MessageBoxIcon.Error);
                textBox1.Focus();

                return;
            }

            int i = spi.cb.FindStringExact(textBox1.Text);

            if (i != -1)
            {
                if (Util.MsgBox(string.Format(Util.translationList["000181"], textBox1.Text),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.No)
                {
                    return;
                }
            }

            spi.name = textBox1.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void SavePresetDialog_Load(object sender, EventArgs e)
        {
            Translate();
            textBox1.Text = spi.name;
            Util.SetToolTip(toolTip1, Controls);
            this.SetInterFaceFont();
        }

        void Translate()
        {
            Text = Util.translationList["000062"];
            label1.Text = Util.translationList["000063"];
            okButton.Text = Util.translationList["000064"];
            cancelButton.Text = Util.translationList["000065"];
        }

        public string PresetName
        {
            get
            {
                return spi.name;
            }
        }

        private void SetInterFaceFont()
        {
            this.Font = Util.GetInterfaceFont();
        }
    }

    public struct SavePresetInfo
    {
        public string name;
        public ComboBox cb;

        public SavePresetInfo(string name, ComboBox cb)
        {
            this.name = name;
            this.cb = cb;
        }
    }
}