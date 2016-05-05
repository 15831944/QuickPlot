using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace VisualWget
{
    public enum AutoShutdownAction
    {
        None = 0,
        Quit = 1,
        StandBy = 2,
        Hibernate = 3,
        TurnOff = 4,
        Restart = 5,
        LogOff = 6,
        LockComputer = 7
    }

    public partial class AutoShutdownPrompt : Form
    {
        int msecCount;
        AutoShutdownAction action;
        string prompt;

        const int totalSec = 30;

        public AutoShutdownPrompt(AutoShutdownAction action)
        {
            msecCount = 0;
            this.action = action;
            prompt = Util.translationList["000122"];

            if (action == AutoShutdownAction.None)
            {
                return;
            }

            InitializeComponent();

            string s = string.Format(prompt, "{0}", totalSec);

            if (action == AutoShutdownAction.Quit)
            {
                msgLabel.Text = String.Format(s, Util.translationList["000124"]);
            }
            else if (action == AutoShutdownAction.StandBy)
            {
                msgLabel.Text = String.Format(s, Util.translationList["000125"]);
            }
            else if (action == AutoShutdownAction.Hibernate)
            {
                msgLabel.Text = String.Format(s, Util.translationList["000126"]);
            }
            else if (action == AutoShutdownAction.TurnOff)
            {
                msgLabel.Text = String.Format(s, Util.translationList["000127"]);
            }
            else if (action == AutoShutdownAction.Restart)
            {
                msgLabel.Text = String.Format(s, Util.translationList["000128"]);
            }
            else if (action == AutoShutdownAction.LogOff)
            {
                msgLabel.Text = String.Format(s, Util.translationList["000129"]);
            }
            else if (action == AutoShutdownAction.LockComputer)
            {
                msgLabel.Text = String.Format(s, Util.translationList["000130"]);
            }
            else
            {
                Debug.Assert(false);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            msecCount += timer1.Interval;

            if (msecCount >= totalSec * 1000)
            {
                Close();
                Util.DoAutoShutdown(action);
            }
            else
            {
                if (action == AutoShutdownAction.Quit)
                {
                    msgLabel.Text = String.Format(prompt, Util.translationList["000124"], totalSec - msecCount / 1000);
                }
                else if (action == AutoShutdownAction.StandBy)
                {
                    msgLabel.Text = String.Format(prompt, Util.translationList["000125"], totalSec - msecCount / 1000);
                }
                else if (action == AutoShutdownAction.Hibernate)
                {
                    msgLabel.Text = String.Format(prompt, Util.translationList["000126"], totalSec - msecCount / 1000);
                }
                else if (action == AutoShutdownAction.TurnOff)
                {
                    msgLabel.Text = String.Format(prompt, Util.translationList["000127"], totalSec - msecCount / 1000);
                }
                else if (action == AutoShutdownAction.Restart)
                {
                    msgLabel.Text = String.Format(prompt, Util.translationList["000128"], totalSec - msecCount / 1000);
                }
                else if (action == AutoShutdownAction.LogOff)
                {
                    msgLabel.Text = String.Format(prompt, Util.translationList["000129"], totalSec - msecCount / 1000);
                }
                else if (action == AutoShutdownAction.LockComputer)
                {
                    msgLabel.Text = String.Format(prompt, Util.translationList["000130"], totalSec - msecCount / 1000);
                }
                else
                {
                    Debug.Assert(false);
                }

                Size = GetPreferredSize(Size.Empty);
            }
        }

        private void AutoShutdownPrompt_Load(object sender, EventArgs e)
        {
            Translate();
            Size = GetPreferredSize(Size.Empty);
            cancelButton.Left = ClientSize.Width / 2 - cancelButton.Width / 2;
            CenterToScreen();
            Util.SetToolTip(toolTip1, Controls);
            this.SetInterFaceFont();
        }

        void Translate()
        {
            Text = Util.translationList["000121"];
            cancelButton.Text = Util.translationList["000123"];
        }

        private void SetInterFaceFont()
        {
            this.Font = Util.GetInterfaceFont();
        }
    }
}