using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DUMeterMZ
{
	/// <summary>
	/// Summary description for ReportForm.
	/// </summary>
	public class ReportForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBoxReport;
		private System.ComponentModel.IContainer components;
		float m_hours = 1F;
		float m_initialHours = 1F;
		DUMeterMZ.Log.RateLogDataTable m_LogTable;
		Bitmap m_bmpGraph;
		Pen m_penGrid;	//grid lines
		Pen m_penRecv;	//receive
		Pen m_penSend;	//send
		Pen m_penBoth;	//both
		Pen m_penLineSpeed;	//time line
		Brush m_brushBkText = null;
		int m_linespeed = 1024;
		private System.Windows.Forms.ToolTip m_toolTip;
		DateTime m_dtTo = DateTime.Now;
		DateTime m_dtFrom = DateTime.Now.AddHours(-1);
		private StatusStrip m_statusStrip;
		private ToolStripStatusLabel m_statusStripLabel1;
		private ToolStripStatusLabel m_statusStripLabel2;
		private ToolStripStatusLabel m_statusStripLabel3;
		private ContextMenuStrip m_contextMenuStrip;
		private ToolStripMenuItem m_ctxMenu_Restore;
		private ToolStripMenuItem m_ctxMenu_ShowSelection;
		private ToolStripSeparator m_ctxMenu_Sep1;
		private ToolStripMenuItem m_ctxMenu_Close;

		Options m_Options;

		public ReportForm(float hours, Log.RateLogDataTable table, Image img, Options options)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_ctxMenu_Restore.Image = img;

			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			m_dtFrom				= m_dtTo.AddHours(-hours);
			this.m_Options			= options;
			this.m_LogTable			= table;
			this.m_hours			= hours;
			this.m_initialHours		= hours;
			m_linespeed				= (int)options.LineSpeed / 8;
			int width				= 720;

			m_bmpGraph				= new Bitmap(width, pictureBoxReport.Height);
			m_penGrid				= new Pen(options.Lines, 1);
			m_penRecv				= new Pen(options.Down);
			m_penSend				= new Pen(options.Up);
			m_penBoth				= new Pen(options.Both);
			
			m_penLineSpeed			= new Pen(Color.Pink, 1);
			m_penLineSpeed.DashStyle= System.Drawing.Drawing2D.DashStyle.DashDotDot;
			
			m_brushBkText			= new SolidBrush(options.Background);

			pictureBoxReport.Image	= m_bmpGraph;
		}//end constructor

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				m_penGrid.Dispose();
				m_penRecv.Dispose();
				m_penBoth.Dispose();
				m_penSend.Dispose();
				m_penLineSpeed.Dispose();
				m_brushBkText.Dispose();

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
			this.pictureBoxReport = new System.Windows.Forms.PictureBox();
			this.m_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.m_ctxMenu_Restore = new System.Windows.Forms.ToolStripMenuItem();
			this.m_ctxMenu_ShowSelection = new System.Windows.Forms.ToolStripMenuItem();
			this.m_ctxMenu_Sep1 = new System.Windows.Forms.ToolStripSeparator();
			this.m_ctxMenu_Close = new System.Windows.Forms.ToolStripMenuItem();
			this.m_toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.m_statusStrip = new System.Windows.Forms.StatusStrip();
			this.m_statusStripLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.m_statusStripLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.m_statusStripLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxReport)).BeginInit();
			this.m_contextMenuStrip.SuspendLayout();
			this.m_statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBoxReport
			// 
			this.pictureBoxReport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBoxReport.ContextMenuStrip = this.m_contextMenuStrip;
			this.pictureBoxReport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxReport.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxReport.Name = "pictureBoxReport";
			this.pictureBoxReport.Size = new System.Drawing.Size(716, 202);
			this.pictureBoxReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxReport.TabIndex = 0;
			this.pictureBoxReport.TabStop = false;
			this.pictureBoxReport.DoubleClick += new System.EventHandler(this.pictureBoxReport_DoubleClick);
			this.pictureBoxReport.Click += new System.EventHandler(this.pictureBoxReport_Click);
			this.pictureBoxReport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxReport_MouseDown);
			this.pictureBoxReport.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxReport_MouseMove);
			this.pictureBoxReport.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxReport_Paint);
			this.pictureBoxReport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxReport_MouseUp);
			// 
			// m_contextMenuStrip
			// 
			this.m_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ctxMenu_ShowSelection,
            this.m_ctxMenu_Restore,
            this.m_ctxMenu_Sep1,
            this.m_ctxMenu_Close});
			this.m_contextMenuStrip.Name = "m_contextMenuStrip";
			this.m_contextMenuStrip.Size = new System.Drawing.Size(188, 98);
			// 
			// m_ctxMenu_Restore
			// 
			this.m_ctxMenu_Restore.Enabled = false;
			this.m_ctxMenu_Restore.Image = ((System.Drawing.Image)(resources.GetObject("m_ctxMenu_Restore.Image")));
			this.m_ctxMenu_Restore.Name = "m_ctxMenu_Restore";
			this.m_ctxMenu_Restore.Size = new System.Drawing.Size(187, 22);
			this.m_ctxMenu_Restore.Text = "&Restore Original Report";
			this.m_ctxMenu_Restore.Click += new System.EventHandler(this.m_ctxMenu_Restore_Click);
			// 
			// m_ctxMenu_ShowSelection
			// 
			this.m_ctxMenu_ShowSelection.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.m_ctxMenu_ShowSelection.Image = ((System.Drawing.Image)(resources.GetObject("m_ctxMenu_ShowSelection.Image")));
			this.m_ctxMenu_ShowSelection.Name = "m_ctxMenu_ShowSelection";
			this.m_ctxMenu_ShowSelection.Size = new System.Drawing.Size(187, 22);
			this.m_ctxMenu_ShowSelection.Text = "&Show Selection";
			this.m_ctxMenu_ShowSelection.Click += new System.EventHandler(this.m_ctxMenu_ShowSelection_Click);
			// 
			// m_ctxMenu_Sep1
			// 
			this.m_ctxMenu_Sep1.Name = "m_ctxMenu_Sep1";
			this.m_ctxMenu_Sep1.Size = new System.Drawing.Size(184, 6);
			// 
			// m_ctxMenu_Close
			// 
			this.m_ctxMenu_Close.Image = ((System.Drawing.Image)(resources.GetObject("m_ctxMenu_Close.Image")));
			this.m_ctxMenu_Close.Name = "m_ctxMenu_Close";
			this.m_ctxMenu_Close.Size = new System.Drawing.Size(187, 22);
			this.m_ctxMenu_Close.Text = "&Close Report";
			this.m_ctxMenu_Close.Click += new System.EventHandler(this.m_ctxMenu_Close_Click);
			// 
			// m_toolTip
			// 
			this.m_toolTip.AutomaticDelay = 0;
			this.m_toolTip.UseFading = true;
			this.m_toolTip.ToolTipIcon = ToolTipIcon.Info;
			this.m_toolTip.UseAnimation = true;
			this.m_toolTip.ReshowDelay = 10;
			this.m_toolTip.AutoPopDelay = 30000;
			this.m_toolTip.ShowAlways = true;
			// 
			// m_statusStrip
			// 
			this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_statusStripLabel1,
            this.m_statusStripLabel2,
            this.m_statusStripLabel3});
			this.m_statusStrip.Location = new System.Drawing.Point(0, 202);
			this.m_statusStrip.Name = "m_statusStrip";
			this.m_statusStrip.Size = new System.Drawing.Size(716, 22);
			this.m_statusStrip.TabIndex = 1;
			this.m_statusStrip.Text = "Ready";
			// 
			// m_statusStripLabel1
			// 
			this.m_statusStripLabel1.Name = "m_statusStripLabel1";
			this.m_statusStripLabel1.Size = new System.Drawing.Size(673, 17);
			this.m_statusStripLabel1.Spring = true;
			this.m_statusStripLabel1.Text = "Ready";
			this.m_statusStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// m_statusStripLabel2
			// 
			this.m_statusStripLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.m_statusStripLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.m_statusStripLabel2.Name = "m_statusStripLabel2";
			this.m_statusStripLabel2.Size = new System.Drawing.Size(14, 17);
			this.m_statusStripLabel2.Text = " ";
			// 
			// m_statusStripLabel3
			// 
			this.m_statusStripLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.m_statusStripLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.m_statusStripLabel3.Name = "m_statusStripLabel3";
			this.m_statusStripLabel3.Size = new System.Drawing.Size(14, 17);
			this.m_statusStripLabel3.Text = " ";
			// 
			// ReportForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(716, 224);
			this.Controls.Add(this.pictureBoxReport);
			this.Controls.Add(this.m_statusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(200, 60);
			this.Name = "ReportForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ReportForm";
			this.Resize += new System.EventHandler(this.ReportForm_Resize);
			this.Load += new System.EventHandler(this.ReportForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxReport)).EndInit();
			this.m_contextMenuStrip.ResumeLayout(false);
			this.m_statusStrip.ResumeLayout(false);
			this.m_statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void ReportForm_Load(object sender, System.EventArgs e)
		{
			DrawGraph();
			SetToolTip();
		}//end ReportForm_Load

		private void DrawGraph()
		{
			Graphics g = Graphics.FromImage(m_bmpGraph);
			g.Clear(m_Options.Background);

			float maxRecv = 0;
			float maxSend = 0;
			foreach (Log.RateLogRow row in m_LogTable)
			{
				if ( (row.ID >= m_dtFrom) && (row.ID < m_dtTo) )
				{
					if ( row.Recv > maxRecv ) maxRecv = row.Recv;
					if ( row.Send > maxSend ) maxSend = row.Send;

					float maxr = row.Recv;
					float minr = row.Send;
					Pen p = m_penRecv;
					if ( row.Recv < row.Send )
					{
						p = m_penSend;
						maxr = row.Send;
						minr = row.Recv;
					}//end if

					float w = m_bmpGraph.Width / (m_hours * 60F);
					float x = (float)((row.ID - m_dtTo.AddHours(-m_hours)).TotalMinutes * w);
					x -= w / 2;
					int h = m_bmpGraph.Height;
					float hMaxR = (float)(h - h * ((maxr / 60) / m_linespeed) / (1 + m_Options.Overflow));
					float hMinR = (float)(h - h * ((minr / 60) / m_linespeed) / (1 + m_Options.Overflow));

					p.Width = w;
					m_penBoth.Width = w;
					g.DrawLine(p, x, h, x, hMaxR);
					g.DrawLine(m_penBoth, x, h, x, hMinR);
				}//end if
			}//end foreach

			this.Text = String.Format("Report from {0} to {1}. Period: {2}",
			   m_dtFrom, m_dtTo, Period(m_dtTo - m_dtFrom));

			m_statusStripLabel2.Text = "max down: " + (maxRecv / (60F * 1024F)).ToString("#,#0.0") + " KB";
			m_statusStripLabel3.Text = "max up: " + (maxSend/(60F*1024F)).ToString("#,#0.0") + " KB";

			pictureBoxReport.Image = m_bmpGraph;
			g.Dispose();
		}//end DrawGraph

		private void GetSelection(out int left, out int right)
		{
			left = m_ptSelStart.X;
			right = m_ptSelEnd.X;
			if (left > right)
			{
				left = m_ptSelEnd.X;
				right = m_ptSelStart.X;
			}//end if
			if ( left < 0 ) left = 0;
			if ( right > m_bmpGraph.Width ) right = m_bmpGraph.Width;
		}//end GetSelection

		private void UpdateSelectionStatus()
		{
			int l, r;
			GetSelection(out l, out r);

			if ( r - l > 4 )
			{
				float minutewidth = m_bmpGraph.Width / (m_hours * 60F);

				DateTime from = this.m_dtTo.AddHours(-m_hours).AddMinutes((l / minutewidth));
				DateTime to = this.m_dtTo.AddHours(-m_hours).AddMinutes((r / minutewidth));
				
				m_statusStripLabel2.Text = "From: " + from.ToString();
				m_statusStripLabel3.Text = "To: " + to.ToString();
			}//end if 
			else
			{
				m_statusStripLabel2.Text = "From: " + m_dtFrom.ToString();
				m_statusStripLabel3.Text = "To: " + m_dtTo.ToString();
			}//end else
		}//end UpdateSelectionStatus

		private void pictureBoxReport_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			//performance lines count for current line speed
			double iSpeedLinesCount = (8F * m_linespeed) / (float)LineSpeed.DSL_512k;
			if (8F * m_linespeed >= (float)LineSpeed.LAN_10m) //for big bandwidths line per 5M
				iSpeedLinesCount = (8F * m_linespeed) / (float)LineSpeed.DSL_5m;

			//pixel count interval between lines
			double iYPixelsPerLine = m_bmpGraph.Height / (iSpeedLinesCount * (1 + m_Options.Overflow));
			int delta = (int)iYPixelsPerLine;
			if (delta < 1) delta = 1;

			//Line Limit Speed
			float fLimitLine = (float)(m_Options.Overflow * m_bmpGraph.Height / (1 + m_Options.Overflow) + 1F);
			int iLimitLine = (int)fLimitLine;
			if ((double)delta != iYPixelsPerLine) //small error - proximate fix
				iLimitLine += (int)(iSpeedLinesCount * (iYPixelsPerLine - (double)delta));
			
			m_penGrid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			for (int j = m_bmpGraph.Height - delta, idx = 1; j >= 0; j -= delta, idx++)
			{
				if (iLimitLine >= j - 2 && iLimitLine <= j) iLimitLine = j - 1; //to overlap
				e.Graphics.DrawLine(m_penGrid, 0, j - 1, m_bmpGraph.Width, j - 1); //horizontal line
				if (idx % 2 == 0) //every second line
				{
					if (iLimitLine == j + 1 || iLimitLine == j - 1) iLimitLine = j; //between the lines
					e.Graphics.DrawLine(m_penGrid, 0, j + 1, m_bmpGraph.Width, j + 1); //horizontal line
				}//end if
			}//end for

			e.Graphics.DrawLine(m_penLineSpeed, 0, iLimitLine, m_bmpGraph.Width, iLimitLine); //horizontal line

			m_penGrid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

			//draw hours tics
			double hours_per_pixel = m_hours / m_bmpGraph.Width;
			int pixels_per_hour = (int)(m_bmpGraph.Width / m_hours);
			
			int prev_day = m_dtFrom.Day;
			int prev_hour = m_dtFrom.Hour;
			int prev_half_hour = m_dtFrom.Minute / 30;

			for ( int i = 1; i < m_bmpGraph.Width; i++ )
			{
				DateTime dtCurr = m_dtFrom.AddHours(i * hours_per_pixel);
				if ( m_hours > 12 )
				{
					if ( prev_day != dtCurr.Day )
					{
						int half_day = pixels_per_hour * 12;
						prev_day = dtCurr.Day;

						DrawTimerTick(e.Graphics, i, dtCurr, half_day);
					}//end if
				}//end if
				else if ( m_hours > 3 )
				{
					if ( prev_hour != dtCurr.Hour )
					{
						int half_hour = pixels_per_hour / 2;
						prev_hour = dtCurr.Hour;

						DrawTimerTick(e.Graphics, i, dtCurr, half_hour);
					}//end if
				}//else if
				else //less than 3 hours
				{
					if ( prev_half_hour != dtCurr.Minute / 30 )
					{
						int qurter_hour = pixels_per_hour / 4;
						prev_half_hour = dtCurr.Minute / 30;

						DrawTimerTick(e.Graphics, i, dtCurr, qurter_hour);
					}//end if
				}//end if
			}//end for

			UpdateSelectionStatus();

			if ( selected ) //draw selection rectangle
			{
				int l, r;
				GetSelection(out l, out r);
				Rectangle dr = new Rectangle(l, 0, r - l, m_bmpGraph.Height - 1);

				//selection fill
				Color c = Color.FromArgb(50, m_Options.Selection);
				Brush selb = new SolidBrush(c);
				e.Graphics.FillRectangle(selb, dr);
				
				//selection border
				Pen selp = new Pen(m_Options.Selection, 1);
				e.Graphics.DrawRectangle(selp, dr);

				selb.Dispose();
				selp.Dispose();
			}//end if
			//else if ( mouseover )
			//{
			//	  int inc = 60;
			//    Point m = m_ptMouseCurr;//pictureBoxReport.PointToClient(MousePosition);
			//    int section = m.X / inc;

			//    float minutewidth = m_bmpGraph.Width / (m_hours * 60F);

			//    int l = (inc) * section,
			//       r = (inc) * (section + 1);
			//    //GetSelection(out l, out r);
			//    Rectangle dr = new Rectangle(l, 0, r - l, m_bmpGraph.Height - 1);
				
			//    Color c = Color.FromArgb(50, options.Selection);
			//    Brush selb = new SolidBrush(c);
			//    e.Graphics.FillRectangle(selb, dr);
				
			//    Pen selp = new Pen(options.Selection, 1);
			//    e.Graphics.DrawRectangle(selp, dr);
				
			//    selb.Dispose();
			//    selp.Dispose();
			//}//end else if
		}//end pictureBoxReport_Paint

		private void DrawTimerTick(Graphics g, int x, DateTime dtStamp, int delta)
		{
			string label = dtStamp.ToString("HH:mm");
			if ( dtStamp.Hour == 0 )
				label = dtStamp.ToString("MMMM dd");

			g.DrawLine(m_penGrid, x, 0, x, m_bmpGraph.Height); //vertical line
			g.DrawLine(m_penGrid, x + 1, 0, x + 1, m_bmpGraph.Height); //vertical line

			if ( x >= delta && x <= delta*2 )
				g.DrawLine(m_penGrid, x - delta, 0, x - delta, m_bmpGraph.Height); //vertical line prev 1/2
			g.DrawLine(m_penGrid, x + delta, 0, x + delta, m_bmpGraph.Height); //vertical line next 1/2

			SizeF szText = g.MeasureString(label, Font);
			Rectangle rcText = new Rectangle(
				(int)(x - szText.Width / 2), (int)(m_bmpGraph.Height / 2 - szText.Height / 2),
				(int)szText.Width, (int)szText.Height);

			//clear text area
			g.FillRectangle(m_brushBkText, rcText); 
			g.DrawString(label, this.Font, Brushes.Black, rcText.Left, rcText.Top);
		}//end DrawTimerTick

		private void ReportForm_Resize(object sender, System.EventArgs e)
		{
			if ( WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized )
			{
				m_bmpGraph = new Bitmap(pictureBoxReport.Width - 2, pictureBoxReport.Height - 2);
				selected = false;
				DrawGraph();
			}//end if
//			mouseover = false;
		}//end ReportForm_Resize

		private void pictureBoxReport_DoubleClick(object sender, System.EventArgs e)
		{
			//if (FormBorderStyle != FormBorderStyle.SizableToolWindow)
			//    FormBorderStyle = FormBorderStyle.SizableToolWindow;
			//else
			//    FormBorderStyle = FormBorderStyle.None;
		}//end pictureBoxReport_DoubleClick

		bool mousedown = false;
		bool selecting = false;
		bool selected = false;
		//bool mouseover = false;

		Point m_ptSelStart;
		Point m_ptSelEnd;

		Point m_ptMouseDown;
		Point m_ptMouseCurr;

		private void pictureBoxReport_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( selected && e.Button == MouseButtons.Left )
			{
				int l, r;
				GetSelection(out l, out r);
				//if left click inside selected - resize to selection
				if ( e.X > l && e.X < r )
				{
					ResizeToSelection(l, r);
					return;
				}//end if
			}//end if selected

			//left click out of selection
			if ( e.Button == MouseButtons.Left )
			{
				selected = false;
				selecting = false;
			}//end if
			
			pictureBoxReport.Invalidate();

			mousedown = true;
			m_ptMouseDown = new Point(e.X, e.Y);

			switch ( e.Button )
			{
				case MouseButtons.Right:
					//mouseover = true;
					break;
				case MouseButtons.Left: //start selecting on left click
					m_ptSelStart = new Point(e.X, e.Y);
					selecting = true;
					m_statusStripLabel1.Text = "Start Selecting...";
					break;
			}//end switch

			UpdateSelectionStatus();
		}//end pictureBoxReport_MouseDown

		private void pictureBoxReport_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( mousedown )
			{
				//Location = new Point(Location.X - (m_ptMouseDown.X - e.X), Location.Y - (m_ptMouseDown.Y - e.Y));
			}//end if

			if ( selecting )
			{
				m_ptSelEnd = new Point(e.X, e.Y);
				selected = true;
				SetToolTip();

				m_statusStripLabel3.Text = e.X.ToString();

				pictureBoxReport.Invalidate();
			}//end else if
			else if ( selected )
			{
				int l, r;
				GetSelection(out l, out r);
				if ( e.X > l && r > e.X )
				{
					Cursor = Cursors.Hand;
					m_ctxMenu_ShowSelection.Enabled = true;
				}//end if
				else
				{
					Cursor = Cursors.Default;
					m_ctxMenu_ShowSelection.Enabled = false;
				}//else
			}//end else if
			else
			{
				Cursor = Cursors.Default;
				m_ctxMenu_ShowSelection.Enabled = false;
			}//end else

			UpdateSelectionStatus();

			m_ptMouseCurr = new Point(e.X, e.Y);
		}//end pictureBoxReport_MouseMove

		private void pictureBoxReport_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					if ( selecting )
					{
						m_ptSelEnd = new Point(e.X, e.Y);
						selecting = false;

						int l, r;
						GetSelection(out l, out r);
						if ( r - l > 4 ) //some selected lines
						{
							m_statusStripLabel1.Text = "Selected";
							selected = true;
						}//end if
						else
						{
							m_statusStripLabel1.Text = "Selected None";
							selected = false;
						}//end else
						pictureBoxReport.Invalidate();
						SetToolTip();
					}//end if
					break;
				default:
					m_statusStripLabel1.Text = "Ready";
					break;
			}//end switch

			UpdateSelectionStatus();

			mousedown = false;
		}//end pictureBoxReport_MouseUp

		private void pictureBoxReport_MouseHover(object sender, System.EventArgs e)
		{
//			mouseover = true;
		}//end pictureBoxReport_MouseHover

		private void ResizeToSelection(int left, int right)
		{
			float minutewidth = m_bmpGraph.Width / (m_hours * 60F);

			m_dtFrom = m_dtTo.AddHours(-m_hours).AddMinutes((left / minutewidth));
			m_dtTo = m_dtTo.AddHours(-m_hours).AddMinutes((right / minutewidth));

			TimeSpan delta = m_dtTo - m_dtFrom;
			m_hours = (float)(delta).TotalHours;

			DrawGraph();

			selected = false;
			selecting = false;
			Cursor = Cursors.Default;

			m_statusStripLabel1.Text = "Show selection";
			pictureBoxReport.Invalidate();

			m_ctxMenu_Restore.Enabled = true;
		}//end ResizeToSelection

		private void SetToolTip()
		{
			//dont confuse these with the members
			DateTime from = m_dtFrom;
			DateTime to = m_dtTo;

			float minutewidth = m_bmpGraph.Width / (m_hours * 60F);

			float rtot = 0; //receive total
			float stot = 0; //send total

			int counter = 0; //dammit :) not 0 , NOT 1 but 0 infact

			if ( selected )
			{
				int l, r;
				GetSelection(out l, out r);

				from = this.m_dtTo.AddHours(-m_hours).AddMinutes(l / minutewidth);
				to = this.m_dtTo.AddHours(-m_hours).AddMinutes(1 + r / minutewidth);
			}//end if

			foreach (Log.RateLogRow row in m_LogTable)
			{
				if ( row.ID >= from && row.ID < to )
				{
					rtot += row.Recv;
					stot += row.Send;
					counter++;
				}//end if
			}//end foreach

			float unit = (m_Options.SpeedUnits == SpeedUnits.KBits) ? 8F : 1F;

			//normalize to KB
			rtot /= 1024F;
			stot /= 1024F;

			string sep = " - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n";

			TimeSpan interval = to - from;
			string tt = String.Format(
				(selected ? "Selected Period: \t" : "Period: \t\t\t") +
				"{0}\n" +
				"Samples: \t\t{1}\n" +
				sep +
				"From:\t\t\t{2}\n" +
				"To:\t\t\t{3}\n" +
				sep+
				"Total Down:\t\t{4}  KB\n" +
				"Total Up:\t\t{5}  KB\n" +
				sep +
				"Avg Down:\t\t{6}  {8}/sec\n" +
				"Avg Up:\t\t\t{7}  {8}/sec\n",
			   Period(interval),
			   counter,
			   from,
			   to,
			   FormatFloat(rtot, 12),
			   FormatFloat(stot, 12),
			   FormatFloat((counter == 0 ? 0F : (float)(unit * rtot / interval.TotalSeconds)), 12),
			   FormatFloat((counter == 0 ? 0F : (float)(unit * stot / interval.TotalSeconds)), 12), 
			   m_Options.SpeedUnits
			   );

			m_toolTip.RemoveAll();
			m_toolTip.SetToolTip(pictureBoxReport, tt);
			m_toolTip.ToolTipTitle = selected ? "Selected Traffic" : "Traffic" ;

			m_statusStripLabel1.Text = tt.Substring(0, tt.IndexOf("\n"));
		}//end SetToolTip

		//right align numbers
		private string FormatFloat(float f, int nPlaces)
		{
			string s = f.ToString("#,#0.0");

			//count commas - need extra space
			int idx = -1;
			int comma = 0;
			while ( (idx = s.IndexOfAny(new char[] { ',' }, idx+1)) >= 0 )
				comma++;
			
			//2 spaces per free space + 1 space per comma
			int rem = 2*(nPlaces - s.Length) + comma;
			while ( rem-- > 0 )
				s = " " + s;
			return s;
		}//end FormatFloat

		private string Period(TimeSpan d)
		{
			if ( d.TotalMinutes < 60 )
			{
				return FormatCounters(0, "", d.Minutes, "minute", d.Seconds, "second");
			}//end if
			else if ( d.TotalDays < 1 )
			{
				return FormatCounters(0, "", d.Hours, "hour", d.Minutes, "minute");
			}//end else if
			else
			{
				return FormatCounters((int)d.TotalDays, "day", d.Hours, "hour", d.Minutes, "minute");
			}//end else
		}//end Period

		private string FormatCounters(int x, string xSuffix, int y, string ySuffix, int z, string zSuffix)
		{
			int[] iv = new int[] { x, y, z };
			string [] sv = new string[] { xSuffix, ySuffix, zSuffix };
			return FormatCounters(iv, sv);
		}//end FormatCounters

		private string FormatCounters(int[] ivValue, string[] svSuffix)
		{
			//go from the begin and from end - remove all consequent zeroes
			int iStartIdx = 0;
			int iEndIdx = ivValue.Length-1;
			for ( int i = 0; i < ivValue.Length; i++ )
				if ( ivValue[i] > 0 )
				{
					iStartIdx = i;
					break;
				}//end if

			for ( int i = ivValue.Length - 1; i >= 0; i-- )
				if ( ivValue[i] > 0 )
				{
					iEndIdx = i;
					break;
				}//end if

			if ( iStartIdx > iEndIdx )
				return "0";

			string s = "";
			for ( int i = iStartIdx; i <= iEndIdx ; i++ )
				s += FormatCounter(ivValue[i], " " + svSuffix[i])+" ";
			return s;
		}//end FormatCounters

		private string FormatCounter(int x, string suffix)
		{
			return (x + suffix) + ((x>1||x==0) ? "s" : "");
		}//end FormatCounter

		private string FormatCounter(double x, string prefix)
		{
			return (x.ToString("f0") + prefix) + ((x>=2||x==0) ? "s" : "");
		}//end FormatCounter

		private void pictureBoxReport_Click(object sender, System.EventArgs e)
		{
			SetToolTip();
		}//end pictureBoxReport_Click

		private void EnableCtxMenu(bool bOnSelection)
		{
			m_ctxMenu_ShowSelection.Enabled = bOnSelection;
		}//end EnableCtxMenu

		private void m_ctxMenu_Restore_Click(object sender, EventArgs e)
		{
			m_hours = m_initialHours;
			m_dtTo = DateTime.Now;
			m_dtFrom = m_dtTo.AddHours(-m_hours);
			
			DrawGraph();
			pictureBoxReport.Invalidate();

			selected = false;
			selecting = false;

			m_ctxMenu_Restore.Enabled = false;

			Cursor = Cursors.Default;
		}//end m_ctxMenu_Restore_Click

		private void m_ctxMenu_ShowSelection_Click(object sender, EventArgs e)
		{
			int l, r;
			GetSelection(out l, out r);
			ResizeToSelection(l, r);
		}//end m_ctxMenu_ShowSelection_Click

		private void m_ctxMenu_Close_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}//end class ReportForm
}//end namespace DUMeterMZ
