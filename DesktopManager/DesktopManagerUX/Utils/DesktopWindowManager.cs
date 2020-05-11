using MZ.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopManagerUX.Utils
{
    //https://docs.microsoft.com/en-us/windows/win32/dwm/dwm-overview
    public class DesktopWindowManager
    {
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out User32.RECT pvAttribute, int cbAttribute);

        //public struct RECT
        //{
        //    public int Left;
        //    public int Top;
        //    public int Right;
        //    public int Bottom;
        //}

        [Flags]
        private enum DwmWindowAttribute : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
        }

        public static User32.RECT GetWindowRectangle(IntPtr hWnd)
        {
            User32.RECT rect;

            int size = Marshal.SizeOf(typeof(User32.RECT));
            DwmGetWindowAttribute(hWnd, (int)DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS, out rect, size);

            return rect;
        }

        //https://stackoverflow.com/questions/34139450/getwindowrect-returns-a-size-including-invisible-borders
        public static User32.RECT GetWindowBorderSize(IntPtr hWnd)
        {
            User32.RECT rect, frame;
            User32.GetWindowRect(hWnd, out rect);

            int size = Marshal.SizeOf(typeof(User32.RECT));
            DwmGetWindowAttribute(hWnd, (int)DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS, out frame, size);

            //rect should be `0, 0, 1280, 1024`
            //frame should be `7, 0, 1273, 1017`

            User32.RECT border;
            border.left = frame.left - rect.left;
            border.top = frame.top - rect.top;
            border.right = rect.right - frame.right;
            border.bottom = rect.bottom - frame.bottom;

            //border should be `7, 0, 7, 7`
            return border;
        }
    }
}
