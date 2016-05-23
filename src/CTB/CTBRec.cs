using System;


namespace QuickPrint.CTB
{
    public class CtbRec
    {
        public string PenName { get; set; }

        public string LocName { get; set; }

        public string Descr { get; set; }

        public string AdLineType { get; set; }

        public int LineType { get; set; }

        public int LineWeight { get; set; }

        public int PenColor { get; set; }

        public int ModeColor { get; set; }

        public int PhysPenNr { get; set; }

        public int VirtPenNr { get; set; }

        public int Screen { get; set; }

        public int ColorPolicy { get; set; }

        public int FillStyle { get; set; }

        public int EndStyle { get; set; }

        public int JoinStyle { get; set; }

        public float LinePatternSize { get; set; }
    }
}
