using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using MZ.Tools;

namespace MkZ.MediaPlayer.Utils
{
    public class AppConfig
    {
        private string _dataFolder;
        private string _fileName;

        public List<VideoPlayerState> OpenedFiles { get; set; } = new List<VideoPlayerState>();

        public AppConfig()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string commonPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dataFolder = Path.Combine(commonPath, "MarkZ", assemblyName);
            Directory.CreateDirectory(_dataFolder);

            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("{0}_{1}.xml", assemblyName, "Files");
            _fileName = Path.Combine(_dataFolder, fileName);
        }

        public void Save()
        {
            XmlHelper.Save(_fileName, this);
        }

        public void Load()
        {
            if (File.Exists(_fileName))
            {
                AppConfig appConfig = XmlHelper.Open<AppConfig>(_fileName);
                this.OpenedFiles.AddRange(appConfig.OpenedFiles);
            }
        }
    }
}
