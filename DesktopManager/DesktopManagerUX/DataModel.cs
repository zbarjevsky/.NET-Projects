using DesktopManagerUX.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DesktopManagerUX
{
    public class DataModel
    {
    }

    public class AppInfo
    {
        private Process p;

        public AppInfo(Process p)
        {
            this.p = p;
            Name = p.MainWindowTitle + " (" + p.ProcessName + ")";
        }

        public AppInfo()
        {
            this.p = null;
            Name = "Select Application";
        }

        public static AppInfo GetEmptyAppInfo() { return new AppInfo(); }

        public Process Process { get { return p; } private set { p = value; } }
        public string Name { get; private set;}
        public SolidColorBrush Color { get; set; } = Brushes.AliceBlue;
    }

    public class DisplayInfo
    {
        public string Name { get { return screen.DeviceName + " - " + Bounds.ToString(); } }
        public System.Windows.Rect Bounds { get { return screen.WorkingArea; } }

        public SolidColorBrush Color { get; } = Brushes.Yellow;

        private WpfScreen screen;

        public DisplayInfo(WpfScreen screen)
        {
            this.screen = screen;
        }
    }
}
