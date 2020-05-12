using System.Windows.Forms;

namespace DUMeterMZ
{


    partial class FormLoadGraph
    {
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopPing();
                if (m_penUp != null) m_penUp.Dispose();
                if (m_penDown != null) m_penDown.Dispose();
                if (m_penBoth != null) m_penBoth.Dispose();
                if (m_penGrid != null) m_penGrid.Dispose();
                //icon.Dispose();

                if (components != null)
                {
                    components.Dispose();
                }//end if
            }//end if
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoadGraph));
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.m_PerformanceCounterRecv = new System.Diagnostics.PerformanceCounter();
            this.m_PerformanceCounterSend = new System.Diagnostics.PerformanceCounter();
            this.m_NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.m_ContextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuShowHide = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_resetPosition = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Pop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Reports = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Reports_LastHour = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Reports_Last3Hours = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Reports_Last6Hours = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Reports_Last12Hours = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Reports_Last24Hours = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Reports_Last3Days = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_ShowText = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Ping = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
            this.m_oleDbDeleteCommand = new System.Data.OleDb.OleDbCommand();
            this.m_OleDbConnection = new System.Data.OleDb.OleDbConnection();
            this.m_oleDbInsertCommand = new System.Data.OleDb.OleDbCommand();
            this.m_oleDbSelectCommand = new System.Data.OleDb.OleDbCommand();
            this.m_oleDbUpdateCommand = new System.Data.OleDb.OleDbCommand();
            this.m_logger = new DUMeterMZ.Log();
            this.m_lblScale = new OrientableText.OrientedTextLabel();
            this.m_PictureBoxGraph = new System.Windows.Forms.PictureBox();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_PerformanceCounterRecv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PerformanceCounterSend)).BeginInit();
            this.m_ContextMenuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_logger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxGraph)).BeginInit();
            this.m_pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_Timer
            // 
            this.m_Timer.Interval = 1000;
            this.m_Timer.Tick += new System.EventHandler(this.m_Timer_Tick);
            // 
            // m_PerformanceCounterRecv
            // 
            this.m_PerformanceCounterRecv.CategoryName = "Network Interface";
            this.m_PerformanceCounterRecv.CounterName = "Bytes Received/sec";
            // 
            // m_PerformanceCounterSend
            // 
            this.m_PerformanceCounterSend.CategoryName = "Network Interface";
            this.m_PerformanceCounterSend.CounterName = "Bytes Sent/sec";
            // 
            // m_NotifyIcon
            // 
            this.m_NotifyIcon.ContextMenuStrip = this.m_ContextMenuMain;
            this.m_NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("m_NotifyIcon.Icon")));
            this.m_NotifyIcon.Text = "DU Meter";
            this.m_NotifyIcon.Visible = true;
            this.m_NotifyIcon.Click += new System.EventHandler(this.m_NotifyIcon_Click);
            this.m_NotifyIcon.DoubleClick += new System.EventHandler(this.m_NotifyIcon_DoubleClick);
            this.m_NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_NotifyIcon_MouseClick);
            this.m_NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_NotifyIcon_MouseDoubleClick);
            // 
            // m_ContextMenuMain
            // 
            this.m_ContextMenuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_ContextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuShowHide,
            this.menu_resetPosition,
            this.menu_Pop,
            this.menuItemSep1,
            this.menu_Reports,
            this.menu_ShowText,
            this.menu_Ping,
            this.menuItemSep2,
            this.menu_Options,
            this.menu_About,
            this.menuItemSep3,
            this.menu_Exit});
            this.m_ContextMenuMain.Name = "m_ContextMenuMain";
            this.m_ContextMenuMain.Size = new System.Drawing.Size(186, 256);
            // 
            // menuShowHide
            // 
            this.menuShowHide.Image = global::DUMeterMZ.Properties.Resources.screen;
            this.menuShowHide.Name = "menuShowHide";
            this.menuShowHide.Size = new System.Drawing.Size(185, 26);
            this.menuShowHide.Text = "Hide";
            this.menuShowHide.Click += new System.EventHandler(this.menuShowHide_Click);
            // 
            // menu_resetPosition
            // 
            this.menu_resetPosition.Image = ((System.Drawing.Image)(resources.GetObject("menu_resetPosition.Image")));
            this.menu_resetPosition.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.menu_resetPosition.Name = "menu_resetPosition";
            this.menu_resetPosition.Size = new System.Drawing.Size(185, 26);
            this.menu_resetPosition.Text = "&Reset Position";
            this.menu_resetPosition.Click += new System.EventHandler(this.menu_resetPosition_Click);
            // 
            // menu_Pop
            // 
            this.menu_Pop.Image = global::DUMeterMZ.Properties.Resources.IconMain;
            this.menu_Pop.Name = "menu_Pop";
            this.menu_Pop.Size = new System.Drawing.Size(185, 26);
            this.menu_Pop.Text = "&Pop!";
            this.menu_Pop.Click += new System.EventHandler(this.menu_Pop_Click);
            // 
            // menuItemSep1
            // 
            this.menuItemSep1.Name = "menuItemSep1";
            this.menuItemSep1.Size = new System.Drawing.Size(182, 6);
            // 
            // menu_Reports
            // 
            this.menu_Reports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Reports_LastHour,
            this.menu_Reports_Last3Hours,
            this.menu_Reports_Last6Hours,
            this.menu_Reports_Last12Hours,
            this.menu_Reports_Last24Hours,
            this.menu_Reports_Last3Days});
            this.menu_Reports.Image = ((System.Drawing.Image)(resources.GetObject("menu_Reports.Image")));
            this.menu_Reports.Name = "menu_Reports";
            this.menu_Reports.Size = new System.Drawing.Size(185, 26);
            this.menu_Reports.Text = "Reports";
            // 
            // menu_Reports_LastHour
            // 
            this.menu_Reports_LastHour.Image = global::DUMeterMZ.Properties.Resources._1_hour;
            this.menu_Reports_LastHour.Name = "menu_Reports_LastHour";
            this.menu_Reports_LastHour.Size = new System.Drawing.Size(143, 22);
            this.menu_Reports_LastHour.Text = "Last Hour";
            this.menu_Reports_LastHour.Click += new System.EventHandler(this.menu_Reports_LastHour_Click);
            // 
            // menu_Reports_Last3Hours
            // 
            this.menu_Reports_Last3Hours.Image = global::DUMeterMZ.Properties.Resources._3_hours;
            this.menu_Reports_Last3Hours.Name = "menu_Reports_Last3Hours";
            this.menu_Reports_Last3Hours.Size = new System.Drawing.Size(143, 22);
            this.menu_Reports_Last3Hours.Text = "Last 3 hours";
            this.menu_Reports_Last3Hours.Click += new System.EventHandler(this.menu_Reports_Last3Hours_Click);
            // 
            // menu_Reports_Last6Hours
            // 
            this.menu_Reports_Last6Hours.Image = global::DUMeterMZ.Properties.Resources._6_hours;
            this.menu_Reports_Last6Hours.Name = "menu_Reports_Last6Hours";
            this.menu_Reports_Last6Hours.Size = new System.Drawing.Size(143, 22);
            this.menu_Reports_Last6Hours.Text = "Last 6 hours";
            this.menu_Reports_Last6Hours.Click += new System.EventHandler(this.menu_Reports_Last6Hours_Click);
            // 
            // menu_Reports_Last12Hours
            // 
            this.menu_Reports_Last12Hours.Image = global::DUMeterMZ.Properties.Resources._12_hours;
            this.menu_Reports_Last12Hours.Name = "menu_Reports_Last12Hours";
            this.menu_Reports_Last12Hours.Size = new System.Drawing.Size(143, 22);
            this.menu_Reports_Last12Hours.Text = "Last 12 hours";
            this.menu_Reports_Last12Hours.Click += new System.EventHandler(this.menu_Reports_Last12Hours_Click);
            // 
            // menu_Reports_Last24Hours
            // 
            this.menu_Reports_Last24Hours.Image = global::DUMeterMZ.Properties.Resources._24_hours;
            this.menu_Reports_Last24Hours.Name = "menu_Reports_Last24Hours";
            this.menu_Reports_Last24Hours.Size = new System.Drawing.Size(143, 22);
            this.menu_Reports_Last24Hours.Text = "Last 24 hours";
            this.menu_Reports_Last24Hours.Click += new System.EventHandler(this.menu_Reports_Last24Hours_Click);
            // 
            // menu_Reports_Last3Days
            // 
            this.menu_Reports_Last3Days.Image = global::DUMeterMZ.Properties.Resources._3_days;
            this.menu_Reports_Last3Days.Name = "menu_Reports_Last3Days";
            this.menu_Reports_Last3Days.Size = new System.Drawing.Size(143, 22);
            this.menu_Reports_Last3Days.Text = "Last 3 days";
            this.menu_Reports_Last3Days.Click += new System.EventHandler(this.menu_Reports_Last3Days_Click);
            // 
            // menu_ShowText
            // 
            this.menu_ShowText.Image = ((System.Drawing.Image)(resources.GetObject("menu_ShowText.Image")));
            this.menu_ShowText.Name = "menu_ShowText";
            this.menu_ShowText.Size = new System.Drawing.Size(185, 26);
            this.menu_ShowText.Text = "Show Text";
            this.menu_ShowText.Click += new System.EventHandler(this.menu_ShowText_Click);
            // 
            // menu_Ping
            // 
            this.menu_Ping.Image = ((System.Drawing.Image)(resources.GetObject("menu_Ping.Image")));
            this.menu_Ping.Name = "menu_Ping";
            this.menu_Ping.Size = new System.Drawing.Size(185, 26);
            this.menu_Ping.Text = "Start Ping";
            this.menu_Ping.Click += new System.EventHandler(this.menu_Ping_Click);
            // 
            // menuItemSep2
            // 
            this.menuItemSep2.Name = "menuItemSep2";
            this.menuItemSep2.Size = new System.Drawing.Size(182, 6);
            // 
            // menu_Options
            // 
            this.menu_Options.Image = ((System.Drawing.Image)(resources.GetObject("menu_Options.Image")));
            this.menu_Options.Name = "menu_Options";
            this.menu_Options.Size = new System.Drawing.Size(185, 26);
            this.menu_Options.Text = "Settings";
            this.menu_Options.Click += new System.EventHandler(this.menu_Options_Click);
            // 
            // menu_About
            // 
            this.menu_About.Name = "menu_About";
            this.menu_About.Size = new System.Drawing.Size(185, 26);
            this.menu_About.Text = "About DU Meter MZ";
            this.menu_About.Click += new System.EventHandler(this.menu_About_Click);
            // 
            // menuItemSep3
            // 
            this.menuItemSep3.Name = "menuItemSep3";
            this.menuItemSep3.Size = new System.Drawing.Size(182, 6);
            // 
            // menu_Exit
            // 
            this.menu_Exit.Image = ((System.Drawing.Image)(resources.GetObject("menu_Exit.Image")));
            this.menu_Exit.Name = "menu_Exit";
            this.menu_Exit.Size = new System.Drawing.Size(185, 26);
            this.menu_Exit.Text = "Exit";
            this.menu_Exit.Click += new System.EventHandler(this.menu_Exit_Click);
            // 
            // m_OleDbDataAdapter
            // 
            this.m_OleDbDataAdapter.DeleteCommand = this.m_oleDbDeleteCommand;
            this.m_OleDbDataAdapter.InsertCommand = this.m_oleDbInsertCommand;
            this.m_OleDbDataAdapter.SelectCommand = this.m_oleDbSelectCommand;
            this.m_OleDbDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "RateLog", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("Recv", "Recv"),
                        new System.Data.Common.DataColumnMapping("Send", "Send")})});
            this.m_OleDbDataAdapter.UpdateCommand = this.m_oleDbUpdateCommand;
            // 
            // m_oleDbDeleteCommand
            // 
            this.m_oleDbDeleteCommand.CommandText = "DELETE FROM RateLog WHERE (ID = ?)";
            this.m_oleDbDeleteCommand.Connection = this.m_OleDbConnection;
            this.m_oleDbDeleteCommand.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Original_ID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null)});
            // 
            // m_OleDbConnection
            // 
            this.m_OleDbConnection.ConnectionString = resources.GetString("m_OleDbConnection.ConnectionString");
            // 
            // m_oleDbInsertCommand
            // 
            this.m_oleDbInsertCommand.CommandText = "INSERT INTO RateLog (Recv, Send) VALUES (?, ?)";
            this.m_oleDbInsertCommand.Connection = this.m_OleDbConnection;
            this.m_oleDbInsertCommand.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Recv", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(7)), ((byte)(0)), "Recv", System.Data.DataRowVersion.Current, null),
            new System.Data.OleDb.OleDbParameter("Send", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(7)), ((byte)(0)), "Send", System.Data.DataRowVersion.Current, null)});
            // 
            // m_oleDbSelectCommand
            // 
            this.m_oleDbSelectCommand.CommandText = "SELECT ID, Recv, Send FROM RateLog WHERE (ID > ?)";
            this.m_oleDbSelectCommand.Connection = this.m_OleDbConnection;
            this.m_oleDbSelectCommand.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.DBDate, 0, "ID")});
            // 
            // m_oleDbUpdateCommand
            // 
            this.m_oleDbUpdateCommand.CommandText = "UPDATE RateLog SET ID = ?, Recv = ?, Send = ? WHERE (ID = ?)";
            this.m_oleDbUpdateCommand.Connection = this.m_OleDbConnection;
            this.m_oleDbUpdateCommand.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer, 0, "ID"),
            new System.Data.OleDb.OleDbParameter("Recv", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(7)), ((byte)(0)), "Recv", System.Data.DataRowVersion.Current, null),
            new System.Data.OleDb.OleDbParameter("Send", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(7)), ((byte)(0)), "Send", System.Data.DataRowVersion.Current, null),
            new System.Data.OleDb.OleDbParameter("Original_ID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null)});
            // 
            // m_logger
            // 
            this.m_logger.DataSetName = "Log";
            this.m_logger.Locale = new System.Globalization.CultureInfo("en-US");
            this.m_logger.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // m_lblScale
            // 
            this.m_lblScale.BackColor = System.Drawing.Color.Silver;
            this.m_lblScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblScale.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblScale.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblScale.Location = new System.Drawing.Point(0, 0);
            this.m_lblScale.Name = "m_lblScale";
            this.m_lblScale.Size = new System.Drawing.Size(21, 58);
            this.m_lblScale.TabIndex = 1;
            this.m_lblScale.Text = "1500 k";
            this.m_lblScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblScale.TextDirection = OrientableText.Direction.Clockwise;
            this.m_lblScale.TextOrientation = OrientableText.Orientation.Rotate;
            this.m_lblScale.TextRotationAngle = 270D;
            // 
            // m_PictureBoxGraph
            // 
            this.m_PictureBoxGraph.ContextMenuStrip = this.m_ContextMenuMain;
            this.m_PictureBoxGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PictureBoxGraph.Location = new System.Drawing.Point(21, 0);
            this.m_PictureBoxGraph.Name = "m_PictureBoxGraph";
            this.m_PictureBoxGraph.Size = new System.Drawing.Size(161, 58);
            this.m_PictureBoxGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_PictureBoxGraph.TabIndex = 0;
            this.m_PictureBoxGraph.TabStop = false;
            this.m_PictureBoxGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.m_PictureBoxGraph_Paint);
            this.m_PictureBoxGraph.DoubleClick += new System.EventHandler(this.m_PictureBoxGraph_DoubleClick);
            this.m_PictureBoxGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_PictureBoxGraph_MouseDown);
            this.m_PictureBoxGraph.MouseLeave += new System.EventHandler(this.m_PictureBoxGraph_MouseLeave);
            this.m_PictureBoxGraph.MouseHover += new System.EventHandler(this.m_PictureBoxGraph_MouseHover);
            this.m_PictureBoxGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_PictureBoxGraph_MouseMove);
            this.m_PictureBoxGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_PictureBoxGraph_MouseUp);
            this.m_PictureBoxGraph.Resize += new System.EventHandler(this.m_PictureBoxGraph_Resize);
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlMain.Controls.Add(this.m_PictureBoxGraph);
            this.m_pnlMain.Controls.Add(this.m_lblScale);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(184, 60);
            this.m_pnlMain.TabIndex = 2;
            // 
            // FormLoadGraph
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(18, 39);
            this.ClientSize = new System.Drawing.Size(184, 60);
            this.ControlBox = false;
            this.Controls.Add(this.m_pnlMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(50, 42);
            this.Name = "FormLoadGraph";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormLoadGraph_Load);
            this.SizeChanged += new System.EventHandler(this.FormLoadGraph_SizeChanged);
            this.Move += new System.EventHandler(this.FormLoadGraph_Move);
            ((System.ComponentModel.ISupportInitialize)(this.m_PerformanceCounterRecv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PerformanceCounterSend)).EndInit();
            this.m_ContextMenuMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_logger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxGraph)).EndInit();
            this.m_pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Data.OleDb.OleDbDataAdapter m_OleDbDataAdapter;
        private System.Data.OleDb.OleDbConnection m_OleDbConnection;
        private System.Data.OleDb.OleDbCommand m_oleDbSelectCommand;
        private System.Data.OleDb.OleDbCommand m_oleDbInsertCommand;
        private System.Data.OleDb.OleDbCommand m_oleDbUpdateCommand;
        private System.Data.OleDb.OleDbCommand m_oleDbDeleteCommand;

        private DUMeterMZ.Log m_logger;

        private TrayIconList m_vTrayIcons;

        private ContextMenuStrip m_ContextMenuMain;
        private ToolStripMenuItem menuShowHide;
        private ToolStripSeparator menuItemSep1;
        private ToolStripMenuItem menu_Options;
        private ToolStripMenuItem menu_Reports;
        private ToolStripMenuItem menu_Reports_LastHour;
        private ToolStripMenuItem menu_Reports_Last3Hours;
        private ToolStripMenuItem menu_Reports_Last6Hours;
        private ToolStripMenuItem menu_Reports_Last12Hours;
        private ToolStripMenuItem menu_Reports_Last24Hours;
        private ToolStripMenuItem menu_Reports_Last3Days;
        private ToolStripMenuItem menu_ShowText;
        private ToolStripMenuItem menu_Ping;
        private ToolStripSeparator menuItemSep2;
        private ToolStripMenuItem menu_About;
        private ToolStripSeparator menuItemSep3;
        private ToolStripMenuItem menu_Exit;
        private ToolStripMenuItem menu_resetPosition;
        private OrientableText.OrientedTextLabel m_lblScale;
        private ToolStripMenuItem menu_Pop;
        private System.Windows.Forms.Timer m_Timer;
        private System.Diagnostics.PerformanceCounter m_PerformanceCounterRecv;
        private System.Diagnostics.PerformanceCounter m_PerformanceCounterSend;
        private System.Windows.Forms.PictureBox m_PictureBoxGraph;
        private System.Windows.Forms.NotifyIcon m_NotifyIcon;
        private Panel m_pnlMain;
    }
}