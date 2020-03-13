using DesktopManagerUX.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopManagerUX
{
    public class DataModel
    {
    }

    public class AppInfo
    {
        public AppInfo(Process p)
        {
            HWND = p.MainWindowHandle;
            ProcessName = p.ProcessName;
            Title = "[" + ProcessName + "] - " + p.MainWindowTitle;

            System.Drawing.Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(p.MainModule.FileName);
            Icon = Logic.GetImageSourceFromIcon(ico);
        }

        public AppInfo(string title, IntPtr hWnd)
        {
            Process p = GetProcessForWindow(hWnd);

            HWND = hWnd;
            ProcessName = p.ProcessName;
            Title = "[" + ProcessName + "] - " + title;

            System.Drawing.Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(p.MainModule.FileName);
            Icon = Logic.GetImageSourceFromIcon(ico);
        }

        public AppInfo()
        {
            HWND = IntPtr.Zero;
            Title = "? Select Window ?";
            ProcessName = "";
            Icon = null;
        }

        public static AppInfo GetEmptyAppInfo() { return new AppInfo(); }

        public IntPtr HWND { get; private set; }
        public string Title { get; private set; }
        public string ProcessName { get; private set; }
        public SolidColorBrush Color { get; set; } = Brushes.AliceBlue;
        public BitmapSource Icon { get; }

        public static int FindApp(string title, List<AppInfo> apps)
        {
            int pos = title.IndexOf("[");
            int end = title.IndexOf("]");
            if (pos < 0 || end < 0)
                return 0;

            string processInTitle = title.Substring(pos + 1, end - pos - 1);

            for (int i = 1; i < apps.Count; i++)
            {
                if (title == apps[i].Title)
                    return i;

                string processName = apps[i].ProcessName;
                if (processInTitle == processName)
                    return i;
            }
            return 0;
        }

        private Process GetProcessForWindow(IntPtr hWnd)
        {
            uint pid;
            User32.GetWindowThreadProcessId(hWnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p;
        }
    }

    public class DisplayInfo
    {
        public int Index { get; }
        public string Name { get { return Index + " - " + Bounds.Width + "x" + Bounds.Height + (screen.IsPrimary?" (Primary)":""); } }
        public System.Windows.Rect Bounds { get { return screen.WorkingArea; } }

        public SolidColorBrush Color { get; } = Brushes.Yellow;

        private WpfScreen screen;

        public DisplayInfo(WpfScreen screen, int index)
        {
            Index = index;
            this.screen = screen;
        }
    }

    public class AppChooserData
    {
        public AppChooserUserControl AppChooser;
        public string AppTitle;
    }

    public class AppChoosersGrid
    {
        private AppChooserData[,] AppChoosers;

        public AppChoosersGrid(int rows, int cols)
        {
            AppChoosers = new AppChooserData[rows, cols];
        }

        public AppChooserData this[int row, int col]
        {
            get { return AppChoosers[row, col]; }
            set { AppChoosers[row, col] = value; }
        }

    }
}
