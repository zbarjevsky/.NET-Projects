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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Connected", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Unplugged", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Disabled", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Not Present", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_imageListSpeakers = new System.Windows.Forms.ImageList(this.components);
            this.m_trackVolume = new System.Windows.Forms.TrackBar();
            this.m_btnMute = new System.Windows.Forms.Button();
            this.m_btnActivate = new System.Windows.Forms.Button();
            this.m_btnRefresh = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_listDevices = new ListViewExtensions.ListViewCollapsibleGroups();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_txtLog = new System.Windows.Forms.RichTextBox();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackVolume)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.m_pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_imageListSpeakers
            // 
            this.m_imageListSpeakers.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.m_imageListSpeakers.ImageSize = new System.Drawing.Size(32, 32);
            this.m_imageListSpeakers.TransparentColor = System.Drawing.Color.Transparent;
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
            this.m_trackVolume.Size = new System.Drawing.Size(45, 192);
            this.m_trackVolume.TabIndex = 3;
            this.m_trackVolume.TickFrequency = 10;
            this.m_trackVolume.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.m_trackVolume.Scroll += new System.EventHandler(this.m_trackVolume_Scroll);
            this.m_trackVolume.ValueChanged += new System.EventHandler(this.m_trackVolume_ValueChanged);
            // 
            // m_btnMute
            // 
            this.m_btnMute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnMute.ImageList = this.m_imageListSpeakers;
            this.m_btnMute.Location = new System.Drawing.Point(4, 235);
            this.m_btnMute.Name = "m_btnMute";
            this.m_btnMute.Size = new System.Drawing.Size(48, 43);
            this.m_btnMute.TabIndex = 4;
            this.m_btnMute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_btnMute.UseVisualStyleBackColor = true;
            this.m_btnMute.Click += new System.EventHandler(this.m_btnMute_Click);
            // 
            // m_btnActivate
            // 
            this.m_btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnActivate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnActivate.Location = new System.Drawing.Point(104, 16);
            this.m_btnActivate.Name = "m_btnActivate";
            this.m_btnActivate.Size = new System.Drawing.Size(467, 25);
            this.m_btnActivate.TabIndex = 1;
            this.m_btnActivate.Text = "Set Active: ";
            this.m_btnActivate.UseVisualStyleBackColor = true;
            this.m_btnActivate.Click += new System.EventHandler(this.m_btnActivate_Click);
            // 
            // m_btnRefresh
            // 
            this.m_btnRefresh.Image = global::PlaybackSoundSwitch.Properties.Resources.Refresh21;
            this.m_btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnRefresh.Location = new System.Drawing.Point(8, 16);
            this.m_btnRefresh.Name = "m_btnRefresh";
            this.m_btnRefresh.Size = new System.Drawing.Size(90, 25);
            this.m_btnRefresh.TabIndex = 0;
            this.m_btnRefresh.Text = "Refresh";
            this.m_btnRefresh.UseVisualStyleBackColor = true;
            this.m_btnRefresh.Click += new System.EventHandler(this.m_btnRefresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.m_status2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 328);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(653, 22);
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
            this.m_status2.Size = new System.Drawing.Size(599, 17);
            this.m_status2.Spring = true;
            this.m_status2.Text = "...";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.m_btnMute);
            this.groupBox1.Controls.Add(this.m_trackVolume);
            this.groupBox1.Location = new System.Drawing.Point(578, 11);
            this.groupBox1.MaximumSize = new System.Drawing.Size(57, 400);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(57, 284);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Volume";
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
            this.splitContainer1.Panel1.Controls.Add(this.m_btnActivate);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.m_listDevices);
            this.splitContainer1.Panel1.Controls.Add(this.m_btnRefresh);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_txtLog);
            this.splitContainer1.Panel2MinSize = 1;
            this.splitContainer1.Size = new System.Drawing.Size(653, 328);
            this.splitContainer1.SplitterDistance = 299;
            this.splitContainer1.TabIndex = 7;
            // 
            // m_listDevices
            // 
            this.m_listDevices.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.m_listDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_listDevices.FullRowSelect = true;
            this.m_listDevices.GridLines = true;
            listViewGroup1.Header = "Connected";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Unplugged";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "Disabled";
            listViewGroup3.Name = "listViewGroup3";
            listViewGroup4.Header = "Not Present";
            listViewGroup4.Name = "listViewGroup4";
            this.m_listDevices.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.m_listDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listDevices.HideSelection = false;
            this.m_listDevices.Location = new System.Drawing.Point(8, 48);
            this.m_listDevices.MultiSelect = false;
            this.m_listDevices.Name = "m_listDevices";
            this.m_listDevices.ShowItemToolTips = true;
            this.m_listDevices.Size = new System.Drawing.Size(563, 247);
            this.m_listDevices.TabIndex = 2;
            this.m_listDevices.UseCompatibleStateImageBehavior = false;
            this.m_listDevices.View = System.Windows.Forms.View.Details;
            this.m_listDevices.SelectedIndexChanged += new System.EventHandler(this.m_listDevices_SelectedIndexChanged);
            this.m_listDevices.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_listDevices_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Devices";
            this.columnHeader1.Width = 362;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Volume";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 73;
            // 
            // m_txtLog
            // 
            this.m_txtLog.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtLog.Location = new System.Drawing.Point(0, 0);
            this.m_txtLog.Name = "m_txtLog";
            this.m_txtLog.Size = new System.Drawing.Size(651, 23);
            this.m_txtLog.TabIndex = 0;
            this.m_txtLog.Text = "";
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.AutoScroll = true;
            this.m_pnlMain.Controls.Add(this.splitContainer1);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(653, 328);
            this.m_pnlMain.TabIndex = 7;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(653, 350);
            this.Controls.Add(this.m_pnlMain);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(660, 360);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Active Audio End Point";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_trackVolume)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.m_pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewExtensions.ListViewCollapsibleGroups m_listDevices;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button m_btnRefresh;
        private System.Windows.Forms.ImageList m_imageListSpeakers;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TrackBar m_trackVolume;
        private System.Windows.Forms.Button m_btnMute;
        private System.Windows.Forms.Button m_btnActivate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel m_status1;
        private System.Windows.Forms.ToolStripStatusLabel m_status2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox m_txtLog;
        private System.Windows.Forms.Panel m_pnlMain;
    }
}

