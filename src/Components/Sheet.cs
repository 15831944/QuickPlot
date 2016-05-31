// -------------------------------------------------------------------------------------
// COPYRIGHT (c) 2016 ENESY.VN
// THIS CLASS IS USED AS INSTANCE OF A SHEET (A PRINT PAGE)
// -------------------------------------------------------------------------------------
// 2016-05-25: Adding basic Properties & Methods
//             - Properties: Window (Min & Max Point)
//                           Full Name (Path) of .dwg file that contains sheet
//                           Name of layout that contains sheet
//             - Methods: Open and Close (if necessary) sheet
//                        Plot with and without override plot configuration
// -------------------------------------------------------------------------------------

using System.IO;
using System.Collections.Generic;
﻿using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using QuickPrint.Model;
using System.ComponentModel;
using QuickPrint.AutoCAD;

namespace QuickPrint.Components
{
    class Sheet
    {
        /// <summary>
        /// Bottom - left 2D point
        /// </summary>
        private EPoint2D MinPoint = new EPoint2D(0, 0);

        /// <summary>
        /// Up - right 2D point
        /// </summary>
        private EPoint2D MaxPoint = new EPoint2D(0, 0);

        /// <summary>
        /// Path of file that contains this sheet
        /// </summary>
        private string Path = "";

        /// <summary>
        /// Name of layout that contains this sheets
        /// </summary>
        public string Name = "";

        /// <summary>
        /// Check if this sheet is ready for plot
        /// </summary>
        [DefaultValue(false)]
        public bool IsReady
        {
            get
            {
                if (Path != "" && Name != "" && MinPoint != MaxPoint)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Title of new sheetSet (subSet)
        /// </summary>
        [DefaultValue("New Set")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Handle of sheet. Default value = (1,0,1) as First sheet
        /// </summary>
        private EHandle m_handle = new EHandle(1, 0, 1);
        public EHandle Handle
        {
            get { return m_handle; }
            set { m_handle = value; }
        }

        /// <summary>
        /// Constructor 1
        /// </summary>
        public Sheet()
        {
        }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="pConfig">Plotting configuration</param>
        /// <param name="minPoint">Bottom - left point</param>
        /// <param name="maxPoint">Up - right point</param>
        public Sheet(EPoint2D minPoint, EPoint2D maxPoint,
                     string sheetPath, string layoutName
            )
        {
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
        /// Gets window plotted from active document and sets to this
        /// </summary>
        public void Set(AcadApplication acApp)
        {
            // Set focus to acad application
            AcadApp.SetFocus(acApp.HWND);

            try
            {
                // Set window to plot
                AcadUtility acUtil = acApp.ActiveDocument.Utility;
                double[] p1, p2;
                p1 = acUtil.GetPoint(System.Type.Missing, "\nSpecify plot window: ") as double[];
                p2 = acUtil.GetCorner(p1, System.Type.Missing) as double[];
                MinPoint = new EPoint2D(p1[0], p1[1]);
                MaxPoint = new EPoint2D(p2[0], p2[1]);

                // Set Path (path of .dwg file)
                this.Path = acApp.ActiveDocument.FullName;

                // Set Name (= name of layout)
                this.Name = acApp.ActiveDocument.ActiveLayout.Name;
            }
            catch { }
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
        public void Plot(AcadApplication acApp, EPlotConfig pConfig)
        {
            AcadDocuments acDocs = acApp.Documents;
            int opFlag = DocIndex(acDocs);
            if (Open(acApp))
            {
                AcadLayout acLot = acApp.ActiveDocument.ActiveLayout;

                // Store plot configuration of active layout
                double numerator, denominator;
                EPlotConfig strConfig = new EPlotConfig(
                    acLot.ConfigName, acLot.CanonicalMediaName, acLot.StyleSheet,
                    acLot.PlotType, acLot.StandardScale, acLot.UseStandardScale,
                    1, acLot.CenterPlot, acLot.PlotRotation, false,
                    acLot.ScaleLineweights);//, acLot.PlotOrigin);
                acLot.GetCustomScale(out numerator, out denominator);

                try
                {
                    // Setting layout for plot
                    acLot.ConfigName = pConfig.Plotter;
                    acLot.CanonicalMediaName = pConfig.CanonicalMediaName;
                    acLot.StyleSheet = pConfig.PlotStyleTable;
                    acLot.PlotType = pConfig.PlotType;
                    acLot.StandardScale = pConfig.StandardScale;
                    acLot.UseStandardScale = pConfig.UseStandardScale;
                    // NOTE: -----------------------------------------------------
                    // ROTATION AUTOMATICALLY FEATURE WILL BE WRITTEN IN NEXT TIME
                    // -----------------------------------------------------------
                    acLot.PlotRotation = pConfig.PlotRotation;
                    acLot.ScaleLineweights = pConfig.IsScaleLineweight;
                    //acLot.PlotOrigin = pConfig.PlotOrigin;
                    acLot.SetCustomScale(pConfig.CustomScale, 1);

                    // Update plot config then Plot
                    acLot.RefreshPlotDeviceInfo();
                    acApp.ActiveDocument.Plot.PlotToDevice();
                }
                catch { }

                // Restore layout setting of plot
                acLot.ConfigName = strConfig.Plotter;
                acLot.CanonicalMediaName = strConfig.CanonicalMediaName;
                acLot.StyleSheet = strConfig.PlotStyleTable;
                acLot.PlotType = strConfig.PlotType;
                acLot.StandardScale = strConfig.StandardScale;
                acLot.UseStandardScale = strConfig.UseStandardScale;
                acLot.PlotRotation = strConfig.PlotRotation;
                acLot.ScaleLineweights = strConfig.IsScaleLineweight;
                //acLot.PlotOrigin = strConfig.PlotOrigin;
                acLot.SetCustomScale(numerator, denominator);
            }

            // If sheet is not opened initially, closing this after plot finish
            if (opFlag == -1 && DocIndex(acDocs) != -1)
            {
                acDocs.Item(DocIndex(acDocs)).Close(false);
            }
        }

        /* MISSING FEATURE:
         * 1. Previews sheet nomarlly
         * 2. Previews sheet with waiting time (sleep) for schedule preview of sheetset
         * 3. If sheet is plotted in type of plot as "window", adding Set Window Plotted
         *    (set MinPoint & MaxPoint) for layout function;
        */
    }
}
