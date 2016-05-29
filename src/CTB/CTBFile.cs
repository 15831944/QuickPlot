using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace QuickPrint.CTB
{

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
        public void UpdateContent()
        {
            Content = new StringBuilder();
            Content.AppendLine("description=\"");
            Content.AppendLine("aci_table_available=" + AciTableAvailable.ToString().ToUpper());
            Content.AppendLine("scale_factor=" + ScaleFactor.ToString("F1", CultureInfo.InvariantCulture));
            Content.AppendLine("apply_factor=" + ApplyFactor.ToString().ToUpper());
            Content.AppendLine("custom_lineweight_display_units=" + CustomLineweightDisplayUnits);
            Content.AppendLine("aci_table{");
            //
            for (int i = 0; i < 255; i++)
            {
                Content.AppendLine(i + "=\"Color_" + (i + 1));
            }
            Content.AppendLine("}");
            Content.AppendLine("plot_style{");
            for (int i = 0; i <= 254; i++)
            {
                Colors.Add(new CTBColor(i));
            }
            for (int i = 0; i < Colors.Count; i++)
            {
                Content.Append(Colors[i].Content.ToString());
            }
            Content.AppendLine("}");
            Content.Append("custom_lineweight_table{\n"+
                 "0=0.0\n" +
                 "1=0.05\n" +
                 "2=0.09\n" +
                 "3=0.1\n" +
                 "4=0.13\n" +
                 "5=0.15\n" +
                 "6=0.18\n" +
                 "7=0.2\n" +
                 "8=0.25\n" +
                 "9=0.3\n" +
                 "10=0.35\n" +
                 "11=0.4\n" +
                 "12=0.45\n" +
                 "13=0.5\n" +
                 "14=0.53\n" +
                 "15=0.6\n" +
                 "16=0.65\n" +
                 "17=0.7\n" +
                 "18=0.8\n" +
                 "19=0.9\n" +
                 "20=1.0\n" +
                 "21=1.06\n" +
                 "22=1.2\n" +
                 "23=1.4\n" +
                 "24=1.58\n" +
                 "25=2.0\n" +
                 "26=2.11\n" +
                "}\n");


        }
        #endregion
        #region Contructors
        public CTBFile()
        {
            Colors = new List<CTBColor>();
            UpdateContent();
        }
        public CTBFile(PrintType printType) : this()
        {
            PrintType = printType;
            UpdateContent();
        
        }
        #endregion
    }
}
