using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardManager
{
    class SingleInstanceHelper
    {
        const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int flags);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        public static bool GlobalShowWindow(string title)
        {
            IntPtr wnd = FindWindow(null, title);
            if (wnd != IntPtr.Zero)
            {
                ShowWindow(wnd, SW_RESTORE);
                SetForegroundWindow(wnd);
            }

            return wnd != IntPtr.Zero;
        }
    }
}
