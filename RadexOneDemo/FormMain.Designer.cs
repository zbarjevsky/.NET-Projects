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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
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
            this.m_chkAutoUpdateLog = new System.Windows.Forms.CheckBox();
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
            this.m_mnuExit1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuDeviceConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tabControlAll = new System.Windows.Forms.TabControl();
            this.m_tabPage1_Device = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.m_tabPage3_About = new System.Windows.Forms.TabPage();
            this.m_chkUseConverter = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.m_tabPage2_Settings = new System.Windows.Forms.TabPage();
            this.m_pnlConfig = new System.Windows.Forms.Panel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.m_btnDeviceConfig = new System.Windows.Forms.Button();
            this.m_splitMain = new System.Windows.Forms.SplitContainer();
            this.m_pnlTools = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.m_notifyIconSysTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.m_ctxMenuSysTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_pnlRadiationStatus = new System.Windows.Forms.Panel();
            this.m_progressMain = new RadexOneDemo.VerticalProgressBar();
            this.m_chart1 = new RadexOneDemo.RadiationGraphControl();
            this.m_listLog = new RadexOneDemo.RadiationLogListView();
            this.radiationConverterControl1 = new RadexOneDemo.RadiationConverterControl();
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
            this.m_statusStripMain.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.m_tabControlAll.SuspendLayout();
            this.m_tabPage1_Device.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.m_tabPage3_About.SuspendLayout();
            this.m_tabPage2_Settings.SuspendLayout();
            this.m_pnlConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).BeginInit();
            this.m_splitMain.Panel1.SuspendLayout();
            this.m_splitMain.Panel2.SuspendLayout();
            this.m_splitMain.SuspendLayout();
            this.m_pnlTools.SuspendLayout();
            this.m_ctxMenuSysTray.SuspendLayout();
            this.m_pnlRadiationStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnRequestData
            // 
            this.m_btnRequestData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRequestData.Location = new System.Drawing.Point(734, 635);
            this.m_btnRequestData.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnRequestData.Name = "m_btnRequestData";
            this.m_btnRequestData.Size = new System.Drawing.Size(200, 28);
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
            this.m_lblConnectStatus.Location = new System.Drawing.Point(5, 2);
            this.m_lblConnectStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblConnectStatus.Name = "m_lblConnectStatus";
            this.m_lblConnectStatus.Size = new System.Drawing.Size(42, 35);
            this.m_lblConnectStatus.TabIndex = 0;
            this.m_lblConnectStatus.Text = "...";
            // 
            // m_chkAutoRequestData
            // 
            this.m_chkAutoRequestData.AutoSize = true;
            this.m_chkAutoRequestData.Location = new System.Drawing.Point(12, 58);
            this.m_chkAutoRequestData.Margin = new System.Windows.Forms.Padding(4);
            this.m_chkAutoRequestData.Name = "m_chkAutoRequestData";
            this.m_chkAutoRequestData.Size = new System.Drawing.Size(204, 21);
            this.m_chkAutoRequestData.TabIndex = 1;
            this.m_chkAutoRequestData.Text = "Request Data Automatically";
            this.m_chkAutoRequestData.UseVisualStyleBackColor = true;
            this.m_chkAutoRequestData.CheckedChanged += new System.EventHandler(this.m_chkAuto_CheckedChanged);
            // 
            // m_txtStatus
            // 
            this.m_txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtStatus.Location = new System.Drawing.Point(12, 92);
            this.m_txtStatus.Margin = new System.Windows.Forms.Padding(4);
            this.m_txtStatus.Multiline = true;
            this.m_txtStatus.Name = "m_txtStatus";
            this.m_txtStatus.ReadOnly = true;
            this.m_txtStatus.Size = new System.Drawing.Size(1125, 61);
            this.m_txtStatus.TabIndex = 6;
            // 
            // m_lblVal
            // 
            this.m_lblVal.BackColor = System.Drawing.Color.Chartreuse;
            this.m_lblVal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblVal.Location = new System.Drawing.Point(7, 10);
            this.m_lblVal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblVal.Name = "m_lblVal";
            this.m_lblVal.Size = new System.Drawing.Size(97, 84);
            this.m_lblVal.TabIndex = 0;
            this.m_lblVal.Text = "0.0";
            this.m_lblVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btnClear
            // 
            this.m_btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClear.Location = new System.Drawing.Point(938, 635);
            this.m_btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnClear.Name = "m_btnClear";
            this.m_btnClear.Size = new System.Drawing.Size(200, 28);
            this.m_btnClear.TabIndex = 10;
            this.m_btnClear.Text = "Clear Data";
            this.m_btnClear.UseVisualStyleBackColor = true;
            this.m_btnClear.Click += new System.EventHandler(this.m_btnClear_Click);
            // 
            // m_btnVer
            // 
            this.m_btnVer.Location = new System.Drawing.Point(31, 23);
            this.m_btnVer.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnVer.Name = "m_btnVer";
            this.m_btnVer.Size = new System.Drawing.Size(185, 28);
            this.m_btnVer.TabIndex = 0;
            this.m_btnVer.Text = "Read Serial Number";
            this.m_btnVer.UseVisualStyleBackColor = true;
            this.m_btnVer.Click += new System.EventHandler(this.m_btnGetVer_Click);
            // 
            // m_btnWriteConfig
            // 
            this.m_btnWriteConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnWriteConfig.Location = new System.Drawing.Point(642, 154);
            this.m_btnWriteConfig.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnWriteConfig.Name = "m_btnWriteConfig";
            this.m_btnWriteConfig.Size = new System.Drawing.Size(177, 28);
            this.m_btnWriteConfig.TabIndex = 5;
            this.m_btnWriteConfig.Text = "Write Settings";
            this.m_btnWriteConfig.UseVisualStyleBackColor = true;
            this.m_btnWriteConfig.Click += new System.EventHandler(this.m_btnSet_Click);
            // 
            // m_btnReadConfig
            // 
            this.m_btnReadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnReadConfig.Location = new System.Drawing.Point(642, 118);
            this.m_btnReadConfig.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnReadConfig.Name = "m_btnReadConfig";
            this.m_btnReadConfig.Size = new System.Drawing.Size(177, 28);
            this.m_btnReadConfig.TabIndex = 4;
            this.m_btnReadConfig.Text = "Read Settings";
            this.m_btnReadConfig.UseVisualStyleBackColor = true;
            this.m_btnReadConfig.Click += new System.EventHandler(this.m_btnGetSett_Click);
            // 
            // m_lblSN
            // 
            this.m_lblSN.AutoSize = true;
            this.m_lblSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.m_lblSN.Location = new System.Drawing.Point(236, 17);
            this.m_lblSN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblSN.Name = "m_lblSN";
            this.m_lblSN.Size = new System.Drawing.Size(101, 35);
            this.m_lblSN.TabIndex = 1;
            this.m_lblSN.Text = "S/N: ?";
            // 
            // m_lblCPM
            // 
            this.m_lblCPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCPM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblCPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblCPM.Location = new System.Drawing.Point(288, 10);
            this.m_lblCPM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblCPM.Name = "m_lblCPM";
            this.m_lblCPM.Size = new System.Drawing.Size(97, 84);
            this.m_lblCPM.TabIndex = 3;
            this.m_lblCPM.Text = "0";
            this.m_lblCPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_numMaxCPM
            // 
            this.m_numMaxCPM.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_numMaxCPM.Location = new System.Drawing.Point(600, 57);
            this.m_numMaxCPM.Margin = new System.Windows.Forms.Padding(4);
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
            this.m_numMaxCPM.Size = new System.Drawing.Size(97, 22);
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
            this.m_splitContainerTools.Margin = new System.Windows.Forms.Padding(4);
            this.m_splitContainerTools.Name = "m_splitContainerTools";
            this.m_splitContainerTools.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainerTools.Panel1
            // 
            this.m_splitContainerTools.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_splitContainerTools.Panel1.Controls.Add(this.m_pnlRadiationStatus);
            // 
            // m_splitContainerTools.Panel2
            // 
            this.m_splitContainerTools.Panel2.Controls.Add(this.label1);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_txtRecords);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblVal);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblCPM);
            this.m_splitContainerTools.Panel2.Controls.Add(this.label22);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblDose);
            this.m_splitContainerTools.Size = new System.Drawing.Size(406, 834);
            this.m_splitContainerTools.SplitterDistance = 290;
            this.m_splitContainerTools.SplitterWidth = 5;
            this.m_splitContainerTools.TabIndex = 0;
            // 
            // m_picRadiationStatus
            // 
            this.m_picRadiationStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_picRadiationStatus.BackColor = System.Drawing.SystemColors.Control;
            this.m_picRadiationStatus.Image = global::RadexOneDemo.Properties.Resources.radiation_symbol;
            this.m_picRadiationStatus.Location = new System.Drawing.Point(15, 16);
            this.m_picRadiationStatus.Margin = new System.Windows.Forms.Padding(4);
            this.m_picRadiationStatus.Name = "m_picRadiationStatus";
            this.m_picRadiationStatus.Size = new System.Drawing.Size(331, 255);
            this.m_picRadiationStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_picRadiationStatus.TabIndex = 0;
            this.m_picRadiationStatus.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 113);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Records(+) and Warnings(*)";
            // 
            // m_txtRecords
            // 
            this.m_txtRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRecords.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtRecords.Location = new System.Drawing.Point(4, 145);
            this.m_txtRecords.Margin = new System.Windows.Forms.Padding(4);
            this.m_txtRecords.Name = "m_txtRecords";
            this.m_txtRecords.Size = new System.Drawing.Size(395, 383);
            this.m_txtRecords.TabIndex = 1;
            this.m_txtRecords.Text = "";
            this.m_txtRecords.WordWrap = false;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(240, 12);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(37, 17);
            this.label22.TabIndex = 2;
            this.label22.Text = "CPM";
            // 
            // m_lblDose
            // 
            this.m_lblDose.AutoSize = true;
            this.m_lblDose.Location = new System.Drawing.Point(112, 12);
            this.m_lblDose.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblDose.Name = "m_lblDose";
            this.m_lblDose.Size = new System.Drawing.Size(44, 17);
            this.m_lblDose.TabIndex = 1;
            this.m_lblDose.Text = "µSv/h";
            // 
            // m_lblAlertVolume
            // 
            this.m_lblAlertVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAlertVolume.AutoSize = true;
            this.m_lblAlertVolume.Location = new System.Drawing.Point(1296, 15);
            this.m_lblAlertVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblAlertVolume.Name = "m_lblAlertVolume";
            this.m_lblAlertVolume.Size = new System.Drawing.Size(44, 17);
            this.m_lblAlertVolume.TabIndex = 7;
            this.m_lblAlertVolume.Text = "100%";
            // 
            // m_trackAlertVolume
            // 
            this.m_trackAlertVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_trackAlertVolume.LargeChange = 100;
            this.m_trackAlertVolume.Location = new System.Drawing.Point(1113, 4);
            this.m_trackAlertVolume.Margin = new System.Windows.Forms.Padding(4);
            this.m_trackAlertVolume.Maximum = 1000;
            this.m_trackAlertVolume.Name = "m_trackAlertVolume";
            this.m_trackAlertVolume.Size = new System.Drawing.Size(174, 56);
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
            this.m_lblInterval.Location = new System.Drawing.Point(245, 59);
            this.m_lblInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblInterval.Name = "m_lblInterval";
            this.m_lblInterval.Size = new System.Drawing.Size(86, 17);
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
            this.m_numInterval.Location = new System.Drawing.Point(335, 57);
            this.m_numInterval.Margin = new System.Windows.Forms.Padding(4);
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
            this.m_numInterval.Size = new System.Drawing.Size(81, 22);
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
            this.m_lblMaxCPM.Location = new System.Drawing.Point(445, 59);
            this.m_lblMaxCPM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblMaxCPM.Name = "m_lblMaxCPM";
            this.m_lblMaxCPM.Size = new System.Drawing.Size(152, 17);
            this.m_lblMaxCPM.TabIndex = 4;
            this.m_lblMaxCPM.Text = "Alert (CPM Threshold):";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.m_lblInterval);
            this.splitContainer2.Panel1.Controls.Add(this.m_numMaxCPM);
            this.splitContainer2.Panel1.Controls.Add(this.m_chart1);
            this.splitContainer2.Panel1.Controls.Add(this.m_numInterval);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkAutoUpdateLog);
            this.splitContainer2.Panel1.Controls.Add(this.m_lblMaxCPM);
            this.splitContainer2.Panel1.Controls.Add(this.m_lblConnectStatus);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkAutoRequestData);
            this.splitContainer2.Panel1.Controls.Add(this.m_txtStatus);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnClear);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnRequestData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_listLog);
            this.splitContainer2.Size = new System.Drawing.Size(1152, 797);
            this.splitContainer2.SplitterDistance = 672;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // m_chkAutoUpdateLog
            // 
            this.m_chkAutoUpdateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_chkAutoUpdateLog.AutoSize = true;
            this.m_chkAutoUpdateLog.Checked = true;
            this.m_chkAutoUpdateLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkAutoUpdateLog.Location = new System.Drawing.Point(12, 643);
            this.m_chkAutoUpdateLog.Margin = new System.Windows.Forms.Padding(4);
            this.m_chkAutoUpdateLog.Name = "m_chkAutoUpdateLog";
            this.m_chkAutoUpdateLog.Size = new System.Drawing.Size(110, 21);
            this.m_chkAutoUpdateLog.TabIndex = 8;
            this.m_chkAutoUpdateLog.Text = "Update LOG";
            this.m_chkAutoUpdateLog.UseVisualStyleBackColor = true;
            // 
            // m_btnDisconnect
            // 
            this.m_btnDisconnect.Location = new System.Drawing.Point(623, 11);
            this.m_btnDisconnect.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnDisconnect.Name = "m_btnDisconnect";
            this.m_btnDisconnect.Size = new System.Drawing.Size(103, 28);
            this.m_btnDisconnect.TabIndex = 3;
            this.m_btnDisconnect.Text = "Disconnect";
            this.m_btnDisconnect.UseVisualStyleBackColor = true;
            this.m_btnDisconnect.Click += new System.EventHandler(this.m_btnDisconnect_Click);
            // 
            // m_btnConnect
            // 
            this.m_btnConnect.Location = new System.Drawing.Point(512, 11);
            this.m_btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnConnect.Name = "m_btnConnect";
            this.m_btnConnect.Size = new System.Drawing.Size(103, 28);
            this.m_btnConnect.TabIndex = 2;
            this.m_btnConnect.Text = "Connect";
            this.m_btnConnect.UseVisualStyleBackColor = true;
            this.m_btnConnect.Click += new System.EventHandler(this.m_btnConnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select COM Port:";
            // 
            // m_btnHistory
            // 
            this.m_btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHistory.Image")));
            this.m_btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnHistory.Location = new System.Drawing.Point(1425, 7);
            this.m_btnHistory.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnHistory.Name = "m_btnHistory";
            this.m_btnHistory.Size = new System.Drawing.Size(137, 33);
            this.m_btnHistory.TabIndex = 8;
            this.m_btnHistory.Text = "&History";
            this.m_btnHistory.UseVisualStyleBackColor = true;
            this.m_btnHistory.Click += new System.EventHandler(this.m_btnHistory_Click);
            // 
            // m_chkAutoConnect
            // 
            this.m_chkAutoConnect.AutoSize = true;
            this.m_chkAutoConnect.Location = new System.Drawing.Point(756, 15);
            this.m_chkAutoConnect.Margin = new System.Windows.Forms.Padding(4);
            this.m_chkAutoConnect.Name = "m_chkAutoConnect";
            this.m_chkAutoConnect.Size = new System.Drawing.Size(169, 21);
            this.m_chkAutoConnect.TabIndex = 4;
            this.m_chkAutoConnect.Text = "Connect Automatically";
            this.m_chkAutoConnect.UseVisualStyleBackColor = true;
            this.m_chkAutoConnect.CheckedChanged += new System.EventHandler(this.m_chkAutoConnect_CheckedChanged);
            // 
            // m_btnTest
            // 
            this.m_btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnTest.Location = new System.Drawing.Point(642, 382);
            this.m_btnTest.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnTest.Name = "m_btnTest";
            this.m_btnTest.Size = new System.Drawing.Size(177, 28);
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
            this.m_cmbDevices.Location = new System.Drawing.Point(143, 11);
            this.m_cmbDevices.Margin = new System.Windows.Forms.Padding(4);
            this.m_cmbDevices.Name = "m_cmbDevices";
            this.m_cmbDevices.Size = new System.Drawing.Size(360, 24);
            this.m_cmbDevices.TabIndex = 1;
            this.m_cmbDevices.SelectionChangeCommitted += new System.EventHandler(this.m_cmbDevices_SelectionChangeCommitted);
            // 
            // m_btnResetDose
            // 
            this.m_btnResetDose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnResetDose.Location = new System.Drawing.Point(642, 254);
            this.m_btnResetDose.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnResetDose.Name = "m_btnResetDose";
            this.m_btnResetDose.Size = new System.Drawing.Size(177, 28);
            this.m_btnResetDose.TabIndex = 6;
            this.m_btnResetDose.Text = "Reset Dose";
            this.m_btnResetDose.UseVisualStyleBackColor = true;
            this.m_btnResetDose.Click += new System.EventHandler(this.m_btnResetDose_Click);
            // 
            // m_statusStripMain
            // 
            this.m_statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.m_status2,
            this.m_ProgressBarStatus});
            this.m_statusStripMain.Location = new System.Drawing.Point(0, 912);
            this.m_statusStripMain.Name = "m_statusStripMain";
            this.m_statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.m_statusStripMain.Size = new System.Drawing.Size(1579, 26);
            this.m_statusStripMain.TabIndex = 3;
            this.m_statusStripMain.Text = "statusStrip1";
            // 
            // m_status1
            // 
            this.m_status1.Name = "m_status1";
            this.m_status1.Size = new System.Drawing.Size(50, 21);
            this.m_status1.Text = "Ready";
            // 
            // m_status2
            // 
            this.m_status2.Name = "m_status2";
            this.m_status2.Size = new System.Drawing.Size(18, 21);
            this.m_status2.Text = "...";
            // 
            // m_ProgressBarStatus
            // 
            this.m_ProgressBarStatus.Name = "m_ProgressBarStatus";
            this.m_ProgressBarStatus.Size = new System.Drawing.Size(133, 20);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1579, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.m_mnuExit1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.connectToolStripMenuItem.Text = "&Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // m_mnuExit1
            // 
            this.m_mnuExit1.Name = "m_mnuExit1";
            this.m_mnuExit1.Size = new System.Drawing.Size(138, 26);
            this.m_mnuExit1.Text = "E&xit";
            this.m_mnuExit1.Click += new System.EventHandler(this.m_mnuExit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuDeviceConfiguration});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // m_mnuDeviceConfiguration
            // 
            this.m_mnuDeviceConfiguration.Name = "m_mnuDeviceConfiguration";
            this.m_mnuDeviceConfiguration.Size = new System.Drawing.Size(233, 26);
            this.m_mnuDeviceConfiguration.Text = "Device Configuration...";
            this.m_mnuDeviceConfiguration.Click += new System.EventHandler(this.m_mnuDeviceConfiguration_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
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
            this.m_tabControlAll.Margin = new System.Windows.Forms.Padding(4);
            this.m_tabControlAll.Name = "m_tabControlAll";
            this.m_tabControlAll.SelectedIndex = 0;
            this.m_tabControlAll.Size = new System.Drawing.Size(1168, 834);
            this.m_tabControlAll.TabIndex = 0;
            // 
            // m_tabPage1_Device
            // 
            this.m_tabPage1_Device.BackColor = System.Drawing.SystemColors.Control;
            this.m_tabPage1_Device.Controls.Add(this.splitContainer3);
            this.m_tabPage1_Device.Location = new System.Drawing.Point(4, 25);
            this.m_tabPage1_Device.Margin = new System.Windows.Forms.Padding(4);
            this.m_tabPage1_Device.Name = "m_tabPage1_Device";
            this.m_tabPage1_Device.Padding = new System.Windows.Forms.Padding(4);
            this.m_tabPage1_Device.Size = new System.Drawing.Size(1160, 805);
            this.m_tabPage1_Device.TabIndex = 0;
            this.m_tabPage1_Device.Text = "Dashboard";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(4, 4);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer3.Panel2Collapsed = true;
            this.splitContainer3.Panel2MinSize = 0;
            this.splitContainer3.Size = new System.Drawing.Size(1152, 797);
            this.splitContainer3.SplitterDistance = 698;
            this.splitContainer3.SplitterWidth = 5;
            this.splitContainer3.TabIndex = 0;
            // 
            // m_tabPage3_About
            // 
            this.m_tabPage3_About.BackColor = System.Drawing.SystemColors.Control;
            this.m_tabPage3_About.Controls.Add(this.radiationConverterControl1);
            this.m_tabPage3_About.Controls.Add(this.m_chkUseConverter);
            this.m_tabPage3_About.Controls.Add(this.richTextBox1);
            this.m_tabPage3_About.Location = new System.Drawing.Point(4, 25);
            this.m_tabPage3_About.Margin = new System.Windows.Forms.Padding(4);
            this.m_tabPage3_About.Name = "m_tabPage3_About";
            this.m_tabPage3_About.Size = new System.Drawing.Size(1160, 805);
            this.m_tabPage3_About.TabIndex = 3;
            this.m_tabPage3_About.Text = "About Radiation";
            // 
            // m_chkUseConverter
            // 
            this.m_chkUseConverter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkUseConverter.AutoSize = true;
            this.m_chkUseConverter.Location = new System.Drawing.Point(730, 610);
            this.m_chkUseConverter.Margin = new System.Windows.Forms.Padding(4);
            this.m_chkUseConverter.Name = "m_chkUseConverter";
            this.m_chkUseConverter.Size = new System.Drawing.Size(180, 21);
            this.m_chkUseConverter.TabIndex = 2;
            this.m_chkUseConverter.Text = "Use Current Rate Value";
            this.m_chkUseConverter.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Location = new System.Drawing.Point(21, 20);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(657, 761);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // m_tabPage2_Settings
            // 
            this.m_tabPage2_Settings.BackColor = System.Drawing.SystemColors.Control;
            this.m_tabPage2_Settings.Controls.Add(this.m_pnlConfig);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnReadConfig);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnDeviceConfig);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnWriteConfig);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnResetDose);
            this.m_tabPage2_Settings.Controls.Add(this.m_lblSN);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnTest);
            this.m_tabPage2_Settings.Controls.Add(this.m_btnVer);
            this.m_tabPage2_Settings.Location = new System.Drawing.Point(4, 25);
            this.m_tabPage2_Settings.Margin = new System.Windows.Forms.Padding(4);
            this.m_tabPage2_Settings.Name = "m_tabPage2_Settings";
            this.m_tabPage2_Settings.Size = new System.Drawing.Size(1160, 805);
            this.m_tabPage2_Settings.TabIndex = 2;
            this.m_tabPage2_Settings.Text = "Device Configuration";
            // 
            // m_pnlConfig
            // 
            this.m_pnlConfig.BackColor = System.Drawing.Color.Silver;
            this.m_pnlConfig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlConfig.Controls.Add(this.propertyGrid1);
            this.m_pnlConfig.Location = new System.Drawing.Point(27, 118);
            this.m_pnlConfig.Margin = new System.Windows.Forms.Padding(4);
            this.m_pnlConfig.Name = "m_pnlConfig";
            this.m_pnlConfig.Size = new System.Drawing.Size(593, 291);
            this.m_pnlConfig.TabIndex = 9;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(8, 6);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(573, 274);
            this.propertyGrid1.TabIndex = 3;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // m_btnDeviceConfig
            // 
            this.m_btnDeviceConfig.Location = new System.Drawing.Point(31, 70);
            this.m_btnDeviceConfig.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnDeviceConfig.Name = "m_btnDeviceConfig";
            this.m_btnDeviceConfig.Size = new System.Drawing.Size(185, 28);
            this.m_btnDeviceConfig.TabIndex = 8;
            this.m_btnDeviceConfig.Text = "Device Configuration...";
            this.m_btnDeviceConfig.UseVisualStyleBackColor = true;
            this.m_btnDeviceConfig.Click += new System.EventHandler(this.m_mnuDeviceConfiguration_Click);
            // 
            // m_splitMain
            // 
            this.m_splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitMain.Location = new System.Drawing.Point(0, 78);
            this.m_splitMain.Margin = new System.Windows.Forms.Padding(4);
            this.m_splitMain.Name = "m_splitMain";
            // 
            // m_splitMain.Panel1
            // 
            this.m_splitMain.Panel1.Controls.Add(this.m_splitContainerTools);
            // 
            // m_splitMain.Panel2
            // 
            this.m_splitMain.Panel2.Controls.Add(this.m_tabControlAll);
            this.m_splitMain.Size = new System.Drawing.Size(1579, 834);
            this.m_splitMain.SplitterDistance = 406;
            this.m_splitMain.SplitterWidth = 5;
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
            this.m_pnlTools.Location = new System.Drawing.Point(0, 28);
            this.m_pnlTools.Margin = new System.Windows.Forms.Padding(4);
            this.m_pnlTools.Name = "m_pnlTools";
            this.m_pnlTools.Size = new System.Drawing.Size(1579, 50);
            this.m_pnlTools.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1013, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Alert Volume:";
            // 
            // m_notifyIconSysTray
            // 
            this.m_notifyIconSysTray.BalloonTipText = "aa";
            this.m_notifyIconSysTray.BalloonTipTitle = "bb";
            this.m_notifyIconSysTray.ContextMenuStrip = this.m_ctxMenuSysTray;
            this.m_notifyIconSysTray.Icon = ((System.Drawing.Icon)(resources.GetObject("m_notifyIconSysTray.Icon")));
            this.m_notifyIconSysTray.Text = "Radex Demo";
            this.m_notifyIconSysTray.Visible = true;
            // 
            // m_ctxMenuSysTray
            // 
            this.m_ctxMenuSysTray.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_ctxMenuSysTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuShow,
            this.toolStripMenuItem1,
            this.m_mnuExit});
            this.m_ctxMenuSysTray.Name = "m_ctxMenuSysTray";
            this.m_ctxMenuSysTray.Size = new System.Drawing.Size(115, 58);
            // 
            // m_mnuShow
            // 
            this.m_mnuShow.Name = "m_mnuShow";
            this.m_mnuShow.Size = new System.Drawing.Size(114, 24);
            this.m_mnuShow.Text = "&Show";
            this.m_mnuShow.Click += new System.EventHandler(this.m_mnuShow_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(111, 6);
            // 
            // m_mnuExit
            // 
            this.m_mnuExit.Name = "m_mnuExit";
            this.m_mnuExit.Size = new System.Drawing.Size(114, 24);
            this.m_mnuExit.Text = "E&xit";
            this.m_mnuExit.Click += new System.EventHandler(this.m_mnuExit_Click);
            // 
            // m_pnlRadiationStatus
            // 
            this.m_pnlRadiationStatus.Controls.Add(this.m_progressMain);
            this.m_pnlRadiationStatus.Controls.Add(this.m_picRadiationStatus);
            this.m_pnlRadiationStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlRadiationStatus.Location = new System.Drawing.Point(0, 0);
            this.m_pnlRadiationStatus.Name = "m_pnlRadiationStatus";
            this.m_pnlRadiationStatus.Size = new System.Drawing.Size(404, 288);
            this.m_pnlRadiationStatus.TabIndex = 1;
            // 
            // m_progressMain
            // 
            this.m_progressMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progressMain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_progressMain.Location = new System.Drawing.Point(368, 16);
            this.m_progressMain.Margin = new System.Windows.Forms.Padding(4);
            this.m_progressMain.Name = "m_progressMain";
            this.m_progressMain.Size = new System.Drawing.Size(17, 255);
            this.m_progressMain.TabIndex = 0;
            this.m_progressMain.Value = 33;
            // 
            // m_chart1
            // 
            this.m_chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chart1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_chart1.Location = new System.Drawing.Point(12, 161);
            this.m_chart1.Margin = new System.Windows.Forms.Padding(5);
            this.m_chart1.MinimumSize = new System.Drawing.Size(640, 280);
            this.m_chart1.Name = "m_chart1";
            this.m_chart1.ScrollPosition = 10;
            this.m_chart1.Series3Color = System.Drawing.Color.DarkOrange;
            this.m_chart1.Series3LegendText = "CPM Threshold";
            this.m_chart1.Size = new System.Drawing.Size(1126, 466);
            this.m_chart1.TabIndex = 7;
            // 
            // m_listLog
            // 
            this.m_listLog.BackColor = System.Drawing.SystemColors.Info;
            this.m_listLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listLog.FullRowSelect = true;
            this.m_listLog.GridLines = true;
            this.m_listLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_listLog.HideSelection = false;
            this.m_listLog.LabelEdit = true;
            this.m_listLog.Location = new System.Drawing.Point(0, 0);
            this.m_listLog.Margin = new System.Windows.Forms.Padding(4);
            this.m_listLog.Name = "m_listLog";
            this.m_listLog.Size = new System.Drawing.Size(1150, 118);
            this.m_listLog.TabIndex = 0;
            this.m_listLog.UseCompatibleStateImageBehavior = false;
            this.m_listLog.View = System.Windows.Forms.View.Details;
            this.m_listLog.VirtualMode = true;
            // 
            // radiationConverterControl1
            // 
            this.radiationConverterControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radiationConverterControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.radiationConverterControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.radiationConverterControl1.Location = new System.Drawing.Point(540, 654);
            this.radiationConverterControl1.Margin = new System.Windows.Forms.Padding(9);
            this.radiationConverterControl1.Name = "radiationConverterControl1";
            this.radiationConverterControl1.Size = new System.Drawing.Size(600, 77);
            this.radiationConverterControl1.TabIndex = 1;
            this.radiationConverterControl1.ValueFrom = 0D;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1579, 938);
            this.Controls.Add(this.m_splitMain);
            this.Controls.Add(this.m_pnlTools);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.m_statusStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1594, 728);
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
            this.m_tabPage3_About.PerformLayout();
            this.m_tabPage2_Settings.ResumeLayout(false);
            this.m_tabPage2_Settings.PerformLayout();
            this.m_pnlConfig.ResumeLayout(false);
            this.m_splitMain.Panel1.ResumeLayout(false);
            this.m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).EndInit();
            this.m_splitMain.ResumeLayout(false);
            this.m_pnlTools.ResumeLayout(false);
            this.m_pnlTools.PerformLayout();
            this.m_ctxMenuSysTray.ResumeLayout(false);
            this.m_pnlRadiationStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadiationLogListView m_listLog;
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
        private System.Windows.Forms.CheckBox m_chkAutoUpdateLog;
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
        private RadiationGraphControl m_chart1;
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
        private RadiationConverterControl radiationConverterControl1;
        private System.Windows.Forms.CheckBox m_chkUseConverter;
        private System.Windows.Forms.NotifyIcon m_notifyIconSysTray;
        private System.Windows.Forms.ContextMenuStrip m_ctxMenuSysTray;
        private System.Windows.Forms.ToolStripMenuItem m_mnuShow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuExit;
        private System.Windows.Forms.ToolStripMenuItem m_mnuExit1;
        private System.Windows.Forms.Panel m_pnlConfig;
        private System.Windows.Forms.Panel m_pnlRadiationStatus;
    }
}

