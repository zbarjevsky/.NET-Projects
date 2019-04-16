using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ClipboardManager
{
    using System.Drawing;
    using HWND = IntPtr;

    public class NativeWIN32
	{
		[StructLayout(LayoutKind.Sequential)] 
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;

			public override string ToString()
			{
				return "l:" + Left + ",t:" + Top + ",r:" + Right + ",b:" + Bottom;
			}//end ToString
		}//end struct RECT

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int x;
			public int y;

			public override string ToString()
			{
				return "l:" + x + ",t:" + y;
			}//end ToString
		}//end struct RECT

		[DllImport("user32.dll")]
		public static extern int SetClipboardViewer(int hWndNewViewer);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool ChangeClipboardChain(HWND hWndRemove, HWND hWndNewNext);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(HWND hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(HWND hwnd, int wMsg, int wParam, StringBuilder lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(HWND hwnd, int wMsg, int wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr PostMessage(HWND hwnd, int wMsg, int wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr GetClassLong(HWND hwnd, int wMsg);

		//flash window several times
		[DllImport("user32.dll")]
		public extern static bool FlashWindow(HWND hwnd, bool bInvert);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll")]
		public extern static IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public extern static IntPtr SetForegroundWindow(HWND hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetActiveWindow();

		[DllImport("user32.dll")]
		public static extern IntPtr SetActiveWindow(HWND hWnd);

		[DllImport ("user32.dll", EntryPoint="GetWindowTextA", CallingConvention=CallingConvention.StdCall)]
		internal extern static bool GetWindowTextA(HWND hWnd, StringBuilder lpString, int nMaxCount);
		
		[DllImport("user32.dll", EntryPoint = "GetWindowTextW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		internal extern static bool GetWindowTextW(HWND hWnd, StringBuilder lpString, int nMaxCount);

		private const int iMaxText = 4000;
		public static string GetWindowText(HWND hWnd)
		{
			StringBuilder sb = new StringBuilder(iMaxText);
			GetWindowTextW(hWnd, sb, iMaxText);
			return sb.ToString();
		}//end GetWindowText

		public static string GetWindowTextA(HWND hWnd)
		{
			StringBuilder sb = new StringBuilder(iMaxText);
			GetWindowTextA(hWnd, sb, iMaxText);
			return sb.ToString();
		}//end GetWindowText

		public static string GetText(HWND hWnd)
		{
			int length = (int)SendMessage(hWnd, (int)WindowMessages.WM_GETTEXTLENGTH, 0, IntPtr.Zero);
			StringBuilder sb = new StringBuilder(length + 1);
			SendMessage(hWnd, (int)WindowMessages.WM_GETTEXT, length + 1, sb);
			return sb.ToString();
		}//end GetText

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetWindowRect(HWND hWnd, out RECT lpRect);

		public static RECT GetWindowRect(HWND hWnd) 
		{ 
			RECT rc; 
			GetWindowRect(hWnd, out rc); 
			return rc;
		}//end GetWindowRect
		
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool RegisterHotKey(
						HWND hWnd,				// handle to window    
						int id,						// hot key identifier    
						KeyModifiers fsModifiers,	// key-modifier options    
						Keys vk						// virtual-key code    
		);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnregisterHotKey(
								HWND hWnd,	// handle to window    
								int id			// hot key identifier    
		);

		[Flags()]
		public enum KeyModifiers
		{
			None		= 0,
			Alt			= 1,
			Control		= 2,
			Shift		= 4,
			Windows		= 8
		}//end enum KeyModifiers

		[DllImport("user32.dll")]
		public static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);

		public const int MF_SEPARATOR	= 0xA00; //for uFlags
		public const int MF_BYCOMMAND	= 0x00000000; //for uFlags
		public const int MF_BYPOSITION = 0x00000400; //for uFlags
		public const int MF_LAST = -1; //for uFlags

		[DllImport("user32.dll")]
		public static extern int AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

		[DllImport("user32.dll")]
		public static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

		[DllImport("user32.dll")]
		public static extern bool SetMenuItemBitmaps(IntPtr hMenu, int uPosition, int uFlags, IntPtr hBitmapUnchecked, IntPtr hBitmapChecked);

		#region Windows - user32.dll

		public const int GWL_HWNDPARENT = (-8);
		public const int GWL_EXSTYLE = (-20);
		public const int GWL_STYLE = (-16);
		public const int GCL_HICON = (-14);
		public const int GCL_HICONSM = (-34);
		public const int WM_QUERYDRAGICON = 0x37;
		public const int WM_GETICON = 0x7F;
		public const int WM_SETICON = 0x80;
		public const int ICON_SMALL = 0;
		public const int ICON_BIG = 1;
		public const int SMTO_ABORTIFHUNG = 0x2;
		public const int TRUE = 1;
		public const int FALSE = 0;

		public const int WHITE_BRUSH = 0;
		public const int LTGRAY_BRUSH = 1;
		public const int GRAY_BRUSH = 2;
		public const int DKGRAY_BRUSH = 3;
		public const int BLACK_BRUSH = 4;
		public const int NULL_BRUSH = 5;
		public const int HOLLOW_BRUSH = NULL_BRUSH;
		public const int WHITE_PEN = 6;
		public const int BLACK_PEN = 7;
		public const int NULL_PEN = 8;
		public const int OEM_FIXED_FONT = 10;
		public const int ANSI_FIXED_FONT = 11;
		public const int ANSI_VAR_FONT = 12;
		public const int SYSTEM_FONT = 13;
		public const int DEVICE_DEFAULT_FONT = 14;
		public const int DEFAULT_PALETTE = 15;
		public const int SYSTEM_FIXED_FONT = 16;


		public const int RDW_INVALIDATE = 0x0001;
		public const int RDW_INTERNALPAINT = 0x0002;
		public const int RDW_ERASE = 0x0004;

		public const int RDW_VALIDATE = 0x0008;
		public const int RDW_NOINTERNALPAINT = 0x0010;
		public const int RDW_NOERASE = 0x0020;

		public const int RDW_NOCHILDREN = 0x0040;
		public const int RDW_ALLCHILDREN = 0x0080;

		public const int RDW_UPDATENOW = 0x0100;
		public const int RDW_ERASENOW = 0x0200;

		public const int RDW_FRAME = 0x0400;
		public const int RDW_NOFRAME = 0x0800;

		public enum ShowWindowCmds
		{
			SW_HIDE = 0,
			SW_SHOWNORMAL = 1,
			SW_NORMAL = 1,
			SW_SHOWMINIMIZED = 2,
			SW_SHOWMAXIMIZED = 3,
			SW_MAXIMIZE = 3,
			SW_SHOWNOACTIVATE = 4,
			SW_SHOW = 5,
			SW_MINIMIZE = 6,
			SW_SHOWMINNOACTIVE = 7,
			SW_SHOWNA = 8,
			SW_RESTORE = 9,
			SW_SHOWDEFAULT = 10,
			SW_FORCEMINIMIZE = 11,
			SW_MAX = 11
		}//end enum ShowWindowCmds

		public const int HIDE_WINDOW = 0;
		public const int SHOW_OPENWINDOW = 1;
		public const int SHOW_ICONWINDOW = 2;
		public const int SHOW_FULLSCREEN = 3;
		public const int SHOW_OPENNOACTIVATE = 4;
		public const int SW_PARENTCLOSING = 1;
		public const int SW_OTHERZOOM = 2;
		public const int SW_PARENTOPENING = 3;
		public const int SW_OTHERUNZOOM = 4;

		public const int SWP_NOSIZE = 0x0001;
		public const int SWP_NOMOVE = 0x0002;
		public const int SWP_NOZORDER = 0x0004;
		public const int SWP_NOREDRAW = 0x0008;
		public const int SWP_NOACTIVATE = 0x0010;
		public const int SWP_FRAMECHANGED = 0x0020; /* The frame changed: send WM_NCCALCSIZE */
		public const int SWP_SHOWWINDOW = 0x0040;
		public const int SWP_HIDEWINDOW = 0x0080;
		public const int SWP_NOCOPYBITS = 0x0100;
		public const int SWP_NOOWNERZORDER = 0x0200; /* Don't do owner Z ordering */
		public const int SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */
		public const int SWP_DRAWFRAME = SWP_FRAMECHANGED;
		public const int SWP_NOREPOSITION = SWP_NOOWNERZORDER;
		public const int SWP_DEFERERASE = 0x2000;
		public const int SWP_ASYNCWINDOWPOS = 0x4000;

		public const int HWND_TOP = 0;
		public const int HWND_BOTTOM = 1;
		public const int HWND_TOPMOST = -1;
		public const int HWND_NOTOPMOST = -2;

		public enum PeekMessageFlags
		{
			PM_NOREMOVE = 0,
			PM_REMOVE = 1,
			PM_NOYIELD = 2
		}//end enum PeekMessageFlags

		public enum WindowMessages
		{
			WM_NULL = 0x0000,
			WM_CREATE = 0x0001,
			WM_DESTROY = 0x0002,
			WM_MOVE = 0x0003,
			WM_SIZE = 0x0005,
			WM_ACTIVATE = 0x0006,
			WM_SETFOCUS = 0x0007,
			WM_KILLFOCUS = 0x0008,
			WM_ENABLE = 0x000A,
			WM_SETREDRAW = 0x000B,
			WM_SETTEXT = 0x000C,
			WM_GETTEXT = 0x000D,
			WM_GETTEXTLENGTH = 0x000E,
			WM_PAINT = 0x000F,
			WM_CLOSE = 0x0010,
			WM_QUERYENDSESSION = 0x0011,
			WM_QUIT = 0x0012,
			WM_QUERYOPEN = 0x0013,
			WM_ERASEBKGND = 0x0014,
			WM_SYSCOLORCHANGE = 0x0015,
			WM_ENDSESSION = 0x0016,
			WM_SHOWWINDOW = 0x0018,
			WM_CTLCOLOR = 0x0019,
			WM_WININICHANGE = 0x001A,
			WM_SETTINGCHANGE = 0x001A,
			WM_DEVMODECHANGE = 0x001B,
			WM_ACTIVATEAPP = 0x001C,
			WM_FONTCHANGE = 0x001D,
			WM_TIMECHANGE = 0x001E,
			WM_CANCELMODE = 0x001F,
			WM_SETCURSOR = 0x0020,
			WM_MOUSEACTIVATE = 0x0021,
			WM_CHILDACTIVATE = 0x0022,
			WM_QUEUESYNC = 0x0023,
			WM_GETMINMAXINFO = 0x0024,
			WM_PAINTICON = 0x0026,
			WM_ICONERASEBKGND = 0x0027,
			WM_NEXTDLGCTL = 0x0028,
			WM_SPOOLERSTATUS = 0x002A,
			WM_DRAWITEM = 0x002B,
			WM_MEASUREITEM = 0x002C,
			WM_DELETEITEM = 0x002D,
			WM_VKEYTOITEM = 0x002E,
			WM_CHARTOITEM = 0x002F,
			WM_SETFONT = 0x0030,
			WM_GETFONT = 0x0031,
			WM_SETHOTKEY = 0x0032,
			WM_GETHOTKEY = 0x0033,
			WM_QUERYDRAGICON = 0x0037,
			WM_COMPAREITEM = 0x0039,
			WM_GETOBJECT = 0x003D,
			WM_COMPACTING = 0x0041,
			WM_COMMNOTIFY = 0x0044,
			WM_WINDOWPOSCHANGING = 0x0046,
			WM_WINDOWPOSCHANGED = 0x0047,
			WM_POWER = 0x0048,
			WM_COPYDATA = 0x004A,
			WM_CANCELJOURNAL = 0x004B,
			WM_NOTIFY = 0x004E,
			WM_INPUTLANGCHANGEREQUEST = 0x0050,
			WM_INPUTLANGCHANGE = 0x0051,
			WM_TCARD = 0x0052,
			WM_HELP = 0x0053,
			WM_USERCHANGED = 0x0054,
			WM_NOTIFYFORMAT = 0x0055,
			WM_CONTEXTMENU = 0x007B,
			WM_STYLECHANGING = 0x007C,
			WM_STYLECHANGED = 0x007D,
			WM_DISPLAYCHANGE = 0x007E,
			WM_GETICON = 0x007F,
			WM_SETICON = 0x0080,
			WM_NCCREATE = 0x0081,
			WM_NCDESTROY = 0x0082,
			WM_NCCALCSIZE = 0x0083,
			WM_NCHITTEST = 0x0084,
			WM_NCPAINT = 0x0085,
			WM_NCACTIVATE = 0x0086,
			WM_GETDLGCODE = 0x0087,
			WM_SYNCPAINT = 0x0088,
			WM_NCMOUSEMOVE = 0x00A0,
			WM_NCLBUTTONDOWN = 0x00A1,
			WM_NCLBUTTONUP = 0x00A2,
			WM_NCLBUTTONDBLCLK = 0x00A3,
			WM_NCRBUTTONDOWN = 0x00A4,
			WM_NCRBUTTONUP = 0x00A5,
			WM_NCRBUTTONDBLCLK = 0x00A6,
			WM_NCMBUTTONDOWN = 0x00A7,
			WM_NCMBUTTONUP = 0x00A8,
			WM_NCMBUTTONDBLCLK = 0x00A9,
			WM_KEYDOWN = 0x0100,
			WM_KEYUP = 0x0101,
			WM_CHAR = 0x0102,
			WM_DEADCHAR = 0x0103,
			WM_SYSKEYDOWN = 0x0104,
			WM_SYSKEYUP = 0x0105,
			WM_SYSCHAR = 0x0106,
			WM_SYSDEADCHAR = 0x0107,
			WM_KEYLAST = 0x0108,
			WM_IME_STARTCOMPOSITION = 0x010D,
			WM_IME_ENDCOMPOSITION = 0x010E,
			WM_IME_COMPOSITION = 0x010F,
			WM_IME_KEYLAST = 0x010F,
			WM_INITDIALOG = 0x0110,
			WM_COMMAND = 0x0111,
			WM_SYSCOMMAND = 0x0112,
			WM_TIMER = 0x0113,
			WM_HSCROLL = 0x0114,
			WM_VSCROLL = 0x0115,
			WM_INITMENU = 0x0116,
			WM_INITMENUPOPUP = 0x0117,
			WM_MENUSELECT = 0x011F,
			WM_MENUCHAR = 0x0120,
			WM_ENTERIDLE = 0x0121,
			WM_MENURBUTTONUP = 0x0122,
			WM_MENUDRAG = 0x0123,
			WM_MENUGETOBJECT = 0x0124,
			WM_UNINITMENUPOPUP = 0x0125,
			WM_MENUCOMMAND = 0x0126,
			WM_CTLCOLORMSGBOX = 0x0132,
			WM_CTLCOLOREDIT = 0x0133,
			WM_CTLCOLORLISTBOX = 0x0134,
			WM_CTLCOLORBTN = 0x0135,
			WM_CTLCOLORDLG = 0x0136,
			WM_CTLCOLORSCROLLBAR = 0x0137,
			WM_CTLCOLORSTATIC = 0x0138,
			WM_MOUSEMOVE = 0x0200,
			WM_LBUTTONDOWN = 0x0201,
			WM_LBUTTONUP = 0x0202,
			WM_LBUTTONDBLCLK = 0x0203,
			WM_RBUTTONDOWN = 0x0204,
			WM_RBUTTONUP = 0x0205,
			WM_RBUTTONDBLCLK = 0x0206,
			WM_MBUTTONDOWN = 0x0207,
			WM_MBUTTONUP = 0x0208,
			WM_MBUTTONDBLCLK = 0x0209,
			WM_MOUSEWHEEL = 0x020A,
			WM_PARENTNOTIFY = 0x0210,
			WM_ENTERMENULOOP = 0x0211,
			WM_EXITMENULOOP = 0x0212,
			WM_NEXTMENU = 0x0213,
			WM_SIZING = 0x0214,
			WM_CAPTURECHANGED = 0x0215,
			WM_MOVING = 0x0216,
			WM_DEVICECHANGE = 0x0219,
			WM_MDICREATE = 0x0220,
			WM_MDIDESTROY = 0x0221,
			WM_MDIACTIVATE = 0x0222,
			WM_MDIRESTORE = 0x0223,
			WM_MDINEXT = 0x0224,
			WM_MDIMAXIMIZE = 0x0225,
			WM_MDITILE = 0x0226,
			WM_MDICASCADE = 0x0227,
			WM_MDIICONARRANGE = 0x0228,
			WM_MDIGETACTIVE = 0x0229,
			WM_MDISETMENU = 0x0230,
			WM_ENTERSIZEMOVE = 0x0231,
			WM_EXITSIZEMOVE = 0x0232,
			WM_DROPFILES = 0x0233,
			WM_MDIREFRESHMENU = 0x0234,
			WM_IME_SETCONTEXT = 0x0281,
			WM_IME_NOTIFY = 0x0282,
			WM_IME_CONTROL = 0x0283,
			WM_IME_COMPOSITIONFULL = 0x0284,
			WM_IME_SELECT = 0x0285,
			WM_IME_CHAR = 0x0286,
			WM_IME_REQUEST = 0x0288,
			WM_IME_KEYDOWN = 0x0290,
			WM_IME_KEYUP = 0x0291,
			WM_MOUSEHOVER = 0x02A1,
			WM_MOUSELEAVE = 0x02A3,
			WM_CUT = 0x0300,
			WM_COPY = 0x0301,
			WM_PASTE = 0x0302,
			WM_CLEAR = 0x0303,
			WM_UNDO = 0x0304,
			WM_RENDERFORMAT = 0x0305,
			WM_RENDERALLFORMATS = 0x0306,
			WM_DESTROYCLIPBOARD = 0x0307,
			WM_DRAWCLIPBOARD = 0x0308,
			WM_PAINTCLIPBOARD = 0x0309,
			WM_VSCROLLCLIPBOARD = 0x030A,
			WM_SIZECLIPBOARD = 0x030B,
			WM_ASKCBFORMATNAME = 0x030C,
			WM_CHANGECBCHAIN = 0x030D,
			WM_HSCROLLCLIPBOARD = 0x030E,
			WM_QUERYNEWPALETTE = 0x030F,
			WM_PALETTEISCHANGING = 0x0310,
			WM_PALETTECHANGED = 0x0311,
			WM_HOTKEY = 0x0312,
			WM_PRINT = 0x0317,
			WM_PRINTCLIENT = 0x0318,
			WM_HANDHELDFIRST = 0x0358,
			WM_HANDHELDLAST = 0x035F,
			WM_AFXFIRST = 0x0360,
			WM_AFXLAST = 0x037F,
			WM_PENWINFIRST = 0x0380,
			WM_PENWINLAST = 0x038F,
			WM_APP = 0x8000,
			WM_USER = 0x0400,
			WM_REFLECT = WM_USER + 0x1c00
		}//end enum WindowMessages
        #endregion Windows - user32.dll

        #region Spy
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("User32.dll")]
		public static extern int GetClassName(HWND hWnd, StringBuilder lpClassName, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern IntPtr SetCapture(HWND hWnd);

		[DllImport("user32.dll")]
		public static extern int ReleaseCapture();

		[DllImport("user32.dll")]
		public static extern int InvalidateRect(HWND hWnd, IntPtr lpRect, int bErase);

		[DllImport("user32.dll")]
		public static extern int UpdateWindow(HWND hWnd);

		[DllImport("user32.dll")]
		public static extern int RedrawWindow(HWND hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(System.Drawing.Point pt);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(HWND hwnd);

		[DllImport("user32.dll")]
		public static extern Int32 ReleaseDC(HWND hwnd, IntPtr hdc);

		//	child windows belonging to a parent window contains the specified point. 
		[DllImport("user32.dll")]
		public static extern HWND ChildWindowFromPoint(
			HWND hWndParent,		// handle to parent window
			System.Drawing.Point pt // structure with point coordinates
		);	

		//The EnumChildWindows function enumerates the child windows that belong 
		//to the specified parent window by passing the handle of each child 
		//window, in turn, to an application-defined callback function. 
		//EnumChildWindows continues until the last child window is enumerated or 
		//the callback function returns FALSE. 
		//Parameters
		//* hWndParent
		//  Identifies the parent window whose child windows are to be 
		//  enumerated. 
		//* lpEnumFunc
		//  Points to an application-defined callback function. For more 
		//  information about the callback function, see the EnumChildProc 
		//  callback function. 
		//* lParam
		//  Specifies a 32-bit, application-defined value to be passed to the 
		//  callback function. 
		//------------------------------------------------------------------------

		[DllImport("user32.dll")]
		public static extern int EnumChildWindows(
			HWND hWndParent,				// handle to parent window
			WindowEnumDelegate lpEnumFunc,	// pointer to callback function
			int lParam 						// application-defined value
		);	

		public static void EnumChildWindows(HWND hWndParent, string title)
		{
			System.Diagnostics.Trace.WriteLine("======================"+title+"=====================");
			WindowEnumDelegate del = new WindowEnumDelegate(WindowEnumProc);
			EnumChildWindows(hWndParent, del, 0);
		}//end EnumChildWindows

		public delegate bool WindowEnumDelegate (HWND hwnd, int lParam);
		public static bool WindowEnumProc(HWND hwnd, int lParam) 
		{
			// get the text from the window
			string text = GetText(hwnd);

			if(text.Length > 0) {
				System.Diagnostics.Trace.WriteLine(text);
			}//end if
			return true;
		}//end WindowEnumProc
		#endregion Spy
	}//end class NativeWIN32

	public class ForegroundWindow : IWin32Window
	{
		private static ForegroundWindow _window = new ForegroundWindow();
		private ForegroundWindow() { }

		public static IWin32Window Instance
		{
			get { return _window; }
		}

		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		IntPtr IWin32Window.Handle
		{
			get
			{
				return GetForegroundWindow();
			}
		}
	}//end class ForegroundWindow

    public class SystemTray
    {

        public static IntPtr GetTrayHandle()
        {
            IntPtr taskBarHandle = NativeWIN32.FindWindow("Shell_TrayWnd", null);
            if (!taskBarHandle.Equals(IntPtr.Zero))
            {
                return NativeWIN32.FindWindowEx(taskBarHandle, IntPtr.Zero, "TrayNotifyWnd", IntPtr.Zero);
            }
            return IntPtr.Zero;
        }

        public static NativeWIN32.RECT GetTrayRectangle()
        {
            NativeWIN32.RECT rect;
            NativeWIN32.GetWindowRect(GetTrayHandle(), out rect);
            return rect;
        }
    }
}//end namespace ClipboardListener
