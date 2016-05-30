

namespace QuickPrint.Components
{
    /// <summary>
    /// Sheetset interface
    /// </summary>
    public interface ISheetSetBase
    {
        void Add(object ss);
        void AddAt(object ss, int index);
        void RemoveAt(int index);
    }
}
