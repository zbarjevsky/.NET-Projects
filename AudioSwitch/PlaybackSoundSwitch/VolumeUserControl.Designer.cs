namespace PlaybackSoundSwitch
{
    partial class VolumeUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VolumeUserControl));
            this.m_grpVolume = new System.Windows.Forms.GroupBox();
            this.m_lbl = new System.Windows.Forms.Label();
            this.m_progrLevel = new MZ.Controls.ColorBarsVerticalProgressBar();
            this.m_btnMute = new System.Windows.Forms.Button();
            this.m_imgListMic = new System.Windows.Forms.ImageList(this.components);
            this.m_trackVolume = new System.Windows.Forms.TrackBar();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_imgListSpk = new System.Windows.Forms.ImageList(this.components);
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_grpVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_progrLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackVolume)).BeginInit();
            this.m_pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_grpVolume
            // 
            this.m_grpVolume.Controls.Add(this.m_lbl);
            this.m_grpVolume.Controls.Add(this.m_progrLevel);
            this.m_grpVolume.Controls.Add(this.m_btnMute);
            this.m_grpVolume.Controls.Add(this.m_trackVolume);
            this.m_grpVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grpVolume.Location = new System.Drawing.Point(0, 0);
            this.m_grpVolume.Name = "m_grpVolume";
            this.m_grpVolume.Size = new System.Drawing.Size(64, 294);
            this.m_grpVolume.TabIndex = 0;
            this.m_grpVolume.TabStop = false;
            this.m_grpVolume.Text = "Volume";
            // 
            // m_lbl
            // 
            this.m_lbl.AutoSize = true;
            this.m_lbl.Location = new System.Drawing.Point(15, 21);
            this.m_lbl.Name = "m_lbl";
            this.m_lbl.Size = new System.Drawing.Size(27, 13);
            this.m_lbl.TabIndex = 0;
            this.m_lbl.Text = "30%";
            // 
            // m_progrLevel
            // 
            this.m_progrLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progrLevel.InactiveColorTheme = MZ.Controls.ColorBarsVerticalProgressBar.InactiveColorsTheme.Pale;
            this.m_progrLevel.Location = new System.Drawing.Point(35, 45);
            this.m_progrLevel.MinimumSize = new System.Drawing.Size(20, 0);
            this.m_progrLevel.Name = "m_progrLevel";
            this.m_progrLevel.Size = new System.Drawing.Size(20, 186);
            this.m_progrLevel.TabIndex = 3;
            this.m_progrLevel.TabStop = false;
            this.m_progrLevel.Value = 80;
            // 
            // m_btnMute
            // 
            this.m_btnMute.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnMute.ImageIndex = 3;
            this.m_btnMute.ImageList = this.m_imgListMic;
            this.m_btnMute.Location = new System.Drawing.Point(8, 245);
            this.m_btnMute.Name = "m_btnMute";
            this.m_btnMute.Size = new System.Drawing.Size(48, 43);
            this.m_btnMute.TabIndex = 0;
            this.m_btnMute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_btnMute.UseVisualStyleBackColor = true;
            this.m_btnMute.Click += new System.EventHandler(this.m_btnMute_Click);
            // 
            // m_imgListMic
            // 
            this.m_imgListMic.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgListMic.ImageStream")));
            this.m_imgListMic.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgListMic.Images.SetKeyName(0, "Mic1.png");
            this.m_imgListMic.Images.SetKeyName(1, "MicMute1.png");
            this.m_imgListMic.Images.SetKeyName(2, "sad_32px.png");
            this.m_imgListMic.Images.SetKeyName(3, "gears_32px.png");
            // 
            // m_trackVolume
            // 
            this.m_trackVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_trackVolume.LargeChange = 10;
            this.m_trackVolume.Location = new System.Drawing.Point(6, 37);
            this.m_trackVolume.Maximum = 100;
            this.m_trackVolume.MinimumSize = new System.Drawing.Size(45, 80);
            this.m_trackVolume.Name = "m_trackVolume";
            this.m_trackVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.m_trackVolume.Size = new System.Drawing.Size(45, 202);
            this.m_trackVolume.TabIndex = 2;
            this.m_trackVolume.TickFrequency = 10;
            this.m_trackVolume.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.m_trackVolume.Value = 30;
            this.m_trackVolume.Scroll += new System.EventHandler(this.m_trackVolume_Scroll);
            this.m_trackVolume.ValueChanged += new System.EventHandler(this.m_trackVolume_ValueChanged);
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Controls.Add(this.m_grpVolume);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(64, 294);
            this.m_pnlMain.TabIndex = 8;
            // 
            // m_imgListSpk
            // 
            this.m_imgListSpk.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgListSpk.ImageStream")));
            this.m_imgListSpk.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgListSpk.Images.SetKeyName(0, "Spk1.png");
            // 
            // m_timer
            // 
            this.m_timer.Enabled = true;
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // VolumeUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlMain);
            this.MinimumSize = new System.Drawing.Size(58, 200);
            this.Name = "VolumeUserControl";
            this.Size = new System.Drawing.Size(64, 294);
            this.Load += new System.EventHandler(this.VolumeUserControl_Load);
            this.m_grpVolume.ResumeLayout(false);
            this.m_grpVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_progrLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackVolume)).EndInit();
            this.m_pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_grpVolume;
        private System.Windows.Forms.Button m_btnMute;
        private System.Windows.Forms.TrackBar m_trackVolume;
        private System.Windows.Forms.Panel m_pnlMain;
        private System.Windows.Forms.ImageList m_imgListMic;
        private System.Windows.Forms.ImageList m_imgListSpk;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.ToolTip toolTip1;
        private MZ.Controls.ColorBarsVerticalProgressBar m_progrLevel;
        private System.Windows.Forms.Label m_lbl;
    }
}
