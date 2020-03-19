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

                User32.WindowStylesEx ex = User32.GetWindowLong(hWnd, User32.WindowLongIndex.GWL_EX_STYLE);
                if (ex.HasFlag(User32.WindowStylesEx.WS_EX_TOOLWINDOW) || ex.HasFlag(User32.WindowStylesEx.WS_EX_NOREDIRECTIONBITMAP))
                    return true;

                windows.Add(new AppInfo(hWnd));
                return true;

            }, 0);

            return windows;
        }

        public static bool IsAltTabWindow(IntPtr hWnd)
        {
            User32.WindowStylesEx ex = User32.GetWindowLong(hWnd, User32.WindowLongIndex.GWL_EX_STYLE);
            if (ex.HasFlag(User32.WindowStylesEx.WS_EX_TOOLWINDOW))
                return false;

            if (!User32.IsWindowVisible(hWnd))
                return true;

            // Start at the root owner
            IntPtr hWndWalk = User32.GetAncestor(hWnd, User32.GetAncestorFlags.GA_ROOTOWNER);
            
            // See if we are the last active visible popup
            IntPtr hWndTry;
            while ((hWndTry = User32.GetLastActivePopup(hWndWalk)) != hWndTry)
            {
                if (User32.IsWindowVisible(hWndTry)) 
                    break;
                hWndWalk = hWndTry;
            }

            return hWndWalk == hWnd;
        }
    }
}
