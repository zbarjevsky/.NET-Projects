namespace YouTubeDownload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_txtUrl = new System.Windows.Forms.TextBox();
            this.m_btnDownload = new System.Windows.Forms.Button();
            this.m_btnOpenFolder = new System.Windows.Forms.Button();
            this.m_txtOutput = new System.Windows.Forms.RichTextBox();
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_Status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_Status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.m_btnUpdate = new System.Windows.Forms.Button();
            this.m_statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtUrl
            // 
            this.m_txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtUrl.Location = new System.Drawing.Point(0, 24);
            this.m_txtUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_txtUrl.Name = "m_txtUrl";
            this.m_txtUrl.Size = new System.Drawing.Size(796, 26);
            this.m_txtUrl.TabIndex = 0;
            // 
            // m_btnDownload
            // 
            this.m_btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDownload.Location = new System.Drawing.Point(819, 24);
            this.m_btnDownload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnDownload.Name = "m_btnDownload";
            this.m_btnDownload.Size = new System.Drawing.Size(136, 35);
            this.m_btnDownload.TabIndex = 1;
            this.m_btnDownload.Text = "Download...";
            this.m_btnDownload.UseVisualStyleBackColor = true;
            this.m_btnDownload.Click += new System.EventHandler(this.m_btnDownload_Click);
            // 
            // m_btnOpenFolder
            // 
            this.m_btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOpenFolder.Location = new System.Drawing.Point(819, 69);
            this.m_btnOpenFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnOpenFolder.Name = "m_btnOpenFolder";
            this.m_btnOpenFolder.Size = new System.Drawing.Size(136, 35);
            this.m_btnOpenFolder.TabIndex = 2;
            this.m_btnOpenFolder.Text = "Open Folder...";
            this.m_btnOpenFolder.UseVisualStyleBackColor = true;
            this.m_btnOpenFolder.Click += new System.EventHandler(this.m_btnOpenFolder_Click);
            // 
            // m_txtOutput
            // 
            this.m_txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtOutput.BackColor = System.Drawing.Color.Black;
            this.m_txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtOutput.ForeColor = System.Drawing.Color.Pink;
            this.m_txtOutput.Location = new System.Drawing.Point(-2, 60);
            this.m_txtOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_txtOutput.Name = "m_txtOutput";
            this.m_txtOutput.ReadOnly = true;
            this.m_txtOutput.Size = new System.Drawing.Size(798, 366);
            this.m_txtOutput.TabIndex = 3;
            this.m_txtOutput.Text = "Output";
            this.m_txtOutput.WordWrap = false;
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_Status1,
            this.m_Status2,
            this.m_StatusProgress});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 431);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.m_statusStrip.Size = new System.Drawing.Size(984, 31);
            this.m_statusStrip.TabIndex = 4;
            this.m_statusStrip.Text = ".";
            // 
            // m_Status1
            // 
            this.m_Status1.Name = "m_Status1";
            this.m_Status1.Size = new System.Drawing.Size(42, 26);
            this.m_Status1.Text = "Ready.";
            // 
            // m_Status2
            // 
            this.m_Status2.Name = "m_Status2";
            this.m_Status2.Size = new System.Drawing.Size(767, 26);
            this.m_Status2.Spring = true;
            this.m_Status2.Text = "...";
            // 
            // m_StatusProgress
            // 
            this.m_StatusProgress.Name = "m_StatusProgress";
            this.m_StatusProgress.Size = new System.Drawing.Size(150, 25);
            // 
            // m_btnUpdate
            // 
            this.m_btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnUpdate.Location = new System.Drawing.Point(819, 391);
            this.m_btnUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnUpdate.Name = "m_btnUpdate";
            this.m_btnUpdate.Size = new System.Drawing.Size(136, 35);
            this.m_btnUpdate.TabIndex = 5;
            this.m_btnUpdate.Text = "Update DL";
            this.m_btnUpdate.UseVisualStyleBackColor = true;
            this.m_btnUpdate.Click += new System.EventHandler(this.m_btnUpdate_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.m_btnUpdate);
            this.Controls.Add(this.m_statusStrip);
            this.Controls.Add(this.m_txtOutput);
            this.Controls.Add(this.m_btnOpenFolder);
            this.Controls.Add(this.m_btnDownload);
            this.Controls.Add(this.m_txtUrl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube - Download";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtUrl;
        private System.Windows.Forms.Button m_btnDownload;
        private System.Windows.Forms.Button m_btnOpenFolder;
        private System.Windows.Forms.RichTextBox m_txtOutput;
        private System.Windows.Forms.StatusStrip m_statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_Status1;
        private System.Windows.Forms.ToolStripStatusLabel m_Status2;
        private System.Windows.Forms.ToolStripProgressBar m_StatusProgress;
        private System.Windows.Forms.Button m_btnUpdate;
    }
}

