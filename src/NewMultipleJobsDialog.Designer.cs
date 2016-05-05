namespace VisualWget
{
    partial class NewMultipleJobsDialog
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
            this.components = new System.ComponentModel.Container();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.urlsTextBox = new System.Windows.Forms.TextBox();
            this.urlsLabel = new System.Windows.Forms.Label();
            this.noticeLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(310, 339);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(391, 339);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // urlsTextBox
            // 
            this.urlsTextBox.AcceptsReturn = true;
            this.urlsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlsTextBox.Location = new System.Drawing.Point(0, 0);
            this.urlsTextBox.Multiline = true;
            this.urlsTextBox.Name = "urlsTextBox";
            this.urlsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.urlsTextBox.Size = new System.Drawing.Size(460, 311);
            this.urlsTextBox.TabIndex = 0;
            this.urlsTextBox.WordWrap = false;
            // 
            // urlsLabel
            // 
            this.urlsLabel.AutoSize = true;
            this.urlsLabel.Location = new System.Drawing.Point(6, 6);
            this.urlsLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.urlsLabel.Name = "urlsLabel";
            this.urlsLabel.Size = new System.Drawing.Size(266, 13);
            this.urlsLabel.TabIndex = 0;
            this.urlsLabel.Text = "URL(s): [Use new line to split URLs into separate jobs]";
            // 
            // noticeLabel
            // 
            this.noticeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.noticeLabel.AutoSize = true;
            this.noticeLabel.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.noticeLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noticeLabel.Location = new System.Drawing.Point(6, 346);
            this.noticeLabel.Name = "noticeLabel";
            this.noticeLabel.Size = new System.Drawing.Size(175, 11);
            this.noticeLabel.TabIndex = 2;
            this.noticeLabel.Text = "* Using the Default Download Properties";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.urlsTextBox);
            this.panel1.Location = new System.Drawing.Point(6, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 311);
            this.panel1.TabIndex = 1;
            // 
            // NewMultipleJobsDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(472, 368);
            this.Controls.Add(this.urlsLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.noticeLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewMultipleJobsDialog";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Multiple Jobs";
            this.Load += new System.EventHandler(this.NewMultipleJobsDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox urlsTextBox;
        private System.Windows.Forms.Label urlsLabel;
        private System.Windows.Forms.Label noticeLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}