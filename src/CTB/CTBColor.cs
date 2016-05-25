using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickPrint.CTB
{
    public class CTBColor
    {
        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public string Description { get; set; }
        public int Color { get; set; }
        public int ColorPolicy { get; set; }
        public int PhysicalPenNumber { get; set; }
        public int VirtualPenNumber { get; set; }
        public int Screen { get; set; }
        public double LinepatternSize { get; set; }
        public int Linetype { get; set; }
        public bool AdaptiveLinetype { get; set; }
        public int Lineweight { get; set; }
        public int FillStyle { get; set; }
        public int EndStyle { get; set; }
        public int JoinStyle { get; set; }
        public StringBuilder Content { get; set; }
        public int Id { get; set; }
        #region Constructors
        public CTBColor()
        {
            Content = new StringBuilder();
        }
        public CTBColor(int id) : this()
        {
            Id = id;
        }
        #endregion
        #region Public Methods
        public void GenerateContent()
        {

        }
        #endregion
    }
}
