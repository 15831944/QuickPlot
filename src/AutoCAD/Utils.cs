using System;
using System.Drawing;
using QuickPrint.AutoCAD;
using QuickPrint.Model;
﻿using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace QuickPrint.AutoCAD
{
    internal class Utils
    {
        public static List<string> LayoutsFromDWGFile(string path)
        {
            // open autocad and set it to background process
            AcadApplication acad = AcadApp.GetApplication();
            if (acad == null)
            {
                acad = new AcadApplication();
                acad.Visible = true;
                AcadApp.SetFocus(acad.HWND);
            }
            AcadDocument doc = acad.Documents.Open(path);

            List<string> res = new List<string>();
            foreach (AcadLayout layoutName in doc.Layouts)
            {
                res.Add(layoutName.Name);
            }
            acad.Application.Quit();
            return res;
            
        }
    }
}
