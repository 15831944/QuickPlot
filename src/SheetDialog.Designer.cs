namespace VisualWget
{
    partial class SheetDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SheetDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.chckPrefixWithFilename = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listDrawings = new QuickPrint.Forms.DrawingListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = " Select drawing files containing layouts:";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(148, 42);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(189, 23);
            this.btn_Browse.TabIndex = 1;
            this.btn_Browse.Text = "&Browse for Draings...";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // chckPrefixWithFilename
            // 
            this.chckPrefixWithFilename.AutoSize = true;
            this.chckPrefixWithFilename.Checked = true;
            this.chckPrefixWithFilename.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckPrefixWithFilename.Location = new System.Drawing.Point(15, 340);
            this.chckPrefixWithFilename.Name = "chckPrefixWithFilename";
            this.chckPrefixWithFilename.Size = new System.Drawing.Size(226, 21);
            this.chckPrefixWithFilename.TabIndex = 3;
            this.chckPrefixWithFilename.Text = "Prefix sheet titles with file name";
            this.chckPrefixWithFilename.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(148, 385);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(130, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "&Import Checked";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(284, 385);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(383, 385);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "&Help";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // listDrawings
            // 
            this.listDrawings.CheckBoxes = true;
            this.listDrawings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listDrawings.FullRowSelect = true;
            this.listDrawings.GridLines = true;
            this.listDrawings.Location = new System.Drawing.Point(15, 80);
            this.listDrawings.Name = "listDrawings";
            this.listDrawings.Size = new System.Drawing.Size(458, 240);
            this.listDrawings.TabIndex = 7;
            this.listDrawings.UseCompatibleStateImageBehavior = false;
            this.listDrawings.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Drawing Name";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Layout Name";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Status";
            this.columnHeader6.Width = 154;
            // 
            // SheetDialog
            // 
            this.ClientSize = new System.Drawing.Size(485, 420);
            this.Controls.Add(this.listDrawings);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.chckPrefixWithFilename);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SheetDialog";
            this.Text = "Import Sheets";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox startOnOkCheckBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.CheckBox chckPrefixWithFilename;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private QuickPrint.Forms.DrawingListView listDrawings;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}