using System.Drawing;
using Autodesk.AutoCAD.Interop.Common;

namespace QuickPrint.Model
{
    public struct EPlotConfig
    {
        /// <summary>
        /// Printer (or Ploter)
        /// In command pallete, printer default is DWG To PDF.pc3
        /// </summary>
        public string Plotter;

        /// <summary>
        /// Paper size
        /// </summary>
        public string CanonicalMediaName;

        /// <summary>
        /// File plot style table (*.ctb)
        /// In command pallete, style table default is monochrome.ctb
        /// </summary>
        public string PlotStyleTable;

        /// <summary>
        /// Type of plot (Layout, Display, Window, Extents)
        /// </summary>
        public AcPlotType PlotType;

        /// <summary>
        /// Standard scale of sheet
        /// </summary>
        public AcPlotScale StandardScale;

        /// <summary>
        /// Read or set whether a plot should use a standard or custom scale
        /// </summary>
        public bool UseStandardScale;

        /// <summary>
        /// If IsFitToPaper is false, this command uses CustomScale to plot
        /// </summary>
        public double CustomScale;

        /// <summary>
        /// Center the plot option
        /// </summary>
        public bool IsCenterThePlot;

        /// <summary>
        /// Rotation of sheet
        /// </summary>
        public AcPlotRotation PlotRotation;

        /// <summary>
        /// Plot rotation automatically (fr Enesy)
        /// </summary>
        public bool IsAutoRotation;

        /// <summary>
        /// Is scale lineweight
        /// </summary>
        public bool IsScaleLineweight;

        /// <summary>
        /// Plot offset group (in plot form, autoCAD software)
        /// </summary>
        //public Point PlotOrigin;

        public EPlotConfig(string plotter, string canonicalMediaName,
            string plotStyleTable, AcPlotType plotType, AcPlotScale standardScale,
            bool useStandardScale, double customScale, bool isCenterThePlot,
            AcPlotRotation plotRotation, bool isAutoRotation,
            bool isScaleLineweight//, Point plotOrigin
            )
        {
            Plotter = plotter;
            CanonicalMediaName = canonicalMediaName;
            PlotStyleTable = plotStyleTable;
            PlotType = plotType;
            StandardScale = standardScale;
            UseStandardScale = useStandardScale;
            CustomScale = customScale;
            IsCenterThePlot = isCenterThePlot;
            PlotRotation = plotRotation;
            IsAutoRotation = isAutoRotation;
            IsScaleLineweight = isScaleLineweight;
            //PlotOrigin = plotOrigin;
        }
        
    }
}
