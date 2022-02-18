namespace RadexOneDemo
{
    partial class FormRadexOneConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRadexOneConfig));
            this.m_grpConfig = new System.Windows.Forms.GroupBox();
            this.m_numLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.m_chkVib = new System.Windows.Forms.CheckBox();
            this.m_chkSnd = new System.Windows.Forms.CheckBox();
            this.m_btnResetDose = new System.Windows.Forms.Button();
            this.m_lblSN = new System.Windows.Forms.Label();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_lblDose = new System.Windows.Forms.Label();
            this.m_grpConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // m_grpConfig
            // 
            this.m_grpConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpConfig.Controls.Add(this.m_lblDose);
            this.m_grpConfig.Controls.Add(this.m_numLimit);
            this.m_grpConfig.Controls.Add(this.label2);
            this.m_grpConfig.Controls.Add(this.m_chkVib);
            this.m_grpConfig.Controls.Add(this.m_chkSnd);
            this.m_grpConfig.Location = new System.Drawing.Point(17, 49);
            this.m_grpConfig.Name = "m_grpConfig";
            this.m_grpConfig.Size = new System.Drawing.Size(255, 149);
            this.m_grpConfig.TabIndex = 1;
            this.m_grpConfig.TabStop = false;
            this.m_grpConfig.Text = "Configuration";
            // 
            // m_numLimit
            // 
            this.m_numLimit.DecimalPlaces = 1;
            this.m_numLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numLimit.Location = new System.Drawing.Point(122, 108);
            this.m_numLimit.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.m_numLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numLimit.Name = "m_numLimit";
            this.m_numLimit.Size = new System.Drawing.Size(71, 20);
            this.m_numLimit.TabIndex = 3;
            this.m_numLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numLimit.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numLimit.ValueChanged += new System.EventHandler(this.m_numLimit_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Threshold (µSv/h)";
            // 
            // m_chkVib
            // 
            this.m_chkVib.AutoSize = true;
            this.m_chkVib.Location = new System.Drawing.Point(15, 83);
            this.m_chkVib.Name = "m_chkVib";
            this.m_chkVib.Size = new System.Drawing.Size(67, 17);
            this.m_chkVib.TabIndex = 1;
            this.m_chkVib.Text = "Vibration";
            this.m_chkVib.UseVisualStyleBackColor = true;
            this.m_chkVib.CheckedChanged += new System.EventHandler(this.m_numLimit_ValueChanged);
            // 
            // m_chkSnd
            // 
            this.m_chkSnd.AutoSize = true;
            this.m_chkSnd.Location = new System.Drawing.Point(15, 54);
            this.m_chkSnd.Name = "m_chkSnd";
            this.m_chkSnd.Size = new System.Drawing.Size(57, 17);
            this.m_chkSnd.TabIndex = 0;
            this.m_chkSnd.Text = "Sound";
            this.m_chkSnd.UseVisualStyleBackColor = true;
            this.m_chkSnd.CheckedChanged += new System.EventHandler(this.m_numLimit_ValueChanged);
            // 
            // m_btnResetDose
            // 
            this.m_btnResetDose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnResetDose.Image = ((System.Drawing.Image)(resources.GetObject("m_btnResetDose.Image")));
            this.m_btnResetDose.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.m_btnResetDose.Location = new System.Drawing.Point(293, 73);
            this.m_btnResetDose.Name = "m_btnResetDose";
            this.m_btnResetDose.Size = new System.Drawing.Size(106, 23);
            this.m_btnResetDose.TabIndex = 2;
            this.m_btnResetDose.Text = "Reset Dose";
            this.m_btnResetDose.UseVisualStyleBackColor = true;
            this.m_btnResetDose.Click += new System.EventHandler(this.m_btnResetDose_Click);
            // 
            // m_lblSN
            // 
            this.m_lblSN.AutoSize = true;
            this.m_lblSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.m_lblSN.Location = new System.Drawing.Point(12, 9);
            this.m_lblSN.Name = "m_lblSN";
            this.m_lblSN.Size = new System.Drawing.Size(83, 29);
            this.m_lblSN.TabIndex = 0;
            this.m_lblSN.Text = "S/N: ?";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Enabled = false;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.m_btnOk.Location = new System.Drawing.Point(293, 141);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(106, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "&Save";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.m_btnCancel.Location = new System.Drawing.Point(293, 170);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(106, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "&Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_lblDose
            // 
            this.m_lblDose.AutoSize = true;
            this.m_lblDose.Location = new System.Drawing.Point(13, 29);
            this.m_lblDose.Name = "m_lblDose";
            this.m_lblDose.Size = new System.Drawing.Size(71, 13);
            this.m_lblDose.TabIndex = 4;
            this.m_lblDose.Text = "Dose (µSv/h)";
            // 
            // FormRadexOneConfig
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(411, 212);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnResetDose);
            this.Controls.Add(this.m_grpConfig);
            this.Controls.Add(this.m_lblSN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(600, 300);
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "FormRadexOneConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RadexOne Configuration";
            this.Load += new System.EventHandler(this.FormRadexOneConfig_Load);
            this.m_grpConfig.ResumeLayout(false);
            this.m_grpConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox m_grpConfig;
        private System.Windows.Forms.NumericUpDown m_numLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox m_chkVib;
        private System.Windows.Forms.CheckBox m_chkSnd;
        private System.Windows.Forms.Button m_btnResetDose;
        private System.Windows.Forms.Label m_lblSN;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label m_lblDose;
    }
}