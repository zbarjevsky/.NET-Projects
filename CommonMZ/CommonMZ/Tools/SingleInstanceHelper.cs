using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Tools
{
    public class SingleInstanceHelper
    {
        public static bool GlobalShowWindow(string title, bool includeInvisible = false)
        {
            WindowInfo wnd = FindWindowContains(title, includeInvisible);
            if (wnd != null)
            {
                User32.ShowWindow(wnd.hWnd, User32.SW_RESTORE);
                User32.SetForegroundWindow(wnd.hWnd);
                MessageBox.Show(wnd.Win32Window, "Window: '" + title + "'\nAlready Opened! \n\nShowing Running Instance...", title, 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return wnd != null;
        }

        public static WindowInfo FindWindowContains(string partOfTitle, bool includeInvisible = false)
        {
            List<WindowInfo> windows = GetOpenWindows(includeInvisible);
            WindowInfo window = windows.FirstOrDefault(app => app.Title.Contains(partOfTitle));
            return window;
        }

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
    }
}
