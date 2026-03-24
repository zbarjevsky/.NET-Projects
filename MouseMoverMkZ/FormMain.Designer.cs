namespace MouseMoverMkZ
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_lblTime = new System.Windows.Forms.Label();
            this.m_chkEnableMouseMove = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_lblTime
            // 
            this.m_lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblTime.Font = new System.Drawing.Font("Lucida Fax", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTime.ForeColor = System.Drawing.Color.Green;
            this.m_lblTime.Location = new System.Drawing.Point(12, 87);
            this.m_lblTime.Name = "m_lblTime";
            this.m_lblTime.Size = new System.Drawing.Size(405, 96);
            this.m_lblTime.TabIndex = 1;
            this.m_lblTime.Text = "00:00:00";
            this.m_lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_chkEnableMouseMove
            // 
            this.m_chkEnableMouseMove.AutoSize = true;
            this.m_chkEnableMouseMove.Checked = true;
            this.m_chkEnableMouseMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkEnableMouseMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_chkEnableMouseMove.Location = new System.Drawing.Point(29, 33);
            this.m_chkEnableMouseMove.Name = "m_chkEnableMouseMove";
            this.m_chkEnableMouseMove.Size = new System.Drawing.Size(375, 29);
            this.m_chkEnableMouseMove.TabIndex = 2;
            this.m_chkEnableMouseMove.Text = "Move mouse after 5 min of inactivity";
            this.m_chkEnableMouseMove.UseVisualStyleBackColor = true;
            this.m_chkEnableMouseMove.CheckedChanged += new System.EventHandler(this.m_chkEnableMouseMove_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 192);
            this.Controls.Add(this.m_chkEnableMouseMove);
            this.Controls.Add(this.m_lblTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mouse Mover";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblTime;
        private System.Windows.Forms.CheckBox m_chkEnableMouseMove;
    }
}

