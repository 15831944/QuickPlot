namespace VisualWget
{
    partial class MainForm
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.newToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.newMultipleToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.editToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.removeToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.sep1ToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.startToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.stopToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.openContainingFolderToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.sep2ToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.moveUpToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.moveDownToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.sep3ToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.defaultDownloadOptionsToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.preferencesToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.speedLimitToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.jobsListViewContextMenu = new System.Windows.Forms.ContextMenu();
            this.jlv_j_openContainingFolderMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_sep1MenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_startMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_stopMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_sep2MenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_moveUpMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_moveDownMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_sep3MenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_removeMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_sep4MenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_exportMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_exportWgetBatchFileMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_sep5MenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_editMultipleMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_setDownloadDirectoryMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_setHttpRefererMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_j_editMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_sep1MenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_h_nameMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_h_numMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_h_sizeMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_h_doneMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_h_statusMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_h_speedMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_h_etaMenuItem = new System.Windows.Forms.MenuItem();
            this.jlv_h_noteMenuItem = new System.Windows.Forms.MenuItem();
            this.trayContextMenu = new System.Windows.Forms.ContextMenu();
            this.t_speedLimitMenuItem = new System.Windows.Forms.MenuItem();
            this.t_detectClipboardMenuItem = new System.Windows.Forms.MenuItem();
            this.t_sep1MenuItem = new System.Windows.Forms.MenuItem();
            this.t_quitMenuItem = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.jobsMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsNewMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsNewMultipleMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsEditMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsRemoveMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsCleanupMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsSep1MenuItem = new System.Windows.Forms.MenuItem();
            this.jobsStartMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsStopMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsOpenContainingFolderMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsSep2MenuItem = new System.Windows.Forms.MenuItem();
            this.jobsMoveUpMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsMoveDownMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsSep3MenuItem = new System.Windows.Forms.MenuItem();
            this.jobsExportMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsExportWgetBatchFileMenuItem = new System.Windows.Forms.MenuItem();
            this.jobsSep4MenuItem = new System.Windows.Forms.MenuItem();
            this.jobsQuitMenuItem = new System.Windows.Forms.MenuItem();
            this.viewMenuItem = new System.Windows.Forms.MenuItem();
            this.viewToolbarMenuItem = new System.Windows.Forms.MenuItem();
            this.viewLogMenuItem = new System.Windows.Forms.MenuItem();
            this.viewStatusBarMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsPreferencesMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsSep1MenuItem = new System.Windows.Forms.MenuItem();
            this.toolsDefaultDownloadOptionsMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsSep2MenuItem = new System.Windows.Forms.MenuItem();
            this.toolsComputeMD5HashMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsComputeSHA1HashMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsSep3MenuItem = new System.Windows.Forms.MenuItem();
            this.toolsAutoRemoveFinishedJobMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedDisabledMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedSep1MenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedQuitMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedStandByMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedHibernateMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedTurnOffMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedRestartMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedLogOffMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedLockComputerMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedSep2MenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedFreezeMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsWhenDownloadsFinishedCancelableMenuItem = new System.Windows.Forms.MenuItem();
            this.helpMenuItem = new System.Windows.Forms.MenuItem();
            this.helpDiscussionGroupMenuItem = new System.Windows.Forms.MenuItem();
            this.helpWebsiteMenuItem = new System.Windows.Forms.MenuItem();
            this.helpSep1MenuItem = new System.Windows.Forms.MenuItem();
            this.helpCheckForUpdatesMenuItem = new System.Windows.Forms.MenuItem();
            this.helpSep2MenuItem = new System.Windows.Forms.MenuItem();
            this.helpAboutVisualWgetMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "VisualWget";
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.AutoSize = false;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.newToolBarButton,
            this.newMultipleToolBarButton,
            this.editToolBarButton,
            this.removeToolBarButton,
            this.sep1ToolBarButton,
            this.startToolBarButton,
            this.stopToolBarButton,
            this.openContainingFolderToolBarButton,
            this.sep2ToolBarButton,
            this.moveUpToolBarButton,
            this.moveDownToolBarButton,
            this.sep3ToolBarButton,
            this.defaultDownloadOptionsToolBarButton,
            this.preferencesToolBarButton,
            this.speedLimitToolBarButton});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Margin = new System.Windows.Forms.Padding(4);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(738, 31);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.Wrappable = false;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            this.toolBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolBar1_MouseDown);
            // 
            // newToolBarButton
            // 
            this.newToolBarButton.ImageIndex = 0;
            this.newToolBarButton.Name = "newToolBarButton";
            this.newToolBarButton.ToolTipText = "New";
            // 
            // newMultipleToolBarButton
            // 
            this.newMultipleToolBarButton.ImageIndex = 10;
            this.newMultipleToolBarButton.Name = "newMultipleToolBarButton";
            this.newMultipleToolBarButton.ToolTipText = "New Multiple";
            // 
            // editToolBarButton
            // 
            this.editToolBarButton.ImageIndex = 1;
            this.editToolBarButton.Name = "editToolBarButton";
            this.editToolBarButton.ToolTipText = "Edit";
            // 
            // removeToolBarButton
            // 
            this.removeToolBarButton.ImageIndex = 2;
            this.removeToolBarButton.Name = "removeToolBarButton";
            this.removeToolBarButton.ToolTipText = "Remove";
            // 
            // sep1ToolBarButton
            // 
            this.sep1ToolBarButton.Name = "sep1ToolBarButton";
            this.sep1ToolBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // startToolBarButton
            // 
            this.startToolBarButton.ImageIndex = 3;
            this.startToolBarButton.Name = "startToolBarButton";
            this.startToolBarButton.ToolTipText = "Start";
            // 
            // stopToolBarButton
            // 
            this.stopToolBarButton.ImageIndex = 4;
            this.stopToolBarButton.Name = "stopToolBarButton";
            this.stopToolBarButton.ToolTipText = "Stop";
            // 
            // openContainingFolderToolBarButton
            // 
            this.openContainingFolderToolBarButton.ImageIndex = 5;
            this.openContainingFolderToolBarButton.Name = "openContainingFolderToolBarButton";
            this.openContainingFolderToolBarButton.ToolTipText = "Open Containing Folder";
            // 
            // sep2ToolBarButton
            // 
            this.sep2ToolBarButton.Name = "sep2ToolBarButton";
            this.sep2ToolBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // moveUpToolBarButton
            // 
            this.moveUpToolBarButton.ImageIndex = 6;
            this.moveUpToolBarButton.Name = "moveUpToolBarButton";
            this.moveUpToolBarButton.ToolTipText = "Move Up";
            // 
            // moveDownToolBarButton
            // 
            this.moveDownToolBarButton.ImageIndex = 7;
            this.moveDownToolBarButton.Name = "moveDownToolBarButton";
            this.moveDownToolBarButton.ToolTipText = "Move Down";
            // 
            // sep3ToolBarButton
            // 
            this.sep3ToolBarButton.Name = "sep3ToolBarButton";
            this.sep3ToolBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // defaultDownloadOptionsToolBarButton
            // 
            this.defaultDownloadOptionsToolBarButton.ImageIndex = 8;
            this.defaultDownloadOptionsToolBarButton.Name = "defaultDownloadOptionsToolBarButton";
            this.defaultDownloadOptionsToolBarButton.ToolTipText = "Default Download Options";
            // 
            // preferencesToolBarButton
            // 
            this.preferencesToolBarButton.ImageIndex = 9;
            this.preferencesToolBarButton.Name = "preferencesToolBarButton";
            this.preferencesToolBarButton.ToolTipText = "Preferences";
            // 
            // speedLimitToolBarButton
            // 
            this.speedLimitToolBarButton.ImageIndex = 11;
            this.speedLimitToolBarButton.Name = "speedLimitToolBarButton";
            this.speedLimitToolBarButton.ToolTipText = "Speed Limit";
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 463);
            this.statusBar1.Margin = new System.Windows.Forms.Padding(4);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanel2});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(738, 28);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.PanelClick += new System.Windows.Forms.StatusBarPanelClickEventHandler(this.statusBar1_PanelClick);
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 617;
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
            this.statusBarPanel2.Name = "statusBarPanel2";
            // 
            // jobsListViewContextMenu
            // 
            this.jobsListViewContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.jlv_j_openContainingFolderMenuItem,
            this.jlv_j_sep1MenuItem,
            this.jlv_j_startMenuItem,
            this.jlv_j_stopMenuItem,
            this.jlv_j_sep2MenuItem,
            this.jlv_j_moveUpMenuItem,
            this.jlv_j_moveDownMenuItem,
            this.jlv_j_sep3MenuItem,
            this.jlv_j_removeMenuItem,
            this.jlv_j_sep4MenuItem,
            this.jlv_j_exportMenuItem,
            this.jlv_j_sep5MenuItem,
            this.jlv_j_editMultipleMenuItem,
            this.jlv_j_editMenuItem,
            this.jlv_sep1MenuItem,
            this.jlv_h_nameMenuItem,
            this.jlv_h_numMenuItem,
            this.jlv_h_sizeMenuItem,
            this.jlv_h_doneMenuItem,
            this.jlv_h_statusMenuItem,
            this.jlv_h_speedMenuItem,
            this.jlv_h_etaMenuItem,
            this.jlv_h_noteMenuItem});
            this.jobsListViewContextMenu.Popup += new System.EventHandler(this.jobsListViewContextMenu_Popup);
            // 
            // jlv_j_openContainingFolderMenuItem
            // 
            this.jlv_j_openContainingFolderMenuItem.Index = 0;
            this.jlv_j_openContainingFolderMenuItem.Tag = "j";
            this.jlv_j_openContainingFolderMenuItem.Text = "Open Containing Folder";
            this.jlv_j_openContainingFolderMenuItem.Click += new System.EventHandler(this.jlv_j_openContainingFolderMenuItem_Click);
            // 
            // jlv_j_sep1MenuItem
            // 
            this.jlv_j_sep1MenuItem.Index = 1;
            this.jlv_j_sep1MenuItem.Tag = "j";
            this.jlv_j_sep1MenuItem.Text = "-";
            // 
            // jlv_j_startMenuItem
            // 
            this.jlv_j_startMenuItem.Index = 2;
            this.jlv_j_startMenuItem.Tag = "j";
            this.jlv_j_startMenuItem.Text = "Start";
            this.jlv_j_startMenuItem.Click += new System.EventHandler(this.jlv_j_startMenuItem_Click);
            // 
            // jlv_j_stopMenuItem
            // 
            this.jlv_j_stopMenuItem.Index = 3;
            this.jlv_j_stopMenuItem.Tag = "j";
            this.jlv_j_stopMenuItem.Text = "Stop";
            this.jlv_j_stopMenuItem.Click += new System.EventHandler(this.jlv_j_stopMenuItem_Click);
            // 
            // jlv_j_sep2MenuItem
            // 
            this.jlv_j_sep2MenuItem.Index = 4;
            this.jlv_j_sep2MenuItem.Tag = "j";
            this.jlv_j_sep2MenuItem.Text = "-";
            // 
            // jlv_j_moveUpMenuItem
            // 
            this.jlv_j_moveUpMenuItem.Index = 5;
            this.jlv_j_moveUpMenuItem.Tag = "j";
            this.jlv_j_moveUpMenuItem.Text = "Move Up";
            this.jlv_j_moveUpMenuItem.Click += new System.EventHandler(this.jlv_j_moveUpMenuItem_Click);
            // 
            // jlv_j_moveDownMenuItem
            // 
            this.jlv_j_moveDownMenuItem.Index = 6;
            this.jlv_j_moveDownMenuItem.Tag = "j";
            this.jlv_j_moveDownMenuItem.Text = "Move Down";
            this.jlv_j_moveDownMenuItem.Click += new System.EventHandler(this.jlv_j_moveDownMenuItem_Click);
            // 
            // jlv_j_sep3MenuItem
            // 
            this.jlv_j_sep3MenuItem.Index = 7;
            this.jlv_j_sep3MenuItem.Tag = "j";
            this.jlv_j_sep3MenuItem.Text = "-";
            // 
            // jlv_j_removeMenuItem
            // 
            this.jlv_j_removeMenuItem.Index = 8;
            this.jlv_j_removeMenuItem.Tag = "j";
            this.jlv_j_removeMenuItem.Text = "Remove...";
            this.jlv_j_removeMenuItem.Click += new System.EventHandler(this.jlv_j_removeMenuItem_Click);
            // 
            // jlv_j_sep4MenuItem
            // 
            this.jlv_j_sep4MenuItem.Index = 9;
            this.jlv_j_sep4MenuItem.Tag = "j";
            this.jlv_j_sep4MenuItem.Text = "-";
            // 
            // jlv_j_exportMenuItem
            // 
            this.jlv_j_exportMenuItem.Index = 10;
            this.jlv_j_exportMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.jlv_j_exportWgetBatchFileMenuItem});
            this.jlv_j_exportMenuItem.Tag = "j";
            this.jlv_j_exportMenuItem.Text = "Export";
            // 
            // jlv_j_exportWgetBatchFileMenuItem
            // 
            this.jlv_j_exportWgetBatchFileMenuItem.Index = 0;
            this.jlv_j_exportWgetBatchFileMenuItem.Tag = "j";
            this.jlv_j_exportWgetBatchFileMenuItem.Text = "Wget Batch File...";
            this.jlv_j_exportWgetBatchFileMenuItem.Click += new System.EventHandler(this.jlv_j_exportWgetBatchFileMenuItem_Click);
            // 
            // jlv_j_sep5MenuItem
            // 
            this.jlv_j_sep5MenuItem.Index = 11;
            this.jlv_j_sep5MenuItem.Tag = "j";
            this.jlv_j_sep5MenuItem.Text = "-";
            // 
            // jlv_j_editMultipleMenuItem
            // 
            this.jlv_j_editMultipleMenuItem.Index = 12;
            this.jlv_j_editMultipleMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.jlv_j_setDownloadDirectoryMenuItem,
            this.jlv_j_setHttpRefererMenuItem});
            this.jlv_j_editMultipleMenuItem.Tag = "j";
            this.jlv_j_editMultipleMenuItem.Text = "Edit Multiple";
            // 
            // jlv_j_setDownloadDirectoryMenuItem
            // 
            this.jlv_j_setDownloadDirectoryMenuItem.Index = 0;
            this.jlv_j_setDownloadDirectoryMenuItem.Tag = "j";
            this.jlv_j_setDownloadDirectoryMenuItem.Text = "Set Download Directory...";
            this.jlv_j_setDownloadDirectoryMenuItem.Click += new System.EventHandler(this.jlv_j_setDownloadDirectoryMenuItem_Click);
            // 
            // jlv_j_setHttpRefererMenuItem
            // 
            this.jlv_j_setHttpRefererMenuItem.Index = 1;
            this.jlv_j_setHttpRefererMenuItem.Tag = "j";
            this.jlv_j_setHttpRefererMenuItem.Text = "Set HTTP Referer...";
            this.jlv_j_setHttpRefererMenuItem.Click += new System.EventHandler(this.jlv_j_setHttpRefererMenuItem_Click);
            // 
            // jlv_j_editMenuItem
            // 
            this.jlv_j_editMenuItem.Index = 13;
            this.jlv_j_editMenuItem.Tag = "j";
            this.jlv_j_editMenuItem.Text = "Edit...";
            this.jlv_j_editMenuItem.Click += new System.EventHandler(this.jlv_j_editMenuItem_Click);
            // 
            // jlv_sep1MenuItem
            // 
            this.jlv_sep1MenuItem.Index = 14;
            this.jlv_sep1MenuItem.Text = "-";
            // 
            // jlv_h_nameMenuItem
            // 
            this.jlv_h_nameMenuItem.Index = 15;
            this.jlv_h_nameMenuItem.Tag = "h";
            this.jlv_h_nameMenuItem.Text = "Name";
            this.jlv_h_nameMenuItem.Click += new System.EventHandler(this.jlv_h_nameMenuItem_Click);
            // 
            // jlv_h_numMenuItem
            // 
            this.jlv_h_numMenuItem.Index = 16;
            this.jlv_h_numMenuItem.Tag = "h";
            this.jlv_h_numMenuItem.Text = "#";
            this.jlv_h_numMenuItem.Click += new System.EventHandler(this.jlv_h_numMenuItem_Click);
            // 
            // jlv_h_sizeMenuItem
            // 
            this.jlv_h_sizeMenuItem.Index = 17;
            this.jlv_h_sizeMenuItem.Tag = "h";
            this.jlv_h_sizeMenuItem.Text = "Size";
            this.jlv_h_sizeMenuItem.Click += new System.EventHandler(this.jlv_h_sizeMenuItem_Click);
            // 
            // jlv_h_doneMenuItem
            // 
            this.jlv_h_doneMenuItem.Index = 18;
            this.jlv_h_doneMenuItem.Tag = "h";
            this.jlv_h_doneMenuItem.Text = "Done";
            this.jlv_h_doneMenuItem.Click += new System.EventHandler(this.jlv_h_doneMenuItem_Click);
            // 
            // jlv_h_statusMenuItem
            // 
            this.jlv_h_statusMenuItem.Index = 19;
            this.jlv_h_statusMenuItem.Tag = "h";
            this.jlv_h_statusMenuItem.Text = "Status";
            this.jlv_h_statusMenuItem.Click += new System.EventHandler(this.jlv_h_statusMenuItem_Click);
            // 
            // jlv_h_speedMenuItem
            // 
            this.jlv_h_speedMenuItem.Index = 20;
            this.jlv_h_speedMenuItem.Tag = "h";
            this.jlv_h_speedMenuItem.Text = "Speed";
            this.jlv_h_speedMenuItem.Click += new System.EventHandler(this.jlv_h_speedMenuItem_Click);
            // 
            // jlv_h_etaMenuItem
            // 
            this.jlv_h_etaMenuItem.Index = 21;
            this.jlv_h_etaMenuItem.Tag = "h";
            this.jlv_h_etaMenuItem.Text = "ETA";
            this.jlv_h_etaMenuItem.Click += new System.EventHandler(this.jlv_h_etaMenuItem_Click);
            // 
            // jlv_h_noteMenuItem
            // 
            this.jlv_h_noteMenuItem.Index = 22;
            this.jlv_h_noteMenuItem.Tag = "h";
            this.jlv_h_noteMenuItem.Text = "Note";
            this.jlv_h_noteMenuItem.Click += new System.EventHandler(this.jlv_h_noteMenuItem_Click);
            // 
            // trayContextMenu
            // 
            this.trayContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.t_speedLimitMenuItem,
            this.t_detectClipboardMenuItem,
            this.t_sep1MenuItem,
            this.t_quitMenuItem});
            this.trayContextMenu.Popup += new System.EventHandler(this.trayContextMenu_Popup);
            // 
            // t_speedLimitMenuItem
            // 
            this.t_speedLimitMenuItem.Index = 0;
            this.t_speedLimitMenuItem.Text = "Speed Limit";
            // 
            // t_detectClipboardMenuItem
            // 
            this.t_detectClipboardMenuItem.Index = 1;
            this.t_detectClipboardMenuItem.Text = "Detect Clipboard";
            this.t_detectClipboardMenuItem.Click += new System.EventHandler(this.t_detectClipboardMenuItem_Click);
            // 
            // t_sep1MenuItem
            // 
            this.t_sep1MenuItem.Index = 2;
            this.t_sep1MenuItem.Text = "-";
            // 
            // t_quitMenuItem
            // 
            this.t_quitMenuItem.Index = 3;
            this.t_quitMenuItem.Text = "Quit";
            this.t_quitMenuItem.Click += new System.EventHandler(this.t_quitMenuItem_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.jobsMenuItem,
            this.viewMenuItem,
            this.toolsMenuItem,
            this.helpMenuItem,
            this.menuItem1});
            // 
            // jobsMenuItem
            // 
            this.jobsMenuItem.Index = 0;
            this.jobsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.jobsNewMenuItem,
            this.jobsNewMultipleMenuItem,
            this.jobsEditMenuItem,
            this.jobsRemoveMenuItem,
            this.jobsCleanupMenuItem,
            this.jobsSep1MenuItem,
            this.jobsStartMenuItem,
            this.jobsStopMenuItem,
            this.jobsOpenContainingFolderMenuItem,
            this.jobsSep2MenuItem,
            this.jobsMoveUpMenuItem,
            this.jobsMoveDownMenuItem,
            this.jobsSep3MenuItem,
            this.jobsExportMenuItem,
            this.jobsSep4MenuItem,
            this.jobsQuitMenuItem});
            this.jobsMenuItem.Text = "Jobs";
            // 
            // jobsNewMenuItem
            // 
            this.jobsNewMenuItem.Index = 0;
            this.jobsNewMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.jobsNewMenuItem.Text = "New...";
            this.jobsNewMenuItem.Click += new System.EventHandler(this.jobsNewMenuItem_Click);
            // 
            // jobsNewMultipleMenuItem
            // 
            this.jobsNewMultipleMenuItem.Index = 1;
            this.jobsNewMultipleMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this.jobsNewMultipleMenuItem.Text = "New Multiple...";
            this.jobsNewMultipleMenuItem.Click += new System.EventHandler(this.jobsNewMultipleMenuItem_Click);
            // 
            // jobsEditMenuItem
            // 
            this.jobsEditMenuItem.Index = 2;
            this.jobsEditMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.jobsEditMenuItem.Text = "Edit...";
            this.jobsEditMenuItem.Click += new System.EventHandler(this.jobsEditMenuItem_Click);
            // 
            // jobsRemoveMenuItem
            // 
            this.jobsRemoveMenuItem.Index = 3;
            this.jobsRemoveMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.jobsRemoveMenuItem.Text = "Remove...";
            this.jobsRemoveMenuItem.Click += new System.EventHandler(this.jobsRemoveMenuItem_Click);
            // 
            // jobsCleanupMenuItem
            // 
            this.jobsCleanupMenuItem.Index = 4;
            this.jobsCleanupMenuItem.Text = "Cleanup...";
            this.jobsCleanupMenuItem.Click += new System.EventHandler(this.jobsCleanupMenuItem_Click);
            // 
            // jobsSep1MenuItem
            // 
            this.jobsSep1MenuItem.Index = 5;
            this.jobsSep1MenuItem.Text = "-";
            // 
            // jobsStartMenuItem
            // 
            this.jobsStartMenuItem.Index = 6;
            this.jobsStartMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.jobsStartMenuItem.Text = "Start";
            this.jobsStartMenuItem.Click += new System.EventHandler(this.jobsStartMenuItem_Click);
            // 
            // jobsStopMenuItem
            // 
            this.jobsStopMenuItem.Index = 7;
            this.jobsStopMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this.jobsStopMenuItem.Text = "Stop";
            this.jobsStopMenuItem.Click += new System.EventHandler(this.jobsStopMenuItem_Click);
            // 
            // jobsOpenContainingFolderMenuItem
            // 
            this.jobsOpenContainingFolderMenuItem.Index = 8;
            this.jobsOpenContainingFolderMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.jobsOpenContainingFolderMenuItem.Text = "Open Containing Folder";
            this.jobsOpenContainingFolderMenuItem.Click += new System.EventHandler(this.jobsOpenContainingFolderMenuItem_Click);
            // 
            // jobsSep2MenuItem
            // 
            this.jobsSep2MenuItem.Index = 9;
            this.jobsSep2MenuItem.Text = "-";
            // 
            // jobsMoveUpMenuItem
            // 
            this.jobsMoveUpMenuItem.Index = 10;
            this.jobsMoveUpMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
            this.jobsMoveUpMenuItem.Text = "Move Up";
            this.jobsMoveUpMenuItem.Click += new System.EventHandler(this.jobsMoveUpMenuItem_Click);
            // 
            // jobsMoveDownMenuItem
            // 
            this.jobsMoveDownMenuItem.Index = 11;
            this.jobsMoveDownMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this.jobsMoveDownMenuItem.Text = "Move Down";
            this.jobsMoveDownMenuItem.Click += new System.EventHandler(this.jobsMoveDownMenuItem_Click);
            // 
            // jobsSep3MenuItem
            // 
            this.jobsSep3MenuItem.Index = 12;
            this.jobsSep3MenuItem.Text = "-";
            // 
            // jobsExportMenuItem
            // 
            this.jobsExportMenuItem.Index = 13;
            this.jobsExportMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.jobsExportWgetBatchFileMenuItem});
            this.jobsExportMenuItem.Text = "Export";
            // 
            // jobsExportWgetBatchFileMenuItem
            // 
            this.jobsExportWgetBatchFileMenuItem.Index = 0;
            this.jobsExportWgetBatchFileMenuItem.Text = "Wget Batch File...";
            this.jobsExportWgetBatchFileMenuItem.Click += new System.EventHandler(this.jobsExportWgetBatchFileMenuItem_Click);
            // 
            // jobsSep4MenuItem
            // 
            this.jobsSep4MenuItem.Index = 14;
            this.jobsSep4MenuItem.Text = "-";
            // 
            // jobsQuitMenuItem
            // 
            this.jobsQuitMenuItem.Index = 15;
            this.jobsQuitMenuItem.Text = "Quit";
            this.jobsQuitMenuItem.Click += new System.EventHandler(this.jobsQuitMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.Index = 1;
            this.viewMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.viewToolbarMenuItem,
            this.viewLogMenuItem,
            this.viewStatusBarMenuItem});
            this.viewMenuItem.Text = "View";
            // 
            // viewToolbarMenuItem
            // 
            this.viewToolbarMenuItem.Checked = true;
            this.viewToolbarMenuItem.Index = 0;
            this.viewToolbarMenuItem.Text = "Toolbar";
            this.viewToolbarMenuItem.Click += new System.EventHandler(this.viewToolbarMenuItem_Click);
            // 
            // viewLogMenuItem
            // 
            this.viewLogMenuItem.Checked = true;
            this.viewLogMenuItem.Index = 1;
            this.viewLogMenuItem.Text = "Log";
            this.viewLogMenuItem.Click += new System.EventHandler(this.viewLogMenuItem_Click);
            // 
            // viewStatusBarMenuItem
            // 
            this.viewStatusBarMenuItem.Checked = true;
            this.viewStatusBarMenuItem.Index = 2;
            this.viewStatusBarMenuItem.Text = "Status Bar";
            this.viewStatusBarMenuItem.Click += new System.EventHandler(this.viewStatusBarMenuItem_Click);
            // 
            // toolsMenuItem
            // 
            this.toolsMenuItem.Index = 2;
            this.toolsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.toolsPreferencesMenuItem,
            this.toolsSep1MenuItem,
            this.toolsDefaultDownloadOptionsMenuItem,
            this.toolsSep2MenuItem,
            this.toolsComputeMD5HashMenuItem,
            this.toolsComputeSHA1HashMenuItem,
            this.toolsSep3MenuItem,
            this.toolsAutoRemoveFinishedJobMenuItem,
            this.toolsWhenDownloadsFinishedMenuItem});
            this.toolsMenuItem.Text = "Tools";
            // 
            // toolsPreferencesMenuItem
            // 
            this.toolsPreferencesMenuItem.Index = 0;
            this.toolsPreferencesMenuItem.Text = "Preferences...";
            this.toolsPreferencesMenuItem.Click += new System.EventHandler(this.toolsPreferencesMenuItem_Click);
            // 
            // toolsSep1MenuItem
            // 
            this.toolsSep1MenuItem.Index = 1;
            this.toolsSep1MenuItem.Text = "-";
            // 
            // toolsDefaultDownloadOptionsMenuItem
            // 
            this.toolsDefaultDownloadOptionsMenuItem.Index = 2;
            this.toolsDefaultDownloadOptionsMenuItem.Text = "Default Download Options...";
            this.toolsDefaultDownloadOptionsMenuItem.Click += new System.EventHandler(this.toolsDefaultDownloadOptionsMenuItem_Click);
            // 
            // toolsSep2MenuItem
            // 
            this.toolsSep2MenuItem.Index = 3;
            this.toolsSep2MenuItem.Text = "-";
            // 
            // toolsComputeMD5HashMenuItem
            // 
            this.toolsComputeMD5HashMenuItem.Index = 4;
            this.toolsComputeMD5HashMenuItem.Text = "Compute MD5 Hash...";
            this.toolsComputeMD5HashMenuItem.Click += new System.EventHandler(this.toolsComputeMD5HashMenuItem_Click);
            // 
            // toolsComputeSHA1HashMenuItem
            // 
            this.toolsComputeSHA1HashMenuItem.Index = 5;
            this.toolsComputeSHA1HashMenuItem.Text = "Compute SHA1 Hash...";
            this.toolsComputeSHA1HashMenuItem.Click += new System.EventHandler(this.toolsComputeSHA1HashMenuItem_Click);
            // 
            // toolsSep3MenuItem
            // 
            this.toolsSep3MenuItem.Index = 6;
            this.toolsSep3MenuItem.Text = "-";
            // 
            // toolsAutoRemoveFinishedJobMenuItem
            // 
            this.toolsAutoRemoveFinishedJobMenuItem.Index = 7;
            this.toolsAutoRemoveFinishedJobMenuItem.Text = "Auto Remove Finished Job";
            this.toolsAutoRemoveFinishedJobMenuItem.Click += new System.EventHandler(this.toolsAutoRemoveFinishedJobMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedMenuItem
            // 
            this.toolsWhenDownloadsFinishedMenuItem.Index = 8;
            this.toolsWhenDownloadsFinishedMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.toolsWhenDownloadsFinishedDisabledMenuItem,
            this.toolsWhenDownloadsFinishedSep1MenuItem,
            this.toolsWhenDownloadsFinishedQuitMenuItem,
            this.toolsWhenDownloadsFinishedStandByMenuItem,
            this.toolsWhenDownloadsFinishedHibernateMenuItem,
            this.toolsWhenDownloadsFinishedTurnOffMenuItem,
            this.toolsWhenDownloadsFinishedRestartMenuItem,
            this.toolsWhenDownloadsFinishedLogOffMenuItem,
            this.toolsWhenDownloadsFinishedLockComputerMenuItem,
            this.toolsWhenDownloadsFinishedSep2MenuItem,
            this.toolsWhenDownloadsFinishedFreezeMenuItem,
            this.toolsWhenDownloadsFinishedCancelableMenuItem});
            this.toolsWhenDownloadsFinishedMenuItem.Text = "When Downloads Finished";
            this.toolsWhenDownloadsFinishedMenuItem.Popup += new System.EventHandler(this.toolsWhenDownloadsFinishedMenuItem_Popup);
            // 
            // toolsWhenDownloadsFinishedDisabledMenuItem
            // 
            this.toolsWhenDownloadsFinishedDisabledMenuItem.Index = 0;
            this.toolsWhenDownloadsFinishedDisabledMenuItem.RadioCheck = true;
            this.toolsWhenDownloadsFinishedDisabledMenuItem.Text = "Disable";
            this.toolsWhenDownloadsFinishedDisabledMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedDisabledMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedSep1MenuItem
            // 
            this.toolsWhenDownloadsFinishedSep1MenuItem.Index = 1;
            this.toolsWhenDownloadsFinishedSep1MenuItem.Text = "-";
            // 
            // toolsWhenDownloadsFinishedQuitMenuItem
            // 
            this.toolsWhenDownloadsFinishedQuitMenuItem.Index = 2;
            this.toolsWhenDownloadsFinishedQuitMenuItem.RadioCheck = true;
            this.toolsWhenDownloadsFinishedQuitMenuItem.Text = "Quit";
            this.toolsWhenDownloadsFinishedQuitMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedQuitMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedStandByMenuItem
            // 
            this.toolsWhenDownloadsFinishedStandByMenuItem.Index = 3;
            this.toolsWhenDownloadsFinishedStandByMenuItem.RadioCheck = true;
            this.toolsWhenDownloadsFinishedStandByMenuItem.Text = "Stand By";
            this.toolsWhenDownloadsFinishedStandByMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedStandByMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedHibernateMenuItem
            // 
            this.toolsWhenDownloadsFinishedHibernateMenuItem.Index = 4;
            this.toolsWhenDownloadsFinishedHibernateMenuItem.RadioCheck = true;
            this.toolsWhenDownloadsFinishedHibernateMenuItem.Text = "Hibernate";
            this.toolsWhenDownloadsFinishedHibernateMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedHibernateMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedTurnOffMenuItem
            // 
            this.toolsWhenDownloadsFinishedTurnOffMenuItem.Index = 5;
            this.toolsWhenDownloadsFinishedTurnOffMenuItem.RadioCheck = true;
            this.toolsWhenDownloadsFinishedTurnOffMenuItem.Text = "Turn Off";
            this.toolsWhenDownloadsFinishedTurnOffMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedTurnOffMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedRestartMenuItem
            // 
            this.toolsWhenDownloadsFinishedRestartMenuItem.Index = 6;
            this.toolsWhenDownloadsFinishedRestartMenuItem.RadioCheck = true;
            this.toolsWhenDownloadsFinishedRestartMenuItem.Text = "Restart";
            this.toolsWhenDownloadsFinishedRestartMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedRestartMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedLogOffMenuItem
            // 
            this.toolsWhenDownloadsFinishedLogOffMenuItem.Index = 7;
            this.toolsWhenDownloadsFinishedLogOffMenuItem.RadioCheck = true;
            this.toolsWhenDownloadsFinishedLogOffMenuItem.Text = "Log Off";
            this.toolsWhenDownloadsFinishedLogOffMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedLogOffMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedLockComputerMenuItem
            // 
            this.toolsWhenDownloadsFinishedLockComputerMenuItem.Index = 8;
            this.toolsWhenDownloadsFinishedLockComputerMenuItem.RadioCheck = true;
            this.toolsWhenDownloadsFinishedLockComputerMenuItem.Text = "Lock Computer";
            this.toolsWhenDownloadsFinishedLockComputerMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedLockComputerMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedSep2MenuItem
            // 
            this.toolsWhenDownloadsFinishedSep2MenuItem.Index = 9;
            this.toolsWhenDownloadsFinishedSep2MenuItem.Text = "-";
            // 
            // toolsWhenDownloadsFinishedFreezeMenuItem
            // 
            this.toolsWhenDownloadsFinishedFreezeMenuItem.Index = 10;
            this.toolsWhenDownloadsFinishedFreezeMenuItem.Text = "Freeze";
            this.toolsWhenDownloadsFinishedFreezeMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedFreezeMenuItem_Click);
            // 
            // toolsWhenDownloadsFinishedCancelableMenuItem
            // 
            this.toolsWhenDownloadsFinishedCancelableMenuItem.Index = 11;
            this.toolsWhenDownloadsFinishedCancelableMenuItem.Text = "Cancelable";
            this.toolsWhenDownloadsFinishedCancelableMenuItem.Click += new System.EventHandler(this.toolsWhenDownloadsFinishedCancelableMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Index = 3;
            this.helpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.helpDiscussionGroupMenuItem,
            this.helpWebsiteMenuItem,
            this.helpSep1MenuItem,
            this.helpCheckForUpdatesMenuItem,
            this.helpSep2MenuItem,
            this.helpAboutVisualWgetMenuItem});
            this.helpMenuItem.Text = "Help";
            // 
            // helpDiscussionGroupMenuItem
            // 
            this.helpDiscussionGroupMenuItem.Index = 0;
            this.helpDiscussionGroupMenuItem.Text = "Discussion Group";
            this.helpDiscussionGroupMenuItem.Click += new System.EventHandler(this.helpDiscussionGroupMenuItem_Click);
            // 
            // helpWebsiteMenuItem
            // 
            this.helpWebsiteMenuItem.Index = 1;
            this.helpWebsiteMenuItem.Text = "Website";
            this.helpWebsiteMenuItem.Click += new System.EventHandler(this.helpWebsiteMenuItem_Click);
            // 
            // helpSep1MenuItem
            // 
            this.helpSep1MenuItem.Index = 2;
            this.helpSep1MenuItem.Text = "-";
            // 
            // helpCheckForUpdatesMenuItem
            // 
            this.helpCheckForUpdatesMenuItem.Index = 3;
            this.helpCheckForUpdatesMenuItem.Text = "Check for Updates";
            this.helpCheckForUpdatesMenuItem.Click += new System.EventHandler(this.helpCheckForUpdatesMenuItem_Click);
            // 
            // helpSep2MenuItem
            // 
            this.helpSep2MenuItem.Index = 4;
            this.helpSep2MenuItem.Text = "-";
            // 
            // helpAboutVisualWgetMenuItem
            // 
            this.helpAboutVisualWgetMenuItem.Index = 5;
            this.helpAboutVisualWgetMenuItem.Text = "About QuickPrint...";
            this.helpAboutVisualWgetMenuItem.Click += new System.EventHandler(this.helpAboutVisualWgetMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem10});
            this.menuItem1.Text = "Test";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6,
            this.menuItem7,
            this.menuItem8});
            this.menuItem3.Text = "AutoCAD";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Show Print Preview";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Draw";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.Text = "Visible";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.Text = "Select Object";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 4;
            this.menuItem7.Text = "Get Print Area";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 5;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9});
            this.menuItem8.Text = "Public Functions";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "Check Layer Exist";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem11,
            this.menuItem12,
            this.menuItem13});
            this.menuItem10.Text = "CTB";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 0;
            this.menuItem11.Text = "1. Read CTB";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 1;
            this.menuItem12.Text = "2. Recompress CTB";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.logTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 432);
            this.panel1.TabIndex = 1;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(738, 214);
            this.panel2.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 214);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(738, 4);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.logTextBox.DetectUrls = false;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logTextBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.logTextBox.HideSelection = false;
            this.logTextBox.Location = new System.Drawing.Point(0, 218);
            this.logTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.logTextBox.Size = new System.Drawing.Size(738, 214);
            this.logTextBox.TabIndex = 2;
            this.logTextBox.Text = "";
            this.logTextBox.WordWrap = false;
            this.logTextBox.Enter += new System.EventHandler(this.logTextBox_Enter);
            this.logTextBox.Leave += new System.EventHandler(this.logTextBox_Leave);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 2;
            this.menuItem13.Text = "PC3";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(738, 491);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.toolBar1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(20, 15);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "QuickPrint";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.VisibleChanged += new System.EventHandler(this.MainForm_VisibleChanged);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ContextMenu jobsListViewContextMenu;
        private System.Windows.Forms.MenuItem jlv_j_openContainingFolderMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_sep1MenuItem;
        private System.Windows.Forms.MenuItem jlv_j_startMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_stopMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_sep2MenuItem;
        private System.Windows.Forms.MenuItem jlv_j_moveUpMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_moveDownMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_sep3MenuItem;
        private System.Windows.Forms.MenuItem jlv_j_removeMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_editMenuItem;
        private System.Windows.Forms.ContextMenu trayContextMenu;
        private System.Windows.Forms.ToolBarButton newToolBarButton;
        private System.Windows.Forms.ToolBarButton editToolBarButton;
        private System.Windows.Forms.ToolBarButton removeToolBarButton;
        private System.Windows.Forms.ToolBarButton sep1ToolBarButton;
        private System.Windows.Forms.ToolBarButton startToolBarButton;
        private System.Windows.Forms.ToolBarButton stopToolBarButton;
        private System.Windows.Forms.ToolBarButton openContainingFolderToolBarButton;
        private System.Windows.Forms.ToolBarButton sep2ToolBarButton;
        private System.Windows.Forms.ToolBarButton moveUpToolBarButton;
        private System.Windows.Forms.ToolBarButton moveDownToolBarButton;
        private System.Windows.Forms.ToolBarButton sep3ToolBarButton;
        private System.Windows.Forms.ToolBarButton defaultDownloadOptionsToolBarButton;
        private System.Windows.Forms.ToolBarButton preferencesToolBarButton;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem jobsMenuItem;
        private System.Windows.Forms.MenuItem jobsNewMenuItem;
        private System.Windows.Forms.MenuItem jobsEditMenuItem;
        private System.Windows.Forms.MenuItem jobsRemoveMenuItem;
        private System.Windows.Forms.MenuItem jobsSep1MenuItem;
        private System.Windows.Forms.MenuItem jobsStartMenuItem;
        private System.Windows.Forms.MenuItem jobsStopMenuItem;
        private System.Windows.Forms.MenuItem jobsOpenContainingFolderMenuItem;
        private System.Windows.Forms.MenuItem jobsSep2MenuItem;
        private System.Windows.Forms.MenuItem jobsMoveUpMenuItem;
        private System.Windows.Forms.MenuItem jobsMoveDownMenuItem;
        private System.Windows.Forms.MenuItem jobsSep3MenuItem;
        private System.Windows.Forms.MenuItem jobsQuitMenuItem;
        private System.Windows.Forms.MenuItem viewMenuItem;
        private System.Windows.Forms.MenuItem viewToolbarMenuItem;
        private System.Windows.Forms.MenuItem viewStatusBarMenuItem;
        private System.Windows.Forms.MenuItem toolsMenuItem;
        private System.Windows.Forms.MenuItem toolsPreferencesMenuItem;
        private System.Windows.Forms.MenuItem toolsSep1MenuItem;
        private System.Windows.Forms.MenuItem toolsDefaultDownloadOptionsMenuItem;
        private System.Windows.Forms.MenuItem toolsSep2MenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedDisabledMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedSep1MenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedQuitMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedStandByMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedHibernateMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedRestartMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedTurnOffMenuItem;
        private System.Windows.Forms.MenuItem helpMenuItem;
        private System.Windows.Forms.MenuItem helpAboutVisualWgetMenuItem;
        private System.Windows.Forms.MenuItem helpCheckForUpdatesMenuItem;
        private System.Windows.Forms.MenuItem helpSep1MenuItem;
        private System.Windows.Forms.StatusBarPanel statusBarPanel1;
        private System.Windows.Forms.StatusBarPanel statusBarPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuItem toolsComputeMD5HashMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuItem toolsComputeSHA1HashMenuItem;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuItem viewLogMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_exportMenuItem;
        private System.Windows.Forms.MenuItem jobsExportMenuItem;
        private System.Windows.Forms.MenuItem jobsSep4MenuItem;
        private System.Windows.Forms.MenuItem jlv_j_sep4MenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuItem jobsCleanupMenuItem;
        private System.Windows.Forms.MenuItem jobsNewMultipleMenuItem;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.MenuItem jlv_j_sep5MenuItem;
        private System.Windows.Forms.MenuItem jlv_h_numMenuItem;
        private System.Windows.Forms.MenuItem jlv_h_sizeMenuItem;
        private System.Windows.Forms.MenuItem jlv_h_doneMenuItem;
        private System.Windows.Forms.MenuItem jlv_h_statusMenuItem;
        private System.Windows.Forms.MenuItem jlv_h_speedMenuItem;
        private System.Windows.Forms.MenuItem jlv_h_etaMenuItem;
        private System.Windows.Forms.MenuItem jlv_sep1MenuItem;
        private System.Windows.Forms.MenuItem toolsAutoRemoveFinishedJobMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedLogOffMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedLockComputerMenuItem;
        private System.Windows.Forms.MenuItem jlv_h_nameMenuItem;
        private System.Windows.Forms.MenuItem helpDiscussionGroupMenuItem;
        private System.Windows.Forms.MenuItem helpWebsiteMenuItem;
        private System.Windows.Forms.MenuItem helpSep2MenuItem;
        private System.Windows.Forms.MenuItem jlv_h_noteMenuItem;
        private System.Windows.Forms.ToolBarButton newMultipleToolBarButton;
        private System.Windows.Forms.MenuItem jlv_j_editMultipleMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_setHttpRefererMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_setDownloadDirectoryMenuItem;
        private System.Windows.Forms.MenuItem t_detectClipboardMenuItem;
        private System.Windows.Forms.MenuItem t_speedLimitMenuItem;
        private System.Windows.Forms.MenuItem toolsSep3MenuItem;
        private System.Windows.Forms.MenuItem t_sep1MenuItem;
        private System.Windows.Forms.MenuItem t_quitMenuItem;
        private System.Windows.Forms.ToolBarButton speedLimitToolBarButton;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedSep2MenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedFreezeMenuItem;
        private System.Windows.Forms.MenuItem toolsWhenDownloadsFinishedCancelableMenuItem;
        private System.Windows.Forms.MenuItem jobsExportWgetBatchFileMenuItem;
        private System.Windows.Forms.MenuItem jlv_j_exportWgetBatchFileMenuItem;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuItem13;
    }
}