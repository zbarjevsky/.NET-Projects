using DesktopManagerUX.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DesktopManagerUX
{
    [Serializable]
    public class Configuration
    {
        public DisplayInfo SelectedDisplayInfo { get; set; }

        private GridSizeData _gridSize = new GridSizeData() { Rows=2, Cols=2 };
        public GridSizeData GridSize { get { return _gridSize; } set { _gridSize = value; } }

        //should be before SelectedGridSize - to avoid doubling when loading from XML
        public List<AppInfo> SelectedApps { get; set; }

        public string GetSelectedGridSizeText()
        {
            return GridSize.ToString();
        }

        public void SetSelectedgridSizeText(string txtGridSize)
        {
            UpdateGridSize(GridSizeData.Parse(txtGridSize), true);
        }

        private void UpdateGridSize(GridSizeData newGridSize, bool updateApps)
        {
            if (updateApps)
            {
                if (newGridSize.Rows != _gridSize.Rows || newGridSize.Cols != _gridSize.Cols)
                {
                    if (SelectedApps.Count > 0)
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
                                if(row< newGridSize.Rows && col<newGridSize.Cols)
                                    list[newGridSize.Pos(row, col)] = this[row, col];
                            }
                        }
                        SelectedApps = list;
                    }
                }
            }

            _gridSize = newGridSize;
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

        public Configuration()
        {
            SelectedApps = new List<AppInfo>(GridSize.CellCount);
            //for (int i = 0; i < Rows*Columns; i++)
            //    SelectedApps.Add(AppInfo.GetEmptyAppInfo());
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
            return SerializerHelper.Open<Configuration>(ConfigurationFileName);
        }
    }
}
