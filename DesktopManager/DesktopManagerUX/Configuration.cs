using DesktopManagerUX.Utils;
using MZ.Tools;
using MZ.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using System.Windows.Desktop;

namespace DesktopManagerUX
{
    [Serializable]
    public class Configuration
    {
        public ObservableCollection<LayoutConfiguration> Layouts { get; set; } = new ObservableCollection<LayoutConfiguration>();

        //current windows displays
        [XmlIgnore]
        public SmartObservableCollection<DisplayInfo> Displays { get; set; }

        public Configuration()
        {
            Displays = new SmartObservableCollection<DisplayInfo>(Logic.GetDisplays());
        }

        public static string ConfigurationFileName
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path = Path.Combine(path, "DesktopManager");
                Directory.CreateDirectory(path);
                string fileName = Path.Combine(path, "Config.xml");
                return fileName;
            }
        }

        public void Save()
        {
            SerializerHelper.Save<Configuration>(ConfigurationFileName, this);
        }

        public static Configuration Load()
        {
            Configuration cnf = SerializerHelper.Open<Configuration>(ConfigurationFileName);
            if (cnf.Layouts.Count == 0) //add one default layout tab
                cnf.Layouts.Add(new LayoutConfiguration(init: true));

            cnf.SmartDisplaysUpdate();
            return cnf;
        }

        public void SmartDisplaysUpdate()
        {
            List<DisplayInfo> info = Logic.GetDisplays();

            Displays.Update(info);

            Debug.Assert(info.Count == Displays.Count);

            for (int i = 0; i < Layouts.Count; i++)
                Layouts[i].NotifyDisplaysChanged(Displays);
        }
    }

    [Serializable]
    public class LayoutConfiguration : NotifyPropertyChangedImpl
    {
        private string _name = "";
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        [XmlIgnore]
        public string DisplayName 
        { 
            get 
            { 
                if (SelectedMonitorInfo == null) 
                    return ""; 
                return "Monitor [" + (SelectedMonitorInfo.Index+1) + (SelectedMonitorInfo.IsPrimary ? "] (Primary)" : "]");
            } 
        }

        private DisplayInfo _displayInfo;
        public DisplayInfo SelectedMonitorInfo { get { return _displayInfo; } set { _displayInfo = value; OnPropertyChanged(nameof(DisplayName)); } }

        private GridSizeData _gridSize = new GridSizeData() { Rows = 2, Cols = 2 };
        public GridSizeData GridSize { get { return _gridSize; } set { _gridSize = value; } }

        public Rect GetCellBounds(int row, int col)
        {
            double totalWidth = SelectedMonitorInfo.Bounds.Width;
            double totalHeight = SelectedMonitorInfo.Bounds.Height;

            double width = (totalWidth * _gridSize.RelativeColumnsWidths[col]);
            double height = (totalHeight * _gridSize.RelativeRowsHeghts[row]);

            double left = SelectedMonitorInfo.Bounds.Left;
            for (int i = 0; i < col; i++) //width of all previous columns
                left += totalWidth * _gridSize.RelativeColumnsWidths[i];

            double top = SelectedMonitorInfo.Bounds.Top;
            for (int i = 0; i < row; i++) //width of all previous rows
                top += totalHeight * _gridSize.RelativeRowsHeghts[i];

            return new Rect(left, top, width, height);
        }

        public Rect GetCorrectedCellBounds(int row, int col, IntPtr hWnd)
        {
            User32.RECT border = DesktopWindowManager.GetWindowBorderSize(hWnd);

            Rect bounds = GetCellBounds(row, col);

            bounds.Offset(-border.left, -border.top);
            bounds.Width += border.left + border.right;
            bounds.Height += border.top + border.bottom;

            return bounds;
        }

        //should be before SelectedGridSize - to avoid doubling when loading from XML
        public List<AppInfo> SelectedApps { get; set; }

        //empty constructor for serialization
        public LayoutConfiguration()
        {
        }

        public LayoutConfiguration(bool init, string name = "Layout 1")
        {
            if (init) Initialize(name);
        }

        public void Initialize(string name)
        {
            _name = name;

            SelectedApps = new List<AppInfo>(GridSize.CellCount);
            for (int i = 0; i < GridSize.CellCount; i++)
                SelectedApps.Add(AppInfo.GetEmptyAppInfo());
        }

        public string GetSelectedGridSizeText()
        {
            return GridSize.ToString();
        }

        public void SetSelectedgridSizeText(string txtGridSize)
        {
            UpdateApps(GridSizeData.Parse(txtGridSize));
        }

        public void UpdateApps(GridSizeData newGridSize)
        {
            if (SelectedApps.Count == _gridSize.CellCount)
            {
                if (newGridSize.Rows != _gridSize.Rows || newGridSize.Cols != _gridSize.Cols)
                {
                    //init
                    List<AppInfo> list = new List<AppInfo>();
                    for (int i = 0; i < newGridSize.CellCount; i++)
                        list.Add(AppInfo.GetEmptyAppInfo());

                    //smart copy
                    for (int row = 0; row < _gridSize.Rows; row++) //copy maximum possible settings
                    {
                        for (int col = 0; col < _gridSize.Cols; col++)
                        {
                            if (row < newGridSize.Rows && col < newGridSize.Cols)
                            {
                                int index = newGridSize.Pos(row, col);
                                list[index] = this[row, col];
                            }
                        }
                    }
                    SelectedApps = list;
                }
            }
            else if(SelectedApps.Count > 0)
            {
                List<AppInfo> list = new List<AppInfo>();
                for (int i = 0; i < newGridSize.CellCount; i++)
                    list.Add(AppInfo.GetEmptyAppInfo());

                //smart copy
                for (int row = 0; row < _gridSize.Rows; row++) //copy maximum possible settings
                {
                    for (int col = 0; col < _gridSize.Cols; col++)
                    {
                        if (row < newGridSize.Rows && col < newGridSize.Cols)
                        {
                            int index = newGridSize.Pos(row, col);
                            if(index < SelectedApps.Count)
                                list[index] = SelectedApps[index];
                        }
                    }
                }
                SelectedApps = list;
            }

            _gridSize = newGridSize;
        }

        public void NotifyDisplaysChanged(SmartObservableCollection<DisplayInfo> displays)
        {
            if (SelectedMonitorInfo == null)
                SelectedMonitorInfo = displays[0];

            SelectedMonitorInfo.Update(displays);

            if (string.IsNullOrWhiteSpace(_name))
                _name = "Conf 1";

            if (SelectedApps.Count == 0)
                Initialize(_name);
            else if (SelectedApps.Count != GridSize.CellCount)
                UpdateApps(GridSize);

            OnPropertyChanged(nameof(Name));
        }

        public AppInfo this[int row, int col]
        {
            get { return SelectedApps[GridSize.Pos(row, col)]; }
            set
            {
                AppInfo info = value;
                if (info == null)
                    info = AppInfo.GetEmptyAppInfo();

                int pos = GridSize.Pos(row, col);
                SelectedApps[pos] = info;
                SelectedApps[pos].Row = row;
                SelectedApps[pos].Col = col;
            }
        }

        public override string ToString()
        {
            return Name + " - " + GridSize;
        }
    }
}
