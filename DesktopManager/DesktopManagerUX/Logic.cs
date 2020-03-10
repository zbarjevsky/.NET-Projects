using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopManagerUX
{
    public class Logic
    {
        [DllImport("User32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public static List<AppInfo> ListTasks()
        {
            Process[] processes = Process.GetProcesses();
            List<Process> pp = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).ToList();

            List<AppInfo> apps = new List<AppInfo>();
            foreach (Process p in pp)
                apps.Add(new AppInfo(p));

            return apps;
        }

        public static void MoveWindow(Process p, double X, double Y, double nWidth, double nHeight)
        {
            if (p != null && !p.HasExited)
            {
                const uint SWP_SHOWWINDOW = 0x0001;
                ShowWindow(p.MainWindowHandle, SWP_SHOWWINDOW);
                SetForegroundWindow(p.MainWindowHandle);
                MoveWindow(p.MainWindowHandle, (int)X, (int)Y, (int)nWidth, (int)nHeight, false);
            }
        }
    }
}
