//﻿ *Enesy.vn
// * First created by Cong
// * Edit: ......
// * Edit: .....
// * Issue: .....
// * ??
// *

using System.Drawing;
using System.IO;
using System.Collections.Generic;
﻿using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;

namespace QuickPrint.AutoCAD
{
    class Sheet
    {
        /// <summary>
        /// Configuration for plotting
        /// </summary>
        private EPlotConfig PConfig;

        /// <summary>
        /// Bottom - left 2D point
        /// </summary>
        private Point MinPoint;

        /// <summary>
        /// Up - right 2D point
        /// </summary>
        private Point MaxPoint;

        /// <summary>
        /// Path of file that contains this sheet
        /// </summary>
        private string Path;

        /// <summary>
        /// Name of layout that contains this sheets
        /// </summary>
        private string Name;

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="pConfig">Plotting configuration</param>
        /// <param name="minPoint">Bottom - left point</param>
        /// <param name="maxPoint">Up - right point</param>
        public Sheet(EPlotConfig pConfig, Point minPoint, Point maxPoint,
                     string sheetPath, string layoutName
            )
        {
            PConfig = pConfig;
            MinPoint = minPoint;
            MaxPoint = maxPoint;
            Path = sheetPath;
            Name = layoutName;
        }
        public Sheet(string sheetPath, string layoutName)
        {
            Path = sheetPath;
            Name = layoutName;
        }

        /// <summary>
        /// Opening sheet on acad application
        /// </summary>
        /// <param name="acApp"></param>
        internal bool Open(AcadApplication acApp)
        {
            try
            {
                AcadDocuments acDocs = acApp.Documents;
                int id = DocIndex(acDocs);
                if (id != -1)
                {
                    // If file is not opened, switch to it
                    acDocs.Item(id).Activate();
                }
                else
                {
                    // If file is not opened, opening it in read-only
                    acDocs.Open(Path, true);
                }

                // Switch to layout
                AcadLayouts acLots = acApp.ActiveDocument.Layouts;
                foreach (AcadLayout acLot in acLots)
                {
                    if (acLot.Name == this.Name)
                    {
                        acApp.ActiveDocument.ActiveLayout = acLot;
                        break;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check whether file of sheet is opened
        /// If file is opened, return index of document (!= -1)
        /// </summary>
        /// /// <param name="acDocs">Acad Documents collection</param>
        private int DocIndex(AcadDocuments acDocs)
        {
            int opFlag = -1;

            for (int i = 0; i < acDocs.Count; i++)
            {
                AcadDocument acDoc = acDocs.Item(i);
                if (acDoc.FullName == Path)
                {
                    opFlag = i;
                    return opFlag;
                }
            }

            return opFlag;
        }

        /// <summary>
        /// Perform plot with current AutoCAD application & current sheet
        /// </summary>
        public void Plot(AcadApplication acApp)
        {
            AcadDocuments acDocs = acApp.Documents;
            int opFlag = DocIndex(acDocs);
            try
            {
                if (Open(acApp))
                {
                    AcadPlot acPlotObject = acApp.ActiveDocument.Plot;

                    // Plot with layout configuration
                    acPlotObject.PlotToDevice();
                }
            }
            catch { }

            // If sheet is not opened initially, closing this after plot finish
            if (opFlag == -1 && DocIndex(acDocs) != -1)
            {
                acDocs.Item(DocIndex(acDocs)).Close(false);
            }
        }

        /// <summary>
        /// Plot with override configuration
        /// </summary>
        /// <param name="acApp">AutoCAD application</param>
        /// <param name="plotConfig">Plot override Configuration</param>
        public void Plot(AcadApplication acApp, int k)
        {
            AcadDocuments acDocs = acApp.Documents;
            int opFlag = DocIndex(acDocs);
            if (Open(acApp))
            {
                try
                {
                    // Init Plot & Configuration
                    AcadPlotConfigurations acPlConfigs;
                    AcadPlotConfiguration PlotConfig;
                    AcadPlot acPlObj;
                    acPlObj = acApp.ActiveDocument.Plot;
                    acPlConfigs = acApp.ActiveDocument.PlotConfigurations;
                    AcadLayout acLot = acApp.ActiveDocument.ActiveLayout;

                    // Add new plotConfig
                    acPlConfigs.Add("PDF", false);

                    //'The plot config you created become active
                    PlotConfig = acPlConfigs.Item("PDF");

                    //Use this method to set the scale
                    PlotConfig.StandardScale = AcPlotScale.acScaleToFit;

                    //Updates the plot
                    PlotConfig.RefreshPlotDeviceInfo();

                    //'Here you specify the pc3 file you want to use
                    PlotConfig.ConfigName = "PDF reDirect v2.pc3";

                    //'You can select the plot style table here
                    PlotConfig.StyleSheet = "acad.ctb";

                    //Specifies whether or not to plot using the plot styles
                    PlotConfig.PlotWithPlotStyles = true;

                    //'Updates the plot
                    PlotConfig.RefreshPlotDeviceInfo();
                    acPlObj.PlotToDevice(PlotConfig);
                    acPlConfigs.Item("PDF").Delete();
                    PlotConfig = null;

                    //List<string> lots = new List<string> { this.Name };
                    //acPlObj.SetLayoutsToPlot(lots);
                    //acLot.StandardScale = AcPlotScale.acScaleToFit;
                    //acLot.RefreshPlotDeviceInfo();
                    //acLot.ConfigName = "PDF reDirect v2.pc3";
                    //acLot.StyleSheet = "acad.ctb";
                    //acLot.PlotWithPlotStyles = true;
                    //acApp.ActiveDocument.Plot.PlotToDevice();
                }
                catch { }
            }

            // If sheet is not opened initially, closing this after plot finish
            if (opFlag == -1 && DocIndex(acDocs) != -1)
            {
                acDocs.Item(DocIndex(acDocs)).Close(false);
            }
        }

        /// <summary>
        /// Previews sheet
        /// </summary>
        /// <param name="acApp"></param>
        public void Preview(AcadApplication acApp)
        {

        }
    }
}
