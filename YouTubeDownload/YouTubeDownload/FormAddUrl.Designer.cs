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
            this.m_cmbFileName = new System.Windows.Forms.ComboBox();
            this.m_lnkOutputFileName = new System.Windows.Forms.LinkLabel();
            this.m_pnlButtons = new System.Windows.Forms.Panel();
            this.m_cmbAdditionalParameters = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.m_pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnBrowseForFolder
            // 
            this.m_btnBrowseForFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseForFolder.Location = new System.Drawing.Point(602, 54);
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
            this.m_chkAudioOnly.Location = new System.Drawing.Point(784, 54);
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
            this.m_lnkOutputFolder.AutoEllipsis = true;
            this.m_lnkOutputFolder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lnkOutputFolder.Location = new System.Drawing.Point(184, 54);
            this.m_lnkOutputFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lnkOutputFolder.Name = "m_lnkOutputFolder";
            this.m_lnkOutputFolder.Size = new System.Drawing.Size(403, 28);
            this.m_lnkOutputFolder.TabIndex = 5;
            this.m_lnkOutputFolder.TabStop = true;
            this.m_lnkOutputFolder.Text = "Output Folder: ";
            this.m_lnkOutputFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lnkOutputFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkOutputFolder_LinkClicked);
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
            this.m_chkNoPlayList.Location = new System.Drawing.Point(784, 13);
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
            this.m_txtUrl.Location = new System.Drawing.Point(184, 11);
            this.m_txtUrl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.m_txtUrl.Name = "m_txtUrl";
            this.m_txtUrl.Size = new System.Drawing.Size(533, 26);
            this.m_txtUrl.TabIndex = 1;
            this.m_txtUrl.TextChanged += new System.EventHandler(this.m_txtUrl_TextChanged);
            // 
            // m_btnAddUrl
            // 
            this.m_btnAddUrl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAddUrl.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnAddUrl.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAddUrl.Image")));
            this.m_btnAddUrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnAddUrl.Location = new System.Drawing.Point(329, 14);
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
            this.m_btnCancel.Location = new System.Drawing.Point(497, 14);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(136, 31);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_cmbFileName
            // 
            this.m_cmbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbFileName.FormattingEnabled = true;
            this.m_cmbFileName.Items.AddRange(new object[] {
            "%(title)s--%(id)s.%(ext)s",
            "%(title)s.%(ext)s",
            "%(playlist)s/%(playlist_index)s - %(title)s.%(ext)s"});
            this.m_cmbFileName.Location = new System.Drawing.Point(281, 103);
            this.m_cmbFileName.Name = "m_cmbFileName";
            this.m_cmbFileName.Size = new System.Drawing.Size(436, 28);
            this.m_cmbFileName.TabIndex = 9;
            // 
            // m_lnkOutputFileName
            // 
            this.m_lnkOutputFileName.AutoSize = true;
            this.m_lnkOutputFileName.Location = new System.Drawing.Point(18, 106);
            this.m_lnkOutputFileName.Name = "m_lnkOutputFileName";
            this.m_lnkOutputFileName.Size = new System.Drawing.Size(207, 20);
            this.m_lnkOutputFileName.TabIndex = 10;
            this.m_lnkOutputFileName.TabStop = true;
            this.m_lnkOutputFileName.Text = "Output File Name Template:";
            this.m_lnkOutputFileName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkOutputFileName_LinkClicked);
            // 
            // m_pnlButtons
            // 
            this.m_pnlButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.m_pnlButtons.Controls.Add(this.m_btnCancel);
            this.m_pnlButtons.Controls.Add(this.m_btnAddUrl);
            this.m_pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlButtons.Location = new System.Drawing.Point(0, 206);
            this.m_pnlButtons.Name = "m_pnlButtons";
            this.m_pnlButtons.Size = new System.Drawing.Size(964, 58);
            this.m_pnlButtons.TabIndex = 11;
            // 
            // m_cmbAdditionalParameters
            // 
            this.m_cmbAdditionalParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbAdditionalParameters.FormattingEnabled = true;
            this.m_cmbAdditionalParameters.Items.AddRange(new object[] {
            " --embed-thumbnail ",
            " --postprocessor-args \"-ss 00:01:00.00\""});
            this.m_cmbAdditionalParameters.Location = new System.Drawing.Point(281, 154);
            this.m_cmbAdditionalParameters.Name = "m_cmbAdditionalParameters";
            this.m_cmbAdditionalParameters.Size = new System.Drawing.Size(436, 28);
            this.m_cmbAdditionalParameters.TabIndex = 14;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(56, 157);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(169, 20);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Additional Parameters:";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkOutputFileName_LinkClicked);
            // 
            // FormAddUrl
            // 
            this.AcceptButton = this.m_btnAddUrl;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(964, 264);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.m_cmbAdditionalParameters);
            this.Controls.Add(this.m_pnlButtons);
            this.Controls.Add(this.m_lnkOutputFileName);
            this.Controls.Add(this.m_cmbFileName);
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
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "FormAddUrl";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Paste URL";
            this.Load += new System.EventHandler(this.FormAddUrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.m_pnlButtons.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox m_cmbFileName;
        private System.Windows.Forms.LinkLabel m_lnkOutputFileName;
        private System.Windows.Forms.Panel m_pnlButtons;
        private System.Windows.Forms.ComboBox m_cmbAdditionalParameters;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}