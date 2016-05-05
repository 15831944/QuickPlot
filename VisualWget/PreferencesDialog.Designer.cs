namespace VisualWget
{
    partial class PreferencesDialog
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
            this.maxRunningJobsLabel = new System.Windows.Forms.Label();
            this.queueGroupBox = new System.Windows.Forms.GroupBox();
            this.maxRunningJobsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.speedLimitGroupBox = new System.Windows.Forms.GroupBox();
            this.speedLimitLabel1 = new System.Windows.Forms.Label();
            this.speedLimitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.speedLimitLabel2 = new System.Windows.Forms.Label();
            this.retryingGroupBox = new System.Windows.Forms.GroupBox();
            this.retryNoticeLabel = new System.Windows.Forms.Label();
            this.retryCheckBox = new System.Windows.Forms.CheckBox();
            this.retryAttemptsLabel = new System.Windows.Forms.Label();
            this.retryAttemptsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.timeBetweenRetryAttemptsLabel = new System.Windows.Forms.Label();
            this.timeBetweenRetryAttemptsComboBox = new System.Windows.Forms.ComboBox();
            this.updatesGroupBox = new System.Windows.Forms.GroupBox();
            this.checkForUpdatesCheckBox = new System.Windows.Forms.CheckBox();
            this.checkForUpdatesComboBox = new System.Windows.Forms.ComboBox();
            this.logOnGroupBox = new System.Windows.Forms.GroupBox();
            this.runWhenLogOnCheckBox = new System.Windows.Forms.CheckBox();
            this.proxiesGroupBox = new System.Windows.Forms.GroupBox();
            this.httpProxyLabel = new System.Windows.Forms.Label();
            this.httpProxyTextBox = new System.Windows.Forms.TextBox();
            this.httpsProxyLabel = new System.Windows.Forms.Label();
            this.httpsProxyTextBox = new System.Windows.Forms.TextBox();
            this.ftpProxyLabel = new System.Windows.Forms.Label();
            this.ftpProxyTextBox = new System.Windows.Forms.TextBox();
            this.noProxyLabel = new System.Windows.Forms.Label();
            this.noProxyTextBox = new System.Windows.Forms.TextBox();
            this.newJobGroupBox = new System.Windows.Forms.GroupBox();
            this.noPromptOnNewJobCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.systemTrayGroupBox = new System.Windows.Forms.GroupBox();
            this.closeToTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.minimizeToTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.showBalloonTipCheckBox = new System.Windows.Forms.CheckBox();
            this.alwaysShowTrayIconCheckBox = new System.Windows.Forms.CheckBox();
            this.translationsGroupBox = new System.Windows.Forms.GroupBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.listViewGroupBox = new System.Windows.Forms.GroupBox();
            this.promptWhenOpenFileTypesTextBox = new System.Windows.Forms.TextBox();
            this.promptWhenOpenFileTypesLabel = new System.Windows.Forms.Label();
            this.listViewDoubleClickOpenCheckBox = new System.Windows.Forms.CheckBox();
            this.generalPanel = new System.Windows.Forms.Panel();
            this.interfacePanel = new System.Windows.Forms.Panel();
            this.sizeUnitGroupBox = new System.Windows.Forms.GroupBox();
            this.sizeTbRadioButton = new System.Windows.Forms.RadioButton();
            this.sizeBytesRadioButton = new System.Windows.Forms.RadioButton();
            this.sizeGbRadioButton = new System.Windows.Forms.RadioButton();
            this.sizeKbRadioButton = new System.Windows.Forms.RadioButton();
            this.sizeMbRadioButton = new System.Windows.Forms.RadioButton();
            this.sizeAutoRadioButton = new System.Windows.Forms.RadioButton();
            this.fontGroupBox = new System.Windows.Forms.GroupBox();
            this.useDefaultFontButton = new System.Windows.Forms.Button();
            this.setInterfaceFontButton = new System.Windows.Forms.Button();
            this.othersPanel = new System.Windows.Forms.Panel();
            this.wgetConsolesGroupBox = new System.Windows.Forms.GroupBox();
            this.hideConsolesCheckBox = new System.Windows.Forms.CheckBox();
            this.redirectOutputsCheckBox = new System.Windows.Forms.CheckBox();
            this.prefCatListBox = new System.Windows.Forms.ListBox();
            this.soundsPanel = new System.Windows.Forms.Panel();
            this.systemSoundsGroupBox = new System.Windows.Forms.GroupBox();
            this.allDownloadsFinishedSoundCheckBox = new System.Windows.Forms.CheckBox();
            this.downloadFinishedSoundCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.queueGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxRunningJobsNumericUpDown)).BeginInit();
            this.speedLimitGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedLimitNumericUpDown)).BeginInit();
            this.retryingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retryAttemptsNumericUpDown)).BeginInit();
            this.updatesGroupBox.SuspendLayout();
            this.logOnGroupBox.SuspendLayout();
            this.proxiesGroupBox.SuspendLayout();
            this.newJobGroupBox.SuspendLayout();
            this.systemTrayGroupBox.SuspendLayout();
            this.translationsGroupBox.SuspendLayout();
            this.listViewGroupBox.SuspendLayout();
            this.generalPanel.SuspendLayout();
            this.interfacePanel.SuspendLayout();
            this.sizeUnitGroupBox.SuspendLayout();
            this.fontGroupBox.SuspendLayout();
            this.othersPanel.SuspendLayout();
            this.wgetConsolesGroupBox.SuspendLayout();
            this.soundsPanel.SuspendLayout();
            this.systemSoundsGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // maxRunningJobsLabel
            // 
            this.maxRunningJobsLabel.AutoSize = true;
            this.maxRunningJobsLabel.Location = new System.Drawing.Point(6, 22);
            this.maxRunningJobsLabel.Name = "maxRunningJobsLabel";
            this.maxRunningJobsLabel.Size = new System.Drawing.Size(93, 13);
            this.maxRunningJobsLabel.TabIndex = 0;
            this.maxRunningJobsLabel.Text = "Max running jobs:";
            // 
            // queueGroupBox
            // 
            this.queueGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.queueGroupBox.Controls.Add(this.maxRunningJobsLabel);
            this.queueGroupBox.Controls.Add(this.maxRunningJobsNumericUpDown);
            this.queueGroupBox.Location = new System.Drawing.Point(3, 56);
            this.queueGroupBox.Name = "queueGroupBox";
            this.queueGroupBox.Size = new System.Drawing.Size(308, 47);
            this.queueGroupBox.TabIndex = 1;
            this.queueGroupBox.TabStop = false;
            this.queueGroupBox.Text = "Queue";
            // 
            // maxRunningJobsNumericUpDown
            // 
            this.maxRunningJobsNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.maxRunningJobsNumericUpDown.Location = new System.Drawing.Point(202, 20);
            this.maxRunningJobsNumericUpDown.Name = "maxRunningJobsNumericUpDown";
            this.maxRunningJobsNumericUpDown.Size = new System.Drawing.Size(100, 21);
            this.maxRunningJobsNumericUpDown.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(290, 341);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(371, 341);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // speedLimitGroupBox
            // 
            this.speedLimitGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.speedLimitGroupBox.Controls.Add(this.speedLimitLabel1);
            this.speedLimitGroupBox.Controls.Add(this.speedLimitNumericUpDown);
            this.speedLimitGroupBox.Controls.Add(this.speedLimitLabel2);
            this.speedLimitGroupBox.Location = new System.Drawing.Point(3, 3);
            this.speedLimitGroupBox.Name = "speedLimitGroupBox";
            this.speedLimitGroupBox.Size = new System.Drawing.Size(308, 47);
            this.speedLimitGroupBox.TabIndex = 0;
            this.speedLimitGroupBox.TabStop = false;
            this.speedLimitGroupBox.Text = "Speed limit";
            // 
            // speedLimitLabel1
            // 
            this.speedLimitLabel1.AutoSize = true;
            this.speedLimitLabel1.Location = new System.Drawing.Point(6, 22);
            this.speedLimitLabel1.Name = "speedLimitLabel1";
            this.speedLimitLabel1.Size = new System.Drawing.Size(117, 13);
            this.speedLimitLabel1.TabIndex = 0;
            this.speedLimitLabel1.Text = "Limit download rate to:";
            // 
            // speedLimitNumericUpDown
            // 
            this.speedLimitNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.speedLimitNumericUpDown.DecimalPlaces = 1;
            this.speedLimitNumericUpDown.Location = new System.Drawing.Point(202, 20);
            this.speedLimitNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            65536});
            this.speedLimitNumericUpDown.Name = "speedLimitNumericUpDown";
            this.speedLimitNumericUpDown.Size = new System.Drawing.Size(66, 21);
            this.speedLimitNumericUpDown.TabIndex = 1;
            // 
            // speedLimitLabel2
            // 
            this.speedLimitLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedLimitLabel2.AutoSize = true;
            this.speedLimitLabel2.Location = new System.Drawing.Point(274, 22);
            this.speedLimitLabel2.Name = "speedLimitLabel2";
            this.speedLimitLabel2.Size = new System.Drawing.Size(28, 13);
            this.speedLimitLabel2.TabIndex = 2;
            this.speedLimitLabel2.Text = "KB/s";
            // 
            // retryingGroupBox
            // 
            this.retryingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.retryingGroupBox.Controls.Add(this.retryNoticeLabel);
            this.retryingGroupBox.Controls.Add(this.retryCheckBox);
            this.retryingGroupBox.Controls.Add(this.retryAttemptsLabel);
            this.retryingGroupBox.Controls.Add(this.retryAttemptsNumericUpDown);
            this.retryingGroupBox.Controls.Add(this.timeBetweenRetryAttemptsLabel);
            this.retryingGroupBox.Controls.Add(this.timeBetweenRetryAttemptsComboBox);
            this.retryingGroupBox.Location = new System.Drawing.Point(3, 109);
            this.retryingGroupBox.Name = "retryingGroupBox";
            this.retryingGroupBox.Size = new System.Drawing.Size(308, 111);
            this.retryingGroupBox.TabIndex = 2;
            this.retryingGroupBox.TabStop = false;
            this.retryingGroupBox.Text = "Retrying";
            // 
            // retryNoticeLabel
            // 
            this.retryNoticeLabel.AutoSize = true;
            this.retryNoticeLabel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retryNoticeLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.retryNoticeLabel.Location = new System.Drawing.Point(6, 17);
            this.retryNoticeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.retryNoticeLabel.Name = "retryNoticeLabel";
            this.retryNoticeLabel.Size = new System.Drawing.Size(209, 11);
            this.retryNoticeLabel.TabIndex = 0;
            this.retryNoticeLabel.Text = "* Applies to single file resumable downloads only";
            // 
            // retryCheckBox
            // 
            this.retryCheckBox.AutoSize = true;
            this.retryCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.retryCheckBox.Location = new System.Drawing.Point(6, 34);
            this.retryCheckBox.Name = "retryCheckBox";
            this.retryCheckBox.Size = new System.Drawing.Size(225, 17);
            this.retryCheckBox.TabIndex = 1;
            this.retryCheckBox.Text = "Retry if download is stopped unexpectedly";
            this.retryCheckBox.UseVisualStyleBackColor = false;
            this.retryCheckBox.CheckedChanged += new System.EventHandler(this.retryCheckBox_CheckedChanged);
            // 
            // retryAttemptsLabel
            // 
            this.retryAttemptsLabel.AutoSize = true;
            this.retryAttemptsLabel.Location = new System.Drawing.Point(6, 59);
            this.retryAttemptsLabel.Name = "retryAttemptsLabel";
            this.retryAttemptsLabel.Size = new System.Drawing.Size(84, 13);
            this.retryAttemptsLabel.TabIndex = 2;
            this.retryAttemptsLabel.Text = "Retry attempts:";
            // 
            // retryAttemptsNumericUpDown
            // 
            this.retryAttemptsNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.retryAttemptsNumericUpDown.Location = new System.Drawing.Point(202, 57);
            this.retryAttemptsNumericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.retryAttemptsNumericUpDown.Name = "retryAttemptsNumericUpDown";
            this.retryAttemptsNumericUpDown.Size = new System.Drawing.Size(100, 21);
            this.retryAttemptsNumericUpDown.TabIndex = 3;
            // 
            // timeBetweenRetryAttemptsLabel
            // 
            this.timeBetweenRetryAttemptsLabel.AutoSize = true;
            this.timeBetweenRetryAttemptsLabel.Location = new System.Drawing.Point(6, 87);
            this.timeBetweenRetryAttemptsLabel.Name = "timeBetweenRetryAttemptsLabel";
            this.timeBetweenRetryAttemptsLabel.Size = new System.Drawing.Size(151, 13);
            this.timeBetweenRetryAttemptsLabel.TabIndex = 4;
            this.timeBetweenRetryAttemptsLabel.Text = "Time between retry attempts:";
            // 
            // timeBetweenRetryAttemptsComboBox
            // 
            this.timeBetweenRetryAttemptsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timeBetweenRetryAttemptsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeBetweenRetryAttemptsComboBox.FormattingEnabled = true;
            this.timeBetweenRetryAttemptsComboBox.Location = new System.Drawing.Point(202, 84);
            this.timeBetweenRetryAttemptsComboBox.Name = "timeBetweenRetryAttemptsComboBox";
            this.timeBetweenRetryAttemptsComboBox.Size = new System.Drawing.Size(100, 21);
            this.timeBetweenRetryAttemptsComboBox.TabIndex = 5;
            // 
            // updatesGroupBox
            // 
            this.updatesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.updatesGroupBox.Controls.Add(this.checkForUpdatesCheckBox);
            this.updatesGroupBox.Controls.Add(this.checkForUpdatesComboBox);
            this.updatesGroupBox.Location = new System.Drawing.Point(3, 226);
            this.updatesGroupBox.Name = "updatesGroupBox";
            this.updatesGroupBox.Size = new System.Drawing.Size(308, 47);
            this.updatesGroupBox.TabIndex = 3;
            this.updatesGroupBox.TabStop = false;
            this.updatesGroupBox.Text = "Updates";
            // 
            // checkForUpdatesCheckBox
            // 
            this.checkForUpdatesCheckBox.AutoSize = true;
            this.checkForUpdatesCheckBox.Location = new System.Drawing.Point(6, 22);
            this.checkForUpdatesCheckBox.Name = "checkForUpdatesCheckBox";
            this.checkForUpdatesCheckBox.Size = new System.Drawing.Size(177, 17);
            this.checkForUpdatesCheckBox.TabIndex = 0;
            this.checkForUpdatesCheckBox.Text = "Check for updates automatically";
            this.checkForUpdatesCheckBox.UseVisualStyleBackColor = true;
            this.checkForUpdatesCheckBox.CheckedChanged += new System.EventHandler(this.checkForUpdatesCheckBox_CheckedChanged);
            // 
            // checkForUpdatesComboBox
            // 
            this.checkForUpdatesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkForUpdatesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checkForUpdatesComboBox.FormattingEnabled = true;
            this.checkForUpdatesComboBox.Location = new System.Drawing.Point(202, 20);
            this.checkForUpdatesComboBox.Name = "checkForUpdatesComboBox";
            this.checkForUpdatesComboBox.Size = new System.Drawing.Size(100, 21);
            this.checkForUpdatesComboBox.TabIndex = 1;
            // 
            // logOnGroupBox
            // 
            this.logOnGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logOnGroupBox.Controls.Add(this.runWhenLogOnCheckBox);
            this.logOnGroupBox.Location = new System.Drawing.Point(3, 3);
            this.logOnGroupBox.Name = "logOnGroupBox";
            this.logOnGroupBox.Size = new System.Drawing.Size(308, 43);
            this.logOnGroupBox.TabIndex = 0;
            this.logOnGroupBox.TabStop = false;
            this.logOnGroupBox.Text = "Log on";
            // 
            // runWhenLogOnCheckBox
            // 
            this.runWhenLogOnCheckBox.AutoSize = true;
            this.runWhenLogOnCheckBox.Location = new System.Drawing.Point(6, 20);
            this.runWhenLogOnCheckBox.Name = "runWhenLogOnCheckBox";
            this.runWhenLogOnCheckBox.Size = new System.Drawing.Size(211, 17);
            this.runWhenLogOnCheckBox.TabIndex = 0;
            this.runWhenLogOnCheckBox.Text = "Run VisualWget each time that I log on";
            this.runWhenLogOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // proxiesGroupBox
            // 
            this.proxiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.proxiesGroupBox.Controls.Add(this.httpProxyLabel);
            this.proxiesGroupBox.Controls.Add(this.httpProxyTextBox);
            this.proxiesGroupBox.Controls.Add(this.httpsProxyLabel);
            this.proxiesGroupBox.Controls.Add(this.httpsProxyTextBox);
            this.proxiesGroupBox.Controls.Add(this.ftpProxyLabel);
            this.proxiesGroupBox.Controls.Add(this.ftpProxyTextBox);
            this.proxiesGroupBox.Controls.Add(this.noProxyLabel);
            this.proxiesGroupBox.Controls.Add(this.noProxyTextBox);
            this.proxiesGroupBox.Location = new System.Drawing.Point(3, 52);
            this.proxiesGroupBox.Name = "proxiesGroupBox";
            this.proxiesGroupBox.Size = new System.Drawing.Size(308, 128);
            this.proxiesGroupBox.TabIndex = 1;
            this.proxiesGroupBox.TabStop = false;
            this.proxiesGroupBox.Text = "Proxies";
            // 
            // httpProxyLabel
            // 
            this.httpProxyLabel.AutoSize = true;
            this.httpProxyLabel.Location = new System.Drawing.Point(6, 23);
            this.httpProxyLabel.Name = "httpProxyLabel";
            this.httpProxyLabel.Size = new System.Drawing.Size(67, 13);
            this.httpProxyLabel.TabIndex = 0;
            this.httpProxyLabel.Text = "HTTP proxy:";
            // 
            // httpProxyTextBox
            // 
            this.httpProxyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.httpProxyTextBox.Location = new System.Drawing.Point(85, 20);
            this.httpProxyTextBox.Name = "httpProxyTextBox";
            this.httpProxyTextBox.Size = new System.Drawing.Size(217, 21);
            this.httpProxyTextBox.TabIndex = 1;
            // 
            // httpsProxyLabel
            // 
            this.httpsProxyLabel.AutoSize = true;
            this.httpsProxyLabel.Location = new System.Drawing.Point(6, 50);
            this.httpsProxyLabel.Name = "httpsProxyLabel";
            this.httpsProxyLabel.Size = new System.Drawing.Size(73, 13);
            this.httpsProxyLabel.TabIndex = 2;
            this.httpsProxyLabel.Text = "HTTPS proxy:";
            // 
            // httpsProxyTextBox
            // 
            this.httpsProxyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.httpsProxyTextBox.Location = new System.Drawing.Point(85, 47);
            this.httpsProxyTextBox.Name = "httpsProxyTextBox";
            this.httpsProxyTextBox.Size = new System.Drawing.Size(217, 21);
            this.httpsProxyTextBox.TabIndex = 3;
            // 
            // ftpProxyLabel
            // 
            this.ftpProxyLabel.AutoSize = true;
            this.ftpProxyLabel.Location = new System.Drawing.Point(6, 77);
            this.ftpProxyLabel.Name = "ftpProxyLabel";
            this.ftpProxyLabel.Size = new System.Drawing.Size(60, 13);
            this.ftpProxyLabel.TabIndex = 4;
            this.ftpProxyLabel.Text = "FTP proxy:";
            // 
            // ftpProxyTextBox
            // 
            this.ftpProxyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ftpProxyTextBox.Location = new System.Drawing.Point(85, 74);
            this.ftpProxyTextBox.Name = "ftpProxyTextBox";
            this.ftpProxyTextBox.Size = new System.Drawing.Size(217, 21);
            this.ftpProxyTextBox.TabIndex = 5;
            // 
            // noProxyLabel
            // 
            this.noProxyLabel.AutoSize = true;
            this.noProxyLabel.Location = new System.Drawing.Point(6, 104);
            this.noProxyLabel.Name = "noProxyLabel";
            this.noProxyLabel.Size = new System.Drawing.Size(55, 13);
            this.noProxyLabel.TabIndex = 6;
            this.noProxyLabel.Text = "No proxy:";
            // 
            // noProxyTextBox
            // 
            this.noProxyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.noProxyTextBox.Location = new System.Drawing.Point(85, 101);
            this.noProxyTextBox.Name = "noProxyTextBox";
            this.noProxyTextBox.Size = new System.Drawing.Size(217, 21);
            this.noProxyTextBox.TabIndex = 7;
            // 
            // newJobGroupBox
            // 
            this.newJobGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newJobGroupBox.Controls.Add(this.noPromptOnNewJobCheckBox);
            this.newJobGroupBox.Location = new System.Drawing.Point(3, 186);
            this.newJobGroupBox.Name = "newJobGroupBox";
            this.newJobGroupBox.Size = new System.Drawing.Size(308, 43);
            this.newJobGroupBox.TabIndex = 2;
            this.newJobGroupBox.TabStop = false;
            this.newJobGroupBox.Text = "New job";
            // 
            // noPromptOnNewJobCheckBox
            // 
            this.noPromptOnNewJobCheckBox.AutoSize = true;
            this.noPromptOnNewJobCheckBox.Location = new System.Drawing.Point(6, 20);
            this.noPromptOnNewJobCheckBox.Name = "noPromptOnNewJobCheckBox";
            this.noPromptOnNewJobCheckBox.Size = new System.Drawing.Size(259, 17);
            this.noPromptOnNewJobCheckBox.TabIndex = 0;
            this.noPromptOnNewJobCheckBox.Text = "Do not prompt when creating a new job externally";
            this.noPromptOnNewJobCheckBox.UseVisualStyleBackColor = true;
            // 
            // systemTrayGroupBox
            // 
            this.systemTrayGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.systemTrayGroupBox.Controls.Add(this.closeToTrayCheckBox);
            this.systemTrayGroupBox.Controls.Add(this.minimizeToTrayCheckBox);
            this.systemTrayGroupBox.Controls.Add(this.showBalloonTipCheckBox);
            this.systemTrayGroupBox.Controls.Add(this.alwaysShowTrayIconCheckBox);
            this.systemTrayGroupBox.Location = new System.Drawing.Point(3, 56);
            this.systemTrayGroupBox.Name = "systemTrayGroupBox";
            this.systemTrayGroupBox.Size = new System.Drawing.Size(308, 66);
            this.systemTrayGroupBox.TabIndex = 1;
            this.systemTrayGroupBox.TabStop = false;
            this.systemTrayGroupBox.Text = "System tray";
            // 
            // closeToTrayCheckBox
            // 
            this.closeToTrayCheckBox.AutoSize = true;
            this.closeToTrayCheckBox.Location = new System.Drawing.Point(6, 20);
            this.closeToTrayCheckBox.Name = "closeToTrayCheckBox";
            this.closeToTrayCheckBox.Size = new System.Drawing.Size(84, 17);
            this.closeToTrayCheckBox.TabIndex = 0;
            this.closeToTrayCheckBox.Text = "Close to tray";
            this.closeToTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // minimizeToTrayCheckBox
            // 
            this.minimizeToTrayCheckBox.AutoSize = true;
            this.minimizeToTrayCheckBox.Location = new System.Drawing.Point(156, 20);
            this.minimizeToTrayCheckBox.Name = "minimizeToTrayCheckBox";
            this.minimizeToTrayCheckBox.Size = new System.Drawing.Size(98, 17);
            this.minimizeToTrayCheckBox.TabIndex = 1;
            this.minimizeToTrayCheckBox.Text = "Minimize to tray";
            this.minimizeToTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // showBalloonTipCheckBox
            // 
            this.showBalloonTipCheckBox.AutoSize = true;
            this.showBalloonTipCheckBox.Location = new System.Drawing.Point(6, 43);
            this.showBalloonTipCheckBox.Name = "showBalloonTipCheckBox";
            this.showBalloonTipCheckBox.Size = new System.Drawing.Size(104, 17);
            this.showBalloonTipCheckBox.TabIndex = 2;
            this.showBalloonTipCheckBox.Text = "Show balloon tip";
            this.showBalloonTipCheckBox.UseVisualStyleBackColor = true;
            // 
            // alwaysShowTrayIconCheckBox
            // 
            this.alwaysShowTrayIconCheckBox.AutoSize = true;
            this.alwaysShowTrayIconCheckBox.Location = new System.Drawing.Point(156, 43);
            this.alwaysShowTrayIconCheckBox.Name = "alwaysShowTrayIconCheckBox";
            this.alwaysShowTrayIconCheckBox.Size = new System.Drawing.Size(130, 17);
            this.alwaysShowTrayIconCheckBox.TabIndex = 3;
            this.alwaysShowTrayIconCheckBox.Text = "Always show tray icon";
            this.alwaysShowTrayIconCheckBox.UseVisualStyleBackColor = true;
            // 
            // translationsGroupBox
            // 
            this.translationsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.translationsGroupBox.Controls.Add(this.languageLabel);
            this.translationsGroupBox.Controls.Add(this.languageComboBox);
            this.translationsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.translationsGroupBox.Name = "translationsGroupBox";
            this.translationsGroupBox.Size = new System.Drawing.Size(308, 47);
            this.translationsGroupBox.TabIndex = 0;
            this.translationsGroupBox.TabStop = false;
            this.translationsGroupBox.Text = "Translations";
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(6, 23);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(58, 13);
            this.languageLabel.TabIndex = 0;
            this.languageLabel.Text = "Language:";
            // 
            // languageComboBox
            // 
            this.languageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Location = new System.Drawing.Point(70, 20);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(232, 21);
            this.languageComboBox.TabIndex = 1;
            // 
            // listViewGroupBox
            // 
            this.listViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewGroupBox.Controls.Add(this.promptWhenOpenFileTypesTextBox);
            this.listViewGroupBox.Controls.Add(this.promptWhenOpenFileTypesLabel);
            this.listViewGroupBox.Controls.Add(this.listViewDoubleClickOpenCheckBox);
            this.listViewGroupBox.Location = new System.Drawing.Point(3, 126);
            this.listViewGroupBox.Name = "listViewGroupBox";
            this.listViewGroupBox.Size = new System.Drawing.Size(308, 83);
            this.listViewGroupBox.TabIndex = 2;
            this.listViewGroupBox.TabStop = false;
            this.listViewGroupBox.Text = "Download list";
            // 
            // promptWhenOpenFileTypesTextBox
            // 
            this.promptWhenOpenFileTypesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.promptWhenOpenFileTypesTextBox.Location = new System.Drawing.Point(6, 56);
            this.promptWhenOpenFileTypesTextBox.Name = "promptWhenOpenFileTypesTextBox";
            this.promptWhenOpenFileTypesTextBox.Size = new System.Drawing.Size(296, 21);
            this.promptWhenOpenFileTypesTextBox.TabIndex = 2;
            // 
            // promptWhenOpenFileTypesLabel
            // 
            this.promptWhenOpenFileTypesLabel.AutoSize = true;
            this.promptWhenOpenFileTypesLabel.Location = new System.Drawing.Point(6, 40);
            this.promptWhenOpenFileTypesLabel.Name = "promptWhenOpenFileTypesLabel";
            this.promptWhenOpenFileTypesLabel.Size = new System.Drawing.Size(205, 13);
            this.promptWhenOpenFileTypesLabel.TabIndex = 1;
            this.promptWhenOpenFileTypesLabel.Text = "Prompt when opening these type of files:";
            // 
            // listViewDoubleClickOpenCheckBox
            // 
            this.listViewDoubleClickOpenCheckBox.AutoSize = true;
            this.listViewDoubleClickOpenCheckBox.Location = new System.Drawing.Point(6, 20);
            this.listViewDoubleClickOpenCheckBox.Name = "listViewDoubleClickOpenCheckBox";
            this.listViewDoubleClickOpenCheckBox.Size = new System.Drawing.Size(257, 17);
            this.listViewDoubleClickOpenCheckBox.TabIndex = 0;
            this.listViewDoubleClickOpenCheckBox.Text = "Double-click to open file with associated program";
            this.listViewDoubleClickOpenCheckBox.UseVisualStyleBackColor = true;
            // 
            // generalPanel
            // 
            this.generalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.generalPanel.Controls.Add(this.speedLimitGroupBox);
            this.generalPanel.Controls.Add(this.queueGroupBox);
            this.generalPanel.Controls.Add(this.updatesGroupBox);
            this.generalPanel.Controls.Add(this.retryingGroupBox);
            this.generalPanel.Location = new System.Drawing.Point(132, 6);
            this.generalPanel.Name = "generalPanel";
            this.generalPanel.Size = new System.Drawing.Size(314, 329);
            this.generalPanel.TabIndex = 1;
            // 
            // interfacePanel
            // 
            this.interfacePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.interfacePanel.Controls.Add(this.sizeUnitGroupBox);
            this.interfacePanel.Controls.Add(this.fontGroupBox);
            this.interfacePanel.Controls.Add(this.listViewGroupBox);
            this.interfacePanel.Controls.Add(this.translationsGroupBox);
            this.interfacePanel.Controls.Add(this.systemTrayGroupBox);
            this.interfacePanel.Location = new System.Drawing.Point(132, 6);
            this.interfacePanel.Name = "interfacePanel";
            this.interfacePanel.Size = new System.Drawing.Size(314, 329);
            this.interfacePanel.TabIndex = 2;
            // 
            // sizeUnitGroupBox
            // 
            this.sizeUnitGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sizeUnitGroupBox.Controls.Add(this.sizeTbRadioButton);
            this.sizeUnitGroupBox.Controls.Add(this.sizeBytesRadioButton);
            this.sizeUnitGroupBox.Controls.Add(this.sizeGbRadioButton);
            this.sizeUnitGroupBox.Controls.Add(this.sizeKbRadioButton);
            this.sizeUnitGroupBox.Controls.Add(this.sizeMbRadioButton);
            this.sizeUnitGroupBox.Controls.Add(this.sizeAutoRadioButton);
            this.sizeUnitGroupBox.Location = new System.Drawing.Point(3, 215);
            this.sizeUnitGroupBox.Name = "sizeUnitGroupBox";
            this.sizeUnitGroupBox.Size = new System.Drawing.Size(308, 43);
            this.sizeUnitGroupBox.TabIndex = 3;
            this.sizeUnitGroupBox.TabStop = false;
            this.sizeUnitGroupBox.Text = "Size unit";
            // 
            // sizeTbRadioButton
            // 
            this.sizeTbRadioButton.AutoSize = true;
            this.sizeTbRadioButton.Location = new System.Drawing.Point(259, 20);
            this.sizeTbRadioButton.Name = "sizeTbRadioButton";
            this.sizeTbRadioButton.Size = new System.Drawing.Size(39, 17);
            this.sizeTbRadioButton.TabIndex = 5;
            this.sizeTbRadioButton.TabStop = true;
            this.sizeTbRadioButton.Text = "TB";
            this.sizeTbRadioButton.UseVisualStyleBackColor = true;
            // 
            // sizeBytesRadioButton
            // 
            this.sizeBytesRadioButton.AutoSize = true;
            this.sizeBytesRadioButton.Location = new System.Drawing.Point(76, 20);
            this.sizeBytesRadioButton.Name = "sizeBytesRadioButton";
            this.sizeBytesRadioButton.Size = new System.Drawing.Size(32, 17);
            this.sizeBytesRadioButton.TabIndex = 1;
            this.sizeBytesRadioButton.TabStop = true;
            this.sizeBytesRadioButton.Text = "B";
            this.sizeBytesRadioButton.UseVisualStyleBackColor = true;
            // 
            // sizeGbRadioButton
            // 
            this.sizeGbRadioButton.AutoSize = true;
            this.sizeGbRadioButton.Location = new System.Drawing.Point(212, 20);
            this.sizeGbRadioButton.Name = "sizeGbRadioButton";
            this.sizeGbRadioButton.Size = new System.Drawing.Size(40, 17);
            this.sizeGbRadioButton.TabIndex = 4;
            this.sizeGbRadioButton.TabStop = true;
            this.sizeGbRadioButton.Text = "GB";
            this.sizeGbRadioButton.UseVisualStyleBackColor = true;
            // 
            // sizeKbRadioButton
            // 
            this.sizeKbRadioButton.AutoSize = true;
            this.sizeKbRadioButton.Location = new System.Drawing.Point(118, 20);
            this.sizeKbRadioButton.Name = "sizeKbRadioButton";
            this.sizeKbRadioButton.Size = new System.Drawing.Size(39, 17);
            this.sizeKbRadioButton.TabIndex = 2;
            this.sizeKbRadioButton.TabStop = true;
            this.sizeKbRadioButton.Text = "KB";
            this.sizeKbRadioButton.UseVisualStyleBackColor = true;
            // 
            // sizeMbRadioButton
            // 
            this.sizeMbRadioButton.AutoSize = true;
            this.sizeMbRadioButton.Location = new System.Drawing.Point(165, 20);
            this.sizeMbRadioButton.Name = "sizeMbRadioButton";
            this.sizeMbRadioButton.Size = new System.Drawing.Size(41, 17);
            this.sizeMbRadioButton.TabIndex = 3;
            this.sizeMbRadioButton.TabStop = true;
            this.sizeMbRadioButton.Text = "MB";
            this.sizeMbRadioButton.UseVisualStyleBackColor = true;
            // 
            // sizeAutoRadioButton
            // 
            this.sizeAutoRadioButton.AutoSize = true;
            this.sizeAutoRadioButton.Location = new System.Drawing.Point(18, 20);
            this.sizeAutoRadioButton.Name = "sizeAutoRadioButton";
            this.sizeAutoRadioButton.Size = new System.Drawing.Size(47, 17);
            this.sizeAutoRadioButton.TabIndex = 0;
            this.sizeAutoRadioButton.TabStop = true;
            this.sizeAutoRadioButton.Text = "Auto";
            this.sizeAutoRadioButton.UseVisualStyleBackColor = true;
            // 
            // fontGroupBox
            // 
            this.fontGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fontGroupBox.Controls.Add(this.useDefaultFontButton);
            this.fontGroupBox.Controls.Add(this.setInterfaceFontButton);
            this.fontGroupBox.Location = new System.Drawing.Point(3, 264);
            this.fontGroupBox.Name = "fontGroupBox";
            this.fontGroupBox.Size = new System.Drawing.Size(308, 49);
            this.fontGroupBox.TabIndex = 4;
            this.fontGroupBox.TabStop = false;
            this.fontGroupBox.Text = "Font";
            // 
            // useDefaultFontButton
            // 
            this.useDefaultFontButton.Location = new System.Drawing.Point(157, 20);
            this.useDefaultFontButton.Name = "useDefaultFontButton";
            this.useDefaultFontButton.Size = new System.Drawing.Size(145, 23);
            this.useDefaultFontButton.TabIndex = 1;
            this.useDefaultFontButton.Text = "Use Default Font";
            this.useDefaultFontButton.UseVisualStyleBackColor = true;
            this.useDefaultFontButton.Click += new System.EventHandler(this.useDefaultFontButton_Click);
            // 
            // setInterfaceFontButton
            // 
            this.setInterfaceFontButton.Location = new System.Drawing.Point(6, 20);
            this.setInterfaceFontButton.Name = "setInterfaceFontButton";
            this.setInterfaceFontButton.Size = new System.Drawing.Size(145, 23);
            this.setInterfaceFontButton.TabIndex = 0;
            this.setInterfaceFontButton.Text = "Set Interface Font...";
            this.setInterfaceFontButton.UseVisualStyleBackColor = true;
            this.setInterfaceFontButton.Click += new System.EventHandler(this.setInterfaceFontButton_Click);
            // 
            // othersPanel
            // 
            this.othersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.othersPanel.Controls.Add(this.wgetConsolesGroupBox);
            this.othersPanel.Controls.Add(this.logOnGroupBox);
            this.othersPanel.Controls.Add(this.proxiesGroupBox);
            this.othersPanel.Controls.Add(this.newJobGroupBox);
            this.othersPanel.Location = new System.Drawing.Point(132, 6);
            this.othersPanel.Name = "othersPanel";
            this.othersPanel.Size = new System.Drawing.Size(314, 329);
            this.othersPanel.TabIndex = 4;
            // 
            // wgetConsolesGroupBox
            // 
            this.wgetConsolesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wgetConsolesGroupBox.Controls.Add(this.hideConsolesCheckBox);
            this.wgetConsolesGroupBox.Controls.Add(this.redirectOutputsCheckBox);
            this.wgetConsolesGroupBox.Location = new System.Drawing.Point(3, 233);
            this.wgetConsolesGroupBox.Name = "wgetConsolesGroupBox";
            this.wgetConsolesGroupBox.Size = new System.Drawing.Size(308, 43);
            this.wgetConsolesGroupBox.TabIndex = 3;
            this.wgetConsolesGroupBox.TabStop = false;
            this.wgetConsolesGroupBox.Text = "Wget consoles";
            // 
            // hideConsolesCheckBox
            // 
            this.hideConsolesCheckBox.AutoSize = true;
            this.hideConsolesCheckBox.Location = new System.Drawing.Point(6, 20);
            this.hideConsolesCheckBox.Name = "hideConsolesCheckBox";
            this.hideConsolesCheckBox.Size = new System.Drawing.Size(93, 17);
            this.hideConsolesCheckBox.TabIndex = 0;
            this.hideConsolesCheckBox.Text = "Hide consoles";
            this.hideConsolesCheckBox.UseVisualStyleBackColor = true;
            this.hideConsolesCheckBox.CheckedChanged += new System.EventHandler(this.hideConsoleCheckBox_CheckedChanged);
            // 
            // redirectOutputsCheckBox
            // 
            this.redirectOutputsCheckBox.AutoSize = true;
            this.redirectOutputsCheckBox.Location = new System.Drawing.Point(156, 20);
            this.redirectOutputsCheckBox.Name = "redirectOutputsCheckBox";
            this.redirectOutputsCheckBox.Size = new System.Drawing.Size(104, 17);
            this.redirectOutputsCheckBox.TabIndex = 1;
            this.redirectOutputsCheckBox.Text = "Redirect outputs";
            this.redirectOutputsCheckBox.UseVisualStyleBackColor = true;
            // 
            // prefCatListBox
            // 
            this.prefCatListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prefCatListBox.FormattingEnabled = true;
            this.prefCatListBox.IntegralHeight = false;
            this.prefCatListBox.Items.AddRange(new object[] {
            "General",
            "Interface",
            "Sounds",
            "Others"});
            this.prefCatListBox.Location = new System.Drawing.Point(0, 0);
            this.prefCatListBox.Name = "prefCatListBox";
            this.prefCatListBox.Size = new System.Drawing.Size(120, 329);
            this.prefCatListBox.TabIndex = 0;
            this.prefCatListBox.SelectedIndexChanged += new System.EventHandler(this.prefCatListBox_SelectedIndexChanged);
            // 
            // soundsPanel
            // 
            this.soundsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.soundsPanel.Controls.Add(this.systemSoundsGroupBox);
            this.soundsPanel.Location = new System.Drawing.Point(132, 6);
            this.soundsPanel.Name = "soundsPanel";
            this.soundsPanel.Size = new System.Drawing.Size(314, 329);
            this.soundsPanel.TabIndex = 3;
            // 
            // systemSoundsGroupBox
            // 
            this.systemSoundsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.systemSoundsGroupBox.Controls.Add(this.allDownloadsFinishedSoundCheckBox);
            this.systemSoundsGroupBox.Controls.Add(this.downloadFinishedSoundCheckBox);
            this.systemSoundsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.systemSoundsGroupBox.Name = "systemSoundsGroupBox";
            this.systemSoundsGroupBox.Size = new System.Drawing.Size(308, 66);
            this.systemSoundsGroupBox.TabIndex = 0;
            this.systemSoundsGroupBox.TabStop = false;
            this.systemSoundsGroupBox.Text = "System sounds";
            // 
            // allDownloadsFinishedSoundCheckBox
            // 
            this.allDownloadsFinishedSoundCheckBox.AutoSize = true;
            this.allDownloadsFinishedSoundCheckBox.Location = new System.Drawing.Point(6, 43);
            this.allDownloadsFinishedSoundCheckBox.Name = "allDownloadsFinishedSoundCheckBox";
            this.allDownloadsFinishedSoundCheckBox.Size = new System.Drawing.Size(267, 17);
            this.allDownloadsFinishedSoundCheckBox.TabIndex = 1;
            this.allDownloadsFinishedSoundCheckBox.Text = "Play sound when all downloads have been finished";
            this.allDownloadsFinishedSoundCheckBox.UseVisualStyleBackColor = true;
            // 
            // downloadFinishedSoundCheckBox
            // 
            this.downloadFinishedSoundCheckBox.AutoSize = true;
            this.downloadFinishedSoundCheckBox.Location = new System.Drawing.Point(6, 20);
            this.downloadFinishedSoundCheckBox.Name = "downloadFinishedSoundCheckBox";
            this.downloadFinishedSoundCheckBox.Size = new System.Drawing.Size(269, 17);
            this.downloadFinishedSoundCheckBox.TabIndex = 0;
            this.downloadFinishedSoundCheckBox.Text = "Play sound when each download has been finished";
            this.downloadFinishedSoundCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.prefCatListBox);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 329);
            this.panel1.TabIndex = 0;
            // 
            // PreferencesDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(452, 370);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.soundsPanel);
            this.Controls.Add(this.othersPanel);
            this.Controls.Add(this.interfacePanel);
            this.Controls.Add(this.generalPanel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesDialog";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.PreferencesDialog_Load);
            this.queueGroupBox.ResumeLayout(false);
            this.queueGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxRunningJobsNumericUpDown)).EndInit();
            this.speedLimitGroupBox.ResumeLayout(false);
            this.speedLimitGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedLimitNumericUpDown)).EndInit();
            this.retryingGroupBox.ResumeLayout(false);
            this.retryingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retryAttemptsNumericUpDown)).EndInit();
            this.updatesGroupBox.ResumeLayout(false);
            this.updatesGroupBox.PerformLayout();
            this.logOnGroupBox.ResumeLayout(false);
            this.logOnGroupBox.PerformLayout();
            this.proxiesGroupBox.ResumeLayout(false);
            this.proxiesGroupBox.PerformLayout();
            this.newJobGroupBox.ResumeLayout(false);
            this.newJobGroupBox.PerformLayout();
            this.systemTrayGroupBox.ResumeLayout(false);
            this.systemTrayGroupBox.PerformLayout();
            this.translationsGroupBox.ResumeLayout(false);
            this.translationsGroupBox.PerformLayout();
            this.listViewGroupBox.ResumeLayout(false);
            this.listViewGroupBox.PerformLayout();
            this.generalPanel.ResumeLayout(false);
            this.interfacePanel.ResumeLayout(false);
            this.sizeUnitGroupBox.ResumeLayout(false);
            this.sizeUnitGroupBox.PerformLayout();
            this.fontGroupBox.ResumeLayout(false);
            this.othersPanel.ResumeLayout(false);
            this.wgetConsolesGroupBox.ResumeLayout(false);
            this.wgetConsolesGroupBox.PerformLayout();
            this.soundsPanel.ResumeLayout(false);
            this.systemSoundsGroupBox.ResumeLayout(false);
            this.systemSoundsGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label maxRunningJobsLabel;
        private System.Windows.Forms.GroupBox queueGroupBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown maxRunningJobsNumericUpDown;
        private System.Windows.Forms.GroupBox retryingGroupBox;
        private System.Windows.Forms.CheckBox retryCheckBox;
        private System.Windows.Forms.NumericUpDown retryAttemptsNumericUpDown;
        private System.Windows.Forms.Label retryAttemptsLabel;
        private System.Windows.Forms.ComboBox timeBetweenRetryAttemptsComboBox;
        private System.Windows.Forms.Label timeBetweenRetryAttemptsLabel;
        private System.Windows.Forms.Label retryNoticeLabel;
        private System.Windows.Forms.Label speedLimitLabel2;
        private System.Windows.Forms.NumericUpDown speedLimitNumericUpDown;
        private System.Windows.Forms.Label speedLimitLabel1;
        private System.Windows.Forms.GroupBox speedLimitGroupBox;
        private System.Windows.Forms.CheckBox checkForUpdatesCheckBox;
        private System.Windows.Forms.GroupBox updatesGroupBox;
        private System.Windows.Forms.ComboBox checkForUpdatesComboBox;
        private System.Windows.Forms.GroupBox proxiesGroupBox;
        private System.Windows.Forms.TextBox httpProxyTextBox;
        private System.Windows.Forms.Label httpProxyLabel;
        private System.Windows.Forms.TextBox ftpProxyTextBox;
        private System.Windows.Forms.Label ftpProxyLabel;
        private System.Windows.Forms.TextBox noProxyTextBox;
        private System.Windows.Forms.Label noProxyLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label httpsProxyLabel;
        private System.Windows.Forms.TextBox httpsProxyTextBox;
        private System.Windows.Forms.GroupBox logOnGroupBox;
        private System.Windows.Forms.CheckBox runWhenLogOnCheckBox;
        private System.Windows.Forms.GroupBox newJobGroupBox;
        private System.Windows.Forms.CheckBox noPromptOnNewJobCheckBox;
        private System.Windows.Forms.GroupBox systemTrayGroupBox;
        private System.Windows.Forms.CheckBox closeToTrayCheckBox;
        private System.Windows.Forms.CheckBox minimizeToTrayCheckBox;
        private System.Windows.Forms.CheckBox showBalloonTipCheckBox;
        private System.Windows.Forms.CheckBox alwaysShowTrayIconCheckBox;
        private System.Windows.Forms.GroupBox translationsGroupBox;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.GroupBox listViewGroupBox;
        private System.Windows.Forms.TextBox promptWhenOpenFileTypesTextBox;
        private System.Windows.Forms.Label promptWhenOpenFileTypesLabel;
        private System.Windows.Forms.CheckBox listViewDoubleClickOpenCheckBox;
        private System.Windows.Forms.Panel generalPanel;
        private System.Windows.Forms.Panel interfacePanel;
        private System.Windows.Forms.Panel othersPanel;
        private System.Windows.Forms.ListBox prefCatListBox;
        private System.Windows.Forms.GroupBox fontGroupBox;
        private System.Windows.Forms.Button setInterfaceFontButton;
        private System.Windows.Forms.Button useDefaultFontButton;
        private System.Windows.Forms.RadioButton sizeGbRadioButton;
        private System.Windows.Forms.RadioButton sizeMbRadioButton;
        private System.Windows.Forms.RadioButton sizeKbRadioButton;
        private System.Windows.Forms.RadioButton sizeAutoRadioButton;
        private System.Windows.Forms.GroupBox wgetConsolesGroupBox;
        private System.Windows.Forms.CheckBox hideConsolesCheckBox;
        private System.Windows.Forms.CheckBox redirectOutputsCheckBox;
        private System.Windows.Forms.GroupBox sizeUnitGroupBox;
        private System.Windows.Forms.RadioButton sizeTbRadioButton;
        private System.Windows.Forms.RadioButton sizeBytesRadioButton;
        private System.Windows.Forms.Panel soundsPanel;
        private System.Windows.Forms.GroupBox systemSoundsGroupBox;
        private System.Windows.Forms.CheckBox allDownloadsFinishedSoundCheckBox;
        private System.Windows.Forms.CheckBox downloadFinishedSoundCheckBox;
        private System.Windows.Forms.Panel panel1;
    }
}