using System;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Interop;

namespace QuickPrint.AutoCAD
{
    internal class AcadApp
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public static AcadApplication GetApplication()
        {
            // "AutoCAD.Application.17" uses 2007 or 2008, whichever was most recently run
            // "AutoCAD.Application.17.1" uses 2008, specifically
            const string progID = "AutoCAD.Application.17";

            AcadApplication acApp = null;

            try
            {
                acApp = (AcadApplication)Marshal.GetActiveObject(progID);
            }
            catch
            {
                try
                {
                    Type acType = Type.GetTypeFromProgID(progID);
                    acApp = (AcadApplication)Activator.CreateInstance(acType, true);
                }
                catch { }
            }

            return acApp;
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        public static void SetFocus(int acApphWnd)
        {
            IntPtr hWnd = new IntPtr(acApphWnd);
            SetForegroundWindow(hWnd);
        }
    }
}
