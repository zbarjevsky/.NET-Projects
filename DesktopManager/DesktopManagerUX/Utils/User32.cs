using System;
using System.Collections.Generic;
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
            PROCESS_PER_MONITOR_DPI_AWARE = 2
        }
        [DllImport("Shcore.dll")]
        public static extern int GetProcessDpiAwareness(IntPtr hprocess, out PROCESS_DPI_AWARENESS value);
    }
}
