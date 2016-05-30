using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.Interop;
﻿using Autodesk.AutoCAD.Interop.Common;

namespace QuickPrint.Forms
{
    public partial class PlotConfigUIBase : UserControl
    {
        #region Properties
        private AcadPlotConfiguration m_tempAcPlConfig = null;

        private AcadApplication m_acApp = null;
        public AcadApplication AcApplication
        {
            get { return m_acApp; }
            set 
            {
                m_acApp = value;

                // Create a template plot configuration
                AcadPlotConfigurations acPlConfigs = AcApplication
                                        .ActiveDocument.PlotConfigurations;
                m_tempAcPlConfig = acPlConfigs.Add("ENESY_TEMP");
            }
        }

        [DefaultValue("")]
        public string Plotter { get; private set; }

        [DefaultValue(false)]
        public bool PlotToFile { get; private set; }

        [DefaultValue("")]
        public string PaperSize { get; private set; }

        [DefaultValue("")]
        public string WhatToPlot { get; private set; }

        
        #endregion

        public PlotConfigUIBase()
        {
            InitializeComponent();
            this.HandleDestroyed += PlotConfigUIBase_HandleDestroyed;
        }

        void PlotConfigUIBase_HandleDestroyed(object sender, EventArgs e)
        {
            if (m_tempAcPlConfig != null)
            {
                m_tempAcPlConfig.Delete();
                m_tempAcPlConfig = null;
            }
        }

        public void Init()
        {
            // Init data for controls
            try
            {
                // Init device cause init paper size
                string[] devices = AcApplication.ActiveDocument
                                    .ActiveLayout.GetPlotDeviceNames();
                if (devices.Length > 0)
                {
                    cboPlotter.Items.AddRange(devices);
                    cboPlotter.SelectedIndex = 0;
                }

                // Init What to plot
                string[] whattoplot = new string[3]
                {
                    "Display",
                    "Limits",
                    "Window"
                };
                cboWhatToPlot.Items.AddRange(whattoplot);
                cboWhatToPlot.SelectedIndex = 2;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error: Init Plot Config UI");
            }
        }

        private void RefreshPaperSizeList()
        {
            // Sets device to temp. plotConfig
            m_tempAcPlConfig.ConfigName = cboPlotter.Text;
            string[] papers = m_tempAcPlConfig.GetCanonicalMediaNames();

            // Re-sets for paper size combobox
            cboPaperSize.Items.Clear();
            if (papers.Length > 0)
            {
                cboPaperSize.Items.AddRange(papers);
                cboPaperSize.SelectedIndex = 0;
            }
        }

        private void RefreshSizeLabel()
        {
            // Sets paper size to temp. plotConfig
            m_tempAcPlConfig.CanonicalMediaName = cboPaperSize.Text;

            // Get size
            double length, height;
            m_tempAcPlConfig.GetPaperSize(out length, out height);
            lblPaperSize.Text = Math.Round(length, 2).ToString() + " x "
                                            + Math.Round(height, 2).ToString();
        }

        private void cboPlotter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPaperSizeList();
        }

        private void cboPaperSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSizeLabel();
        }

        private void cboWhatToPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWhatToPlot.SelectedIndex == 2)
            {
                butWindow.Enabled = true;
            }
            else
            {
                butWindow.Enabled = false;
            }
        }

        private void chkCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCenter.Checked)
            {
                txtOffsetX.Enabled = false;
                txtOffsetY.Enabled = false;
            }
            else
            {
                txtOffsetX.Enabled = true;
                txtOffsetY.Enabled = true;
            }
        }

        private void chkFit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFit.Checked)
            {
                cboScale.Enabled = false;
                
            }
            else
            {

            }
        }
    }
}
