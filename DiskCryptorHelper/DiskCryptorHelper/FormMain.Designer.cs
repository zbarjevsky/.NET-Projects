namespace DiskCryptorHelper
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
            this.m_listDrives = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_btnMountAll = new System.Windows.Forms.Button();
            this.m_btnUnmoutAll = new System.Windows.Forms.Button();
            this.m_lblPwd = new System.Windows.Forms.Label();
            this.m_txtPwd = new System.Windows.Forms.TextBox();
            this.m_btnMount = new System.Windows.Forms.Button();
            this.m_lblSelected = new System.Windows.Forms.Label();
            this.m_cmbAvailableDriveLetters = new System.Windows.Forms.ComboBox();
            this.m_btnReload = new System.Windows.Forms.Button();
            this.m_txtLog = new System.Windows.Forms.RichTextBox();
            this.m_btnUnmount = new System.Windows.Forms.Button();
            this.m_sysIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.m_sysIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuEject = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuPlaceholder = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuUnmountAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuFileAttachVHD = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFileOpenVHD_File = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFileCreateVHD = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_btnUnmountAllandBSOD = new System.Windows.Forms.Button();
            this.m_btnEject = new System.Windows.Forms.Button();
            this.m_splitMain = new System.Windows.Forms.SplitContainer();
            this.m_btnDetach = new System.Windows.Forms.Button();
            this.m_chkPermanent = new System.Windows.Forms.CheckBox();
            this.m_lblVHD_File = new System.Windows.Forms.Label();
            this.m_txtVHD_FileName = new System.Windows.Forms.TextBox();
            this.m_btnAttachVHD = new System.Windows.Forms.Button();
            this.m_splitDisks = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_treeDrives = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.m_menuStripMain = new System.Windows.Forms.MenuStrip();
            this.m_mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuOptionsHideWhenMinimized = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuOptionsVHD = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.hideDriveLetterControl1 = new DiskCryptorHelper.HideDriveLetterControl();
            this.m_btnOpenVHD = new System.Windows.Forms.Button();
            this.m_sysIconMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).BeginInit();
            this.m_splitMain.Panel1.SuspendLayout();
            this.m_splitMain.Panel2.SuspendLayout();
            this.m_splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitDisks)).BeginInit();
            this.m_splitDisks.Panel1.SuspendLayout();
            this.m_splitDisks.Panel2.SuspendLayout();
            this.m_splitDisks.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_menuStripMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_listDrives
            // 
            this.m_listDrives.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.m_listDrives.FullRowSelect = true;
            this.m_listDrives.GridLines = true;
            this.m_listDrives.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listDrives.HideSelection = false;
            this.m_listDrives.Location = new System.Drawing.Point(6, 6);
            this.m_listDrives.MultiSelect = false;
            this.m_listDrives.Name = "m_listDrives";
            this.m_listDrives.Size = new System.Drawing.Size(432, 181);
            this.m_listDrives.TabIndex = 0;
            this.m_listDrives.UseCompatibleStateImageBehavior = false;
            this.m_listDrives.View = System.Windows.Forms.View.Details;
            this.m_listDrives.SelectedIndexChanged += new System.EventHandler(this.m_listDrives_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Volume";
            this.columnHeader1.Width = 89;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Mount Point";
            this.columnHeader2.Width = 102;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            this.columnHeader3.Width = 71;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Status";
            this.columnHeader4.Width = 139;
            // 
            // m_btnMountAll
            // 
            this.m_btnMountAll.Location = new System.Drawing.Point(448, 219);
            this.m_btnMountAll.Name = "m_btnMountAll";
            this.m_btnMountAll.Size = new System.Drawing.Size(89, 23);
            this.m_btnMountAll.TabIndex = 16;
            this.m_btnMountAll.Text = "Mount All";
            this.m_btnMountAll.UseVisualStyleBackColor = true;
            this.m_btnMountAll.Click += new System.EventHandler(this.m_btnMountAll_Click);
            // 
            // m_btnUnmoutAll
            // 
            this.m_btnUnmoutAll.Location = new System.Drawing.Point(448, 192);
            this.m_btnUnmoutAll.Name = "m_btnUnmoutAll";
            this.m_btnUnmoutAll.Size = new System.Drawing.Size(89, 23);
            this.m_btnUnmoutAll.TabIndex = 15;
            this.m_btnUnmoutAll.Text = "UnMount All";
            this.m_btnUnmoutAll.UseVisualStyleBackColor = true;
            this.m_btnUnmoutAll.Click += new System.EventHandler(this.m_btnUnmoutAll_Click);
            // 
            // m_lblPwd
            // 
            this.m_lblPwd.AutoSize = true;
            this.m_lblPwd.Location = new System.Drawing.Point(10, 227);
            this.m_lblPwd.Name = "m_lblPwd";
            this.m_lblPwd.Size = new System.Drawing.Size(59, 13);
            this.m_lblPwd.TabIndex = 2;
            this.m_lblPwd.Text = "Password: ";
            // 
            // m_txtPwd
            // 
            this.m_txtPwd.Location = new System.Drawing.Point(75, 224);
            this.m_txtPwd.Name = "m_txtPwd";
            this.m_txtPwd.Size = new System.Drawing.Size(100, 20);
            this.m_txtPwd.TabIndex = 3;
            this.m_txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_txtPwd.UseSystemPasswordChar = true;
            // 
            // m_btnMount
            // 
            this.m_btnMount.Location = new System.Drawing.Point(246, 222);
            this.m_btnMount.Name = "m_btnMount";
            this.m_btnMount.Size = new System.Drawing.Size(93, 23);
            this.m_btnMount.TabIndex = 5;
            this.m_btnMount.Text = "Mount As:";
            this.m_btnMount.UseVisualStyleBackColor = true;
            this.m_btnMount.Click += new System.EventHandler(this.m_btnMount_Click);
            // 
            // m_lblSelected
            // 
            this.m_lblSelected.AutoSize = true;
            this.m_lblSelected.Location = new System.Drawing.Point(7, 197);
            this.m_lblSelected.Name = "m_lblSelected";
            this.m_lblSelected.Size = new System.Drawing.Size(67, 13);
            this.m_lblSelected.TabIndex = 1;
            this.m_lblSelected.Text = "Slected: ???";
            // 
            // m_cmbAvailableDriveLetters
            // 
            this.m_cmbAvailableDriveLetters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAvailableDriveLetters.FormattingEnabled = true;
            this.m_cmbAvailableDriveLetters.Location = new System.Drawing.Point(191, 224);
            this.m_cmbAvailableDriveLetters.Name = "m_cmbAvailableDriveLetters";
            this.m_cmbAvailableDriveLetters.Size = new System.Drawing.Size(40, 21);
            this.m_cmbAvailableDriveLetters.TabIndex = 4;
            this.m_cmbAvailableDriveLetters.SelectedIndexChanged += new System.EventHandler(this.m_cmbAvailableDriveLetters_SelectedIndexChanged);
            // 
            // m_btnReload
            // 
            this.m_btnReload.Location = new System.Drawing.Point(448, 5);
            this.m_btnReload.Name = "m_btnReload";
            this.m_btnReload.Size = new System.Drawing.Size(89, 23);
            this.m_btnReload.TabIndex = 13;
            this.m_btnReload.Text = "Reload";
            this.m_btnReload.UseVisualStyleBackColor = true;
            this.m_btnReload.Click += new System.EventHandler(this.m_btnReload_Click);
            // 
            // m_txtLog
            // 
            this.m_txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_txtLog.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_txtLog.Location = new System.Drawing.Point(9, 307);
            this.m_txtLog.Name = "m_txtLog";
            this.m_txtLog.ReadOnly = true;
            this.m_txtLog.Size = new System.Drawing.Size(532, 195);
            this.m_txtLog.TabIndex = 17;
            this.m_txtLog.Text = "";
            this.m_txtLog.WordWrap = false;
            // 
            // m_btnUnmount
            // 
            this.m_btnUnmount.Location = new System.Drawing.Point(345, 222);
            this.m_btnUnmount.Name = "m_btnUnmount";
            this.m_btnUnmount.Size = new System.Drawing.Size(93, 23);
            this.m_btnUnmount.TabIndex = 6;
            this.m_btnUnmount.Text = "UnMount";
            this.m_btnUnmount.UseVisualStyleBackColor = true;
            this.m_btnUnmount.Click += new System.EventHandler(this.m_btnUnmount_Click);
            // 
            // m_sysIcon
            // 
            this.m_sysIcon.ContextMenuStrip = this.m_sysIconMenu;
            this.m_sysIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("m_sysIcon.Icon")));
            this.m_sysIcon.Text = "XaXa";
            this.m_sysIcon.Visible = true;
            this.m_sysIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_sysIcon_MouseDoubleClick);
            // 
            // m_sysIconMenu
            // 
            this.m_sysIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuShow,
            this.toolStripMenuItem2,
            this.m_mnuEject,
            this.m_mnuUnmountAll,
            this.toolStripMenuItem1,
            this.m_mnuFileAttachVHD,
            this.m_mnuFileCreateVHD,
            this.toolStripMenuItem3,
            this.m_mnuExit});
            this.m_sysIconMenu.Name = "m_sysIconMenu";
            this.m_sysIconMenu.Size = new System.Drawing.Size(177, 154);
            // 
            // m_mnuShow
            // 
            this.m_mnuShow.Name = "m_mnuShow";
            this.m_mnuShow.Size = new System.Drawing.Size(176, 22);
            this.m_mnuShow.Text = "&Show";
            this.m_mnuShow.Click += new System.EventHandler(this.m_mnuShow_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(173, 6);
            // 
            // m_mnuEject
            // 
            this.m_mnuEject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuPlaceholder});
            this.m_mnuEject.Name = "m_mnuEject";
            this.m_mnuEject.Size = new System.Drawing.Size(176, 22);
            this.m_mnuEject.Text = "UnMount and Eject";
            // 
            // m_mnuPlaceholder
            // 
            this.m_mnuPlaceholder.Name = "m_mnuPlaceholder";
            this.m_mnuPlaceholder.Size = new System.Drawing.Size(152, 22);
            this.m_mnuPlaceholder.Text = "<placeholder>";
            // 
            // m_mnuUnmountAll
            // 
            this.m_mnuUnmountAll.Name = "m_mnuUnmountAll";
            this.m_mnuUnmountAll.Size = new System.Drawing.Size(176, 22);
            this.m_mnuUnmountAll.Text = "&Unmount All";
            this.m_mnuUnmountAll.Click += new System.EventHandler(this.m_mnuUnmountAll_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(173, 6);
            // 
            // m_mnuFileAttachVHD
            // 
            this.m_mnuFileAttachVHD.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFileOpenVHD_File});
            this.m_mnuFileAttachVHD.Name = "m_mnuFileAttachVHD";
            this.m_mnuFileAttachVHD.Size = new System.Drawing.Size(176, 22);
            this.m_mnuFileAttachVHD.Text = "Attach VHD";
            // 
            // m_mnuFileOpenVHD_File
            // 
            this.m_mnuFileOpenVHD_File.Name = "m_mnuFileOpenVHD_File";
            this.m_mnuFileOpenVHD_File.Size = new System.Drawing.Size(133, 22);
            this.m_mnuFileOpenVHD_File.Text = "Open File...";
            this.m_mnuFileOpenVHD_File.Click += new System.EventHandler(this.m_mnuFileOpenVHD_File_Click);
            // 
            // m_mnuFileCreateVHD
            // 
            this.m_mnuFileCreateVHD.Name = "m_mnuFileCreateVHD";
            this.m_mnuFileCreateVHD.Size = new System.Drawing.Size(176, 22);
            this.m_mnuFileCreateVHD.Text = "Create VHD";
            this.m_mnuFileCreateVHD.Click += new System.EventHandler(this.m_mnuFileCreateVHD_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(173, 6);
            // 
            // m_mnuExit
            // 
            this.m_mnuExit.Name = "m_mnuExit";
            this.m_mnuExit.Size = new System.Drawing.Size(176, 22);
            this.m_mnuExit.Text = "E&xit";
            this.m_mnuExit.Click += new System.EventHandler(this.m_mnuExit_Click);
            // 
            // m_btnUnmountAllandBSOD
            // 
            this.m_btnUnmountAllandBSOD.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_btnUnmountAllandBSOD.BackgroundImage")));
            this.m_btnUnmountAllandBSOD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_btnUnmountAllandBSOD.Location = new System.Drawing.Point(448, 60);
            this.m_btnUnmountAllandBSOD.Name = "m_btnUnmountAllandBSOD";
            this.m_btnUnmountAllandBSOD.Size = new System.Drawing.Size(89, 96);
            this.m_btnUnmountAllandBSOD.TabIndex = 14;
            this.m_btnUnmountAllandBSOD.UseVisualStyleBackColor = true;
            this.m_btnUnmountAllandBSOD.Click += new System.EventHandler(this.m_btnUnmountAllandBSOD_Click);
            // 
            // m_btnEject
            // 
            this.m_btnEject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEject.Location = new System.Drawing.Point(3, 3);
            this.m_btnEject.Name = "m_btnEject";
            this.m_btnEject.Size = new System.Drawing.Size(309, 23);
            this.m_btnEject.TabIndex = 0;
            this.m_btnEject.Text = "UnMount and Eject";
            this.m_btnEject.UseVisualStyleBackColor = true;
            this.m_btnEject.Click += new System.EventHandler(this.m_btnEject_Click);
            // 
            // m_splitMain
            // 
            this.m_splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_splitMain.Location = new System.Drawing.Point(0, 24);
            this.m_splitMain.Name = "m_splitMain";
            // 
            // m_splitMain.Panel1
            // 
            this.m_splitMain.Panel1.Controls.Add(this.m_btnOpenVHD);
            this.m_splitMain.Panel1.Controls.Add(this.m_btnDetach);
            this.m_splitMain.Panel1.Controls.Add(this.m_chkPermanent);
            this.m_splitMain.Panel1.Controls.Add(this.m_lblVHD_File);
            this.m_splitMain.Panel1.Controls.Add(this.m_txtVHD_FileName);
            this.m_splitMain.Panel1.Controls.Add(this.m_btnAttachVHD);
            this.m_splitMain.Panel1.Controls.Add(this.m_listDrives);
            this.m_splitMain.Panel1.Controls.Add(this.m_btnMountAll);
            this.m_splitMain.Panel1.Controls.Add(this.m_btnUnmoutAll);
            this.m_splitMain.Panel1.Controls.Add(this.m_btnUnmountAllandBSOD);
            this.m_splitMain.Panel1.Controls.Add(this.m_lblPwd);
            this.m_splitMain.Panel1.Controls.Add(this.m_btnUnmount);
            this.m_splitMain.Panel1.Controls.Add(this.m_txtPwd);
            this.m_splitMain.Panel1.Controls.Add(this.m_txtLog);
            this.m_splitMain.Panel1.Controls.Add(this.m_btnMount);
            this.m_splitMain.Panel1.Controls.Add(this.m_btnReload);
            this.m_splitMain.Panel1.Controls.Add(this.m_lblSelected);
            this.m_splitMain.Panel1.Controls.Add(this.m_cmbAvailableDriveLetters);
            // 
            // m_splitMain.Panel2
            // 
            this.m_splitMain.Panel2.Controls.Add(this.m_splitDisks);
            this.m_splitMain.Size = new System.Drawing.Size(984, 516);
            this.m_splitMain.SplitterDistance = 550;
            this.m_splitMain.TabIndex = 1;
            // 
            // m_btnDetach
            // 
            this.m_btnDetach.Location = new System.Drawing.Point(319, 277);
            this.m_btnDetach.Name = "m_btnDetach";
            this.m_btnDetach.Size = new System.Drawing.Size(119, 23);
            this.m_btnDetach.TabIndex = 12;
            this.m_btnDetach.Text = "UnMount && Detach";
            this.m_btnDetach.UseVisualStyleBackColor = true;
            this.m_btnDetach.Click += new System.EventHandler(this.m_btnDetach_Click);
            // 
            // m_chkPermanent
            // 
            this.m_chkPermanent.Location = new System.Drawing.Point(75, 278);
            this.m_chkPermanent.Name = "m_chkPermanent";
            this.m_chkPermanent.Size = new System.Drawing.Size(93, 23);
            this.m_chkPermanent.TabIndex = 10;
            this.m_chkPermanent.Text = "Permanent";
            this.m_chkPermanent.UseVisualStyleBackColor = true;
            // 
            // m_lblVHD_File
            // 
            this.m_lblVHD_File.AutoSize = true;
            this.m_lblVHD_File.Location = new System.Drawing.Point(10, 256);
            this.m_lblVHD_File.Name = "m_lblVHD_File";
            this.m_lblVHD_File.Size = new System.Drawing.Size(55, 13);
            this.m_lblVHD_File.TabIndex = 7;
            this.m_lblVHD_File.Text = "VHD File: ";
            // 
            // m_txtVHD_FileName
            // 
            this.m_txtVHD_FileName.Location = new System.Drawing.Point(75, 253);
            this.m_txtVHD_FileName.Name = "m_txtVHD_FileName";
            this.m_txtVHD_FileName.Size = new System.Drawing.Size(329, 20);
            this.m_txtVHD_FileName.TabIndex = 8;
            // 
            // m_btnAttachVHD
            // 
            this.m_btnAttachVHD.Location = new System.Drawing.Point(176, 277);
            this.m_btnAttachVHD.Name = "m_btnAttachVHD";
            this.m_btnAttachVHD.Size = new System.Drawing.Size(123, 23);
            this.m_btnAttachVHD.TabIndex = 11;
            this.m_btnAttachVHD.Text = "Attach && Mount";
            this.m_btnAttachVHD.UseVisualStyleBackColor = true;
            this.m_btnAttachVHD.Click += new System.EventHandler(this.m_btnAttachVHD_Click);
            // 
            // m_splitDisks
            // 
            this.m_splitDisks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitDisks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitDisks.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_splitDisks.Location = new System.Drawing.Point(0, 0);
            this.m_splitDisks.Name = "m_splitDisks";
            // 
            // m_splitDisks.Panel1
            // 
            this.m_splitDisks.Panel1.Controls.Add(this.hideDriveLetterControl1);
            // 
            // m_splitDisks.Panel2
            // 
            this.m_splitDisks.Panel2.Controls.Add(this.panel1);
            this.m_splitDisks.Panel2.Controls.Add(this.m_treeDrives);
            this.m_splitDisks.Panel2.Controls.Add(this.label1);
            this.m_splitDisks.Size = new System.Drawing.Size(428, 514);
            this.m_splitDisks.SplitterDistance = 105;
            this.m_splitDisks.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnEject);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 323);
            this.panel1.TabIndex = 3;
            // 
            // m_treeDrives
            // 
            this.m_treeDrives.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_treeDrives.FullRowSelect = true;
            this.m_treeDrives.HideSelection = false;
            this.m_treeDrives.Location = new System.Drawing.Point(0, 18);
            this.m_treeDrives.Name = "m_treeDrives";
            this.m_treeDrives.Size = new System.Drawing.Size(315, 169);
            this.m_treeDrives.TabIndex = 1;
            this.m_treeDrives.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_treeDrives_AfterSelect);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plugged USB Drives:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_menuStripMain
            // 
            this.m_menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFile,
            this.m_mnuOptions});
            this.m_menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.m_menuStripMain.Name = "m_menuStripMain";
            this.m_menuStripMain.Size = new System.Drawing.Size(984, 24);
            this.m_menuStripMain.TabIndex = 0;
            this.m_menuStripMain.Text = "menuStrip1";
            // 
            // m_mnuFile
            // 
            this.m_mnuFile.Name = "m_mnuFile";
            this.m_mnuFile.Size = new System.Drawing.Size(37, 20);
            this.m_mnuFile.Text = "&File";
            // 
            // m_mnuOptions
            // 
            this.m_mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuOptionsHideWhenMinimized,
            this.m_mnuOptionsVHD});
            this.m_mnuOptions.Name = "m_mnuOptions";
            this.m_mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.m_mnuOptions.Text = "Options";
            // 
            // m_mnuOptionsHideWhenMinimized
            // 
            this.m_mnuOptionsHideWhenMinimized.Name = "m_mnuOptionsHideWhenMinimized";
            this.m_mnuOptionsHideWhenMinimized.Size = new System.Drawing.Size(202, 22);
            this.m_mnuOptionsHideWhenMinimized.Text = "Hide When Minimized";
            this.m_mnuOptionsHideWhenMinimized.Click += new System.EventHandler(this.m_mnuOptionsHideWhenMinimized_Click);
            // 
            // m_mnuOptionsVHD
            // 
            this.m_mnuOptionsVHD.Name = "m_mnuOptionsVHD";
            this.m_mnuOptionsVHD.Size = new System.Drawing.Size(202, 22);
            this.m_mnuOptionsVHD.Text = "Virtual Hard Drive (VHD)";
            this.m_mnuOptionsVHD.Click += new System.EventHandler(this.m_mnuOptionsVHD_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.m_status2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // m_status1
            // 
            this.m_status1.Name = "m_status1";
            this.m_status1.Size = new System.Drawing.Size(39, 17);
            this.m_status1.Text = "Ready";
            // 
            // m_status2
            // 
            this.m_status2.Name = "m_status2";
            this.m_status2.Size = new System.Drawing.Size(930, 17);
            this.m_status2.Spring = true;
            this.m_status2.Text = "...";
            // 
            // hideDriveLetterControl1
            // 
            this.hideDriveLetterControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hideDriveLetterControl1.Location = new System.Drawing.Point(0, 0);
            this.hideDriveLetterControl1.Name = "hideDriveLetterControl1";
            this.hideDriveLetterControl1.Size = new System.Drawing.Size(101, 510);
            this.hideDriveLetterControl1.TabIndex = 0;
            // 
            // m_btnOpenVHD
            // 
            this.m_btnOpenVHD.Location = new System.Drawing.Point(410, 251);
            this.m_btnOpenVHD.Name = "m_btnOpenVHD";
            this.m_btnOpenVHD.Size = new System.Drawing.Size(28, 23);
            this.m_btnOpenVHD.TabIndex = 9;
            this.m_btnOpenVHD.Text = "...";
            this.m_btnOpenVHD.UseVisualStyleBackColor = true;
            this.m_btnOpenVHD.Click += new System.EventHandler(this.m_btnOpenVHD_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.m_splitMain);
            this.Controls.Add(this.m_menuStripMain);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_menuStripMain;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DiskCryptor Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.m_sysIconMenu.ResumeLayout(false);
            this.m_splitMain.Panel1.ResumeLayout(false);
            this.m_splitMain.Panel1.PerformLayout();
            this.m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).EndInit();
            this.m_splitMain.ResumeLayout(false);
            this.m_splitDisks.Panel1.ResumeLayout(false);
            this.m_splitDisks.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitDisks)).EndInit();
            this.m_splitDisks.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_menuStripMain.ResumeLayout(false);
            this.m_menuStripMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView m_listDrives;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button m_btnMountAll;
        private System.Windows.Forms.Button m_btnUnmoutAll;
        private System.Windows.Forms.Label m_lblPwd;
        private System.Windows.Forms.TextBox m_txtPwd;
        private System.Windows.Forms.Button m_btnMount;
        private System.Windows.Forms.Label m_lblSelected;
        private System.Windows.Forms.ComboBox m_cmbAvailableDriveLetters;
        private System.Windows.Forms.Button m_btnReload;
        private System.Windows.Forms.RichTextBox m_txtLog;
        private System.Windows.Forms.Button m_btnUnmount;
        private System.Windows.Forms.NotifyIcon m_sysIcon;
        private System.Windows.Forms.ContextMenuStrip m_sysIconMenu;
        private System.Windows.Forms.ToolStripMenuItem m_mnuExit;
        private System.Windows.Forms.ToolStripMenuItem m_mnuUnmountAll;
        private System.Windows.Forms.Button m_btnUnmountAllandBSOD;
        private System.Windows.Forms.Button m_btnEject;
        private System.Windows.Forms.SplitContainer m_splitMain;
        private System.Windows.Forms.ToolStripMenuItem m_mnuEject;
        private System.Windows.Forms.ToolStripMenuItem m_mnuPlaceholder;
        private System.Windows.Forms.MenuStrip m_menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem m_mnuFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.TreeView m_treeDrives;
        private System.Windows.Forms.ToolStripMenuItem m_mnuShow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel m_status1;
        private System.Windows.Forms.ToolStripStatusLabel m_status2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem m_mnuOptionsHideWhenMinimized;
        private System.Windows.Forms.ToolStripMenuItem m_mnuOptionsVHD;
        private System.Windows.Forms.Label m_lblVHD_File;
        private System.Windows.Forms.TextBox m_txtVHD_FileName;
        private System.Windows.Forms.Button m_btnAttachVHD;
        private System.Windows.Forms.ToolStripMenuItem m_mnuFileAttachVHD;
        private System.Windows.Forms.ToolStripMenuItem m_mnuFileOpenVHD_File;
        private System.Windows.Forms.ToolStripMenuItem m_mnuFileCreateVHD;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.CheckBox m_chkPermanent;
        private System.Windows.Forms.SplitContainer m_splitDisks;
        private HideDriveLetterControl hideDriveLetterControl1;
        private System.Windows.Forms.Button m_btnDetach;
        private System.Windows.Forms.Button m_btnOpenVHD;
    }
}

