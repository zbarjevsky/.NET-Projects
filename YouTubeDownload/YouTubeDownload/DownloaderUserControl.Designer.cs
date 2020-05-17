namespace YouTubeDownload
{
    partial class DownloaderUserControl
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
            this.m_txtOutput = new System.Windows.Forms.RichTextBox();
            this.m_lnkDestination = new System.Windows.Forms.LinkLabel();
            this.m_ProgressBar = new MZ.Tools.Windows7ProgressBar();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.m_pnlOutput = new System.Windows.Forms.Panel();
            this.m_lblTime = new System.Windows.Forms.Label();
            this.m_pnlLinks = new System.Windows.Forms.Panel();
            this.m_lblPlayListStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.m_pnlOutput.SuspendLayout();
            this.m_pnlLinks.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtOutput
            // 
            this.m_txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtOutput.BackColor = System.Drawing.Color.Black;
            this.m_txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtOutput.ForeColor = System.Drawing.Color.Pink;
            this.m_txtOutput.Location = new System.Drawing.Point(1, 22);
            this.m_txtOutput.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.m_txtOutput.Name = "m_txtOutput";
            this.m_txtOutput.ReadOnly = true;
            this.m_txtOutput.Size = new System.Drawing.Size(588, 186);
            this.m_txtOutput.TabIndex = 4;
            this.m_txtOutput.Text = "Output";
            this.m_txtOutput.WordWrap = false;
            // 
            // m_lnkDestination
            // 
            this.m_lnkDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkDestination.Location = new System.Drawing.Point(4, 4);
            this.m_lnkDestination.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lnkDestination.Name = "m_lnkDestination";
            this.m_lnkDestination.Size = new System.Drawing.Size(584, 16);
            this.m_lnkDestination.TabIndex = 5;
            this.m_lnkDestination.TabStop = true;
            this.m_lnkDestination.Text = "Destination: N/A";
            this.m_lnkDestination.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkDestination_LinkClicked);
            // 
            // m_ProgressBar
            // 
            this.m_ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ProgressBar.Location = new System.Drawing.Point(1, 0);
            this.m_ProgressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_ProgressBar.Name = "m_ProgressBar";
            this.m_ProgressBar.ShowInTaskbar = true;
            this.m_ProgressBar.Size = new System.Drawing.Size(439, 17);
            this.m_ProgressBar.TabIndex = 6;
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblStatus.Location = new System.Drawing.Point(4, 55);
            this.m_lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(584, 16);
            this.m_lblStatus.TabIndex = 8;
            this.m_lblStatus.Text = "Status: N/A";
            // 
            // m_pnlOutput
            // 
            this.m_pnlOutput.Controls.Add(this.m_lblTime);
            this.m_pnlOutput.Controls.Add(this.m_txtOutput);
            this.m_pnlOutput.Controls.Add(this.m_ProgressBar);
            this.m_pnlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlOutput.Location = new System.Drawing.Point(0, 76);
            this.m_pnlOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_pnlOutput.Name = "m_pnlOutput";
            this.m_pnlOutput.Size = new System.Drawing.Size(592, 210);
            this.m_pnlOutput.TabIndex = 9;
            // 
            // m_lblTime
            // 
            this.m_lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTime.AutoSize = true;
            this.m_lblTime.Location = new System.Drawing.Point(448, 1);
            this.m_lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblTime.Name = "m_lblTime";
            this.m_lblTime.Size = new System.Drawing.Size(20, 17);
            this.m_lblTime.TabIndex = 7;
            this.m_lblTime.Text = "...";
            // 
            // m_pnlLinks
            // 
            this.m_pnlLinks.Controls.Add(this.m_lblPlayListStatus);
            this.m_pnlLinks.Controls.Add(this.m_lnkDestination);
            this.m_pnlLinks.Controls.Add(this.m_lblStatus);
            this.m_pnlLinks.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlLinks.Location = new System.Drawing.Point(0, 0);
            this.m_pnlLinks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_pnlLinks.Name = "m_pnlLinks";
            this.m_pnlLinks.Size = new System.Drawing.Size(592, 76);
            this.m_pnlLinks.TabIndex = 10;
            // 
            // m_lblPlayListStatus
            // 
            this.m_lblPlayListStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblPlayListStatus.Location = new System.Drawing.Point(4, 30);
            this.m_lblPlayListStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblPlayListStatus.Name = "m_lblPlayListStatus";
            this.m_lblPlayListStatus.Size = new System.Drawing.Size(584, 16);
            this.m_lblPlayListStatus.TabIndex = 9;
            this.m_lblPlayListStatus.Text = "Status: N/A";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DownloaderUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlOutput);
            this.Controls.Add(this.m_pnlLinks);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DownloaderUserControl";
            this.Size = new System.Drawing.Size(592, 286);
            this.Load += new System.EventHandler(this.DownloaderUserControl_Load);
            this.m_pnlOutput.ResumeLayout(false);
            this.m_pnlOutput.PerformLayout();
            this.m_pnlLinks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox m_txtOutput;
        private System.Windows.Forms.LinkLabel m_lnkDestination;
        private MZ.Tools.Windows7ProgressBar m_ProgressBar;
        private System.Windows.Forms.Label m_lblStatus;
        private System.Windows.Forms.Panel m_pnlOutput;
        private System.Windows.Forms.Panel m_pnlLinks;
        private System.Windows.Forms.Label m_lblPlayListStatus;
        private System.Windows.Forms.Label m_lblTime;
        private System.Windows.Forms.Timer timer1;
    }
}
