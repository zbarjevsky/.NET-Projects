namespace SmartBackup
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
            this.m_listFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_txtStatus = new System.Windows.Forms.TextBox();
            this.m_btnClose = new System.Windows.Forms.Button();
            this.m_progrFile = new System.Windows.Forms.ProgressBar();
            this.m_btnContinue = new System.Windows.Forms.Button();
            this.m_cmbOption = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnPause = new System.Windows.Forms.Button();
            this.m_btnStart = new System.Windows.Forms.Button();
            this.m_btnAbort = new System.Windows.Forms.Button();
            this.m_txtInfo = new System.Windows.Forms.TextBox();
            this.m_cmbViewFilter = new System.Windows.Forms.ComboBox();
            this.m_progressBar1 = new MZ.Tools.Windows7ProgressBar();
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
            this.m_listFiles.Location = new System.Drawing.Point(12, 62);
            this.m_listFiles.MultiSelect = false;
            this.m_listFiles.Name = "m_listFiles";
            this.m_listFiles.Size = new System.Drawing.Size(785, 220);
            this.m_listFiles.SmallImageList = this.imageList1;
            this.m_listFiles.TabIndex = 0;
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
            this.imageList1.Images.SetKeyName(1, "SmileYellow.ico");
            this.imageList1.Images.SetKeyName(2, "SmileGreen.ico");
            this.imageList1.Images.SetKeyName(3, "SmileRed.ico");
            this.imageList1.Images.SetKeyName(4, "ok.ico");
            this.imageList1.Images.SetKeyName(5, "close.ico");
            // 
            // m_txtStatus
            // 
            this.m_txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtStatus.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtStatus.Location = new System.Drawing.Point(13, 335);
            this.m_txtStatus.Name = "m_txtStatus";
            this.m_txtStatus.ReadOnly = true;
            this.m_txtStatus.Size = new System.Drawing.Size(692, 13);
            this.m_txtStatus.TabIndex = 3;
            this.m_txtStatus.Text = "Press Start To Begin";
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(722, 330);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 4;
            this.m_btnClose.Text = "Close";
            this.m_btnClose.UseVisualStyleBackColor = true;
            // 
            // m_progrFile
            // 
            this.m_progrFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progrFile.Location = new System.Drawing.Point(12, 288);
            this.m_progrFile.Name = "m_progrFile";
            this.m_progrFile.Size = new System.Drawing.Size(785, 9);
            this.m_progrFile.TabIndex = 6;
            // 
            // m_btnContinue
            // 
            this.m_btnContinue.Location = new System.Drawing.Point(175, 9);
            this.m_btnContinue.Name = "m_btnContinue";
            this.m_btnContinue.Size = new System.Drawing.Size(75, 23);
            this.m_btnContinue.TabIndex = 8;
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
            this.m_cmbOption.Location = new System.Drawing.Point(368, 11);
            this.m_cmbOption.Name = "m_cmbOption";
            this.m_cmbOption.Size = new System.Drawing.Size(138, 21);
            this.m_cmbOption.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Overwrite Options:";
            // 
            // m_btnPause
            // 
            this.m_btnPause.Image = global::SmartBackup.Properties.Resources.pause_on;
            this.m_btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnPause.Location = new System.Drawing.Point(94, 9);
            this.m_btnPause.Name = "m_btnPause";
            this.m_btnPause.Size = new System.Drawing.Size(75, 23);
            this.m_btnPause.TabIndex = 7;
            this.m_btnPause.Text = "Pause";
            this.m_btnPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnPause.UseVisualStyleBackColor = true;
            this.m_btnPause.Click += new System.EventHandler(this.m_btnPause_Click);
            // 
            // m_btnStart
            // 
            this.m_btnStart.Image = global::SmartBackup.Properties.Resources.play_on;
            this.m_btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnStart.Location = new System.Drawing.Point(13, 9);
            this.m_btnStart.Name = "m_btnStart";
            this.m_btnStart.Size = new System.Drawing.Size(75, 23);
            this.m_btnStart.TabIndex = 2;
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
            this.m_btnAbort.Location = new System.Drawing.Point(722, 302);
            this.m_btnAbort.Name = "m_btnAbort";
            this.m_btnAbort.Size = new System.Drawing.Size(75, 23);
            this.m_btnAbort.TabIndex = 1;
            this.m_btnAbort.Text = "Abort";
            this.m_btnAbort.UseVisualStyleBackColor = true;
            this.m_btnAbort.Click += new System.EventHandler(this.m_btnAbort_Click);
            // 
            // m_txtInfo
            // 
            this.m_txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInfo.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtInfo.Location = new System.Drawing.Point(13, 43);
            this.m_txtInfo.Name = "m_txtInfo";
            this.m_txtInfo.ReadOnly = true;
            this.m_txtInfo.Size = new System.Drawing.Size(784, 13);
            this.m_txtInfo.TabIndex = 11;
            this.m_txtInfo.Text = "Disk Free Space: 0k";
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
            this.m_cmbViewFilter.Location = new System.Drawing.Point(697, 12);
            this.m_cmbViewFilter.Name = "m_cmbViewFilter";
            this.m_cmbViewFilter.Size = new System.Drawing.Size(100, 21);
            this.m_cmbViewFilter.TabIndex = 12;
            this.m_cmbViewFilter.SelectionChangeCommitted += new System.EventHandler(this.m_cmbViewFilter_SelectionChangeCommitted);
            // 
            // m_progressBar1
            // 
            this.m_progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progressBar1.Location = new System.Drawing.Point(12, 302);
            this.m_progressBar1.Maximum = 10000;
            this.m_progressBar1.Name = "m_progressBar1";
            this.m_progressBar1.ShowInTaskbar = true;
            this.m_progressBar1.Size = new System.Drawing.Size(704, 23);
            this.m_progressBar1.TabIndex = 5;
            // 
            // FormBackupProgress
            // 
            this.AcceptButton = this.m_btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(809, 361);
            this.Controls.Add(this.m_cmbViewFilter);
            this.Controls.Add(this.m_txtInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_cmbOption);
            this.Controls.Add(this.m_btnContinue);
            this.Controls.Add(this.m_btnPause);
            this.Controls.Add(this.m_progrFile);
            this.Controls.Add(this.m_progressBar1);
            this.Controls.Add(this.m_btnClose);
            this.Controls.Add(this.m_txtStatus);
            this.Controls.Add(this.m_btnStart);
            this.Controls.Add(this.m_btnAbort);
            this.Controls.Add(this.m_listFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(650, 400);
            this.Name = "FormBackupProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Backup Progress";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBackupProgress_FormClosing);
            this.Load += new System.EventHandler(this.FormBackupProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView m_listFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button m_btnAbort;
        private System.Windows.Forms.Button m_btnStart;
        private System.Windows.Forms.TextBox m_txtStatus;
        private System.Windows.Forms.Button m_btnClose;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private MZ.Tools.Windows7ProgressBar m_progressBar1;
        private System.Windows.Forms.ProgressBar m_progrFile;
        private System.Windows.Forms.Button m_btnPause;
        private System.Windows.Forms.Button m_btnContinue;
        private System.Windows.Forms.ComboBox m_cmbOption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtInfo;
        private System.Windows.Forms.ComboBox m_cmbViewFilter;
    }
}