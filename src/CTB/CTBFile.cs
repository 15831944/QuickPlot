using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPrint.CTB
{
    public enum PrintType { 
        BlackAndWhite = 0,
        Color = 1
    }

    public class CTBFile
    {
        PrintType m_PrintType = PrintType.BlackAndWhite;
        public PrintType PrintType {
            get { return m_PrintType; }
            set { m_PrintType = value; }
        }

        string m_Description = "";
        public string Description {
            get { return m_Description; }
            set { m_Description = value; }
        }
        bool m_AciTableAvailable = true;
        public bool AciTableAvailable
        {
            get { return m_AciTableAvailable; }
            set { m_AciTableAvailable = value; }
        }
        double m_ScaleFactor = 1.0;
        public double ScaleFactor
        {
            get { return m_ScaleFactor; }
            set { m_ScaleFactor = value; }
        }
        bool m_ApplyFactor = false;
        public bool ApplyFactor
        {
            get { return m_ApplyFactor; }
            set { m_ApplyFactor = value; }
        }

        double m_CustomLineweightDisplayUnits = 0;
        public double CustomLineweightDisplayUnits
        {
            get { return m_CustomLineweightDisplayUnits; }
            set { m_CustomLineweightDisplayUnits = value; }
        }
        public StringBuilder Content { get; set; }
        public List<CTBColor> Colors { get; set; }
        #region Public Methods
        public void GenerateContent()
        {
            Content.AppendLine("description=\'\'");
            Content.AppendLine("aci_table_available=" + AciTableAvailable);
            Content.AppendLine("scale_factor=" + ScaleFactor);
            Content.AppendLine("apply_factor=" + ApplyFactor);
            Content.AppendLine("custom_lineweight_display_units=" + CustomLineweightDisplayUnits);
            Content.AppendLine("aci_table{");
            //
            for (int i = 0; i < 255; i++)
            {
                Content.AppendLine(i + "=\"Color_" + (i + 1));
            }
            Content.AppendLine("}");

        }
        #endregion
        #region Contructors
        public CTBFile()
        {
            Content = new StringBuilder();
            Colors = new List<CTBColor>();

        }
        public CTBFile(PrintType printType) : this()
        {
            PrintType = printType;
        
        }
        #endregion
    }
}
