using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Tools
{
    using HWND = IntPtr;

    public class WindowInfo
    {
        public HWND hWnd { get; set; }
        public string Title { get; set; }

        public WindowInfo(IntPtr hWnd, string title)
        {
            Title = title;
            this.hWnd = hWnd;
        }

        public System.Windows.Forms.IWin32Window Win32Window { get { return System.Windows.Forms.Control.FromHandle(hWnd); } }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Title))
                return Title;
            return "---";
        }
    }

    public static class User32HotKey
    {

        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
                        HWND hWnd,                  // handle to window    
                        int id,                     // hot key identifier    
                        KeyModifiers fsModifiers,   // key-modifier options    
                        Keys vk                     // virtual-key code    
        );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
                                HWND hWnd,  // handle to window    
                                int id      // hot key identifier    
        );

    }

    public static class User32Clipboard
    {
        [DllImport("user32.dll")]
        public static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(HWND hWndRemove, HWND hWndNewNext);
    }

    public static class User32SystemMenu
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);

        public const int MF_SEPARATOR = 0xA00; //for uFlags
        public const int MF_BYCOMMAND = 0x00000000; //for uFlags
        public const int MF_BYPOSITION = 0x00000400; //for uFlags
        public const int MF_LAST = -1; //for uFlags

        [DllImport("user32.dll")]
        public static extern int AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        public static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        public static extern bool SetMenuItemBitmaps(IntPtr hMenu, int uPosition, int uFlags, IntPtr hBitmapUnchecked, IntPtr hBitmapChecked);
    }

    public class User32SystemTray
    {

        public static IntPtr GetTrayHandle()
        {
            IntPtr taskBarHandle = User32.FindWindow("Shell_TrayWnd", null);
            if (!taskBarHandle.Equals(IntPtr.Zero))
            {
                return User32.FindWindowEx(taskBarHandle, IntPtr.Zero, "TrayNotifyWnd", IntPtr.Zero);
            }
            return IntPtr.Zero;
        }

        public static User32.RECT GetTrayRectangle()
        {
            User32.GetWindowRect(GetTrayHandle(), out User32.RECT rect);
            return rect;
        }
    }

    public static class User32EnumChildWindows
    {
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
            HWND hWndParent,                // handle to parent window
            WindowEnumDelegate lpEnumFunc,  // pointer to callback function
            int lParam                      // application-defined value
        );

        public static void EnumChildWindows(HWND hWndParent, string title)
        {
            System.Diagnostics.Trace.WriteLine("======================" + title + "=====================");
            WindowEnumDelegate del = new WindowEnumDelegate(WindowEnumProc);
            EnumChildWindows(hWndParent, del, 0);
        }//end EnumChildWindows

        public delegate bool WindowEnumDelegate(HWND hwnd, int lParam);
        public static bool WindowEnumProc(HWND hwnd, int lParam)
        {
            // get the text from the window
            string text = User32.GetText(hwnd);

            if (text.Length > 0)
            {
                System.Diagnostics.Trace.WriteLine(text);
            }//end if
            return true;
        }//end WindowEnumProc

    }

    public class ForegroundWindow : IWin32Window
    {
        private static ForegroundWindow _window = new ForegroundWindow();
        private ForegroundWindow() { }

        public static IWin32Window Instance
        {
            get { return _window; }
        }

        IntPtr IWin32Window.Handle
        {
            get
            {
                return User32.GetForegroundWindow();
            }
        }
    }//end class ForegroundWindow

    public class User32
    {
        public const uint SWP_SHOWWINDOW = 0x0001;
        public const uint SW_RESTORE = 0x09;
        public const uint SW_SHOWNOACTIVATE = 0x04;
        
        public const uint SW_MAXIMIZE = 3;
        public const uint SW_MINIMIZE = 6;
        public const uint SW_FORCEMINIMIZE = 11;
        public const uint SW_SHOWMINNOACTIVE = 7;
        public const uint SW_SHOWMINIMIZED = 2;

        public const UInt32 WM_CLOSE = 0x0010;

        //Print Window
        public const uint PW_CLIENTONLY = 1;
        public const uint PW_RENDERFULLCONTENT = 0x00000002;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width { get { return Right - Left; } }
            public int Height { get { return Bottom - Top; } }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 X;
            public Int32 Y;

            public override string ToString()
            {
                return string.Format("X = {0}, Y = {1}", X, Y);
            }
        }

        //http://kenneththorman.blogspot.com/2010/08/c-net-active-windows-size-helper.html
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;

            public WINDOWINFO(Boolean? filler)
             : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            {
                cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
            }
        }

        public enum RDW
        {
            RDW_INVALIDATE = 0x0001,
            RDW_INTERNALPAINT = 0x0002,
            RDW_ERASE = 0x0004,

            RDW_VALIDATE = 0x0008,
            RDW_NOINTERNALPAINT = 0x0010,
            RDW_NOERASE = 0x0020,

            RDW_NOCHILDREN = 0x0040,
            RDW_ALLCHILDREN = 0x0080,

            RDW_UPDATENOW = 0x0100,
            RDW_ERASENOW = 0x0200,

            RDW_FRAME = 0x0400,
            RDW_NOFRAME = 0x0800
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(HWND hwnd, int wMsg, int wParam, StringBuilder lParam);

        [DllImport("User32.dll")]
        public static extern int GetClassName(HWND hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern IntPtr GetClassLong(HWND hwnd, int wMsg);

        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(HWND hWnd);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, out RECT rect);
        
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT rect);
        
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(HWND hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport("user32.dll")]
        public static extern int InvalidateRect(IntPtr hWnd, IntPtr lpRect, int bErase);

        [DllImport("user32.dll")]
        public static extern int UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RDW flags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(System.Drawing.Point pt);

        //	child windows belonging to a parent window contains the specified point. 
        [DllImport("user32.dll")]
        public static extern HWND ChildWindowFromPoint(
            HWND hWndParent,        // handle to parent window
            System.Drawing.Point pt // structure with point coordinates
        );

        //[DllImport("user32.dll")]
        //public static extern IntPtr GetWindowThreadProcessId([In] IntPtr hWnd, [Out] out uint ProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        internal delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        //[DllImport("user32.dll")]
        //public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        internal const uint WINEVENT_OUTOFCONTEXT = 0;
        internal const uint EVENT_SYSTEM_FOREGROUND = 0x0003;
        internal const uint EVENT_SYSTEM_MINIMIZEEND = 0x0017;

        public static void CloseWindow(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        public static void MinimizeWindow(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_MINIMIZE);
        }

        /// <summary>
        /// Gets the window info.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="pwi">The pwi.</param>
        /// <returns></returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        public static WINDOWINFO GetWindowInfo(IntPtr hWnd)
        {
            WINDOWINFO pwi = new WINDOWINFO();
            if (GetWindowInfo(hWnd, ref pwi))
                return pwi;
            return new WINDOWINFO();
        }

        public struct WINDOWPLACEMENT
        {

            [Flags]
            public enum Flags : uint
            {
                WPF_ASYNCWINDOWPLACEMENT = 0x0004,
                WPF_RESTORETOMAXIMIZED = 0x0002,
                WPF_SETMINPOSITION = 0x0001
            }

            /// <summary>
            /// The length of the structure, in bytes. Before calling the GetWindowPlacement or SetWindowPlacement functions, set this member to sizeof(WINDOWPLACEMENT).
            /// </summary>
            public uint length;
            /// <summary>
            /// The flags that control the position of the minimized window and the method by which the window is restored. This member can be one or more of the following values.
            /// </summary>
            /// 
            public Flags flags;//uint flags;
            /// <summary>
            /// The current show state of the window. This member can be one of the following values.
            /// </summary>
            public uint showCmd;
            /// <summary>
            /// The coordinates of the window's upper-left corner when the window is minimized.
            /// </summary>
            public POINT ptMinPosition;
            /// <summary>
            /// The coordinates of the window's upper-left corner when the window is maximized.
            /// </summary>
            public POINT ptMaxPosition;
            /// <summary>
            /// The window's coordinates when the window is in the restored position.
            /// </summary>
            public RECT rcNormalPosition;
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

        public static bool IsMinimized(IntPtr hWnd)
        {
            WINDOWPLACEMENT WinPlacement = new WINDOWPLACEMENT();
            if (!GetWindowPlacement(hWnd, out WinPlacement))
                return true; //window not found

            return (WinPlacement.showCmd == SW_FORCEMINIMIZE
                || WinPlacement.showCmd == SW_SHOWMINIMIZED
                || WinPlacement.showCmd == SW_SHOWMINNOACTIVE
                || WinPlacement.showCmd == SW_MINIMIZE);
        }

        public static void RestoreWindow(IntPtr handle)
        {
            WINDOWPLACEMENT WinPlacement = new WINDOWPLACEMENT();
            GetWindowPlacement(handle, out WinPlacement);
            if (WinPlacement.showCmd != SW_FORCEMINIMIZE 
                && WinPlacement.showCmd != SW_SHOWMINIMIZED
                && WinPlacement.showCmd != SW_SHOWMINNOACTIVE
                && WinPlacement.showCmd != SW_MINIMIZE)
                return;

            if (WinPlacement.flags.HasFlag(WINDOWPLACEMENT.Flags.WPF_RESTORETOMAXIMIZED))
            {
                ShowWindow(handle, SW_MAXIMIZE);
            }
            else
            {
                ShowWindow(handle, (int)SW_SHOWNOACTIVATE);// SW_RESTORE);
            }
        }

        public static uint ForegroundProcessId
        {
            get
            {
                var activeWindowHandle = GetForegroundWindow();
                uint processId;
                GetWindowThreadProcessId(activeWindowHandle, out processId);
                return processId;
            }
        }

        private const int S_OK = 0;
        public enum PROCESS_DPI_AWARENESS
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_AWARE = 2,
            PROCESS_DPI_ERROR = 3
        }
        [DllImport("Shcore.dll", SetLastError = true)]
        private static extern int GetProcessDpiAwareness(IntPtr hprocess, out PROCESS_DPI_AWARENESS value);

        public static PROCESS_DPI_AWARENESS GetProcessDpiAwareness(IntPtr hprocess)
        {
            PROCESS_DPI_AWARENESS pda = PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
            if(GetProcessDpiAwareness(hprocess, out pda) != 0)
            {
                int error = Marshal.GetLastWin32Error();
                var exception = new System.ComponentModel.Win32Exception(error);
                Debug.WriteLine("ERROR: " + error + ": " + exception.Message);
                return PROCESS_DPI_AWARENESS.PROCESS_DPI_ERROR;
            }
            return pda;
        }

        [DllImport("SHCore.dll", SetLastError = true)]
        private static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate bool EnumWindowsProc(IntPtr IntPtr, int lParam);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        public static string GetText(HWND hWnd)
        {
            int length = (int)SendMessage(hWnd, (int)WM_Message.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
            StringBuilder sb = new StringBuilder(length + 1);
            SendMessage(hWnd, (int)WM_Message.WM_GETTEXT, length + 1, sb);
            return sb.ToString();
        }//end GetText

        public static string GetWindowText(IntPtr hWnd)
        {
            int length = User32.GetWindowTextLength(hWnd);
            if (length == 0) 
                return "";

            StringBuilder builder = new StringBuilder(length);
            User32.GetWindowText(hWnd, builder, length + 1);
            
            return builder.ToString();
        }

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr IntPtr);

        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();

        public enum GetAncestorFlags : uint
        {
            GA_PARENT       = 1, // Retrieves the parent window.This does not include the owner, as it does with the GetParent function.
            GA_ROOT         = 2, // Retrieves the root window by walking the chain of parent windows.
            GA_ROOTOWNER    = 3 // Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.
        }

        /// <summary>
        /// Retrieves the handle to the ancestor of the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose ancestor is to be retrieved.
        /// If this parameter is the desktop window, the function returns NULL. </param>
        /// <param name="flags">The ancestor to be retrieved.</param>
        /// <returns>The return value is the handle to the ancestor window.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        #region Screen
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);
        public const int SM_CMONITORS = 80;

        [DllImport("user32.dll")]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref RECT rc, int nUpdate);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX info);

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);

        [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
        public class MONITORINFOEX
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szDevice = new char[32];
            public int dwFlags;
        }
        #endregion

        [Flags()]
        private enum SetWindowPosFlags : uint
        {
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
            /// blocking its execution while other threads process the request.</summary>
            /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
            AsynchronousWindowPosition = 0x4000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            /// <remarks>SWP_DEFERERASE</remarks>
            DeferErase = 0x2000,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            /// <remarks>SWP_DRAWFRAME</remarks>
            DrawFrame = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
            /// is sent only when the window's size is being changed.</summary>
            /// <remarks>SWP_FRAMECHANGED</remarks>
            FrameChanged = 0x0020,
            /// <summary>Hides the window.</summary>
            /// <remarks>SWP_HIDEWINDOW</remarks>
            HideWindow = 0x0080,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
            /// parameter).</summary>
            /// <remarks>SWP_NOACTIVATE</remarks>
            DoNotActivate = 0x0010,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
            /// contents of the client area are saved and copied back into the client area after the window is sized or 
            /// repositioned.</summary>
            /// <remarks>SWP_NOCOPYBITS</remarks>
            DoNotCopyBits = 0x0100,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            /// <remarks>SWP_NOMOVE</remarks>
            IgnoreMove = 0x0002,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            /// <remarks>SWP_NOOWNERZORDER</remarks>
            DoNotChangeOwnerZOrder = 0x0200,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
            /// window uncovered as a result of the window being moved. When this flag is set, the application must 
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            /// <remarks>SWP_NOREDRAW</remarks>
            DoNotRedraw = 0x0008,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            /// <remarks>SWP_NOREPOSITION</remarks>
            DoNotReposition = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            /// <remarks>SWP_NOSENDCHANGING</remarks>
            DoNotSendChangingEvent = 0x0400,
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            /// <remarks>SWP_NOSIZE</remarks>
            IgnoreResize = 0x0001,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            /// <remarks>SWP_NOZORDER</remarks>
            IgnoreZOrder = 0x0004,
            /// <summary>Displays the window.</summary>
            /// <remarks>SWP_SHOWWINDOW</remarks>
            ShowWindow = 0x0040,
        }

        //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga
        public enum WindowLongIndex : int
        {
            GWL_EX_STYLE = -20, //Retrieves the extended window styles.
            GWL_HINSTANCE = -6, //Retrieves a handle to the application instance.
            GWL_HWNDPARENT = -8, //Retrieves a handle to the parent window, if any.
            GWL_ID = -12, //Retrieves the identifier of the window.
            GWL_STYLE = -16, //Retrieves the window styles.
            GWL_USERDATA = -21, //Retrieves the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
            GWL_WNDPROC = -4, //Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern WindowStylesEx GetWindowLong(IntPtr hWnd, WindowLongIndex nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, WindowLongIndex nIndex, WindowStylesEx dwNewLong);

        [Flags]
        public enum WindowStylesEx : uint
        {
            /// <summary>Specifies a window that accepts drag-drop files.</summary>
            WS_EX_ACCEPTFILES = 0x00000010,

            /// <summary>Forces a top-level window onto the taskbar when the window is visible.</summary>
            WS_EX_APPWINDOW = 0x00040000,

            /// <summary>Specifies a window that has a border with a sunken edge.</summary>
            WS_EX_CLIENTEDGE = 0x00000200,

            /// <summary>
            /// Specifies a window that paints all descendants in bottom-to-top painting order using double-buffering.
            /// This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. This style is not supported in Windows 2000.
            /// </summary>
            /// <remarks>
            /// With WS_EX_COMPOSITED set, all descendants of a window get bottom-to-top painting order using double-buffering.
            /// Bottom-to-top painting order allows a descendent window to have translucency (alpha) and transparency (color-key) effects,
            /// but only if the descendent window also has the WS_EX_TRANSPARENT bit set.
            /// Double-buffering allows the window and its descendents to be painted without flicker.
            /// </remarks>
            WS_EX_COMPOSITED = 0x02000000,

            /// <summary>
            /// Specifies a window that includes a question mark in the title bar. When the user clicks the question mark,
            /// the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message.
            /// The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
            /// The Help application displays a pop-up window that typically contains help for the child window.
            /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
            /// </summary>
            WS_EX_CONTEXTHELP = 0x00000400,

            /// <summary>
            /// Specifies a window which contains child windows that should take part in dialog box navigation.
            /// If this style is specified, the dialog manager recurses into children of this window when performing navigation operations
            /// such as handling the TAB key, an arrow key, or a keyboard mnemonic.
            /// </summary>
            WS_EX_CONTROLPARENT = 0x00010000,

            /// <summary>Specifies a window that has a double border.</summary>
            WS_EX_DLGMODALFRAME = 0x00000001,

            /// <summary>
            /// Specifies a window that is a layered window.
            /// This cannot be used for child windows or if the window has a class style of either CS_OWNDC or CS_CLASSDC.
            /// </summary>
            WS_EX_LAYERED = 0x00080000,

            /// <summary>
            /// Specifies a window with the horizontal origin on the right edge. Increasing horizontal values advance to the left.
            /// The shell language must support reading-order alignment for this to take effect.
            /// </summary>
            WS_EX_LAYOUTRTL = 0x00400000,

            /// <summary>Specifies a window that has generic left-aligned properties. This is the default.</summary>
            WS_EX_LEFT = 0x00000000,

            /// <summary>
            /// Specifies a window with the vertical scroll bar (if present) to the left of the client area.
            /// The shell language must support reading-order alignment for this to take effect.
            /// </summary>
            WS_EX_LEFTSCROLLBAR = 0x00004000,

            /// <summary>
            /// Specifies a window that displays text using left-to-right reading-order properties. This is the default.
            /// </summary>
            WS_EX_LTRREADING = 0x00000000,

            /// <summary>
            /// Specifies a multiple-document interface (MDI) child window.
            /// </summary>
            WS_EX_MDICHILD = 0x00000040,

            /// <summary>
            /// Specifies a top-level window created with this style does not become the foreground window when the user clicks it.
            /// The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
            /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
            /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
            /// </summary>
            WS_EX_NOACTIVATE = 0x08000000,

            /// <summary>
            /// Specifies a window which does not pass its window layout to its child windows.
            /// </summary>
            WS_EX_NOINHERITLAYOUT = 0x00100000,

            /// <summary>
            /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
            /// </summary>
            WS_EX_NOPARENTNOTIFY = 0x00000004,

            /// <summary>
            /// The window does not render to a redirection surface.
            /// This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
            /// </summary>
            WS_EX_NOREDIRECTIONBITMAP = 0x00200000,

            /// <summary>Specifies an overlapped window.</summary>
            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

            /// <summary>Specifies a palette window, which is a modeless dialog box that presents an array of commands.</summary>
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,

            /// <summary>
            /// Specifies a window that has generic "right-aligned" properties. This depends on the window class.
            /// The shell language must support reading-order alignment for this to take effect.
            /// Using the WS_EX_RIGHT style has the same effect as using the SS_RIGHT (static), ES_RIGHT (edit), and BS_RIGHT/BS_RIGHTBUTTON (button) control styles.
            /// </summary>
            WS_EX_RIGHT = 0x00001000,

            /// <summary>Specifies a window with the vertical scroll bar (if present) to the right of the client area. This is the default.</summary>
            WS_EX_RIGHTSCROLLBAR = 0x00000000,

            /// <summary>
            /// Specifies a window that displays text using right-to-left reading-order properties.
            /// The shell language must support reading-order alignment for this to take effect.
            /// </summary>
            WS_EX_RTLREADING = 0x00002000,

            /// <summary>Specifies a window with a three-dimensional border style intended to be used for items that do not accept user input.</summary>
            WS_EX_STATICEDGE = 0x00020000,

            /// <summary>
            /// Specifies a window that is intended to be used as a floating toolbar.
            /// A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
            /// A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB.
            /// If a tool window has a system menu, its icon is not displayed on the title bar.
            /// However, you can display the system menu by right-clicking or by typing ALT+SPACE.
            /// </summary>
            WS_EX_TOOLWINDOW = 0x00000080,

            /// <summary>
            /// Specifies a window that should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
            /// To add or remove this style, use the SetWindowPos function.
            /// </summary>
            WS_EX_TOPMOST = 0x00000008,

            /// <summary>
            /// Specifies a window that should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
            /// The window appears transparent because the bits of underlying sibling windows have already been painted.
            /// To achieve transparency without these restrictions, use the SetWindowRgn function.
            /// </summary>
            WS_EX_TRANSPARENT = 0x00000020,

            /// <summary>Specifies a window that has a border with a raised edge.</summary>
            WS_EX_WINDOWEDGE = 0x00000100
        }
    }
    
    public class EnumOpenWindows
    {
        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
        public static List<WindowInfo> GetOpenWindows(bool includeInvisible = false)
        {
            IntPtr shellWindow = User32.GetShellWindow();
            List<WindowInfo> windows = new List<WindowInfo>();

            User32.EnumWindows(delegate (IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow)
                    return true;

                if (!includeInvisible && !User32.IsWindowVisible(hWnd))
                    return true;

                string title = User32.GetWindowText(hWnd);
                if (string.IsNullOrWhiteSpace(title))
                    return true;

                User32.WindowStylesEx ex = User32.GetWindowLong(hWnd, User32.WindowLongIndex.GWL_EX_STYLE);
                if (ex.HasFlag(User32.WindowStylesEx.WS_EX_TOOLWINDOW) || ex.HasFlag(User32.WindowStylesEx.WS_EX_NOREDIRECTIONBITMAP))
                    return true;

                windows.Add(new WindowInfo(hWnd, title));
                return true;
            }, 0);

            return windows;
        }

        public static bool IsAltTabWindow(IntPtr hWnd)
        {
            User32.WindowStylesEx ex = User32.GetWindowLong(hWnd, User32.WindowLongIndex.GWL_EX_STYLE);
            if (ex.HasFlag(User32.WindowStylesEx.WS_EX_TOOLWINDOW))
                return false;

            if (!User32.IsWindowVisible(hWnd))
                return true;

            // Start at the root owner
            IntPtr hWndWalk = User32.GetAncestor(hWnd, User32.GetAncestorFlags.GA_ROOTOWNER);

            // See if we are the last active visible popup
            IntPtr hWndTry;
            while ((hWndTry = User32.GetLastActivePopup(hWndWalk)) != hWndTry)
            {
                if (User32.IsWindowVisible(hWndTry))
                    break;
                hWndWalk = hWndTry;
            }

            return hWndWalk == hWnd;
        }
    }
}
