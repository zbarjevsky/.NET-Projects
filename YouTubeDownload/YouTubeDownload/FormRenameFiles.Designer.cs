
namespace YouTubeDownload
{
    partial class FormRenameFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRenameFiles));
            this.m_btnBrowseForFolder = new System.Windows.Forms.Button();
            this.m_lnkOutputFolder = new System.Windows.Forms.LinkLabel();
            this.m_listFileNames = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_btnApply = new System.Windows.Forms.Button();
            this.m_btnClose = new System.Windows.Forms.Button();
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_progress = new System.Windows.Forms.ToolStripProgressBar();
            this.m_status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_numMaxLength = new System.Windows.Forms.NumericUpDown();
            this.m_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numMaxLength)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnBrowseForFolder
            // 
            this.m_btnBrowseForFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseForFolder.Image = ((System.Drawing.Image)(resources.GetObject("m_btnBrowseForFolder.Image")));
            this.m_btnBrowseForFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnBrowseForFolder.Location = new System.Drawing.Point(760, 12);
            this.m_btnBrowseForFolder.Name = "m_btnBrowseForFolder";
            this.m_btnBrowseForFolder.Size = new System.Drawing.Size(41, 33);
            this.m_btnBrowseForFolder.TabIndex = 6;
            this.m_btnBrowseForFolder.Text = "...";
            this.m_btnBrowseForFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnBrowseForFolder.UseVisualStyleBackColor = true;
            this.m_btnBrowseForFolder.Click += new System.EventHandler(this.m_btnBrowseForFolder_Click);
            // 
            // m_lnkOutputFolder
            // 
            this.m_lnkOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkOutputFolder.BackColor = System.Drawing.SystemColors.Control;
            this.m_lnkOutputFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lnkOutputFolder.Location = new System.Drawing.Point(12, 13);
            this.m_lnkOutputFolder.Name = "m_lnkOutputFolder";
            this.m_lnkOutputFolder.Size = new System.Drawing.Size(742, 32);
            this.m_lnkOutputFolder.TabIndex = 7;
            this.m_lnkOutputFolder.TabStop = true;
            this.m_lnkOutputFolder.Text = "Working Folder";
            this.m_lnkOutputFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lnkOutputFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkOutputFolder_LinkClicked);
            // 
            // m_listFileNames
            // 
            this.m_listFileNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listFileNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.m_listFileNames.FullRowSelect = true;
            this.m_listFileNames.GridLines = true;
            this.m_listFileNames.HideSelection = false;
            this.m_listFileNames.LabelEdit = true;
            this.m_listFileNames.Location = new System.Drawing.Point(12, 137);
            this.m_listFileNames.Name = "m_listFileNames";
            this.m_listFileNames.Size = new System.Drawing.Size(789, 249);
            this.m_listFileNames.TabIndex = 8;
            this.m_listFileNames.UseCompatibleStateImageBehavior = false;
            this.m_listFileNames.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Name - After";
            this.columnHeader1.Width = 400;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File Name - Before";
            this.columnHeader2.Width = 800;
            // 
            // m_btnApply
            // 
            this.m_btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnApply.Image = ((System.Drawing.Image)(resources.GetObject("m_btnApply.Image")));
            this.m_btnApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnApply.Location = new System.Drawing.Point(724, 396);
            this.m_btnApply.Name = "m_btnApply";
            this.m_btnApply.Size = new System.Drawing.Size(75, 23);
            this.m_btnApply.TabIndex = 9;
            this.m_btnApply.Text = "Apply...";
            this.m_btnApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnApply.UseVisualStyleBackColor = true;
            this.m_btnApply.Click += new System.EventHandler(this.m_btnApply_Click);
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("m_btnClose.Image")));
            this.m_btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnClose.Location = new System.Drawing.Point(643, 396);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 10;
            this.m_btnClose.Text = "Cancel";
            this.m_btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnClose.UseVisualStyleBackColor = true;
            this.m_btnClose.Click += new System.EventHandler(this.m_btnClose_Click);
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.m_status2,
            this.m_progress});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 428);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Size = new System.Drawing.Size(813, 22);
            this.m_statusStrip.TabIndex = 12;
            this.m_statusStrip.Text = "statusStrip1";
            // 
            // m_status1
            // 
            this.m_status1.Name = "m_status1";
            this.m_status1.Size = new System.Drawing.Size(39, 17);
            this.m_status1.Text = "Ready";
            // 
            // m_progress
            // 
            this.m_progress.AutoSize = false;
            this.m_progress.Name = "m_progress";
            this.m_progress.Size = new System.Drawing.Size(400, 16);
            // 
            // m_status2
            // 
            this.m_status2.Name = "m_status2";
            this.m_status2.Size = new System.Drawing.Size(357, 17);
            this.m_status2.Spring = true;
            this.m_status2.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Max Length:";
            // 
            // m_numMaxLength
            // 
            this.m_numMaxLength.Location = new System.Drawing.Point(82, 73);
            this.m_numMaxLength.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.m_numMaxLength.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.m_numMaxLength.Name = "m_numMaxLength";
            this.m_numMaxLength.Size = new System.Drawing.Size(52, 20);
            this.m_numMaxLength.TabIndex = 14;
            this.m_numMaxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numMaxLength.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // FormRenameFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(813, 450);
            this.Controls.Add(this.m_numMaxLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_statusStrip);
            this.Controls.Add(this.m_btnClose);
            this.Controls.Add(this.m_btnApply);
            this.Controls.Add(this.m_listFileNames);
            this.Controls.Add(this.m_lnkOutputFolder);
            this.Controls.Add(this.m_btnBrowseForFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormRenameFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormRenameFiles";
            this.Load += new System.EventHandler(this.FormRenameFiles_Load);
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numMaxLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button m_btnBrowseForFolder;
        private System.Windows.Forms.LinkLabel m_lnkOutputFolder;
        private System.Windows.Forms.ListView m_listFileNames;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button m_btnApply;
        private System.Windows.Forms.Button m_btnClose;
        private System.Windows.Forms.StatusStrip m_statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_status1;
        private System.Windows.Forms.ToolStripStatusLabel m_status2;
        private System.Windows.Forms.ToolStripProgressBar m_progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown m_numMaxLength;
    }
}