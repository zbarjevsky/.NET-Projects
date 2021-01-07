namespace MkZ
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
            MkZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet1 = new MkZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            MkZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet2 = new MkZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            MkZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet3 = new MkZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainTest));
            this.m_btnTestEdit = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.foldersTreeUserControl1 = new MkZ.WinForms.FoldersTreeUserControl();
            this.fileExplorerUserControl1 = new MkZ.WinForms.FileExplorerUserControl();
            this.m_cmbListViewType = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_btnEjectDrive = new System.Windows.Forms.Button();
            this.chartProgressUserControl1 = new WindowsFormsApp1.ChartProgressUserControl();
            this.m_chkEnable = new System.Windows.Forms.CheckBox();
            this.m_btnGradientWpfProgress = new System.Windows.Forms.Button();
            this.m_btnColorSlider = new System.Windows.Forms.Button();
            this.m_btnTestWPFMessageBoxWPF = new System.Windows.Forms.Button();
            this.m_btnTestWPFMessageBox = new System.Windows.Forms.Button();
            this.trackBar1 = new ColorSlider.ColorSlider();
            this.colorBarsProgressBar3 = new MkZ.WinForms.ColorBarsProgressBar();
            this.colorBarsProgressBar2 = new MkZ.WinForms.ColorBarsProgressBar();
            this.colorBarsProgressBar1 = new MkZ.WinForms.ColorBarsProgressBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_btnSaveIcons = new System.Windows.Forms.Button();
            this.m_btnOpenIconsFile = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.displayTopologyUserControl1 = new WpfApplication6.DisplayTopologyUserControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkNonStickMouse = new System.Windows.Forms.CheckBox();
            this.m_cmbDriveLetter = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnTestEdit
            // 
            this.m_btnTestEdit.Location = new System.Drawing.Point(160, 17);
            this.m_btnTestEdit.Name = "m_btnTestEdit";
            this.m_btnTestEdit.Size = new System.Drawing.Size(144, 45);
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
            this.fileExplorerUserControl1.CheckBoxes = false;
            this.fileExplorerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileExplorerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.fileExplorerUserControl1.Name = "fileExplorerUserControl1";
            this.fileExplorerUserControl1.Size = new System.Drawing.Size(541, 422);
            this.fileExplorerUserControl1.TabIndex = 3;
            // 
            // m_cmbListViewType
            // 
            this.m_cmbListViewType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbListViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbListViewType.FormattingEnabled = true;
            this.m_cmbListViewType.Location = new System.Drawing.Point(612, 6);
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
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(750, 454);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_cmbDriveLetter);
            this.tabPage3.Controls.Add(this.m_btnEjectDrive);
            this.tabPage3.Controls.Add(this.chartProgressUserControl1);
            this.tabPage3.Controls.Add(this.m_chkEnable);
            this.tabPage3.Controls.Add(this.m_btnGradientWpfProgress);
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
            // m_btnEjectDrive
            // 
            this.m_btnEjectDrive.Location = new System.Drawing.Point(208, 161);
            this.m_btnEjectDrive.Name = "m_btnEjectDrive";
            this.m_btnEjectDrive.Size = new System.Drawing.Size(75, 23);
            this.m_btnEjectDrive.TabIndex = 17;
            this.m_btnEjectDrive.Text = "Eject...";
            this.m_btnEjectDrive.UseVisualStyleBackColor = true;
            this.m_btnEjectDrive.Click += new System.EventHandler(this.m_btnEjectDrive_Click);
            // 
            // chartProgressUserControl1
            // 
            this.chartProgressUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartProgressUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartProgressUserControl1.GraphBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(250)))));
            this.chartProgressUserControl1.GraphMainColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.chartProgressUserControl1.GraphTitle = "Progress Bar";
            this.chartProgressUserControl1.Location = new System.Drawing.Point(160, 274);
            this.chartProgressUserControl1.Name = "chartProgressUserControl1";
            this.chartProgressUserControl1.Size = new System.Drawing.Size(567, 115);
            this.chartProgressUserControl1.TabIndex = 15;
            // 
            // m_chkEnable
            // 
            this.m_chkEnable.AutoSize = true;
            this.m_chkEnable.Checked = true;
            this.m_chkEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkEnable.Location = new System.Drawing.Point(160, 130);
            this.m_chkEnable.Name = "m_chkEnable";
            this.m_chkEnable.Size = new System.Drawing.Size(59, 17);
            this.m_chkEnable.TabIndex = 12;
            this.m_chkEnable.Text = "Enable";
            this.m_chkEnable.UseVisualStyleBackColor = true;
            this.m_chkEnable.CheckedChanged += new System.EventHandler(this.m_chkEnable_CheckedChanged);
            // 
            // m_btnGradientWpfProgress
            // 
            this.m_btnGradientWpfProgress.Location = new System.Drawing.Point(319, 79);
            this.m_btnGradientWpfProgress.Name = "m_btnGradientWpfProgress";
            this.m_btnGradientWpfProgress.Size = new System.Drawing.Size(144, 45);
            this.m_btnGradientWpfProgress.TabIndex = 14;
            this.m_btnGradientWpfProgress.Text = "WPF Gradient Progress";
            this.m_btnGradientWpfProgress.UseVisualStyleBackColor = true;
            this.m_btnGradientWpfProgress.Click += new System.EventHandler(this.m_btnGradientWpfProgress_Click);
            // 
            // m_btnColorSlider
            // 
            this.m_btnColorSlider.Location = new System.Drawing.Point(319, 17);
            this.m_btnColorSlider.Name = "m_btnColorSlider";
            this.m_btnColorSlider.Size = new System.Drawing.Size(144, 45);
            this.m_btnColorSlider.TabIndex = 13;
            this.m_btnColorSlider.Text = "Color Slider Demo";
            this.m_btnColorSlider.UseVisualStyleBackColor = true;
            this.m_btnColorSlider.Click += new System.EventHandler(this.m_btnColorSlider_Click);
            // 
            // m_btnTestWPFMessageBoxWPF
            // 
            this.m_btnTestWPFMessageBoxWPF.Location = new System.Drawing.Point(484, 79);
            this.m_btnTestWPFMessageBoxWPF.Name = "m_btnTestWPFMessageBoxWPF";
            this.m_btnTestWPFMessageBoxWPF.Size = new System.Drawing.Size(144, 45);
            this.m_btnTestWPFMessageBoxWPF.TabIndex = 12;
            this.m_btnTestWPFMessageBoxWPF.Text = "Test WPFMessageBox(WPF)";
            this.m_btnTestWPFMessageBoxWPF.UseVisualStyleBackColor = true;
            this.m_btnTestWPFMessageBoxWPF.Click += new System.EventHandler(this.m_btnTestWPFMessageBoxWPF_Click);
            // 
            // m_btnTestWPFMessageBox
            // 
            this.m_btnTestWPFMessageBox.Location = new System.Drawing.Point(484, 17);
            this.m_btnTestWPFMessageBox.Name = "m_btnTestWPFMessageBox";
            this.m_btnTestWPFMessageBox.Size = new System.Drawing.Size(144, 45);
            this.m_btnTestWPFMessageBox.TabIndex = 11;
            this.m_btnTestWPFMessageBox.Text = "Test WPFMessageBox (WinForms)";
            this.m_btnTestWPFMessageBox.UseVisualStyleBackColor = true;
            this.m_btnTestWPFMessageBox.Click += new System.EventHandler(this.m_btnTestWPFMessageBox_Click);
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
            themeColorSet1.Theme = MkZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Speaker;
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
            themeColorSet2.Theme = MkZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Custom;
            themeColorSet2.Threshold1 = 101;
            themeColorSet2.Threshold2 = 101;
            this.colorBarsProgressBar2.ColorTheme = themeColorSet2;
            this.colorBarsProgressBar2.Location = new System.Drawing.Point(160, 393);
            this.colorBarsProgressBar2.Name = "colorBarsProgressBar2";
            this.colorBarsProgressBar2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.colorBarsProgressBar2.Size = new System.Drawing.Size(567, 27);
            this.colorBarsProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
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
            themeColorSet3.Theme = MkZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Microphone;
            themeColorSet3.Threshold1 = 10;
            themeColorSet3.Threshold2 = 85;
            this.colorBarsProgressBar1.ColorTheme = themeColorSet3;
            this.colorBarsProgressBar1.Location = new System.Drawing.Point(53, 29);
            this.colorBarsProgressBar1.Name = "colorBarsProgressBar1";
            this.colorBarsProgressBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar1.Size = new System.Drawing.Size(21, 369);
            this.colorBarsProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.colorBarsProgressBar1.TabIndex = 0;
            this.colorBarsProgressBar1.TabStop = false;
            this.colorBarsProgressBar1.Value = 60;
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
            this.tabPage1.Controls.Add(this.m_btnSaveIcons);
            this.tabPage1.Controls.Add(this.m_btnOpenIconsFile);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.m_cmbListViewType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(742, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Icon Browser";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_btnSaveIcons
            // 
            this.m_btnSaveIcons.Location = new System.Drawing.Point(86, 6);
            this.m_btnSaveIcons.Name = "m_btnSaveIcons";
            this.m_btnSaveIcons.Size = new System.Drawing.Size(75, 23);
            this.m_btnSaveIcons.TabIndex = 9;
            this.m_btnSaveIcons.Text = "Save As...";
            this.m_btnSaveIcons.UseVisualStyleBackColor = true;
            this.m_btnSaveIcons.Click += new System.EventHandler(this.m_btnSaveIcons_Click);
            // 
            // m_btnOpenIconsFile
            // 
            this.m_btnOpenIconsFile.Location = new System.Drawing.Point(5, 6);
            this.m_btnOpenIconsFile.Name = "m_btnOpenIconsFile";
            this.m_btnOpenIconsFile.Size = new System.Drawing.Size(75, 23);
            this.m_btnOpenIconsFile.TabIndex = 8;
            this.m_btnOpenIconsFile.Text = "Open...";
            this.m_btnOpenIconsFile.UseVisualStyleBackColor = true;
            this.m_btnOpenIconsFile.Click += new System.EventHandler(this.m_btnOpenIconsFile_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.elementHost1);
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(742, 428);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Displays Topology";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(742, 393);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.displayTopologyUserControl1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkNonStickMouse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(742, 35);
            this.panel1.TabIndex = 1;
            // 
            // m_chkNonStickMouse
            // 
            this.m_chkNonStickMouse.AutoSize = true;
            this.m_chkNonStickMouse.Location = new System.Drawing.Point(13, 9);
            this.m_chkNonStickMouse.Name = "m_chkNonStickMouse";
            this.m_chkNonStickMouse.Size = new System.Drawing.Size(102, 17);
            this.m_chkNonStickMouse.TabIndex = 0;
            this.m_chkNonStickMouse.Text = "NonStickMouse";
            this.m_chkNonStickMouse.UseVisualStyleBackColor = true;
            this.m_chkNonStickMouse.CheckedChanged += new System.EventHandler(this.m_chkNonStickMouse_CheckedChanged);
            // 
            // m_cmbDriveLetter
            // 
            this.m_cmbDriveLetter.FormattingEnabled = true;
            this.m_cmbDriveLetter.Location = new System.Drawing.Point(161, 162);
            this.m_cmbDriveLetter.Name = "m_cmbDriveLetter";
            this.m_cmbDriveLetter.Size = new System.Drawing.Size(41, 21);
            this.m_cmbDriveLetter.TabIndex = 18;
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMainTest_FormClosed);
            this.Load += new System.EventHandler(this.FormMainTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Button m_btnGradientWpfProgress;
        private System.Windows.Forms.CheckBox m_chkEnable;
        private WindowsFormsApp1.ChartProgressUserControl chartProgressUserControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private WpfApplication6.DisplayTopologyUserControl displayTopologyUserControl1;
        private System.Windows.Forms.Button m_btnSaveIcons;
        private System.Windows.Forms.Button m_btnOpenIconsFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox m_chkNonStickMouse;
        private System.Windows.Forms.Button m_btnEjectDrive;
        private System.Windows.Forms.ComboBox m_cmbDriveLetter;
    }
}