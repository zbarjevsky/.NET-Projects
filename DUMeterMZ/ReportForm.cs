using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DUMeterMZ.Common;

namespace DUMeterMZ
{
	/// <summary>
	/// Summary description for ReportForm.
	/// </summary>
	public partial class ReportForm : System.Windows.Forms.Form
	{
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
		DateTime m_dtTo = DateTime.Now;
		DateTime m_dtFrom = DateTime.Now.AddHours(-1);

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

			this.Text = String.Format("Report from {0} to {1}. Period: {2} - {3}",
			   m_dtFrom, m_dtTo, Period(m_dtTo - m_dtFrom), Utils.AppName);

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
			//performance lines count for current line speed - horizontal lines/grid lines
			double iSpeedLinesCount = (8F * m_linespeed) / (float)LineSpeed.ISDN_128k;
			if (8F * m_linespeed >= (float)LineSpeed.LAN_10M) //for big bandwidths line per 1M
				iSpeedLinesCount = (8F * m_linespeed) / (float)LineSpeed.DSL_1M;
			if (8F * m_linespeed >= (float)LineSpeed.LAN_100M) //for big bandwidths line per 10M
				iSpeedLinesCount = (8F * m_linespeed) / (float)LineSpeed.LAN_10M;
			if (8F * m_linespeed >= (float)LineSpeed.LAN_1GB) //for big bandwidths line per 100M
				iSpeedLinesCount = (8F * m_linespeed) / (float)LineSpeed.LAN_100M;

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
