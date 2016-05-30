// -------------------------------------------------------------------------------------
// COPYRIGHT (c) 2016 ENESY.VN
// THIS CLASS IS A SET OF SHEETS AND OTHERS SHEETSET.
// IT IS STRUCTURED IN SIMILAR STRUCTURE OF NODE (OF XML), IT MAY CONTAIN SHEET, SHEETS,
// AND EVEN ITSELF.
// EXPECTED FEATURE:
//  - SHEET MANAGEMENT
//  - MULTIPLE PLOTTING WITH CURRENT SETTING OF SHEET OR WITH OVERRIDE PLOT CONFIG.
//  - SINGLE PLOT (A SUBSET OR SHEET, SHEETS ONLY)
//  - ADDING SHEET, SHEETS
//  - ADDING OTHERS SHEETSET
//  - RENUMBER MEMBER IN ITSELF (RE-ORDER FOR PUBLISH)
//  - PREVIEW PARTIAL OR TOTAL SHEETSET WITH AND WITHOUT SCHEDULE (WAITING TIME FOR
//                                                                          A PREVIEW)
//  - EXPORT TO XML
//  - IMPORT FROM XML
//      (NOTE THAT XML STRUCTUR MUST BE EASY FOR ADD/REMOVE BETWEEN XML FILES)
//  - EXPORT CONTENTS
//  - ADDING SHEET ORDER (BY DTEXT)
//  - MAY PACK FILES IN SHEETSET, CONSITS XREF FILES (AS E-TRANMITS FEATURE, AUTOCAD)
// -------------------------------------------------------------------------------------
// 2016-05-25: Adding basic Properties & Methods
//             - Properties: Title
//             - Methods: ...
// -------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using QuickPrint.Model;
﻿using Autodesk.AutoCAD.Interop;

namespace QuickPrint.Components
{
    /// <summary>
    /// The order of sheet/sheetSet is numbered from zero (0)
    /// </summary>
    internal class SheetSet : ICloneable
    {
        /// <summary>
        /// Store sheet & sheet sets
        /// </summary>
        private List<object> m_sets = new List<object>();
        public List<object> Items
        {
            get { return m_sets; }
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
        /// Handle of sheetSet. Default value = (0,0,0) as Root Set
        /// </summary>
        private EHandle m_handle = new EHandle(0, 0, 0);
        public EHandle Handle
        {
            get { return m_handle; }
            set
            {
                m_handle = value;
                foreach (object obj in m_sets)
                {
                    if (obj is Sheet)
                    {
                        Sheet sh = obj as Sheet;
                        sh.Handle = new EHandle(
                            this.Handle.Grade + 1, this.Handle.Order, sh.Handle.Order
                            );
                    }
                    else
                    {
                        SheetSet sh = obj as SheetSet;
                        sh.Handle = new EHandle(
                            this.Handle.Grade + 1, this.Handle.Order, sh.Handle.Order
                            );
                    }
                }
            }
        }

        /// <summary>
        /// Number of sheet (except for sheetSet)
        /// </summary>
        public int CountSheet
        {
            get
            {
                int numSheet = 0;
                foreach (object obj in m_sets)
                {
                    if (obj is Sheet)
                    {
                        numSheet++;
                    }
                    else
                    {
                        SheetSet ss = obj as SheetSet;
                        numSheet += ss.CountSheet;
                    }
                }
                return numSheet;
            }
        }

        /// <summary>
        /// Gets or Sets a member with index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>A Sheet or SheetSet</returns>
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= m_sets.Count)
                {
                    return null;
                }
                return m_sets[index];
            }
            set
            {
                if (index >= 0 && index < m_sets.Count)
                {
                    if (value is Sheet || value is SheetSet)
                    {
                        m_sets[index] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SheetSet()
        {

        }

        /// <summary>
        /// Add another sheet set to this (= sheet set)
        /// </summary>
        /// <param name="ss">Is sheet or sheetSet</param>
        public void Add(object ss)
        {
            if (ss is Sheet || ss is SheetSet)
            {
                if (ss is SheetSet && this.Contains(ss))
                {
                    SheetSet sSet = (SheetSet)(ss as SheetSet).Clone();
                    sSet.Handle = new EHandle(
                        this.Handle.Grade + 1, this.Handle.Order, m_sets.Count
                    );
                    m_sets.Add(sSet);
                }
                else
                {
                    if (ss is Sheet)
                    {
                        Sheet s = ss as Sheet;
                        s.Handle = new EHandle(
                            this.Handle.Grade + 1, this.Handle.Order, m_sets.Count
                        );
                        m_sets.Add(s);
                    }
                    else
                    {
                        SheetSet s = ss as SheetSet;
                        s.Handle = new EHandle(
                            this.Handle.Grade + 1, this.Handle.Order, m_sets.Count
                        );
                        m_sets.Add(s);
                    }
                }
            }
        }

        /// <summary>
        /// Create a copy of this
        /// Fix error: if a sheetset is added to itself, plot will be performed forever
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            SheetSet clone = (SheetSet)this.MemberwiseClone();
            clone.m_sets = new List<object>();
            foreach (object obj in this.Items)
            {
                clone.m_sets.Add(obj);
            }
            clone.Title = this.Title;
            clone.Handle = this.Handle;

            return clone;
        }

        /// <summary>
        /// Check whether sheetset contains sheet/sheetset
        /// </summary>
        /// <param name="obj">A Sheet or Sheetset</param>
        /// <returns></returns>
        public bool Contains(object ss)
        {
            if (this == (ss as SheetSet)) return true;
            foreach (object obj in m_sets)
            {
                if (ss is Sheet && obj is Sheet)
                {
                    if ((Sheet)ss == (Sheet)obj) return true;
                }
                if (ss is SheetSet && obj is SheetSet)
                {
                    SheetSet sSet = obj as SheetSet;
                    if (sSet.Contains(ss)) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Remove sheet/sheetset away this
        /// </summary>
        /// <param name="index">Order of sheet/sheetset</param>
        public void RemoveAt(int index)
        {
            m_sets.RemoveAt(index);
        }

        /// <summary>
        /// Plot with layout's plot configuration
        /// </summary>
        public void Plot(AcadApplication acApp)
        {
            // Sort sheetset
            object[] sets = new object[m_sets.Count];

            foreach (object obj in m_sets)
            {
                if (obj is Sheet)
                {
                    Sheet s = obj as Sheet;
                    sets[s.Handle.Order] = obj;
                }
                else
                {
                    SheetSet s = obj as SheetSet;
                    sets[s.Handle.Order] = obj;
                }
            }

            // Plot without override config
            foreach (object obj in sets)
            {
                if (obj is Sheet)
                {
                    Sheet s = obj as Sheet;
                    s.Plot(acApp);
                }
                else
                {
                    SheetSet s = obj as SheetSet;
                    s.Plot(acApp);
                }
            }
            System.GC.Collect();
        }

        /// <summary>
        /// Plot with override plot configuration
        /// </summary>
        /// <param name="pConfig"></param>
        public void Plot(AcadApplication acApp, EPlotConfig pConfig)
        {
            // Sort sheetset
            object[] sets = new object[m_sets.Count];

            foreach (object obj in m_sets)
            {
                if (obj is Sheet)
                {
                    Sheet s = obj as Sheet;
                    sets[s.Handle.Order] = obj;
                }
                else
                {
                    SheetSet s = obj as SheetSet;
                    sets[s.Handle.Order] = obj;
                }
            }

            // Plot without override config
            foreach (object obj in sets)
            {
                if (obj is Sheet)
                {
                    Sheet s = obj as Sheet;
                    s.Plot(acApp, pConfig);
                }
                else
                {
                    SheetSet s = obj as SheetSet;
                    s.Plot(acApp, pConfig);
                }
            }
            System.GC.Collect();
        }
    }
}
