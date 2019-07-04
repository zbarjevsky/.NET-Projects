namespace YouTubeDownload
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_Status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_Status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.m_btnUpdate = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.m_mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFileAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuToolsSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuToolsUpdateDL = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuToolsOutputFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.m_listUrls = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_ContextMenuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ctxmnuOpenSelectedFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctxmnuOpenOutputFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctxmnuRemoveSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ctxmnuAddUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ctxmnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_btnAddUrl = new System.Windows.Forms.Button();
            this.m_spliMain = new System.Windows.Forms.SplitContainer();
            this.m_btnClearList = new System.Windows.Forms.Button();
            this.m_lnkOutputFolder = new System.Windows.Forms.LinkLabel();
            this.m_btnPause = new System.Windows.Forms.Button();
            this.m_imageListStartStop = new System.Windows.Forms.ImageList(this.components);
            this.m_btnRemove = new System.Windows.Forms.Button();
            this.m_btnBrowseForFolder = new System.Windows.Forms.Button();
            this.m_errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_ctxmnuDownloadAgain = new System.Windows.Forms.ToolStripMenuItem();
            this.m_DownloaderUserControl = new YouTubeDownload.DownloaderUserControl();
            this.m_statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.m_ContextMenuList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spliMain)).BeginInit();
            this.m_spliMain.Panel1.SuspendLayout();
            this.m_spliMain.Panel2.SuspendLayout();
            this.m_spliMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_Status1,
            this.m_Status2,
            this.m_StatusProgress});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 531);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.m_statusStrip.Size = new System.Drawing.Size(984, 31);
            this.m_statusStrip.TabIndex = 9;
            this.m_statusStrip.Text = ".";
            // 
            // m_Status1
            // 
            this.m_Status1.Name = "m_Status1";
            this.m_Status1.Size = new System.Drawing.Size(53, 26);
            this.m_Status1.Text = "Ready.";
            // 
            // m_Status2
            // 
            this.m_Status2.Name = "m_Status2";
            this.m_Status2.Size = new System.Drawing.Size(756, 26);
            this.m_Status2.Spring = true;
            this.m_Status2.Text = "...";
            // 
            // m_StatusProgress
            // 
            this.m_StatusProgress.Name = "m_StatusProgress";
            this.m_StatusProgress.Size = new System.Drawing.Size(150, 25);
            // 
            // m_btnUpdate
            // 
            this.m_btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("m_btnUpdate.Image")));
            this.m_btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnUpdate.Location = new System.Drawing.Point(706, 29);
            this.m_btnUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnUpdate.Name = "m_btnUpdate";
            this.m_btnUpdate.Size = new System.Drawing.Size(265, 33);
            this.m_btnUpdate.TabIndex = 0;
            this.m_btnUpdate.Text = "Update youtube-dl engine...";
            this.m_btnUpdate.UseVisualStyleBackColor = true;
            this.m_btnUpdate.Click += new System.EventHandler(this.m_btnUpdate_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFile,
            this.m_mnuTools,
            this.m_mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // m_mnuFile
            // 
            this.m_mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFileAdd,
            this.toolStripMenuItem1,
            this.m_mnuFileExit});
            this.m_mnuFile.Name = "m_mnuFile";
            this.m_mnuFile.Size = new System.Drawing.Size(44, 24);
            this.m_mnuFile.Text = "&File";
            // 
            // m_mnuFileAdd
            // 
            this.m_mnuFileAdd.Name = "m_mnuFileAdd";
            this.m_mnuFileAdd.Size = new System.Drawing.Size(219, 26);
            this.m_mnuFileAdd.Text = "Add New Download";
            this.m_mnuFileAdd.Click += new System.EventHandler(this.m_mnuFileAdd_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(216, 6);
            // 
            // m_mnuFileExit
            // 
            this.m_mnuFileExit.Name = "m_mnuFileExit";
            this.m_mnuFileExit.Size = new System.Drawing.Size(219, 26);
            this.m_mnuFileExit.Text = "E&xit";
            this.m_mnuFileExit.Click += new System.EventHandler(this.m_mnuFileExit_Click);
            // 
            // m_mnuTools
            // 
            this.m_mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuToolsSettings,
            this.m_mnuToolsUpdateDL,
            this.toolStripMenuItem2,
            this.m_mnuToolsOutputFolder});
            this.m_mnuTools.Name = "m_mnuTools";
            this.m_mnuTools.Size = new System.Drawing.Size(56, 24);
            this.m_mnuTools.Text = "&Tools";
            // 
            // m_mnuToolsSettings
            // 
            this.m_mnuToolsSettings.Name = "m_mnuToolsSettings";
            this.m_mnuToolsSettings.Size = new System.Drawing.Size(210, 26);
            this.m_mnuToolsSettings.Text = "&Settings";
            this.m_mnuToolsSettings.Click += new System.EventHandler(this.m_mnuToolsSettings_Click);
            // 
            // m_mnuToolsUpdateDL
            // 
            this.m_mnuToolsUpdateDL.Name = "m_mnuToolsUpdateDL";
            this.m_mnuToolsUpdateDL.Size = new System.Drawing.Size(210, 26);
            this.m_mnuToolsUpdateDL.Text = "Update youtube-dl";
            this.m_mnuToolsUpdateDL.Click += new System.EventHandler(this.m_mnuToolsUpdateDL_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(207, 6);
            // 
            // m_mnuToolsOutputFolder
            // 
            this.m_mnuToolsOutputFolder.Name = "m_mnuToolsOutputFolder";
            this.m_mnuToolsOutputFolder.Size = new System.Drawing.Size(210, 26);
            this.m_mnuToolsOutputFolder.Text = "Output Folder...";
            this.m_mnuToolsOutputFolder.Click += new System.EventHandler(this.m_mnuToolsOutputFolder_Click);
            // 
            // m_mnuHelp
            // 
            this.m_mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuHelpAbout});
            this.m_mnuHelp.Name = "m_mnuHelp";
            this.m_mnuHelp.Size = new System.Drawing.Size(53, 24);
            this.m_mnuHelp.Text = "Help";
            // 
            // m_mnuHelpAbout
            // 
            this.m_mnuHelpAbout.Name = "m_mnuHelpAbout";
            this.m_mnuHelpAbout.Size = new System.Drawing.Size(125, 26);
            this.m_mnuHelpAbout.Text = "About";
            this.m_mnuHelpAbout.Click += new System.EventHandler(this.m_mnuHelpAbout_Click);
            // 
            // m_listUrls
            // 
            this.m_listUrls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_listUrls.ContextMenuStrip = this.m_ContextMenuList;
            this.m_listUrls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listUrls.FullRowSelect = true;
            this.m_listUrls.GridLines = true;
            this.m_listUrls.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listUrls.HideSelection = false;
            this.m_listUrls.Location = new System.Drawing.Point(0, 0);
            this.m_listUrls.Name = "m_listUrls";
            this.m_listUrls.Size = new System.Drawing.Size(959, 155);
            this.m_listUrls.TabIndex = 0;
            this.m_listUrls.UseCompatibleStateImageBehavior = false;
            this.m_listUrls.View = System.Windows.Forms.View.Details;
            this.m_listUrls.SelectedIndexChanged += new System.EventHandler(this.m_listUrls_SelectedIndexChanged);
            this.m_listUrls.DoubleClick += new System.EventHandler(this.m_listUrls_DoubleClick);
            this.m_listUrls.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_listUrls_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "State";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 600;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Url";
            this.columnHeader3.Width = 600;
            // 
            // m_ContextMenuList
            // 
            this.m_ContextMenuList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_ContextMenuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ctxmnuAddUrl,
            this.toolStripMenuItem3,
            this.m_ctxmnuOpenSelectedFile,
            this.m_ctxmnuOpenOutputFolder,
            this.m_ctxmnuRemoveSelected,
            this.m_ctxmnuDownloadAgain,
            this.toolStripMenuItem4,
            this.m_ctxmnuExit});
            this.m_ContextMenuList.Name = "contextMenuStrip1";
            this.m_ContextMenuList.Size = new System.Drawing.Size(195, 200);
            // 
            // m_ctxmnuOpenSelectedFile
            // 
            this.m_ctxmnuOpenSelectedFile.Name = "m_ctxmnuOpenSelectedFile";
            this.m_ctxmnuOpenSelectedFile.Size = new System.Drawing.Size(194, 26);
            this.m_ctxmnuOpenSelectedFile.Text = "Open File";
            this.m_ctxmnuOpenSelectedFile.Click += new System.EventHandler(this.m_mnuOpenSelectedFile_Click);
            // 
            // m_ctxmnuOpenOutputFolder
            // 
            this.m_ctxmnuOpenOutputFolder.Name = "m_ctxmnuOpenOutputFolder";
            this.m_ctxmnuOpenOutputFolder.Size = new System.Drawing.Size(194, 26);
            this.m_ctxmnuOpenOutputFolder.Text = "Open Folder";
            this.m_ctxmnuOpenOutputFolder.Click += new System.EventHandler(this.m_mnuOpenOutputFolder_Click);
            // 
            // m_ctxmnuRemoveSelected
            // 
            this.m_ctxmnuRemoveSelected.Name = "m_ctxmnuRemoveSelected";
            this.m_ctxmnuRemoveSelected.Size = new System.Drawing.Size(194, 26);
            this.m_ctxmnuRemoveSelected.Text = "Remove";
            this.m_ctxmnuRemoveSelected.Click += new System.EventHandler(this.m_btnRemove_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(191, 6);
            // 
            // m_ctxmnuAddUrl
            // 
            this.m_ctxmnuAddUrl.Name = "m_ctxmnuAddUrl";
            this.m_ctxmnuAddUrl.Size = new System.Drawing.Size(194, 26);
            this.m_ctxmnuAddUrl.Text = "Add";
            this.m_ctxmnuAddUrl.Click += new System.EventHandler(this.m_mnuFileAdd_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(191, 6);
            // 
            // m_ctxmnuExit
            // 
            this.m_ctxmnuExit.Image = ((System.Drawing.Image)(resources.GetObject("m_ctxmnuExit.Image")));
            this.m_ctxmnuExit.Name = "m_ctxmnuExit";
            this.m_ctxmnuExit.Size = new System.Drawing.Size(194, 26);
            this.m_ctxmnuExit.Text = "E&xit";
            this.m_ctxmnuExit.Click += new System.EventHandler(this.m_mnuFileExit_Click);
            // 
            // m_btnAddUrl
            // 
            this.m_btnAddUrl.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAddUrl.Image")));
            this.m_btnAddUrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnAddUrl.Location = new System.Drawing.Point(10, 29);
            this.m_btnAddUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnAddUrl.Name = "m_btnAddUrl";
            this.m_btnAddUrl.Size = new System.Drawing.Size(136, 33);
            this.m_btnAddUrl.TabIndex = 1;
            this.m_btnAddUrl.Text = "Add && Start";
            this.m_btnAddUrl.UseVisualStyleBackColor = true;
            this.m_btnAddUrl.Click += new System.EventHandler(this.m_btnAddUrl_Click);
            // 
            // m_spliMain
            // 
            this.m_spliMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_spliMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_spliMain.Location = new System.Drawing.Point(8, 103);
            this.m_spliMain.Name = "m_spliMain";
            this.m_spliMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_spliMain.Panel1
            // 
            this.m_spliMain.Panel1.Controls.Add(this.m_listUrls);
            // 
            // m_spliMain.Panel2
            // 
            this.m_spliMain.Panel2.Controls.Add(this.m_DownloaderUserControl);
            this.m_spliMain.Size = new System.Drawing.Size(963, 421);
            this.m_spliMain.SplitterDistance = 159;
            this.m_spliMain.TabIndex = 7;
            // 
            // m_btnClearList
            // 
            this.m_btnClearList.Image = ((System.Drawing.Image)(resources.GetObject("m_btnClearList.Image")));
            this.m_btnClearList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnClearList.Location = new System.Drawing.Point(298, 64);
            this.m_btnClearList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnClearList.Name = "m_btnClearList";
            this.m_btnClearList.Size = new System.Drawing.Size(136, 33);
            this.m_btnClearList.TabIndex = 4;
            this.m_btnClearList.Text = "Remove All";
            this.m_btnClearList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnClearList.UseVisualStyleBackColor = true;
            this.m_btnClearList.Click += new System.EventHandler(this.m_btnClearList_Click);
            // 
            // m_lnkOutputFolder
            // 
            this.m_lnkOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkOutputFolder.BackColor = System.Drawing.SystemColors.Window;
            this.m_lnkOutputFolder.Location = new System.Drawing.Point(441, 65);
            this.m_lnkOutputFolder.Name = "m_lnkOutputFolder";
            this.m_lnkOutputFolder.Size = new System.Drawing.Size(411, 32);
            this.m_lnkOutputFolder.TabIndex = 6;
            this.m_lnkOutputFolder.TabStop = true;
            this.m_lnkOutputFolder.Text = "Output Folder";
            this.m_lnkOutputFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lnkOutputFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkOutputFolder_LinkClicked);
            // 
            // m_btnPause
            // 
            this.m_btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnPause.ImageIndex = 0;
            this.m_btnPause.ImageList = this.m_imageListStartStop;
            this.m_btnPause.Location = new System.Drawing.Point(10, 64);
            this.m_btnPause.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnPause.Name = "m_btnPause";
            this.m_btnPause.Size = new System.Drawing.Size(136, 33);
            this.m_btnPause.TabIndex = 2;
            this.m_btnPause.Text = "Start";
            this.m_btnPause.UseVisualStyleBackColor = true;
            this.m_btnPause.Click += new System.EventHandler(this.m_btnPause_Click);
            // 
            // m_imageListStartStop
            // 
            this.m_imageListStartStop.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListStartStop.ImageStream")));
            this.m_imageListStartStop.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListStartStop.Images.SetKeyName(0, "play_on.PNG");
            this.m_imageListStartStop.Images.SetKeyName(1, "pause_on.PNG");
            this.m_imageListStartStop.Images.SetKeyName(2, "stop_on.PNG");
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("m_btnRemove.Image")));
            this.m_btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnRemove.Location = new System.Drawing.Point(154, 64);
            this.m_btnRemove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.Size = new System.Drawing.Size(136, 33);
            this.m_btnRemove.TabIndex = 3;
            this.m_btnRemove.Text = "Remove Sel";
            this.m_btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnRemove.UseVisualStyleBackColor = true;
            this.m_btnRemove.Click += new System.EventHandler(this.m_btnRemove_Click);
            // 
            // m_btnBrowseForFolder
            // 
            this.m_btnBrowseForFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseForFolder.Location = new System.Drawing.Point(858, 65);
            this.m_btnBrowseForFolder.Name = "m_btnBrowseForFolder";
            this.m_btnBrowseForFolder.Size = new System.Drawing.Size(111, 33);
            this.m_btnBrowseForFolder.TabIndex = 5;
            this.m_btnBrowseForFolder.Text = "Change...";
            this.m_btnBrowseForFolder.UseVisualStyleBackColor = true;
            this.m_btnBrowseForFolder.Click += new System.EventHandler(this.m_btnBrowseForFolder_Click);
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // m_ctxmnuDownloadAgain
            // 
            this.m_ctxmnuDownloadAgain.Image = ((System.Drawing.Image)(resources.GetObject("m_ctxmnuDownloadAgain.Image")));
            this.m_ctxmnuDownloadAgain.Name = "m_ctxmnuDownloadAgain";
            this.m_ctxmnuDownloadAgain.Size = new System.Drawing.Size(194, 26);
            this.m_ctxmnuDownloadAgain.Text = "Download Again";
            this.m_ctxmnuDownloadAgain.Click += new System.EventHandler(this.m_ctxmnuDownloadAgain_Click);
            // 
            // m_DownloaderUserControl
            // 
            this.m_DownloaderUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DownloaderUserControl.Location = new System.Drawing.Point(0, 0);
            this.m_DownloaderUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_DownloaderUserControl.Name = "m_DownloaderUserControl";
            this.m_DownloaderUserControl.Size = new System.Drawing.Size(959, 254);
            this.m_DownloaderUserControl.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.m_btnBrowseForFolder);
            this.Controls.Add(this.m_btnRemove);
            this.Controls.Add(this.m_btnPause);
            this.Controls.Add(this.m_lnkOutputFolder);
            this.Controls.Add(this.m_btnClearList);
            this.Controls.Add(this.m_spliMain);
            this.Controls.Add(this.m_btnAddUrl);
            this.Controls.Add(this.m_btnUpdate);
            this.Controls.Add(this.m_statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube - Download";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.m_ContextMenuList.ResumeLayout(false);
            this.m_spliMain.Panel1.ResumeLayout(false);
            this.m_spliMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_spliMain)).EndInit();
            this.m_spliMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip m_statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_Status1;
        private System.Windows.Forms.ToolStripStatusLabel m_Status2;
        private System.Windows.Forms.ToolStripProgressBar m_StatusProgress;
        private System.Windows.Forms.Button m_btnUpdate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem m_mnuTools;
        private System.Windows.Forms.ToolStripMenuItem m_mnuToolsSettings;
        private System.Windows.Forms.ToolStripMenuItem m_mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem m_mnuHelpAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem m_mnuToolsOutputFolder;
        private DownloaderUserControl m_DownloaderUserControl;
        private System.Windows.Forms.ListView m_listUrls;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button m_btnAddUrl;
        private System.Windows.Forms.SplitContainer m_spliMain;
        private System.Windows.Forms.Button m_btnClearList;
        private System.Windows.Forms.LinkLabel m_lnkOutputFolder;
        private System.Windows.Forms.Button m_btnPause;
        private System.Windows.Forms.Button m_btnRemove;
        private System.Windows.Forms.ImageList m_imageListStartStop;
        private System.Windows.Forms.Button m_btnBrowseForFolder;
        private System.Windows.Forms.ErrorProvider m_errorProvider;
        private System.Windows.Forms.ToolStripMenuItem m_mnuFileAdd;
        private System.Windows.Forms.ToolStripMenuItem m_mnuToolsUpdateDL;
        private System.Windows.Forms.ContextMenuStrip m_ContextMenuList;
        private System.Windows.Forms.ToolStripMenuItem m_ctxmnuOpenSelectedFile;
        private System.Windows.Forms.ToolStripMenuItem m_ctxmnuOpenOutputFolder;
        private System.Windows.Forms.ToolStripMenuItem m_ctxmnuRemoveSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem m_ctxmnuAddUrl;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem m_ctxmnuExit;
        private System.Windows.Forms.ToolStripMenuItem m_ctxmnuDownloadAgain;
    }
}

