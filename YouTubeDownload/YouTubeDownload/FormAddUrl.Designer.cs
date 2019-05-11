namespace YouTubeDownload
{
    partial class FormAddUrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddUrl));
            this.m_btnBrowseForFolder = new System.Windows.Forms.Button();
            this.m_chkAudioOnly = new System.Windows.Forms.CheckBox();
            this.m_lnkOutputFolder = new System.Windows.Forms.LinkLabel();
            this.m_lblUrl = new System.Windows.Forms.Label();
            this.m_chkNoPlayList = new System.Windows.Forms.CheckBox();
            this.m_txtUrl = new System.Windows.Forms.TextBox();
            this.m_btnAddUrl = new System.Windows.Forms.Button();
            this.m_errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnBrowseForFolder
            // 
            this.m_btnBrowseForFolder.Location = new System.Drawing.Point(18, 54);
            this.m_btnBrowseForFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnBrowseForFolder.Name = "m_btnBrowseForFolder";
            this.m_btnBrowseForFolder.Size = new System.Drawing.Size(115, 29);
            this.m_btnBrowseForFolder.TabIndex = 4;
            this.m_btnBrowseForFolder.Text = "Change...";
            this.m_btnBrowseForFolder.UseVisualStyleBackColor = true;
            this.m_btnBrowseForFolder.Click += new System.EventHandler(this.m_btnBrowseForFolder_Click);
            // 
            // m_chkAudioOnly
            // 
            this.m_chkAudioOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkAudioOnly.AutoSize = true;
            this.m_chkAudioOnly.Location = new System.Drawing.Point(816, 47);
            this.m_chkAudioOnly.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_chkAudioOnly.Name = "m_chkAudioOnly";
            this.m_chkAudioOnly.Size = new System.Drawing.Size(168, 24);
            this.m_chkAudioOnly.TabIndex = 3;
            this.m_chkAudioOnly.Text = "Extract Audio (mp3)";
            this.m_chkAudioOnly.UseVisualStyleBackColor = true;
            // 
            // m_lnkOutputFolder
            // 
            this.m_lnkOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkOutputFolder.Location = new System.Drawing.Point(142, 58);
            this.m_lnkOutputFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lnkOutputFolder.Name = "m_lnkOutputFolder";
            this.m_lnkOutputFolder.Size = new System.Drawing.Size(653, 24);
            this.m_lnkOutputFolder.TabIndex = 5;
            this.m_lnkOutputFolder.TabStop = true;
            this.m_lnkOutputFolder.Text = "Output Folder";
            // 
            // m_lblUrl
            // 
            this.m_lblUrl.AutoSize = true;
            this.m_lblUrl.Location = new System.Drawing.Point(18, 14);
            this.m_lblUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblUrl.Name = "m_lblUrl";
            this.m_lblUrl.Size = new System.Drawing.Size(115, 20);
            this.m_lblUrl.TabIndex = 0;
            this.m_lblUrl.Text = "YouTube URL:";
            // 
            // m_chkNoPlayList
            // 
            this.m_chkNoPlayList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkNoPlayList.AutoSize = true;
            this.m_chkNoPlayList.Checked = true;
            this.m_chkNoPlayList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkNoPlayList.Location = new System.Drawing.Point(816, 13);
            this.m_chkNoPlayList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_chkNoPlayList.Name = "m_chkNoPlayList";
            this.m_chkNoPlayList.Size = new System.Drawing.Size(110, 24);
            this.m_chkNoPlayList.TabIndex = 2;
            this.m_chkNoPlayList.Text = "No Play List";
            this.m_chkNoPlayList.UseVisualStyleBackColor = true;
            // 
            // m_txtUrl
            // 
            this.m_txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtUrl.Location = new System.Drawing.Point(144, 11);
            this.m_txtUrl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.m_txtUrl.Name = "m_txtUrl";
            this.m_txtUrl.Size = new System.Drawing.Size(651, 26);
            this.m_txtUrl.TabIndex = 1;
            this.m_txtUrl.TextChanged += new System.EventHandler(this.m_txtUrl_TextChanged);
            // 
            // m_btnAddUrl
            // 
            this.m_btnAddUrl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAddUrl.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnAddUrl.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAddUrl.Image")));
            this.m_btnAddUrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnAddUrl.Location = new System.Drawing.Point(341, 115);
            this.m_btnAddUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_btnAddUrl.Name = "m_btnAddUrl";
            this.m_btnAddUrl.Size = new System.Drawing.Size(136, 31);
            this.m_btnAddUrl.TabIndex = 6;
            this.m_btnAddUrl.Text = "Add && Start";
            this.m_btnAddUrl.UseVisualStyleBackColor = true;
            this.m_btnAddUrl.Click += new System.EventHandler(this.m_btnAddUrl_Click);
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnCancel.Location = new System.Drawing.Point(509, 115);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(136, 31);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormAddUrl
            // 
            this.AcceptButton = this.m_btnAddUrl;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(984, 162);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnAddUrl);
            this.Controls.Add(this.m_btnBrowseForFolder);
            this.Controls.Add(this.m_chkAudioOnly);
            this.Controls.Add(this.m_lnkOutputFolder);
            this.Controls.Add(this.m_lblUrl);
            this.Controls.Add(this.m_chkNoPlayList);
            this.Controls.Add(this.m_txtUrl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 180);
            this.Name = "FormAddUrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Paste URL";
            this.Load += new System.EventHandler(this.FormAddUrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnBrowseForFolder;
        private System.Windows.Forms.CheckBox m_chkAudioOnly;
        private System.Windows.Forms.LinkLabel m_lnkOutputFolder;
        private System.Windows.Forms.Label m_lblUrl;
        private System.Windows.Forms.CheckBox m_chkNoPlayList;
        private System.Windows.Forms.TextBox m_txtUrl;
        private System.Windows.Forms.Button m_btnAddUrl;
        private System.Windows.Forms.ErrorProvider m_errorProvider;
        private System.Windows.Forms.Button m_btnCancel;
    }
}