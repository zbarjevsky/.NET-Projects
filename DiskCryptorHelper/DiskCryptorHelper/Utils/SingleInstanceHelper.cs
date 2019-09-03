using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace DiskCryptorHelper
{
	/// <summary>
	/// Summary description for SingleApp.
	/// </summary>
	public class SingleInstanceHelper
	{
        const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int flags);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(0);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;

        public static bool GlobalShowWindow(string title)
	    {
	        IntPtr wnd = FindWindow(null, title);
	        if (wnd != IntPtr.Zero)
	        {
                SetForegroundWindow(wnd);
                SetWindowPos(wnd, HWND_TOP, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
                ShowWindow(wnd, SW_RESTORE);
            }

            return wnd != IntPtr.Zero;
	    }
	}
}
