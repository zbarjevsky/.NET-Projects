namespace MeditationStopWatch
{
	partial class FormStopWatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStopWatch));
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.m_splitContainerImage = new System.Windows.Forms.SplitContainer();
            this.m_toolStripContainerPictureInfo = new System.Windows.Forms.ToolStripContainer();
            this.m_toolStrip_Picture = new System.Windows.Forms.ToolStrip();
            this.m_tsTxt_FileName = new ToolStripSpringTextBox();
            this.m_lblVolume = new MeditationStopWatch.Tools.LabelWithTimeout();
            this.m_btnHideSumbnails = new System.Windows.Forms.Button();
            this.m_imageListBtnHide = new System.Windows.Forms.ImageList(this.components);
            this.m_pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_listThumbnails = new System.Windows.Forms.ListView();
            this.m_imageListThumbnails = new System.Windows.Forms.ImageList(this.components);
            this.m_splitContainerTools = new System.Windows.Forms.SplitContainer();
            this.m_splitClocks = new System.Windows.Forms.SplitContainer();
            this.m_analogClock = new MeditationStopWatch.AnalogClock();
            this.digitalClockCtrl1 = new MeditationStopWatch.DigitalClockCtrl();
            this.m_audioPlayerControl = new MeditationStopWatch.AudioPlayerControl();
            this.m_menuMain = new System.Windows.Forms.MenuStrip();
            this.m_mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFavorites_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFavorites_Organize = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFavorites_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuTools_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.m_statusBar = new System.Windows.Forms.StatusStrip();
            this.m_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_toolStripMain = new System.Windows.Forms.ToolStrip();
            this.m_btnOpenImage = new System.Windows.Forms.ToolStripButton();
            this.m_btnFitWindow = new System.Windows.Forms.ToolStripButton();
            this.m_btnOrigSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_btnPrevImage = new System.Windows.Forms.ToolStripButton();
            this.m_btnNextImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_txtImageIndex = new System.Windows.Forms.ToolStripTextBox();
            this.m_lblImageDesc = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_btnSlideShow = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.m_pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerMain)).BeginInit();
            this.m_splitContainerMain.Panel1.SuspendLayout();
            this.m_splitContainerMain.Panel2.SuspendLayout();
            this.m_splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerImage)).BeginInit();
            this.m_splitContainerImage.Panel1.SuspendLayout();
            this.m_splitContainerImage.Panel2.SuspendLayout();
            this.m_splitContainerImage.SuspendLayout();
            this.m_toolStripContainerPictureInfo.BottomToolStripPanel.SuspendLayout();
            this.m_toolStripContainerPictureInfo.ContentPanel.SuspendLayout();
            this.m_toolStripContainerPictureInfo.SuspendLayout();
            this.m_toolStrip_Picture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerTools)).BeginInit();
            this.m_splitContainerTools.Panel1.SuspendLayout();
            this.m_splitContainerTools.Panel2.SuspendLayout();
            this.m_splitContainerTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitClocks)).BeginInit();
            this.m_splitClocks.Panel1.SuspendLayout();
            this.m_splitClocks.Panel2.SuspendLayout();
            this.m_splitClocks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_analogClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalClockCtrl1)).BeginInit();
            this.m_menuMain.SuspendLayout();
            this.m_statusBar.SuspendLayout();
            this.m_toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Controls.Add(this.m_splitContainerMain);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 53);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(792, 498);
            this.m_pnlMain.TabIndex = 1;
            // 
            // m_splitContainerMain
            // 
            this.m_splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainerMain.Name = "m_splitContainerMain";
            // 
            // m_splitContainerMain.Panel1
            // 
            this.m_splitContainerMain.Panel1.BackColor = System.Drawing.Color.Black;
            this.m_splitContainerMain.Panel1.Controls.Add(this.m_splitContainerImage);
            // 
            // m_splitContainerMain.Panel2
            // 
            this.m_splitContainerMain.Panel2.Controls.Add(this.m_splitContainerTools);
            this.m_splitContainerMain.Size = new System.Drawing.Size(792, 498);
            this.m_splitContainerMain.SplitterDistance = 442;
            this.m_splitContainerMain.TabIndex = 2;
            // 
            // m_splitContainerImage
            // 
            this.m_splitContainerImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainerImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainerImage.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitContainerImage.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainerImage.Name = "m_splitContainerImage";
            this.m_splitContainerImage.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainerImage.Panel1
            // 
            this.m_splitContainerImage.Panel1.Controls.Add(this.m_toolStripContainerPictureInfo);
            this.m_splitContainerImage.Panel1MinSize = 250;
            // 
            // m_splitContainerImage.Panel2
            // 
            this.m_splitContainerImage.Panel2.Controls.Add(this.m_listThumbnails);
            this.m_splitContainerImage.Panel2MinSize = 0;
            this.m_splitContainerImage.Size = new System.Drawing.Size(442, 498);
            this.m_splitContainerImage.SplitterDistance = 338;
            this.m_splitContainerImage.TabIndex = 1;
            // 
            // m_toolStripContainerPictureInfo
            // 
            // 
            // m_toolStripContainerPictureInfo.BottomToolStripPanel
            // 
            this.m_toolStripContainerPictureInfo.BottomToolStripPanel.Controls.Add(this.m_toolStrip_Picture);
            // 
            // m_toolStripContainerPictureInfo.ContentPanel
            // 
            this.m_toolStripContainerPictureInfo.ContentPanel.Controls.Add(this.m_lblVolume);
            this.m_toolStripContainerPictureInfo.ContentPanel.Controls.Add(this.m_btnHideSumbnails);
            this.m_toolStripContainerPictureInfo.ContentPanel.Controls.Add(this.m_pictureBox1);
            this.m_toolStripContainerPictureInfo.ContentPanel.Size = new System.Drawing.Size(438, 284);
            this.m_toolStripContainerPictureInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // m_toolStripContainerPictureInfo.LeftToolStripPanel
            // 
            this.m_toolStripContainerPictureInfo.LeftToolStripPanel.Enabled = false;
            this.m_toolStripContainerPictureInfo.LeftToolStripPanelVisible = false;
            this.m_toolStripContainerPictureInfo.Location = new System.Drawing.Point(0, 0);
            this.m_toolStripContainerPictureInfo.Name = "m_toolStripContainerPictureInfo";
            // 
            // m_toolStripContainerPictureInfo.RightToolStripPanel
            // 
            this.m_toolStripContainerPictureInfo.RightToolStripPanel.Enabled = false;
            this.m_toolStripContainerPictureInfo.RightToolStripPanelVisible = false;
            this.m_toolStripContainerPictureInfo.Size = new System.Drawing.Size(438, 334);
            this.m_toolStripContainerPictureInfo.TabIndex = 2;
            this.m_toolStripContainerPictureInfo.Text = "toolStripContainer1";
            // 
            // m_toolStrip_Picture
            // 
            this.m_toolStrip_Picture.Dock = System.Windows.Forms.DockStyle.None;
            this.m_toolStrip_Picture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsTxt_FileName});
            this.m_toolStrip_Picture.Location = new System.Drawing.Point(0, 0);
            this.m_toolStrip_Picture.Name = "m_toolStrip_Picture";
            this.m_toolStrip_Picture.Padding = new System.Windows.Forms.Padding(0, 2, 1, 0);
            this.m_toolStrip_Picture.Size = new System.Drawing.Size(438, 25);
            this.m_toolStrip_Picture.Stretch = true;
            this.m_toolStrip_Picture.TabIndex = 0;
            // 
            // m_tsTxt_FileName
            // 
            this.m_tsTxt_FileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.m_tsTxt_FileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_tsTxt_FileName.ForeColor = System.Drawing.Color.Silver;
            this.m_tsTxt_FileName.Name = "m_tsTxt_FileName";
            this.m_tsTxt_FileName.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.m_tsTxt_FileName.ReadOnly = true;
            this.m_tsTxt_FileName.Size = new System.Drawing.Size(395, 23);
            this.m_tsTxt_FileName.Text = "Hello I am here";
            // 
            // m_lblVolume
            // 
            this.m_lblVolume.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_lblVolume.AutoSize = true;
            this.m_lblVolume.BackColor = System.Drawing.Color.Transparent;
            this.m_lblVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblVolume.ForeColor = System.Drawing.Color.Lime;
            this.m_lblVolume.Location = new System.Drawing.Point(116, 26);
            this.m_lblVolume.Name = "m_lblVolume";
            this.m_lblVolume.Size = new System.Drawing.Size(208, 42);
            this.m_lblVolume.TabIndex = 2;
            this.m_lblVolume.Text = "Volume 3%";
            // 
            // m_btnHideSumbnails
            // 
            this.m_btnHideSumbnails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnHideSumbnails.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnHideSumbnails.ImageIndex = 0;
            this.m_btnHideSumbnails.ImageList = this.m_imageListBtnHide;
            this.m_btnHideSumbnails.Location = new System.Drawing.Point(3, 261);
            this.m_btnHideSumbnails.Name = "m_btnHideSumbnails";
            this.m_btnHideSumbnails.Size = new System.Drawing.Size(111, 23);
            this.m_btnHideSumbnails.TabIndex = 1;
            this.m_btnHideSumbnails.Text = "Hide Thumbnails";
            this.m_btnHideSumbnails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnHideSumbnails.UseVisualStyleBackColor = true;
            this.m_btnHideSumbnails.Click += new System.EventHandler(this.m_btnHideSumbnails_Click);
            // 
            // m_imageListBtnHide
            // 
            this.m_imageListBtnHide.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListBtnHide.ImageStream")));
            this.m_imageListBtnHide.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListBtnHide.Images.SetKeyName(0, "arrow-down_16.ico");
            this.m_imageListBtnHide.Images.SetKeyName(1, "arrow-up_16.ico");
            // 
            // m_pictureBox1
            // 
            this.m_pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.m_pictureBox1.Name = "m_pictureBox1";
            this.m_pictureBox1.Size = new System.Drawing.Size(438, 284);
            this.m_pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_pictureBox1.TabIndex = 0;
            this.m_pictureBox1.TabStop = false;
            this.m_pictureBox1.Click += new System.EventHandler(this.m_pictureBox1_Click);
            // 
            // m_listThumbnails
            // 
            this.m_listThumbnails.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.m_listThumbnails.BackColor = System.Drawing.Color.Black;
            this.m_listThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_listThumbnails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listThumbnails.ForeColor = System.Drawing.Color.White;
            this.m_listThumbnails.HideSelection = false;
            this.m_listThumbnails.LargeImageList = this.m_imageListThumbnails;
            this.m_listThumbnails.Location = new System.Drawing.Point(0, 0);
            this.m_listThumbnails.MultiSelect = false;
            this.m_listThumbnails.Name = "m_listThumbnails";
            this.m_listThumbnails.Size = new System.Drawing.Size(438, 152);
            this.m_listThumbnails.SmallImageList = this.m_imageListThumbnails;
            this.m_listThumbnails.TabIndex = 0;
            this.m_listThumbnails.UseCompatibleStateImageBehavior = false;
            this.m_listThumbnails.VirtualMode = true;
            this.m_listThumbnails.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.m_listThumbnails_RetrieveVirtualItem);
            this.m_listThumbnails.SelectedIndexChanged += new System.EventHandler(this.m_listThumbnails_SelectedIndexChanged);
            // 
            // m_imageListThumbnails
            // 
            this.m_imageListThumbnails.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.m_imageListThumbnails.ImageSize = new System.Drawing.Size(96, 96);
            this.m_imageListThumbnails.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // m_splitContainerTools
            // 
            this.m_splitContainerTools.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainerTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainerTools.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_splitContainerTools.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainerTools.Name = "m_splitContainerTools";
            this.m_splitContainerTools.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainerTools.Panel1
            // 
            this.m_splitContainerTools.Panel1.Controls.Add(this.m_splitClocks);
            this.m_splitContainerTools.Panel1MinSize = 120;
            // 
            // m_splitContainerTools.Panel2
            // 
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_audioPlayerControl);
            this.m_splitContainerTools.Size = new System.Drawing.Size(346, 498);
            this.m_splitContainerTools.SplitterDistance = 283;
            this.m_splitContainerTools.TabIndex = 1;
            // 
            // m_splitClocks
            // 
            this.m_splitClocks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitClocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitClocks.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitClocks.Location = new System.Drawing.Point(0, 0);
            this.m_splitClocks.Name = "m_splitClocks";
            this.m_splitClocks.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitClocks.Panel1
            // 
            this.m_splitClocks.Panel1.Controls.Add(this.m_analogClock);
            // 
            // m_splitClocks.Panel2
            // 
            this.m_splitClocks.Panel2.Controls.Add(this.digitalClockCtrl1);
            this.m_splitClocks.Size = new System.Drawing.Size(346, 283);
            this.m_splitClocks.SplitterDistance = 219;
            this.m_splitClocks.TabIndex = 1;
            // 
            // m_analogClock
            // 
            this.m_analogClock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_analogClock.Draw1MinuteTicks = true;
            this.m_analogClock.Draw5MinuteTicks = true;
            this.m_analogClock.HourHandColor = System.Drawing.Color.DarkGoldenrod;
            this.m_analogClock.Location = new System.Drawing.Point(0, 0);
            this.m_analogClock.Margin = new System.Windows.Forms.Padding(30);
            this.m_analogClock.MinuteHandColor = System.Drawing.Color.Goldenrod;
            this.m_analogClock.Name = "m_analogClock";
            this.m_analogClock.SecondHandColor = System.Drawing.Color.Red;
            this.m_analogClock.Size = new System.Drawing.Size(342, 215);
            this.m_analogClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_analogClock.SuspendScreenSaver = false;
            this.m_analogClock.TabIndex = 0;
            this.m_analogClock.TabStop = false;
            this.m_analogClock.TicksColor = System.Drawing.Color.Sienna;
            // 
            // digitalClockCtrl1
            // 
            this.digitalClockCtrl1.BackColor = System.Drawing.Color.Black;
            this.digitalClockCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitalClockCtrl1.ForeColor = System.Drawing.Color.LimeGreen;
            this.digitalClockCtrl1.Location = new System.Drawing.Point(0, 0);
            this.digitalClockCtrl1.Name = "digitalClockCtrl1";
            this.digitalClockCtrl1.Size = new System.Drawing.Size(342, 56);
            this.digitalClockCtrl1.TabIndex = 1;
            this.digitalClockCtrl1.TabStop = false;
            // 
            // m_audioPlayerControl
            // 
            this.m_audioPlayerControl.AutoScroll = true;
            this.m_audioPlayerControl.AutoScrollMinSize = new System.Drawing.Size(310, 0);
            this.m_audioPlayerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_audioPlayerControl.Location = new System.Drawing.Point(0, 0);
            this.m_audioPlayerControl.Loop = true;
            this.m_audioPlayerControl.Name = "m_audioPlayerControl";
            this.m_audioPlayerControl.Size = new System.Drawing.Size(342, 207);
            this.m_audioPlayerControl.TabIndex = 0;
            this.m_audioPlayerControl.Volume = 300;
            // 
            // m_menuMain
            // 
            this.m_menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFile,
            this.m_mnuEdit,
            this.m_mnuView,
            this.m_mnuFavorites,
            this.m_mnuTools,
            this.m_mnuHelp});
            this.m_menuMain.Location = new System.Drawing.Point(0, 0);
            this.m_menuMain.Name = "m_menuMain";
            this.m_menuMain.Size = new System.Drawing.Size(792, 24);
            this.m_menuMain.TabIndex = 2;
            this.m_menuMain.Text = "menuStrip1";
            // 
            // m_mnuFile
            // 
            this.m_mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFile_Open,
            this.m_mnuFile_Exit});
            this.m_mnuFile.Name = "m_mnuFile";
            this.m_mnuFile.Size = new System.Drawing.Size(37, 20);
            this.m_mnuFile.Text = "&File";
            // 
            // m_mnuFile_Open
            // 
            this.m_mnuFile_Open.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuFile_Open.Image")));
            this.m_mnuFile_Open.Name = "m_mnuFile_Open";
            this.m_mnuFile_Open.Size = new System.Drawing.Size(139, 22);
            this.m_mnuFile_Open.Text = "&Open Image";
            this.m_mnuFile_Open.Click += new System.EventHandler(this.m_mnuFile_Open_Click);
            // 
            // m_mnuFile_Exit
            // 
            this.m_mnuFile_Exit.Name = "m_mnuFile_Exit";
            this.m_mnuFile_Exit.Size = new System.Drawing.Size(139, 22);
            this.m_mnuFile_Exit.Text = "E&xit";
            this.m_mnuFile_Exit.Click += new System.EventHandler(this.m_mnuFile_Exit_Click);
            // 
            // m_mnuEdit
            // 
            this.m_mnuEdit.Name = "m_mnuEdit";
            this.m_mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.m_mnuEdit.Text = "&Edit";
            // 
            // m_mnuView
            // 
            this.m_mnuView.Name = "m_mnuView";
            this.m_mnuView.Size = new System.Drawing.Size(44, 20);
            this.m_mnuView.Text = "&View";
            // 
            // m_mnuFavorites
            // 
            this.m_mnuFavorites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFavorites_Add,
            this.m_mnuFavorites_Organize,
            this.m_mnuFavorites_Sep1});
            this.m_mnuFavorites.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuFavorites.Image")));
            this.m_mnuFavorites.Name = "m_mnuFavorites";
            this.m_mnuFavorites.Size = new System.Drawing.Size(82, 20);
            this.m_mnuFavorites.Text = "F&avorites";
            // 
            // m_mnuFavorites_Add
            // 
            this.m_mnuFavorites_Add.Name = "m_mnuFavorites_Add";
            this.m_mnuFavorites_Add.Size = new System.Drawing.Size(171, 22);
            this.m_mnuFavorites_Add.Text = "&Add to Favorites";
            this.m_mnuFavorites_Add.Click += new System.EventHandler(this.m_mnuFavorites_Add_Click);
            // 
            // m_mnuFavorites_Organize
            // 
            this.m_mnuFavorites_Organize.Name = "m_mnuFavorites_Organize";
            this.m_mnuFavorites_Organize.Size = new System.Drawing.Size(171, 22);
            this.m_mnuFavorites_Organize.Text = "&Organize Favorites";
            this.m_mnuFavorites_Organize.Click += new System.EventHandler(this.m_mnuFavorites_Organize_Click);
            // 
            // m_mnuFavorites_Sep1
            // 
            this.m_mnuFavorites_Sep1.Name = "m_mnuFavorites_Sep1";
            this.m_mnuFavorites_Sep1.Size = new System.Drawing.Size(168, 6);
            // 
            // m_mnuTools
            // 
            this.m_mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuTools_Options});
            this.m_mnuTools.Name = "m_mnuTools";
            this.m_mnuTools.Size = new System.Drawing.Size(48, 20);
            this.m_mnuTools.Text = "&Tools";
            // 
            // m_mnuTools_Options
            // 
            this.m_mnuTools_Options.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuTools_Options.Image")));
            this.m_mnuTools_Options.Name = "m_mnuTools_Options";
            this.m_mnuTools_Options.Size = new System.Drawing.Size(116, 22);
            this.m_mnuTools_Options.Text = "&Options";
            this.m_mnuTools_Options.Click += new System.EventHandler(this.m_mnuTools_Options_Click);
            // 
            // m_mnuHelp
            // 
            this.m_mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuHelp_About});
            this.m_mnuHelp.Name = "m_mnuHelp";
            this.m_mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.m_mnuHelp.Text = "&Help";
            // 
            // m_mnuHelp_About
            // 
            this.m_mnuHelp_About.Name = "m_mnuHelp_About";
            this.m_mnuHelp_About.Size = new System.Drawing.Size(107, 22);
            this.m_mnuHelp_About.Text = "&About";
            this.m_mnuHelp_About.Click += new System.EventHandler(this.m_mnuHelp_About_Click);
            // 
            // m_statusBar
            // 
            this.m_statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel1,
            this.m_toolStripStatusLabel2,
            this.m_toolStripProgressBar1});
            this.m_statusBar.Location = new System.Drawing.Point(0, 551);
            this.m_statusBar.Name = "m_statusBar";
            this.m_statusBar.Size = new System.Drawing.Size(792, 22);
            this.m_statusBar.TabIndex = 4;
            this.m_statusBar.Text = "statusStrip1";
            // 
            // m_toolStripStatusLabel1
            // 
            this.m_toolStripStatusLabel1.Name = "m_toolStripStatusLabel1";
            this.m_toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.m_toolStripStatusLabel1.Text = "Ready";
            // 
            // m_toolStripStatusLabel2
            // 
            this.m_toolStripStatusLabel2.Name = "m_toolStripStatusLabel2";
            this.m_toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // m_toolStripProgressBar1
            // 
            this.m_toolStripProgressBar1.Name = "m_toolStripProgressBar1";
            this.m_toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.DefaultExt = "jpg";
            this.m_openFileDialog.FileName = "*.jpg";
            this.m_openFileDialog.Filter = "Picture Files(*.jpg)|*.jpg|All files|*.*";
            this.m_openFileDialog.Multiselect = true;
            // 
            // m_toolStripMain
            // 
            this.m_toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_toolStripMain.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.m_toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_btnOpenImage,
            this.m_btnFitWindow,
            this.m_btnOrigSize,
            this.toolStripSeparator1,
            this.m_btnPrevImage,
            this.m_btnNextImage,
            this.toolStripSeparator2,
            this.m_txtImageIndex,
            this.m_lblImageDesc,
            this.toolStripSeparator3,
            this.m_btnSlideShow});
            this.m_toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.m_toolStripMain.Name = "m_toolStripMain";
            this.m_toolStripMain.Size = new System.Drawing.Size(792, 29);
            this.m_toolStripMain.TabIndex = 5;
            this.m_toolStripMain.Text = "Picture Tools";
            // 
            // m_btnOpenImage
            // 
            this.m_btnOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpenImage.Image")));
            this.m_btnOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_btnOpenImage.Name = "m_btnOpenImage";
            this.m_btnOpenImage.Size = new System.Drawing.Size(98, 26);
            this.m_btnOpenImage.Text = "Open Image";
            this.m_btnOpenImage.Click += new System.EventHandler(this.m_btnOpenImage_Click);
            // 
            // m_btnFitWindow
            // 
            this.m_btnFitWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_btnFitWindow.Image = ((System.Drawing.Image)(resources.GetObject("m_btnFitWindow.Image")));
            this.m_btnFitWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_btnFitWindow.Name = "m_btnFitWindow";
            this.m_btnFitWindow.Size = new System.Drawing.Size(26, 26);
            this.m_btnFitWindow.Text = "Fit Window";
            this.m_btnFitWindow.Click += new System.EventHandler(this.m_btnFitWindow_Click);
            // 
            // m_btnOrigSize
            // 
            this.m_btnOrigSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_btnOrigSize.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOrigSize.Image")));
            this.m_btnOrigSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_btnOrigSize.Name = "m_btnOrigSize";
            this.m_btnOrigSize.Size = new System.Drawing.Size(26, 26);
            this.m_btnOrigSize.Text = "Original Size";
            this.m_btnOrigSize.Click += new System.EventHandler(this.m_btnOrigSize_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // m_btnPrevImage
            // 
            this.m_btnPrevImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_btnPrevImage.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPrevImage.Image")));
            this.m_btnPrevImage.ImageTransparentColor = System.Drawing.Color.White;
            this.m_btnPrevImage.Name = "m_btnPrevImage";
            this.m_btnPrevImage.Size = new System.Drawing.Size(26, 26);
            this.m_btnPrevImage.Text = "Previous Image";
            this.m_btnPrevImage.Click += new System.EventHandler(this.m_btnPrevImage_Click);
            // 
            // m_btnNextImage
            // 
            this.m_btnNextImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_btnNextImage.Image = ((System.Drawing.Image)(resources.GetObject("m_btnNextImage.Image")));
            this.m_btnNextImage.ImageTransparentColor = System.Drawing.Color.White;
            this.m_btnNextImage.Name = "m_btnNextImage";
            this.m_btnNextImage.Size = new System.Drawing.Size(26, 26);
            this.m_btnNextImage.Text = "Next Image";
            this.m_btnNextImage.Click += new System.EventHandler(this.m_btnNextImage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // m_txtImageIndex
            // 
            this.m_txtImageIndex.AcceptsReturn = true;
            this.m_txtImageIndex.MaxLength = 30;
            this.m_txtImageIndex.Name = "m_txtImageIndex";
            this.m_txtImageIndex.Size = new System.Drawing.Size(40, 29);
            this.m_txtImageIndex.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_txtImageIndex.TextChanged += new System.EventHandler(this.m_txtImageIndex_TextChanged);
            // 
            // m_lblImageDesc
            // 
            this.m_lblImageDesc.Name = "m_lblImageDesc";
            this.m_lblImageDesc.Size = new System.Drawing.Size(24, 26);
            this.m_lblImageDesc.Text = "1/1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            // 
            // m_btnSlideShow
            // 
            this.m_btnSlideShow.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSlideShow.Image")));
            this.m_btnSlideShow.ImageTransparentColor = System.Drawing.Color.White;
            this.m_btnSlideShow.Name = "m_btnSlideShow";
            this.m_btnSlideShow.Size = new System.Drawing.Size(90, 26);
            this.m_btnSlideShow.Text = "Slide Show";
            this.m_btnSlideShow.Click += new System.EventHandler(this.m_btnSlideShow_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // FormStopWatch
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.m_pnlMain);
            this.Controls.Add(this.m_toolStripMain);
            this.Controls.Add(this.m_statusBar);
            this.Controls.Add(this.m_menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.m_menuMain;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "FormStopWatch";
            this.Text = "Meditation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStopWatch_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormStopWatch_FormClosed);
            this.Load += new System.EventHandler(this.FormStopWatch_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormStopWatch_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormStopWatch_DragEnter);
            this.m_pnlMain.ResumeLayout(false);
            this.m_splitContainerMain.Panel1.ResumeLayout(false);
            this.m_splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerMain)).EndInit();
            this.m_splitContainerMain.ResumeLayout(false);
            this.m_splitContainerImage.Panel1.ResumeLayout(false);
            this.m_splitContainerImage.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerImage)).EndInit();
            this.m_splitContainerImage.ResumeLayout(false);
            this.m_toolStripContainerPictureInfo.BottomToolStripPanel.ResumeLayout(false);
            this.m_toolStripContainerPictureInfo.BottomToolStripPanel.PerformLayout();
            this.m_toolStripContainerPictureInfo.ContentPanel.ResumeLayout(false);
            this.m_toolStripContainerPictureInfo.ContentPanel.PerformLayout();
            this.m_toolStripContainerPictureInfo.ResumeLayout(false);
            this.m_toolStripContainerPictureInfo.PerformLayout();
            this.m_toolStrip_Picture.ResumeLayout(false);
            this.m_toolStrip_Picture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureBox1)).EndInit();
            this.m_splitContainerTools.Panel1.ResumeLayout(false);
            this.m_splitContainerTools.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerTools)).EndInit();
            this.m_splitContainerTools.ResumeLayout(false);
            this.m_splitClocks.Panel1.ResumeLayout(false);
            this.m_splitClocks.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitClocks)).EndInit();
            this.m_splitClocks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_analogClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalClockCtrl1)).EndInit();
            this.m_menuMain.ResumeLayout(false);
            this.m_menuMain.PerformLayout();
            this.m_statusBar.ResumeLayout(false);
            this.m_statusBar.PerformLayout();
            this.m_toolStripMain.ResumeLayout(false);
            this.m_toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private AnalogClock m_analogClock;
		private System.Windows.Forms.Panel m_pnlMain;
		private System.Windows.Forms.MenuStrip m_menuMain;
		private System.Windows.Forms.StatusStrip m_statusBar;
		private System.Windows.Forms.SplitContainer m_splitContainerTools;
		private AudioPlayerControl m_audioPlayerControl;
		private System.Windows.Forms.SplitContainer m_splitContainerMain;
		private System.Windows.Forms.PictureBox m_pictureBox1;
		private System.Windows.Forms.OpenFileDialog m_openFileDialog;
		private System.Windows.Forms.ToolStrip m_toolStripMain;
		private System.Windows.Forms.ToolStripButton m_btnOpenImage;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFile;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFile_Open;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFile_Exit;
		private System.Windows.Forms.ToolStripMenuItem m_mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem m_mnuView;
		private System.Windows.Forms.ToolStripMenuItem m_mnuTools;
		private System.Windows.Forms.ToolStripMenuItem m_mnuHelp;
		private System.Windows.Forms.ToolStripMenuItem m_mnuHelp_About;
		private System.Windows.Forms.ToolStripMenuItem m_mnuTools_Options;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFavorites;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFavorites_Add;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFavorites_Organize;
		private System.Windows.Forms.ToolStripSeparator m_mnuFavorites_Sep1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel m_lblImageDesc;
		private System.Windows.Forms.ToolStripButton m_btnPrevImage;
		private System.Windows.Forms.ToolStripButton m_btnNextImage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton m_btnSlideShow;
		private System.Windows.Forms.SplitContainer m_splitContainerImage;
		private System.Windows.Forms.ListView m_listThumbnails;
		private System.Windows.Forms.ImageList m_imageListThumbnails;
		private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripProgressBar m_toolStripProgressBar1;
		private System.Windows.Forms.ToolStripButton m_btnFitWindow;
		private System.Windows.Forms.ToolStripButton m_btnOrigSize;
		private System.Windows.Forms.ToolStripTextBox m_txtImageIndex;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripContainer m_toolStripContainerPictureInfo;
		private System.Windows.Forms.ToolStrip m_toolStrip_Picture;
		private ToolStripSpringTextBox m_tsTxt_FileName;
        private DigitalClockCtrl digitalClockCtrl1;
        private System.Windows.Forms.SplitContainer m_splitClocks;
        private System.Windows.Forms.Button m_btnHideSumbnails;
        private System.Windows.Forms.ImageList m_imageListBtnHide;
        private Tools.LabelWithTimeout m_lblVolume;
    }
}

