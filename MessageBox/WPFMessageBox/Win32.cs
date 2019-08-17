using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MZ.WPF.MessageBox
{
    public static class Win32
    {
        public static System.Windows.Rect GetWindowRect(IntPtr hWnd)
        {
            RECT r;
            if (GetWindowRect(hWnd, out r))
                return new System.Windows.Rect(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
            return new System.Windows.Rect();
        } 

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
    }
}
