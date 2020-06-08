namespace SimpleBackup
{
    partial class FormBackupFolderProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBackupFolderProperties));
            MZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet1 = new MZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_txtFileType = new System.Windows.Forms.TextBox();
            this.m_txtExcludeType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_cmbPriority = new System.Windows.Forms.ComboBox();
            this.m_txtInfo = new System.Windows.Forms.TextBox();
            this.m_cmbSearchOptions = new System.Windows.Forms.ComboBox();
            this.m_splitFolders = new System.Windows.Forms.SplitContainer();
            this.m_explorerSrc = new MZ.WinForms.FileExplorerUserControl();
            this.m_explorerDst = new MZ.WinForms.FileExplorerUserControl();
            this.m_pnlOptions = new System.Windows.Forms.Panel();
            this.m_btnStartBackup = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_progressBar = new MZ.WinForms.ColorBarsProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitFolders)).BeginInit();
            this.m_splitFolders.Panel1.SuspendLayout();
            this.m_splitFolders.Panel2.SuspendLayout();
            this.m_splitFolders.SuspendLayout();
            this.m_pnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Backup to:";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Location = new System.Drawing.Point(921, 519);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 26);
            this.m_btnOk.TabIndex = 13;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(1002, 519);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 26);
            this.m_btnCancel.TabIndex = 14;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_txtFileType
            // 
            this.m_txtFileType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_txtFileType.Location = new System.Drawing.Point(274, 12);
            this.m_txtFileType.Name = "m_txtFileType";
            this.m_txtFileType.Size = new System.Drawing.Size(81, 20);
            this.m_txtFileType.TabIndex = 7;
            this.m_txtFileType.Text = "*.*";
            this.m_txtFileType.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // m_txtExcludeType
            // 
            this.m_txtExcludeType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_txtExcludeType.Location = new System.Drawing.Point(487, 12);
            this.m_txtExcludeType.Name = "m_txtExcludeType";
            this.m_txtExcludeType.Size = new System.Drawing.Size(81, 20);
            this.m_txtExcludeType.TabIndex = 9;
            this.m_txtExcludeType.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Include Files Types";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Exclude File Types";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(583, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Priority";
            // 
            // m_cmbPriority
            // 
            this.m_cmbPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbPriority.FormattingEnabled = true;
            this.m_cmbPriority.Location = new System.Drawing.Point(627, 12);
            this.m_cmbPriority.Name = "m_cmbPriority";
            this.m_cmbPriority.Size = new System.Drawing.Size(99, 21);
            this.m_cmbPriority.TabIndex = 11;
            // 
            // m_txtInfo
            // 
            this.m_txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorProvider1.SetIconAlignment(this.m_txtInfo, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.errorProvider1.SetIconPadding(this.m_txtInfo, 2);
            this.m_txtInfo.Location = new System.Drawing.Point(16, 527);
            this.m_txtInfo.Name = "m_txtInfo";
            this.m_txtInfo.ReadOnly = true;
            this.m_txtInfo.Size = new System.Drawing.Size(763, 13);
            this.m_txtInfo.TabIndex = 12;
            this.m_txtInfo.Text = "Calculating Folder Size...";
            // 
            // m_cmbSearchOptions
            // 
            this.m_cmbSearchOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmbSearchOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSearchOptions.FormattingEnabled = true;
            this.m_cmbSearchOptions.Location = new System.Drawing.Point(6, 12);
            this.m_cmbSearchOptions.Name = "m_cmbSearchOptions";
            this.m_cmbSearchOptions.Size = new System.Drawing.Size(151, 21);
            this.m_cmbSearchOptions.TabIndex = 16;
            this.m_cmbSearchOptions.SelectionChangeCommitted += new System.EventHandler(this.ValidateInput);
            // 
            // m_splitFolders
            // 
            this.m_splitFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_splitFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitFolders.Location = new System.Drawing.Point(0, 53);
            this.m_splitFolders.Name = "m_splitFolders";
            // 
            // m_splitFolders.Panel1
            // 
            this.m_splitFolders.Panel1.Controls.Add(this.m_explorerSrc);
            this.m_splitFolders.Panel1.Controls.Add(this.label1);
            // 
            // m_splitFolders.Panel2
            // 
            this.m_splitFolders.Panel2.Controls.Add(this.m_explorerDst);
            this.m_splitFolders.Panel2.Controls.Add(this.label2);
            this.m_splitFolders.Size = new System.Drawing.Size(1084, 462);
            this.m_splitFolders.SplitterDistance = 540;
            this.m_splitFolders.TabIndex = 20;
            // 
            // m_explorerSrc
            // 
            this.m_explorerSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_explorerSrc.CheckBoxes = true;
            this.m_explorerSrc.Location = new System.Drawing.Point(3, 40);
            this.m_explorerSrc.Name = "m_explorerSrc";
            this.m_explorerSrc.Size = new System.Drawing.Size(532, 417);
            this.m_explorerSrc.TabIndex = 19;
            // 
            // m_explorerDst
            // 
            this.m_explorerDst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_explorerDst.CheckBoxes = false;
            this.m_explorerDst.Location = new System.Drawing.Point(3, 40);
            this.m_explorerDst.Name = "m_explorerDst";
            this.m_explorerDst.Size = new System.Drawing.Size(532, 417);
            this.m_explorerDst.TabIndex = 18;
            // 
            // m_pnlOptions
            // 
            this.m_pnlOptions.Controls.Add(this.m_cmbSearchOptions);
            this.m_pnlOptions.Controls.Add(this.m_txtFileType);
            this.m_pnlOptions.Controls.Add(this.m_txtExcludeType);
            this.m_pnlOptions.Controls.Add(this.label3);
            this.m_pnlOptions.Controls.Add(this.m_cmbPriority);
            this.m_pnlOptions.Controls.Add(this.label4);
            this.m_pnlOptions.Controls.Add(this.label5);
            this.m_pnlOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlOptions.Location = new System.Drawing.Point(0, 0);
            this.m_pnlOptions.Name = "m_pnlOptions";
            this.m_pnlOptions.Size = new System.Drawing.Size(1084, 47);
            this.m_pnlOptions.TabIndex = 21;
            // 
            // m_btnStartBackup
            // 
            this.m_btnStartBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnStartBackup.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.errorProvider1.SetIconAlignment(this.m_btnStartBackup, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.errorProvider1.SetIconPadding(this.m_btnStartBackup, 3);
            this.m_btnStartBackup.Image = ((System.Drawing.Image)(resources.GetObject("m_btnStartBackup.Image")));
            this.m_btnStartBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnStartBackup.Location = new System.Drawing.Point(798, 519);
            this.m_btnStartBackup.Name = "m_btnStartBackup";
            this.m_btnStartBackup.Size = new System.Drawing.Size(105, 26);
            this.m_btnStartBackup.TabIndex = 23;
            this.m_btnStartBackup.Text = "Start Backup...";
            this.m_btnStartBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnStartBackup.UseVisualStyleBackColor = true;
            this.m_btnStartBackup.Click += new System.EventHandler(this.m_btnStartBackup_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // m_progressBar
            // 
            themeColorSet1.Part1_ActiveColor = System.Drawing.Color.MediumSlateBlue;
            themeColorSet1.Part1_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Part2_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet1.Part2_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Part3_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet1.Part3_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet1.Theme = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Custom;
            themeColorSet1.Threshold1 = 101;
            themeColorSet1.Threshold2 = 101;
            this.m_progressBar.ColorTheme = themeColorSet1;
            this.m_progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_progressBar.Location = new System.Drawing.Point(0, 549);
            this.m_progressBar.Name = "m_progressBar";
            this.m_progressBar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.m_progressBar.Size = new System.Drawing.Size(1084, 12);
            this.m_progressBar.TabIndex = 22;
            this.m_progressBar.TabStop = false;
            // 
            // FormBackupFolderProperties
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.m_btnStartBackup);
            this.Controls.Add(this.m_progressBar);
            this.Controls.Add(this.m_pnlOptions);
            this.Controls.Add(this.m_splitFolders);
            this.Controls.Add(this.m_txtInfo);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormBackupFolderProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Backup Properties";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormBackupFolderProperties_FormClosed);
            this.Load += new System.EventHandler(this.FormBackupFolderProperties_Load);
            this.m_splitFolders.Panel1.ResumeLayout(false);
            this.m_splitFolders.Panel1.PerformLayout();
            this.m_splitFolders.Panel2.ResumeLayout(false);
            this.m_splitFolders.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitFolders)).EndInit();
            this.m_splitFolders.ResumeLayout(false);
            this.m_pnlOptions.ResumeLayout(false);
            this.m_pnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.TextBox m_txtFileType;
        private System.Windows.Forms.TextBox m_txtExcludeType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox m_cmbPriority;
        private System.Windows.Forms.TextBox m_txtInfo;
        private System.Windows.Forms.ComboBox m_cmbSearchOptions;
        private MZ.WinForms.FileExplorerUserControl m_explorerDst;
        private MZ.WinForms.FileExplorerUserControl m_explorerSrc;
        private System.Windows.Forms.SplitContainer m_splitFolders;
        private System.Windows.Forms.Panel m_pnlOptions;
        private MZ.WinForms.ColorBarsProgressBar m_progressBar;
        private System.Windows.Forms.Button m_btnStartBackup;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}