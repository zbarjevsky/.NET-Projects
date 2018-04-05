namespace WinFFAvi
{
  partial class FormWinFF
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWinFF));
		this.m_toolBar = new System.Windows.Forms.ToolStrip();
		this.m_toolStripButton_BrowseMov = new System.Windows.Forms.ToolStripButton();
		this.m_toolStripButton_Remove = new System.Windows.Forms.ToolStripButton();
		this.m_Separator5 = new System.Windows.Forms.ToolStripSeparator();
		this.m_btnRemoveAll = new System.Windows.Forms.ToolStripButton();
		this.m_Separator4 = new System.Windows.Forms.ToolStripSeparator();
		this.m_toolStripButton_Convert = new System.Windows.Forms.ToolStripButton();
		this.m_menuBar = new System.Windows.Forms.MenuStrip();
		this.m_mnuFile = new System.Windows.Forms.ToolStripMenuItem();
		this.m_mnuFile_Browse = new System.Windows.Forms.ToolStripMenuItem();
		this.m_mnuFile_Remove = new System.Windows.Forms.ToolStripMenuItem();
		this.m_mnuFile_Convert = new System.Windows.Forms.ToolStripMenuItem();
		this.m_Separator3 = new System.Windows.Forms.ToolStripSeparator();
		this.m_mnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
		this.m_mnuTools = new System.Windows.Forms.ToolStripMenuItem();
		this.m_mnuTools_Preferences = new System.Windows.Forms.ToolStripMenuItem();
		this.m_mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.m_mnuHelp_About = new System.Windows.Forms.ToolStripMenuItem();
		this.m_statusBar = new System.Windows.Forms.StatusStrip();
		this.m_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.m_btnBrowseFolder = new System.Windows.Forms.Button();
		this.m_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
		this.m_openFileDialogMov = new System.Windows.Forms.OpenFileDialog();
		this.m_splitContainer_Main = new System.Windows.Forms.SplitContainer();
		this.m_pnlFiles = new System.Windows.Forms.Panel();
		this.m_listSelectedFiles = new System.Windows.Forms.ListView();
		this.m_columnHeader1 = new System.Windows.Forms.ColumnHeader();
		this.m_columnHeader2 = new System.Windows.Forms.ColumnHeader();
		this.m_contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.m_ctxmnuInsert = new System.Windows.Forms.ToolStripMenuItem();
		this.m_ctxmnuRemove = new System.Windows.Forms.ToolStripMenuItem();
		this.m_Separator1 = new System.Windows.Forms.ToolStripSeparator();
		this.m_ctxmnuOpenFile = new System.Windows.Forms.ToolStripMenuItem();
		this.m_ctxmnuOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
		this.m_Separator2 = new System.Windows.Forms.ToolStripSeparator();
		this.m_ctxmnuPreferences = new System.Windows.Forms.ToolStripMenuItem();
		this.m_cmbOutFolder = new System.Windows.Forms.ComboBox();
		this.m_lblOutputFolder = new System.Windows.Forms.Label();
		this.m_pnlShowHideOptions = new System.Windows.Forms.Panel();
		this.m_chkUseOutOptions = new System.Windows.Forms.CheckBox();
		this.m_pnlOptions = new System.Windows.Forms.Panel();
		this.m_chkFrameRate = new System.Windows.Forms.CheckBox();
		this.m_cmbFrameRate = new System.Windows.Forms.ComboBox();
		this.m_chkAspect = new System.Windows.Forms.CheckBox();
		this.m_cmbAspect = new System.Windows.Forms.ComboBox();
		this.m_chkOutSize = new System.Windows.Forms.CheckBox();
		this.m_chkDeleteLog = new System.Windows.Forms.CheckBox();
		this.m_chkOrigFolder = new System.Windows.Forms.CheckBox();
		this.m_btnResetOut = new System.Windows.Forms.Button();
		this.m_cmbOutSize = new System.Windows.Forms.ComboBox();
		this.m_txtConsole = new System.Windows.Forms.RichTextBox();
		this.m_Separator6 = new System.Windows.Forms.ToolStripSeparator();
		this.m_btnFindFiles = new System.Windows.Forms.ToolStripButton();
		this.m_toolBar.SuspendLayout();
		this.m_menuBar.SuspendLayout();
		this.m_statusBar.SuspendLayout();
		this.m_splitContainer_Main.Panel1.SuspendLayout();
		this.m_splitContainer_Main.Panel2.SuspendLayout();
		this.m_splitContainer_Main.SuspendLayout();
		this.m_pnlFiles.SuspendLayout();
		this.m_contextMenuStrip1.SuspendLayout();
		this.m_pnlShowHideOptions.SuspendLayout();
		this.m_pnlOptions.SuspendLayout();
		this.SuspendLayout();
		// 
		// m_toolBar
		// 
		this.m_toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripButton_BrowseMov,
            this.m_btnFindFiles,
            this.m_Separator6,
            this.m_toolStripButton_Remove,
            this.m_Separator5,
            this.m_btnRemoveAll,
            this.m_Separator4,
            this.m_toolStripButton_Convert});
		this.m_toolBar.Location = new System.Drawing.Point(0, 24);
		this.m_toolBar.Name = "m_toolBar";
		this.m_toolBar.Size = new System.Drawing.Size(728, 25);
		this.m_toolBar.TabIndex = 1;
		this.m_toolBar.Text = "toolStrip1";
		// 
		// m_toolStripButton_BrowseMov
		// 
		this.m_toolStripButton_BrowseMov.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_BrowseMov.Image")));
		this.m_toolStripButton_BrowseMov.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.m_toolStripButton_BrowseMov.Name = "m_toolStripButton_BrowseMov";
		this.m_toolStripButton_BrowseMov.Size = new System.Drawing.Size(70, 22);
		this.m_toolStripButton_BrowseMov.Text = "Add Files";
		this.m_toolStripButton_BrowseMov.ToolTipText = "Add Files (Ins)";
		this.m_toolStripButton_BrowseMov.Click += new System.EventHandler(this.m_toolStripButton_BrowseMov_Click);
		// 
		// m_toolStripButton_Remove
		// 
		this.m_toolStripButton_Remove.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Remove.Image")));
		this.m_toolStripButton_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.m_toolStripButton_Remove.Name = "m_toolStripButton_Remove";
		this.m_toolStripButton_Remove.Size = new System.Drawing.Size(66, 22);
		this.m_toolStripButton_Remove.Text = "Remove";
		this.m_toolStripButton_Remove.ToolTipText = "Remove (Del)";
		this.m_toolStripButton_Remove.Click += new System.EventHandler(this.m_toolStripButton_Remove_Click);
		// 
		// m_Separator5
		// 
		this.m_Separator5.Name = "m_Separator5";
		this.m_Separator5.Size = new System.Drawing.Size(6, 25);
		// 
		// m_btnRemoveAll
		// 
		this.m_btnRemoveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.m_btnRemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("m_btnRemoveAll.Image")));
		this.m_btnRemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.m_btnRemoveAll.Name = "m_btnRemoveAll";
		this.m_btnRemoveAll.Size = new System.Drawing.Size(64, 22);
		this.m_btnRemoveAll.Text = "Remove All";
		this.m_btnRemoveAll.Click += new System.EventHandler(this.m_btnRemoveAll_Click);
		// 
		// m_Separator4
		// 
		this.m_Separator4.Name = "m_Separator4";
		this.m_Separator4.Size = new System.Drawing.Size(6, 25);
		// 
		// m_toolStripButton_Convert
		// 
		this.m_toolStripButton_Convert.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Convert.Image")));
		this.m_toolStripButton_Convert.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.m_toolStripButton_Convert.Name = "m_toolStripButton_Convert";
		this.m_toolStripButton_Convert.Size = new System.Drawing.Size(66, 22);
		this.m_toolStripButton_Convert.Text = "Convert";
		this.m_toolStripButton_Convert.Click += new System.EventHandler(this.m_toolStripButton_Convert_Click);
		// 
		// m_menuBar
		// 
		this.m_menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFile,
            this.m_mnuTools,
            this.m_mnuHelp});
		this.m_menuBar.Location = new System.Drawing.Point(0, 0);
		this.m_menuBar.Name = "m_menuBar";
		this.m_menuBar.Size = new System.Drawing.Size(728, 24);
		this.m_menuBar.TabIndex = 0;
		this.m_menuBar.Text = "Main Menu";
		// 
		// m_mnuFile
		// 
		this.m_mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFile_Browse,
            this.m_mnuFile_Remove,
            this.m_mnuFile_Convert,
            this.m_Separator3,
            this.m_mnuFile_Exit});
		this.m_mnuFile.Name = "m_mnuFile";
		this.m_mnuFile.Size = new System.Drawing.Size(35, 20);
		this.m_mnuFile.Text = "&File";
		// 
		// m_mnuFile_Browse
		// 
		this.m_mnuFile_Browse.Image = global::WinFFAvi.Properties.Resources.add_16;
		this.m_mnuFile_Browse.Name = "m_mnuFile_Browse";
		this.m_mnuFile_Browse.ShortcutKeys = System.Windows.Forms.Keys.Insert;
		this.m_mnuFile_Browse.Size = new System.Drawing.Size(172, 22);
		this.m_mnuFile_Browse.Text = "Add *.mov Files";
		this.m_mnuFile_Browse.Click += new System.EventHandler(this.m_toolStripButton_BrowseMov_Click);
		// 
		// m_mnuFile_Remove
		// 
		this.m_mnuFile_Remove.Image = global::WinFFAvi.Properties.Resources.delete_16;
		this.m_mnuFile_Remove.Name = "m_mnuFile_Remove";
		this.m_mnuFile_Remove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
		this.m_mnuFile_Remove.Size = new System.Drawing.Size(172, 22);
		this.m_mnuFile_Remove.Text = "Remove";
		this.m_mnuFile_Remove.Click += new System.EventHandler(this.m_toolStripButton_Remove_Click);
		// 
		// m_mnuFile_Convert
		// 
		this.m_mnuFile_Convert.Image = global::WinFFAvi.Properties.Resources.redo_16;
		this.m_mnuFile_Convert.Name = "m_mnuFile_Convert";
		this.m_mnuFile_Convert.Size = new System.Drawing.Size(172, 22);
		this.m_mnuFile_Convert.Text = "Convert";
		this.m_mnuFile_Convert.Click += new System.EventHandler(this.m_toolStripButton_Convert_Click);
		// 
		// m_Separator3
		// 
		this.m_Separator3.Name = "m_Separator3";
		this.m_Separator3.Size = new System.Drawing.Size(169, 6);
		// 
		// m_mnuFile_Exit
		// 
		this.m_mnuFile_Exit.Image = global::WinFFAvi.Properties.Resources.home_16;
		this.m_mnuFile_Exit.Name = "m_mnuFile_Exit";
		this.m_mnuFile_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
		this.m_mnuFile_Exit.Size = new System.Drawing.Size(172, 22);
		this.m_mnuFile_Exit.Text = "Exit";
		this.m_mnuFile_Exit.Click += new System.EventHandler(this.m_mnuFile_Exit_Click);
		// 
		// m_mnuTools
		// 
		this.m_mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuTools_Preferences});
		this.m_mnuTools.Name = "m_mnuTools";
		this.m_mnuTools.Size = new System.Drawing.Size(44, 20);
		this.m_mnuTools.Text = "&Tools";
		// 
		// m_mnuTools_Preferences
		// 
		this.m_mnuTools_Preferences.Image = global::WinFFAvi.Properties.Resources.applications_16;
		this.m_mnuTools_Preferences.Name = "m_mnuTools_Preferences";
		this.m_mnuTools_Preferences.Size = new System.Drawing.Size(132, 22);
		this.m_mnuTools_Preferences.Text = "&Preferences";
		this.m_mnuTools_Preferences.Click += new System.EventHandler(this.m_mnuTools_Preferences_Click);
		// 
		// m_mnuHelp
		// 
		this.m_mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuHelp_About});
		this.m_mnuHelp.Name = "m_mnuHelp";
		this.m_mnuHelp.Size = new System.Drawing.Size(40, 20);
		this.m_mnuHelp.Text = "&Help";
		// 
		// m_mnuHelp_About
		// 
		this.m_mnuHelp_About.Name = "m_mnuHelp_About";
		this.m_mnuHelp_About.Size = new System.Drawing.Size(103, 22);
		this.m_mnuHelp_About.Text = "&About";
		this.m_mnuHelp_About.Click += new System.EventHandler(this.m_mnuHelp_About_Click);
		// 
		// m_statusBar
		// 
		this.m_statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel1});
		this.m_statusBar.Location = new System.Drawing.Point(0, 395);
		this.m_statusBar.Name = "m_statusBar";
		this.m_statusBar.Size = new System.Drawing.Size(728, 22);
		this.m_statusBar.TabIndex = 2;
		this.m_statusBar.Text = "statusStrip1";
		// 
		// m_toolStripStatusLabel1
		// 
		this.m_toolStripStatusLabel1.Name = "m_toolStripStatusLabel1";
		this.m_toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
		this.m_toolStripStatusLabel1.Text = "Ready";
		// 
		// m_btnBrowseFolder
		// 
		this.m_btnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.m_btnBrowseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.m_btnBrowseFolder.Location = new System.Drawing.Point(697, 173);
		this.m_btnBrowseFolder.Name = "m_btnBrowseFolder";
		this.m_btnBrowseFolder.Size = new System.Drawing.Size(27, 21);
		this.m_btnBrowseFolder.TabIndex = 0;
		this.m_btnBrowseFolder.Text = "...";
		this.m_btnBrowseFolder.UseVisualStyleBackColor = true;
		this.m_btnBrowseFolder.Click += new System.EventHandler(this.m_btnBrowseFolder_Click);
		// 
		// m_folderBrowserDialog
		// 
		this.m_folderBrowserDialog.Description = "Select output folder for converted files";
		// 
		// m_openFileDialogMov
		// 
		this.m_openFileDialogMov.FileName = "*.mov";
		this.m_openFileDialogMov.Filter = "Video Files *.mov|*.mov|All Files|*.*";
		this.m_openFileDialogMov.Multiselect = true;
		// 
		// m_splitContainer_Main
		// 
		this.m_splitContainer_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.m_splitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_splitContainer_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.m_splitContainer_Main.ForeColor = System.Drawing.SystemColors.ControlText;
		this.m_splitContainer_Main.Location = new System.Drawing.Point(0, 49);
		this.m_splitContainer_Main.Name = "m_splitContainer_Main";
		this.m_splitContainer_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
		// 
		// m_splitContainer_Main.Panel1
		// 
		this.m_splitContainer_Main.Panel1.Controls.Add(this.m_pnlFiles);
		this.m_splitContainer_Main.Panel1.Controls.Add(this.m_pnlShowHideOptions);
		this.m_splitContainer_Main.Panel1.Controls.Add(this.m_pnlOptions);
		this.m_splitContainer_Main.Panel1MinSize = 250;
		// 
		// m_splitContainer_Main.Panel2
		// 
		this.m_splitContainer_Main.Panel2.Controls.Add(this.m_txtConsole);
		this.m_splitContainer_Main.Size = new System.Drawing.Size(728, 346);
		this.m_splitContainer_Main.SplitterDistance = 299;
		this.m_splitContainer_Main.TabIndex = 2;
		// 
		// m_pnlFiles
		// 
		this.m_pnlFiles.Controls.Add(this.m_listSelectedFiles);
		this.m_pnlFiles.Controls.Add(this.m_btnBrowseFolder);
		this.m_pnlFiles.Controls.Add(this.m_cmbOutFolder);
		this.m_pnlFiles.Controls.Add(this.m_lblOutputFolder);
		this.m_pnlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_pnlFiles.Location = new System.Drawing.Point(0, 0);
		this.m_pnlFiles.Name = "m_pnlFiles";
		this.m_pnlFiles.Size = new System.Drawing.Size(726, 197);
		this.m_pnlFiles.TabIndex = 4;
		// 
		// m_listSelectedFiles
		// 
		this.m_listSelectedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
					| System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.m_listSelectedFiles.CheckBoxes = true;
		this.m_listSelectedFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_columnHeader1,
            this.m_columnHeader2});
		this.m_listSelectedFiles.ContextMenuStrip = this.m_contextMenuStrip1;
		this.m_listSelectedFiles.FullRowSelect = true;
		this.m_listSelectedFiles.GridLines = true;
		this.m_listSelectedFiles.HideSelection = false;
		this.m_listSelectedFiles.Location = new System.Drawing.Point(0, 0);
		this.m_listSelectedFiles.Name = "m_listSelectedFiles";
		this.m_listSelectedFiles.Size = new System.Drawing.Size(726, 149);
		this.m_listSelectedFiles.TabIndex = 1;
		this.m_listSelectedFiles.UseCompatibleStateImageBehavior = false;
		this.m_listSelectedFiles.View = System.Windows.Forms.View.Details;
		this.m_listSelectedFiles.SelectedIndexChanged += new System.EventHandler(this.m_listSelectedFiles_SelectedIndexChanged);
		// 
		// m_columnHeader1
		// 
		this.m_columnHeader1.Text = "Files to Convert";
		this.m_columnHeader1.Width = 500;
		// 
		// m_columnHeader2
		// 
		this.m_columnHeader2.Text = "Size";
		this.m_columnHeader2.Width = 100;
		// 
		// m_contextMenuStrip1
		// 
		this.m_contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ctxmnuInsert,
            this.m_ctxmnuRemove,
            this.m_Separator1,
            this.m_ctxmnuOpenFile,
            this.m_ctxmnuOpenFolder,
            this.m_Separator2,
            this.m_ctxmnuPreferences});
		this.m_contextMenuStrip1.Name = "m_contextMenuStrip1";
		this.m_contextMenuStrip1.Size = new System.Drawing.Size(188, 126);
		// 
		// m_ctxmnuInsert
		// 
		this.m_ctxmnuInsert.Image = global::WinFFAvi.Properties.Resources.add_16;
		this.m_ctxmnuInsert.Name = "m_ctxmnuInsert";
		this.m_ctxmnuInsert.Size = new System.Drawing.Size(187, 22);
		this.m_ctxmnuInsert.Text = "&Add Files";
		this.m_ctxmnuInsert.Click += new System.EventHandler(this.m_toolStripButton_BrowseMov_Click);
		// 
		// m_ctxmnuRemove
		// 
		this.m_ctxmnuRemove.Image = global::WinFFAvi.Properties.Resources.delete_16;
		this.m_ctxmnuRemove.Name = "m_ctxmnuRemove";
		this.m_ctxmnuRemove.Size = new System.Drawing.Size(187, 22);
		this.m_ctxmnuRemove.Text = "&Remove";
		this.m_ctxmnuRemove.Click += new System.EventHandler(this.m_toolStripButton_Remove_Click);
		// 
		// m_Separator1
		// 
		this.m_Separator1.Name = "m_Separator1";
		this.m_Separator1.Size = new System.Drawing.Size(184, 6);
		// 
		// m_ctxmnuOpenFile
		// 
		this.m_ctxmnuOpenFile.Image = global::WinFFAvi.Properties.Resources.disc_media_16;
		this.m_ctxmnuOpenFile.Name = "m_ctxmnuOpenFile";
		this.m_ctxmnuOpenFile.Size = new System.Drawing.Size(187, 22);
		this.m_ctxmnuOpenFile.Text = "&Open";
		this.m_ctxmnuOpenFile.Click += new System.EventHandler(this.m_ctxmnuOpenFile_Click);
		// 
		// m_ctxmnuOpenFolder
		// 
		this.m_ctxmnuOpenFolder.Image = global::WinFFAvi.Properties.Resources.folder_open_161;
		this.m_ctxmnuOpenFolder.Name = "m_ctxmnuOpenFolder";
		this.m_ctxmnuOpenFolder.Size = new System.Drawing.Size(187, 22);
		this.m_ctxmnuOpenFolder.Text = "Open Containing &Folder";
		this.m_ctxmnuOpenFolder.Click += new System.EventHandler(this.m_ctxmnuOpenFolder_Click);
		// 
		// m_Separator2
		// 
		this.m_Separator2.Name = "m_Separator2";
		this.m_Separator2.Size = new System.Drawing.Size(184, 6);
		// 
		// m_ctxmnuPreferences
		// 
		this.m_ctxmnuPreferences.Image = global::WinFFAvi.Properties.Resources.applications_16;
		this.m_ctxmnuPreferences.Name = "m_ctxmnuPreferences";
		this.m_ctxmnuPreferences.Size = new System.Drawing.Size(187, 22);
		this.m_ctxmnuPreferences.Text = "&Preferences";
		this.m_ctxmnuPreferences.Click += new System.EventHandler(this.m_mnuTools_Preferences_Click);
		// 
		// m_cmbOutFolder
		// 
		this.m_cmbOutFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.m_cmbOutFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.m_cmbOutFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
		this.m_cmbOutFolder.FormattingEnabled = true;
		this.m_cmbOutFolder.Items.AddRange(new object[] {
            ".",
            "c:\\Temp\\Photo\\"});
		this.m_cmbOutFolder.Location = new System.Drawing.Point(0, 173);
		this.m_cmbOutFolder.Name = "m_cmbOutFolder";
		this.m_cmbOutFolder.Size = new System.Drawing.Size(695, 21);
		this.m_cmbOutFolder.TabIndex = 3;
		this.m_cmbOutFolder.Text = ".";
		// 
		// m_lblOutputFolder
		// 
		this.m_lblOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.m_lblOutputFolder.AutoSize = true;
		this.m_lblOutputFolder.Location = new System.Drawing.Point(0, 157);
		this.m_lblOutputFolder.Name = "m_lblOutputFolder";
		this.m_lblOutputFolder.Size = new System.Drawing.Size(257, 13);
		this.m_lblOutputFolder.TabIndex = 2;
		this.m_lblOutputFolder.Text = "Output Folder: (. means  - use same  folder  to output)";
		// 
		// m_pnlShowHideOptions
		// 
		this.m_pnlShowHideOptions.Controls.Add(this.m_chkUseOutOptions);
		this.m_pnlShowHideOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.m_pnlShowHideOptions.Location = new System.Drawing.Point(0, 197);
		this.m_pnlShowHideOptions.Name = "m_pnlShowHideOptions";
		this.m_pnlShowHideOptions.Size = new System.Drawing.Size(726, 27);
		this.m_pnlShowHideOptions.TabIndex = 0;
		// 
		// m_chkUseOutOptions
		// 
		this.m_chkUseOutOptions.AutoSize = true;
		this.m_chkUseOutOptions.Checked = true;
		this.m_chkUseOutOptions.CheckState = System.Windows.Forms.CheckState.Checked;
		this.m_chkUseOutOptions.Enabled = false;
		this.m_chkUseOutOptions.Location = new System.Drawing.Point(1, 5);
		this.m_chkUseOutOptions.Name = "m_chkUseOutOptions";
		this.m_chkUseOutOptions.Size = new System.Drawing.Size(100, 17);
		this.m_chkUseOutOptions.TabIndex = 0;
		this.m_chkUseOutOptions.Text = "Output Options:";
		this.m_chkUseOutOptions.UseVisualStyleBackColor = true;
		this.m_chkUseOutOptions.CheckedChanged += new System.EventHandler(this.m_chkUseOutFormat_CheckedChanged);
		// 
		// m_pnlOptions
		// 
		this.m_pnlOptions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.m_pnlOptions.Controls.Add(this.m_chkFrameRate);
		this.m_pnlOptions.Controls.Add(this.m_cmbFrameRate);
		this.m_pnlOptions.Controls.Add(this.m_chkAspect);
		this.m_pnlOptions.Controls.Add(this.m_cmbAspect);
		this.m_pnlOptions.Controls.Add(this.m_chkOutSize);
		this.m_pnlOptions.Controls.Add(this.m_chkDeleteLog);
		this.m_pnlOptions.Controls.Add(this.m_chkOrigFolder);
		this.m_pnlOptions.Controls.Add(this.m_btnResetOut);
		this.m_pnlOptions.Controls.Add(this.m_cmbOutSize);
		this.m_pnlOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.m_pnlOptions.Location = new System.Drawing.Point(0, 224);
		this.m_pnlOptions.Name = "m_pnlOptions";
		this.m_pnlOptions.Size = new System.Drawing.Size(726, 73);
		this.m_pnlOptions.TabIndex = 1;
		// 
		// m_chkFrameRate
		// 
		this.m_chkFrameRate.AutoSize = true;
		this.m_chkFrameRate.Location = new System.Drawing.Point(399, 8);
		this.m_chkFrameRate.Name = "m_chkFrameRate";
		this.m_chkFrameRate.Size = new System.Drawing.Size(84, 17);
		this.m_chkFrameRate.TabIndex = 7;
		this.m_chkFrameRate.Text = "Frame Rate:";
		this.m_chkFrameRate.UseVisualStyleBackColor = true;
		// 
		// m_cmbFrameRate
		// 
		this.m_cmbFrameRate.FormattingEnabled = true;
		this.m_cmbFrameRate.Location = new System.Drawing.Point(489, 6);
		this.m_cmbFrameRate.Name = "m_cmbFrameRate";
		this.m_cmbFrameRate.Size = new System.Drawing.Size(110, 21);
		this.m_cmbFrameRate.TabIndex = 8;
		// 
		// m_chkAspect
		// 
		this.m_chkAspect.AutoSize = true;
		this.m_chkAspect.Checked = true;
		this.m_chkAspect.CheckState = System.Windows.Forms.CheckState.Checked;
		this.m_chkAspect.Location = new System.Drawing.Point(198, 8);
		this.m_chkAspect.Name = "m_chkAspect";
		this.m_chkAspect.Size = new System.Drawing.Size(62, 17);
		this.m_chkAspect.TabIndex = 2;
		this.m_chkAspect.Text = "Aspect:";
		this.m_chkAspect.UseVisualStyleBackColor = true;
		// 
		// m_cmbAspect
		// 
		this.m_cmbAspect.FormattingEnabled = true;
		this.m_cmbAspect.Location = new System.Drawing.Point(266, 6);
		this.m_cmbAspect.Name = "m_cmbAspect";
		this.m_cmbAspect.Size = new System.Drawing.Size(110, 21);
		this.m_cmbAspect.TabIndex = 3;
		// 
		// m_chkOutSize
		// 
		this.m_chkOutSize.AutoSize = true;
		this.m_chkOutSize.Location = new System.Drawing.Point(10, 8);
		this.m_chkOutSize.Name = "m_chkOutSize";
		this.m_chkOutSize.Size = new System.Drawing.Size(49, 17);
		this.m_chkOutSize.TabIndex = 0;
		this.m_chkOutSize.Text = "Size:";
		this.m_chkOutSize.UseVisualStyleBackColor = true;
		this.m_chkOutSize.CheckedChanged += new System.EventHandler(this.m_chkOutputSize_CheckedChanged);
		// 
		// m_chkDeleteLog
		// 
		this.m_chkDeleteLog.AutoSize = true;
		this.m_chkDeleteLog.Checked = true;
		this.m_chkDeleteLog.CheckState = System.Windows.Forms.CheckState.Checked;
		this.m_chkDeleteLog.Location = new System.Drawing.Point(10, 48);
		this.m_chkDeleteLog.Name = "m_chkDeleteLog";
		this.m_chkDeleteLog.Size = new System.Drawing.Size(174, 17);
		this.m_chkDeleteLog.TabIndex = 6;
		this.m_chkDeleteLog.Text = "Delete log files after conversion";
		this.m_chkDeleteLog.UseVisualStyleBackColor = true;
		// 
		// m_chkOrigFolder
		// 
		this.m_chkOrigFolder.AutoSize = true;
		this.m_chkOrigFolder.Checked = true;
		this.m_chkOrigFolder.CheckState = System.Windows.Forms.CheckState.Checked;
		this.m_chkOrigFolder.Location = new System.Drawing.Point(10, 29);
		this.m_chkOrigFolder.Name = "m_chkOrigFolder";
		this.m_chkOrigFolder.Size = new System.Drawing.Size(417, 17);
		this.m_chkOrigFolder.TabIndex = 5;
		this.m_chkOrigFolder.Text = "Create \'Orig\' folder if output to \'.\' folder and move original files there after " +
			"conversion";
		this.m_chkOrigFolder.UseVisualStyleBackColor = true;
		// 
		// m_btnResetOut
		// 
		this.m_btnResetOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.m_btnResetOut.Location = new System.Drawing.Point(616, 4);
		this.m_btnResetOut.Name = "m_btnResetOut";
		this.m_btnResetOut.Size = new System.Drawing.Size(105, 23);
		this.m_btnResetOut.TabIndex = 4;
		this.m_btnResetOut.Text = "Restore Defaults";
		this.m_btnResetOut.UseVisualStyleBackColor = true;
		this.m_btnResetOut.Click += new System.EventHandler(this.m_btnResetOut_Click);
		// 
		// m_cmbOutSize
		// 
		this.m_cmbOutSize.FormattingEnabled = true;
		this.m_cmbOutSize.Location = new System.Drawing.Point(65, 6);
		this.m_cmbOutSize.Name = "m_cmbOutSize";
		this.m_cmbOutSize.Size = new System.Drawing.Size(110, 21);
		this.m_cmbOutSize.TabIndex = 1;
		this.m_cmbOutSize.SelectedIndexChanged += new System.EventHandler(this.m_cmbOutSize_SelectedIndexChanged);
		this.m_cmbOutSize.TextUpdate += new System.EventHandler(this.m_cmbOutSize_TextUpdate);
		// 
		// m_txtConsole
		// 
		this.m_txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_txtConsole.Location = new System.Drawing.Point(0, 0);
		this.m_txtConsole.Name = "m_txtConsole";
		this.m_txtConsole.Size = new System.Drawing.Size(726, 41);
		this.m_txtConsole.TabIndex = 0;
		this.m_txtConsole.Text = "";
		// 
		// m_Separator6
		// 
		this.m_Separator6.Name = "m_Separator6";
		this.m_Separator6.Size = new System.Drawing.Size(6, 25);
		// 
		// m_btnFindFiles
		// 
		this.m_btnFindFiles.Image = ((System.Drawing.Image)(resources.GetObject("m_btnFindFiles.Image")));
		this.m_btnFindFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.m_btnFindFiles.Name = "m_btnFindFiles";
		this.m_btnFindFiles.Size = new System.Drawing.Size(71, 22);
		this.m_btnFindFiles.Text = "Find Files";
		this.m_btnFindFiles.Click += new System.EventHandler(this.m_btnFindFiles_Click);
		// 
		// FormWinFF
		// 
		this.AllowDrop = true;
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(728, 417);
		this.Controls.Add(this.m_splitContainer_Main);
		this.Controls.Add(this.m_statusBar);
		this.Controls.Add(this.m_toolBar);
		this.Controls.Add(this.m_menuBar);
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MainMenuStrip = this.m_menuBar;
		this.MinimumSize = new System.Drawing.Size(300, 400);
		this.Name = "FormWinFF";
		this.Text = "WinFF Mov -> Avi";
		this.Load += new System.EventHandler(this.FormWinFF_Load);
		this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormWinFF_DragDrop);
		this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormWinFF_DragEnter);
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWinFF_FormClosing);
		this.m_toolBar.ResumeLayout(false);
		this.m_toolBar.PerformLayout();
		this.m_menuBar.ResumeLayout(false);
		this.m_menuBar.PerformLayout();
		this.m_statusBar.ResumeLayout(false);
		this.m_statusBar.PerformLayout();
		this.m_splitContainer_Main.Panel1.ResumeLayout(false);
		this.m_splitContainer_Main.Panel2.ResumeLayout(false);
		this.m_splitContainer_Main.ResumeLayout(false);
		this.m_pnlFiles.ResumeLayout(false);
		this.m_pnlFiles.PerformLayout();
		this.m_contextMenuStrip1.ResumeLayout(false);
		this.m_pnlShowHideOptions.ResumeLayout(false);
		this.m_pnlShowHideOptions.PerformLayout();
		this.m_pnlOptions.ResumeLayout(false);
		this.m_pnlOptions.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip m_toolBar;
    private System.Windows.Forms.MenuStrip m_menuBar;
    private System.Windows.Forms.StatusStrip m_statusBar;
    private System.Windows.Forms.Button m_btnBrowseFolder;
    private System.Windows.Forms.ToolStripButton m_toolStripButton_BrowseMov;
    private System.Windows.Forms.ToolStripButton m_toolStripButton_Convert;
    private System.Windows.Forms.ToolStripMenuItem m_mnuFile;
    private System.Windows.Forms.ToolStripMenuItem m_mnuFile_Browse;
    private System.Windows.Forms.ToolStripMenuItem m_mnuFile_Convert;
    private System.Windows.Forms.FolderBrowserDialog m_folderBrowserDialog;
    private System.Windows.Forms.OpenFileDialog m_openFileDialogMov;
    private System.Windows.Forms.ToolStripSeparator m_Separator3;
    private System.Windows.Forms.ToolStripMenuItem m_mnuFile_Exit;
    private System.Windows.Forms.SplitContainer m_splitContainer_Main;
    private System.Windows.Forms.RichTextBox m_txtConsole;
    private System.Windows.Forms.Label m_lblOutputFolder;
    private System.Windows.Forms.ListView m_listSelectedFiles;
    private System.Windows.Forms.ToolStripButton m_toolStripButton_Remove;
    private System.Windows.Forms.ToolStripSeparator m_Separator4;
    private System.Windows.Forms.ComboBox m_cmbOutFolder;
    private System.Windows.Forms.ToolStripMenuItem m_mnuFile_Remove;
    private System.Windows.Forms.ToolStripMenuItem m_mnuTools;
    private System.Windows.Forms.ToolStripMenuItem m_mnuTools_Preferences;
    private System.Windows.Forms.ContextMenuStrip m_contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem m_ctxmnuInsert;
    private System.Windows.Forms.ToolStripMenuItem m_ctxmnuRemove;
    private System.Windows.Forms.ToolStripSeparator m_Separator1;
    private System.Windows.Forms.ToolStripMenuItem m_ctxmnuPreferences;
    private System.Windows.Forms.ToolStripMenuItem m_ctxmnuOpenFile;
    private System.Windows.Forms.ToolStripMenuItem m_ctxmnuOpenFolder;
    private System.Windows.Forms.ToolStripSeparator m_Separator2;
    private System.Windows.Forms.ColumnHeader m_columnHeader1;
    private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel1;
    private System.Windows.Forms.Panel m_pnlFiles;
    private System.Windows.Forms.Panel m_pnlOptions;
	private System.Windows.Forms.Panel m_pnlShowHideOptions;
    private System.Windows.Forms.CheckBox m_chkUseOutOptions;
    private System.Windows.Forms.Button m_btnResetOut;
    private System.Windows.Forms.ComboBox m_cmbOutSize;
    private System.Windows.Forms.ToolStripMenuItem m_mnuHelp;
    private System.Windows.Forms.ToolStripMenuItem m_mnuHelp_About;
    private System.Windows.Forms.ColumnHeader m_columnHeader2;
	private System.Windows.Forms.CheckBox m_chkOrigFolder;
	private System.Windows.Forms.CheckBox m_chkDeleteLog;
	private System.Windows.Forms.CheckBox m_chkOutSize;
	private System.Windows.Forms.CheckBox m_chkAspect;
	private System.Windows.Forms.ComboBox m_cmbAspect;
	private System.Windows.Forms.ToolStripButton m_btnRemoveAll;
	private System.Windows.Forms.CheckBox m_chkFrameRate;
	private System.Windows.Forms.ComboBox m_cmbFrameRate;
	private System.Windows.Forms.ToolStripSeparator m_Separator5;
	private System.Windows.Forms.ToolStripButton m_btnFindFiles;
	private System.Windows.Forms.ToolStripSeparator m_Separator6;
  }
}

