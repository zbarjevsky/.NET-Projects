namespace VhdApiExample {
    partial class VHD_CreateForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VHD_CreateForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.buttonFileBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_numDiskSize = new System.Windows.Forms.NumericUpDown();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.m_progrCreation = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_cnkPreallocate = new System.Windows.Forms.CheckBox();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.m_progrCompletion = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.m_numDiskSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File:";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtFileName.Location = new System.Drawing.Point(61, 8);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(376, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // buttonFileBrowse
            // 
            this.buttonFileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFileBrowse.Location = new System.Drawing.Point(441, 6);
            this.buttonFileBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFileBrowse.Name = "buttonFileBrowse";
            this.buttonFileBrowse.Size = new System.Drawing.Size(25, 21);
            this.buttonFileBrowse.TabIndex = 2;
            this.buttonFileBrowse.Text = "...";
            this.buttonFileBrowse.UseVisualStyleBackColor = true;
            this.buttonFileBrowse.Click += new System.EventHandler(this.buttonFileBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Size:";
            // 
            // m_numDiskSize
            // 
            this.m_numDiskSize.Location = new System.Drawing.Point(61, 35);
            this.m_numDiskSize.Margin = new System.Windows.Forms.Padding(2);
            this.m_numDiskSize.Maximum = new decimal(new int[] {
            2040,
            0,
            0,
            0});
            this.m_numDiskSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_numDiskSize.Name = "m_numDiskSize";
            this.m_numDiskSize.Size = new System.Drawing.Size(103, 20);
            this.m_numDiskSize.TabIndex = 4;
            this.m_numDiskSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numDiskSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // buttonCreate
            // 
            this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreate.Location = new System.Drawing.Point(382, 33);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(2, 12, 2, 2);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(84, 23);
            this.buttonCreate.TabIndex = 7;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // m_progrCreation
            // 
            this.m_progrCreation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progrCreation.Location = new System.Drawing.Point(11, 68);
            this.m_progrCreation.Margin = new System.Windows.Forms.Padding(2);
            this.m_progrCreation.Name = "m_progrCreation";
            this.m_progrCreation.Size = new System.Drawing.Size(455, 10);
            this.m_progrCreation.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_progrCreation.TabIndex = 8;
            this.m_progrCreation.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "GB";
            // 
            // saveDialog
            // 
            this.saveDialog.Filter = "Virtual disks|*.vhd|All files|*.*";
            // 
            // m_cnkPreallocate
            // 
            this.m_cnkPreallocate.AutoSize = true;
            this.m_cnkPreallocate.Checked = true;
            this.m_cnkPreallocate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_cnkPreallocate.Location = new System.Drawing.Point(217, 38);
            this.m_cnkPreallocate.Name = "m_cnkPreallocate";
            this.m_cnkPreallocate.Size = new System.Drawing.Size(121, 17);
            this.m_cnkPreallocate.TabIndex = 6;
            this.m_cnkPreallocate.Text = "Preallocate this Size";
            this.m_cnkPreallocate.UseVisualStyleBackColor = true;
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblStatus.Location = new System.Drawing.Point(12, 100);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(454, 27);
            this.m_lblStatus.TabIndex = 10;
            this.m_lblStatus.Text = "...";
            this.m_lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_progrCompletion
            // 
            this.m_progrCompletion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progrCompletion.Location = new System.Drawing.Point(11, 82);
            this.m_progrCompletion.Margin = new System.Windows.Forms.Padding(2);
            this.m_progrCompletion.Name = "m_progrCompletion";
            this.m_progrCompletion.Size = new System.Drawing.Size(455, 10);
            this.m_progrCompletion.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_progrCompletion.TabIndex = 9;
            this.m_progrCompletion.Visible = false;
            // 
            // VHD_CreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 136);
            this.Controls.Add(this.m_progrCompletion);
            this.Controls.Add(this.m_lblStatus);
            this.Controls.Add(this.m_cnkPreallocate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_progrCreation);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.m_numDiskSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonFileBrowse);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 136);
            this.Name = "VHD_CreateForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create VHD";
            this.Load += new System.EventHandler(this.VHD_CreateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_numDiskSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button buttonFileBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown m_numDiskSize;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ProgressBar m_progrCreation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.CheckBox m_cnkPreallocate;
        private System.Windows.Forms.Label m_lblStatus;
        private System.Windows.Forms.ProgressBar m_progrCompletion;
    }
}