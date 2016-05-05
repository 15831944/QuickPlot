using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CADAutomation;
using Autodesk.AutoCAD;

namespace CADAutomation
{
    public partial class frmCADAutomation : Form
    {
        public frmCADAutomation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double _startX = 100.0;
            double _startY = 100.0;

            //Create New AutoCAD Instance & Closes Already Opened Instance
            PF.CreateAutoCADObject();

            PF.DrawLine((_startX) - 20.0, _startY, (_startX + 120), _startY, "HIDDEN");
            PF.DrawSolid((_startX) - 10.0, _startY + (60.0), 120.0, 2.0);
            PF.DrawLine((_startX) - 10.0, (_startY + (60.0)) - 1.0, (_startX) - 15.0, (_startY + (60.0)) - 1.0);
            PF.DrawLine((_startX) - 15.0, (_startY + (60.0)) - 1.0, (_startX) - 15.0, _startY);
            PF.DrawTerminationPoint(_startX - 15, _startY);

            PF.gbl_doc.ActiveLayer = PF.TerminationPoints;
            PF.DrawText((_startX - 15) + 1.0, (_startY - 15.0), "Point 0", 2.0, PF.gbl_pi / 2);
            PF.DrawText(((_startX) - 10.0) + 50.0, (_startY + (60.0)) + 2.0, "SWITCH 110", 3.0, 0);
            PF.gbl_doc.ActiveLayer = PF.SwitchLayer;

            for (int i = 0; i <= 10; i++)
            {
                PF.DrawLine(_startX + (10 * i), _startY, _startX + (10 * i), (_startY + 50.0), "", false, true);
                PF.DrawTerminationPoint(_startX + (10 * i), _startY);

                PF.gbl_doc.ActiveLayer = PF.TerminationPoints;
                PF.DrawText((_startX + (10 * i)) + 1.0, (_startY - 15.0), "Point " + (i + 1), 2.0, PF.gbl_pi / 2);
                PF.gbl_doc.ActiveLayer = PF.SwitchLayer;
            }
            PF.gbl_doc.Plot.DisplayPlotPreview(Autodesk.AutoCAD.Interop.Common.AcPreviewMode.acPartialPreview);
        }
    }
}