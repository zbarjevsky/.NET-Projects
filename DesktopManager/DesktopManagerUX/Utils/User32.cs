using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopManagerUX.Utils
{
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
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 x;
            public Int32 y;
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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT rect);
        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        public static void CloseWindow(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
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

        /// <summary>
        /// Gets the window text.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="text">The text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

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

        public static RECT RestoreWindow(IntPtr handle)
        {
            WINDOWPLACEMENT WinPlacement = new WINDOWPLACEMENT();
            GetWindowPlacement(handle, out WinPlacement);
            if (WinPlacement.showCmd != SW_FORCEMINIMIZE 
                && WinPlacement.showCmd != SW_SHOWMINIMIZED
                && WinPlacement.showCmd != SW_SHOWMINNOACTIVE
                && WinPlacement.showCmd != SW_MINIMIZE)
                return WinPlacement.rcNormalPosition;

            if (WinPlacement.flags.HasFlag(WINDOWPLACEMENT.Flags.WPF_RESTORETOMAXIMIZED))
            {
                ShowWindow(handle, SW_MAXIMIZE);
            }
            else
            {
                ShowWindow(handle, (int)SW_SHOWNOACTIVATE);// SW_RESTORE);
            }
            return WinPlacement.rcNormalPosition;
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
    }
}
