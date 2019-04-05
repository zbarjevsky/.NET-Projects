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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAboutLevels));
            this.m_txtAbouLevels = new System.Windows.Forms.RichTextBox();
            this.m_tabAbout = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_webBrowser = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnlRadonLevelMap = new System.Windows.Forms.LinkLabel();
            this.m_lnkRadiationMap = new System.Windows.Forms.LinkLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radiationConverterControl1 = new RadexOneDemo.RadiationConverterControl();
            this.m_ctxMenu_link = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tabAbout.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.m_ctxMenu_link.SuspendLayout();
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
            this.m_tabAbout.Controls.Add(this.tabPage3);
            this.m_tabAbout.Controls.Add(this.tabPage2);
            this.m_tabAbout.Location = new System.Drawing.Point(2, 6);
            this.m_tabAbout.Name = "m_tabAbout";
            this.m_tabAbout.SelectedIndex = 0;
            this.m_tabAbout.Size = new System.Drawing.Size(562, 658);
            this.m_tabAbout.TabIndex = 3;
            this.m_tabAbout.SelectedIndexChanged += new System.EventHandler(this.m_tabAbout_SelectedIndexChanged);
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
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(554, 632);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Radiation Levels";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.m_webBrowser);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(554, 593);
            this.panel2.TabIndex = 4;
            // 
            // m_webBrowser
            // 
            this.m_webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_webBrowser.Location = new System.Drawing.Point(0, 0);
            this.m_webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.m_webBrowser.Name = "m_webBrowser";
            this.m_webBrowser.ScriptErrorsSuppressed = true;
            this.m_webBrowser.Size = new System.Drawing.Size(550, 589);
            this.m_webBrowser.TabIndex = 0;
            this.m_webBrowser.Url = new System.Uri("https://www.epa.gov/radnet/near-real-time-and-laboratory-data-state", System.UriKind.Absolute);
            this.m_webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.m_webBrowser_DocumentCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lnlRadonLevelMap);
            this.panel1.Controls.Add(this.m_lnkRadiationMap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 39);
            this.panel1.TabIndex = 3;
            // 
            // m_lnlRadonLevelMap
            // 
            this.m_lnlRadonLevelMap.AutoSize = true;
            this.m_lnlRadonLevelMap.ContextMenuStrip = this.m_ctxMenu_link;
            this.m_lnlRadonLevelMap.Location = new System.Drawing.Point(260, 11);
            this.m_lnlRadonLevelMap.Name = "m_lnlRadonLevelMap";
            this.m_lnlRadonLevelMap.Size = new System.Drawing.Size(123, 13);
            this.m_lnlRadonLevelMap.TabIndex = 1;
            this.m_lnlRadonLevelMap.TabStop = true;
            this.m_lnlRadonLevelMap.Text = "Radon Level Map (USA)";
            this.m_lnlRadonLevelMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnlRadonLevelMap_LinkClicked);
            // 
            // m_lnkRadiationMap
            // 
            this.m_lnkRadiationMap.AutoSize = true;
            this.m_lnkRadiationMap.ContextMenuStrip = this.m_ctxMenu_link;
            this.m_lnkRadiationMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lnkRadiationMap.Location = new System.Drawing.Point(6, 11);
            this.m_lnkRadiationMap.Name = "m_lnkRadiationMap";
            this.m_lnkRadiationMap.Size = new System.Drawing.Size(163, 13);
            this.m_lnkRadiationMap.TabIndex = 2;
            this.m_lnkRadiationMap.TabStop = true;
            this.m_lnkRadiationMap.Text = "Radiation Map(Interactive - USA)";
            this.m_lnkRadiationMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkRadiationMap_LinkClicked);
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
            // m_ctxMenu_link
            // 
            this.m_ctxMenu_link.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuCopy,
            this.m_mnuOpen});
            this.m_ctxMenu_link.Name = "m_ctxMenu_link";
            this.m_ctxMenu_link.Size = new System.Drawing.Size(173, 48);
            // 
            // m_mnuCopy
            // 
            this.m_mnuCopy.Name = "m_mnuCopy";
            this.m_mnuCopy.Size = new System.Drawing.Size(172, 22);
            this.m_mnuCopy.Text = "&Copy Link Address";
            this.m_mnuCopy.Click += new System.EventHandler(this.m_mnuCopy_Click);
            // 
            // m_mnuOpen
            // 
            this.m_mnuOpen.Name = "m_mnuOpen";
            this.m_mnuOpen.Size = new System.Drawing.Size(172, 22);
            this.m_mnuOpen.Text = "&Open in Browser...";
            this.m_mnuOpen.Click += new System.EventHandler(this.m_mnuOpen_Click);
            // 
            // FormAboutLevels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
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
            this.Text = "About Radiation";
            this.Load += new System.EventHandler(this.FormAboutLevels_Load);
            this.m_tabAbout.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.m_ctxMenu_link.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox m_txtAbouLevels;
        private RadiationConverterControl radiationConverterControl1;
        private System.Windows.Forms.TabControl m_tabAbout;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.WebBrowser m_webBrowser;
        private System.Windows.Forms.LinkLabel m_lnlRadonLevelMap;
        private System.Windows.Forms.LinkLabel m_lnkRadiationMap;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip m_ctxMenu_link;
        private System.Windows.Forms.ToolStripMenuItem m_mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem m_mnuOpen;
    }
}