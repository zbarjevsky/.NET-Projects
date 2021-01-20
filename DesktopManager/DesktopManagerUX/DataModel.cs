﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;


using DesktopManagerUX.Utils;
using MkZ.Tools;
using MkZ.WPF.Utils;
using System;

namespace DesktopManagerUX
{
    public class DataModel
    {
    }

    public enum eGridSizes : int
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
    public class AppInfo : INotifyPropertyChanged
    {
        public AppInfo(Process p)
        {
            Init(p, p.MainWindowHandle);
        }

        public AppInfo(WindowInfo wnd)
        {
            Process p = GetProcessForWindow(wnd.hWnd);
            Init(p, wnd.hWnd);
        }

        public AppInfo()
        {
            HWND = IntPtr.Zero;
            Process = null;
            Title = "? Select Window ?";
            ProcessName = "";
            Icon = null;
        }

        public static AppInfo GetEmptyAppInfo() { return new AppInfo(); }

        public int Row { get; set; }
        public int Col { get; set; }

        [XmlIgnore]
        public IntPtr HWND { get; private set; }
        [XmlIgnore]
        public Process Process { get; private set; }

        [XmlAttribute]
        public string Title { get; set; }
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
        public Color Color { get; set; } = Colors.AliceBlue;

        [XmlIgnore]
        public BitmapSource Icon { get; private set; }
        [XmlIgnore]
        public bool IsActive { get { return Process != null && !Process.HasExited && HWND != IntPtr.Zero; } }

        public static int FindApp(string title, SmartObservableCollection<AppInfo> apps)
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

        public void Refresh()
        {
            Init(Process, HWND);
        }

        private void Init(Process p, IntPtr hWnd)
        {
            Process = p;
            if(Process != null && !Process.HasExited) //process is alive
            {
                ProcessName = Process.ProcessName;
                ProcessPath = Process.MainModule.FileName;

                string title = User32.GetWindowText(hWnd);
                if (!string.IsNullOrEmpty(title))
                {
                    HWND = hWnd;
                    Title = "[" + ProcessName + "] - " + title;
                }
            
                OnPropertyChanged(nameof(Title));
            }

            
            if (Icon == null && File.Exists(ProcessPath))
            {
                System.Drawing.Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(ProcessPath);
                Icon = Logic.GetImageSourceFromIcon(ico);
                OnPropertyChanged(nameof(Icon));
            }
        }

        private static Process GetProcessForWindow(IntPtr hWnd)
        {
            uint pid;
            User32.GetWindowThreadProcessId(hWnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p;
        }

        public override bool Equals(object obj)
        {
            AppInfo app = obj as AppInfo;
            if (app == null)
                return false;

            if(!string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(app.Title))
                return app.Title == Title;

            return app.ProcessName == ProcessName;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        public override string ToString()
        {
            return Title;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DisplayInfo
    {
        [XmlAttribute]
        public int Index { get; set; }

        [XmlIgnore]
        public string Name { get { return "Display " +(Index+1) + " - " + Bounds.Width + "x" + Bounds.Height + (screen.IsPrimary ? " (Primary)" : ""); } }

        [XmlIgnore]
        public System.Windows.Rect Bounds { get { return screen.WorkingArea; } set { } }

        public Color Color { get; set; } = Colors.Yellow;

        [XmlIgnore]
        public SolidColorBrush Brush { get { return new SolidColorBrush(this.Color); } }

        public bool IsPrimary { get { return screen != null ? screen.IsPrimary : false; } }

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

        public void Update(SmartObservableCollection<DisplayInfo> displays)
        {
            if (Index < 0 || Index >= displays.Count)
                Index = 0;

            this.screen = displays[Index].screen;
        }

        public override bool Equals(object obj)
        {
            if (obj is DisplayInfo info)
            {
                return info.Name == Name && info.Index == Index;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class GridSizeData
    {
        [XmlAttribute]
        private int _rows = 2;
        public int Rows { get { return _rows; } set { _rows = value; ResetRowsHeight(); } }

        [XmlAttribute]
        private int _cols = 2;
        public int Cols { get { return _cols; } set { _cols = value; ResetColsWidth(); } }

        public double[] RelativeColumnsWidths { get; set; }
        public double[] RelativeRowsHeghts { get; set; }

        [XmlIgnore]
        public int CellCount { get { return Rows * Cols; } }

        public GridSizeData() : this(2, 2)
        {
        }

        public GridSizeData(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
        }

        public int Pos(int row, int col)
        {
            return row * this.Cols + col;
        }

        private void ResetRowsHeight()
        {
            RelativeRowsHeghts = new double[Rows];
            for (int row = 0; row < Rows; row++)
                RelativeRowsHeghts[row] = 1.0 / Rows;
        }

        private void ResetColsWidth()
        {
            RelativeColumnsWidths = new double[Cols];
            for (int col = 0; col < Cols; col++)
                RelativeColumnsWidths[col] = 1.0 / Cols;
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

        public static ObservableCollection<string> GetAllSizesEnumNames()
        {
            return new ObservableCollection<string>(Enum.GetNames(typeof(eGridSizes)));
        }

        public void UpdateCellSizes(Grid gridApps)
        {
            double totalWidth = 0;
            for (int col = 0; col < gridApps.ColumnDefinitions.Count; col++)
            {
                double width = gridApps.ColumnDefinitions[col].ActualWidth;
                if ((col % 2) == 0) //apps col
                {
                    totalWidth += width;
                    RelativeColumnsWidths[col / 2] = width;
                }
            }
            for (int i = 0; i < Cols; i++)
                RelativeColumnsWidths[i] /= totalWidth;

            double totalHeight = 0;
            for (int row = 0; row < gridApps.RowDefinitions.Count; row++)
            {
                double height = gridApps.RowDefinitions[row].ActualHeight;
                if ((row % 2) == 0) //apps row
                {
                    totalHeight += height;
                    RelativeRowsHeghts[row / 2] = height;
                }
            }
            for (int i = 0; i < Rows; i++)
                RelativeRowsHeghts[i] /= totalHeight;
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
