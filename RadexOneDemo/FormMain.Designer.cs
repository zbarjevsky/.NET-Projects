namespace RadexOneDemo
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_txtLog = new System.Windows.Forms.RichTextBox();
            this.m_btnRequestData = new System.Windows.Forms.Button();
            this.m_lblConnectStatus = new System.Windows.Forms.Label();
            this.m_chkAutoRequestData = new System.Windows.Forms.CheckBox();
            this.m_txtStatus = new System.Windows.Forms.TextBox();
            this.m_lblVal = new System.Windows.Forms.Label();
            this.m_btnClear = new System.Windows.Forms.Button();
            this.m_btnVer = new System.Windows.Forms.Button();
            this.m_btnWriteConfig = new System.Windows.Forms.Button();
            this.m_btnReadConfig = new System.Windows.Forms.Button();
            this.m_lblSN = new System.Windows.Forms.Label();
            this.m_lblCPM = new System.Windows.Forms.Label();
            this.m_numMaxCPM = new System.Windows.Forms.NumericUpDown();
            this.m_splitContainerTools = new System.Windows.Forms.SplitContainer();
            this.m_picRadiationStatus = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtRecords = new System.Windows.Forms.RichTextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.m_lblDose = new System.Windows.Forms.Label();
            this.m_lblAlertVolume = new System.Windows.Forms.Label();
            this.m_trackAlertVolume = new System.Windows.Forms.TrackBar();
            this.m_lblInterval = new System.Windows.Forms.Label();
            this.m_numInterval = new System.Windows.Forms.NumericUpDown();
            this.m_lblMaxCPM = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.m_chkShowLog = new System.Windows.Forms.CheckBox();
            this.m_btnDisconnect = new System.Windows.Forms.Button();
            this.m_btnConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnHistory = new System.Windows.Forms.Button();
            this.m_chkAutoConnect = new System.Windows.Forms.CheckBox();
            this.m_btnTest = new System.Windows.Forms.Button();
            this.m_cmbDevices = new System.Windows.Forms.ComboBox();
            this.m_btnResetDose = new System.Windows.Forms.Button();
            this.m_statusStripMain = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_ProgressBarStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuDeviceConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tabControlAll = new System.Windows.Forms.TabControl();
            this.m_tabPage1_Device = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.m_tabPage3_About = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.m_tabPage2_Settings = new System.Windows.Forms.TabPage();
            this.m_btnDeviceConfig = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.m_splitMain = new System.Windows.Forms.SplitContainer();
            this.m_pnlTools = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.m_progressMain = new RadexOneDemo.VerticalProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.m_numMaxCPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerTools)).BeginInit();
            this.m_splitContainerTools.Panel1.SuspendLayout();
            this.m_splitContainerTools.Panel2.SuspendLayout();
            this.m_splitContainerTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picRadiationStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackAlertVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).BeginInit();
            this.m_statusStripMain.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.m_tabControlAll.SuspendLayout();
            this.m_tabPage1_Device.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.m_tabPage3_About.SuspendLayout();
            this.m_tabPage2_Settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).BeginInit();
            this.m_splitMain.Panel1.SuspendLayout();
            this.m_splitMain.Panel2.SuspendLayout();
            this.m_splitMain.SuspendLayout();
            this.m_pnlTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtLog
            // 
            this.m_txtLog.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtLog.Location = new System.Drawing.Point(0, 0);
            this.m_txtLog.Name = "m_txtLog";
            this.m_txtLog.Size = new System.Drawing.Size(859, 117);
            this.m_txtLog.TabIndex = 0;
            this.m_txtLog.Text = "";
            // 
            // m_btnRequestData
            // 
            this.m_btnRequestData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRequestData.Location = new System.Drawing.Point(553, 490);
            this.m_btnRequestData.Name = "m_btnRequestData";
            this.m_btnRequestData.Size = new System.Drawing.Size(150, 23);
            this.m_btnRequestData.TabIndex = 9;
            this.m_btnRequestData.Text = "Request Data";
            this.m_btnRequestData.UseVisualStyleBackColor = true;
            this.m_btnRequestData.Click += new System.EventHandler(this.m_btnRequest_Click);
            // 
            // m_lblConnectStatus
            // 
            this.m_lblConnectStatus.AutoSize = true;
            this.m_lblConnectStatus.Enabled = false;
            this.m_lblConnectStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.m_lblConnectStatus.Location = new System.Drawing.Point(4, 2);
            this.m_lblConnectStatus.Name = "m_lblConnectStatus";
            this.m_lblConnectStatus.Size = new System.Drawing.Size(34, 29);
            this.m_lblConnectStatus.TabIndex = 0;
            this.m_lblConnectStatus.Text = "...";
            // 
            // m_chkAutoRequestData
            // 
            this.m_chkAutoRequestData.AutoSize = true;
            this.m_chkAutoRequestData.Location = new System.Drawing.Point(9, 47);
            this.m_chkAutoRequestData.Name = "m_chkAutoRequestData";
            this.m_chkAutoRequestData.Size = new System.Drawing.Size(157, 17);
            this.m_chkAutoRequestData.TabIndex = 1;
            this.m_chkAutoRequestData.Text = "Request Data Automatically";
            this.m_chkAutoRequestData.UseVisualStyleBackColor = true;
            this.m_chkAutoRequestData.CheckedChanged += new System.EventHandler(this.m_chkAuto_CheckedChanged);
            // 
            // m_txtStatus
            // 
            this.m_txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtStatus.Location = new System.Drawing.Point(9, 75);
            this.m_txtStatus.Multiline = true;
            this.m_txtStatus.Name = "m_txtStatus";
            this.m_txtStatus.ReadOnly = true;
            this.m_txtStatus.Size = new System.Drawing.Size(841, 50);
            this.m_txtStatus.TabIndex = 6;
            // 
            // m_lblVal
            // 
            this.m_lblVal.BackColor = System.Drawing.Color.Chartreuse;
            this.m_lblVal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblVal.Location = new System.Drawing.Point(5, 8);
            this.m_lblVal.Name = "m_lblVal";
            this.m_lblVal.Size = new System.Drawing.Size(73, 68);
            this.m_lblVal.TabIndex = 0;
            this.m_lblVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btnClear
            // 
            this.m_btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClear.Location = new System.Drawing.Point(706, 490);
            this.m_btnClear.Name = "m_btnClear";
            this.m_btnClear.Size = new System.Drawing.Size(150, 23);
            this.m_btnClear.TabIndex = 10;
            this.m_btnClear.Text = "Clear Data";
            this.m_btnClear.UseVisualStyleBackColor = true;
            this.m_btnClear.Click += new System.EventHandler(this.m_btnClear_Click);
            // 
            // m_btnVer
            // 
            this.m_btnVer.Location = new System.Drawing.Point(23, 19);
            this.m_btnVer.Name = "m_btnVer";
            this.m_btnVer.Size = new System.Drawing.Size(139, 23);
            this.m_btnVer.TabIndex = 0;
            this.m_btnVer.Text = "Read Serial Number";
            this.m_btnVer.UseVisualStyleBackColor = true;
            this.m_btnVer.Click += new System.EventHandler(this.m_btnGetVer_Click);
            // 
            // m_btnWriteConfig
            // 
            this.m_btnWriteConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnWriteConfig.Location = new System.Drawing.Point(481, 125);
            this.m_btnWriteConfig.Name = "m_btnWriteConfig";
            this.m_btnWriteConfig.Size = new System.Drawing.Size(133, 23);
            this.m_btnWriteConfig.TabIndex = 5;
            this.m_btnWriteConfig.Text = "Write Settings";
            this.m_btnWriteConfig.UseVisualStyleBackColor = true;
            this.m_btnWriteConfig.Click += new System.EventHandler(this.m_btnSet_Click);
            // 
            // m_btnReadConfig
            // 
            this.m_btnReadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnReadConfig.Location = new System.Drawing.Point(481, 96);
            this.m_btnReadConfig.Name = "m_btnReadConfig";
            this.m_btnReadConfig.Size = new System.Drawing.Size(133, 23);
            this.m_btnReadConfig.TabIndex = 4;
            this.m_btnReadConfig.Text = "Read Settings";
            this.m_btnReadConfig.UseVisualStyleBackColor = true;
            this.m_btnReadConfig.Click += new System.EventHandler(this.m_btnGetSett_Click);
            // 
            // m_lblSN
            // 
            this.m_lblSN.AutoSize = true;
            this.m_lblSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.m_lblSN.Location = new System.Drawing.Point(177, 14);
            this.m_lblSN.Name = "m_lblSN";
            this.m_lblSN.Size = new System.Drawing.Size(83, 29);
            this.m_lblSN.TabIndex = 1;
            this.m_lblSN.Text = "S/N: ?";
            // 
            // m_lblCPM
            // 
            this.m_lblCPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCPM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblCPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblCPM.Location = new System.Drawing.Point(216, 8);
            this.m_lblCPM.Name = "m_lblCPM";
            this.m_lblCPM.Size = new System.Drawing.Size(73, 68);
            this.m_lblCPM.TabIndex = 3;
            this.m_lblCPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_numMaxCPM
            // 
            this.m_numMaxCPM.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_numMaxCPM.Location = new System.Drawing.Point(440, 46);
            this.m_numMaxCPM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numMaxCPM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_numMaxCPM.Name = "m_numMaxCPM";
            this.m_numMaxCPM.Size = new System.Drawing.Size(73, 20);
            this.m_numMaxCPM.TabIndex = 5;
            this.m_numMaxCPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numMaxCPM.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.m_numMaxCPM.ValueChanged += new System.EventHandler(this.m_numMaxCPM_ValueChanged);
            // 
            // m_splitContainerTools
            // 
            this.m_splitContainerTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitContainerTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainerTools.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainerTools.Name = "m_splitContainerTools";
            this.m_splitContainerTools.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainerTools.Panel1
            // 
            this.m_splitContainerTools.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_splitContainerTools.Panel1.Controls.Add(this.m_picRadiationStatus);
            this.m_splitContainerTools.Panel1.Controls.Add(this.m_progressMain);
            // 
            // m_splitContainerTools.Panel2
            // 
            this.m_splitContainerTools.Panel2.Controls.Add(this.label1);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_txtRecords);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblVal);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblCPM);
            this.m_splitContainerTools.Panel2.Controls.Add(this.label22);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblDose);
            this.m_splitContainerTools.Size = new System.Drawing.Size(305, 675);
            this.m_splitContainerTools.SplitterDistance = 235;
            this.m_splitContainerTools.TabIndex = 0;
            // 
            // m_picRadiationStatus
            // 
            this.m_picRadiationStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_picRadiationStatus.BackColor = System.Drawing.SystemColors.Control;
            this.m_picRadiationStatus.Image = global::RadexOneDemo.Properties.Resources.radiation_symbol;
            this.m_picRadiationStatus.Location = new System.Drawing.Point(11, 13);
            this.m_picRadiationStatus.Name = "m_picRadiationStatus";
            this.m_picRadiationStatus.Size = new System.Drawing.Size(249, 206);
            this.m_picRadiationStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_picRadiationStatus.TabIndex = 0;
            this.m_picRadiationStatus.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Records(+) and Warnings(*)";
            // 
            // m_txtRecords
            // 
            this.m_txtRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRecords.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtRecords.Location = new System.Drawing.Point(3, 118);
            this.m_txtRecords.Name = "m_txtRecords";
            this.m_txtRecords.Size = new System.Drawing.Size(297, 310);
            this.m_txtRecords.TabIndex = 1;
            this.m_txtRecords.Text = "";
            this.m_txtRecords.WordWrap = false;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(180, 10);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(30, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "CPM";
            // 
            // m_lblDose
            // 
            this.m_lblDose.AutoSize = true;
            this.m_lblDose.Location = new System.Drawing.Point(84, 10);
            this.m_lblDose.Name = "m_lblDose";
            this.m_lblDose.Size = new System.Drawing.Size(37, 13);
            this.m_lblDose.TabIndex = 1;
            this.m_lblDose.Text = "µSv/h";
            // 
            // m_lblAlertVolume
            // 
            this.m_lblAlertVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAlertVolume.AutoSize = true;
            this.m_lblAlertVolume.Location = new System.Drawing.Point(971, 12);
            this.m_lblAlertVolume.Name = "m_lblAlertVolume";
            this.m_lblAlertVolume.Size = new System.Drawing.Size(33, 13);
            this.m_lblAlertVolume.TabIndex = 7;
            this.m_lblAlertVolume.Text = "100%";
            // 
            // m_trackAlertVolume
            // 
            this.m_trackAlertVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_trackAlertVolume.LargeChange = 100;
            this.m_trackAlertVolume.Location = new System.Drawing.Point(835, 3);
            this.m_trackAlertVolume.Maximum = 1000;
            this.m_trackAlertVolume.Name = "m_trackAlertVolume";
            this.m_trackAlertVolume.Size = new System.Drawing.Size(130, 45);
            this.m_trackAlertVolume.SmallChange = 2;
            this.m_trackAlertVolume.TabIndex = 6;
            this.m_trackAlertVolume.TickFrequency = 100;
            this.m_trackAlertVolume.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.m_trackAlertVolume.Value = 200;
            this.m_trackAlertVolume.ValueChanged += new System.EventHandler(this.m_trackAlertVolume_ValueChanged);
            // 
            // m_lblInterval
            // 
            this.m_lblInterval.AutoSize = true;
            this.m_lblInterval.Location = new System.Drawing.Point(184, 48);
            this.m_lblInterval.Name = "m_lblInterval";
            this.m_lblInterval.Size = new System.Drawing.Size(65, 13);
            this.m_lblInterval.TabIndex = 2;
            this.m_lblInterval.Text = "Interval(sec)";
            // 
            // m_numInterval
            // 
            this.m_numInterval.DecimalPlaces = 1;
            this.m_numInterval.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.m_numInterval.Location = new System.Drawing.Point(251, 46);
            this.m_numInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.m_numInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numInterval.Name = "m_numInterval";
            this.m_numInterval.Size = new System.Drawing.Size(61, 20);
            this.m_numInterval.TabIndex = 3;
            this.m_numInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numInterval.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.m_numInterval.ValueChanged += new System.EventHandler(this.m_numInterval_ValueChanged);
            // 
            // m_lblMaxCPM
            // 
            this.m_lblMaxCPM.AutoSize = true;
            this.m_lblMaxCPM.Location = new System.Drawing.Point(334, 48);
            this.m_lblMaxCPM.Name = "m_lblMaxCPM";
            this.m_lblMaxCPM.Size = new System.Drawing.Size(104, 13);
            this.m_lblMaxCPM.TabIndex = 4;
            this.m_lblMaxCPM.Text = "Alert Threshold CPM";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_lblInterval);
            this.splitContainer2.Panel1.Controls.Add(this.m_numMaxCPM);
            this.splitContainer2.Panel1.Controls.Add(this.m_chart1);
            this.splitContainer2.Panel1.Controls.Add(this.m_numInterval);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkShowLog);
            this.splitContainer2.Panel1.Controls.Add(this.m_lblMaxCPM);
            this.splitContainer2.Panel1.Controls.Add(this.m_lblConnectStatus);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkAutoRequestData);
            this.splitContainer2.Panel1.Controls.Add(this.m_txtStatus);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnClear);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnRequestData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_txtLog);
            this.splitContainer2.Size = new System.Drawing.Size(861, 643);
            this.splitContainer2.SplitterDistance = 520;
            this.splitContainer2.TabIndex = 0;
            // 
            // m_chart1
            // 
            this.m_chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chart1.BorderlineColor = System.Drawing.Color.Empty;
            this.m_chart1.BorderSkin.BorderColor = System.Drawing.Color.Empty;
            this.m_chart1.BorderSkin.PageColor = System.Drawing.SystemColors.Control;
            this.m_chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Sunken;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DodgerBlue;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.DarkOrange;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.m_chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.m_chart1.Legends.Add(legend1);
            this.m_chart1.Location = new System.Drawing.Point(9, 137);
            this.m_chart1.Name = "m_chart1";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.LegendText = "DOSE µSv/h";
            series1.Name = "SeriesDOSE";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.LegendText = "CPM";
            series2.Name = "SeriesCPM";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.m_chart1.Series.Add(series1);
            this.m_chart1.Series.Add(series2);
            this.m_chart1.Size = new System.Drawing.Size(838, 347);
            this.m_chart1.TabIndex = 7;
            this.m_chart1.Text = "chart1";
            // 
            // m_chkShowLog
            // 
            this.m_chkShowLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_chkShowLog.AutoSize = true;
            this.m_chkShowLog.Checked = true;
            this.m_chkShowLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkShowLog.Location = new System.Drawing.Point(3, 496);
            this.m_chkShowLog.Name = "m_chkShowLog";
            this.m_chkShowLog.Size = new System.Drawing.Size(78, 17);
            this.m_chkShowLog.TabIndex = 8;
            this.m_chkShowLog.Text = "Show LOG";
            this.m_chkShowLog.UseVisualStyleBackColor = true;
            // 
            // m_btnDisconnect
            // 
            this.m_btnDisconnect.Location = new System.Drawing.Point(467, 9);
            this.m_btnDisconnect.Name = "m_btnDisconnect";
            this.m_btnDisconnect.Size = new System.Drawing.Size(77, 23);
            this.m_btnDisconnect.TabIndex = 3;
            this.m_btnDisconnect.Text = "Disconnect";
            this.m_btnDisconnect.UseVisualStyleBackColor = true;
            this.m_btnDisconnect.Click += new System.EventHandler(this.m_btnDisconnect_Click);
            // 
            // m_btnConnect
            // 
            this.m_btnConnect.Location = new System.Drawing.Point(384, 9);
            this.m_btnConnect.Name = "m_btnConnect";
            this.m_btnConnect.Size = new System.Drawing.Size(77, 23);
            this.m_btnConnect.TabIndex = 2;
            this.m_btnConnect.Text = "Connect";
            this.m_btnConnect.UseVisualStyleBackColor = true;
            this.m_btnConnect.Click += new System.EventHandler(this.m_btnConnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select COM Port:";
            // 
            // m_btnHistory
            // 
            this.m_btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHistory.Image")));
            this.m_btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnHistory.Location = new System.Drawing.Point(1068, 6);
            this.m_btnHistory.Name = "m_btnHistory";
            this.m_btnHistory.Size = new System.Drawing.Size(103, 27);
            this.m_btnHistory.TabIndex = 8;
            this.m_btnHistory.Text = "&History";
            this.m_btnHistory.UseVisualStyleBackColor = true;
            this.m_btnHistory.Click += new System.EventHandler(this.m_btnHistory_Click);
            // 
            // m_chkAutoConnect
            // 
            this.m_chkAutoConnect.AutoSize = true;
            this.m_chkAutoConnect.Location = new System.Drawing.Point(567, 12);
            this.m_chkAutoConnect.Name = "m_chkAutoConnect";
            this.m_chkAutoConnect.Size = new System.Drawing.Size(131, 17);
            this.m_chkAutoConnect.TabIndex = 4;
            this.m_chkAutoConnect.Text = "Connect Automatically";
            this.m_chkAutoConnect.UseVisualStyleBackColor = true;
            this.m_chkAutoConnect.CheckedChanged += new System.EventHandler(this.m_chkAutoConnect_CheckedChanged);
            // 
            // m_btnTest
            // 
            this.m_btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnTest.Location = new System.Drawing.Point(481, 310);
            this.m_btnTest.Name = "m_btnTest";
            this.m_btnTest.Size = new System.Drawing.Size(133, 23);
            this.m_btnTest.TabIndex = 4;
            this.m_btnTest.Text = "Test Request";
            this.m_btnTest.UseVisualStyleBackColor = true;
            this.m_btnTest.Click += new System.EventHandler(this.m_btnTest_Click);
            // 
            // m_cmbDevices
            // 
            this.m_cmbDevices.BackColor = System.Drawing.SystemColors.Info;
            this.m_cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbDevices.FormattingEnabled = true;
            this.m_cmbDevices.Location = new System.Drawing.Point(107, 9);
            this.m_cmbDevices.Name = "m_cmbDevices";
            this.m_cmbDevices.Size = new System.Drawing.Size(271, 21);
            this.m_cmbDevices.TabIndex = 1;
            this.m_cmbDevices.SelectionChangeCommitted += new System.EventHandler(this.m_cmbDevices_SelectionChangeCommitted);
            // 
            // m_btnResetDose
            // 
            this.m_btnResetDose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnResetDose.Location = new System.Drawing.Point(481, 206);
            this.m_btnResetDose.Name = "m_btnResetDose";
            this.m_btnResetDose.Size = new System.Drawing.Size(133, 23);
            this.m_btnResetDose.TabIndex = 6;
            this.m_btnResetDose.Text = "Reset Dose";
            this.m_btnResetDose.UseVisualStyleBackColor = true;
            this.m_btnResetDose.Click += new System.EventHandler(this.m_btnResetDose_Click);
            // 
            // m_statusStripMain
            // 
            this.m_statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.m_status2,
            this.m_ProgressBarStatus});
            this.m_statusStripMain.Location = new System.Drawing.Point(0, 740);
            this.m_statusStripMain.Name = "m_statusStripMain";
            this.m_statusStripMain.Size = new System.Drawing.Size(1184, 22);
            this.m_statusStripMain.TabIndex = 3;
            this.m_statusStripMain.Text = "statusStrip1";
            // 
            // m_status1
            // 
            this.m_status1.Name = "m_status1";
            this.m_status1.Size = new System.Drawing.Size(39, 17);
            this.m_status1.Text = "Ready";
            // 
            // m_status2
            // 
            this.m_status2.Name = "m_status2";
            this.m_status2.Size = new System.Drawing.Size(16, 17);
            this.m_status2.Text = "...";
            // 
            // m_ProgressBarStatus
            // 
            this.m_ProgressBarStatus.Name = "m_ProgressBarStatus";
            this.m_ProgressBarStatus.Size = new System.Drawing.Size(100, 16);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.connectToolStripMenuItem.Text = "&Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuDeviceConfiguration});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // m_mnuDeviceConfiguration
            // 
            this.m_mnuDeviceConfiguration.Name = "m_mnuDeviceConfiguration";
            this.m_mnuDeviceConfiguration.Size = new System.Drawing.Size(195, 22);
            this.m_mnuDeviceConfiguration.Text = "Device Configuration...";
            this.m_mnuDeviceConfiguration.Click += new System.EventHandler(this.m_mnuDeviceConfiguration_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // m_tabControlAll
            // 
            this.m_tabControlAll.Controls.Add(this.m_tabPage1_Device);
            this.m_tabControlAll.Controls.Add(this.m_tabPage3_About);
            this.m_tabControlAll.Controls.Add(this.m_tabPage2_Settings);
            this.m_tabControlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabControlAll.Location = new System.Drawing.Point(0, 0);
            this.m_tabControlAll.Name = "m_tabControlAll";
            this.m_tabControlAll.SelectedIndex = 0;
            this.m_tabControlAll.Size = new System.Drawing.Size(875, 675);
            this.m_tabControlAll.TabIndex = 0;
            // 
            // m_tabPage1_Device
            // 
            this.m_tabPage1_Device.Controls.Add(this.splitContainer3);
            this.m_tabPage1_Device.Location = new System.Drawing.Point(4, 22);
            this.m_tabPage1_Device.Name = "m_tabPage1_Device";
            this.m_tabPage1_Device.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabPage1_Device.Size = new System.Drawing.Size(867, 649);
            this.m_tabPage1_Device.TabIndex = 0;
            this.m_tabPage1_Device.Text = "Dashboard";
            this.m_tabPage1_Device.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer3.Panel2Collapsed = true;
            this.splitContainer3.Panel2MinSize = 0;
            this.splitContainer3.Size = new System.Drawing.Size(861, 643);
            this.splitContainer3.SplitterDistance = 698;
            this.splitContainer3.TabIndex = 0;
            // 
            // m_tabPage3_About
            // 
            this.m_tabPage3_About.Controls.Add(this.richTextBox1);
            this.m_tabPage3_About.Location = new System.Drawing.Point(4, 22);
            this.m_tabPage3_About.Name = "m_tabPage3_About";
            this.m_tabPage3_About.Size = new System.Drawing.Size(867, 649);
            this.m_tabPage3_About.TabIndex = 3;
            this.m_tabPage3_About.Text = "About Radiation";
            this.m_tabPage3_About.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(184, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(542, 602);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // m_tabPage2_Settings
            // 
            this.m_tabPage2_Settings.Controls.Add(this.m_btnReadConfig);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnDeviceConfig);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnWriteConfig);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnResetDose);
            this.m_tabPage2_Settings.Controls.Add(this.m_lblSN);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnTest);
            this.m_tabPage2_Settings.Controls.Add(this.propertyGrid1);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnVer);
            this.m_tabPage2_Settings.Location = new System.Drawing.Point(4, 22);
            this.m_tabPage2_Settings.Name = "m_tabPage2_Settings";
            this.m_tabPage2_Settings.Size = new System.Drawing.Size(867, 649);
            this.m_tabPage2_Settings.TabIndex = 2;
            this.m_tabPage2_Settings.Text = "Device Configuration";
            this.m_tabPage2_Settings.UseVisualStyleBackColor = true;
            // 
            // m_btnDeviceConfig
            // 
            this.m_btnDeviceConfig.Location = new System.Drawing.Point(23, 57);
            this.m_btnDeviceConfig.Name = "m_btnDeviceConfig";
            this.m_btnDeviceConfig.Size = new System.Drawing.Size(139, 23);
            this.m_btnDeviceConfig.TabIndex = 8;
            this.m_btnDeviceConfig.Text = "Device Configuration...";
            this.m_btnDeviceConfig.UseVisualStyleBackColor = true;
            this.m_btnDeviceConfig.Click += new System.EventHandler(this.m_mnuDeviceConfiguration_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(23, 96);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(439, 238);
            this.propertyGrid1.TabIndex = 3;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // m_splitMain
            // 
            this.m_splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitMain.Location = new System.Drawing.Point(0, 65);
            this.m_splitMain.Name = "m_splitMain";
            // 
            // m_splitMain.Panel1
            // 
            this.m_splitMain.Panel1.Controls.Add(this.m_splitContainerTools);
            // 
            // m_splitMain.Panel2
            // 
            this.m_splitMain.Panel2.Controls.Add(this.m_tabControlAll);
            this.m_splitMain.Size = new System.Drawing.Size(1184, 675);
            this.m_splitMain.SplitterDistance = 305;
            this.m_splitMain.TabIndex = 2;
            // 
            // m_pnlTools
            // 
            this.m_pnlTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlTools.Controls.Add(this.m_trackAlertVolume);
            this.m_pnlTools.Controls.Add(this.m_lblAlertVolume);
            this.m_pnlTools.Controls.Add(this.label4);
            this.m_pnlTools.Controls.Add(this.m_cmbDevices);
            this.m_pnlTools.Controls.Add(this.m_chkAutoConnect);
            this.m_pnlTools.Controls.Add(this.label3);
            this.m_pnlTools.Controls.Add(this.m_btnConnect);
            this.m_pnlTools.Controls.Add(this.m_btnDisconnect);
            this.m_pnlTools.Controls.Add(this.m_btnHistory);
            this.m_pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTools.Location = new System.Drawing.Point(0, 24);
            this.m_pnlTools.Name = "m_pnlTools";
            this.m_pnlTools.Size = new System.Drawing.Size(1184, 41);
            this.m_pnlTools.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(760, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Alert Volume:";
            // 
            // m_progressMain
            // 
            this.m_progressMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progressMain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_progressMain.Location = new System.Drawing.Point(276, 13);
            this.m_progressMain.Name = "m_progressMain";
            this.m_progressMain.Size = new System.Drawing.Size(13, 206);
            this.m_progressMain.TabIndex = 0;
            this.m_progressMain.Value = 33;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.m_splitMain);
            this.Controls.Add(this.m_pnlTools);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.m_statusStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radex Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_numMaxCPM)).EndInit();
            this.m_splitContainerTools.Panel1.ResumeLayout(false);
            this.m_splitContainerTools.Panel2.ResumeLayout(false);
            this.m_splitContainerTools.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerTools)).EndInit();
            this.m_splitContainerTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picRadiationStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackAlertVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numInterval)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).EndInit();
            this.m_statusStripMain.ResumeLayout(false);
            this.m_statusStripMain.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.m_tabControlAll.ResumeLayout(false);
            this.m_tabPage1_Device.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.m_tabPage3_About.ResumeLayout(false);
            this.m_tabPage2_Settings.ResumeLayout(false);
            this.m_tabPage2_Settings.PerformLayout();
            this.m_splitMain.Panel1.ResumeLayout(false);
            this.m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).EndInit();
            this.m_splitMain.ResumeLayout(false);
            this.m_pnlTools.ResumeLayout(false);
            this.m_pnlTools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox m_txtLog;
        private System.Windows.Forms.Button m_btnRequestData;
        private System.Windows.Forms.Label m_lblConnectStatus;
        private System.Windows.Forms.CheckBox m_chkAutoRequestData;
        private System.Windows.Forms.TextBox m_txtStatus;
        private System.Windows.Forms.Label m_lblVal;
        private VerticalProgressBar m_progressMain;
        private System.Windows.Forms.Button m_btnClear;
        private System.Windows.Forms.Button m_btnVer;
        private System.Windows.Forms.Button m_btnWriteConfig;
        private System.Windows.Forms.Button m_btnReadConfig;
        private System.Windows.Forms.Label m_lblSN;
        private System.Windows.Forms.Label m_lblCPM;
        private System.Windows.Forms.NumericUpDown m_numMaxCPM;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox m_picRadiationStatus;
        private System.Windows.Forms.Button m_btnResetDose;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label m_lblDose;
        private System.Windows.Forms.Label m_lblInterval;
        private System.Windows.Forms.NumericUpDown m_numInterval;
        private System.Windows.Forms.Label m_lblMaxCPM;
        private System.Windows.Forms.CheckBox m_chkShowLog;
        private System.Windows.Forms.StatusStrip m_statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel m_status1;
        private System.Windows.Forms.ToolStripStatusLabel m_status2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar m_ProgressBarStatus;
        private System.Windows.Forms.ComboBox m_cmbDevices;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_chart1;
        private System.Windows.Forms.Button m_btnTest;
        private System.Windows.Forms.CheckBox m_chkAutoConnect;
        private System.Windows.Forms.SplitContainer m_splitContainerTools;
        private System.Windows.Forms.RichTextBox m_txtRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnHistory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar m_trackAlertVolume;
        private System.Windows.Forms.Label m_lblAlertVolume;
        private System.Windows.Forms.Button m_btnConnect;
        private System.Windows.Forms.Button m_btnDisconnect;
        private System.Windows.Forms.TabControl m_tabControlAll;
        private System.Windows.Forms.TabPage m_tabPage1_Device;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabPage m_tabPage2_Settings;
        private System.Windows.Forms.SplitContainer m_splitMain;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Panel m_pnlTools;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem m_mnuDeviceConfiguration;
        private System.Windows.Forms.Button m_btnDeviceConfig;
        private System.Windows.Forms.TabPage m_tabPage3_About;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

