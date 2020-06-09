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
            MZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet1 = new MZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VolumeUserControl));
            this.m_grpVolume = new System.Windows.Forms.GroupBox();
            this.m_lbl = new System.Windows.Forms.Label();
            this.m_progrLevel = new MZ.WinForms.ColorBarsProgressBar();
            this.m_btnMute = new System.Windows.Forms.Button();
            this.m_imgListMic = new System.Windows.Forms.ImageList(this.components);
            this.m_trackVolume = new ColorSlider.ColorSlider();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_imgListSpk = new System.Windows.Forms.ImageList(this.components);
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_grpVolume.SuspendLayout();
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
            themeColorSet1.Part1_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet1.Part1_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Part2_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet1.Part2_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Part3_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet1.Part3_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Theme = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Regular;
            themeColorSet1.Threshold1 = 100;
            themeColorSet1.Threshold2 = 100;
            this.m_progrLevel.ColorTheme = themeColorSet1;
            this.m_progrLevel.Location = new System.Drawing.Point(12, 44);
            this.m_progrLevel.MinimumSize = new System.Drawing.Size(16, 0);
            this.m_progrLevel.Name = "m_progrLevel";
            this.m_progrLevel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.m_progrLevel.Size = new System.Drawing.Size(16, 187);
            this.m_progrLevel.TabIndex = 3;
            this.m_progrLevel.TabStop = false;
            this.m_progrLevel.Value = 30;
            // 
            // m_btnMute
            // 
            this.m_btnMute.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnMute.ImageIndex = 3;
            this.m_btnMute.ImageList = this.m_imgListMic;
            this.m_btnMute.Location = new System.Drawing.Point(7, 240);
            this.m_btnMute.Name = "m_btnMute";
            this.m_btnMute.Size = new System.Drawing.Size(48, 48);
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
            this.m_trackVolume.BarPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(94)))), ((int)(((byte)(110)))));
            this.m_trackVolume.BarPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(60)))), ((int)(((byte)(74)))));
            this.m_trackVolume.BorderRoundRectSize = new System.Drawing.Size(1, 1);
            this.m_trackVolume.ElapsedInnerColor = System.Drawing.Color.DarkOliveGreen;
            this.m_trackVolume.ElapsedPenColorBottom = System.Drawing.Color.ForestGreen;
            this.m_trackVolume.ElapsedPenColorTop = System.Drawing.Color.DarkGreen;
            this.m_trackVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.m_trackVolume.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_trackVolume.LargeChange = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_trackVolume.Location = new System.Drawing.Point(28, 47);
            this.m_trackVolume.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_trackVolume.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_trackVolume.MinimumSize = new System.Drawing.Size(20, 80);
            this.m_trackVolume.Name = "m_trackVolume";
            this.m_trackVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.m_trackVolume.ScaleDivisions = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_trackVolume.ScaleSubDivisions = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.m_trackVolume.ShowDivisionsText = true;
            this.m_trackVolume.ShowSmallScale = false;
            this.m_trackVolume.Size = new System.Drawing.Size(25, 182);
            this.m_trackVolume.SmallChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_trackVolume.TabIndex = 2;
            this.m_trackVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
            this.m_trackVolume.ThumbPenColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
            this.m_trackVolume.ThumbRoundRectSize = new System.Drawing.Size(4, 4);
            this.m_trackVolume.ThumbSize = new System.Drawing.Size(20, 10);
            this.m_trackVolume.TickAdd = 0F;
            this.m_trackVolume.TickColor = System.Drawing.Color.White;
            this.m_trackVolume.TickDivide = 0F;
            this.m_trackVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.m_trackVolume.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.m_trackVolume.ValueChanged += new System.EventHandler(this.m_trackVolume_ValueChanged);
            this.m_trackVolume.Scroll += new System.Windows.Forms.ScrollEventHandler(this.m_trackVolume_Scroll);
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
            this.m_pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_grpVolume;
        private System.Windows.Forms.Button m_btnMute;
        private ColorSlider.ColorSlider m_trackVolume;
        private System.Windows.Forms.Panel m_pnlMain;
        private System.Windows.Forms.ImageList m_imgListMic;
        private System.Windows.Forms.ImageList m_imgListSpk;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.ToolTip toolTip1;
        private MZ.WinForms.ColorBarsProgressBar m_progrLevel;
        private System.Windows.Forms.Label m_lbl;
    }
}
