namespace HotPixelTest
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
			this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.m_txtFileName = new System.Windows.Forms.TextBox();
			this.m_statusStrip = new System.Windows.Forms.StatusStrip();
			this.m_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.m_toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.m_numMinimum = new System.Windows.Forms.NumericUpDown();
			this.m_listDeadPixels = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.m_numZoom = new System.Windows.Forms.NumericUpDown();
			this.m_lblThreshold = new System.Windows.Forms.Label();
			this.m_lblZoom = new System.Windows.Forms.Label();
			this.m_toolStripMain = new System.Windows.Forms.ToolStrip();
			this.m_btnOpen = new System.Windows.Forms.ToolStripButton();
			this.m_btnSave = new System.Windows.Forms.ToolStripButton();
			this.m_btnStart = new System.Windows.Forms.ToolStripButton();
			this.m_btnStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.m_btnAbout = new System.Windows.Forms.ToolStripButton();
			this.m_splitContainerMain = new System.Windows.Forms.SplitContainer();
			this.m_pnlThreshold = new System.Windows.Forms.Panel();
			this.m_pnlPictureControl = new System.Windows.Forms.Panel();
			this.m_chkShowPixel = new System.Windows.Forms.CheckBox();
			this.m_pnlFile = new System.Windows.Forms.Panel();
			this.m_btnTest = new System.Windows.Forms.Button();
			this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.m_splitContainerInfo = new System.Windows.Forms.SplitContainer();
			this.m_listExif = new System.Windows.Forms.ListView();
			this.m_clmnExifAtt = new System.Windows.Forms.ColumnHeader();
			this.m_clmnExifVal = new System.Windows.Forms.ColumnHeader();
			this.m_pictureControl = new HotPixelTest.PictureControl();
			this.m_statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_numMinimum)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_numZoom)).BeginInit();
			this.m_toolStripMain.SuspendLayout();
			this.m_splitContainerMain.Panel1.SuspendLayout();
			this.m_splitContainerMain.Panel2.SuspendLayout();
			this.m_splitContainerMain.SuspendLayout();
			this.m_pnlThreshold.SuspendLayout();
			this.m_pnlPictureControl.SuspendLayout();
			this.m_pnlFile.SuspendLayout();
			this.m_splitContainerInfo.Panel1.SuspendLayout();
			this.m_splitContainerInfo.Panel2.SuspendLayout();
			this.m_splitContainerInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_pictureControl)).BeginInit();
			this.SuspendLayout();
			// 
			// m_openFileDialog
			// 
			this.m_openFileDialog.FileName = "*.jpg";
			// 
			// m_txtFileName
			// 
			this.m_txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_txtFileName.Location = new System.Drawing.Point(0, 5);
			this.m_txtFileName.Name = "m_txtFileName";
			this.m_txtFileName.Size = new System.Drawing.Size(576, 20);
			this.m_txtFileName.TabIndex = 0;
			this.m_txtFileName.Text = "P:\\Photo\\2010\\07\\2010_07_28\\Dead Pixel test SD4000\\20100728_SD4000_Dead_5509.JPG";
			// 
			// m_statusStrip
			// 
			this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel1,
            this.m_toolStripProgressBar1});
			this.m_statusStrip.Location = new System.Drawing.Point(0, 425);
			this.m_statusStrip.Name = "m_statusStrip";
			this.m_statusStrip.Size = new System.Drawing.Size(655, 22);
			this.m_statusStrip.TabIndex = 3;
			this.m_statusStrip.Text = "statusStrip1";
			// 
			// m_toolStripStatusLabel1
			// 
			this.m_toolStripStatusLabel1.Name = "m_toolStripStatusLabel1";
			this.m_toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
			this.m_toolStripStatusLabel1.Text = "Ready";
			// 
			// m_toolStripProgressBar1
			// 
			this.m_toolStripProgressBar1.Name = "m_toolStripProgressBar1";
			this.m_toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
			// 
			// m_numMinimum
			// 
			this.m_numMinimum.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.m_numMinimum.Location = new System.Drawing.Point(69, 3);
			this.m_numMinimum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.m_numMinimum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.m_numMinimum.Name = "m_numMinimum";
			this.m_numMinimum.Size = new System.Drawing.Size(65, 20);
			this.m_numMinimum.TabIndex = 1;
			this.m_numMinimum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_numMinimum.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.m_numMinimum.ValueChanged += new System.EventHandler(this.m_numMinimum_ValueChanged);
			// 
			// m_listDeadPixels
			// 
			this.m_listDeadPixels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
			this.m_listDeadPixels.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_listDeadPixels.FullRowSelect = true;
			this.m_listDeadPixels.GridLines = true;
			this.m_listDeadPixels.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.m_listDeadPixels.HideSelection = false;
			this.m_listDeadPixels.Location = new System.Drawing.Point(0, 28);
			this.m_listDeadPixels.MultiSelect = false;
			this.m_listDeadPixels.Name = "m_listDeadPixels";
			this.m_listDeadPixels.Size = new System.Drawing.Size(334, 341);
			this.m_listDeadPixels.TabIndex = 0;
			this.m_listDeadPixels.UseCompatibleStateImageBehavior = false;
			this.m_listDeadPixels.View = System.Windows.Forms.View.Details;
			this.m_listDeadPixels.SelectedIndexChanged += new System.EventHandler(this.m_listDeadPixels_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "idx";
			this.columnHeader1.Width = 40;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "x";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 40;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "y";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader3.Width = 40;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Luminance";
			this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader4.Width = 65;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "R";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader5.Width = 40;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "G";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader6.Width = 40;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "B";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader7.Width = 40;
			// 
			// m_numZoom
			// 
			this.m_numZoom.Increment = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.m_numZoom.Location = new System.Drawing.Point(51, 3);
			this.m_numZoom.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
			this.m_numZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.m_numZoom.Name = "m_numZoom";
			this.m_numZoom.Size = new System.Drawing.Size(65, 20);
			this.m_numZoom.TabIndex = 1;
			this.m_numZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_numZoom.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.m_numZoom.ValueChanged += new System.EventHandler(this.m_numZoom_ValueChanged);
			// 
			// m_lblThreshold
			// 
			this.m_lblThreshold.AutoSize = true;
			this.m_lblThreshold.Location = new System.Drawing.Point(6, 7);
			this.m_lblThreshold.Name = "m_lblThreshold";
			this.m_lblThreshold.Size = new System.Drawing.Size(57, 13);
			this.m_lblThreshold.TabIndex = 0;
			this.m_lblThreshold.Text = "Threshold:";
			// 
			// m_lblZoom
			// 
			this.m_lblZoom.AutoSize = true;
			this.m_lblZoom.Location = new System.Drawing.Point(8, 7);
			this.m_lblZoom.Name = "m_lblZoom";
			this.m_lblZoom.Size = new System.Drawing.Size(37, 13);
			this.m_lblZoom.TabIndex = 0;
			this.m_lblZoom.Text = "Zoom:";
			// 
			// m_toolStripMain
			// 
			this.m_toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_btnOpen,
            this.m_btnSave,
            this.m_btnStart,
            this.m_btnStop,
            this.toolStripSeparator1,
            this.m_btnAbout});
			this.m_toolStripMain.Location = new System.Drawing.Point(0, 0);
			this.m_toolStripMain.Name = "m_toolStripMain";
			this.m_toolStripMain.Size = new System.Drawing.Size(655, 25);
			this.m_toolStripMain.TabIndex = 0;
			this.m_toolStripMain.Text = "Tools";
			// 
			// m_btnOpen
			// 
			this.m_btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpen.Image")));
			this.m_btnOpen.ImageTransparentColor = System.Drawing.Color.Blue;
			this.m_btnOpen.Name = "m_btnOpen";
			this.m_btnOpen.Size = new System.Drawing.Size(53, 22);
			this.m_btnOpen.Text = "Open";
			this.m_btnOpen.ToolTipText = "Open Black Photo To Test...";
			this.m_btnOpen.Click += new System.EventHandler(this.m_btnOpen_Click);
			// 
			// m_btnSave
			// 
			this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
			this.m_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.m_btnSave.Name = "m_btnSave";
			this.m_btnSave.Size = new System.Drawing.Size(78, 22);
			this.m_btnSave.Text = "Save As...";
			this.m_btnSave.ToolTipText = "Save Result As CSV File ";
			this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
			// 
			// m_btnStart
			// 
			this.m_btnStart.Image = ((System.Drawing.Image)(resources.GetObject("m_btnStart.Image")));
			this.m_btnStart.ImageTransparentColor = System.Drawing.Color.Olive;
			this.m_btnStart.Name = "m_btnStart";
			this.m_btnStart.Size = new System.Drawing.Size(48, 22);
			this.m_btnStart.Text = "Test";
			this.m_btnStart.ToolTipText = "Start Processing Photo";
			this.m_btnStart.Click += new System.EventHandler(this.m_btnStart_Click);
			// 
			// m_btnStop
			// 
			this.m_btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.m_btnStop.Enabled = false;
			this.m_btnStop.Image = ((System.Drawing.Image)(resources.GetObject("m_btnStop.Image")));
			this.m_btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.m_btnStop.Name = "m_btnStop";
			this.m_btnStop.Size = new System.Drawing.Size(23, 22);
			this.m_btnStop.Text = "Break";
			this.m_btnStop.ToolTipText = "Break Operation";
			this.m_btnStop.Click += new System.EventHandler(this.m_btnStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// m_btnAbout
			// 
			this.m_btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAbout.Image")));
			this.m_btnAbout.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.m_btnAbout.Name = "m_btnAbout";
			this.m_btnAbout.Size = new System.Drawing.Size(68, 22);
			this.m_btnAbout.Text = "About...";
			this.m_btnAbout.Click += new System.EventHandler(this.m_btnAbout_Click);
			// 
			// m_splitContainerMain
			// 
			this.m_splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.m_splitContainerMain.Location = new System.Drawing.Point(0, 54);
			this.m_splitContainerMain.Name = "m_splitContainerMain";
			// 
			// m_splitContainerMain.Panel1
			// 
			this.m_splitContainerMain.Panel1.Controls.Add(this.m_listDeadPixels);
			this.m_splitContainerMain.Panel1.Controls.Add(this.m_pnlThreshold);
			// 
			// m_splitContainerMain.Panel2
			// 
			this.m_splitContainerMain.Panel2.Controls.Add(this.m_splitContainerInfo);
			this.m_splitContainerMain.Size = new System.Drawing.Size(655, 371);
			this.m_splitContainerMain.SplitterDistance = 336;
			this.m_splitContainerMain.TabIndex = 2;
			// 
			// m_pnlThreshold
			// 
			this.m_pnlThreshold.Controls.Add(this.m_numMinimum);
			this.m_pnlThreshold.Controls.Add(this.m_lblThreshold);
			this.m_pnlThreshold.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_pnlThreshold.Location = new System.Drawing.Point(0, 0);
			this.m_pnlThreshold.Name = "m_pnlThreshold";
			this.m_pnlThreshold.Size = new System.Drawing.Size(334, 28);
			this.m_pnlThreshold.TabIndex = 1;
			// 
			// m_pnlPictureControl
			// 
			this.m_pnlPictureControl.Controls.Add(this.m_chkShowPixel);
			this.m_pnlPictureControl.Controls.Add(this.m_lblZoom);
			this.m_pnlPictureControl.Controls.Add(this.m_numZoom);
			this.m_pnlPictureControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_pnlPictureControl.Location = new System.Drawing.Point(0, 0);
			this.m_pnlPictureControl.Name = "m_pnlPictureControl";
			this.m_pnlPictureControl.Size = new System.Drawing.Size(313, 28);
			this.m_pnlPictureControl.TabIndex = 0;
			// 
			// m_chkShowPixel
			// 
			this.m_chkShowPixel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_chkShowPixel.AutoSize = true;
			this.m_chkShowPixel.Checked = true;
			this.m_chkShowPixel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_chkShowPixel.Location = new System.Drawing.Point(230, 6);
			this.m_chkShowPixel.Name = "m_chkShowPixel";
			this.m_chkShowPixel.Size = new System.Drawing.Size(78, 17);
			this.m_chkShowPixel.TabIndex = 2;
			this.m_chkShowPixel.Text = "Show Pixel";
			this.m_chkShowPixel.UseVisualStyleBackColor = true;
			this.m_chkShowPixel.CheckedChanged += new System.EventHandler(this.m_chkShowPixel_CheckedChanged);
			// 
			// m_pnlFile
			// 
			this.m_pnlFile.Controls.Add(this.m_btnTest);
			this.m_pnlFile.Controls.Add(this.m_txtFileName);
			this.m_pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_pnlFile.Location = new System.Drawing.Point(0, 25);
			this.m_pnlFile.Name = "m_pnlFile";
			this.m_pnlFile.Size = new System.Drawing.Size(655, 29);
			this.m_pnlFile.TabIndex = 1;
			// 
			// m_btnTest
			// 
			this.m_btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnTest.Image = ((System.Drawing.Image)(resources.GetObject("m_btnTest.Image")));
			this.m_btnTest.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.m_btnTest.Location = new System.Drawing.Point(580, 4);
			this.m_btnTest.Name = "m_btnTest";
			this.m_btnTest.Size = new System.Drawing.Size(75, 23);
			this.m_btnTest.TabIndex = 1;
			this.m_btnTest.Text = "Test";
			this.m_btnTest.UseVisualStyleBackColor = true;
			this.m_btnTest.Click += new System.EventHandler(this.m_btnTest_Click);
			// 
			// m_saveFileDialog
			// 
			this.m_saveFileDialog.DefaultExt = "csv";
			this.m_saveFileDialog.Filter = "CSV Files(*.csv)|*.csv|All Files(*.*)|*.*";
			// 
			// m_splitContainerInfo
			// 
			this.m_splitContainerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_splitContainerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_splitContainerInfo.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.m_splitContainerInfo.Location = new System.Drawing.Point(0, 0);
			this.m_splitContainerInfo.Name = "m_splitContainerInfo";
			this.m_splitContainerInfo.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// m_splitContainerInfo.Panel1
			// 
			this.m_splitContainerInfo.Panel1.Controls.Add(this.m_listExif);
			// 
			// m_splitContainerInfo.Panel2
			// 
			this.m_splitContainerInfo.Panel2.Controls.Add(this.m_pictureControl);
			this.m_splitContainerInfo.Panel2.Controls.Add(this.m_pnlPictureControl);
			this.m_splitContainerInfo.Size = new System.Drawing.Size(315, 371);
			this.m_splitContainerInfo.SplitterDistance = 167;
			this.m_splitContainerInfo.TabIndex = 8;
			// 
			// m_listExif
			// 
			this.m_listExif.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_clmnExifAtt,
            this.m_clmnExifVal});
			this.m_listExif.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_listExif.FullRowSelect = true;
			this.m_listExif.GridLines = true;
			this.m_listExif.Location = new System.Drawing.Point(0, 0);
			this.m_listExif.Name = "m_listExif";
			this.m_listExif.Size = new System.Drawing.Size(313, 165);
			this.m_listExif.TabIndex = 0;
			this.m_listExif.UseCompatibleStateImageBehavior = false;
			this.m_listExif.View = System.Windows.Forms.View.Details;
			// 
			// m_clmnExifAtt
			// 
			this.m_clmnExifAtt.Text = "Parameter";
			this.m_clmnExifAtt.Width = 150;
			// 
			// m_clmnExifVal
			// 
			this.m_clmnExifVal.Text = "Value";
			this.m_clmnExifVal.Width = 150;
			// 
			// m_pictureControl
			// 
			this.m_pictureControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_pictureControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_pictureControl.Location = new System.Drawing.Point(0, 28);
			this.m_pictureControl.Name = "m_pictureControl";
			this.m_pictureControl.Size = new System.Drawing.Size(313, 170);
			this.m_pictureControl.TabIndex = 7;
			this.m_pictureControl.TabStop = false;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(655, 447);
			this.Controls.Add(this.m_splitContainerMain);
			this.Controls.Add(this.m_pnlFile);
			this.Controls.Add(this.m_toolStripMain);
			this.Controls.Add(this.m_statusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 300);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Hot Pixel Test";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
			this.m_statusStrip.ResumeLayout(false);
			this.m_statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_numMinimum)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_numZoom)).EndInit();
			this.m_toolStripMain.ResumeLayout(false);
			this.m_toolStripMain.PerformLayout();
			this.m_splitContainerMain.Panel1.ResumeLayout(false);
			this.m_splitContainerMain.Panel2.ResumeLayout(false);
			this.m_splitContainerMain.ResumeLayout(false);
			this.m_pnlThreshold.ResumeLayout(false);
			this.m_pnlThreshold.PerformLayout();
			this.m_pnlPictureControl.ResumeLayout(false);
			this.m_pnlPictureControl.PerformLayout();
			this.m_pnlFile.ResumeLayout(false);
			this.m_pnlFile.PerformLayout();
			this.m_splitContainerInfo.Panel1.ResumeLayout(false);
			this.m_splitContainerInfo.Panel2.ResumeLayout(false);
			this.m_splitContainerInfo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_pictureControl)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog m_openFileDialog;
		private System.Windows.Forms.TextBox m_txtFileName;
		private System.Windows.Forms.StatusStrip m_statusStrip;
		private System.Windows.Forms.ToolStripProgressBar m_toolStripProgressBar1;
		private System.Windows.Forms.NumericUpDown m_numMinimum;
		private System.Windows.Forms.ListView m_listDeadPixels;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.NumericUpDown m_numZoom;
		private System.Windows.Forms.Label m_lblThreshold;
		private System.Windows.Forms.Label m_lblZoom;
		private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel1;
		private System.Windows.Forms.ToolStrip m_toolStripMain;
		private System.Windows.Forms.SplitContainer m_splitContainerMain;
		private System.Windows.Forms.Panel m_pnlFile;
		private System.Windows.Forms.Panel m_pnlThreshold;
		private System.Windows.Forms.Panel m_pnlPictureControl;
		private System.Windows.Forms.ToolStripButton m_btnAbout;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ToolStripButton m_btnOpen;
		private System.Windows.Forms.ToolStripButton m_btnStart;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton m_btnSave;
		private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
		private System.Windows.Forms.Button m_btnTest;
		private System.Windows.Forms.ToolStripButton m_btnStop;
		private System.Windows.Forms.CheckBox m_chkShowPixel;
		private PictureControl m_pictureControl;
		private System.Windows.Forms.SplitContainer m_splitContainerInfo;
		private System.Windows.Forms.ListView m_listExif;
		private System.Windows.Forms.ColumnHeader m_clmnExifAtt;
		private System.Windows.Forms.ColumnHeader m_clmnExifVal;
	}
}

