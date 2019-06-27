using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Threading;

namespace DUMeterMZ
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class FormLoadGraph : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer m_Timer;
		private System.ComponentModel.IContainer components;
		private System.Diagnostics.PerformanceCounter m_PerformanceCounterRecv;
		private System.Diagnostics.PerformanceCounter m_PerformanceCounterSend;
		private System.Windows.Forms.PictureBox m_PictureBoxGraph;
		private System.Windows.Forms.NotifyIcon m_NotifyIcon;
        private Thread _workingThreadCounters;
        private bool _close = false;

		private DUMeterMZ.Log logger;

		private System.Data.OleDb.OleDbDataAdapter	m_OleDbDataAdapter;
		private System.Data.OleDb.OleDbConnection	m_OleDbConnection;
		private System.Data.OleDb.OleDbCommand		m_oleDbSelectCommand;
		private System.Data.OleDb.OleDbCommand		m_oleDbInsertCommand;
		private System.Data.OleDb.OleDbCommand		m_oleDbUpdateCommand;
		private System.Data.OleDb.OleDbCommand		m_oleDbDeleteCommand;

        private Bitmap              m_bmp;
        private int					linespeed	= (int)LineSpeed.DSL_1500k;
        private Options				m_Options;
		
		private TrayIconList		m_vTrayIcons;
		
		private ContextMenuStrip	m_ContextMenuMain;
		private ToolStripMenuItem	menuShowHide;
		private ToolStripSeparator	menuItemSep1;
		private ToolStripMenuItem	menu_Options;
		private ToolStripMenuItem	menu_Reports;
		private ToolStripMenuItem	menu_Reports_LastHour;
		private ToolStripMenuItem	menu_Reports_Last3Hours;
		private ToolStripMenuItem	menu_Reports_Last6Hours;
		private ToolStripMenuItem	menu_Reports_Last12Hours;
		private ToolStripMenuItem	menu_Reports_Last24Hours;
		private ToolStripMenuItem	menu_Reports_Last3Days;
		private ToolStripMenuItem	menu_ShowText;
		private ToolStripMenuItem	menu_Ping;
		private ToolStripSeparator	menuItemSep2;
		private ToolStripMenuItem	menu_About;
		private ToolStripSeparator	menuItemSep3;
		private ToolStripMenuItem	menu_Exit;
		private ToolStripMenuItem	menu_resetPosition;

        private string m_sSettingsFile = "DUMeterMZ.config";
        private OrientableText.OrientedTextLabel m_lblScale;

		private SendEmail m_SendIPEmail;
		private ToolStripMenuItem menu_Pop;

		private string	m_sGraphText = "Initializing...";
      
		public FormLoadGraph()
		{
            if ( !HasCounterCategory("Network Interface") )
            {
                this.Close();
                throw new Exception("This computer has no: 'Network Interface' counter");
            }//end if

			InitializeComponent();

			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			m_Options = new Options();
			m_vTrayIcons = new TrayIconList();

            string sDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            m_sSettingsFile = sDirectory + "\\" + m_sSettingsFile;

            if (!(File.Exists(sDirectory + "\\" + "log.mdb")))
			{
				Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("DUMeterMZ.log.mdb");
                Stream r = File.Create(sDirectory + "\\" + "log.mdb");
				int len = 8192;
				byte[] buffer = new byte[len];
				while (len > 0)
				{
					len = s.Read(buffer, 0, len);
					r.Write(buffer, 0, len);
				}
				r.Close();
				s.Close();
			}//end if      

			m_OleDbConnection.ConnectionString = 
				@"Provider=Microsoft.Jet.OLEDB.4.0;"+
				@"Password="""";User ID=Admin;"+
                @"Data Source=" + sDirectory + @"\log.mdb;" +
				@"Mode=Share Deny None;"+
				@"Extended Properties="""";"+
				@"Jet OLEDB:System database="""";"+
				@"Jet OLEDB:Registry Path="""";"+
				@"Jet OLEDB:Database Password="""";"+
				@"Jet OLEDB:Engine Type=5;"+
				@"Jet OLEDB:Database Locking Mode=1;"+
				@"Jet OLEDB:Global Partial Bulk Ops=2;"+
				@"Jet OLEDB:Global Bulk Transactions=1;"+
				@"Jet OLEDB:New Database Password="""";"+
				@"Jet OLEDB:Create System Database=False;"+
				@"Jet OLEDB:Encrypt Database=False;"+
				@"Jet OLEDB:Don't Copy Locale on Compact=False;"+
				@"Jet OLEDB:Compact Without Replica Repair=False;"+
				@"Jet OLEDB:SFP=False";

			Application.ApplicationExit += new EventHandler(this.AppExit);
			MouseWheel += new MouseEventHandler(MyMouseWheel);
			m_bmp = new Bitmap(m_PictureBoxGraph.Width, m_PictureBoxGraph.Height);
			m_NotifyIcon.Icon = m_vTrayIcons[3];
			//icon = Icon.ToBitmap();

            //initialize queue with screen width
			m_recv_all_q = new FixedSizeQueue<float>(Screen.PrimaryScreen.Bounds.Width);
            m_send_all_q = new FixedSizeQueue<float>(Screen.PrimaryScreen.Bounds.Width);
			m_OleDbConnection.Open();
		}//end Constructor

        private bool HasCounterCategory(string sCat)
        {
            try
            {
                PerformanceCounterCategory[] vCat = PerformanceCounterCategory.GetCategories();
                foreach (PerformanceCounterCategory c in vCat)
                {
                    if (c.CategoryName == sCat)
                        return true;
                }//end foreach
            }
            catch (Exception err)
            {
            }

            return false;
        }//end HasCounterCategory

		private void MyMouseWheel(object sender, MouseEventArgs e)
		{
			if (briteness)
			{
				m_Options.Transparency += 0.05 * (e.Delta/Math.Abs(e.Delta));
				if (m_Options.Transparency < 0.1)
				{
					m_Options.Transparency = 0.1;
				}//end if
				if (m_Options.Transparency > 1)
				{
					m_Options.Transparency = 1;
				}//end if
				Opacity = m_Options.Transparency;
				m_hidecounter = 0;
				Invalidate();
			}//end if
		}//end MyMouseWheel


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				StopPing();
				if ( m_penUp != null )      m_penUp.Dispose();
                if ( m_penDown != null )    m_penDown.Dispose();
                if ( m_penBoth != null )    m_penBoth.Dispose();
                if ( m_penGrid != null )    m_penGrid.Dispose();
				//icon.Dispose();

				if ( components != null ) 
				{
					components.Dispose();
				}//end if
			}//end if
			base.Dispose( disposing );
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
            this.logger = new DUMeterMZ.Log();
            this.m_lblScale = new OrientableText.OrientedTextLabel();
            this.m_PictureBoxGraph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_PerformanceCounterRecv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PerformanceCounterSend)).BeginInit();
            this.m_ContextMenuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxGraph)).BeginInit();
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
            this.m_ContextMenuMain.Size = new System.Drawing.Size(218, 284);
            // 
            // menuShowHide
            // 
            this.menuShowHide.Image = global::DUMeterMZ.Properties.Resources.screen;
            this.menuShowHide.Name = "menuShowHide";
            this.menuShowHide.Size = new System.Drawing.Size(217, 26);
            this.menuShowHide.Text = "Hide";
            this.menuShowHide.Click += new System.EventHandler(this.menuShowHide_Click);
            // 
            // menu_resetPosition
            // 
            this.menu_resetPosition.Image = ((System.Drawing.Image)(resources.GetObject("menu_resetPosition.Image")));
            this.menu_resetPosition.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.menu_resetPosition.Name = "menu_resetPosition";
            this.menu_resetPosition.Size = new System.Drawing.Size(217, 26);
            this.menu_resetPosition.Text = "&Reset Position";
            this.menu_resetPosition.Click += new System.EventHandler(this.menu_resetPosition_Click);
            // 
            // menu_Pop
            // 
            this.menu_Pop.Image = global::DUMeterMZ.Properties.Resources.IconMain;
            this.menu_Pop.Name = "menu_Pop";
            this.menu_Pop.Size = new System.Drawing.Size(217, 26);
            this.menu_Pop.Text = "&Pop!";
            this.menu_Pop.Click += new System.EventHandler(this.menu_Pop_Click);
            // 
            // menuItemSep1
            // 
            this.menuItemSep1.Name = "menuItemSep1";
            this.menuItemSep1.Size = new System.Drawing.Size(214, 6);
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
            this.menu_Reports.Size = new System.Drawing.Size(217, 26);
            this.menu_Reports.Text = "Reports";
            // 
            // menu_Reports_LastHour
            // 
            this.menu_Reports_LastHour.Image = global::DUMeterMZ.Properties.Resources._1_hour;
            this.menu_Reports_LastHour.Name = "menu_Reports_LastHour";
            this.menu_Reports_LastHour.Size = new System.Drawing.Size(170, 26);
            this.menu_Reports_LastHour.Text = "Last Hour";
            this.menu_Reports_LastHour.Click += new System.EventHandler(this.menu_Reports_LastHour_Click);
            // 
            // menu_Reports_Last3Hours
            // 
            this.menu_Reports_Last3Hours.Image = global::DUMeterMZ.Properties.Resources._3_hours;
            this.menu_Reports_Last3Hours.Name = "menu_Reports_Last3Hours";
            this.menu_Reports_Last3Hours.Size = new System.Drawing.Size(170, 26);
            this.menu_Reports_Last3Hours.Text = "Last 3 hours";
            this.menu_Reports_Last3Hours.Click += new System.EventHandler(this.menu_Reports_Last3Hours_Click);
            // 
            // menu_Reports_Last6Hours
            // 
            this.menu_Reports_Last6Hours.Image = global::DUMeterMZ.Properties.Resources._6_hours;
            this.menu_Reports_Last6Hours.Name = "menu_Reports_Last6Hours";
            this.menu_Reports_Last6Hours.Size = new System.Drawing.Size(170, 26);
            this.menu_Reports_Last6Hours.Text = "Last 6 hours";
            this.menu_Reports_Last6Hours.Click += new System.EventHandler(this.menu_Reports_Last6Hours_Click);
            // 
            // menu_Reports_Last12Hours
            // 
            this.menu_Reports_Last12Hours.Image = global::DUMeterMZ.Properties.Resources._12_hours;
            this.menu_Reports_Last12Hours.Name = "menu_Reports_Last12Hours";
            this.menu_Reports_Last12Hours.Size = new System.Drawing.Size(170, 26);
            this.menu_Reports_Last12Hours.Text = "Last 12 hours";
            this.menu_Reports_Last12Hours.Click += new System.EventHandler(this.menu_Reports_Last12Hours_Click);
            // 
            // menu_Reports_Last24Hours
            // 
            this.menu_Reports_Last24Hours.Image = global::DUMeterMZ.Properties.Resources._24_hours;
            this.menu_Reports_Last24Hours.Name = "menu_Reports_Last24Hours";
            this.menu_Reports_Last24Hours.Size = new System.Drawing.Size(170, 26);
            this.menu_Reports_Last24Hours.Text = "Last 24 hours";
            this.menu_Reports_Last24Hours.Click += new System.EventHandler(this.menu_Reports_Last24Hours_Click);
            // 
            // menu_Reports_Last3Days
            // 
            this.menu_Reports_Last3Days.Image = global::DUMeterMZ.Properties.Resources._3_days;
            this.menu_Reports_Last3Days.Name = "menu_Reports_Last3Days";
            this.menu_Reports_Last3Days.Size = new System.Drawing.Size(170, 26);
            this.menu_Reports_Last3Days.Text = "Last 3 days";
            this.menu_Reports_Last3Days.Click += new System.EventHandler(this.menu_Reports_Last3Days_Click);
            // 
            // menu_ShowText
            // 
            this.menu_ShowText.Image = ((System.Drawing.Image)(resources.GetObject("menu_ShowText.Image")));
            this.menu_ShowText.Name = "menu_ShowText";
            this.menu_ShowText.Size = new System.Drawing.Size(217, 26);
            this.menu_ShowText.Text = "Show Text";
            this.menu_ShowText.Click += new System.EventHandler(this.menu_ShowText_Click);
            // 
            // menu_Ping
            // 
            this.menu_Ping.Image = ((System.Drawing.Image)(resources.GetObject("menu_Ping.Image")));
            this.menu_Ping.Name = "menu_Ping";
            this.menu_Ping.Size = new System.Drawing.Size(217, 26);
            this.menu_Ping.Text = "Start Ping";
            this.menu_Ping.Click += new System.EventHandler(this.menu_Ping_Click);
            // 
            // menuItemSep2
            // 
            this.menuItemSep2.Name = "menuItemSep2";
            this.menuItemSep2.Size = new System.Drawing.Size(214, 6);
            // 
            // menu_Options
            // 
            this.menu_Options.Image = ((System.Drawing.Image)(resources.GetObject("menu_Options.Image")));
            this.menu_Options.Name = "menu_Options";
            this.menu_Options.Size = new System.Drawing.Size(217, 26);
            this.menu_Options.Text = "Settings";
            this.menu_Options.Click += new System.EventHandler(this.menu_Options_Click);
            // 
            // menu_About
            // 
            this.menu_About.Name = "menu_About";
            this.menu_About.Size = new System.Drawing.Size(217, 26);
            this.menu_About.Text = "About DU Meter MZ";
            this.menu_About.Click += new System.EventHandler(this.menu_About_Click);
            // 
            // menuItemSep3
            // 
            this.menuItemSep3.Name = "menuItemSep3";
            this.menuItemSep3.Size = new System.Drawing.Size(214, 6);
            // 
            // menu_Exit
            // 
            this.menu_Exit.Image = ((System.Drawing.Image)(resources.GetObject("menu_Exit.Image")));
            this.menu_Exit.Name = "menu_Exit";
            this.menu_Exit.Size = new System.Drawing.Size(217, 26);
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
            // logger
            // 
            this.logger.DataSetName = "Log";
            this.logger.Locale = new System.Globalization.CultureInfo("en-US");
            this.logger.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // m_lblScale
            // 
            this.m_lblScale.BackColor = System.Drawing.Color.Silver;
            this.m_lblScale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblScale.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblScale.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblScale.Location = new System.Drawing.Point(0, 0);
            this.m_lblScale.Name = "m_lblScale";
            this.m_lblScale.Size = new System.Drawing.Size(26, 60);
            this.m_lblScale.TabIndex = 1;
            this.m_lblScale.Text = "1500 k";
            this.m_lblScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblScale.TextDirection = OrientableText.Direction.Clockwise;
            this.m_lblScale.TextOrientation = OrientableText.Orientation.Rotate;
            this.m_lblScale.TextRotationAngle = 270D;
            // 
            // m_PictureBoxGraph
            // 
            this.m_PictureBoxGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PictureBoxGraph.ContextMenuStrip = this.m_ContextMenuMain;
            this.m_PictureBoxGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PictureBoxGraph.Location = new System.Drawing.Point(26, 0);
            this.m_PictureBoxGraph.Name = "m_PictureBoxGraph";
            this.m_PictureBoxGraph.Size = new System.Drawing.Size(154, 60);
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
            // FormLoadGraph
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(22, 49);
            this.ClientSize = new System.Drawing.Size(180, 60);
            this.ControlBox = false;
            this.Controls.Add(this.m_PictureBoxGraph);
            this.Controls.Add(this.m_lblScale);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(61, 53);
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
            ((System.ComponentModel.ISupportInitialize)(this.logger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxGraph)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        int m_hidecounter = 0;
		int m_iCheckIpCount = 0;
        private bool m_bInTimer = false;
        private void m_Timer_Tick(object sender, System.EventArgs e)
		{
            if (m_bInTimer)
                return;
            m_bInTimer = true;
            try
            {
                m_Timer.Stop();
                if (m_Options.HideWhenIdle)
                {
                    //hide form if last 10 counters sum less than 0.5
                    if (GetLastValues(m_send_all_q, 10) < 0.5 &&
                        GetLastValues(m_recv_all_q, 10) < 0.5 &&
                        m_hidecounter++ >= 30)
                    {
                        HideForm(false);
                    }//end if
                    else
                    {
                        ShowForm();
                    }//end else
                }//end if

				//check IP and send email if changed every ~15 min
				if (m_iCheckIpCount % 1000 == 0)
				{
					m_SendIPEmail.SendIPEmailMessageThread();
				}
				m_iCheckIpCount++;

                //LogData();
                DrawGraph();
            }//end try
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine("* Timer tick: " + err.Message);
            }//end catch
            finally
            {
                m_bInTimer = false;
                m_Timer.Start();
            }
        }//end m_Timer_Tick

        //private float GetAvg(List<float> log)
        //{
        //    if (log == null || log.Count == 0)
        //        return 0F;

        //    float total = 0;
        //    foreach (float n in log)
        //        total += n;
        //    float avg = total/log.Count;
        //    log.Clear();
        //    return avg;
        //}//end GetAvg

        //private DateTime m_lastLog = DateTime.Now;
        private Stopwatch _stopper = new Stopwatch();
        private TimeSpan m_EllapsedSec = new TimeSpan(); //update queue every second
        private TimeSpan m_EllapsedMin = new TimeSpan(); //update queue every second

        //private List<float> m_vRcv = new List<float>();
        //private List<float> m_vSnd = new List<float>();


        private void LogData()
		{
            //TimeSpan ts = DateTime.Now - m_lastLog;
            //m_lastLog = DateTime.Now;

            //_stopper.Start();

            TimeSpan tsEllapsed = _stopper.Elapsed - m_EllapsedSec;

            System.Diagnostics.Debug.WriteLine("Time ellapsed: " + tsEllapsed);
            
            if (tsEllapsed < TimeSpan.FromSeconds(1))
                return;

            m_EllapsedSec = _stopper.Elapsed;

            TimeSpan ts1 = _stopper.Elapsed;

            float recv = 0;
            float send = 0;

            try
            {
                recv = m_PerformanceCounterRecv.NextValue();
                send = m_PerformanceCounterSend.NextValue();
            }//end try
            catch (System.InvalidOperationException err1)
            {
                System.Diagnostics.Debug.WriteLine("LogData 1: " + err1.Message);
                return;
            }//end catch
            catch (Exception err2)
            {
                System.Diagnostics.Debug.WriteLine("LogData 2: " + err2.Message);
                return;
            }//end catch

            TimeSpan ts2 = _stopper.Elapsed;

            //get values per second
            //and store it separately
            int iSeconds = (int)tsEllapsed.TotalSeconds;
            for (int i = 0; i < iSeconds; i++)
            {
                m_recv_all_q.Enqueue(recv/iSeconds);
                m_send_all_q.Enqueue(send/iSeconds);
            }//end for

            TimeSpan ts3 = _stopper.Elapsed;
            System.Diagnostics.Debug.WriteLine(string.Format("Time: {0} sec  {1} -- {2}",
                iSeconds, ts2 - ts1, ts3 - ts2));

            ////get values per second
            ////and store it separately
            //for (int i = 0; i < iSeconds; i++)
            //{
            //    try
            //    {
            //        //m_vRcv.Add(m_PerformanceCounterRecv.NextValue());
            //        //m_vSnd.Add(m_PerformanceCounterSend.NextValue());

            //        //float recv = GetAvg(m_vRcv);
            //        //float send = GetAvg(m_vSnd);

            //        ts1 = _stopper.Elapsed;

            //        recv = m_PerformanceCounterRecv.NextValue();
            //        send = m_PerformanceCounterSend.NextValue();

            //        ts2 = _stopper.Elapsed;

            //        m_recv_all_q.Enqueue(recv);
            //        m_send_all_q.Enqueue(send);

            //        ts3 = _stopper.Elapsed;
            //        System.Diagnostics.Debug.WriteLine(string.Format("Time: {0} of {1}  {2} -- {3}",
            //            i, iSeconds, ts2 - ts1, ts3 - ts2));
            //    }//end try
            //    catch (System.InvalidOperationException err1)
            //    {
            //        System.Diagnostics.Debug.WriteLine("LogData 1: " + err1.Message);
            //    }//end catch
            //    catch (Exception err2)
            //    {
            //        System.Diagnostics.Debug.WriteLine("LogData 2: " + err2.Message);
            //    }//end catch
            //}//end for

            UpdateTexts(recv / iSeconds, send / iSeconds);

            TimeSpan tsMinutes = _stopper.Elapsed - m_EllapsedMin;
            if (tsMinutes > TimeSpan.FromMinutes(1.0))
            {
                //per minute
                UpdateDB();
                m_EllapsedMin = _stopper.Elapsed;
            }
		}//end LogData

		private float GetLastValues(Queue<float> queue, int count)
		{
			float[] x = queue.ToArray();
			float total = 0;
            int idxFrom = (x.Length > count) ? x.Length - count : 0 ;
            for (int i = idxFrom; i < x.Length; i++)
            {
                total += x[i];
            }//end for
			return total;
		}//end GetLastValues

		private class FixedSizeQueue<T> : Queue<T> 
		{
			int maxcap; //maximum capability
			public FixedSizeQueue(int maxcap): base(maxcap)
			{
				this.maxcap = maxcap;
			}//end constructor

			public new void Enqueue(T val)
			{
				if ( Count >= maxcap )
					this.Dequeue();
				base.Enqueue(val);
			}//end Enqueue
		}//end class FixedSizeQueue

		private Pen m_penUp;
		private Pen m_penDown;
		private Pen m_penBoth;
		private Pen m_penGrid;
		private Brush m_brushText;

        //these queues contains counters per second
        // - will draw vertical line per second for each value
        private FixedSizeQueue<float> m_recv_all_q;
        private FixedSizeQueue<float> m_send_all_q;

        //insert last m_Options.LogInterval values into one DB value
        //for now sum of last minute(60 sec) is inserted
        //I will save to DB every single minute for reports
        private void UpdateDB()
        {
            m_oleDbInsertCommand.Parameters["Recv"].Value =
                GetLastValues(m_recv_all_q, m_Options.LogInterval);
            m_oleDbInsertCommand.Parameters["Send"].Value =
                GetLastValues(m_send_all_q, m_Options.LogInterval);

            //we keep it open, this and the performance counter SUCKs CPU.
            m_oleDbInsertCommand.ExecuteNonQuery();
        }//end UpdateDB

        private void UpdateTexts(float recv, float send)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke((Action)(() => {UpdateTexts(recv, send);}));
            }
            else
            {
                if (m_Options.ShowWindowCaption)
                    this.Text = GetText(send, recv, false, true);
                else
                    this.Text = "";

                string sTrayText = "DUMeter\n" + GetText(send, recv, false, true);
                if (sTrayText.Length > 63)
                    sTrayText = sTrayText.Substring(0, 63);

                m_NotifyIcon.Text = sTrayText;
                this.m_sGraphText = GetText(send, recv, true, false);
                m_NotifyIcon.Icon = m_vTrayIcons.Get(send, recv, linespeed);
                //this.m_NotifyIcon.ShowBalloon(DUMeterMZ.NotifyIcon.EBalloonIcon.Info, "hello", "title", 3000);
            }
        }//end UpdateTexts

		private void DrawGraph()
		{
			Graphics g = Graphics.FromImage(m_bmp);
			g.Clear(m_Options.Background);
         
            //if ( m_Options.HideWhenIdle )
            //{
            //    //hide form if last 10 counters sum less than 0.5
            //    if (GetLastValues(m_send_all_q, 10) < 0.5 &
            //        GetLastValues(m_recv_all_q, 10) < 0.5 &
            //        m_hidecounter++ >= 600 / m_Options.LogInterval)
            //    {
            //        HideForm(false);
            //    }//end if
            //    else
            //    {
            //        ShowForm();
            //    }//end else
            //}//end if

            //if (m_minute_count == m_Options.LogInterval - 1)
            //{
            //    m_oleDbInsertCommand.Parameters["Recv"].Value = 
            //        GetLastValues(m_recv_all_q, m_Options.LogInterval);
            //    m_oleDbInsertCommand.Parameters["Send"].Value = 
            //        GetLastValues(m_send_all_q, m_Options.LogInterval);

            //    //we keep it open, this and the performance counter SUCKs CPU.
            //    m_oleDbInsertCommand.ExecuteNonQuery();

            //    m_minute_count = 0;
            //}//end if
            //else 
            //{
            //    m_minute_count++;
            //}//end else

            //if ( m_Options.ShowWindowCaption )
            //    this.Text = GetText(send, recv, false, true);
            //else
            //    this.Text = "";

            //m_NotifyIcon.Text = "DUMeter\n"+GetText(send, recv, false, true);
            //this.m_sGraphText = GetText(send, recv, true, false);
            //m_NotifyIcon.Icon = m_vTrayIcons.Get(send, recv, linespeed);
            ////this.m_NotifyIcon.ShowBalloon(DUMeterMZ.NotifyIcon.EBalloonIcon.Info, "hello", "title", 3000);

			float[] rf = m_recv_all_q.ToArray();
            float[] sf = m_send_all_q.ToArray();

			for (int i = 0; i < m_bmp.Width & i < rf.Length; i++)
			{
                float recv2 = rf[rf.Length - (1 + i)];
                float send2 = sf[sf.Length - (1 + i)];

				recv2 /= linespeed;
				send2 /= linespeed;

				float maxr = recv2;
				float minr = send2;
				Pen maxp = m_penDown;
				if (recv2 < send2)
				{
					maxp = m_penUp;
					maxr = send2;
					minr = recv2;
				}//end if
         
				//Pen s = both;

				int max = Convert.ToInt32(m_bmp.Height - (maxr * m_bmp.Height / (1 + m_Options.Overflow)));
				int min = Convert.ToInt32(m_bmp.Height - (minr * m_bmp.Height  / (1 + m_Options.Overflow)));

				if (max < 0)
					max = 0;
				if (min < 0)
					min = 0;

				g.DrawLine(maxp,
					m_bmp.Width - i - 1, m_bmp.Height, 
					m_bmp.Width - i - 1, max);
         
				g.DrawLine(m_penBoth,
					m_bmp.Width - i - 1, m_bmp.Height, 
					m_bmp.Width - i - 1, min);
			}//end for

			m_PictureBoxGraph.Image = m_bmp; 

            g.Dispose();
		}//end DrawGraph

		private string GetText(float up, float down, bool bShort, bool bServer)
		{
			//to show traffic in Kb/KB
            float unit = (int)m_Options.SpeedUnits/1024.0F; 
            string speed_unit = m_Options.SpeedUnits.ToString();

			string txt =
				"Up: " + (up * unit).ToString("#,##0.0") + " " + speed_unit + " " +
				"Down: " + (down * unit).ToString("#,##0.0") + " " + speed_unit;
			if ( bShort )
				txt =
					"U: " + (up * unit).ToString("#,##0.0") + " " +
					"D: " + (down * unit).ToString("#,##0.0");
			if ( bServer )
				txt += " Server: " + m_Options.MachineName;

			return txt;
		}//end GetText

        public static void MessageBox(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, "DUMeterMZ",
                MessageBoxButtons.OK, MessageBoxIcon.Error, 
                MessageBoxDefaultButton.Button1, 
                MessageBoxOptions.ServiceNotification);
        }

        private void m_PictureBoxGraph_Resize(object sender, System.EventArgs e)
		{
			m_bmp = new Bitmap(m_bmp, m_PictureBoxGraph.Width - 2,m_PictureBoxGraph.Height - 2);
		}//end m_PictureBoxGraph_Resize

		private bool mousedown = false;
		private Point mousept;
		private bool briteness = false;

		private void m_PictureBoxGraph_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			switch ( e.Button )
			{
				case MouseButtons.Left:
					briteness = false;
					Cursor = Cursors.SizeAll;
					mousept = new Point(e.X, e.Y);
					mousedown = true;
					break;
				case MouseButtons.Middle:
					if ( briteness )
					{
						Cursor = Cursors.Default;
						briteness = false;
					}//end if
					else 
					{
						Cursor = Cursors.NoMoveVert;
						briteness = true;
					}//end else
					break;
				default:
					break;
			}//end switch
			//clear the counter so it wont hide so quick
			m_hidecounter = 0;
		}//end m_PictureBoxGraph_MouseDown

		private void m_PictureBoxGraph_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( mousedown )
			{
				Opacity = 0.55F;
				Location = new Point(Location.X - (mousept.X - e.X), Location.Y - (mousept.Y - e.Y)); 
			}//end if
		}//end m_PictureBoxGraph_MouseMove

		private void m_PictureBoxGraph_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{      
			if ( e.Button == MouseButtons.Left )
			{
				Cursor = Cursors.Default;
				mousedown = false;
				Opacity = m_Options.Transparency;
			}//end if

			//clear the counter so it wont hide so quick, incase we were holding the mouse button in too long
			m_hidecounter = 0;
		}//end m_PictureBoxGraph_MouseUp

      #region Options and other

		private void menu_Options_Click(object sender, System.EventArgs e)
		{
			ShowOptions();
		}//end menu_Options_Click

		private void ShowOptions()
		{
			FormProperties opt = new FormProperties(m_Options);
			opt.ShowDialog(this);
			opt.Dispose();
			if (m_Options.Interface == "")
			{
				MessageBox("Please select a valid interface");
				ShowOptions();
			}//end if
			else 
			{
                OptionsSerializer.Save(m_sSettingsFile, m_Options);
				GenericSerializer.SaveAs<Options>(m_Options, m_sSettingsFile+".xml");
				ReApplyOptions();
			}//end else
		}//end ShowOptions

		private void ReApplyOptions()
		{
			m_PerformanceCounterRecv.InstanceName	= m_Options.Interface;
			m_PerformanceCounterSend.InstanceName	= m_Options.Interface;
			m_PerformanceCounterRecv.MachineName	= m_Options.MachineName;
			m_PerformanceCounterSend.MachineName	= m_Options.MachineName;
            
			linespeed = (int)m_Options.LineSpeed / 8; //bits to bytes
			
			this.Opacity = m_Options.Transparency;
			this.BackColor = m_Options.Background;
			
			if (m_Options.AlwaysShowText)
				menu_ShowText.Text = "Hide Text";
			else
				menu_ShowText.Text = "Show Text";

            m_lblScale.Text = m_Options.LineSpeed.ToString().Replace('_', ' ');
			m_lblScale.Visible = m_Options.ShowLineSpeedLabel;
			
			if (m_penUp != null) m_penUp.Dispose();
			if (m_penDown != null) m_penDown.Dispose();
			if (m_penBoth != null) m_penBoth.Dispose();
			if (m_penGrid != null) m_penGrid.Dispose();
			if (m_brushText != null) m_brushText.Dispose();

			m_penUp		= new Pen(m_Options.Up);
			m_penDown	= new Pen(m_Options.Down);
			m_penBoth	= new Pen(m_Options.Both);
			m_brushText = new SolidBrush(m_Options.Text);
			m_penGrid	= new Pen(m_Options.Lines);

			m_PictureBoxGraph.BorderStyle = m_Options.BorderStyle;
			this.ControlBox = m_Options.ShowWindowCaption;
		}//end ReApplyOptions

		private void menu_Exit_Click(object sender, System.EventArgs e)
		{
            _close = true;
            //Thread.Sleep(1000);
			Application.Exit();
		}//end menu_Exit_Click

		private void AppExit(object sender, EventArgs e)
		{
			m_OleDbConnection.Close();
            OptionsSerializer.Save(m_sSettingsFile, m_Options);
		}//end AppExit

		private void FormLoadGraph_Move(object sender, System.EventArgs e)
		{
			m_Options.Location = this.Location;
		}//end FormLoadGraph_Move

		private void FormLoadGraph_SizeChanged(object sender, EventArgs e)
		{
			m_Options.Size = this.Size;
		}//end FormLoadGraph_SizeChanged

		private void FormLoadGraph_Load(object sender, System.EventArgs e)
		{
			try 
			{
                OptionsSerializer.Load(m_sSettingsFile, m_Options);

				if ( m_Options.Location.X > 0 || m_Options.Location.Y > 0 )
				{
					this.Size = m_Options.Size;
					this.Location = m_Options.Location;
				}//end if
				else
					this.CenterToScreen();

				ReApplyOptions();
				ShowWindowBorder(m_Options.ShowWindowBorder);
			}//end try
			catch (System.IO.FileNotFoundException)
			{
				ShowOptions();
			}//end catch
            catch (Exception err)
            {
                string msg = err.Message;
                while(err.InnerException!=null)
                {
                    err = err.InnerException;
                    msg = err.Message;
                }
                MessageBox("Load error: "+msg);
                Close();
            }
            finally
			{
				m_SendIPEmail = new SendEmail(m_Options);

				m_Timer.Start();

                _workingThreadCounters = new Thread(st => {

                    while (!_close)
                    {
                        LogData();
                        if(!_close)
                            Thread.Sleep(1000);
                    }

                });
                _stopper.Start();
                _workingThreadCounters.IsBackground = true;
                _workingThreadCounters.Start();

				//ICollection thds = Process.GetCurrentProcess().Threads;
				//foreach (ProcessThread pt in thds)
				//	pt.PriorityLevel = ThreadPriorityLevel.BelowNormal;
			}//end finally
		}//end FormLoadGraph_Load

		private void menu_About_Click(object sender, System.EventArgs e)
		{
			FormAbout frm = new FormAbout();
			frm.ShowDialog(this);
		}//end menu_About_Click

      #endregion

		private void menuShowHide_Click(object sender, System.EventArgs e)
		{
			if ( this.Visible )
				HideForm(true);
			else
				ShowForm();

			//this.menuShowHide.Text = this.Visible ? "Hide" : "Show";
		}//end menuShowHide_Click

		private void menu_resetPosition_Click(object sender, EventArgs e)
		{
			this.CenterToScreen();
			ShowForm();
		}//end menu_resetPosition_Click

		private void menu_Pop_Click(object sender, EventArgs e)
		{
			m_NotifyIcon.Visible = false;
			m_NotifyIcon.Visible = true;
			ShowForm();
		}//end menu_Pop_Click

		//private void menu_Reports_Click(object sender, System.EventArgs e)
		//{
		//	HideForm(true);
		//}//end menu_Reports_Click

		private void HideForm(bool force)
		{
			if (this.Visible || force)
			{
				if (force) m_hidecounter = 600/m_Options.LogInterval;
				//else forewin = GetForeGroundWindow();
				//m_NotifyIcon.Visible = true;
				this.Hide();
				this.menuShowHide.Text = "Show";
			}//end if
		}//end HideForm

		private void ShowForm()
		{
			if ( !Visible )
			{
				m_hidecounter = 0;
				//m_NotifyIcon.Visible = false;

				IntPtr fw = GetForegroundWindow(); 
#if DEBUG
				if (fw == IntPtr.Zero)
				   Trace.WriteLine("Window not get", "GUI");

				if (fw == Handle)
				   Trace.WriteLine("I should not have focus here!", "GUI");
#endif   
				this.Visible = true;
				this.Show();

				SetForegroundWindow(this.Handle);

				//if out of desktop window more than a half
				if (this.Location.X<0 || this.Location.X >= SystemInformation.WorkingArea.Width - this.Width/2)
					this.Left = 100;
				if (this.Location.Y<0 || this.Location.Y >= SystemInformation.WorkingArea.Height - this.Height/2)
					this.Top = 100;

				this.menuShowHide.Text = "Hide";

				if (fw != IntPtr.Zero)
					if (!SetForegroundWindow(fw))
						Trace.WriteLine("Window not set", "GUI");
			}//end if
		}//end ShowForm

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern IntPtr GetForegroundWindow();

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		private void m_NotifyIcon_Click(object sender, System.EventArgs e)
		{
		}//end m_NotifyIcon_Click

		private void m_NotifyIcon_DoubleClick(object sender, System.EventArgs e)
		{
		}//end m_NotifyIcon_DoubleClick
		
		private void m_NotifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
			{
				HideForm(true);
				ShowForm();
			}//end if
		}//end m_NotifyIcon_MouseClick

		private void m_NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			System.Diagnostics.Trace.WriteLine("Main visible: " + this.Visible);

			if ( e.Button == MouseButtons.Left )
			{
				//if ( !this.Visible )
				//	ShowForm();
				//else
				//	HideForm(true);
			}//end if
		}//end m_NotifyIcon_MouseDoubleClick

		private void m_PictureBoxGraph_DoubleClick(object sender, System.EventArgs e)
		{
			bool bShow = (this.FormBorderStyle != FormBorderStyle.SizableToolWindow);
			ShowWindowBorder(bShow);
		}//end m_PictureBoxGraph_DoubleClick

		private void ShowWindowBorder(bool bShow)
		{
			if (!bShow) //hide border
			{
				m_Options.ShowWindowBorder = false;
				this.FormBorderStyle = FormBorderStyle.None;
			}//end if
			else //show border with or without caption
			{
				m_Options.ShowWindowBorder = true;
				this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
				if (m_Options.ShowWindowCaption)
				{
					this.Text = "DUMeterMZ";
					this.ControlBox = true;
				}//end if
				else
				{
					this.Text = ""; //if caption text is empty - will not show caption
					this.ControlBox = false;
				}//end else
			}//end else
		}//end ShowWindowBorder

		private bool hover = false;

		private void m_PictureBoxGraph_MouseHover(object sender, System.EventArgs e)
		{
			hover = true;
		}//end m_PictureBoxGraph_MouseHover

		private void m_PictureBoxGraph_MouseLeave(object sender, System.EventArgs e)
		{
			hover = false;
		}//end m_PictureBoxGraph_MouseLeave

		private void m_PictureBoxGraph_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			//horizontal grid - capacity
			m_penGrid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			e.Graphics.DrawLine(m_penGrid,
				1, Convert.ToInt32(m_bmp.Height * (m_Options.Overflow)),
				m_bmp.Width, Convert.ToInt32(m_bmp.Height * (m_Options.Overflow))
				);

			//draw border
			m_penGrid.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			if ( m_PictureBoxGraph.BorderStyle == BorderStyle.None )
			{
				Rectangle r = e.ClipRectangle;
				r.Width -= 1;
				r.Height -= 1;
				//r.Offset(-1, -1);
				e.Graphics.DrawRectangle(m_penGrid, r);
			}//end if

			//vertical grid
			m_penGrid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			for (int i = 60; i < m_bmp.Width; i+=60)
			{
				e.Graphics.DrawLine(m_penGrid,
					m_bmp.Width - i, 1, m_bmp.Width - i, m_bmp.Height+1);
			}//end for

			if ( hover || m_Options.AlwaysShowText )
			{
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				SizeF s = e.Graphics.MeasureString(m_sGraphText, Font);

				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			
				float xRatio = e.ClipRectangle.Width/s.Width;
				float yRatio = e.ClipRectangle.Height/s.Height;

				float emSize = Font.Size*(Math.Min(xRatio, yRatio)) / 4;
				if ( emSize > e.ClipRectangle.Height ) emSize = e.ClipRectangle.Height - 4 ;
				if ( emSize < 8.0 ) emSize = 8F;
				//System.Diagnostics.Trace.WriteLine("size: "+emSize+" vsize: "+e.ClipRectangle.Height);
				Font font = new Font(Font.FontFamily, emSize);

				e.Graphics.DrawString(m_sGraphText, font, m_brushText, e.ClipRectangle, sf);
				
				if ( m_bPinging )
				{
					sf.Alignment = StringAlignment.Far;
					sf.LineAlignment = StringAlignment.Near;
					e.Graphics.DrawString(m_sLastPingResult, font, m_brushText, e.ClipRectangle, sf);
				}//end if
				
				sf.Dispose();
				font.Dispose();
			}//end if

			if ( briteness )
			{
				Brush selb = new SolidBrush(m_Options.Selection);
				e.Graphics.FillRectangle(selb, 4 ,(float)(4 + (m_bmp.Height)*(1 - Opacity)),
					m_bmp.Width/20F, (m_bmp.Height - 10) - (float)((m_bmp.Height)*(1 - Opacity)));
				selb.Dispose();
			}//end if
		}//end m_PictureBoxGraph_Paint

		private void menu_ShowText_Click(object sender, System.EventArgs e)
		{
			if ( m_Options.AlwaysShowText )
			{
				m_Options.AlwaysShowText = false;
				menu_ShowText.Text = "Show Text";
			}//end if
			else 
			{
				m_Options.AlwaysShowText = true;
				menu_ShowText.Text = "Hide Text";
			}//end else
		}//end menu_ShowText_Click

      #region ReportInit

		private void menu_Reports_LastHour_Click(object sender, System.EventArgs e)
		{
			MakeReport(1, menu_Reports_LastHour.Image);
		}//end menu_Reports_LastHour_Click

		private void MakeReport(int from, Image img)
		{
			m_oleDbSelectCommand.Parameters["ID"].Value = DateTime.Now.AddHours(-from);
			int res = m_OleDbDataAdapter.Fill(logger.RateLog);
			ReportForm report = new ReportForm(from, logger.RateLog, img, m_Options);
			Bitmap bmp = new Bitmap(img);
			report.Icon = Icon.FromHandle(bmp.GetHicon());
			report.Show();
		}//end MakeReport

		private void menu_Reports_Last12Hours_Click(object sender, System.EventArgs e)
		{
			MakeReport(12, menu_Reports_Last12Hours.Image);
		}//end menu_Reports_Last12Hours_Click

		private void menu_Reports_Last24Hours_Click(object sender, System.EventArgs e)
		{
			MakeReport(24, menu_Reports_Last24Hours.Image);
		}//end menu_Reports_Last24Hours_Click

		private void menu_Reports_Last3Days_Click(object sender, System.EventArgs e)
		{
			MakeReport(72, menu_Reports_Last3Days.Image);
		}//end menu_Reports_Last3Days_Click

		private void menu_Reports_Last3Hours_Click(object sender, System.EventArgs e)
		{
			MakeReport(3, menu_Reports_Last3Hours.Image);
		}//end menu_Reports_Last3Hours_Click

		private void menu_Reports_Last6Hours_Click(object sender, System.EventArgs e)
		{
			MakeReport(6, menu_Reports_Last6Hours.Image);
		}
      #endregion

      #region Pinger

		Thread m_PingThread;
		volatile bool m_bPinging = false;
		string m_sLastPingResult = "";

		private void StopPing()
		{
			m_sLastPingResult = "";
			if ( !m_bPinging )
				return;

			m_bPinging = false;
			m_PingThread = null;

			menu_Ping.Text = "Start ping";
			menu_Ping.Checked = false;
		}//end StopPing

		private void StartPing()
		{
			if (m_PingThread == null)
				InitThread();

			menu_Ping.Text = "Stop ping";
			menu_Ping.Checked = true;
			
			m_bPinging = true;
			m_sLastPingResult = "Start Pinging...";
			m_PingThread.Start();
		}//end StartPing


		private void StartPingThread()
		{
			while (m_bPinging)
			{
				Thread.Sleep(m_Options.PingInterval);
				try
				{
					long roundtrip = PingImplementation.PingWrapper.Ping(m_Options.PingEndPoint);
					m_sLastPingResult = string.Format("{0} {1} ms", 
						m_Options.PingEndPoint, roundtrip.ToString("#,##0"));
				}//end try
				catch (Exception err)
				{
					m_sLastPingResult = string.Format("{0} {1}", m_Options.PingEndPoint, err.Message); 
				}//end catch
			}//end while
		}//end StartPingThread

		private void menu_Ping_Click(object sender, System.EventArgs e)
		{
			if ( m_bPinging )
				StopPing();
			else
				StartPing();
		}//end menu_Ping_Click

		private void InitThread()
		{
			m_PingThread = new Thread(new ThreadStart(StartPingThread));
			m_PingThread.Name = "Pinger";
		}//end InitThread
	  #endregion Pinger

	}//end class FormLoadGraph
}//end namespace DUMeterMZ
