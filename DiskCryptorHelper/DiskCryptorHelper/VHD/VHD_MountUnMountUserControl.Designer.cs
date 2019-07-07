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
            this.components = new System.ComponentModel.Container();
            this.m_grpVHD = new System.Windows.Forms.GroupBox();
            this.m_cmbAvailableDriveLetters = new System.Windows.Forms.ComboBox();
            this.m_lblVHD_File = new System.Windows.Forms.Label();
            this.m_btnAttachVHD = new System.Windows.Forms.Button();
            this.m_cmbVHD_FileName = new System.Windows.Forms.ComboBox();
            this.m_btnOpenVHD = new System.Windows.Forms.Button();
            this.m_chkPermanent = new System.Windows.Forms.CheckBox();
            this.m_btnDetach = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_btnDetachAll = new System.Windows.Forms.Button();
            this.m_grpVHD.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_grpVHD
            // 
            this.m_grpVHD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpVHD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_grpVHD.Controls.Add(this.m_btnDetachAll);
            this.m_grpVHD.Controls.Add(this.m_cmbAvailableDriveLetters);
            this.m_grpVHD.Controls.Add(this.m_lblVHD_File);
            this.m_grpVHD.Controls.Add(this.m_btnAttachVHD);
            this.m_grpVHD.Controls.Add(this.m_cmbVHD_FileName);
            this.m_grpVHD.Controls.Add(this.m_btnOpenVHD);
            this.m_grpVHD.Controls.Add(this.m_chkPermanent);
            this.m_grpVHD.Controls.Add(this.m_btnDetach);
            this.m_grpVHD.Location = new System.Drawing.Point(8, 7);
            this.m_grpVHD.Margin = new System.Windows.Forms.Padding(4);
            this.m_grpVHD.Name = "m_grpVHD";
            this.m_grpVHD.Padding = new System.Windows.Forms.Padding(4);
            this.m_grpVHD.Size = new System.Drawing.Size(672, 106);
            this.m_grpVHD.TabIndex = 6;
            this.m_grpVHD.TabStop = false;
            this.m_grpVHD.Text = "Virtual Hard Drive (VHD)";
            // 
            // m_cmbAvailableDriveLetters
            // 
            this.m_cmbAvailableDriveLetters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAvailableDriveLetters.FormattingEnabled = true;
            this.m_cmbAvailableDriveLetters.Location = new System.Drawing.Point(143, 67);
            this.m_cmbAvailableDriveLetters.Margin = new System.Windows.Forms.Padding(4);
            this.m_cmbAvailableDriveLetters.Name = "m_cmbAvailableDriveLetters";
            this.m_cmbAvailableDriveLetters.Size = new System.Drawing.Size(52, 24);
            this.m_cmbAvailableDriveLetters.TabIndex = 6;
            this.m_cmbAvailableDriveLetters.SelectedIndexChanged += new System.EventHandler(this.m_cmbAvailableDriveLetters_SelectedIndexChanged);
            // 
            // m_lblVHD_File
            // 
            this.m_lblVHD_File.AutoSize = true;
            this.m_lblVHD_File.Location = new System.Drawing.Point(8, 35);
            this.m_lblVHD_File.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblVHD_File.Name = "m_lblVHD_File";
            this.m_lblVHD_File.Size = new System.Drawing.Size(71, 17);
            this.m_lblVHD_File.TabIndex = 0;
            this.m_lblVHD_File.Text = "VHD File: ";
            // 
            // m_btnAttachVHD
            // 
            this.m_btnAttachVHD.Location = new System.Drawing.Point(11, 64);
            this.m_btnAttachVHD.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnAttachVHD.Name = "m_btnAttachVHD";
            this.m_btnAttachVHD.Size = new System.Drawing.Size(125, 28);
            this.m_btnAttachVHD.TabIndex = 4;
            this.m_btnAttachVHD.Text = "Attach && Mount";
            this.toolTip1.SetToolTip(this.m_btnAttachVHD, "Attach VHD & Mount DiskCryptor Drive");
            this.m_btnAttachVHD.UseVisualStyleBackColor = true;
            this.m_btnAttachVHD.Click += new System.EventHandler(this.m_btnAttachVHDandMount_Click);
            // 
            // m_cmbVHD_FileName
            // 
            this.m_cmbVHD_FileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbVHD_FileName.Location = new System.Drawing.Point(91, 32);
            this.m_cmbVHD_FileName.Margin = new System.Windows.Forms.Padding(4);
            this.m_cmbVHD_FileName.Name = "m_cmbVHD_FileName";
            this.m_cmbVHD_FileName.Size = new System.Drawing.Size(538, 24);
            this.m_cmbVHD_FileName.TabIndex = 1;
            // 
            // m_btnOpenVHD
            // 
            this.m_btnOpenVHD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOpenVHD.Location = new System.Drawing.Point(637, 31);
            this.m_btnOpenVHD.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnOpenVHD.Name = "m_btnOpenVHD";
            this.m_btnOpenVHD.Size = new System.Drawing.Size(24, 22);
            this.m_btnOpenVHD.TabIndex = 2;
            this.m_btnOpenVHD.Text = "...";
            this.m_btnOpenVHD.UseVisualStyleBackColor = true;
            this.m_btnOpenVHD.Click += new System.EventHandler(this.m_btnOpenVHD_Click);
            // 
            // m_chkPermanent
            // 
            this.m_chkPermanent.AutoSize = true;
            this.m_chkPermanent.Checked = true;
            this.m_chkPermanent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkPermanent.Location = new System.Drawing.Point(205, 69);
            this.m_chkPermanent.Margin = new System.Windows.Forms.Padding(4);
            this.m_chkPermanent.Name = "m_chkPermanent";
            this.m_chkPermanent.Size = new System.Drawing.Size(99, 21);
            this.m_chkPermanent.TabIndex = 3;
            this.m_chkPermanent.Text = "Permanent";
            this.toolTip1.SetToolTip(this.m_chkPermanent, "Do not detach VHD on exit");
            this.m_chkPermanent.UseVisualStyleBackColor = true;
            // 
            // m_btnDetach
            // 
            this.m_btnDetach.Location = new System.Drawing.Point(312, 64);
            this.m_btnDetach.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnDetach.Name = "m_btnDetach";
            this.m_btnDetach.Size = new System.Drawing.Size(135, 28);
            this.m_btnDetach.TabIndex = 5;
            this.m_btnDetach.Text = "UnMount && Detach";
            this.m_btnDetach.UseVisualStyleBackColor = true;
            this.m_btnDetach.Click += new System.EventHandler(this.m_btnUnmountAndDetach_Click);
            // 
            // m_btnDetachAll
            // 
            this.m_btnDetachAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDetachAll.Location = new System.Drawing.Point(526, 64);
            this.m_btnDetachAll.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnDetachAll.Name = "m_btnDetachAll";
            this.m_btnDetachAll.Size = new System.Drawing.Size(135, 28);
            this.m_btnDetachAll.TabIndex = 7;
            this.m_btnDetachAll.Text = "Detach &All";
            this.m_btnDetachAll.UseVisualStyleBackColor = true;
            this.m_btnDetachAll.Click += new System.EventHandler(this.m_btnDetachAll_Click);
            // 
            // VHD_MountUnMountUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.m_grpVHD);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(500, 120);
            this.Name = "VHD_MountUnMountUserControl";
            this.Size = new System.Drawing.Size(690, 120);
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
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button m_btnDetachAll;
    }
}
