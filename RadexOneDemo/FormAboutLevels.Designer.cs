namespace RadexOneDemo
{
    partial class FormAboutLevels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAboutLevels));
            this.m_txtAbouLevels = new System.Windows.Forms.RichTextBox();
            this.m_tabAbout = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radiationConverterControl1 = new RadexOneDemo.RadiationConverterControl();
            this.m_tabAbout.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_txtAbouLevels
            // 
            this.m_txtAbouLevels.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtAbouLevels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtAbouLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtAbouLevels.Location = new System.Drawing.Point(3, 3);
            this.m_txtAbouLevels.Margin = new System.Windows.Forms.Padding(30);
            this.m_txtAbouLevels.Name = "m_txtAbouLevels";
            this.m_txtAbouLevels.ReadOnly = true;
            this.m_txtAbouLevels.Size = new System.Drawing.Size(548, 626);
            this.m_txtAbouLevels.TabIndex = 1;
            this.m_txtAbouLevels.Text = "";
            this.m_txtAbouLevels.WordWrap = false;
            // 
            // m_tabAbout
            // 
            this.m_tabAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabAbout.Controls.Add(this.tabPage1);
            this.m_tabAbout.Controls.Add(this.tabPage2);
            this.m_tabAbout.Location = new System.Drawing.Point(2, 6);
            this.m_tabAbout.Name = "m_tabAbout";
            this.m_tabAbout.SelectedIndex = 0;
            this.m_tabAbout.Size = new System.Drawing.Size(562, 658);
            this.m_tabAbout.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_txtAbouLevels);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(554, 632);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "About Levels";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(554, 632);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pain Scale";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(548, 626);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // radiationConverterControl1
            // 
            this.radiationConverterControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radiationConverterControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.radiationConverterControl1.Location = new System.Drawing.Point(15, 682);
            this.radiationConverterControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radiationConverterControl1.Name = "radiationConverterControl1";
            this.radiationConverterControl1.Size = new System.Drawing.Size(532, 59);
            this.radiationConverterControl1.TabIndex = 2;
            this.radiationConverterControl1.ValueFrom = 0.1D;
            // 
            // FormAboutLevels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(564, 762);
            this.Controls.Add(this.m_tabAbout);
            this.Controls.Add(this.radiationConverterControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1600, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 600);
            this.Name = "FormAboutLevels";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Radiation Levels";
            this.Load += new System.EventHandler(this.FormAboutLevels_Load);
            this.m_tabAbout.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox m_txtAbouLevels;
        private RadiationConverterControl radiationConverterControl1;
        private System.Windows.Forms.TabControl m_tabAbout;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}