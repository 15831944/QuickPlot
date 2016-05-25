using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace QuickPrint.AutoCAD
{
    public interface ISheetset
    {
        void AddSheet();
        void DeleteSheet();
        void ExportToPDF();
        void SetPrinter();
    }
}
