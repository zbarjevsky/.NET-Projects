using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MkZ.Windows.Win32API;

namespace MkZ.Tools
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
                MessageBox.Show(wnd.Win32Window, 
                    "Window: '" + wnd.Title + "'\nAlready Opened! \n\nShowing Running Instance...", 
                    wnd.Title, 
                    MessageBoxButtons.OK, MessageBoxIcon.Information, 
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }

            return wnd != null;
        }

        public static WindowInfo FindWindowContains(string partOfTitle, bool includeInvisible = false)
        {
            List<WindowInfo> windows = EnumOpenWindows.GetOpenWindows(includeInvisible);
            WindowInfo window = windows.FirstOrDefault(app => app.Title.Contains(partOfTitle));
            return window;
        }

        public static bool IsSingleInstance(bool showOtherInstance)
        {
            Process thisProcess = Process.GetCurrentProcess();
            var processes = Process.GetProcesses().Where(p =>
                p.ProcessName == thisProcess.ProcessName && p.Id != thisProcess.Id).ToList();

            bool single = processes.Count == 0;
            if (!single && showOtherInstance)
            {
                Process otherProcess = processes[0];
                if (otherProcess != null)
                {
                    User32.ShowWindow(otherProcess.MainWindowHandle, User32.SW_SHOW);
                    User32.SetForegroundWindow(otherProcess.MainWindowHandle);
                }
            }

            return single;
        }
    }
}
