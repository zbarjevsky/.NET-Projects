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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.m_txtLog = new System.Windows.Forms.RichTextBox();
            this.m_btnRequest = new System.Windows.Forms.Button();
            this.m_chkConnect = new System.Windows.Forms.CheckBox();
            this.m_chkPause = new System.Windows.Forms.CheckBox();
            this.m_txtStatus = new System.Windows.Forms.TextBox();
            this.m_lblVal = new System.Windows.Forms.Label();
            this.m_btnClear = new System.Windows.Forms.Button();
            this.m_btnVer = new System.Windows.Forms.Button();
            this.m_btnSet = new System.Windows.Forms.Button();
            this.m_btnVer2 = new System.Windows.Forms.Button();
            this.m_lblSN = new System.Windows.Forms.Label();
            this.m_chkSnd = new System.Windows.Forms.CheckBox();
            this.m_chkVib = new System.Windows.Forms.CheckBox();
            this.m_lblWarn = new System.Windows.Forms.Label();
            this.m_numLimit = new System.Windows.Forms.NumericUpDown();
            this.m_numMaxCPM = new System.Windows.Forms.NumericUpDown();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_splitContainerTools = new System.Windows.Forms.SplitContainer();
            this.m_picYellowLight = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtRecords = new System.Windows.Forms.RichTextBox();
            this.m_lblInterval = new System.Windows.Forms.Label();
            this.m_numInterval = new System.Windows.Forms.NumericUpDown();
            this.m_lblMaxCPM = new System.Windows.Forms.Label();
            this.m_lblCPM = new System.Windows.Forms.Label();
            this.m_lblDose = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_btnHistory = new System.Windows.Forms.Button();
            this.m_chkAutoConnect = new System.Windows.Forms.CheckBox();
            this.m_btnTest = new System.Windows.Forms.Button();
            this.m_chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.m_cmbDevices = new System.Windows.Forms.ComboBox();
            this.m_chkShowLog = new System.Windows.Forms.CheckBox();
            this.m_btnResetDose = new System.Windows.Forms.Button();
            this.m_statusStripMain = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_ProgressBarStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_progressMain = new RadexOneDemo.VerticalProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.m_numLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numMaxCPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerTools)).BeginInit();
            this.m_splitContainerTools.Panel1.SuspendLayout();
            this.m_splitContainerTools.Panel2.SuspendLayout();
            this.m_splitContainerTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picYellowLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).BeginInit();
            this.m_statusStripMain.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtLog
            // 
            this.m_txtLog.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtLog.Location = new System.Drawing.Point(0, 0);
            this.m_txtLog.Name = "m_txtLog";
            this.m_txtLog.Size = new System.Drawing.Size(852, 117);
            this.m_txtLog.TabIndex = 0;
            this.m_txtLog.Text = "";
            // 
            // m_btnRequest
            // 
            this.m_btnRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRequest.Location = new System.Drawing.Point(546, 563);
            this.m_btnRequest.Name = "m_btnRequest";
            this.m_btnRequest.Size = new System.Drawing.Size(150, 23);
            this.m_btnRequest.TabIndex = 2;
            this.m_btnRequest.Text = "Request Data";
            this.m_btnRequest.UseVisualStyleBackColor = true;
            this.m_btnRequest.Click += new System.EventHandler(this.m_btnRequest_Click);
            // 
            // m_chkConnect
            // 
            this.m_chkConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkConnect.AutoSize = true;
            this.m_chkConnect.Location = new System.Drawing.Point(657, 10);
            this.m_chkConnect.Name = "m_chkConnect";
            this.m_chkConnect.Size = new System.Drawing.Size(66, 17);
            this.m_chkConnect.TabIndex = 3;
            this.m_chkConnect.Text = "Connect";
            this.m_chkConnect.UseVisualStyleBackColor = true;
            this.m_chkConnect.CheckedChanged += new System.EventHandler(this.m_chkConnect_CheckedChanged);
            // 
            // m_chkPause
            // 
            this.m_chkPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkPause.AutoSize = true;
            this.m_chkPause.Location = new System.Drawing.Point(785, 10);
            this.m_chkPause.Name = "m_chkPause";
            this.m_chkPause.Size = new System.Drawing.Size(56, 17);
            this.m_chkPause.TabIndex = 4;
            this.m_chkPause.Text = "Pause";
            this.m_chkPause.UseVisualStyleBackColor = true;
            this.m_chkPause.CheckedChanged += new System.EventHandler(this.m_chkAuto_CheckedChanged);
            // 
            // m_txtStatus
            // 
            this.m_txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtStatus.Location = new System.Drawing.Point(7, 84);
            this.m_txtStatus.Multiline = true;
            this.m_txtStatus.Name = "m_txtStatus";
            this.m_txtStatus.ReadOnly = true;
            this.m_txtStatus.Size = new System.Drawing.Size(834, 50);
            this.m_txtStatus.TabIndex = 6;
            // 
            // m_lblVal
            // 
            this.m_lblVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblVal.BackColor = System.Drawing.Color.Chartreuse;
            this.m_lblVal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblVal.Location = new System.Drawing.Point(10, 450);
            this.m_lblVal.Name = "m_lblVal";
            this.m_lblVal.Size = new System.Drawing.Size(73, 68);
            this.m_lblVal.TabIndex = 7;
            this.m_lblVal.Text = "100";
            this.m_lblVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btnClear
            // 
            this.m_btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClear.Location = new System.Drawing.Point(699, 563);
            this.m_btnClear.Name = "m_btnClear";
            this.m_btnClear.Size = new System.Drawing.Size(150, 23);
            this.m_btnClear.TabIndex = 9;
            this.m_btnClear.Text = "Clear Data";
            this.m_btnClear.UseVisualStyleBackColor = true;
            this.m_btnClear.Click += new System.EventHandler(this.m_btnClear_Click);
            // 
            // m_btnVer
            // 
            this.m_btnVer.Location = new System.Drawing.Point(154, 145);
            this.m_btnVer.Name = "m_btnVer";
            this.m_btnVer.Size = new System.Drawing.Size(139, 23);
            this.m_btnVer.TabIndex = 10;
            this.m_btnVer.Text = "Request Version";
            this.m_btnVer.UseVisualStyleBackColor = true;
            this.m_btnVer.Click += new System.EventHandler(this.m_btnGetVer_Click);
            // 
            // m_btnSet
            // 
            this.m_btnSet.Location = new System.Drawing.Point(232, 178);
            this.m_btnSet.Name = "m_btnSet";
            this.m_btnSet.Size = new System.Drawing.Size(133, 23);
            this.m_btnSet.TabIndex = 11;
            this.m_btnSet.Text = "Set Settings";
            this.m_btnSet.UseVisualStyleBackColor = true;
            this.m_btnSet.Click += new System.EventHandler(this.m_btnSet_Click);
            // 
            // m_btnVer2
            // 
            this.m_btnVer2.Location = new System.Drawing.Point(9, 145);
            this.m_btnVer2.Name = "m_btnVer2";
            this.m_btnVer2.Size = new System.Drawing.Size(139, 23);
            this.m_btnVer2.TabIndex = 13;
            this.m_btnVer2.Text = "Request Settings";
            this.m_btnVer2.UseVisualStyleBackColor = true;
            this.m_btnVer2.Click += new System.EventHandler(this.m_btnGetSett_Click);
            // 
            // m_lblSN
            // 
            this.m_lblSN.AutoSize = true;
            this.m_lblSN.Location = new System.Drawing.Point(335, 39);
            this.m_lblSN.Name = "m_lblSN";
            this.m_lblSN.Size = new System.Drawing.Size(39, 13);
            this.m_lblSN.TabIndex = 14;
            this.m_lblSN.Text = "S/N: ?";
            // 
            // m_chkSnd
            // 
            this.m_chkSnd.AutoSize = true;
            this.m_chkSnd.Location = new System.Drawing.Point(37, 182);
            this.m_chkSnd.Name = "m_chkSnd";
            this.m_chkSnd.Size = new System.Drawing.Size(57, 17);
            this.m_chkSnd.TabIndex = 15;
            this.m_chkSnd.Text = "Sound";
            this.m_chkSnd.UseVisualStyleBackColor = true;
            // 
            // m_chkVib
            // 
            this.m_chkVib.AutoSize = true;
            this.m_chkVib.Location = new System.Drawing.Point(100, 182);
            this.m_chkVib.Name = "m_chkVib";
            this.m_chkVib.Size = new System.Drawing.Size(44, 17);
            this.m_chkVib.TabIndex = 16;
            this.m_chkVib.Text = "Vibr";
            this.m_chkVib.UseVisualStyleBackColor = true;
            // 
            // m_lblWarn
            // 
            this.m_lblWarn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblWarn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblWarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblWarn.Location = new System.Drawing.Point(242, 450);
            this.m_lblWarn.Name = "m_lblWarn";
            this.m_lblWarn.Size = new System.Drawing.Size(73, 68);
            this.m_lblWarn.TabIndex = 17;
            this.m_lblWarn.Text = "100";
            this.m_lblWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_numLimit
            // 
            this.m_numLimit.DecimalPlaces = 1;
            this.m_numLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numLimit.Location = new System.Drawing.Point(150, 181);
            this.m_numLimit.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.m_numLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numLimit.Name = "m_numLimit";
            this.m_numLimit.Size = new System.Drawing.Size(71, 20);
            this.m_numLimit.TabIndex = 18;
            this.m_numLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numLimit.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // m_numMaxCPM
            // 
            this.m_numMaxCPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_numMaxCPM.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_numMaxCPM.Location = new System.Drawing.Point(242, 415);
            this.m_numMaxCPM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numMaxCPM.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_numMaxCPM.Name = "m_numMaxCPM";
            this.m_numMaxCPM.Size = new System.Drawing.Size(73, 20);
            this.m_numMaxCPM.TabIndex = 19;
            this.m_numMaxCPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numMaxCPM.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.m_numMaxCPM.ValueChanged += new System.EventHandler(this.m_numMaxCPM_ValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_splitContainerTools);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 716);
            this.splitContainer1.SplitterDistance = 326;
            this.splitContainer1.TabIndex = 20;
            // 
            // m_splitContainerTools
            // 
            this.m_splitContainerTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitContainerTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainerTools.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_splitContainerTools.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainerTools.Name = "m_splitContainerTools";
            this.m_splitContainerTools.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainerTools.Panel1
            // 
            this.m_splitContainerTools.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_splitContainerTools.Panel1.Controls.Add(this.m_picYellowLight);
            this.m_splitContainerTools.Panel1.Controls.Add(this.m_progressMain);
            // 
            // m_splitContainerTools.Panel2
            // 
            this.m_splitContainerTools.Panel2.Controls.Add(this.label1);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_txtRecords);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblInterval);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_numMaxCPM);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_numInterval);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblVal);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblMaxCPM);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblWarn);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblCPM);
            this.m_splitContainerTools.Panel2.Controls.Add(this.m_lblDose);
            this.m_splitContainerTools.Size = new System.Drawing.Size(326, 716);
            this.m_splitContainerTools.SplitterDistance = 181;
            this.m_splitContainerTools.TabIndex = 25;
            // 
            // m_picYellowLight
            // 
            this.m_picYellowLight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_picYellowLight.BackColor = System.Drawing.SystemColors.Control;
            this.m_picYellowLight.Image = global::RadexOneDemo.Properties.Resources.radiation_symbol;
            this.m_picYellowLight.Location = new System.Drawing.Point(11, 13);
            this.m_picYellowLight.Name = "m_picYellowLight";
            this.m_picYellowLight.Size = new System.Drawing.Size(273, 152);
            this.m_picYellowLight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_picYellowLight.TabIndex = 0;
            this.m_picYellowLight.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Records(+) and Warnings(*)";
            // 
            // m_txtRecords
            // 
            this.m_txtRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRecords.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtRecords.Location = new System.Drawing.Point(3, 29);
            this.m_txtRecords.Name = "m_txtRecords";
            this.m_txtRecords.Size = new System.Drawing.Size(318, 380);
            this.m_txtRecords.TabIndex = 25;
            this.m_txtRecords.Text = "";
            this.m_txtRecords.WordWrap = false;
            // 
            // m_lblInterval
            // 
            this.m_lblInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblInterval.AutoSize = true;
            this.m_lblInterval.Location = new System.Drawing.Point(8, 417);
            this.m_lblInterval.Name = "m_lblInterval";
            this.m_lblInterval.Size = new System.Drawing.Size(65, 13);
            this.m_lblInterval.TabIndex = 24;
            this.m_lblInterval.Text = "Interval(sec)";
            // 
            // m_numInterval
            // 
            this.m_numInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_numInterval.DecimalPlaces = 1;
            this.m_numInterval.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.m_numInterval.Location = new System.Drawing.Point(81, 415);
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
            this.m_numInterval.TabIndex = 23;
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
            this.m_lblMaxCPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblMaxCPM.AutoSize = true;
            this.m_lblMaxCPM.Location = new System.Drawing.Point(183, 417);
            this.m_lblMaxCPM.Name = "m_lblMaxCPM";
            this.m_lblMaxCPM.Size = new System.Drawing.Size(53, 13);
            this.m_lblMaxCPM.TabIndex = 22;
            this.m_lblMaxCPM.Text = "Max CPM";
            // 
            // m_lblCPM
            // 
            this.m_lblCPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCPM.AutoSize = true;
            this.m_lblCPM.Location = new System.Drawing.Point(206, 452);
            this.m_lblCPM.Name = "m_lblCPM";
            this.m_lblCPM.Size = new System.Drawing.Size(30, 13);
            this.m_lblCPM.TabIndex = 21;
            this.m_lblCPM.Text = "CPM";
            // 
            // m_lblDose
            // 
            this.m_lblDose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblDose.AutoSize = true;
            this.m_lblDose.Location = new System.Drawing.Point(89, 452);
            this.m_lblDose.Name = "m_lblDose";
            this.m_lblDose.Size = new System.Drawing.Size(37, 13);
            this.m_lblDose.TabIndex = 20;
            this.m_lblDose.Text = "µSv/h";
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
            this.splitContainer2.Panel1.Controls.Add(this.m_btnHistory);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkAutoConnect);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnTest);
            this.splitContainer2.Panel1.Controls.Add(this.m_chart1);
            this.splitContainer2.Panel1.Controls.Add(this.m_cmbDevices);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkShowLog);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkConnect);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnResetDose);
            this.splitContainer2.Panel1.Controls.Add(this.m_txtStatus);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkSnd);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnClear);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkVib);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnVer);
            this.splitContainer2.Panel1.Controls.Add(this.m_lblSN);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnRequest);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnSet);
            this.splitContainer2.Panel1.Controls.Add(this.m_chkPause);
            this.splitContainer2.Panel1.Controls.Add(this.m_numLimit);
            this.splitContainer2.Panel1.Controls.Add(this.m_btnVer2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_txtLog);
            this.splitContainer2.Size = new System.Drawing.Size(854, 716);
            this.splitContainer2.SplitterDistance = 593;
            this.splitContainer2.TabIndex = 0;
            // 
            // m_btnHistory
            // 
            this.m_btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHistory.Image")));
            this.m_btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnHistory.Location = new System.Drawing.Point(737, 49);
            this.m_btnHistory.Name = "m_btnHistory";
            this.m_btnHistory.Size = new System.Drawing.Size(103, 27);
            this.m_btnHistory.TabIndex = 26;
            this.m_btnHistory.Text = "&History";
            this.m_btnHistory.UseVisualStyleBackColor = true;
            this.m_btnHistory.Click += new System.EventHandler(this.m_btnHistory_Click);
            // 
            // m_chkAutoConnect
            // 
            this.m_chkAutoConnect.AutoSize = true;
            this.m_chkAutoConnect.Location = new System.Drawing.Point(286, 10);
            this.m_chkAutoConnect.Name = "m_chkAutoConnect";
            this.m_chkAutoConnect.Size = new System.Drawing.Size(131, 17);
            this.m_chkAutoConnect.TabIndex = 25;
            this.m_chkAutoConnect.Text = "Connect Automatically";
            this.m_chkAutoConnect.UseVisualStyleBackColor = true;
            // 
            // m_btnTest
            // 
            this.m_btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnTest.Location = new System.Drawing.Point(554, 145);
            this.m_btnTest.Name = "m_btnTest";
            this.m_btnTest.Size = new System.Drawing.Size(139, 23);
            this.m_btnTest.TabIndex = 24;
            this.m_btnTest.Text = "Test Request";
            this.m_btnTest.UseVisualStyleBackColor = true;
            this.m_btnTest.Click += new System.EventHandler(this.m_btnTest_Click);
            // 
            // m_chart1
            // 
            this.m_chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chart1.BackColor = System.Drawing.SystemColors.Control;
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
            chartArea1.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.BackSecondaryColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            this.m_chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.m_chart1.Legends.Add(legend1);
            this.m_chart1.Location = new System.Drawing.Point(9, 214);
            this.m_chart1.Name = "m_chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series1.Legend = "Legend1";
            series1.LegendText = "DOSE µSv/h";
            series1.Name = "SeriesDOSE";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series2.Legend = "Legend1";
            series2.LegendText = "CPM";
            series2.Name = "SeriesCPM";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.m_chart1.Series.Add(series1);
            this.m_chart1.Series.Add(series2);
            this.m_chart1.Size = new System.Drawing.Size(831, 343);
            this.m_chart1.TabIndex = 23;
            this.m_chart1.Text = "chart1";
            // 
            // m_cmbDevices
            // 
            this.m_cmbDevices.BackColor = System.Drawing.SystemColors.Info;
            this.m_cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.m_cmbDevices.FormattingEnabled = true;
            this.m_cmbDevices.Location = new System.Drawing.Point(9, 8);
            this.m_cmbDevices.Name = "m_cmbDevices";
            this.m_cmbDevices.Size = new System.Drawing.Size(271, 73);
            this.m_cmbDevices.TabIndex = 22;
            // 
            // m_chkShowLog
            // 
            this.m_chkShowLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_chkShowLog.AutoSize = true;
            this.m_chkShowLog.Checked = true;
            this.m_chkShowLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkShowLog.Location = new System.Drawing.Point(3, 569);
            this.m_chkShowLog.Name = "m_chkShowLog";
            this.m_chkShowLog.Size = new System.Drawing.Size(78, 17);
            this.m_chkShowLog.TabIndex = 21;
            this.m_chkShowLog.Text = "Show LOG";
            this.m_chkShowLog.UseVisualStyleBackColor = true;
            // 
            // m_btnResetDose
            // 
            this.m_btnResetDose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnResetDose.Location = new System.Drawing.Point(699, 145);
            this.m_btnResetDose.Name = "m_btnResetDose";
            this.m_btnResetDose.Size = new System.Drawing.Size(139, 23);
            this.m_btnResetDose.TabIndex = 20;
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
            this.m_statusStripMain.TabIndex = 25;
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
            this.menuStrip1.TabIndex = 22;
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
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
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
            // m_progressMain
            // 
            this.m_progressMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progressMain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_progressMain.Location = new System.Drawing.Point(302, 13);
            this.m_progressMain.Name = "m_progressMain";
            this.m_progressMain.Size = new System.Drawing.Size(13, 152);
            this.m_progressMain.TabIndex = 8;
            this.m_progressMain.Value = 33;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.m_statusStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radex Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_numLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numMaxCPM)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.m_splitContainerTools.Panel1.ResumeLayout(false);
            this.m_splitContainerTools.Panel2.ResumeLayout(false);
            this.m_splitContainerTools.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerTools)).EndInit();
            this.m_splitContainerTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picYellowLight)).EndInit();
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox m_txtLog;
        private System.Windows.Forms.Button m_btnRequest;
        private System.Windows.Forms.CheckBox m_chkConnect;
        private System.Windows.Forms.CheckBox m_chkPause;
        private System.Windows.Forms.TextBox m_txtStatus;
        private System.Windows.Forms.Label m_lblVal;
        private VerticalProgressBar m_progressMain;
        private System.Windows.Forms.Button m_btnClear;
        private System.Windows.Forms.Button m_btnVer;
        private System.Windows.Forms.Button m_btnSet;
        private System.Windows.Forms.Button m_btnVer2;
        private System.Windows.Forms.Label m_lblSN;
        private System.Windows.Forms.CheckBox m_chkSnd;
        private System.Windows.Forms.CheckBox m_chkVib;
        private System.Windows.Forms.Label m_lblWarn;
        private System.Windows.Forms.NumericUpDown m_numLimit;
        private System.Windows.Forms.NumericUpDown m_numMaxCPM;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox m_picYellowLight;
        private System.Windows.Forms.Button m_btnResetDose;
        private System.Windows.Forms.Label m_lblCPM;
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
    }
}

