/*Enesy.vn
 * First created by Cong
 * Edit: ......
 * Edit: .....
 * Issue: .....
 * ??
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace QuickPrint.AutoCAD
{
    public enum SheetStatus
    {
        Error = 0,
        Ready = 1,
        Printing = 2,
        Queued = 3,
        Stopped = 4,
        Finished = 5
    }

    /// <summary>
    /// A Sheet of AutoCAD Drawing
    /// </summary>
    public class Sheet
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SheetStatus Status { get; set; }

        public void GetPrintPreview() { }
        public void OpenInExplorer() { }
    }
}
