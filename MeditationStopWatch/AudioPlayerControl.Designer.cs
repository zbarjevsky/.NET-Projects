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
            this.m_timer1 = new System.Windows.Forms.Timer(this.components);
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_reiKi = new System.Windows.Forms.Integration.ElementHost();
            this.m_progrReiKi = new ReiKi.ReikiProgressBar();
            this.m_imageListTab = new System.Windows.Forms.ImageList(this.components);
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_splitFiles = new System.Windows.Forms.SplitContainer();
            this.m_playLists = new MeditationStopWatch.PlayListTabControl();
            this.m_pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitInfo)).BeginInit();
            this.m_splitInfo.Panel1.SuspendLayout();
            this.m_splitInfo.Panel2.SuspendLayout();
            this.m_splitInfo.SuspendLayout();
            this.m_pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackBarPosition)).BeginInit();
            this.m_toolStripPlayer.SuspendLayout();
            this.m_pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitFiles)).BeginInit();
            this.m_splitFiles.Panel1.SuspendLayout();
            this.m_splitFiles.Panel2.SuspendLayout();
            this.m_splitFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlControls
            // 
            this.m_pnlControls.BackColor = System.Drawing.Color.Transparent;
            this.m_pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlControls.Controls.Add(this.m_splitInfo);
            this.m_pnlControls.Controls.Add(this.m_pnlButtons);
            this.m_pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlControls.Location = new System.Drawing.Point(0, 465);
            this.m_pnlControls.Margin = new System.Windows.Forms.Padding(4);
            this.m_pnlControls.Name = "m_pnlControls";
            this.m_pnlControls.Size = new System.Drawing.Size(679, 131);
            this.m_pnlControls.TabIndex = 0;
            // 
            // m_splitInfo
            // 
            this.m_splitInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.m_splitInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitInfo.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitInfo.Location = new System.Drawing.Point(0, 89);
            this.m_splitInfo.Margin = new System.Windows.Forms.Padding(4);
            this.m_splitInfo.Name = "m_splitInfo";
            // 
            // m_splitInfo.Panel1
            // 
            this.m_splitInfo.Panel1.Controls.Add(this.m_lblStatus);
            // 
            // m_splitInfo.Panel2
            // 
            this.m_splitInfo.Panel2.Controls.Add(this.m_lblTime);
            this.m_splitInfo.Size = new System.Drawing.Size(675, 38);
            this.m_splitInfo.SplitterDistance = 503;
            this.m_splitInfo.SplitterWidth = 5;
            this.m_splitInfo.TabIndex = 2;
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.AutoEllipsis = true;
            this.m_lblStatus.BackColor = System.Drawing.Color.Black;
            this.m_lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_lblStatus.Location = new System.Drawing.Point(0, 0);
            this.m_lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(501, 36);
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
            this.m_lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblTime.Name = "m_lblTime";
            this.m_lblTime.Size = new System.Drawing.Size(165, 36);
            this.m_lblTime.TabIndex = 0;
            this.m_lblTime.Text = "00:00/00:00";
            this.m_lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_pnlButtons
            // 
            this.m_pnlButtons.Controls.Add(this.m_trackBarPosition);
            this.m_pnlButtons.Controls.Add(this.m_toolStripPlayer);
            this.m_pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.m_pnlButtons.Margin = new System.Windows.Forms.Padding(4);
            this.m_pnlButtons.Name = "m_pnlButtons";
            this.m_pnlButtons.Size = new System.Drawing.Size(675, 89);
            this.m_pnlButtons.TabIndex = 3;
            // 
            // m_trackBarPosition
            // 
            this.m_trackBarPosition.AutoSize = false;
            this.m_trackBarPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_trackBarPosition.LargeChange = 50;
            this.m_trackBarPosition.Location = new System.Drawing.Point(0, 0);
            this.m_trackBarPosition.Margin = new System.Windows.Forms.Padding(4);
            this.m_trackBarPosition.Maximum = 300;
            this.m_trackBarPosition.Name = "m_trackBarPosition";
            this.m_trackBarPosition.Size = new System.Drawing.Size(675, 50);
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
            this.m_toolStripPlayer.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.m_toolStripPlayer.Location = new System.Drawing.Point(0, 50);
            this.m_toolStripPlayer.Name = "m_toolStripPlayer";
            this.m_toolStripPlayer.Size = new System.Drawing.Size(675, 39);
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
            this.m_tbbtnPlay.Size = new System.Drawing.Size(29, 36);
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
            this.m_tbbtnPause.Size = new System.Drawing.Size(29, 36);
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
            this.m_tbbtnStop.Size = new System.Drawing.Size(29, 36);
            this.m_tbbtnStop.Text = "Stop";
            this.m_tbbtnStop.Click += new System.EventHandler(this.m_tbbtnStop_Click);
            // 
            // m_toolStripSeparator1
            // 
            this.m_toolStripSeparator1.Name = "m_toolStripSeparator1";
            this.m_toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // m_tbbtnPrev
            // 
            this.m_tbbtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tbbtnPrev.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnPrev.Image")));
            this.m_tbbtnPrev.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tbbtnPrev.ImageTransparentColor = System.Drawing.Color.White;
            this.m_tbbtnPrev.Name = "m_tbbtnPrev";
            this.m_tbbtnPrev.Size = new System.Drawing.Size(29, 36);
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
            this.m_tbbtnNext.Size = new System.Drawing.Size(29, 36);
            this.m_tbbtnNext.Text = "Next";
            this.m_tbbtnNext.Click += new System.EventHandler(this.m_tbbtnNext_Click);
            // 
            // m_toolStripSeparator2
            // 
            this.m_toolStripSeparator2.Name = "m_toolStripSeparator2";
            this.m_toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // m_toolStripTrackBarVolume
            // 
            this.m_toolStripTrackBarVolume.AutoSize = false;
            this.m_toolStripTrackBarVolume.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.m_toolStripTrackBarVolume.LargeChange = 100;
            this.m_toolStripTrackBarVolume.Maximum = 1000;
            this.m_toolStripTrackBarVolume.Name = "m_toolStripTrackBarVolume";
            this.m_toolStripTrackBarVolume.Size = new System.Drawing.Size(104, 36);
            this.m_toolStripTrackBarVolume.SmallChange = 40;
            this.m_toolStripTrackBarVolume.ToolTipText = "Volume Control";
            this.m_toolStripTrackBarVolume.Value = 300;
            this.m_toolStripTrackBarVolume.ValueChanged += new System.EventHandler(this.m_toolStripTrackBarVolume_ValueChanged);
            // 
            // m_toolStripSeparator3
            // 
            this.m_toolStripSeparator3.Name = "m_toolStripSeparator3";
            this.m_toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // m_chkLoop
            // 
            this.m_chkLoop.Image = ((System.Drawing.Image)(resources.GetObject("m_chkLoop.Image")));
            this.m_chkLoop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_chkLoop.Name = "m_chkLoop";
            this.m_chkLoop.Size = new System.Drawing.Size(67, 36);
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
            // m_timer1
            // 
            this.m_timer1.Enabled = true;
            this.m_timer1.Interval = 1000;
            this.m_timer1.Tick += new System.EventHandler(this.m_timer1_Tick);
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
            // m_reiKi
            // 
            this.m_reiKi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_reiKi.Location = new System.Drawing.Point(0, 0);
            this.m_reiKi.Margin = new System.Windows.Forms.Padding(4);
            this.m_reiKi.Name = "m_reiKi";
            this.m_reiKi.Size = new System.Drawing.Size(675, 23);
            this.m_reiKi.TabIndex = 0;
            this.m_reiKi.Text = "elementHost1";
            this.m_reiKi.Child = this.m_progrReiKi;
            // 
            // m_imageListTab
            // 
            this.m_imageListTab.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListTab.ImageStream")));
            this.m_imageListTab.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListTab.Images.SetKeyName(0, "Show Detail.ico");
            this.m_imageListTab.Images.SetKeyName(1, "Hide Detail.ico");
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Controls.Add(this.m_splitFiles);
            this.m_pnlMain.Controls.Add(this.m_pnlControls);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Margin = new System.Windows.Forms.Padding(4);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(679, 596);
            this.m_pnlMain.TabIndex = 6;
            // 
            // m_splitFiles
            // 
            this.m_splitFiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitFiles.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitFiles.Location = new System.Drawing.Point(0, 0);
            this.m_splitFiles.Margin = new System.Windows.Forms.Padding(4);
            this.m_splitFiles.Name = "m_splitFiles";
            this.m_splitFiles.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitFiles.Panel1
            // 
            this.m_splitFiles.Panel1.Controls.Add(this.m_playLists);
            this.m_splitFiles.Panel1MinSize = 55;
            // 
            // m_splitFiles.Panel2
            // 
            this.m_splitFiles.Panel2.Controls.Add(this.m_reiKi);
            this.m_splitFiles.Panel2MinSize = 10;
            this.m_splitFiles.Size = new System.Drawing.Size(679, 465);
            this.m_splitFiles.SplitterDistance = 433;
            this.m_splitFiles.SplitterWidth = 5;
            this.m_splitFiles.TabIndex = 5;
            // 
            // m_playLists
            // 
            this.m_playLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_playLists.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_playLists.Location = new System.Drawing.Point(0, 0);
            this.m_playLists.Margin = new System.Windows.Forms.Padding(5);
            this.m_playLists.Name = "m_playLists";
            this.m_playLists.Size = new System.Drawing.Size(675, 429);
            this.m_playLists.TabIndex = 0;
            // 
            // AudioPlayerControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(310, 0);
            this.Controls.Add(this.m_pnlMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AudioPlayerControl";
            this.Size = new System.Drawing.Size(679, 596);
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
            this.m_pnlMain.ResumeLayout(false);
            this.m_splitFiles.Panel1.ResumeLayout(false);
            this.m_splitFiles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitFiles)).EndInit();
            this.m_splitFiles.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel m_pnlControls;
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
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton m_chkLoop;
		private System.Windows.Forms.OpenFileDialog m_openFileDialog;
        private System.Windows.Forms.ToolTip m_toolTip1;
        private System.Windows.Forms.Integration.ElementHost m_reiKi;
        private ReiKi.ReikiProgressBar m_progrReiKi;
		private System.Windows.Forms.SplitContainer m_splitInfo;
		private System.Windows.Forms.Panel m_pnlButtons;
        private System.Windows.Forms.Panel m_pnlMain;
        private System.Windows.Forms.SplitContainer m_splitFiles;
        private System.Windows.Forms.ImageList m_imageListTab;
        private PlayListTabControl m_playLists;
    }
}
