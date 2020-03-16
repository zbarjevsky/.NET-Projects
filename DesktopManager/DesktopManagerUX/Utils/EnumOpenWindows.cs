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
            IntPtr shellWindow = User32.GetShellWindow();
            List<AppInfo> windows = new List<AppInfo>();

            User32.EnumWindows(delegate (IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow) 
                    return true;
                if (!User32.IsWindowVisible(hWnd)) 
                    return true;

                string title = User32.GetWindowText(hWnd);
                if (string.IsNullOrWhiteSpace(title))
                    return true;

                windows.Add(new AppInfo(hWnd));
                return true;

            }, 0);

            return windows;
        }
    }
}
