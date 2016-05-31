using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickPrint
{
    public class PrinterHelp
    {
        public static IEnumerable<string> GetAllPrinters()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                yield return printer;
            }
        }
    }
}
