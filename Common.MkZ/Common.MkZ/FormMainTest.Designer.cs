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
            MZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet1 = new MZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            MZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet2 = new MZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            MZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet3 = new MZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainTest));
            this.m_btnTestEdit = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_cmbListViewType = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_btnColorSlider = new System.Windows.Forms.Button();
            this.m_btnTestWPFMessageBoxWPF = new System.Windows.Forms.Button();
            this.m_btnTestWPFMessageBox = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.trackBar1 = new ColorSlider.ColorSlider();
            this.colorBarsProgressBar3 = new MZ.WinForms.ColorBarsProgressBar();
            this.colorBarsProgressBar2 = new MZ.WinForms.ColorBarsProgressBar();
            this.colorBarsProgressBar1 = new MZ.WinForms.ColorBarsProgressBar();
            this.foldersTreeUserControl1 = new MZ.WinForms.FoldersTreeUserControl();
            this.fileExplorerUserControl1 = new MZ.WinForms.FileExplorerUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnTestEdit
            // 
            this.m_btnTestEdit.Location = new System.Drawing.Point(160, 29);
            this.m_btnTestEdit.Name = "m_btnTestEdit";
            this.m_btnTestEdit.Size = new System.Drawing.Size(144, 68);
            this.m_btnTestEdit.TabIndex = 1;
            this.m_btnTestEdit.Text = "Test In-Place-Edit Box";
            this.m_btnTestEdit.UseVisualStyleBackColor = true;
            this.m_btnTestEdit.Click += new System.EventHandler(this.m_btnTestEdit_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(728, 389);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.foldersTreeUserControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileExplorerUserControl1);
            this.splitContainer1.Size = new System.Drawing.Size(736, 422);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.TabIndex = 6;
            // 
            // m_cmbListViewType
            // 
            this.m_cmbListViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbListViewType.FormattingEnabled = true;
            this.m_cmbListViewType.Location = new System.Drawing.Point(6, 6);
            this.m_cmbListViewType.Name = "m_cmbListViewType";
            this.m_cmbListViewType.Size = new System.Drawing.Size(121, 21);
            this.m_cmbListViewType.TabIndex = 7;
            this.m_cmbListViewType.SelectedIndexChanged += new System.EventHandler(this.m_cmbListViewType_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(750, 454);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_btnColorSlider);
            this.tabPage3.Controls.Add(this.m_btnTestWPFMessageBoxWPF);
            this.tabPage3.Controls.Add(this.m_btnTestWPFMessageBox);
            this.tabPage3.Controls.Add(this.m_btnTestEdit);
            this.tabPage3.Controls.Add(this.trackBar1);
            this.tabPage3.Controls.Add(this.colorBarsProgressBar3);
            this.tabPage3.Controls.Add(this.colorBarsProgressBar2);
            this.tabPage3.Controls.Add(this.colorBarsProgressBar1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(742, 428);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Common Controls";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // m_btnColorSlider
            // 
            this.m_btnColorSlider.Location = new System.Drawing.Point(333, 29);
            this.m_btnColorSlider.Name = "m_btnColorSlider";
            this.m_btnColorSlider.Size = new System.Drawing.Size(144, 68);
            this.m_btnColorSlider.TabIndex = 13;
            this.m_btnColorSlider.Text = "Color Slider Demo";
            this.m_btnColorSlider.UseVisualStyleBackColor = true;
            this.m_btnColorSlider.Click += new System.EventHandler(this.m_btnColorSlider_Click);
            // 
            // m_btnTestWPFMessageBoxWPF
            // 
            this.m_btnTestWPFMessageBoxWPF.Location = new System.Drawing.Point(160, 235);
            this.m_btnTestWPFMessageBoxWPF.Name = "m_btnTestWPFMessageBoxWPF";
            this.m_btnTestWPFMessageBoxWPF.Size = new System.Drawing.Size(144, 68);
            this.m_btnTestWPFMessageBoxWPF.TabIndex = 12;
            this.m_btnTestWPFMessageBoxWPF.Text = "Test WPFMessageBox(WPF)";
            this.m_btnTestWPFMessageBoxWPF.UseVisualStyleBackColor = true;
            this.m_btnTestWPFMessageBoxWPF.Click += new System.EventHandler(this.m_btnTestWPFMessageBoxWPF_Click);
            // 
            // m_btnTestWPFMessageBox
            // 
            this.m_btnTestWPFMessageBox.Location = new System.Drawing.Point(160, 146);
            this.m_btnTestWPFMessageBox.Name = "m_btnTestWPFMessageBox";
            this.m_btnTestWPFMessageBox.Size = new System.Drawing.Size(144, 68);
            this.m_btnTestWPFMessageBox.TabIndex = 11;
            this.m_btnTestWPFMessageBox.Text = "Test WPFMessageBox (WinForms)";
            this.m_btnTestWPFMessageBox.UseVisualStyleBackColor = true;
            this.m_btnTestWPFMessageBox.Click += new System.EventHandler(this.m_btnTestWPFMessageBox_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(742, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Explorer Controls";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.m_cmbListViewType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(742, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Shell32 icons";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBar1.BarPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(94)))), ((int)(((byte)(110)))));
            this.trackBar1.BarPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(60)))), ((int)(((byte)(74)))));
            this.trackBar1.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.trackBar1.DrawSemitransparentThumb = false;
            this.trackBar1.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
            this.trackBar1.ElapsedPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(130)))), ((int)(((byte)(208)))));
            this.trackBar1.ElapsedPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(140)))), ((int)(((byte)(180)))));
            this.trackBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.trackBar1.LargeChange = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.trackBar1.Location = new System.Drawing.Point(80, 29);
            this.trackBar1.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.trackBar1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.ScaleDivisions = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.trackBar1.ScaleSubDivisions = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.trackBar1.ShowDivisionsText = true;
            this.trackBar1.ShowSmallScale = false;
            this.trackBar1.Size = new System.Drawing.Size(74, 369);
            this.trackBar1.SmallChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.trackBar1.TabIndex = 8;
            this.trackBar1.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
            this.trackBar1.ThumbPenColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
            this.trackBar1.ThumbRoundRectSize = new System.Drawing.Size(4, 4);
            this.trackBar1.ThumbSize = new System.Drawing.Size(24, 10);
            this.trackBar1.TickAdd = 0F;
            this.trackBar1.TickColor = System.Drawing.Color.White;
            this.trackBar1.TickDivide = 0F;
            this.trackBar1.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.trackBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.trackBar1_Scroll);
            // 
            // colorBarsProgressBar3
            // 
            this.colorBarsProgressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            themeColorSet1.Part1_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet1.Part1_InactiveColor = System.Drawing.Color.Honeydew;
            themeColorSet1.Part2_ActiveColor = System.Drawing.Color.Orange;
            themeColorSet1.Part2_InactiveColor = System.Drawing.Color.LightGoldenrodYellow;
            themeColorSet1.Part3_ActiveColor = System.Drawing.Color.Red;
            themeColorSet1.Part3_InactiveColor = System.Drawing.Color.MistyRose;
            themeColorSet1.Theme = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Speaker;
            themeColorSet1.Threshold1 = 60;
            themeColorSet1.Threshold2 = 85;
            this.colorBarsProgressBar3.ColorTheme = themeColorSet1;
            this.colorBarsProgressBar3.Location = new System.Drawing.Point(26, 29);
            this.colorBarsProgressBar3.Name = "colorBarsProgressBar3";
            this.colorBarsProgressBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar3.Size = new System.Drawing.Size(21, 369);
            this.colorBarsProgressBar3.TabIndex = 10;
            this.colorBarsProgressBar3.TabStop = false;
            this.colorBarsProgressBar3.Value = 60;
            // 
            // colorBarsProgressBar2
            // 
            this.colorBarsProgressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            themeColorSet2.Part1_ActiveColor = System.Drawing.Color.RoyalBlue;
            themeColorSet2.Part1_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet2.Part2_ActiveColor = System.Drawing.Color.AliceBlue;
            themeColorSet2.Part2_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet2.Part3_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet2.Part3_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet2.Theme = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Custom;
            themeColorSet2.Threshold1 = 100;
            themeColorSet2.Threshold2 = 100;
            this.colorBarsProgressBar2.ColorTheme = themeColorSet2;
            this.colorBarsProgressBar2.Location = new System.Drawing.Point(160, 385);
            this.colorBarsProgressBar2.Name = "colorBarsProgressBar2";
            this.colorBarsProgressBar2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.colorBarsProgressBar2.Size = new System.Drawing.Size(567, 27);
            this.colorBarsProgressBar2.TabIndex = 9;
            this.colorBarsProgressBar2.TabStop = false;
            this.colorBarsProgressBar2.Value = 60;
            // 
            // colorBarsProgressBar1
            // 
            this.colorBarsProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            themeColorSet3.Part1_ActiveColor = System.Drawing.Color.Orange;
            themeColorSet3.Part1_InactiveColor = System.Drawing.Color.LightGoldenrodYellow;
            themeColorSet3.Part2_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet3.Part2_InactiveColor = System.Drawing.Color.Honeydew;
            themeColorSet3.Part3_ActiveColor = System.Drawing.Color.Red;
            themeColorSet3.Part3_InactiveColor = System.Drawing.Color.MistyRose;
            themeColorSet3.Theme = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Microphone;
            themeColorSet3.Threshold1 = 10;
            themeColorSet3.Threshold2 = 85;
            this.colorBarsProgressBar1.ColorTheme = themeColorSet3;
            this.colorBarsProgressBar1.Location = new System.Drawing.Point(53, 29);
            this.colorBarsProgressBar1.Name = "colorBarsProgressBar1";
            this.colorBarsProgressBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar1.Size = new System.Drawing.Size(21, 369);
            this.colorBarsProgressBar1.TabIndex = 0;
            this.colorBarsProgressBar1.TabStop = false;
            this.colorBarsProgressBar1.Value = 60;
            // 
            // foldersTreeUserControl1
            // 
            this.foldersTreeUserControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.foldersTreeUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foldersTreeUserControl1.Location = new System.Drawing.Point(0, 0);
            this.foldersTreeUserControl1.Name = "foldersTreeUserControl1";
            this.foldersTreeUserControl1.Size = new System.Drawing.Size(191, 422);
            this.foldersTreeUserControl1.TabIndex = 2;
            // 
            // fileExplorerUserControl1
            // 
            this.fileExplorerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileExplorerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.fileExplorerUserControl1.Name = "fileExplorerUserControl1";
            this.fileExplorerUserControl1.Size = new System.Drawing.Size(541, 422);
            this.fileExplorerUserControl1.TabIndex = 3;
            // 
            // FormMainTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 454);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormMainTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Main Test";
            this.Load += new System.EventHandler(this.FormMainTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WinForms.ColorBarsProgressBar colorBarsProgressBar1;
        private System.Windows.Forms.Button m_btnTestEdit;
        private WinForms.FoldersTreeUserControl foldersTreeUserControl1;
        private WinForms.FileExplorerUserControl fileExplorerUserControl1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox m_cmbListViewType;
        private ColorSlider.ColorSlider trackBar1;
        private WinForms.ColorBarsProgressBar colorBarsProgressBar2;
        private WinForms.ColorBarsProgressBar colorBarsProgressBar3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button m_btnTestWPFMessageBox;
        private System.Windows.Forms.Button m_btnTestWPFMessageBoxWPF;
        private System.Windows.Forms.Button m_btnColorSlider;
    }
}