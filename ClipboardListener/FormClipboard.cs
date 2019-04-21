using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Threading;
using System.Reflection;
using ClipboardManager.Zip;
using ClipboardManager.DesktopUtil;
using Microsoft.Win32;
using Utils;

namespace ClipboardManager
{
	public partial class FormClipboard : Form
	{
		private IntPtr m_NextClipboardViewer			= IntPtr.Zero;
		private String m_sFileName						= "ClipboardListener.xml";
		private ClipboardList m_ClipboardListMain		= null;
		private ClipboardList m_ClipboardListFavorites	= null;
		private IntPtr m_hWndToRestore					= IntPtr.Zero;
		private volatile bool m_bPasteFromThis			= false; //to prevent loop
		private volatile bool m_bCopyFromSnapShot		= false;
		public static FormClipboard m_This				= null;
		public static Utils.Settings m_Settings			= new Utils.Settings();
		private bool m_bModified						= false;
		private int m_iAboutId							= 32164;
		private int m_iExitId							= 32165;

		//timer data for automatic reconnect
		private System.Windows.Forms.Timer m_TimerReconnect = new System.Windows.Forms.Timer();
		private DateTime m_TimeStamp					= DateTime.Now.AddSeconds(-1); //for first time update
        private const int TIMEOUT                       = 1000000; //~ 15 min

        public static string TITLE = "Clipboard Manager";

        static FormClipboard()
        {
#if DEBUG
            TITLE += "(Debug)";
#endif
        }

        public FormClipboard()
		{
			InitializeComponent();

			m_This = this; //for trace purpose
            m_notifyIconCoodClip.Visible = false;

            this.Text = TITLE;
            m_notifyIconCoodClip.Text = this.Text;
            
            //copy edit menu items
            CopyMenuStripItem(m_ToolStripMenuItem_Edit_Cut, m_contextMenuStrip_RichTextBox_Cut, m_ToolStripMenuItem_Edit_Cut_Click);
			CopyMenuStripItem(m_ToolStripMenuItem_Edit_Copy, m_contextMenuStrip_RichTextBox_Copy, m_ToolStripMenuItem_Edit_Copy_Click);
			CopyMenuStripItem(m_ToolStripMenuItem_Edit_Paste, m_contextMenuStrip_RichTextBox_Paste, m_ToolStripMenuItem_Edit_Paste_Click);

			this.Hide();

			m_ClipboardListMain = new ClipboardList("Main", m_imageListClipboardTypes);
			m_ClipboardListFavorites = new ClipboardList("Favorites", m_imageListClipboardTypes);

            string appDataFolder = Path.GetDirectoryName(Application.LocalUserAppDataPath); //exclude version

            m_sFileName = Path.Combine(appDataFolder, m_sFileName);
			LogMethodEx(true, "FormClipboard", "C-tor", "Settings file: {0}", m_sFileName);

			this.SetupSystemMenu();
			Application.AddMessageFilter(new ClipboardMessageFilter(this));

            // Define the priority of the application (0x3FF = The higher priority)
            Win32_Shutdown.SetProcessShutdownParameters(0x3FF, Win32_Shutdown.SHUTDOWN_NORETRY);
        }//end constructor

        private int count = 0;
        protected override void WndProc(ref Message m)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0}. MAINF: PreFilterMessage({1}-{2})",
            count++, m.Msg, WindowsMessages.Message(m.Msg)));
            
            if (!ProcessWindowsMessage(ref m))
                base.WndProc(ref m);
        }

        //return true if processed
		public bool ProcessWindowsMessage(ref Message m)
		{
			// defined in winuser.h
			const int WM_HOTKEY			= 0x0312;
			const int WM_DRAWCLIPBOARD	= 0x308;
			const int WM_CHANGECBCHAIN	= 0x030D;
			const int WM_SYSCOMMAND		= 0x112;
            const int WM_ENDSESSION     = 0x0016;
            const int WM_QUERYENDSESSION = 0x0011;

			switch (m.Msg)
			{
				case WM_HOTKEY:
					ProcessHotkey(null);
					return true;

				case WM_DRAWCLIPBOARD:
					if (ProcessClipboardData())
						NativeWIN32.SendMessage(m_NextClipboardViewer, m.Msg, m.WParam, m.LParam);
					return true;

				case WM_CHANGECBCHAIN:
					if (m.WParam == m_NextClipboardViewer)
						m_NextClipboardViewer = m.LParam;
					else if ( m_NextClipboardViewer != IntPtr.Zero )
						NativeWIN32.SendMessage(m_NextClipboardViewer, m.Msg, m.WParam, m.LParam);
					return true;

				case WM_SYSCOMMAND: //respond to About & Exit
					if ( m.WParam.ToInt32() == m_iAboutId )
						m_ToolStripMenuItem_Help_About_Click(null, null);
					if ( m.WParam.ToInt32() == m_iExitId )
						m_ToolStripMenuItem_File_Exit_Click(null, null);
					return false;

                case WM_QUERYENDSESSION:
                case WM_ENDSESSION:
                    return ShutdownHandler.ProcessShutdownMessage(this.Handle, ref m);

                default:
					return false;
			}//end switch
		}//end ProcessWindowsMessage

		private void FormClipboard_Load(object sender, EventArgs e)
		{
			m_Settings.Load(m_sFileName, this, m_ClipboardListMain, m_ClipboardListFavorites, this.Icon.ToBitmap());
            m_NextClipboardViewer = (IntPtr)NativeWIN32.SetClipboardViewer((int)this.Handle);
			bool success = m_Settings.m_HotKey.RegisterHotKey();
			
			m_richTextBoxClipboard_SelectionChanged(null, null); //to enable copy/paste
			
			m_ToolStripMenuItem_View_SnapShot.Checked = m_Settings.m_bShowSnapShot;
			m_splitContainerClipboard.Panel2Collapsed = !m_Settings.m_bShowSnapShot;

			m_ToolStripMenuItem_View_Debug.Checked = m_Settings.m_bShowDebug;
			m_ToolStripMenuItem_View_Debug_Click(sender, e);

			m_Settings.m_Encodings.CreateEncodingsMenuItems(
				m_ToolStripMenuItem_Tools_Encoding.DropDownItems,
				m_richTextBoxSnapShot,
				m_richTextBoxClipboard);

			m_TimerReconnect.Tick += new EventHandler(m_TimerReconnect_Tick);
			m_TimerReconnect.Interval = TIMEOUT; //~15 min
			if ( m_Settings.m_AutoReconnect ) 
				m_TimerReconnect.Start();

            ShutdownHandler.AbortShutdownIfScheduled = m_Settings.m_bAbortShutdown;

            Utils.ServicesManipulator.ContinuousMonitoringServices = m_Settings.m_bStopServices;
            Utils.ServicesManipulator.Start();

            m_notifyIconCoodClip.Visible = true;
            this.Hide();
		}//end FormClipboard_Load

		private void FormClipboard_FormClosing(object sender, FormClosingEventArgs e)
		{
			if ( e.CloseReason == CloseReason.UserClosing )
			{
				e.Cancel = true;
				this.Hide();
				return;
			}//end if

			m_notifyIconCoodClip.Visible = false;
			m_Settings.m_HotKey.UnregisterHotKey();
            ShutdownHandler.CancelMonitoringShutdown();
            Utils.ServicesManipulator.Stop();

            NativeWIN32.ChangeClipboardChain(this.Handle, m_NextClipboardViewer);

			Save();
		}//end FormClipboard_FormClosing

		private Bitmap m_bmpExit, m_bmpClose, m_bmpAbout;
		private void SetupSystemMenu()
		{
			m_bmpExit = new Bitmap(m_ImageListSysMenu.Images[2]);
			m_bmpClose = new Bitmap(m_ImageListSysMenu.Images[3]);
			m_bmpAbout = new Bitmap(m_ImageListSysMenu.Images[4]);

			// get handle to system menu
			IntPtr menu = NativeWIN32.GetSystemMenu(this.Handle, 0);
			// add exit item with a unique ID
			NativeWIN32.InsertMenu(menu, NativeWIN32.MF_LAST, NativeWIN32.MF_BYPOSITION, m_iExitId, "Exit\tAlt+X");
			// add a separator
			NativeWIN32.AppendMenu(menu, NativeWIN32.MF_SEPARATOR, 0, null);
			// add About item with a unique ID
			NativeWIN32.AppendMenu(menu, 0, m_iAboutId, "About " + this.Text);

			// change icons
			NativeWIN32.SetMenuItemBitmaps(menu, 6, NativeWIN32.MF_BYPOSITION, m_bmpClose.GetHbitmap(), IntPtr.Zero);
			NativeWIN32.SetMenuItemBitmaps(menu, 7, NativeWIN32.MF_BYPOSITION, m_bmpExit.GetHbitmap(), IntPtr.Zero);
			NativeWIN32.SetMenuItemBitmaps(menu, 9, NativeWIN32.MF_BYPOSITION, m_bmpAbout.GetHbitmap(), IntPtr.Zero);
		}//end SetupSystemMenu

		private void CopyMenuStripItem(ToolStripMenuItem src, ToolStripMenuItem dst, EventHandler onClick)
		{
			dst.Text			= src.Text;
			dst.Image			= src.Image;
			dst.ShortcutKeys	= src.ShortcutKeys;
			dst.ToolTipText		= src.ToolTipText;
			dst.Click			+= onClick;
		}//end CopyMenuStripItem

		//sometimes after long time of no action it stoppes to receive events, need to reconnect
		private void m_TimerReconnect_Tick(object sender, EventArgs e)
		{
			try
			{
				TimeSpan sp = DateTime.Now - m_TimeStamp;
                bool bReconnect = (sp.TotalMilliseconds > 1.5*TIMEOUT);

                if ( bReconnect )
					m_contextMenuStripTrayIcon_Reconnect_Click(sender, e);

                m_contextMenuStripTrayIcon_UAC_Click(null, e);

                if ( m_bModified )
				{
                    LogMethodEx(true, "FormClipboard", "m_TimerReconnect_Tick", "SaveInThread()");
					m_Settings.m_bShowSnapShot = m_ToolStripMenuItem_View_SnapShot.Checked;
					Thread thr = new Thread(new ThreadStart(Save));
                    thr.IsBackground = false;
					thr.Start();
				}//end if
                
				LogMethodEx(true, "FormClipboard", "m_TimerReconnect_Tick", "*** Reconnect is Active == {0}, Timeout: {1}",
                    bReconnect, sp.ToString());

				GC.Collect();
			}//end try
			catch ( Exception err ) 
			{
				LogMethodEx(true, "Settings", "m_TimerReconnect_Tick", 
					"Error: {0}", err.Message);
			}//end catch		
		}//end m_TimerReconnect_Tick

		private void Save()
		{
			m_Settings.m_bShowSnapShot = m_ToolStripMenuItem_View_SnapShot.Checked;
			m_Settings.m_bShowDebug = m_ToolStripMenuItem_View_Debug.Checked;

			try
			{
                LogMethod("Save", "Saving: {0}", m_sFileName);
				m_Settings.Save(m_sFileName, m_ClipboardListMain, m_ClipboardListFavorites);
				m_bModified = false;
			}//end try
			catch ( Exception err )
			{
				LogMethod("Save", "Error: {0}", err.ToString());
			}//end catch
		}//end Save

		private bool ProcessClipboardData()
		{
			try
			{
                TimeSpan ts = DateTime.Now - m_TimeStamp;
                if (ts.TotalSeconds < 1.0)
                    return false; //to avoid infinite loop

				LogMethod("ProcessClipboardData", "past from this={0} snapshot: {1}",
					m_bPasteFromThis, m_bCopyFromSnapShot);

				//remember last time received data
				m_TimeStamp = DateTime.Now;

				string sAppTitle = "Clipboard Manager";
				if ( !m_bPasteFromThis )
				{
					IntPtr hWndCurr = ForegroundWindow.Instance.Handle;
					sAppTitle = NativeWIN32.GetWindowText(hWndCurr);
					if ( sAppTitle.Trim().Length == 0 )
						sAppTitle = "--Unknown--";
                    
                    //if the new item
					if ( m_ClipboardListMain.AddEntry(GetIconFromHWnd(hWndCurr)) )
                        m_bModified = true;
				}//end if

                m_listHistory.UpdateHistoryList(m_ClipboardListMain);
                FillFormatsCombo(m_ClipboardListMain.GetCurrentEntry()._dataType);

				SetFontSize(-1); //reset font size menu
				m_ClipboardListMain.GetCurrentEntry().SetRichText(m_richTextBoxClipboard, m_icoClipboardApp, m_lblClipboardType);
				if ( !m_bCopyFromSnapShot )
					m_ClipboardListMain.GetCurrentEntry().SetRichText(m_richTextBoxSnapShot, m_icoSnapShotApp, m_lblSnapShotType);

				LogMethod("ProcessClipboardData", "Clipboard Current Entry: {0}",
					m_ClipboardListMain.GetCurrentEntry().ShortDesc());

				m_toolStripStatusLabel1.Image = m_ClipboardListMain.GetCurrentEntry()._icoAppFrom;
				m_toolStripStatusLabel1.Text = "From: " + sAppTitle;
			}//end try
			catch ( Exception e )
			{
				LogMethod("ProcessClipboardData", "Clipboard error: {0}", e.ToString());
				m_toolStripStatusLabel1.Image = m_imageListClipboardTypes.Images[5];
				m_toolStripStatusLabel1.Text = "Clipboard error: " + e.ToString();
				//MessageBox.Show(e.ToString());
			}//end catch
			finally { m_bCopyFromSnapShot = false; m_bPasteFromThis = false; }
			return true;
		}//end ProcessClipboardData

		private void FillFormatsCombo(string sCurrentType)
		{
			IDataObject objData = Clipboard.GetDataObject();
			string[] svFormats = objData.GetFormats(true);

			m_ToolStripComboBox_CFormats.Items.Clear();
			if ( svFormats != null && svFormats.Length > 0 )
			{
				m_ToolStripComboBox_CFormats.Items.AddRange(svFormats);
				m_ToolStripComboBox_CFormats.SelectedItem = sCurrentType;
			}//end if
		}//end FillFormatsCombo()

		private void m_ToolStripComboBox_CFormats_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string sFormat = m_ToolStripComboBox_CFormats.Text;
				object data = Clipboard.GetData(sFormat);
                string sData = data == null ? "null" : data.ToString();
                if (sData.Length > 1024)
                    sData = sData.Substring(0, 1024) + " ...";
				LogMethod("CFormats_SelectedIndexChanged", "Clipboard Format: '{0}', Data: {1}", sFormat, sData);
			}//end try
			catch ( Exception err)
			{
				LogMethod("CFormats_SelectedIndexChanged", "Error: {0}", err.ToString());
			}//end catch
		}//end m_ToolStripComboBox_CFormats_SelectedIndexChanged

		private Image GetIconFromHWnd(IntPtr hWnd)
		{
			const int WM_GETICON		= 0x007F;
			const int WM_QUERYDRAGICON	= 0x0037;
			const int GCL_HICON			= -14;
			const int GCL_HICONSM		= -34;
			const int ICON_SMALL2		= 2;
			const int ICON_SMALL		= 0;
			const int ICON_BIG			= 1;
			
			string sTitle = NativeWIN32.GetWindowText(hWnd);
			LogMethod("GetIconFromHWnd", "Caption(ico): {0}", sTitle);
			if (hWnd == this.Handle || sTitle.Trim().Length == 0 )
				return this.Icon.ToBitmap();

			IntPtr hIcon = NativeWIN32.GetClassLong(hWnd, GCL_HICONSM);
			if (hIcon == IntPtr.Zero)
				hIcon = NativeWIN32.SendMessage(hWnd, WM_GETICON, ICON_SMALL, IntPtr.Zero);
			if (hIcon == IntPtr.Zero)
				hIcon = NativeWIN32.SendMessage(hWnd, WM_GETICON, ICON_SMALL2, IntPtr.Zero);
			if (hIcon == IntPtr.Zero)
				hIcon = NativeWIN32.GetClassLong(hWnd, GCL_HICON);
			if (hIcon == IntPtr.Zero)
				hIcon = NativeWIN32.SendMessage(hWnd, WM_GETICON, ICON_BIG, IntPtr.Zero);
			if (hIcon == IntPtr.Zero)
				hIcon = NativeWIN32.SendMessage(hWnd, WM_QUERYDRAGICON, 0, IntPtr.Zero);
			
			if (hIcon == IntPtr.Zero)
			{
				return this.Icon.ToBitmap();
			}//end if

			return Icon.FromHandle(hIcon).ToBitmap();
		}//end 

		private class Pos
		{
			private NativeWIN32.RECT r;
			public Pos(Control ctrl) 
			{
				r.Left		= ctrl.Left;
				r.Top		= ctrl.Top;
				r.Right		= ctrl.Right;
				r.Bottom	= ctrl.Bottom;
			}//end constructor

			public Pos(Size sz)
			{
				r.Left		= sz.Width - 16;
				r.Top		= sz.Height - 16;
				r.Right		= sz.Width;
				r.Bottom	= sz.Height;
			}//end constructor

			public Pos(NativeWIN32.RECT r)
			{
				this.r = r;
			}//end constructor

			public Point GetCenterPos(Size sz, bool bRelativePos)
			{
				return CalculateCenter(r, sz, bRelativePos);
			}//end GetCenterPos
		}//end class Pos

		private void ProcessHotkey(Pos rectangle)
		{
			m_hWndToRestore = ForegroundWindow.Instance.Handle;
			NativeWIN32.RECT rc = NativeWIN32.GetWindowRect(m_hWndToRestore);
			LogMethod("ProcessHotkey", "Parent rc: {0}",  rc);
			LogMethod("ProcessHotkey", "Caption(HotKey): {0}", NativeWIN32.GetWindowText(m_hWndToRestore));

			//redbuild menu without flickering
			m_contextMenuStripClipboard.Visible = false;
			m_contextMenuStripClipboard.SuspendLayout();
			
			while ( m_contextMenuStripClipboard.Items.Count > 4 )
				m_contextMenuStripClipboard.Items.RemoveAt(4);

			ClipboardList.ClipboardEntry clp = m_ClipboardListMain.GetCurrentEntry();
			m_contextMenuStripClipboard_Current.Text = clp.ShortDesc();
            if ( m_contextMenuStripClipboard_Current.Text != clp.ToString() )
                m_contextMenuStripClipboard_Current.ToolTipText = clp.ShortDesc(400, false);
            m_contextMenuStripClipboard_Current.Image = clp.GetCombinedIcon(true);
			m_contextMenuStripClipboard_Current.Tag = clp;

			m_contextMenuStripClipboard_Favorites.DropDownItems.Clear();
			m_contextMenuStripClipboard_Favorites.DropDownItems.AddRange(BuildFavoritesList(true));
			m_contextMenuStripClipboard_Favorites.Image = ImagesUtil.Combine(m_ImageListSysMenu.Images[0], m_ImageListSysMenu.Images[0]);

			//build main clipboard history menu
			int idx = clp.IsEmpty ? 0 : 1;
			m_contextMenuStripClipboard.Items.AddRange(BuildMainClipboardList(idx));

			ToolStripMenuItem c = new System.Windows.Forms.ToolStripMenuItem();
			c.Text = "Cancel";
			m_contextMenuStripClipboard.Items.Add(c);

			//m_contextMenuStripClipboard.Visible = true;
			m_contextMenuStripClipboard.ResumeLayout(true);
			
			//set this active - to receive menu events
			if ( m_hWndToRestore != this.Handle )
				this.Visible = false;
			NativeWIN32.SetForegroundWindow(this.Handle);

			//fix - show system tray icon
			m_notifyIconCoodClip.Visible = true;

			if ( rectangle == null )
				rectangle = new Pos(rc);

			m_contextMenuStripClipboard.Show(rectangle.GetCenterPos(m_contextMenuStripClipboard.Size, false));
		}//end ProcessHotkey

		//build main clipboard history menu
		private ToolStripMenuItem[] BuildMainClipboardList(int startIdx)
		{
			ToolStripMenuItem[] list = new ToolStripMenuItem[m_ClipboardListMain.Count - startIdx];
			for (int idx = startIdx; idx < m_ClipboardListMain.Count; idx++)
			{
				ClipboardList.ClipboardEntry clp = m_ClipboardListMain.GetEntry(idx);
				list[idx - startIdx] = new System.Windows.Forms.ToolStripMenuItem();
				list[idx - startIdx].Text = clp.ShortDesc();
                if ( list[idx - startIdx].Text != clp.ToString() )
                    list[idx - startIdx].ToolTipText = clp.ShortDesc(400, false);
				list[idx - startIdx].MouseDown += (this.m_contextMenuStrip_ClipboardEntry_MouseDown);
				list[idx - startIdx].MouseUp += (this.m_contextMenuStrip_ClipboardEntry_MouseUp);
				list[idx - startIdx].Click += (this.m_contextMenuStrip_ClipboardEntry_Click);
				list[idx - startIdx].Image = clp.GetCombinedIcon(true);
				list[idx - startIdx].Tag = clp;
				//list[idx - startIdx].DropDownItems.Add(m_ClipboardMenuItem_CreateSubMenu(clp));
			}//end foreach
			return list;
		}//end BuildMainClipboardList

		private ToolStripMenuItem[] BuildFavoritesList(bool bTwinImage)
		{
			ToolStripMenuItem[] list = new ToolStripMenuItem[m_ClipboardListFavorites.Count];
			for (int idx = 0; idx < m_ClipboardListFavorites.Count; idx++)
			{
				ClipboardList.ClipboardEntry clp = m_ClipboardListFavorites.GetEntry(idx);
				list[idx] = new System.Windows.Forms.ToolStripMenuItem();
				list[idx].Text = clp.ShortDesc();
                if ( list[idx].Text != clp.ToString() )
                    list[idx].ToolTipText = clp.ToString();
                //list[idx].MouseUp += new MouseEventHandler(this.m_contextMenuStripClipboard_Favorites_Entry_MouseUp);
				list[idx].Click += (this.m_contextMenuStrip_ClipboardEntry_LeftClick);
				list[idx].Image = clp.GetCombinedIcon(bTwinImage);
				list[idx].Tag = clp;
			}//end for
			return list;
		}//end BuildFavoritesList

		private static Point CalculateCenter(NativeWIN32.RECT r, Size szMenu, bool bRelativePos)
		{
			int x = ((r.Right - r.Left) - szMenu.Width) / 2;
			int y = ((r.Bottom - r.Top) - szMenu.Height) / 2;
			if (x < 0 && !bRelativePos) x = 200;
			if (y < 0 && !bRelativePos) y = 150;
			if ( bRelativePos )
				return new Point(x, y);
			else
				return new Point(r.Left + x, r.Top + y);
		}//end CalculateCenter

		private void m_toolStripButton_OnTop_Click(object sender, EventArgs e)
		{
			this.TopMost = m_toolStripButton_OnTop.Checked;
		}//end m_toolStripButton_OnTop_Click

		private void m_toolStripButton_Target_Click(object sender, EventArgs e)
		{
			FormSpyWindow frm = new FormSpyWindow();
			frm.Icon = this.Icon;
			bool bVisible = this.Visible;
			this.Visible = false;
			frm.ShowDialog();
			if ( bVisible ) this.Visible = true;
		}//end m_toolStripButton_Target_Click

		private bool m_bLeaveContextMenuOpened = false; //to show context menu
		private MouseEventArgs m_LastMouseEventArgs = null; //to know which mouse button was pressed
		private void m_contextMenuStrip_ClipboardEntry_MouseDown(object sender, MouseEventArgs e)
		{
			m_LastMouseEventArgs = e; //set
			if (e.Button == MouseButtons.Right)
				m_bLeaveContextMenuOpened = true;
		}//end m_contextMenuStrip_ClipboardEntry_MouseDown

		private void m_contextMenuStrip_ClipboardEntry_MouseUp(object sender, MouseEventArgs e)
		{
			m_LastMouseEventArgs = null;//reset
			if (e.Button == MouseButtons.Right)
			{
				ToolStripMenuItem itm = (ToolStripMenuItem)sender;
				itm.BackColor = SystemColors.ControlLight; //select context menu item
				Point pt = new Point(e.Location.X, e.Location.Y + itm.Bounds.Top);
				
				m_contextMenuStrip_ClipboardEntry_AddToFavorites.Tag = itm.Tag;
				m_contextMenuStrip_ClipboardEntry_Edit.Tag = itm.Tag;
				m_contextMenuStrip_ClipboardEntry_Remove.Tag = itm.Tag;

				//disable first entry 'delete' context menu - can't delete current
				bool bEnable = ( m_contextMenuStripClipboard.Items.IndexOf(itm) > 0 );
				m_contextMenuStrip_ClipboardEntry_Remove.Enabled = bEnable;

				m_contextMenuStrip_ClipboardEntry.Tag = itm;
				m_contextMenuStrip_ClipboardEntry.Show(m_contextMenuStripClipboard, pt);
			}//end if
			else if (e.Button == MouseButtons.Left)
			{
				m_contextMenuStrip_ClipboardEntry_LeftClick(sender, e);
			}//end else if
		}//end m_contextMenuStrip_ClipboardEntry_MouseUp

		//I need this function to process 'Enter' key only
		private void m_contextMenuStrip_ClipboardEntry_Click(object sender, EventArgs e)
		{
			if (m_LastMouseEventArgs != null) //mouse was clicked
				return; //already processed in MouseUp

			m_contextMenuStrip_ClipboardEntry_LeftClick(sender, e);
		}//end m_contextMenuStrip_ClipboardEntry_Click

		private void m_contextMenuStrip_ClipboardEntry_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			m_bLeaveContextMenuOpened = false;

			IntPtr hWndCurr = ForegroundWindow.Instance.Handle;
			LogMethod("ClipboardEntry_Closed", "Caption3: {0} Text: {1}", 
				hWndCurr, NativeWIN32.GetWindowText(hWndCurr));

			//restore color of selected item
			ToolStripMenuItem itm = (ToolStripMenuItem)m_contextMenuStrip_ClipboardEntry.Tag;
			itm.BackColor = SystemColors.Control;
			
			//if click outside menu close menu - clear screen
			if (hWndCurr != this.Handle && hWndCurr != m_contextMenuStripClipboard.Handle)
				m_contextMenuStripClipboard.Close(ToolStripDropDownCloseReason.AppFocusChange);
			else //resore focus
				m_contextMenuStripClipboard.Focus();
		}//end m_contextMenuStrip_ClipboardEntry_Closed

		private void m_contextMenuStrip_ClipboardEntry_AddToFavorites_Click(object sender, EventArgs e)
		{
			ToolStripItem itm = (ToolStripItem)sender;
			if (itm == null || itm.Tag == null)
				return;

			try
			{
				ClipboardList.ClipboardEntry clp = (ClipboardList.ClipboardEntry)itm.Tag;
				LogMethod("ClipboardEntry_AddToFavorites_Click", "Add tofavorites: {0}", clp.ShortDesc());

				m_ClipboardListFavorites.AddEntry(clp);

				//rebuild favorites list
				m_contextMenuStripClipboard_Favorites.DropDownItems.Clear();
				m_contextMenuStripClipboard_Favorites.DropDownItems.AddRange(BuildFavoritesList(true));

				m_contextMenuStripClipboard.Focus();
			}//end try
			finally
			{
			}//end finally
		}//end m_contextMenuStrip_ClipboardEntry_AddToFavorites_Click

		private void m_contextMenuStrip_ClipboardEntry_Edit_Click(object sender, EventArgs e)
		{
			ToolStripItem itm = (ToolStripItem)sender;
			if (itm == null || itm.Tag == null)
				return;

			//make this item current
			m_contextMenuStrip_ClipboardEntry_LeftClick(sender, e);
			//open this item for editing
            m_contextMenuStripTrayIcon_Show_Click(sender, e);
		}//end m_contextMenuStrip_ClipboardEntry_Edit_Click

		private void m_contextMenuStrip_ClipboardEntry_Remove_Click(object sender, EventArgs e)
		{
			ToolStripItem itm = (ToolStripItem)sender;
			if (itm == null || itm.Tag == null)
				return;

			try
			{
				ClipboardList.ClipboardEntry clp = (ClipboardList.ClipboardEntry)itm.Tag;
				LogMethod("ClipboardEntry_Remove_Click", "Delete: {0}", clp.ShortDesc());
				int idx = m_ClipboardListMain.FindEntry(clp);
				if (idx > 0) //cannot remove last entry
					m_ClipboardListMain.RemoveAt(idx);
				m_contextMenuStripClipboard.Close(ToolStripDropDownCloseReason.AppFocusChange);
				m_bModified = true;
			}//end try
			finally
			{
				GC.Collect();
			}//end finally
		}//end m_contextMenuStrip_ClipboardEntry_Remove_Click

		private void m_contextMenuStripClipboard_Favorites_Entry_MouseUp(object sender, MouseEventArgs e)
		{
			//m_contextMenuStrip_ClipboardEntry_LeftClick(sender, e);
		}//end m_contextMenuStripClipboard_Favorites_Entry_MouseUp

		private void m_contextMenuStrip_ClipboardEntry_LeftClick(object sender, EventArgs e)
		{
			ToolStripItem itm = (ToolStripItem)sender;
			if (itm == null || itm.Tag == null)
				return;

			try
			{
				ClipboardList.ClipboardEntry clp = (ClipboardList.ClipboardEntry)itm.Tag;
				ClipboardList.ClipboardEntry last = m_ClipboardListMain.GetCurrentEntry();

				//special threatment for last entry click
				//for main list only
				if ( clp._ownerType == m_ClipboardListMain.m_sListType && last.Equals(clp) ) 
				{
					m_contextMenuStripTrayIcon_Show_Click(sender, e);
                }//end if
				else
				{
					//make this item first and preserve original icon
					m_ClipboardListMain.AddEntry(clp);

					m_bPasteFromThis = true;
					clp.Put();
                }//end else

                //close parent menu
                m_contextMenuStripClipboard.Visible = false;
            }//end try
			catch ( Exception err )
			{
				LogMethod("m_contextMenuStrip_ClipboardEntry_LeftClick()", 
					"Error: ", err.Message);
			}//end catch
			finally
			{
				//m_bPasteFromThis = false;
			}//end finally
		}//end m_contextMenuStrip_ClipboardEntry_LeftClick

		private void m_contextMenuStripClipboard_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if ( m_bLeaveContextMenuOpened )
				e.Cancel = true;
		}//end m_contextMenuStripClipboard_Closing

		private void m_contextMenuStripClipboard_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			//restore previous active window - if still foreground
			IntPtr hWndCurr = ForegroundWindow.Instance.Handle;

			LogMethod("m_contextMenuStripClipboard_Closed", "Caption1: {0}", NativeWIN32.GetWindowText(hWndCurr));
			LogMethod("m_contextMenuStripClipboard_Closed", "Caption2: {0}", NativeWIN32.GetWindowText(m_hWndToRestore));

			//close menu - clear screen
			m_contextMenuStripClipboard.Close(ToolStripDropDownCloseReason.AppFocusChange);

			if (hWndCurr == this.Handle)
			    hWndCurr = m_hWndToRestore;

			if ( m_hWndToRestore == hWndCurr )
			{
				if (m_hWndToRestore == this.Handle) //open this window
					m_contextMenuStripTrayIcon_Show_Click(sender, null);
				else //restore last foreground window
				{
					NativeWIN32.SetActiveWindow(m_hWndToRestore);
					NativeWIN32.SetForegroundWindow(m_hWndToRestore);
				}//end else
			}//end if

			m_hWndToRestore = IntPtr.Zero; //restored
		}//end m_contextMenuStripClipboard_Closed

		private void m_ToolStripMenuItem_File_Save_Click(object sender, EventArgs e)
		{
			ClipboardList.ClipboardEntry clp = m_ClipboardListMain.GetCurrentEntry();

			m_SaveFileDialog.FileName = "Clipboard";
			m_SaveFileDialog.Filter = clp.FileFilter();
			m_SaveFileDialog.FilterIndex = 1;
			if ( clp._icoItemType == 2 ) //rtf
				m_SaveFileDialog.FilterIndex = 2;

			if ( DialogResult.OK != m_SaveFileDialog.ShowDialog() )
				return;

			try
			{
				string sFileName = m_SaveFileDialog.FileName;
				if ( clp._icoItemType == 3 ) //image
				{
					clp.SavePicture(sFileName);
					return;
				}//end if

				byte[] buff = null;
				//rtf or text
				if ( m_SaveFileDialog.FilterIndex == 1 ) //Text
					buff = System.Text.UnicodeEncoding.Unicode.GetBytes(m_richTextBoxClipboard.Text);
				else if ( m_SaveFileDialog.FilterIndex == 2 ) //rtf
					buff = System.Text.ASCIIEncoding.ASCII.GetBytes(m_richTextBoxClipboard.Rtf);
				else
					throw new Exception("Unknown format");

				BinaryWriter wr = new BinaryWriter(File.Open(sFileName, FileMode.OpenOrCreate));
				wr.Write(buff);
				wr.Close();
			}//end try
			catch ( Exception err )
			{
				MessageBox.Show(this, "Cannot save: "+err.Message, this.Text,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}//end catch
		}//end m_ToolStripMenuItem_File_Save_Click

		private void m_ToolStripMenuItem_File_Hide_Click(object sender, EventArgs e)
		{
			this.Hide();
		}//end m_ToolStripMenuItem_File_Hide_Click

		private void m_ToolStripMenuItem_File_Exit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}//end m_ToolStripMenuItem_File_Exit_Click

		private RichTextBox GetRichTextBoxInFocus(object sender)
		{
			//because I use same context menu for both RichTextBoxs
			//I need to know where was it issued and restore focus 
			if ( sender != null )
			{
				Control ctrl = null;
				if ( sender is ContextMenuStrip )
					ctrl = ((ContextMenuStrip)sender).SourceControl;
				else if ( sender is ToolStripDropDownItem )
				{
					ToolStripDropDownItem itm = (ToolStripDropDownItem)sender;
					ToolStrip strp = itm.GetCurrentParent();
					if ( strp is ContextMenuStrip )
					{
						ContextMenuStrip str = (ContextMenuStrip)strp;
						ctrl = str.SourceControl;
					}//end if
					else if ( strp is ToolStripDropDownMenu )
					{
						ToolStripDropDownMenu str = (ToolStripDropDownMenu)strp;
						ctrl = str;
					}//end if
				}//end if
				if ( ctrl != null )
					ctrl.Focus();
			}//end if

			if ( m_richTextBoxClipboard.Focused )
				return m_richTextBoxClipboard;

			if ( m_richTextBoxSnapShot.Focused )
				return m_richTextBoxSnapShot;

			if ( m_richTextBoxDebug.Focused )
				return m_richTextBoxDebug;

			return m_richTextBoxClipboard;
		}//end GetRichTextBoxInFocus

		private void m_contextMenuStrip_RichTextBox_Opening(object sender, CancelEventArgs e)
		{
			EnableCopyPaste(sender);
		}//end m_contextMenuStrip_RichTextBox_Opening

		private void m_ToolStripMenuItem_Edit_Cut_Click(object sender, EventArgs e)
		{
			m_bCopyFromSnapShot = true;
			LogMethod("m_ToolStripMenuItem_Edit_Cut_Click", "Cut1");
			GetRichTextBoxInFocus(sender).Cut();
			LogMethod("m_ToolStripMenuItem_Edit_Cut_Click", "Cut2");
			//m_bCopyFromSnapShot = false;
		}//end m_ToolStripMenuItem_Edit_Cut_Click

		private void m_ToolStripMenuItem_Edit_Copy_Click(object sender, EventArgs e)
		{
			m_bCopyFromSnapShot = true;
			LogMethod("m_ToolStripMenuItem_Edit_Copy_Click", "Copy1");
			GetRichTextBoxInFocus(sender).Copy();
			LogMethod("m_ToolStripMenuItem_Edit_Copy_Click", "Copy2");
			//m_bCopyFromSnapShot = false;
		}//end m_ToolStripMenuItem_Edit_Copy_Click

		private void m_ToolStripMenuItem_Edit_Paste_Click(object sender, EventArgs e)
		{
			m_richTextBoxClipboard.Paste();
		}//end m_ToolStripMenuItem_Edit_Paste_Click

		private void m_ToolStripMenuItem_Tools_Settings_Click(object sender, EventArgs e)
		{
			try
			{
				FormSettings frm = new FormSettings(m_Settings);
				frm.Icon = this.Icon;
				if ( frm.ShowDialog(this) != DialogResult.OK )
					return;

				if ( m_Settings.m_AutoReconnect )
					m_TimerReconnect.Start();
				else
					m_TimerReconnect.Stop();

                ShutdownHandler.AbortShutdownIfScheduled = m_Settings.m_bAbortShutdown;
                Utils.ServicesManipulator.ContinuousMonitoringServices = m_Settings.m_bStopServices;
            }//end try
            catch ( Exception err )
			{
                CenteredMessageBox.MsgBoxErr("Error in options: {0}", err.ToString());
			}//end catch
		}//end m_ToolStripMenuItem_Tools_Settings_Click

		private void m_ToolStripMenuItem_Help_About_Click(object sender, EventArgs e)
		{
			FormAbout frm = new FormAbout();
			frm.Text = "About " + this.Text;
			frm.Icon = this.Icon;
			if ( DialogResult.OK != frm.ShowDialog(this) )
				return;
		}//end m_ToolStripMenuItem_Help_About_Click

		private void m_contextMenuStripTrayIcon_Show_Click(object sender, EventArgs e)
		{
			m_ClipboardListMain.GetCurrentEntry().SetRichText(m_richTextBoxSnapShot, m_icoSnapShotApp, m_lblSnapShotType);

			this.Visible = true;

			this.Show();
			this.WindowState = FormWindowState.Normal;

			//if out of desktop window more than a half
			if (this.Location.X < 0 || this.Location.X > SystemInformation.WorkingArea.Width - this.Width/2)
				this.Left = 200;

			if (this.Location.Y < 0 || this.Location.Y > SystemInformation.WorkingArea.Height - this.Height/2)
				this.Top = 150;

			NativeWIN32.SetForegroundWindow(this.Handle);
		}//end m_contextMenuStripTrayIcon_Show_Click

		private void m_notifyIconCoodClip_MouseClick(object sender, MouseEventArgs e)
		{
			LogMethod("m_notifyIconCoodClip_MouseClick", "m_notifyIconCoodClip 1 clicks: {0}", e.Clicks);
            if (e.Button == MouseButtons.Left)
            {
                //calculate rigth - lower quater of main screen
                //Rectangle r = Screen.PrimaryScreen.Bounds;
                //NativeWIN32.RECT rc = new NativeWIN32.RECT();
                //rc.Left = r.X + (int)(0.8 * r.Width);
                //rc.Top = r.Y + (int)(0.8 * r.Height);
                //rc.Right = r.X + r.Width;
                //rc.Bottom = r.Y + r.Height;
                NativeWIN32.RECT rc = SystemTray.GetTrayRectangle();

                ProcessHotkey(new Pos(rc));
            }
		}//end m_notifyIconCoodClip_MouseClick

		private void m_notifyIconCoodClip_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			LogMethod("m_notifyIconCoodClip_MouseDoubleClick", "m_notifyIconCoodClip 2 clicks: {0}", e.Clicks);
			
			//hide menu if opened
			//m_contextMenuStripClipboard.Visible = false;

			//if ( e.Button == MouseButtons.Left )
			//	m_contextMenuStripTrayIcon_Show_Click(sender, e);
		}//end m_notifyIconCoodClip_MouseDoubleClick

		private void m_contextMenuStripTrayIcon_History_Click(object sender, EventArgs e)
		{
            Point location = m_contextMenuStripTrayIcon.PointToScreen(new Point());
            ProcessHotkey(new Pos(new Size(location)));
		}//end m_contextMenuStripTrayIcon_History_Click

		private void m_contextMenuStripTrayIcon_Settings_Click(object sender, EventArgs e)
		{
			m_ToolStripMenuItem_Tools_Settings_Click(sender, e);
		}//end m_contextMenuStripTrayIcon_Settings_Click

		private void m_contextMenuStripTrayIcon_Exit_Click(object sender, EventArgs e)
		{
			m_ToolStripMenuItem_File_Exit_Click(sender, e);
		}//end m_contextMenuStripTrayIcon_Exit_Click

		private void m_toolStripButton_History_Click(object sender, EventArgs e)
		{
			ProcessHotkey(new Pos(this));
		}//end m_toolStripButton_History_Click

		private void m_toolStripButton_Cut_Click(object sender, EventArgs e)
		{
			m_ToolStripMenuItem_Edit_Cut_Click(sender, e);
		}//end m_toolStripButton_Cut_Click

		private void m_toolStripButton_Copy_Click(object sender, EventArgs e)
		{
			m_ToolStripMenuItem_Edit_Copy_Click(sender, e);
		}//end m_toolStripButton_Copy_Click

		private void m_toolStripButton_Paste_Click(object sender, EventArgs e)
		{
			m_ToolStripMenuItem_Edit_Paste_Click(sender, e);
		}//end m_toolStripButton_Paste_Click

		private void m_toolStripButton_Print_Click(object sender, EventArgs e)
		{

		}//end m_toolStripButton_Print_Click

		private void m_ToolStripMenuItem_File_ClearHistory_Click(object sender, EventArgs e)
		{
			m_ClipboardListMain.Clear();
			GC.Collect();
		}//end m_ToolStripMenuItem_File_ClearHistory_Click

		private void m_ToolStripMenuItem_File_ClearLast_Click(object sender, EventArgs e)
		{
			Clipboard.Clear();
		}//end m_ToolStripMenuItem_File_ClearLast_Click

		private void m_ToolStripMenuItem_Edit_Test_Click(object sender, EventArgs e)
		{
            //flush the log file
            Utils.Log.FlushLog();

			LogMethod("m_ToolStripMenuItem_Edit_Test_Click", "Click...");
			AnimEffect.AnimEffect eff = new AnimEffect.AnimEffect();
			eff.Play(this.Bounds, Color.Black, true);
			eff.Play(this.Bounds, Color.Black, false);
		}//end m_ToolStripMenuItem_Edit_Test_Click

		private void m_ToolStripMenuItem_File_ShowHistory_Click(object sender, EventArgs e)
		{
			ProcessHotkey(null);
		}//end m_ToolStripMenuItem_File_ShowHistory_Click

		private void m_toolStripButton_Wordwrap_Click(object sender, EventArgs e)
		{
			m_bCopyFromSnapShot = true; //WordWrap changes clipboard
			m_richTextBoxClipboard.WordWrap = m_toolStripButton_Wordwrap.Checked;
			m_richTextBoxSnapShot.WordWrap = m_toolStripButton_Wordwrap.Checked;
			m_richTextBoxDebug.WordWrap = m_toolStripButton_Wordwrap.Checked;
			m_bCopyFromSnapShot = false;
		}//end m_toolStripButton_Wordwrap_Click

		private void m_ToolStripMenuItem_Favorites_Add_Click(object sender, EventArgs e)
		{
			m_ClipboardListFavorites.AddEntry(m_ClipboardListMain.GetCurrentEntry());
		}//end m_ToolStripMenuItem_Favorites_Add_Click

		private void m_ToolStripMenuItem_Favorites_Organize_Click(object sender, EventArgs e)
		{
			FormFavorites frm = new FormFavorites(m_ClipboardListFavorites, m_imageListClipboardTypes);
			frm.Icon = this.Icon;
			frm.ShowDialog(this);
		}//end m_ToolStripMenuItem_Favorites_Organize_Click

		private void m_ToolStripMenuItem_Favorites_DropDownOpening(object sender, EventArgs e)
		{
			while (m_ToolStripMenuItem_Favorites.DropDownItems.Count > 3)
				m_ToolStripMenuItem_Favorites.DropDownItems.RemoveAt(3);
			m_ToolStripMenuItem_Favorites.DropDownItems.AddRange(BuildFavoritesList(false));
		}//end m_ToolStripMenuItem_Favorites_DropDownOpening

		private void EnableCopyPaste(object sender)
		{
			RichTextBox rich = GetRichTextBoxInFocus(sender);

			bool bReadOnly = rich.ReadOnly;
			bool bHasSelection = rich.SelectedText.Length > 0;

			this.m_ToolStripMenuItem_Edit_Cut.Enabled = !bReadOnly && bHasSelection;
			this.m_ToolStripMenuItem_Edit_Copy.Enabled = bHasSelection;
			this.m_ToolStripMenuItem_Edit_Paste.Enabled = !bReadOnly && !m_ClipboardListMain.GetCurrentEntry().IsEmpty;

			this.m_contextMenuStrip_RichTextBox_Cut.Enabled = this.m_ToolStripMenuItem_Edit_Cut.Enabled;
			this.m_contextMenuStrip_RichTextBox_Copy.Enabled = this.m_ToolStripMenuItem_Edit_Copy.Enabled;
			this.m_contextMenuStrip_RichTextBox_Paste.Enabled = this.m_ToolStripMenuItem_Edit_Paste.Enabled;

			this.m_toolStripButton_Cut.Enabled = this.m_ToolStripMenuItem_Edit_Cut.Enabled;
			this.m_toolStripButton_Copy.Enabled = this.m_ToolStripMenuItem_Edit_Copy.Enabled;
			this.m_toolStripButton_Paste.Enabled = this.m_ToolStripMenuItem_Edit_Paste.Enabled;
		}//end EnableCopyPaste

		private void m_richTextBoxClipboard_Enter(object sender, EventArgs e)
		{
			EnableCopyPaste(null);
		}//end m_richTextBoxClipboard_Enter

		private void m_richTextBoxClipboard_TextChanged(object sender, EventArgs e)
		{
			EnableCopyPaste(null);
		}//end m_richTextBoxClipboard_TextChanged

		private void m_richTextBoxClipboard_SelectionChanged(object sender, EventArgs e)
		{
			EnableCopyPaste(null);
		}//end m_richTextBoxClipboard_SelectionChanged

		private void m_richTextBoxClipboard_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}//end m_richTextBoxClipboard_LinkClicked

		private void m_richTextBoxSnapShot_Enter(object sender, EventArgs e)
		{
			EnableCopyPaste(null);
		}//end m_richTextBoxSnapShot_Enter

		private void m_richTextBoxSnapShot_SelectionChanged(object sender, EventArgs e)
		{
			EnableCopyPaste(null);
		}//end m_richTextBoxSnapShot_SelectionChanged

		private void m_richTextBoxSnapShot_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}//end m_richTextBoxSnapShot_LinkClicked

		private int m_iFontSize = 0;
		private void SetFontSize() { SetFontSize(m_iFontSize); }
		private void SetFontSize(int iSize)
		{
			bool bReset = false;
			if ( iSize != m_iFontSize )
				bReset = true;
			m_iFontSize = iSize;

			m_toolStripButton_FontSize_0.Checked = (iSize == 0);
			m_toolStripButton_FontSize_1.Checked = (iSize == 1);
			m_toolStripButton_FontSize_2.Checked = (iSize == 2);
			m_toolStripButton_FontSize_3.Checked = (iSize == 3);
			m_toolStripButton_FontSize_4.Checked = (iSize == 4);

			float [] vTextSize = new float[] { 8, 10, 14, 22, 38 };

			if ( iSize >= 0 && iSize < vTextSize.Length )
			{
				Font f = m_richTextBoxClipboard.Font;
				f = new Font("Courier New", vTextSize[iSize]);
				m_richTextBoxClipboard.Font = f;
				m_richTextBoxSnapShot.Font = f;
			}//end if
			else if ( bReset ) //reset font
			{
				m_richTextBoxClipboard.Font = SystemInformation.MenuFont;
				m_richTextBoxSnapShot.Font = SystemInformation.MenuFont;
			}//end else
		}//end SetFontSize

		private void m_ToolStripSplitButton_FontSize_ButtonClick(object sender, EventArgs e)
		{
			int iSize = m_iFontSize + 1;
			if ( iSize > 4 )
				iSize = 0;
			SetFontSize(iSize);
		}//end m_ToolStripSplitButton_FontSize_ButtonClick

		private void m_toolStripButton_FontSize_0_Click(object sender, EventArgs e)
		{
			SetFontSize(0);
		}//end m_toolStripButton_FontSize_0_Click

		private void m_toolStripButton_FontSize_1_Click(object sender, EventArgs e)
		{
			SetFontSize(1);
		}//end m_toolStripButton_FontSize_1_Click

		private void m_toolStripButton_FontSize_2_Click(object sender, EventArgs e)
		{
			SetFontSize(2);
		}//end m_toolStripButton_FontSize_2_Click

		private void m_toolStripButton_FontSize_3_Click(object sender, EventArgs e)
		{
			SetFontSize(3);
		}//end m_toolStripButton_FontSize_3_Click

		private void m_toolStripButton_FontSize_4_Click(object sender, EventArgs e)
		{
			SetFontSize(4);
		}//end m_toolStripButton_FontSize_4_Click
		public static void TraceLn(bool bAlways, string sModule, string sMethod, string sFormat, params object[] args)
		{
			if ( m_This == null )
				return;

			m_This.LogMethodEx(bAlways, sModule, sMethod, sFormat, args);
		}//end TraceLnEx
		
		private void LogMethod(string sMethod, string sFormat, params object[] args)
		{
			LogMethodEx(false, "FormClipboard", sMethod, sFormat, args);
		}//end TraceLn

		delegate void TraceLnExCallback(bool bAlways, string sModule, string sMethod, string sFormat, params object[] args);
		private void LogMethodEx(bool bAlwaysAddToDebugWindow, string sModule, string sMethod, string sFormat, params object[] args)
		{
			try
			{
                string sMessage = Utils.Log.WriteLineF(bAlwaysAddToDebugWindow, 
                    "[{0}][{1}] : {2}", sModule, sMethod, string.Format(sFormat, args));

                //only if debug window is open
                AppendTextToDebugWindow(bAlwaysAddToDebugWindow, sMessage);
			}//end try
			catch ( Exception err )
			{
                Utils.Log.WriteLineF("Problem in LogMethodEx: " + err.ToString());
			}//end catch
		}//end TraceLnEx

        private void AppendTextToDebugWindow(bool bAlwaysAddToDebugWindow, string sMessage)
        {
            if (this.InvokeRequired)
            {
                Utils.Log.WriteLog(bAlwaysAddToDebugWindow, false, 
                    "[AppendTextToDebugWindow][Invoke Required] : " + sMessage);

                this.BeginInvoke(new MethodInvoker(() => {
                    AppendTextToDebugWindow(bAlwaysAddToDebugWindow, sMessage);
                }));

                return;
            }//end if

            //only if debug window is open
            if (!bAlwaysAddToDebugWindow)
            {
                if (!m_ToolStripMenuItem_View_Debug.Checked)
                    return;

                if (!m_richTextBoxDebug.Visible)
                    return;
            }//end if

            m_richTextBoxDebug.AppendText(sMessage);
            bool bScroll = m_btnDebugScroll.ImageIndex == 0;
            if (bScroll)
                m_richTextBoxDebug.ScrollToCaret();
        }

        private void m_btnDebugClear_Click(object sender, EventArgs e)
		{
			m_richTextBoxDebug.Text = "";
		}//end m_btnDebugClear_Click

		private void m_btnDebugCopy_Click(object sender, EventArgs e)
		{
			if ( m_richTextBoxDebug.Text.Length == 0 )
				return;

			if ( m_richTextBoxDebug.SelectedText.Length > 0 )
				Clipboard.SetText(m_richTextBoxDebug.SelectedText);
			else
				Clipboard.SetText(m_richTextBoxDebug.Text);
		}//end m_btnDebugCopy_Click

		private void m_btnDebugScroll_Click(object sender, EventArgs e)
		{
			bool bScroll = m_btnDebugScroll.ImageIndex == 0;
			bScroll = !bScroll; //toggle

			m_ToolTip.SetToolTip(this.m_btnDebugScroll, bScroll ? "Scroll Unlocked" : "Scroll Locked");
			m_btnDebugScroll.ImageIndex = bScroll ? 0 : 1; 
		}//end m_btnDebugScroll_Click

		private void m_ToolStripMenuItem_View_Debug_Click(object sender, EventArgs e)
		{
			bool bShow = m_ToolStripMenuItem_View_Debug.Checked;
			m_HSplitterDebug.Visible = bShow;
			m_pnlDebug.Visible = bShow;
		}//end m_ToolStripMenuItem_View_Debug_Click

		private void m_ToolStripMenuItem_View_SnapShot_Click(object sender, EventArgs e)
		{
			bool bShow = m_ToolStripMenuItem_View_SnapShot.Checked;
			m_splitContainerClipboard.Panel2Collapsed = !bShow;
		}//end m_ToolStripMenuItem_View_SnapShot_Click

		private void m_ToolStripMenuItem_Tools_ReverseChars_Click(object sender, EventArgs e)
		{
			string s = m_richTextBoxClipboard.SelectedText;
			if ( s == null || s.Length == 0 )
			{
                CenteredMessageBox.MsgBoxIfo("No text selected");
				return;
			}//end if
			StringBuilder sb = new StringBuilder(s.Length);
			for ( int i = s.Length - 1; i >= 0; i-- )
			{
				sb.Append(s[i]);
			}//end for
			m_richTextBoxClipboard.SelectedText = sb.ToString();
		}//end m_ToolStripMenuItem_Tools_ReverseChars_Click

        private void m_ToolStripMenuItem_Tools_UnescapeURI_Click(object sender, EventArgs e)
        {
            string s = m_richTextBoxClipboard.SelectedText;
            if (s == null || s.Length == 0)
            {
                MessageBox.Show(this, "No text selected", this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }//end if
            m_richTextBoxClipboard.SelectedText = Uri.UnescapeDataString(s);
        }//end m_ToolStripMenuItem_Tools_UnescapeURI_Click

		private void m_ToolStripMenuItem_Tools_Encoding_Config_Click(object sender, EventArgs e)
		{
			FormEncodings frm = new FormEncodings(m_Settings.m_Encodings);
			frm.Icon = this.Icon;
			if ( DialogResult.OK != frm.ShowDialog(this) )
				return;

			m_Settings.m_Encodings.CreateEncodingsMenuItems(
				m_ToolStripMenuItem_Tools_Encoding.DropDownItems,
				m_richTextBoxSnapShot,
				m_richTextBoxClipboard);
		}//end m_ToolStripMenuItem_Tools_Encoding_Config_Click

		private void m_contextMenuStripTrayIcon_Reconnect_Click(object sender, EventArgs e)
		{
            bool bOldModified = m_bModified;
            try
            {
                //unregister
                NativeWIN32.ChangeClipboardChain(this.Handle, m_NextClipboardViewer);
                //register
                m_NextClipboardViewer = (IntPtr)NativeWIN32.SetClipboardViewer((int)this.Handle);

                //save Desktop snapshot
                m_contextMenuStripTrayIcon_DesktopSave_Click(sender, e);
            }//end try
            catch (Exception err)
            {
                LogMethod("m_contextMenuStripTrayIcon_Reconnect_Click()", "Error: {0}", err.ToString());
            }//end catch
            finally { m_bModified = bOldModified; }
		}//end m_contextMenuStripTrayIcon_Reconnect_Click

        private void m_contextMenuStripTrayIcon_DesktopSave_Click(object sender, EventArgs e)
        {
            try
            {
                DesctopCommands desctopCommands = new DesctopCommands();
                desctopCommands.SavePositions.Execute(new object());
            }
            catch (Exception err)
            {
                CenteredMessageBox.MsgBoxErr("DesktopSave_Click: " + err.Message);
            }
        }

        private void m_contextMenuStripTrayIcon_DesktopRestore_Click(object sender, EventArgs e)
        {
            try
            {
                DesctopCommands desctopCommands = new DesctopCommands();
                desctopCommands.RestorePositions.Execute(new object());
            }
            catch (Exception err)
            {
                CenteredMessageBox.MsgBoxErr("DesktopRestore_Click: " + err.Message);
            }
        }

        private void m_contextMenuStripTrayIcon_UAC_Click(object sender, EventArgs e)
        {
            try
            {
                bool bUserClick = (sender != null);
                const string REG_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_KEY, writable: true);
                int val = (int)key.GetValue("EnableLUA");
                if(val != 0)
                {
                    bool reset = m_Settings.m_bAutoUAC;
                    if ((bUserClick || !m_Settings.m_bAutoUAC)) //show question if user clicked or not Automatic UAC
                    {
                        reset = (DialogResult.Yes == MessageBox.Show(
                            @"User Account Control Enabled\n  Disable?", @"EnableLUA",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification));
                    }

                    if (reset)
                    {
                        key.SetValue("EnableLUA", 0);
                    }
                }
                else if (bUserClick) //comes from user
                {
                    CenteredMessageBox.MsgBoxIfo("UAC disabled");
                }
            }
            catch (Exception err)
            {
                CenteredMessageBox.MsgBoxErr("UAC_Click: " + err.Message);
            }
        }

        private void m_tbbtnDataFolder_Click(object sender, EventArgs e)
        {
            m_mnuToolsOpenLogFolder_Click(sender, e);
        }

        private void m_mnuToolsOpenLogFolder_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(m_sFileName))
                Process.Start(Path.GetDirectoryName(m_sFileName));
        }

        private void m_contextMenuStripTrayIcon_About_Click(object sender, EventArgs e)
		{
			m_ToolStripMenuItem_Help_About_Click(sender, e);
        }//end m_contextMenuStripTrayIcon_About_Click

		private void m_contextMenuStripTrayIcon_Spy_Click(object sender, EventArgs e)
		{
			m_toolStripButton_Target_Click(sender, e);
		}
	}//end class class FormClipboard
}//end namespace ClipboardListener