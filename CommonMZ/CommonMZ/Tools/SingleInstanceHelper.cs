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
        public static bool GlobalShowWindow(string title_sub_string, bool includeInvisible = false)
        {
            WindowInfo wnd = FindWindowContains(title_sub_string, includeInvisible);
            if (wnd != null)
            {
                User32.ShowWindow(wnd.hWnd, User32.SW_RESTORE);
                User32.SetForegroundWindow(wnd.hWnd);
                MessageBox.Show(wnd.Win32Window, "Window: '" + wnd.Title + "'\nAlready Opened! \n\nShowing Running Instance...", wnd.Title, 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return wnd != null;
        }

        public static WindowInfo FindWindowContains(string partOfTitle, bool includeInvisible = false)
        {
            List<WindowInfo> windows = EnumOpenWindows.GetOpenWindows(includeInvisible);
            WindowInfo window = windows.FirstOrDefault(app => app.Title.Contains(partOfTitle));
            return window;
        }
    }
}
