using QuickPrint.Model;

namespace QuickPrint.Components
{
    /// <summary>
    /// Sheetset interface
    /// </summary>
    public interface ISheetSetPlot
    {
        void Plot();
        void Plot(EPlotConfig pConfig);
    }
}
