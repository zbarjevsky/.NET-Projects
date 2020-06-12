namespace SimpleBackup
{
    partial class FormBackupProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBackupProgress));
            MZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet1 = new MZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            this.m_listFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_txtInfoBottom = new System.Windows.Forms.TextBox();
            this.m_btnClose = new System.Windows.Forms.Button();
            this.m_btnContinue = new System.Windows.Forms.Button();
            this.m_cmbOption = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnPause = new System.Windows.Forms.Button();
            this.m_btnStart = new System.Windows.Forms.Button();
            this.m_btnAbort = new System.Windows.Forms.Button();
            this.m_txtInfoTop = new System.Windows.Forms.TextBox();
            this.m_cmbViewFilter = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_lblStatusProgress = new System.Windows.Forms.Label();
            this.m_btnPrepare = new System.Windows.Forms.Button();
            this.m_spliMain = new System.Windows.Forms.SplitContainer();
            this.m_progressFile = new MZ.WinForms.ColorBarsProgressBar();
            this.m_progressBarMain = new MZ.Tools.Windows7ProgressBar();
            this.m_chartProgress = new WindowsFormsApp1.ChartProgressUserControl();
            this.m_pnlStatus = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_spliMain)).BeginInit();
            this.m_spliMain.Panel1.SuspendLayout();
            this.m_spliMain.Panel2.SuspendLayout();
            this.m_spliMain.SuspendLayout();
            this.m_pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_listFiles
            // 
            this.m_listFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.m_listFiles.FullRowSelect = true;
            this.m_listFiles.GridLines = true;
            this.m_listFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listFiles.HideSelection = false;
            this.m_listFiles.Location = new System.Drawing.Point(12, 63);
            this.m_listFiles.MultiSelect = false;
            this.m_listFiles.Name = "m_listFiles";
            this.m_listFiles.Size = new System.Drawing.Size(805, 199);
            this.m_listFiles.SmallImageList = this.imageList1;
            this.m_listFiles.TabIndex = 8;
            this.m_listFiles.UseCompatibleStateImageBehavior = false;
            this.m_listFiles.View = System.Windows.Forms.View.Details;
            this.m_listFiles.VirtualListSize = 1000;
            this.m_listFiles.VirtualMode = true;
            this.m_listFiles.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.m_listFiles_RetrieveVirtualItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Status";
            this.columnHeader1.Width = 95;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "From";
            this.columnHeader2.Width = 500;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "To";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 78;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "SmileGray.ico");
            this.imageList1.Images.SetKeyName(1, "arrow-forward_16.ico");
            this.imageList1.Images.SetKeyName(2, "SmileGreen.ico");
            this.imageList1.Images.SetKeyName(3, "SmileRed.ico");
            this.imageList1.Images.SetKeyName(4, "ok.ico");
            this.imageList1.Images.SetKeyName(5, "close.ico");
            // 
            // m_txtInfoBottom
            // 
            this.m_txtInfoBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInfoBottom.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtInfoBottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtInfoBottom.Location = new System.Drawing.Point(12, 13);
            this.m_txtInfoBottom.Name = "m_txtInfoBottom";
            this.m_txtInfoBottom.ReadOnly = true;
            this.m_txtInfoBottom.Size = new System.Drawing.Size(714, 13);
            this.m_txtInfoBottom.TabIndex = 0;
            this.m_txtInfoBottom.Text = "Press Start To Begin";
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(744, 8);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 1;
            this.m_btnClose.Text = "Done";
            this.m_btnClose.UseVisualStyleBackColor = true;
            // 
            // m_btnContinue
            // 
            this.m_btnContinue.Location = new System.Drawing.Point(173, 11);
            this.m_btnContinue.Name = "m_btnContinue";
            this.m_btnContinue.Size = new System.Drawing.Size(75, 23);
            this.m_btnContinue.TabIndex = 3;
            this.m_btnContinue.Text = "Resume";
            this.m_btnContinue.UseVisualStyleBackColor = true;
            this.m_btnContinue.Click += new System.EventHandler(this.m_btnContinue_Click);
            // 
            // m_cmbOption
            // 
            this.m_cmbOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbOption.DropDownWidth = 150;
            this.m_cmbOption.FormattingEnabled = true;
            this.m_cmbOption.Items.AddRange(new object[] {
            "Overwrite All Older",
            "Overwrite All",
            "Skip All Existing"});
            this.m_cmbOption.Location = new System.Drawing.Point(447, 13);
            this.m_cmbOption.Name = "m_cmbOption";
            this.m_cmbOption.Size = new System.Drawing.Size(138, 21);
            this.m_cmbOption.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(347, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Overwrite Options:";
            // 
            // m_btnPause
            // 
            this.m_btnPause.Image = global::SimpleBackup.Properties.Resources.pause_on;
            this.m_btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnPause.Location = new System.Drawing.Point(254, 11);
            this.m_btnPause.Name = "m_btnPause";
            this.m_btnPause.Size = new System.Drawing.Size(75, 23);
            this.m_btnPause.TabIndex = 2;
            this.m_btnPause.Text = "Pause";
            this.m_btnPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnPause.UseVisualStyleBackColor = true;
            this.m_btnPause.Click += new System.EventHandler(this.m_btnPause_Click);
            // 
            // m_btnStart
            // 
            this.m_btnStart.Image = global::SimpleBackup.Properties.Resources.play_on;
            this.m_btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnStart.Location = new System.Drawing.Point(92, 11);
            this.m_btnStart.Name = "m_btnStart";
            this.m_btnStart.Size = new System.Drawing.Size(75, 23);
            this.m_btnStart.TabIndex = 1;
            this.m_btnStart.Text = "Start";
            this.m_btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnStart.UseVisualStyleBackColor = true;
            this.m_btnStart.Click += new System.EventHandler(this.m_btnStart_Click);
            // 
            // m_btnAbort
            // 
            this.m_btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAbort.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnAbort.ImageIndex = 5;
            this.m_btnAbort.ImageList = this.imageList1;
            this.m_btnAbort.Location = new System.Drawing.Point(742, 285);
            this.m_btnAbort.Name = "m_btnAbort";
            this.m_btnAbort.Size = new System.Drawing.Size(75, 23);
            this.m_btnAbort.TabIndex = 12;
            this.m_btnAbort.Text = "Abort";
            this.m_btnAbort.UseVisualStyleBackColor = true;
            this.m_btnAbort.Click += new System.EventHandler(this.m_btnAbort_Click);
            // 
            // m_txtInfoTop
            // 
            this.m_txtInfoTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInfoTop.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtInfoTop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorProvider1.SetIconAlignment(this.m_txtInfoTop, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.errorProvider1.SetIconPadding(this.m_txtInfoTop, 1);
            this.m_txtInfoTop.Location = new System.Drawing.Point(13, 44);
            this.m_txtInfoTop.Name = "m_txtInfoTop";
            this.m_txtInfoTop.ReadOnly = true;
            this.m_txtInfoTop.Size = new System.Drawing.Size(804, 13);
            this.m_txtInfoTop.TabIndex = 7;
            this.m_txtInfoTop.Text = "Disk Free Space: 0k";
            // 
            // m_cmbViewFilter
            // 
            this.m_cmbViewFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbViewFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbViewFilter.DropDownWidth = 150;
            this.m_cmbViewFilter.FormattingEnabled = true;
            this.m_cmbViewFilter.Items.AddRange(new object[] {
            "Show All",
            "Show Errors",
            "Show Unrocessed"});
            this.m_cmbViewFilter.Location = new System.Drawing.Point(717, 14);
            this.m_cmbViewFilter.Name = "m_cmbViewFilter";
            this.m_cmbViewFilter.Size = new System.Drawing.Size(100, 21);
            this.m_cmbViewFilter.TabIndex = 6;
            this.m_cmbViewFilter.SelectionChangeCommitted += new System.EventHandler(this.m_cmbViewFilter_SelectionChangeCommitted);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // m_lblStatusProgress
            // 
            this.m_lblStatusProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblStatusProgress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblStatusProgress.Location = new System.Drawing.Point(39, 118);
            this.m_lblStatusProgress.Name = "m_lblStatusProgress";
            this.m_lblStatusProgress.Size = new System.Drawing.Size(748, 77);
            this.m_lblStatusProgress.TabIndex = 9;
            this.m_lblStatusProgress.Text = "Ready...";
            this.m_lblStatusProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btnPrepare
            // 
            this.m_btnPrepare.Location = new System.Drawing.Point(13, 11);
            this.m_btnPrepare.Name = "m_btnPrepare";
            this.m_btnPrepare.Size = new System.Drawing.Size(75, 23);
            this.m_btnPrepare.TabIndex = 0;
            this.m_btnPrepare.Text = "Prepare...";
            this.m_btnPrepare.UseVisualStyleBackColor = true;
            this.m_btnPrepare.Click += new System.EventHandler(this.m_btnPrepare_Click);
            // 
            // m_spliMain
            // 
            this.m_spliMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_spliMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_spliMain.Location = new System.Drawing.Point(0, 0);
            this.m_spliMain.Name = "m_spliMain";
            this.m_spliMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_spliMain.Panel1
            // 
            this.m_spliMain.Panel1.Controls.Add(this.m_btnPrepare);
            this.m_spliMain.Panel1.Controls.Add(this.m_lblStatusProgress);
            this.m_spliMain.Panel1.Controls.Add(this.m_listFiles);
            this.m_spliMain.Panel1.Controls.Add(this.m_progressFile);
            this.m_spliMain.Panel1.Controls.Add(this.m_btnAbort);
            this.m_spliMain.Panel1.Controls.Add(this.m_cmbViewFilter);
            this.m_spliMain.Panel1.Controls.Add(this.m_btnStart);
            this.m_spliMain.Panel1.Controls.Add(this.m_txtInfoTop);
            this.m_spliMain.Panel1.Controls.Add(this.m_progressBarMain);
            this.m_spliMain.Panel1.Controls.Add(this.label1);
            this.m_spliMain.Panel1.Controls.Add(this.m_btnPause);
            this.m_spliMain.Panel1.Controls.Add(this.m_cmbOption);
            this.m_spliMain.Panel1.Controls.Add(this.m_btnContinue);
            // 
            // m_spliMain.Panel2
            // 
            this.m_spliMain.Panel2.Controls.Add(this.m_chartProgress);
            this.m_spliMain.Size = new System.Drawing.Size(831, 476);
            this.m_spliMain.SplitterDistance = 319;
            this.m_spliMain.TabIndex = 0;
            // 
            // m_progressFile
            // 
            this.m_progressFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            themeColorSet1.Part1_ActiveColor = System.Drawing.Color.DodgerBlue;
            themeColorSet1.Part1_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Part2_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet1.Part2_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Part3_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet1.Part3_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Theme = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Custom;
            themeColorSet1.Threshold1 = 101;
            themeColorSet1.Threshold2 = 101;
            this.m_progressFile.ColorTheme = themeColorSet1;
            this.m_progressFile.Location = new System.Drawing.Point(12, 268);
            this.m_progressFile.Name = "m_progressFile";
            this.m_progressFile.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.m_progressFile.Size = new System.Drawing.Size(805, 13);
            this.m_progressFile.TabIndex = 10;
            this.m_progressFile.TabStop = false;
            // 
            // m_progressBarMain
            // 
            this.m_progressBarMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progressBarMain.Location = new System.Drawing.Point(12, 285);
            this.m_progressBarMain.Maximum = 10000;
            this.m_progressBarMain.Name = "m_progressBarMain";
            this.m_progressBarMain.ShowInTaskbar = true;
            this.m_progressBarMain.Size = new System.Drawing.Size(724, 23);
            this.m_progressBarMain.TabIndex = 11;
            // 
            // m_chartProgress
            // 
            this.m_chartProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_chartProgress.Location = new System.Drawing.Point(0, 0);
            this.m_chartProgress.Name = "m_chartProgress";
            this.m_chartProgress.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.m_chartProgress.Size = new System.Drawing.Size(829, 151);
            this.m_chartProgress.TabIndex = 16;
            // 
            // m_pnlStatus
            // 
            this.m_pnlStatus.Controls.Add(this.m_btnClose);
            this.m_pnlStatus.Controls.Add(this.m_txtInfoBottom);
            this.m_pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlStatus.Location = new System.Drawing.Point(0, 476);
            this.m_pnlStatus.Name = "m_pnlStatus";
            this.m_pnlStatus.Size = new System.Drawing.Size(831, 38);
            this.m_pnlStatus.TabIndex = 1;
            // 
            // FormBackupProgress
            // 
            this.AcceptButton = this.m_btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(831, 514);
            this.Controls.Add(this.m_spliMain);
            this.Controls.Add(this.m_pnlStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(650, 400);
            this.Name = "FormBackupProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Copy Progress";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBackupProgress_FormClosing);
            this.Load += new System.EventHandler(this.FormBackupProgress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.m_spliMain.Panel1.ResumeLayout(false);
            this.m_spliMain.Panel1.PerformLayout();
            this.m_spliMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_spliMain)).EndInit();
            this.m_spliMain.ResumeLayout(false);
            this.m_pnlStatus.ResumeLayout(false);
            this.m_pnlStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_listFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button m_btnAbort;
        private System.Windows.Forms.Button m_btnStart;
        private System.Windows.Forms.TextBox m_txtInfoBottom;
        private System.Windows.Forms.Button m_btnClose;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private MZ.Tools.Windows7ProgressBar m_progressBarMain;
        private System.Windows.Forms.Button m_btnPause;
        private System.Windows.Forms.Button m_btnContinue;
        private System.Windows.Forms.ComboBox m_cmbOption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtInfoTop;
        private System.Windows.Forms.ComboBox m_cmbViewFilter;
        private MZ.WinForms.ColorBarsProgressBar m_progressFile;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label m_lblStatusProgress;
        private System.Windows.Forms.Button m_btnPrepare;
        private System.Windows.Forms.SplitContainer m_spliMain;
        private System.Windows.Forms.Panel m_pnlStatus;
        private WindowsFormsApp1.ChartProgressUserControl m_chartProgress;
    }
}