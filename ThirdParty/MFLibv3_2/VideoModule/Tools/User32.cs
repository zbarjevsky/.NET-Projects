using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoModule.Tools
{
    public static class User32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public int width { get { return right - left; } }
            public int height { get { return bottom - top; } }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        public static Rect GetClientRect(IntPtr hWnd)
        {
            GetClientRect(hWnd, out RECT r);
            return new Rect(r.left, r.top, r.width, r.height);
        }
    }
}
