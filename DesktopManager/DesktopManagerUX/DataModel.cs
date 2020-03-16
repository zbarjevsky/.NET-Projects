using DesktopManagerUX.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace DesktopManagerUX
{
    public class DataModel
    {
    }

    public enum GridSizes : int
    {
        Grid_1x1 = 1,
        Grid_1x2 = 2,
        Grid_2x1 = 2,
        Grid_2x2 = 4,
        Grid_2x3 = 6,
        Grid_3x2 = 6,
        Grid_3x3 = 9
    }

    [Serializable]
    public class AppInfo
    {
        public AppInfo(Process p)
        {
            HWND = p.MainWindowHandle;
            ProcessName = p.ProcessName;
            ProcessPath = p.MainModule.FileName;
            Title = "[" + ProcessName + "] - " + p.MainWindowTitle;

            System.Drawing.Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(ProcessPath);
            Icon = Logic.GetImageSourceFromIcon(ico);
        }

        public AppInfo(string title, IntPtr hWnd)
        {
            Process p = GetProcessForWindow(hWnd);

            HWND = hWnd;
            ProcessName = p.ProcessName;
            ProcessPath = p.MainModule.FileName;
            Title = "[" + ProcessName + "] - " + title;

            System.Drawing.Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(ProcessPath);
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

        public int Row { get; set; }
        public int Col { get; set; }

        [XmlIgnore]
        public IntPtr HWND { get; private set; }
        [XmlAttribute]
        public string Title { get; set; }
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
        public Color Color { get; set; } = Colors.AliceBlue;
        public BitmapSource Icon { get; }

        public static int FindApp(string title, ObservableCollection<AppInfo> apps)
        {
            int pos = title.IndexOf("[");
            int end = title.IndexOf("]");
            if (pos < 0 || end < 0)
                return 0;

            string processInTitle = title.Substring(pos + 1, end - pos - 1);

            //search first by title
            for (int i = 1; i < apps.Count; i++)
            {
                if (title == apps[i].Title)
                    return i;
            }

            //then by process name
            for (int i = 1; i < apps.Count; i++)
            {
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

        public override string ToString()
        {
            return Title;
        }
    }

    public class DisplayInfo
    {
        [XmlAttribute]
        public int Index { get; set; }

        [XmlIgnore]
        public string Name { get { return Index + " - " + Bounds.Width + "x" + Bounds.Height + (screen.IsPrimary ? " (Primary)" : ""); } }
        [XmlIgnore]
        public System.Windows.Rect Bounds { get { return screen.WorkingArea; } set { } }

        public Color Color { get; set; } = Colors.Yellow;

        [XmlIgnore]
        public SolidColorBrush Brush { get { return new SolidColorBrush(this.Color); } }

        [XmlIgnore]
        private WpfScreen screen;

        public DisplayInfo() 
        {
            Index = -1;
            this.screen = null;
        }

        public DisplayInfo(WpfScreen screen, int index)
        {
            Index = index;
            this.screen = screen;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class GridSizeData
    {
        [XmlAttribute]
        public int Rows { get; set; }
        [XmlAttribute]
        public int Cols { get; set; }

        public GridSizeData()
        {
            Rows = 2;
            Cols = 2;
        }

        public GridSizeData(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
        }

        [XmlIgnore]
        public int CellCount { get { return Rows * Cols; } }

        public int Pos(int row, int col)
        {
            return row * this.Cols + col;
        }

        public static GridSizeData Parse(string txtGridSize)
        {
            int rows = int.Parse(txtGridSize.Substring(5, 1));
            int cols = int.Parse(txtGridSize.Substring(7, 1));
            return new GridSizeData(rows, cols);
        }

        public override string ToString()
        {
            return Rows + "x" + Cols;
        }

        public static ObservableCollection<string> GetAllSizes()
        {
            return new ObservableCollection<string>(Enum.GetNames(typeof(GridSizes)));
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
