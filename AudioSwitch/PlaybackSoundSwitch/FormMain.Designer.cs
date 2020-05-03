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
            this.m_listDevices = new ListViewExtensions.ListViewCollapsibleGroups();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_imageListSpeakers = new System.Windows.Forms.ImageList(this.components);
            this.m_trackVolume = new System.Windows.Forms.TrackBar();
            this.m_btnMute = new System.Windows.Forms.Button();
            this.m_btnActivate = new System.Windows.Forms.Button();
            this.m_btnRefresh = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_status2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackVolume)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.m_listDevices.Location = new System.Drawing.Point(12, 44);
            this.m_listDevices.MultiSelect = false;
            this.m_listDevices.Name = "m_listDevices";
            this.m_listDevices.ShowItemToolTips = true;
            this.m_listDevices.Size = new System.Drawing.Size(565, 271);
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
            this.m_trackVolume.Location = new System.Drawing.Point(596, 41);
            this.m_trackVolume.Maximum = 100;
            this.m_trackVolume.Name = "m_trackVolume";
            this.m_trackVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.m_trackVolume.Size = new System.Drawing.Size(45, 234);
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
            this.m_btnMute.Location = new System.Drawing.Point(590, 291);
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
            this.m_btnActivate.Location = new System.Drawing.Point(108, 12);
            this.m_btnActivate.Name = "m_btnActivate";
            this.m_btnActivate.Size = new System.Drawing.Size(469, 25);
            this.m_btnActivate.TabIndex = 1;
            this.m_btnActivate.Text = "Set Active: ";
            this.m_btnActivate.UseVisualStyleBackColor = true;
            this.m_btnActivate.Click += new System.EventHandler(this.m_btnActivate_Click);
            // 
            // m_btnRefresh
            // 
            this.m_btnRefresh.Image = global::PlaybackSoundSwitch.Properties.Resources.Refresh21;
            this.m_btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.m_btnRefresh.Name = "m_btnRefresh";
            this.m_btnRefresh.Size = new System.Drawing.Size(90, 25);
            this.m_btnRefresh.TabIndex = 0;
            this.m_btnRefresh.Text = "Refresh";
            this.m_btnRefresh.UseVisualStyleBackColor = true;
            this.m_btnRefresh.Click += new System.EventHandler(this.m_btnRefresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.m_status2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 324);
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
            this.m_status2.Size = new System.Drawing.Size(568, 17);
            this.m_status2.Spring = true;
            this.m_status2.Text = "...";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(653, 346);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.m_btnActivate);
            this.Controls.Add(this.m_btnMute);
            this.Controls.Add(this.m_trackVolume);
            this.Controls.Add(this.m_btnRefresh);
            this.Controls.Add(this.m_listDevices);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 250);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Active Audio End Point";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_trackVolume)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
    }
}

