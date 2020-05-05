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
            this.m_txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnBrowseSrc = new System.Windows.Forms.Button();
            this.m_btnBrowseDst = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtDstFolder = new System.Windows.Forms.TextBox();
            this.m_chkImportant = new System.Windows.Forms.CheckBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_txtFileType = new System.Windows.Forms.TextBox();
            this.m_txtExcludeType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_txtSource
            // 
            this.m_txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSource.Location = new System.Drawing.Point(68, 14);
            this.m_txtSource.Name = "m_txtSource";
            this.m_txtSource.Size = new System.Drawing.Size(363, 20);
            this.m_txtSource.TabIndex = 0;
            this.m_txtSource.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source:";
            // 
            // m_btnBrowseSrc
            // 
            this.m_btnBrowseSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseSrc.Location = new System.Drawing.Point(438, 12);
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
            this.m_btnBrowseDst.Location = new System.Drawing.Point(438, 39);
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
            this.label2.TabIndex = 4;
            this.label2.Text = "Backup to:";
            // 
            // m_txtDstFolder
            // 
            this.m_txtDstFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDstFolder.Location = new System.Drawing.Point(68, 41);
            this.m_txtDstFolder.Name = "m_txtDstFolder";
            this.m_txtDstFolder.Size = new System.Drawing.Size(363, 20);
            this.m_txtDstFolder.TabIndex = 3;
            this.m_txtDstFolder.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // m_chkImportant
            // 
            this.m_chkImportant.AutoSize = true;
            this.m_chkImportant.Location = new System.Drawing.Point(71, 132);
            this.m_chkImportant.Name = "m_chkImportant";
            this.m_chkImportant.Size = new System.Drawing.Size(70, 17);
            this.m_chkImportant.TabIndex = 6;
            this.m_chkImportant.Text = "Important";
            this.m_chkImportant.UseVisualStyleBackColor = true;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Location = new System.Drawing.Point(321, 166);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 7;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(402, 166);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 8;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_txtFileType
            // 
            this.m_txtFileType.Location = new System.Drawing.Point(194, 73);
            this.m_txtFileType.Name = "m_txtFileType";
            this.m_txtFileType.Size = new System.Drawing.Size(226, 20);
            this.m_txtFileType.TabIndex = 9;
            this.m_txtFileType.Text = "*.*";
            // 
            // m_txtExcludeType
            // 
            this.m_txtExcludeType.Location = new System.Drawing.Point(194, 99);
            this.m_txtExcludeType.Name = "m_txtExcludeType";
            this.m_txtExcludeType.Size = new System.Drawing.Size(226, 20);
            this.m_txtExcludeType.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Include Files Types";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Exclude File Types";
            // 
            // FormBackupFolderProperties
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 201);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_txtExcludeType);
            this.Controls.Add(this.m_txtFileType);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_chkImportant);
            this.Controls.Add(this.m_btnBrowseDst);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txtDstFolder);
            this.Controls.Add(this.m_btnBrowseSrc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_txtSource);
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
        private System.Windows.Forms.CheckBox m_chkImportant;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.TextBox m_txtFileType;
        private System.Windows.Forms.TextBox m_txtExcludeType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}