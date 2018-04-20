namespace MindLamp
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.m_trackBuffer = new System.Windows.Forms.TrackBar();
            this.m_trackInterval = new System.Windows.Forms.TrackBar();
            this.m_pic = new System.Windows.Forms.PictureBox();
            this.m_lblBuffer = new System.Windows.Forms.Label();
            this.m_lblTInterval = new System.Windows.Forms.Label();
            this.m_cmbDevice = new System.Windows.Forms.ComboBox();
            this.m_btnStart = new System.Windows.Forms.Button();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.m_picCurrent = new System.Windows.Forms.PictureBox();
            this.m_trackRange = new System.Windows.Forms.TrackBar();
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_pnlTools = new System.Windows.Forms.Panel();
            this.m_tabMode = new System.Windows.Forms.TabControl();
            this.m_tabPage1 = new System.Windows.Forms.TabPage();
            this.m_picWhite2Color = new System.Windows.Forms.PictureBox();
            this.m_tabPage2 = new System.Windows.Forms.TabPage();
            this.m_picRainbow = new System.Windows.Forms.PictureBox();
            this.m_pnlStatus = new System.Windows.Forms.Panel();
            this.m_pnlColors = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_lblCurrenColor = new System.Windows.Forms.Label();
            this.m_splitMain = new System.Windows.Forms.SplitContainer();
            this.m_splitView = new System.Windows.Forms.SplitContainer();
            this.m_splitSliders = new System.Windows.Forms.SplitContainer();
            this.m_numRange = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.m_numLuminosity = new System.Windows.Forms.NumericUpDown();
            this.m_chartValues = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.m_pnlControl = new System.Windows.Forms.Panel();
            this.m_chkSW_RG = new System.Windows.Forms.CheckBox();
            this.m_pnlRainbow = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackBuffer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackRange)).BeginInit();
            this.m_statusStrip.SuspendLayout();
            this.m_pnlTools.SuspendLayout();
            this.m_tabMode.SuspendLayout();
            this.m_tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picWhite2Color)).BeginInit();
            this.m_tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picRainbow)).BeginInit();
            this.m_pnlStatus.SuspendLayout();
            this.m_pnlColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).BeginInit();
            this.m_splitMain.Panel1.SuspendLayout();
            this.m_splitMain.Panel2.SuspendLayout();
            this.m_splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitView)).BeginInit();
            this.m_splitView.Panel1.SuspendLayout();
            this.m_splitView.Panel2.SuspendLayout();
            this.m_splitView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitSliders)).BeginInit();
            this.m_splitSliders.Panel1.SuspendLayout();
            this.m_splitSliders.Panel2.SuspendLayout();
            this.m_splitSliders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numLuminosity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chartValues)).BeginInit();
            this.m_pnlControl.SuspendLayout();
            this.m_pnlRainbow.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trackBuffer
            // 
            this.m_trackBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_trackBuffer.Location = new System.Drawing.Point(185, 60);
            this.m_trackBuffer.Maximum = 1000;
            this.m_trackBuffer.Minimum = 10;
            this.m_trackBuffer.Name = "m_trackBuffer";
            this.m_trackBuffer.Size = new System.Drawing.Size(300, 45);
            this.m_trackBuffer.TabIndex = 3;
            this.m_trackBuffer.TickFrequency = 100;
            this.m_trackBuffer.Value = 300;
            this.m_trackBuffer.ValueChanged += new System.EventHandler(this.m_trackBuffer_ValueChanged);
            // 
            // m_trackInterval
            // 
            this.m_trackInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_trackInterval.LargeChange = 500;
            this.m_trackInterval.Location = new System.Drawing.Point(185, 9);
            this.m_trackInterval.Maximum = 10000;
            this.m_trackInterval.Minimum = 10;
            this.m_trackInterval.Name = "m_trackInterval";
            this.m_trackInterval.Size = new System.Drawing.Size(300, 45);
            this.m_trackInterval.SmallChange = 100;
            this.m_trackInterval.TabIndex = 5;
            this.m_trackInterval.TickFrequency = 1000;
            this.m_trackInterval.Value = 1000;
            this.m_trackInterval.Scroll += new System.EventHandler(this.m_trackInterval_Scroll);
            this.m_trackInterval.ValueChanged += new System.EventHandler(this.m_trackInterval_ValueChanged);
            // 
            // m_pic
            // 
            this.m_pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pic.BackColor = System.Drawing.SystemColors.Info;
            this.m_pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pic.Location = new System.Drawing.Point(93, 60);
            this.m_pic.Name = "m_pic";
            this.m_pic.Size = new System.Drawing.Size(384, 186);
            this.m_pic.TabIndex = 2;
            this.m_pic.TabStop = false;
            // 
            // m_lblBuffer
            // 
            this.m_lblBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblBuffer.AutoSize = true;
            this.m_lblBuffer.Location = new System.Drawing.Point(10, 57);
            this.m_lblBuffer.Name = "m_lblBuffer";
            this.m_lblBuffer.Size = new System.Drawing.Size(38, 13);
            this.m_lblBuffer.TabIndex = 2;
            this.m_lblBuffer.Text = "Buffer:";
            // 
            // m_lblTInterval
            // 
            this.m_lblTInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblTInterval.AutoSize = true;
            this.m_lblTInterval.Location = new System.Drawing.Point(8, 12);
            this.m_lblTInterval.Name = "m_lblTInterval";
            this.m_lblTInterval.Size = new System.Drawing.Size(67, 13);
            this.m_lblTInterval.TabIndex = 4;
            this.m_lblTInterval.Text = "Interval (ms):";
            // 
            // m_cmbDevice
            // 
            this.m_cmbDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbDevice.FormattingEnabled = true;
            this.m_cmbDevice.Location = new System.Drawing.Point(3, 11);
            this.m_cmbDevice.Name = "m_cmbDevice";
            this.m_cmbDevice.Size = new System.Drawing.Size(547, 21);
            this.m_cmbDevice.TabIndex = 0;
            // 
            // m_btnStart
            // 
            this.m_btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnStart.Location = new System.Drawing.Point(748, 9);
            this.m_btnStart.Name = "m_btnStart";
            this.m_btnStart.Size = new System.Drawing.Size(75, 23);
            this.m_btnStart.TabIndex = 1;
            this.m_btnStart.Text = "Play >";
            this.m_btnStart.UseVisualStyleBackColor = true;
            this.m_btnStart.Click += new System.EventHandler(this.m_btnStart_Click);
            // 
            // m_timer
            // 
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // m_picCurrent
            // 
            this.m_picCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_picCurrent.BackColor = System.Drawing.Color.White;
            this.m_picCurrent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_picCurrent.Location = new System.Drawing.Point(1, 1);
            this.m_picCurrent.Name = "m_picCurrent";
            this.m_picCurrent.Size = new System.Drawing.Size(79, 248);
            this.m_picCurrent.TabIndex = 6;
            this.m_picCurrent.TabStop = false;
            // 
            // m_trackRange
            // 
            this.m_trackRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_trackRange.LargeChange = 50;
            this.m_trackRange.Location = new System.Drawing.Point(97, 27);
            this.m_trackRange.Maximum = 780;
            this.m_trackRange.Minimum = 380;
            this.m_trackRange.Name = "m_trackRange";
            this.m_trackRange.Size = new System.Drawing.Size(371, 45);
            this.m_trackRange.TabIndex = 7;
            this.m_trackRange.TickFrequency = 40;
            this.m_trackRange.Value = 500;
            this.m_trackRange.Scroll += new System.EventHandler(this.m_trackRange_Scroll);
            this.m_trackRange.ValueChanged += new System.EventHandler(this.m_trackRange_ValueChanged);
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_lblStatus});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 540);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Size = new System.Drawing.Size(834, 22);
            this.m_statusStrip.TabIndex = 8;
            this.m_statusStrip.Text = "statusStrip1";
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(39, 17);
            this.m_lblStatus.Text = "Ready";
            // 
            // m_pnlTools
            // 
            this.m_pnlTools.Controls.Add(this.m_tabMode);
            this.m_pnlTools.Controls.Add(this.m_pnlStatus);
            this.m_pnlTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlTools.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTools.Name = "m_pnlTools";
            this.m_pnlTools.Size = new System.Drawing.Size(334, 336);
            this.m_pnlTools.TabIndex = 9;
            // 
            // m_tabMode
            // 
            this.m_tabMode.Controls.Add(this.m_tabPage1);
            this.m_tabMode.Controls.Add(this.m_tabPage2);
            this.m_tabMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabMode.Location = new System.Drawing.Point(0, 0);
            this.m_tabMode.Name = "m_tabMode";
            this.m_tabMode.SelectedIndex = 0;
            this.m_tabMode.Size = new System.Drawing.Size(334, 269);
            this.m_tabMode.TabIndex = 0;
            // 
            // m_tabPage1
            // 
            this.m_tabPage1.Controls.Add(this.m_picWhite2Color);
            this.m_tabPage1.Location = new System.Drawing.Point(4, 22);
            this.m_tabPage1.Name = "m_tabPage1";
            this.m_tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabPage1.Size = new System.Drawing.Size(326, 243);
            this.m_tabPage1.TabIndex = 0;
            this.m_tabPage1.Text = "White to Color";
            this.m_tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_picWhite2Color
            // 
            this.m_picWhite2Color.BackColor = System.Drawing.Color.White;
            this.m_picWhite2Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_picWhite2Color.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_picWhite2Color.Image = ((System.Drawing.Image)(resources.GetObject("m_picWhite2Color.Image")));
            this.m_picWhite2Color.Location = new System.Drawing.Point(3, 3);
            this.m_picWhite2Color.Name = "m_picWhite2Color";
            this.m_picWhite2Color.Size = new System.Drawing.Size(320, 237);
            this.m_picWhite2Color.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_picWhite2Color.TabIndex = 0;
            this.m_picWhite2Color.TabStop = false;
            // 
            // m_tabPage2
            // 
            this.m_tabPage2.Controls.Add(this.m_picRainbow);
            this.m_tabPage2.Location = new System.Drawing.Point(4, 22);
            this.m_tabPage2.Name = "m_tabPage2";
            this.m_tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabPage2.Size = new System.Drawing.Size(326, 243);
            this.m_tabPage2.TabIndex = 1;
            this.m_tabPage2.Text = "Rainbow";
            this.m_tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_picRainbow
            // 
            this.m_picRainbow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_picRainbow.Image = ((System.Drawing.Image)(resources.GetObject("m_picRainbow.Image")));
            this.m_picRainbow.Location = new System.Drawing.Point(3, 3);
            this.m_picRainbow.Name = "m_picRainbow";
            this.m_picRainbow.Size = new System.Drawing.Size(320, 237);
            this.m_picRainbow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_picRainbow.TabIndex = 0;
            this.m_picRainbow.TabStop = false;
            // 
            // m_pnlStatus
            // 
            this.m_pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlStatus.Controls.Add(this.m_pnlColors);
            this.m_pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlStatus.Location = new System.Drawing.Point(0, 269);
            this.m_pnlStatus.Name = "m_pnlStatus";
            this.m_pnlStatus.Size = new System.Drawing.Size(334, 67);
            this.m_pnlStatus.TabIndex = 10;
            // 
            // m_pnlColors
            // 
            this.m_pnlColors.ColumnCount = 4;
            this.m_pnlColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.m_pnlColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.m_pnlColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.m_pnlColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.m_pnlColors.Controls.Add(this.label8, 3, 1);
            this.m_pnlColors.Controls.Add(this.label1, 0, 0);
            this.m_pnlColors.Controls.Add(this.label7, 2, 1);
            this.m_pnlColors.Controls.Add(this.label2, 1, 0);
            this.m_pnlColors.Controls.Add(this.label6, 1, 1);
            this.m_pnlColors.Controls.Add(this.label3, 2, 0);
            this.m_pnlColors.Controls.Add(this.label5, 0, 1);
            this.m_pnlColors.Controls.Add(this.label4, 3, 0);
            this.m_pnlColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlColors.Location = new System.Drawing.Point(0, 0);
            this.m_pnlColors.Name = "m_pnlColors";
            this.m_pnlColors.RowCount = 3;
            this.m_pnlColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_pnlColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_pnlColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_pnlColors.Size = new System.Drawing.Size(330, 63);
            this.m_pnlColors.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(249, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 21);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(167, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "label7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkOrange;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(85, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Blue;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(85, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "label6";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Yellow;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(167, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Cyan;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Lime;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(249, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblCurrenColor
            // 
            this.m_lblCurrenColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblCurrenColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_lblCurrenColor.Location = new System.Drawing.Point(0, 336);
            this.m_lblCurrenColor.Name = "m_lblCurrenColor";
            this.m_lblCurrenColor.Size = new System.Drawing.Size(334, 31);
            this.m_lblCurrenColor.TabIndex = 11;
            this.m_lblCurrenColor.Text = "oooo";
            // 
            // m_splitMain
            // 
            this.m_splitMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitMain.Location = new System.Drawing.Point(0, 44);
            this.m_splitMain.Name = "m_splitMain";
            this.m_splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitMain.Panel1
            // 
            this.m_splitMain.Panel1.Controls.Add(this.m_splitView);
            // 
            // m_splitMain.Panel2
            // 
            this.m_splitMain.Panel2.Controls.Add(this.m_chartValues);
            this.m_splitMain.Panel2.Controls.Add(this.m_pnlRainbow);
            this.m_splitMain.Size = new System.Drawing.Size(834, 496);
            this.m_splitMain.SplitterDistance = 371;
            this.m_splitMain.TabIndex = 12;
            // 
            // m_splitView
            // 
            this.m_splitView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitView.Location = new System.Drawing.Point(0, 0);
            this.m_splitView.Name = "m_splitView";
            // 
            // m_splitView.Panel1
            // 
            this.m_splitView.Panel1.Controls.Add(this.m_splitSliders);
            // 
            // m_splitView.Panel2
            // 
            this.m_splitView.Panel2.Controls.Add(this.m_pnlTools);
            this.m_splitView.Panel2.Controls.Add(this.m_lblCurrenColor);
            this.m_splitView.Size = new System.Drawing.Size(834, 371);
            this.m_splitView.SplitterDistance = 492;
            this.m_splitView.TabIndex = 12;
            // 
            // m_splitSliders
            // 
            this.m_splitSliders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitSliders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitSliders.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitSliders.Location = new System.Drawing.Point(0, 0);
            this.m_splitSliders.Name = "m_splitSliders";
            this.m_splitSliders.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitSliders.Panel1
            // 
            this.m_splitSliders.Panel1.Controls.Add(this.m_pic);
            this.m_splitSliders.Panel1.Controls.Add(this.m_numRange);
            this.m_splitSliders.Panel1.Controls.Add(this.m_picCurrent);
            this.m_splitSliders.Panel1.Controls.Add(this.label9);
            this.m_splitSliders.Panel1.Controls.Add(this.m_trackRange);
            this.m_splitSliders.Panel1.Controls.Add(this.m_numLuminosity);
            // 
            // m_splitSliders.Panel2
            // 
            this.m_splitSliders.Panel2.Controls.Add(this.m_lblTInterval);
            this.m_splitSliders.Panel2.Controls.Add(this.m_trackInterval);
            this.m_splitSliders.Panel2.Controls.Add(this.m_lblBuffer);
            this.m_splitSliders.Panel2.Controls.Add(this.m_trackBuffer);
            this.m_splitSliders.Size = new System.Drawing.Size(492, 371);
            this.m_splitSliders.SplitterDistance = 255;
            this.m_splitSliders.TabIndex = 14;
            // 
            // m_numRange
            // 
            this.m_numRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_numRange.Location = new System.Drawing.Point(411, 9);
            this.m_numRange.Maximum = new decimal(new int[] {
            780,
            0,
            0,
            0});
            this.m_numRange.Minimum = new decimal(new int[] {
            380,
            0,
            0,
            0});
            this.m_numRange.Name = "m_numRange";
            this.m_numRange.Size = new System.Drawing.Size(57, 20);
            this.m_numRange.TabIndex = 11;
            this.m_numRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numRange.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(94, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Color by WaveLength (380 - 780 nm) - or MindLamp Use";
            // 
            // m_numLuminosity
            // 
            this.m_numLuminosity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_numLuminosity.Location = new System.Drawing.Point(11, 264);
            this.m_numLuminosity.Maximum = new decimal(new int[] {
            239,
            0,
            0,
            0});
            this.m_numLuminosity.Name = "m_numLuminosity";
            this.m_numLuminosity.Size = new System.Drawing.Size(69, 20);
            this.m_numLuminosity.TabIndex = 12;
            this.m_numLuminosity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numLuminosity.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.m_numLuminosity.ValueChanged += new System.EventHandler(this.m_numLuminosity_ValueChanged);
            // 
            // m_chartValues
            // 
            this.m_chartValues.BackColor = System.Drawing.Color.Black;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            chartArea1.AxisX.LineColor = System.Drawing.Color.Yellow;
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Olive;
            chartArea1.AxisX.MajorTickMark.LineWidth = 2;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Green;
            chartArea1.AxisX.MinorGrid.LineWidth = 2;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Blue;
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            chartArea1.AxisY.LineWidth = 3;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Magenta;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.Name = "ChartArea1";
            this.m_chartValues.ChartAreas.Add(chartArea1);
            this.m_chartValues.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.m_chartValues.Legends.Add(legend1);
            this.m_chartValues.Location = new System.Drawing.Point(0, 0);
            this.m_chartValues.Name = "m_chartValues";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Lime;
            series1.LabelForeColor = System.Drawing.Color.Empty;
            series1.Legend = "Legend1";
            series1.Name = "Current";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Yellow;
            series2.Legend = "Legend1";
            series2.Name = "ZeroLine";
            this.m_chartValues.Series.Add(series1);
            this.m_chartValues.Series.Add(series2);
            this.m_chartValues.Size = new System.Drawing.Size(750, 117);
            this.m_chartValues.TabIndex = 0;
            this.m_chartValues.Text = "chart1";
            // 
            // m_pnlControl
            // 
            this.m_pnlControl.Controls.Add(this.m_chkSW_RG);
            this.m_pnlControl.Controls.Add(this.m_cmbDevice);
            this.m_pnlControl.Controls.Add(this.m_btnStart);
            this.m_pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlControl.Location = new System.Drawing.Point(0, 0);
            this.m_pnlControl.Name = "m_pnlControl";
            this.m_pnlControl.Size = new System.Drawing.Size(834, 44);
            this.m_pnlControl.TabIndex = 13;
            // 
            // m_chkSW_RG
            // 
            this.m_chkSW_RG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkSW_RG.AutoSize = true;
            this.m_chkSW_RG.Location = new System.Drawing.Point(565, 13);
            this.m_chkSW_RG.Name = "m_chkSW_RG";
            this.m_chkSW_RG.Size = new System.Drawing.Size(174, 17);
            this.m_chkSW_RG.TabIndex = 2;
            this.m_chkSW_RG.Text = "Use software random generator";
            this.m_chkSW_RG.UseVisualStyleBackColor = true;
            this.m_chkSW_RG.CheckedChanged += new System.EventHandler(this.m_chkSW_RG_CheckedChanged);
            // 
            // m_pnlRainbow
            // 
            this.m_pnlRainbow.ColumnCount = 1;
            this.m_pnlRainbow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_pnlRainbow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_pnlRainbow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_pnlRainbow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_pnlRainbow.Controls.Add(this.label11, 0, 7);
            this.m_pnlRainbow.Controls.Add(this.label13, 0, 6);
            this.m_pnlRainbow.Controls.Add(this.label15, 0, 5);
            this.m_pnlRainbow.Controls.Add(this.label17, 0, 4);
            this.m_pnlRainbow.Controls.Add(this.label16, 0, 3);
            this.m_pnlRainbow.Controls.Add(this.label14, 0, 2);
            this.m_pnlRainbow.Controls.Add(this.label12, 0, 1);
            this.m_pnlRainbow.Controls.Add(this.label10, 0, 0);
            this.m_pnlRainbow.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_pnlRainbow.Location = new System.Drawing.Point(750, 0);
            this.m_pnlRainbow.Name = "m_pnlRainbow";
            this.m_pnlRainbow.RowCount = 8;
            this.m_pnlRainbow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.m_pnlRainbow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.m_pnlRainbow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.m_pnlRainbow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.m_pnlRainbow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.m_pnlRainbow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.m_pnlRainbow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.m_pnlRainbow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.m_pnlRainbow.Size = new System.Drawing.Size(80, 117);
            this.m_pnlRainbow.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 14);
            this.label10.TabIndex = 7;
            this.label10.Text = "...";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Red;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 19);
            this.label11.TabIndex = 0;
            this.label11.Text = "...";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 14);
            this.label12.TabIndex = 6;
            this.label12.Text = "...";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.DarkOrange;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(3, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 14);
            this.label13.TabIndex = 1;
            this.label13.Text = "...";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Blue;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 14);
            this.label14.TabIndex = 5;
            this.label14.Text = "...";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Yellow;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 14);
            this.label15.TabIndex = 2;
            this.label15.Text = "...";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Cyan;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(3, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 14);
            this.label16.TabIndex = 4;
            this.label16.Text = "...";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Lime;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(3, 56);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 14);
            this.label17.TabIndex = 3;
            this.label17.Text = "...";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 562);
            this.Controls.Add(this.m_splitMain);
            this.Controls.Add(this.m_pnlControl);
            this.Controls.Add(this.m_statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.Text = "Lamp !!!";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_trackBuffer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackRange)).EndInit();
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.m_pnlTools.ResumeLayout(false);
            this.m_tabMode.ResumeLayout(false);
            this.m_tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picWhite2Color)).EndInit();
            this.m_tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picRainbow)).EndInit();
            this.m_pnlStatus.ResumeLayout(false);
            this.m_pnlColors.ResumeLayout(false);
            this.m_pnlColors.PerformLayout();
            this.m_splitMain.Panel1.ResumeLayout(false);
            this.m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).EndInit();
            this.m_splitMain.ResumeLayout(false);
            this.m_splitView.Panel1.ResumeLayout(false);
            this.m_splitView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitView)).EndInit();
            this.m_splitView.ResumeLayout(false);
            this.m_splitSliders.Panel1.ResumeLayout(false);
            this.m_splitSliders.Panel1.PerformLayout();
            this.m_splitSliders.Panel2.ResumeLayout(false);
            this.m_splitSliders.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitSliders)).EndInit();
            this.m_splitSliders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_numRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numLuminosity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chartValues)).EndInit();
            this.m_pnlControl.ResumeLayout(false);
            this.m_pnlControl.PerformLayout();
            this.m_pnlRainbow.ResumeLayout(false);
            this.m_pnlRainbow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TrackBar m_trackBuffer;
		private System.Windows.Forms.TrackBar m_trackInterval;
		private System.Windows.Forms.PictureBox m_pic;
		private System.Windows.Forms.Label m_lblBuffer;
		private System.Windows.Forms.Label m_lblTInterval;
		private System.Windows.Forms.ComboBox m_cmbDevice;
		private System.Windows.Forms.Button m_btnStart;
		private System.Windows.Forms.Timer m_timer;
		private System.Windows.Forms.PictureBox m_picCurrent;
		private System.Windows.Forms.TrackBar m_trackRange;
		private System.Windows.Forms.StatusStrip m_statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel m_lblStatus;
		private System.Windows.Forms.Panel m_pnlTools;
		private System.Windows.Forms.TabControl m_tabMode;
		private System.Windows.Forms.TabPage m_tabPage1;
		private System.Windows.Forms.TabPage m_tabPage2;
		private System.Windows.Forms.PictureBox m_picWhite2Color;
		private System.Windows.Forms.PictureBox m_picRainbow;
		private System.Windows.Forms.Panel m_pnlStatus;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label m_lblCurrenColor;
        private System.Windows.Forms.SplitContainer m_splitMain;
        private System.Windows.Forms.SplitContainer m_splitView;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_chartValues;
        private System.Windows.Forms.Panel m_pnlControl;
        private System.Windows.Forms.NumericUpDown m_numRange;
        private System.Windows.Forms.NumericUpDown m_numLuminosity;
        private System.Windows.Forms.TableLayoutPanel m_pnlColors;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.SplitContainer m_splitSliders;
        private System.Windows.Forms.CheckBox m_chkSW_RG;
        private System.Windows.Forms.TableLayoutPanel m_pnlRainbow;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
    }
}

