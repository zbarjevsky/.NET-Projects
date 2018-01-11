namespace MeditationStopWatch
{
	partial class AudioPlayerControl
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
				m_Mp3Player.Close();
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioPlayerControl));
            this.m_pnlControls = new System.Windows.Forms.Panel();
            this.m_splitInfo = new System.Windows.Forms.SplitContainer();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.m_lblTime = new System.Windows.Forms.Label();
            this.m_pnlButtons = new System.Windows.Forms.Panel();
            this.m_trackBarPosition = new System.Windows.Forms.TrackBar();
            this.m_toolStripPlayer = new System.Windows.Forms.ToolStrip();
            this.m_tbbtnPlay = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnPause = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnStop = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tbbtnPrev = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnNext = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStripTrackBarVolume = new MeditationStopWatch.ToolStripTrackBar();
            this.m_toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_chkLoop = new System.Windows.Forms.ToolStripButton();
            this.m_imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_listFiles = new System.Windows.Forms.ListView();
            this.m_clmnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_clmnDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_clmnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuPause = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuNext = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripMenuSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.m_timer1 = new System.Windows.Forms.Timer(this.components);
            this.m_toolbarPlayer = new System.Windows.Forms.ToolStrip();
            this.m_toolStripButton_AddFiles = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripButton_Remove = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripButton_RemoveAll = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStripButton_Up = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripButton_Down = new System.Windows.Forms.ToolStripButton();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_pnl4Progress = new System.Windows.Forms.Panel();
            this.m_reiKi = new System.Windows.Forms.Integration.ElementHost();
            this.m_progrReiKi = new Wizard.ReikiProgressBar();
            this.m_pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitInfo)).BeginInit();
            this.m_splitInfo.Panel1.SuspendLayout();
            this.m_splitInfo.Panel2.SuspendLayout();
            this.m_splitInfo.SuspendLayout();
            this.m_pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackBarPosition)).BeginInit();
            this.m_toolStripPlayer.SuspendLayout();
            this.m_contextMenuStrip1.SuspendLayout();
            this.m_toolbarPlayer.SuspendLayout();
            this.m_pnl4Progress.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlControls
            // 
            this.m_pnlControls.BackColor = System.Drawing.Color.Transparent;
            this.m_pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlControls.Controls.Add(this.m_splitInfo);
            this.m_pnlControls.Controls.Add(this.m_pnlButtons);
            this.m_pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlControls.Location = new System.Drawing.Point(0, 244);
            this.m_pnlControls.Name = "m_pnlControls";
            this.m_pnlControls.Size = new System.Drawing.Size(312, 107);
            this.m_pnlControls.TabIndex = 0;
            // 
            // m_splitInfo
            // 
            this.m_splitInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.m_splitInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitInfo.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitInfo.Location = new System.Drawing.Point(0, 72);
            this.m_splitInfo.Name = "m_splitInfo";
            // 
            // m_splitInfo.Panel1
            // 
            this.m_splitInfo.Panel1.Controls.Add(this.m_lblStatus);
            // 
            // m_splitInfo.Panel2
            // 
            this.m_splitInfo.Panel2.Controls.Add(this.m_lblTime);
            this.m_splitInfo.Size = new System.Drawing.Size(308, 31);
            this.m_splitInfo.SplitterDistance = 139;
            this.m_splitInfo.TabIndex = 2;
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.BackColor = System.Drawing.Color.Black;
            this.m_lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_lblStatus.Location = new System.Drawing.Point(0, 0);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(137, 29);
            this.m_lblStatus.TabIndex = 0;
            this.m_lblStatus.Text = "Ready";
            this.m_lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblTime
            // 
            this.m_lblTime.BackColor = System.Drawing.Color.Black;
            this.m_lblTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTime.ForeColor = System.Drawing.Color.Lime;
            this.m_lblTime.Location = new System.Drawing.Point(0, 0);
            this.m_lblTime.Name = "m_lblTime";
            this.m_lblTime.Size = new System.Drawing.Size(163, 29);
            this.m_lblTime.TabIndex = 1;
            this.m_lblTime.Text = "00:00/00:00";
            this.m_lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_pnlButtons
            // 
            this.m_pnlButtons.Controls.Add(this.m_trackBarPosition);
            this.m_pnlButtons.Controls.Add(this.m_toolStripPlayer);
            this.m_pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.m_pnlButtons.Name = "m_pnlButtons";
            this.m_pnlButtons.Size = new System.Drawing.Size(308, 72);
            this.m_pnlButtons.TabIndex = 3;
            // 
            // m_trackBarPosition
            // 
            this.m_trackBarPosition.AutoSize = false;
            this.m_trackBarPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_trackBarPosition.LargeChange = 50;
            this.m_trackBarPosition.Location = new System.Drawing.Point(0, 0);
            this.m_trackBarPosition.Maximum = 300;
            this.m_trackBarPosition.Name = "m_trackBarPosition";
            this.m_trackBarPosition.Size = new System.Drawing.Size(308, 40);
            this.m_trackBarPosition.SmallChange = 10;
            this.m_trackBarPosition.TabIndex = 0;
            this.m_trackBarPosition.TickFrequency = 180;
            this.m_trackBarPosition.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.m_toolTip1.SetToolTip(this.m_trackBarPosition, "Position");
            this.m_trackBarPosition.Scroll += new System.EventHandler(this.m_trackBarPosition_Scroll);
            // 
            // m_toolStripPlayer
            // 
            this.m_toolStripPlayer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_toolStripPlayer.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_toolStripPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tbbtnPlay,
            this.m_tbbtnPause,
            this.m_tbbtnStop,
            this.m_toolStripSeparator1,
            this.m_tbbtnPrev,
            this.m_tbbtnNext,
            this.m_toolStripSeparator2,
            this.m_toolStripTrackBarVolume,
            this.m_toolStripSeparator3,
            this.m_chkLoop});
            this.m_toolStripPlayer.Location = new System.Drawing.Point(0, 40);
            this.m_toolStripPlayer.Name = "m_toolStripPlayer";
            this.m_toolStripPlayer.Size = new System.Drawing.Size(308, 32);
            this.m_toolStripPlayer.TabIndex = 1;
            this.m_toolStripPlayer.Text = "Tools";
            // 
            // m_tbbtnPlay
            // 
            this.m_tbbtnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tbbtnPlay.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnPlay.Image")));
            this.m_tbbtnPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tbbtnPlay.ImageTransparentColor = System.Drawing.Color.White;
            this.m_tbbtnPlay.Name = "m_tbbtnPlay";
            this.m_tbbtnPlay.Size = new System.Drawing.Size(29, 29);
            this.m_tbbtnPlay.Text = "Play";
            this.m_tbbtnPlay.Click += new System.EventHandler(this.m_tbbtnPlay_Click);
            // 
            // m_tbbtnPause
            // 
            this.m_tbbtnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tbbtnPause.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnPause.Image")));
            this.m_tbbtnPause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tbbtnPause.ImageTransparentColor = System.Drawing.Color.White;
            this.m_tbbtnPause.Name = "m_tbbtnPause";
            this.m_tbbtnPause.Size = new System.Drawing.Size(29, 29);
            this.m_tbbtnPause.Text = "Pause";
            this.m_tbbtnPause.Click += new System.EventHandler(this.m_tbbtnPause_Click);
            // 
            // m_tbbtnStop
            // 
            this.m_tbbtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tbbtnStop.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnStop.Image")));
            this.m_tbbtnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tbbtnStop.ImageTransparentColor = System.Drawing.Color.White;
            this.m_tbbtnStop.Name = "m_tbbtnStop";
            this.m_tbbtnStop.Size = new System.Drawing.Size(29, 29);
            this.m_tbbtnStop.Text = "Stop";
            this.m_tbbtnStop.Click += new System.EventHandler(this.m_tbbtnStop_Click);
            // 
            // m_toolStripSeparator1
            // 
            this.m_toolStripSeparator1.Name = "m_toolStripSeparator1";
            this.m_toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // m_tbbtnPrev
            // 
            this.m_tbbtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tbbtnPrev.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnPrev.Image")));
            this.m_tbbtnPrev.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tbbtnPrev.ImageTransparentColor = System.Drawing.Color.White;
            this.m_tbbtnPrev.Name = "m_tbbtnPrev";
            this.m_tbbtnPrev.Size = new System.Drawing.Size(29, 29);
            this.m_tbbtnPrev.Text = "Previous";
            this.m_tbbtnPrev.Click += new System.EventHandler(this.m_tbbtnPrev_Click);
            // 
            // m_tbbtnNext
            // 
            this.m_tbbtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tbbtnNext.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnNext.Image")));
            this.m_tbbtnNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tbbtnNext.ImageTransparentColor = System.Drawing.Color.White;
            this.m_tbbtnNext.Name = "m_tbbtnNext";
            this.m_tbbtnNext.Size = new System.Drawing.Size(29, 29);
            this.m_tbbtnNext.Text = "Next";
            this.m_tbbtnNext.Click += new System.EventHandler(this.m_tbbtnNext_Click);
            // 
            // m_toolStripSeparator2
            // 
            this.m_toolStripSeparator2.Name = "m_toolStripSeparator2";
            this.m_toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // m_toolStripTrackBarVolume
            // 
            this.m_toolStripTrackBarVolume.AutoSize = false;
            this.m_toolStripTrackBarVolume.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.m_toolStripTrackBarVolume.LargeChange = 100;
            this.m_toolStripTrackBarVolume.Maximum = 1000;
            this.m_toolStripTrackBarVolume.Name = "m_toolStripTrackBarVolume";
            this.m_toolStripTrackBarVolume.Size = new System.Drawing.Size(78, 29);
            this.m_toolStripTrackBarVolume.SmallChange = 40;
            this.m_toolStripTrackBarVolume.ToolTipText = "Volume Control";
            this.m_toolStripTrackBarVolume.Value = 300;
            this.m_toolStripTrackBarVolume.ValueChanged += new System.EventHandler(this.m_toolStripTrackBarVolume_ValueChanged);
            // 
            // m_toolStripSeparator3
            // 
            this.m_toolStripSeparator3.Name = "m_toolStripSeparator3";
            this.m_toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // m_chkLoop
            // 
            this.m_chkLoop.Image = ((System.Drawing.Image)(resources.GetObject("m_chkLoop.Image")));
            this.m_chkLoop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_chkLoop.Name = "m_chkLoop";
            this.m_chkLoop.Size = new System.Drawing.Size(54, 29);
            this.m_chkLoop.Text = "Loop";
            this.m_chkLoop.Click += new System.EventHandler(this.m_chkLoop_Click);
            // 
            // m_imageList1
            // 
            this.m_imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList1.ImageStream")));
            this.m_imageList1.TransparentColor = System.Drawing.Color.White;
            this.m_imageList1.Images.SetKeyName(0, "play_on.PNG");
            this.m_imageList1.Images.SetKeyName(1, "play_off.PNG");
            this.m_imageList1.Images.SetKeyName(2, "pause_on.PNG");
            this.m_imageList1.Images.SetKeyName(3, "pause_off.PNG");
            this.m_imageList1.Images.SetKeyName(4, "stop_on.PNG");
            this.m_imageList1.Images.SetKeyName(5, "stop_off.PNG");
            this.m_imageList1.Images.SetKeyName(6, "next_on.PNG");
            this.m_imageList1.Images.SetKeyName(7, "next_off.PNG");
            this.m_imageList1.Images.SetKeyName(8, "previus_on.PNG");
            this.m_imageList1.Images.SetKeyName(9, "previus_off.PNG");
            this.m_imageList1.Images.SetKeyName(10, "selected.PNG");
            this.m_imageList1.Images.SetKeyName(11, "selected_off.PNG");
            // 
            // m_listFiles
            // 
            this.m_listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_clmnFileName,
            this.m_clmnDuration,
            this.m_clmnSize});
            this.m_listFiles.ContextMenuStrip = this.m_contextMenuStrip1;
            this.m_listFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_listFiles.FullRowSelect = true;
            this.m_listFiles.GridLines = true;
            this.m_listFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listFiles.HideSelection = false;
            this.m_listFiles.Location = new System.Drawing.Point(0, 25);
            this.m_listFiles.Name = "m_listFiles";
            this.m_listFiles.ShowItemToolTips = true;
            this.m_listFiles.Size = new System.Drawing.Size(312, 199);
            this.m_listFiles.TabIndex = 1;
            this.m_listFiles.UseCompatibleStateImageBehavior = false;
            this.m_listFiles.View = System.Windows.Forms.View.Details;
            this.m_listFiles.SelectedIndexChanged += new System.EventHandler(this.m_listFiles_SelectedIndexChanged);
            this.m_listFiles.DoubleClick += new System.EventHandler(this.m_listFiles_DoubleClick);
            // 
            // m_clmnFileName
            // 
            this.m_clmnFileName.Text = "File name";
            this.m_clmnFileName.Width = 150;
            // 
            // m_clmnDuration
            // 
            this.m_clmnDuration.Text = "Duration";
            this.m_clmnDuration.Width = 70;
            // 
            // m_clmnSize
            // 
            this.m_clmnSize.Text = "Size";
            this.m_clmnSize.Width = 70;
            // 
            // m_contextMenuStrip1
            // 
            this.m_contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuPlay,
            this.m_mnuPause,
            this.m_mnuStop,
            this.m_toolStripMenuSep1,
            this.m_mnuPrev,
            this.m_mnuNext,
            this.m_toolStripMenuSep2,
            this.m_mnuAdd,
            this.m_mnuRemove,
            this.m_mnuRemoveAll,
            this.m_toolStripMenuSep3,
            this.m_mnuUp,
            this.m_mnuDown});
            this.m_contextMenuStrip1.Name = "m_contextMenuStrip1";
            this.m_contextMenuStrip1.Size = new System.Drawing.Size(135, 242);
            // 
            // m_mnuPlay
            // 
            this.m_mnuPlay.Name = "m_mnuPlay";
            this.m_mnuPlay.Size = new System.Drawing.Size(134, 22);
            this.m_mnuPlay.Text = "Play";
            this.m_mnuPlay.Click += new System.EventHandler(this.m_tbbtnPlay_Click);
            // 
            // m_mnuPause
            // 
            this.m_mnuPause.Name = "m_mnuPause";
            this.m_mnuPause.Size = new System.Drawing.Size(134, 22);
            this.m_mnuPause.Text = "Pause";
            this.m_mnuPause.Click += new System.EventHandler(this.m_tbbtnPause_Click);
            // 
            // m_mnuStop
            // 
            this.m_mnuStop.Name = "m_mnuStop";
            this.m_mnuStop.Size = new System.Drawing.Size(134, 22);
            this.m_mnuStop.Text = "Stop";
            this.m_mnuStop.Click += new System.EventHandler(this.m_tbbtnStop_Click);
            // 
            // m_toolStripMenuSep1
            // 
            this.m_toolStripMenuSep1.Name = "m_toolStripMenuSep1";
            this.m_toolStripMenuSep1.Size = new System.Drawing.Size(131, 6);
            // 
            // m_mnuPrev
            // 
            this.m_mnuPrev.Name = "m_mnuPrev";
            this.m_mnuPrev.Size = new System.Drawing.Size(134, 22);
            this.m_mnuPrev.Text = "Previous";
            this.m_mnuPrev.Click += new System.EventHandler(this.m_tbbtnPrev_Click);
            // 
            // m_mnuNext
            // 
            this.m_mnuNext.Name = "m_mnuNext";
            this.m_mnuNext.Size = new System.Drawing.Size(134, 22);
            this.m_mnuNext.Text = "Next";
            this.m_mnuNext.Click += new System.EventHandler(this.m_tbbtnNext_Click);
            // 
            // m_toolStripMenuSep2
            // 
            this.m_toolStripMenuSep2.Name = "m_toolStripMenuSep2";
            this.m_toolStripMenuSep2.Size = new System.Drawing.Size(131, 6);
            // 
            // m_mnuAdd
            // 
            this.m_mnuAdd.Name = "m_mnuAdd";
            this.m_mnuAdd.Size = new System.Drawing.Size(134, 22);
            this.m_mnuAdd.Text = "Add Files";
            this.m_mnuAdd.Click += new System.EventHandler(this.m_toolStripButton_AddFiles_Click);
            // 
            // m_mnuRemove
            // 
            this.m_mnuRemove.Name = "m_mnuRemove";
            this.m_mnuRemove.Size = new System.Drawing.Size(134, 22);
            this.m_mnuRemove.Text = "Remove";
            this.m_mnuRemove.Click += new System.EventHandler(this.m_toolStripButton_Remove_Click);
            // 
            // m_mnuRemoveAll
            // 
            this.m_mnuRemoveAll.Name = "m_mnuRemoveAll";
            this.m_mnuRemoveAll.Size = new System.Drawing.Size(134, 22);
            this.m_mnuRemoveAll.Text = "Remove All";
            this.m_mnuRemoveAll.Click += new System.EventHandler(this.m_toolStripButton_RemoveAll_Click);
            // 
            // m_toolStripMenuSep3
            // 
            this.m_toolStripMenuSep3.Name = "m_toolStripMenuSep3";
            this.m_toolStripMenuSep3.Size = new System.Drawing.Size(131, 6);
            // 
            // m_mnuUp
            // 
            this.m_mnuUp.Name = "m_mnuUp";
            this.m_mnuUp.Size = new System.Drawing.Size(134, 22);
            this.m_mnuUp.Text = "Up";
            this.m_mnuUp.Click += new System.EventHandler(this.m_toolStripButton_Up_Click);
            // 
            // m_mnuDown
            // 
            this.m_mnuDown.Name = "m_mnuDown";
            this.m_mnuDown.Size = new System.Drawing.Size(134, 22);
            this.m_mnuDown.Text = "Down";
            this.m_mnuDown.Click += new System.EventHandler(this.m_toolStripButton_Down_Click);
            // 
            // m_timer1
            // 
            this.m_timer1.Enabled = true;
            this.m_timer1.Interval = 1000;
            this.m_timer1.Tick += new System.EventHandler(this.m_timer1_Tick);
            // 
            // m_toolbarPlayer
            // 
            this.m_toolbarPlayer.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_toolbarPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripButton_AddFiles,
            this.m_toolStripButton_Remove,
            this.m_toolStripButton_RemoveAll,
            this.m_toolStripSeparator4,
            this.m_toolStripButton_Up,
            this.m_toolStripButton_Down});
            this.m_toolbarPlayer.Location = new System.Drawing.Point(0, 0);
            this.m_toolbarPlayer.Name = "m_toolbarPlayer";
            this.m_toolbarPlayer.Size = new System.Drawing.Size(312, 25);
            this.m_toolbarPlayer.TabIndex = 0;
            this.m_toolbarPlayer.Text = "toolStrip1";
            // 
            // m_toolStripButton_AddFiles
            // 
            this.m_toolStripButton_AddFiles.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_AddFiles.Image")));
            this.m_toolStripButton_AddFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_AddFiles.Name = "m_toolStripButton_AddFiles";
            this.m_toolStripButton_AddFiles.Size = new System.Drawing.Size(75, 22);
            this.m_toolStripButton_AddFiles.Text = "Add Files";
            this.m_toolStripButton_AddFiles.ToolTipText = "Add Files (Ins)";
            this.m_toolStripButton_AddFiles.Click += new System.EventHandler(this.m_toolStripButton_AddFiles_Click);
            // 
            // m_toolStripButton_Remove
            // 
            this.m_toolStripButton_Remove.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Remove.Image")));
            this.m_toolStripButton_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Remove.Name = "m_toolStripButton_Remove";
            this.m_toolStripButton_Remove.Size = new System.Drawing.Size(70, 22);
            this.m_toolStripButton_Remove.Text = "Remove";
            this.m_toolStripButton_Remove.ToolTipText = "Remove (Del)";
            this.m_toolStripButton_Remove.Click += new System.EventHandler(this.m_toolStripButton_Remove_Click);
            // 
            // m_toolStripButton_RemoveAll
            // 
            this.m_toolStripButton_RemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_RemoveAll.Image")));
            this.m_toolStripButton_RemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_RemoveAll.Name = "m_toolStripButton_RemoveAll";
            this.m_toolStripButton_RemoveAll.Size = new System.Drawing.Size(87, 22);
            this.m_toolStripButton_RemoveAll.Text = "Remove All";
            this.m_toolStripButton_RemoveAll.Click += new System.EventHandler(this.m_toolStripButton_RemoveAll_Click);
            // 
            // m_toolStripSeparator4
            // 
            this.m_toolStripSeparator4.Name = "m_toolStripSeparator4";
            this.m_toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // m_toolStripButton_Up
            // 
            this.m_toolStripButton_Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Up.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Up.Image")));
            this.m_toolStripButton_Up.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Up.Name = "m_toolStripButton_Up";
            this.m_toolStripButton_Up.Size = new System.Drawing.Size(23, 22);
            this.m_toolStripButton_Up.Text = "toolStripButton1";
            this.m_toolStripButton_Up.Click += new System.EventHandler(this.m_toolStripButton_Up_Click);
            // 
            // m_toolStripButton_Down
            // 
            this.m_toolStripButton_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Down.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Down.Image")));
            this.m_toolStripButton_Down.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Down.Name = "m_toolStripButton_Down";
            this.m_toolStripButton_Down.Size = new System.Drawing.Size(23, 22);
            this.m_toolStripButton_Down.Text = "Down";
            this.m_toolStripButton_Down.Click += new System.EventHandler(this.m_toolStripButton_Down_Click);
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.CheckFileExists = false;
            this.m_openFileDialog.DefaultExt = "mp3";
            this.m_openFileDialog.FileName = "*.mp3";
            this.m_openFileDialog.Filter = "Music Files(*.mp3)|*.mp3|All files|*.*";
            this.m_openFileDialog.Multiselect = true;
            this.m_openFileDialog.ValidateNames = false;
            // 
            // m_pnl4Progress
            // 
            this.m_pnl4Progress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnl4Progress.Controls.Add(this.m_reiKi);
            this.m_pnl4Progress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnl4Progress.Location = new System.Drawing.Point(0, 224);
            this.m_pnl4Progress.Name = "m_pnl4Progress";
            this.m_pnl4Progress.Size = new System.Drawing.Size(312, 20);
            this.m_pnl4Progress.TabIndex = 4;
            // 
            // m_reiKi
            // 
            this.m_reiKi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_reiKi.Location = new System.Drawing.Point(3, 1);
            this.m_reiKi.Name = "m_reiKi";
            this.m_reiKi.Size = new System.Drawing.Size(302, 14);
            this.m_reiKi.TabIndex = 4;
            this.m_reiKi.Text = "elementHost1";
            this.m_reiKi.Child = this.m_progrReiKi;
            // 
            // AudioPlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(310, 0);
            this.Controls.Add(this.m_listFiles);
            this.Controls.Add(this.m_pnl4Progress);
            this.Controls.Add(this.m_toolbarPlayer);
            this.Controls.Add(this.m_pnlControls);
            this.Name = "AudioPlayerControl";
            this.Size = new System.Drawing.Size(312, 351);
            this.Load += new System.EventHandler(this.AudioPlayerControl_Load);
            this.m_pnlControls.ResumeLayout(false);
            this.m_splitInfo.Panel1.ResumeLayout(false);
            this.m_splitInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitInfo)).EndInit();
            this.m_splitInfo.ResumeLayout(false);
            this.m_pnlButtons.ResumeLayout(false);
            this.m_pnlButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackBarPosition)).EndInit();
            this.m_toolStripPlayer.ResumeLayout(false);
            this.m_toolStripPlayer.PerformLayout();
            this.m_contextMenuStrip1.ResumeLayout(false);
            this.m_toolbarPlayer.ResumeLayout(false);
            this.m_toolbarPlayer.PerformLayout();
            this.m_pnl4Progress.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel m_pnlControls;
		private System.Windows.Forms.ListView m_listFiles;
		private System.Windows.Forms.ColumnHeader m_clmnFileName;
		private System.Windows.Forms.ColumnHeader m_clmnDuration;
		private System.Windows.Forms.TrackBar m_trackBarPosition;
		private System.Windows.Forms.Timer m_timer1;
		private System.Windows.Forms.Label m_lblTime;
		private System.Windows.Forms.ImageList m_imageList1;
		private System.Windows.Forms.ToolStrip m_toolStripPlayer;
		private System.Windows.Forms.ToolStripButton m_tbbtnPlay;
		private System.Windows.Forms.ToolStripButton m_tbbtnPause;
		private System.Windows.Forms.ToolStripButton m_tbbtnStop;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton m_tbbtnPrev;
		private System.Windows.Forms.ToolStripButton m_tbbtnNext;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator2;
		private ToolStripTrackBar m_toolStripTrackBarVolume;
		private System.Windows.Forms.Label m_lblStatus;
		private System.Windows.Forms.ColumnHeader m_clmnSize;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton m_chkLoop;
		private System.Windows.Forms.ToolStrip m_toolbarPlayer;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_AddFiles;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Remove;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_RemoveAll;
		private System.Windows.Forms.OpenFileDialog m_openFileDialog;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Up;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Down;
		private System.Windows.Forms.ContextMenuStrip m_contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem m_mnuPlay;
		private System.Windows.Forms.ToolStripMenuItem m_mnuPause;
		private System.Windows.Forms.ToolStripMenuItem m_mnuStop;
		private System.Windows.Forms.ToolStripSeparator m_toolStripMenuSep1;
		private System.Windows.Forms.ToolStripMenuItem m_mnuPrev;
		private System.Windows.Forms.ToolStripMenuItem m_mnuNext;
		private System.Windows.Forms.ToolStripSeparator m_toolStripMenuSep2;
		private System.Windows.Forms.ToolStripMenuItem m_mnuAdd;
		private System.Windows.Forms.ToolStripMenuItem m_mnuRemove;
		private System.Windows.Forms.ToolStripMenuItem m_mnuRemoveAll;
		private System.Windows.Forms.ToolStripSeparator m_toolStripMenuSep3;
		private System.Windows.Forms.ToolStripMenuItem m_mnuUp;
		private System.Windows.Forms.ToolStripMenuItem m_mnuDown;
        private System.Windows.Forms.ToolTip m_toolTip1;
        private System.Windows.Forms.Panel m_pnl4Progress;
        private System.Windows.Forms.Integration.ElementHost m_reiKi;
        private Wizard.ReikiProgressBar m_progrReiKi;
		private System.Windows.Forms.SplitContainer m_splitInfo;
		private System.Windows.Forms.Panel m_pnlButtons;
	}
}
