using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopManagerUX.Utils
{
    public class EnumOpenWindows
    {
        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
        public static List<AppInfo> GetOpenWindows()
        {
            IntPtr shellWindow = GetShellWindow();
            List<AppInfo> windows = new List<AppInfo>();

            EnumWindows(delegate (IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows.Add(new AppInfo(builder.ToString(), hWnd));
                return true;

            }, 0);

            return windows;
        }

        private delegate bool EnumWindowsProc(IntPtr IntPtr, int lParam);

        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(IntPtr IntPtr, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr IntPtr);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr IntPtr);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();
    }
}
