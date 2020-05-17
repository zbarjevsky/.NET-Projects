namespace SmartBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBackupFolderProperties));
            this.m_txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnBrowseSrc = new System.Windows.Forms.Button();
            this.m_btnBrowseDst = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtDstFolder = new System.Windows.Forms.TextBox();
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
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_txtSource
            // 
            this.m_txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSource.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtSource.Location = new System.Drawing.Point(68, 14);
            this.m_txtSource.Name = "m_txtSource";
            this.m_txtSource.ReadOnly = true;
            this.m_txtSource.Size = new System.Drawing.Size(352, 20);
            this.m_txtSource.TabIndex = 1;
            this.m_txtSource.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source:";
            // 
            // m_btnBrowseSrc
            // 
            this.m_btnBrowseSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseSrc.Location = new System.Drawing.Point(427, 12);
            this.m_btnBrowseSrc.Name = "m_btnBrowseSrc";
            this.m_btnBrowseSrc.Size = new System.Drawing.Size(34, 23);
            this.m_btnBrowseSrc.TabIndex = 2;
            this.m_btnBrowseSrc.Text = "...";
            this.m_btnBrowseSrc.UseVisualStyleBackColor = true;
            this.m_btnBrowseSrc.Click += new System.EventHandler(this.m_btnBrowseSrc_Click);
            // 
            // m_btnBrowseDst
            // 
            this.m_btnBrowseDst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseDst.Location = new System.Drawing.Point(427, 39);
            this.m_btnBrowseDst.Name = "m_btnBrowseDst";
            this.m_btnBrowseDst.Size = new System.Drawing.Size(34, 23);
            this.m_btnBrowseDst.TabIndex = 5;
            this.m_btnBrowseDst.Text = "...";
            this.m_btnBrowseDst.UseVisualStyleBackColor = true;
            this.m_btnBrowseDst.Click += new System.EventHandler(this.m_btnBrowseDst_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Backup to:";
            // 
            // m_txtDstFolder
            // 
            this.m_txtDstFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDstFolder.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtDstFolder.Location = new System.Drawing.Point(68, 41);
            this.m_txtDstFolder.Name = "m_txtDstFolder";
            this.m_txtDstFolder.ReadOnly = true;
            this.m_txtDstFolder.Size = new System.Drawing.Size(352, 20);
            this.m_txtDstFolder.TabIndex = 4;
            this.m_txtDstFolder.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Location = new System.Drawing.Point(310, 209);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 13;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(391, 209);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 14;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_txtFileType
            // 
            this.m_txtFileType.Location = new System.Drawing.Point(194, 102);
            this.m_txtFileType.Name = "m_txtFileType";
            this.m_txtFileType.Size = new System.Drawing.Size(226, 20);
            this.m_txtFileType.TabIndex = 7;
            this.m_txtFileType.Text = "*.*";
            this.m_txtFileType.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // m_txtExcludeType
            // 
            this.m_txtExcludeType.Location = new System.Drawing.Point(194, 128);
            this.m_txtExcludeType.Name = "m_txtExcludeType";
            this.m_txtExcludeType.Size = new System.Drawing.Size(226, 20);
            this.m_txtExcludeType.TabIndex = 9;
            this.m_txtExcludeType.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Include Files Types";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Exclude File Types";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(69, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Priority";
            // 
            // m_cmbPriority
            // 
            this.m_cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbPriority.FormattingEnabled = true;
            this.m_cmbPriority.Location = new System.Drawing.Point(194, 158);
            this.m_cmbPriority.Name = "m_cmbPriority";
            this.m_cmbPriority.Size = new System.Drawing.Size(226, 21);
            this.m_cmbPriority.TabIndex = 11;
            // 
            // m_txtInfo
            // 
            this.m_txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtInfo.Location = new System.Drawing.Point(12, 214);
            this.m_txtInfo.Name = "m_txtInfo";
            this.m_txtInfo.ReadOnly = true;
            this.m_txtInfo.Size = new System.Drawing.Size(280, 13);
            this.m_txtInfo.TabIndex = 12;
            this.m_txtInfo.Text = "Calculating Folder Size...";
            // 
            // m_cmbSearchOptions
            // 
            this.m_cmbSearchOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSearchOptions.FormattingEnabled = true;
            this.m_cmbSearchOptions.Location = new System.Drawing.Point(194, 75);
            this.m_cmbSearchOptions.Name = "m_cmbSearchOptions";
            this.m_cmbSearchOptions.Size = new System.Drawing.Size(226, 21);
            this.m_cmbSearchOptions.TabIndex = 16;
            this.m_cmbSearchOptions.SelectionChangeCommitted += new System.EventHandler(this.ValidateInput);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(69, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Include Subfolders";
            // 
            // FormBackupFolderProperties
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(473, 244);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_cmbSearchOptions);
            this.Controls.Add(this.m_txtInfo);
            this.Controls.Add(this.m_cmbPriority);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_txtExcludeType);
            this.Controls.Add(this.m_txtFileType);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnBrowseDst);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txtDstFolder);
            this.Controls.Add(this.m_btnBrowseSrc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_txtSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 240);
            this.Name = "FormBackupFolderProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Backup Properties";
            this.Load += new System.EventHandler(this.FormBackupFolderProperties_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnBrowseSrc;
        private System.Windows.Forms.Button m_btnBrowseDst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_txtDstFolder;
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
        private System.Windows.Forms.Label label6;
    }
}