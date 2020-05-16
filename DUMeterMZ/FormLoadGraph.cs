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
using DUMeterMZ.Common;
using MarkZ.Common;

namespace DUMeterMZ
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public partial class FormLoadGraph : System.Windows.Forms.Form
	{
        private Thread _workingThreadCounters;
        private bool _close = false;

        private Bitmap              m_bmp;
        private int					linespeed	= (int)LineSpeed.DSL_1MB;
        private Options				m_Options;

        private string m_sSettingsFile = "DUMeterMZ.config";

		private SendEmail m_SendIPEmail;

		private string	m_sGraphText = "Initializing...";

		private ScreenSaver _screenSaver;

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

			string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;

			string sDirectory = Path.Combine(path, Utils.AppName); //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			Directory.CreateDirectory(sDirectory);

            m_sSettingsFile = Path.Combine(sDirectory, m_sSettingsFile);

			string dataBaseFileName = Path.Combine(sDirectory, "log.mdb");

			if (!(File.Exists(dataBaseFileName)))
			{
				Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("DUMeterMZ.log.mdb");
                Stream r = File.Create(dataBaseFileName);
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
			m_PictureBoxGraph_Resize(this, null);
			m_NotifyIcon.Icon = m_vTrayIcons[3];
			//icon = Icon.ToBitmap();

            //initialize queue with screen width
			m_recv_all_q = new FixedSizeQueue<float>(Screen.PrimaryScreen.Bounds.Width);
            m_send_all_q = new FixedSizeQueue<float>(Screen.PrimaryScreen.Bounds.Width);
			m_OleDbConnection.Open();
		}//end Constructor

		private void FormLoadGraph_Load(object sender, System.EventArgs e)
		{
			try
			{
				OptionsSerializer.Load(m_sSettingsFile, m_Options);

				if (m_Options.Location.X > 0 || m_Options.Location.Y > 0)
				{
					this.Size = m_Options.Size;
					this.Location = m_Options.Location;
				}//end if
				else
				{
					this.CenterToScreen();
				}

				_screenSaver = new ScreenSaver(this);

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
				while (err.InnerException != null)
				{
					err = err.InnerException;
					msg = err.Message;
				}
				Utils.MessageBox("Load error: " + msg);
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
						if (!_close)
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


		private long m_hidecounter = 0;
		private long m_iCheckIpCount = 0;
		private long m_iHideControlsCounter = 0;
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
					_screenSaver.PerformScreenSaving();
				}
				m_iCheckIpCount++;

				if (!_hover && m_iHideControlsCounter++ > 5)
					m_btnHide.Visible = false;

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

		Graphics m_graphics = null;

		private void m_PictureBoxGraph_Resize(object sender, System.EventArgs e)
		{
			if (m_graphics != null)
				m_graphics.Dispose();

			m_bmp = new Bitmap(m_PictureBoxGraph.Width - 2,m_PictureBoxGraph.Height - 2);
			m_graphics = Graphics.FromImage(m_bmp);
		}//end m_PictureBoxGraph_Resize

		private void DrawGraph()
		{
			m_graphics.Clear(m_Options.Background);

			float[] rf = m_recv_all_q.ToArray();
            float[] sf = m_send_all_q.ToArray();

			for (int i = 0; i < m_bmp.Width && i < rf.Length; i++)
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

				int max = (int)(m_bmp.Height - (maxr * m_bmp.Height / (1 + m_Options.Overflow)));
				int min = (int)(m_bmp.Height - (minr * m_bmp.Height  / (1 + m_Options.Overflow)));

				if (max < 0)
					max = 0;
				if (min < 0)
					min = 0;

				int pos = m_bmp.Width - i - 1;
				m_graphics.DrawLine(maxp, pos, min, pos, max);
				m_graphics.DrawLine(m_penBoth, pos, m_bmp.Height, pos, min);
			}//end for

			m_PictureBoxGraph.Image = m_bmp; 
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
			opt.OnPropertyChangedAction = (propertyName) => 
			{
				ReApplyOptions();
			};

			opt.ShowDialog(this);
			opt.Dispose();
			if (m_Options.Interface == "")
			{
				Utils.MessageBox("Please select a valid Network interface");
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

		private void m_btnHide_Click(object sender, EventArgs e)
		{
			menuShowHide_Click(sender, e);
		}

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

		private bool _hover = false;

		private void m_PictureBoxGraph_MouseHover(object sender, System.EventArgs e)
		{
			_hover = true;
			m_iHideControlsCounter = 0;
			m_btnHide.Visible = true;
		}//end m_PictureBoxGraph_MouseHover

		private void m_PictureBoxGraph_MouseLeave(object sender, System.EventArgs e)
		{
			_hover = false;
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

			if ( _hover || m_Options.AlwaysShowText )
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
			int res = m_OleDbDataAdapter.Fill(m_logger.RateLog);
			ReportForm report = new ReportForm(from, m_logger.RateLog, img, m_Options);
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
