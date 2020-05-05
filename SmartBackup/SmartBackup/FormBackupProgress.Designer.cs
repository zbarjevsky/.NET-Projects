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
            this.m_btnAbort = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_btnStart = new System.Windows.Forms.Button();
            this.m_txtStatus = new System.Windows.Forms.TextBox();
            this.m_btnClose = new System.Windows.Forms.Button();
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
            this.columnHeader3});
            this.m_listFiles.FullRowSelect = true;
            this.m_listFiles.GridLines = true;
            this.m_listFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listFiles.HideSelection = false;
            this.m_listFiles.Location = new System.Drawing.Point(13, 42);
            this.m_listFiles.MultiSelect = false;
            this.m_listFiles.Name = "m_listFiles";
            this.m_listFiles.Size = new System.Drawing.Size(685, 463);
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
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "From";
            this.columnHeader2.Width = 300;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "To";
            this.columnHeader3.Width = 300;
            // 
            // m_btnAbort
            // 
            this.m_btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAbort.Location = new System.Drawing.Point(716, 77);
            this.m_btnAbort.Name = "m_btnAbort";
            this.m_btnAbort.Size = new System.Drawing.Size(75, 23);
            this.m_btnAbort.TabIndex = 1;
            this.m_btnAbort.Text = "Abort";
            this.m_btnAbort.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ok.ico");
            this.imageList1.Images.SetKeyName(1, "close.ico");
            this.imageList1.Images.SetKeyName(2, "SmileRed.ico");
            this.imageList1.Images.SetKeyName(3, "SmileGreen.ico");
            this.imageList1.Images.SetKeyName(4, "SmileGray.ico");
            // 
            // m_btnStart
            // 
            this.m_btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnStart.Location = new System.Drawing.Point(716, 42);
            this.m_btnStart.Name = "m_btnStart";
            this.m_btnStart.Size = new System.Drawing.Size(75, 23);
            this.m_btnStart.TabIndex = 2;
            this.m_btnStart.Text = "Start";
            this.m_btnStart.UseVisualStyleBackColor = true;
            // 
            // m_txtStatus
            // 
            this.m_txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtStatus.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtStatus.Location = new System.Drawing.Point(13, 517);
            this.m_txtStatus.Name = "m_txtStatus";
            this.m_txtStatus.ReadOnly = true;
            this.m_txtStatus.Size = new System.Drawing.Size(685, 13);
            this.m_txtStatus.TabIndex = 3;
            this.m_txtStatus.Text = "Press Start To Begin";
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(716, 483);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 4;
            this.m_btnClose.Text = "Close";
            this.m_btnClose.UseVisualStyleBackColor = true;
            // 
            // FormBackupProgress
            // 
            this.AcceptButton = this.m_btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(803, 543);
            this.Controls.Add(this.m_btnClose);
            this.Controls.Add(this.m_txtStatus);
            this.Controls.Add(this.m_btnStart);
            this.Controls.Add(this.m_btnAbort);
            this.Controls.Add(this.m_listFiles);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormBackupProgress";
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
    }
}