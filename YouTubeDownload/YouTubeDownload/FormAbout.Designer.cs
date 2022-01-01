namespace YouTubeDownload
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_lnkYouTubeDL = new System.Windows.Forms.LinkLabel();
            this.m_lnkFFMpeg = new System.Windows.Forms.LinkLabel();
            this.m_lblVer1 = new System.Windows.Forms.Label();
            this.m_txtVer = new System.Windows.Forms.RichTextBox();
            this.m_lnkYouTubeDLP = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(53, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // m_lnkYouTubeDL
            // 
            this.m_lnkYouTubeDL.AutoSize = true;
            this.m_lnkYouTubeDL.Location = new System.Drawing.Point(97, 47);
            this.m_lnkYouTubeDL.Name = "m_lnkYouTubeDL";
            this.m_lnkYouTubeDL.Size = new System.Drawing.Size(77, 13);
            this.m_lnkYouTubeDL.TabIndex = 1;
            this.m_lnkYouTubeDL.TabStop = true;
            this.m_lnkYouTubeDL.Text = "youtube-dl ver:";
            this.m_lnkYouTubeDL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkYouTubeDL_LinkClicked);
            // 
            // m_lnkFFMpeg
            // 
            this.m_lnkFFMpeg.AutoSize = true;
            this.m_lnkFFMpeg.Location = new System.Drawing.Point(97, 81);
            this.m_lnkFFMpeg.Name = "m_lnkFFMpeg";
            this.m_lnkFFMpeg.Size = new System.Drawing.Size(60, 13);
            this.m_lnkFFMpeg.TabIndex = 2;
            this.m_lnkFFMpeg.TabStop = true;
            this.m_lnkFFMpeg.Text = "ffmpeg ver:";
            this.m_lnkFFMpeg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkFFMpeg_LinkClicked);
            // 
            // m_lblVer1
            // 
            this.m_lblVer1.AutoSize = true;
            this.m_lblVer1.Location = new System.Drawing.Point(97, 18);
            this.m_lblVer1.Name = "m_lblVer1";
            this.m_lblVer1.Size = new System.Drawing.Size(115, 13);
            this.m_lblVer1.TabIndex = 3;
            this.m_lblVer1.Text = "Video Downloader ver:";
            // 
            // m_txtVer
            // 
            this.m_txtVer.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtVer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtVer.Location = new System.Drawing.Point(100, 113);
            this.m_txtVer.Name = "m_txtVer";
            this.m_txtVer.ReadOnly = true;
            this.m_txtVer.Size = new System.Drawing.Size(517, 184);
            this.m_txtVer.TabIndex = 4;
            this.m_txtVer.Text = "";
            this.m_txtVer.WordWrap = false;
            // 
            // m_lnkYouTubeDLP
            // 
            this.m_lnkYouTubeDLP.AutoSize = true;
            this.m_lnkYouTubeDLP.Location = new System.Drawing.Point(97, 61);
            this.m_lnkYouTubeDLP.Name = "m_lnkYouTubeDLP";
            this.m_lnkYouTubeDLP.Size = new System.Drawing.Size(53, 13);
            this.m_lnkYouTubeDLP.TabIndex = 5;
            this.m_lnkYouTubeDLP.TabStop = true;
            this.m_lnkYouTubeDLP.Text = "yt-dlp ver:";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 309);
            this.Controls.Add(this.m_lnkYouTubeDLP);
            this.Controls.Add(this.m_txtVer);
            this.Controls.Add(this.m_lblVer1);
            this.Controls.Add(this.m_lnkFFMpeg);
            this.Controls.Add(this.m_lnkYouTubeDL);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormAbout";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel m_lnkYouTubeDL;
        private System.Windows.Forms.LinkLabel m_lnkFFMpeg;
        private System.Windows.Forms.Label m_lblVer1;
        private System.Windows.Forms.RichTextBox m_txtVer;
        private System.Windows.Forms.LinkLabel m_lnkYouTubeDLP;
    }
}