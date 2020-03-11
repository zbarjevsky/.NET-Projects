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
        private Process p;
        
        public AppInfo(Process p)
        {
            this.p = p;
            Name = p.MainWindowTitle + " (" + p.ProcessName + ")";

            System.Drawing.Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(p.MainModule.FileName);
            Icon = Logic.GetImageSourceFromIcon(ico);
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
        public BitmapSource Icon { get; }
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
