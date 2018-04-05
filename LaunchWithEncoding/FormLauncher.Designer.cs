namespace LaunchWithEncoding
{
    partial class FormLauncher
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
            this.m_txtProgram = new System.Windows.Forms.TextBox();
            this.m_btnBrowse = new System.Windows.Forms.Button();
            this.m_cmbEncoding = new System.Windows.Forms.ComboBox();
            this.m_lblEncoding = new System.Windows.Forms.Label();
            this.m_btnLaunch = new System.Windows.Forms.Button();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // m_txtProgram
            // 
            this.m_txtProgram.Location = new System.Drawing.Point(13, 13);
            this.m_txtProgram.Name = "m_txtProgram";
            this.m_txtProgram.Size = new System.Drawing.Size(270, 20);
            this.m_txtProgram.TabIndex = 0;
            // 
            // m_btnBrowse
            // 
            this.m_btnBrowse.Location = new System.Drawing.Point(310, 11);
            this.m_btnBrowse.Name = "m_btnBrowse";
            this.m_btnBrowse.Size = new System.Drawing.Size(31, 23);
            this.m_btnBrowse.TabIndex = 1;
            this.m_btnBrowse.Text = "...";
            this.m_btnBrowse.UseVisualStyleBackColor = true;
            this.m_btnBrowse.Click += new System.EventHandler(this.m_btnBrowse_Click);
            // 
            // m_cmbEncoding
            // 
            this.m_cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbEncoding.FormattingEnabled = true;
            this.m_cmbEncoding.Location = new System.Drawing.Point(71, 48);
            this.m_cmbEncoding.Name = "m_cmbEncoding";
            this.m_cmbEncoding.Size = new System.Drawing.Size(178, 21);
            this.m_cmbEncoding.TabIndex = 2;
            // 
            // m_lblEncoding
            // 
            this.m_lblEncoding.AutoSize = true;
            this.m_lblEncoding.Location = new System.Drawing.Point(13, 51);
            this.m_lblEncoding.Name = "m_lblEncoding";
            this.m_lblEncoding.Size = new System.Drawing.Size(52, 13);
            this.m_lblEncoding.TabIndex = 3;
            this.m_lblEncoding.Text = "Encoding";
            // 
            // m_btnLaunch
            // 
            this.m_btnLaunch.Location = new System.Drawing.Point(266, 48);
            this.m_btnLaunch.Name = "m_btnLaunch";
            this.m_btnLaunch.Size = new System.Drawing.Size(75, 23);
            this.m_btnLaunch.TabIndex = 4;
            this.m_btnLaunch.Text = "Launch...";
            this.m_btnLaunch.UseVisualStyleBackColor = true;
            this.m_btnLaunch.Click += new System.EventHandler(this.m_btnLaunch_Click);
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.FileName = "*.exe";
            this.m_openFileDialog.Filter = "Executable files(*.exe)|*.exe";
            // 
            // FormLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 90);
            this.Controls.Add(this.m_btnLaunch);
            this.Controls.Add(this.m_lblEncoding);
            this.Controls.Add(this.m_cmbEncoding);
            this.Controls.Add(this.m_btnBrowse);
            this.Controls.Add(this.m_txtProgram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormLauncher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Launch";
            this.Load += new System.EventHandler(this.FormLauncher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtProgram;
        private System.Windows.Forms.Button m_btnBrowse;
        private System.Windows.Forms.ComboBox m_cmbEncoding;
        private System.Windows.Forms.Label m_lblEncoding;
        private System.Windows.Forms.Button m_btnLaunch;
        private System.Windows.Forms.OpenFileDialog m_openFileDialog;
    }
}

