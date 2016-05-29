using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QuickPrint.CTB
{
    public class CTBColor : INotifyPropertyChanged
    {
        #region Noty
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        public string Name { get { return "Color_" + (Id + 1); } }
        public string LocalizedName { get { return "Color_" + (Id + 1); } }
        public string Description { get; set; }
        public const int Color = -16777216;
        public const int ColorPolicy = 5;
        public const int PhysicalPenNumber = 0;
        public const int VirtualPenNumber = 0;
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
            Id = 0;
            LinepatternSize = 0.5;
            Linetype = 31;
            FillStyle = 73;
            EndStyle = 4;
            JoinStyle = 5;
            AdaptiveLinetype = true;
            UpdateContent();
        }
        public CTBColor(int id) : this()
        {
            Id = id;
            UpdateContent();
        }
        #endregion
        #region Public Methods
        public void UpdateContent()
        {
            Content = new StringBuilder();
            Content.AppendLine(Id + "{");
            Content.AppendLine("name=\"" + Name);
            Content.AppendLine("localized_name=\"" + LocalizedName);
            Content.AppendLine("description=\"");
            Content.AppendLine("color=" + Color);
            Content.AppendLine("color_policy=" + ColorPolicy);
            Content.AppendLine("physical_pen_number=" + PhysicalPenNumber);
            Content.AppendLine("virtual_pen_number=" + VirtualPenNumber);
            Content.AppendLine("screen=" + Screen);
            Content.AppendLine("linepattern_size=" + LinepatternSize);
            Content.AppendLine("linetype=" + Linetype);
            Content.AppendLine("adaptive_linetype=" + AdaptiveLinetype.ToString().ToUpper());
            Content.AppendLine("lineweight=" + Lineweight);
            Content.AppendLine("fill_style=" + FillStyle);
            Content.AppendLine("end_style=" + EndStyle);
            Content.AppendLine("join_style=" + JoinStyle);
            Content.AppendLine("}");
        }
        #endregion
    }
}
