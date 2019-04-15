using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

namespace ClipboardManager
{
	using HWND = IntPtr;
    using ClipboardManager.Properties;
    using System.IO;

	/// <summary>
	/// Summary description for SpyWindow.
	/// </summary>
	public partial class FormSpyWindow : System.Windows.Forms.Form
	{
		public delegate void DisplayImageEventHandler(Image image, bool autoDecideOnSizing, PictureBoxSizeMode manualSizeMode);

		//public event DisplayImageEventHandler ImageReadyForDisplay;
		
		private bool _capturing;
		private Image _finderHome;
		private Image _finderGone;		
		private Cursor _cursorDefault;
		private Cursor _cursorFinder;
		private IntPtr _hPreviousWindow;
        private HWND _hCurrentWindow = IntPtr.Zero;

		/// <summary>
		/// Initializes a new instance of the SpyWindow class
		/// </summary>
		public FormSpyWindow()
		{		
			this.InitializeComponent();	
			
			_cursorDefault = Cursor.Current;
            _cursorFinder = new Cursor(new MemoryStream(Resources.Finder));
		    _finderHome = Resources.FinderHome;
		    _finderGone = Resources.FinderGone;
			
			m_picFinder.Image = _finderHome;
		}//end constructor

		/// <summary>
		/// Processes window messages sent to the Spy Window
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{									
			switch(m.Msg)
			{
				/*
				 * stop capturing events as soon as the user releases the left mouse button
				 * */
				case (int)NativeWIN32.WindowMessages.WM_LBUTTONUP:
					this.CaptureMouse(false);
					break;
				/*
				 * handle all the mouse movements
				 * */
				case (int)NativeWIN32.WindowMessages.WM_MOUSEMOVE:
					this.HandleMouseMovements();
					break;			
			};

			base.WndProc (ref m);
		}//end WndProc

		private void FormSpyWindow_Load(object sender, EventArgs e)
		{
			m_btnAdvanced_Click(sender, e); //close advanced
		}//end FormSpyWindow_Load
		
		/// <summary>
		/// Processes the mouse down events for the finder tool 
		/// </summary>
		private void OnFinderToolMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				this.CaptureMouse(true);
		}//end OnFinderToolMouseDown

		/// <summary>
		/// Returns the name of a window's class
		/// </summary>
		private string GetClassName(IntPtr hWnd)
		{			
			StringBuilder sb = new StringBuilder(256);
			NativeWIN32.GetClassName(hWnd, sb, 256);								
			return sb.ToString();
		}//end GetClassName

		/// <summary>
		/// Captures or releases the mouse
		/// </summary>
		/// <param name="captured"></param>
		private void CaptureMouse(bool captured)
		{
			// if we're supposed to capture the window
			if (captured)
			{
				// capture the mouse movements and send them to ourself
				NativeWIN32.SetCapture(this.Handle);

				// set the mouse cursor to our finder cursor
				Cursor.Current = _cursorFinder;

				// change the image to the finder gone image
				m_picFinder.Image = _finderGone;
			}//end if
			// otherwise we're supposed to release the mouse capture
			else
			{
				// so release it
				NativeWIN32.ReleaseCapture();

				// put the default cursor back
				Cursor.Current = _cursorDefault;

				// change the image back to the finder at home image
				m_picFinder.Image = _finderHome;

				// and finally refresh any window that we were highlighting
				if (_hPreviousWindow != IntPtr.Zero)
				{
					WindowHighlighter.Refresh(_hPreviousWindow);
					_hPreviousWindow = IntPtr.Zero;
				}//end if
			}//end else

			// save our capturing state
			_capturing = captured;
		}//end CaptureMouse

		/// <summary>
		/// Handles all mouse move messages sent to the Spy Window
		/// </summary>
		private void HandleMouseMovements()
		{
			// if we're not capturing, then bail out
			if (!_capturing)
				return;
											
			try
			{
                // capture the window under the cursor's position
                _hCurrentWindow = NativeWIN32.WindowFromPoint(Cursor.Position);

				// if the window we're over, is not the same as the one before, and we had one before, refresh it
				if (_hPreviousWindow != IntPtr.Zero && _hPreviousWindow != _hCurrentWindow)
					WindowHighlighter.Refresh(_hPreviousWindow);

				// if we didn't find a window.. that's pretty hard to imagine. lol
				if (_hCurrentWindow == IntPtr.Zero)
				{
					m_txtHandle.Text = "";
					m_txtClass.Text = "";
					m_txtCaption.Text = "";
					m_txtStyle.Text = "";
					m_txtRect.Text = "";
				}//end if
				else
				{
					// save the window we're over
					_hPreviousWindow = _hCurrentWindow;

                    m_txtProcess.Text = GetProcessPath(_hCurrentWindow);

                    m_txtHandle.Text = string.Format("{0}", _hCurrentWindow.ToInt32().ToString());

					m_txtClass.Text = this.GetClassName(_hCurrentWindow);

					m_txtCaption.Text = NativeWIN32.GetWindowText(_hCurrentWindow);

					if ( m_txtClass.Text == "Edit" || m_txtCaption.Text == "" )
					{
						m_txtCaption.Text = NativeWIN32.GetText(_hCurrentWindow);
					}//end if

					NativeWIN32.RECT rc = NativeWIN32.GetWindowRect(_hCurrentWindow);
					NativeWIN32.EnumChildWindows(_hCurrentWindow, m_txtCaption.Text);
						
					// rect
					m_txtRect.Text = string.Format(
						"[{0} x {1}], ({2})", 
						rc.Right - rc.Left, rc.Bottom - rc.Top, rc.ToString());

					// highlight the window
					WindowHighlighter.Highlight(_hCurrentWindow);
				}//end else
			}//end try
			catch(Exception err)
			{
				Debug.WriteLine("HandleMouseMovements: "+err);
				FormClipboard.TraceLn(false, "FormSpyWindow", "HandleMouseMovements",
					"Error: {0}", err.Message);
			}//end catch
		}//end HandleMouseMovements

		private void OnButtonOKClicked(object sender, EventArgs e)
		{
			//if no text selected copy all text
			if ( m_txtCaption.SelectionLength == 0 )
				m_txtCaption.SelectAll();
			m_txtCaption.Copy();
		}//end OnButtonOKClicked

		private void OnButtonCancelClicked(object sender, EventArgs e)
		{
		}//end OnButtonCancelClicked

		private void m_btnAdvanced_Click(object sender, EventArgs e)
		{
			this.SuspendLayout(); 
			
			m_pnlAdvanced.Visible = !m_pnlAdvanced.Visible;
			m_btnAdvanced.ImageIndex = m_pnlAdvanced.Visible ? 1 : 0;

			int y = m_pnlAdvanced.Visible ? 350 : 230;
			Size newSize = new Size(MinimumSize.Width, y);
			if ( this.Height >= y ) this.MinimumSize = newSize; //to ensure minimum size BEFORE resizing
			this.Height += m_pnlAdvanced.Visible ? m_pnlAdvanced.Height : -m_pnlAdvanced.Height;
			this.MinimumSize = newSize;

			this.ResumeLayout(true);
		}//end m_btnAdvanced_Click

		private void m_chkOnTop_CheckedChanged(object sender, EventArgs e)
		{
			this.TopMost = m_chkOnTop.Checked;
		}

		private void m_sliderTransparency_Scroll(object sender, EventArgs e)
		{
			this.Opacity = m_sliderTransparency.Value / 100F;
		}

        private void m_btBrowseProcess_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(m_txtProcess.Text));
        }

        private void m_btnKillProcess_Click(object sender, EventArgs e)
        {
            if (_hCurrentWindow != IntPtr.Zero)
            {
                Process p = GetWindowProcess(_hCurrentWindow);
                if (p != null)
                    p.Kill();
            }
        }

        private string GetProcessPath(IntPtr hWnd)
        {
            Process process = GetWindowProcess(hWnd);
            return process.MainModule.FileName;
        }

        private Process GetWindowProcess(IntPtr hWnd)
        {
            uint processId;
            NativeWIN32.GetWindowThreadProcessId(hWnd, out processId);
            return Process.GetProcessById((int)processId);
        }
    }//end class FormSpyWindow
}//end namespace ClipboardListener
