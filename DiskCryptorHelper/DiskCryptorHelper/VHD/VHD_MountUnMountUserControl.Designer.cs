namespace DiskCryptorHelper.VHD
{
    partial class VHD_MountUnMountUserControl
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
            this.m_grpVHD = new System.Windows.Forms.GroupBox();
            this.m_cmbAvailableDriveLetters = new System.Windows.Forms.ComboBox();
            this.m_lblVHD_File = new System.Windows.Forms.Label();
            this.m_btnAttachVHD = new System.Windows.Forms.Button();
            this.m_cmbVHD_FileName = new System.Windows.Forms.ComboBox();
            this.m_btnOpenVHD = new System.Windows.Forms.Button();
            this.m_chkPermanent = new System.Windows.Forms.CheckBox();
            this.m_btnDetach = new System.Windows.Forms.Button();
            this.m_grpVHD.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_grpVHD
            // 
            this.m_grpVHD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpVHD.Controls.Add(this.m_cmbAvailableDriveLetters);
            this.m_grpVHD.Controls.Add(this.m_lblVHD_File);
            this.m_grpVHD.Controls.Add(this.m_btnAttachVHD);
            this.m_grpVHD.Controls.Add(this.m_cmbVHD_FileName);
            this.m_grpVHD.Controls.Add(this.m_btnOpenVHD);
            this.m_grpVHD.Controls.Add(this.m_chkPermanent);
            this.m_grpVHD.Controls.Add(this.m_btnDetach);
            this.m_grpVHD.Location = new System.Drawing.Point(6, 6);
            this.m_grpVHD.Name = "m_grpVHD";
            this.m_grpVHD.Size = new System.Drawing.Size(541, 94);
            this.m_grpVHD.TabIndex = 6;
            this.m_grpVHD.TabStop = false;
            this.m_grpVHD.Text = "Virtual Hard Drive (VHD)";
            // 
            // m_cmbAvailableDriveLetters
            // 
            this.m_cmbAvailableDriveLetters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAvailableDriveLetters.FormattingEnabled = true;
            this.m_cmbAvailableDriveLetters.Location = new System.Drawing.Point(298, 54);
            this.m_cmbAvailableDriveLetters.Name = "m_cmbAvailableDriveLetters";
            this.m_cmbAvailableDriveLetters.Size = new System.Drawing.Size(40, 21);
            this.m_cmbAvailableDriveLetters.TabIndex = 6;
            this.m_cmbAvailableDriveLetters.SelectedIndexChanged += new System.EventHandler(this.m_cmbAvailableDriveLetters_SelectedIndexChanged);
            // 
            // m_lblVHD_File
            // 
            this.m_lblVHD_File.AutoSize = true;
            this.m_lblVHD_File.Location = new System.Drawing.Point(20, 32);
            this.m_lblVHD_File.Name = "m_lblVHD_File";
            this.m_lblVHD_File.Size = new System.Drawing.Size(55, 13);
            this.m_lblVHD_File.TabIndex = 0;
            this.m_lblVHD_File.Text = "VHD File: ";
            // 
            // m_btnAttachVHD
            // 
            this.m_btnAttachVHD.Location = new System.Drawing.Point(169, 53);
            this.m_btnAttachVHD.Name = "m_btnAttachVHD";
            this.m_btnAttachVHD.Size = new System.Drawing.Size(123, 23);
            this.m_btnAttachVHD.TabIndex = 4;
            this.m_btnAttachVHD.Text = "Attach && Mount";
            this.m_btnAttachVHD.UseVisualStyleBackColor = true;
            this.m_btnAttachVHD.Click += new System.EventHandler(this.m_btnAttachVHDandMount_Click);
            // 
            // m_cmbVHD_FileName
            // 
            this.m_cmbVHD_FileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbVHD_FileName.Location = new System.Drawing.Point(85, 26);
            this.m_cmbVHD_FileName.Name = "m_cmbVHD_FileName";
            this.m_cmbVHD_FileName.Size = new System.Drawing.Size(391, 21);
            this.m_cmbVHD_FileName.TabIndex = 1;
            // 
            // m_btnOpenVHD
            // 
            this.m_btnOpenVHD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOpenVHD.Location = new System.Drawing.Point(494, 25);
            this.m_btnOpenVHD.Name = "m_btnOpenVHD";
            this.m_btnOpenVHD.Size = new System.Drawing.Size(28, 23);
            this.m_btnOpenVHD.TabIndex = 2;
            this.m_btnOpenVHD.Text = "...";
            this.m_btnOpenVHD.UseVisualStyleBackColor = true;
            this.m_btnOpenVHD.Click += new System.EventHandler(this.m_btnOpenVHD_Click);
            // 
            // m_chkPermanent
            // 
            this.m_chkPermanent.Checked = true;
            this.m_chkPermanent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkPermanent.Location = new System.Drawing.Point(85, 54);
            this.m_chkPermanent.Name = "m_chkPermanent";
            this.m_chkPermanent.Size = new System.Drawing.Size(93, 23);
            this.m_chkPermanent.TabIndex = 3;
            this.m_chkPermanent.Text = "Permanent";
            this.m_chkPermanent.UseVisualStyleBackColor = true;
            // 
            // m_btnDetach
            // 
            this.m_btnDetach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDetach.Location = new System.Drawing.Point(357, 53);
            this.m_btnDetach.Name = "m_btnDetach";
            this.m_btnDetach.Size = new System.Drawing.Size(119, 23);
            this.m_btnDetach.TabIndex = 5;
            this.m_btnDetach.Text = "UnMount && Detach";
            this.m_btnDetach.UseVisualStyleBackColor = true;
            this.m_btnDetach.Click += new System.EventHandler(this.m_btnUnmountAndDetach_Click);
            // 
            // VHD_MountUnMountUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.m_grpVHD);
            this.MinimumSize = new System.Drawing.Size(550, 100);
            this.Name = "VHD_MountUnMountUserControl";
            this.Size = new System.Drawing.Size(554, 106);
            this.Load += new System.EventHandler(this.VHD_MountUnMountUserControl_Load);
            this.m_grpVHD.ResumeLayout(false);
            this.m_grpVHD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_grpVHD;
        private System.Windows.Forms.Label m_lblVHD_File;
        private System.Windows.Forms.Button m_btnAttachVHD;
        private System.Windows.Forms.ComboBox m_cmbVHD_FileName;
        private System.Windows.Forms.Button m_btnOpenVHD;
        private System.Windows.Forms.CheckBox m_chkPermanent;
        private System.Windows.Forms.Button m_btnDetach;
        private System.Windows.Forms.ComboBox m_cmbAvailableDriveLetters;
    }
}
