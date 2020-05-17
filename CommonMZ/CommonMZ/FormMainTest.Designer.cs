namespace MZ
{
    partial class FormMainTest
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
            this.colorBarsProgressBar1 = new MZ.Controls.ColorBarsProgressBar();
            this.m_btnTestEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.colorBarsProgressBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // colorBarsProgressBar1
            // 
            this.colorBarsProgressBar1.ActiveColor = System.Drawing.Color.LimeGreen;
            this.colorBarsProgressBar1.ActiveColorTheme = MZ.Controls.ColorBarsProgressBar.ActiveColorsTheme.Multicolor;
            this.colorBarsProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.colorBarsProgressBar1.InactiveColorTheme = MZ.Controls.ColorBarsProgressBar.InactiveColorsTheme.Pale;
            this.colorBarsProgressBar1.Location = new System.Drawing.Point(26, 34);
            this.colorBarsProgressBar1.Maximum = 100;
            this.colorBarsProgressBar1.Minimum = 0;
            this.colorBarsProgressBar1.Name = "colorBarsProgressBar1";
            this.colorBarsProgressBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar1.Size = new System.Drawing.Size(21, 353);
            this.colorBarsProgressBar1.TabIndex = 0;
            this.colorBarsProgressBar1.TabStop = false;
            this.colorBarsProgressBar1.Value = 65;
            // 
            // m_btnTestEdit
            // 
            this.m_btnTestEdit.Location = new System.Drawing.Point(99, 13);
            this.m_btnTestEdit.Name = "m_btnTestEdit";
            this.m_btnTestEdit.Size = new System.Drawing.Size(144, 116);
            this.m_btnTestEdit.TabIndex = 1;
            this.m_btnTestEdit.Text = "Test In-Place-Edit Box";
            this.m_btnTestEdit.UseVisualStyleBackColor = true;
            this.m_btnTestEdit.Click += new System.EventHandler(this.m_btnTestEdit_Click);
            // 
            // FormMainTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_btnTestEdit);
            this.Controls.Add(this.colorBarsProgressBar1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormMainTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Main Test";
            this.Load += new System.EventHandler(this.FormMainTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.colorBarsProgressBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ColorBarsProgressBar colorBarsProgressBar1;
        private System.Windows.Forms.Button m_btnTestEdit;
    }
}