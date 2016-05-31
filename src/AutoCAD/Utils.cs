using System;
using System.Drawing;
using QuickPrint.AutoCAD;
using QuickPrint.Model;
﻿using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using QuickPrint.Components;

namespace QuickPrint.AutoCAD
{
    internal class Utils
    {
        public static List<Sheet> LayoutsFromDWGFile(string path)
        {
            // open autocad and set it to background process
            AcadApplication acad = AcadApp.GetApplication();
            if (acad == null)
            {
                acad = new AcadApplication();
                acad.Visible = true;
                AcadApp.SetFocus(acad.HWND);
            }
            AcadDocument doc = acad.Documents.Open(path, true);

            List<Sheet> res = new List<Sheet>();
            foreach (AcadLayout layoutName in doc.Layouts)
            {
                res.Add(new Sheet(path, layoutName.Name));
            }
            //foreach (AcadDocument doc in acad.Documents)
            //    doc.Close();
            acad.Application.Quit();
            return res; 
        }
    }
}
