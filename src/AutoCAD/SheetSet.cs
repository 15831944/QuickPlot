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
//  - !!!!!!!!! QUICK !!!!!!!!
//  - 1. Export Sheetset to PDF Just One Click
//  - 2. Change Printer of All Sheets Just One Click
// -------------------------------------------------------------------------------------
// 2016-05-25: Adding basic Properties & Methods
//             - Properties: Title
//             - Methods: ...
// -------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace QuickPrint.AutoCAD
{
    /// <summary>
    /// A Collection of Sheets
    /// </summary>
    internal class SheetSet : SheetsetBase
    {

    }
}
