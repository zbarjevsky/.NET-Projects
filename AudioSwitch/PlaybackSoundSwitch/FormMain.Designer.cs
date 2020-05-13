namespace PlaybackSoundSwitch
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
            this.m_imageListSpeakers = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_imageListMic = new System.Windows.Forms.ImageList(this.components);
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_tabDevices = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_DeviceListPlayback = new PlaybackSoundSwitch.MediaDeviceListUserControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_DeviceListRecording = new PlaybackSoundSwitch.MediaDeviceListUserControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_progrLevelsMic = new MZ.Controls.VerticalProgressBar();
            this.m_btnMicMute = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_progrLevelSpk = new MZ.Controls.VerticalProgressBar();
            this.m_btnMute = new System.Windows.Forms.Button();
            this.m_trackVolume = new System.Windows.Forms.TrackBar();
            this.m_txtLog = new System.Windows.Forms.RichTextBox();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.m_pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.m_tabDevices.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // m_imageListSpeakers
            // 
            this.m_imageListSpeakers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListSpeakers.ImageStream")));
            this.m_imageListSpeakers.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListSpeakers.Images.SetKeyName(0, "Speaker5_16x16.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.m_status2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 414);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(717, 22);
            this.statusStrip1.TabIndex = 5;
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
            this.m_status2.Size = new System.Drawing.Size(663, 17);
            this.m_status2.Spring = true;
            this.m_status2.Text = "...";
            // 
            // m_imageListMic
            // 
            this.m_imageListMic.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListMic.ImageStream")));
            this.m_imageListMic.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListMic.Images.SetKeyName(0, "Mic1.png");
            this.m_imageListMic.Images.SetKeyName(1, "MicMute1.png");
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.AutoScroll = true;
            this.m_pnlMain.Controls.Add(this.splitContainer1);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(717, 414);
            this.m_pnlMain.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_tabDevices);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_txtLog);
            this.splitContainer1.Panel2MinSize = 1;
            this.splitContainer1.Size = new System.Drawing.Size(717, 414);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.TabIndex = 7;
            // 
            // m_tabDevices
            // 
            this.m_tabDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabDevices.Controls.Add(this.tabPage1);
            this.m_tabDevices.Controls.Add(this.tabPage2);
            this.m_tabDevices.Location = new System.Drawing.Point(2, 3);
            this.m_tabDevices.Name = "m_tabDevices";
            this.m_tabDevices.SelectedIndex = 0;
            this.m_tabDevices.Size = new System.Drawing.Size(576, 368);
            this.m_tabDevices.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_DeviceListPlayback);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(568, 342);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Playback Devices";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_DeviceListPlayback
            // 
            this.m_DeviceListPlayback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DeviceListPlayback.Location = new System.Drawing.Point(3, 3);
            this.m_DeviceListPlayback.Name = "m_DeviceListPlayback";
            this.m_DeviceListPlayback.Size = new System.Drawing.Size(562, 336);
            this.m_DeviceListPlayback.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_DeviceListRecording);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(568, 342);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Recording Devices";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_DeviceListRecording
            // 
            this.m_DeviceListRecording.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DeviceListRecording.Location = new System.Drawing.Point(3, 3);
            this.m_DeviceListRecording.Name = "m_DeviceListRecording";
            this.m_DeviceListRecording.Size = new System.Drawing.Size(562, 336);
            this.m_DeviceListRecording.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.m_progrLevelsMic);
            this.groupBox2.Controls.Add(this.m_btnMicMute);
            this.groupBox2.Location = new System.Drawing.Point(654, 11);
            this.groupBox2.MaximumSize = new System.Drawing.Size(57, 400);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(57, 363);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "  Mic Level";
            // 
            // m_progrLevelsMic
            // 
            this.m_progrLevelsMic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progrLevelsMic.Location = new System.Drawing.Point(40, 45);
            this.m_progrLevelsMic.Name = "m_progrLevelsMic";
            this.m_progrLevelsMic.Size = new System.Drawing.Size(9, 255);
            this.m_progrLevelsMic.TabIndex = 5;
            this.m_progrLevelsMic.Value = 10;
            // 
            // m_btnMicMute
            // 
            this.m_btnMicMute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnMicMute.ImageIndex = 0;
            this.m_btnMicMute.ImageList = this.m_imageListMic;
            this.m_btnMicMute.Location = new System.Drawing.Point(4, 314);
            this.m_btnMicMute.Name = "m_btnMicMute";
            this.m_btnMicMute.Size = new System.Drawing.Size(48, 43);
            this.m_btnMicMute.TabIndex = 4;
            this.m_btnMicMute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.m_btnMicMute, "Mute/Unmute Default Microphone");
            this.m_btnMicMute.UseVisualStyleBackColor = true;
            this.m_btnMicMute.Click += new System.EventHandler(this.m_btnMicMute_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.m_progrLevelSpk);
            this.groupBox1.Controls.Add(this.m_btnMute);
            this.groupBox1.Controls.Add(this.m_trackVolume);
            this.groupBox1.Location = new System.Drawing.Point(589, 11);
            this.groupBox1.MaximumSize = new System.Drawing.Size(57, 400);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(57, 363);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Volume";
            // 
            // m_progrLevelSpk
            // 
            this.m_progrLevelSpk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progrLevelSpk.Location = new System.Drawing.Point(40, 45);
            this.m_progrLevelSpk.Name = "m_progrLevelSpk";
            this.m_progrLevelSpk.Size = new System.Drawing.Size(10, 255);
            this.m_progrLevelSpk.TabIndex = 6;
            this.m_progrLevelSpk.Value = 10;
            // 
            // m_btnMute
            // 
            this.m_btnMute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnMute.ImageList = this.m_imageListSpeakers;
            this.m_btnMute.Location = new System.Drawing.Point(4, 314);
            this.m_btnMute.Name = "m_btnMute";
            this.m_btnMute.Size = new System.Drawing.Size(48, 43);
            this.m_btnMute.TabIndex = 4;
            this.m_btnMute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_btnMute.UseVisualStyleBackColor = true;
            this.m_btnMute.Click += new System.EventHandler(this.m_btnMute_Click);
            // 
            // m_trackVolume
            // 
            this.m_trackVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_trackVolume.LargeChange = 10;
            this.m_trackVolume.Location = new System.Drawing.Point(7, 37);
            this.m_trackVolume.Maximum = 100;
            this.m_trackVolume.MinimumSize = new System.Drawing.Size(45, 80);
            this.m_trackVolume.Name = "m_trackVolume";
            this.m_trackVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.m_trackVolume.Size = new System.Drawing.Size(45, 271);
            this.m_trackVolume.TabIndex = 3;
            this.m_trackVolume.TickFrequency = 10;
            this.m_trackVolume.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.m_trackVolume.Scroll += new System.EventHandler(this.m_trackVolume_Scroll);
            this.m_trackVolume.ValueChanged += new System.EventHandler(this.m_trackVolume_ValueChanged);
            // 
            // m_txtLog
            // 
            this.m_txtLog.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtLog.Location = new System.Drawing.Point(0, 0);
            this.m_txtLog.Name = "m_txtLog";
            this.m_txtLog.Size = new System.Drawing.Size(715, 32);
            this.m_txtLog.TabIndex = 0;
            this.m_txtLog.Text = "";
            // 
            // m_timer
            // 
            this.m_timer.Enabled = true;
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(717, 436);
            this.Controls.Add(this.m_pnlMain);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(660, 360);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Active Audio End Point";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.m_pnlMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.m_tabDevices.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList m_imageListSpeakers;
        private System.Windows.Forms.TrackBar m_trackVolume;
        private System.Windows.Forms.Button m_btnMute;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel m_status1;
        private System.Windows.Forms.ToolStripStatusLabel m_status2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox m_txtLog;
        private System.Windows.Forms.Panel m_pnlMain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button m_btnMicMute;
        private MZ.Controls.VerticalProgressBar m_progrLevelsMic;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.ImageList m_imageListMic;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl m_tabDevices;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MediaDeviceListUserControl m_DeviceListPlayback;
        private MediaDeviceListUserControl m_DeviceListRecording;
        private MZ.Controls.VerticalProgressBar m_progrLevelSpk;
    }
}

