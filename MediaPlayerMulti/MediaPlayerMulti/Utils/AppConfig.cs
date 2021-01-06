using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using MZ.Tools;

namespace MkZ.MediaPlayer.Utils
{
    [Serializable]
    public class Configuration
    {
        public ePlayMode PlayMode { get; set; } = ePlayMode.RepeatOne;

        public void CopyFrom(Configuration config)
        {
            PlayMode = config.PlayMode;
        }

        public override string ToString()
        {
            return string.Format("Configuration, PlayMode: {0}", PlayMode);
        }
    }

    //directory tree style play list
    public class PlayList
    {
        public List<PlayList> PlayLists { get; set; } = new List<PlayList>();

        public int SelectedIndex { get; set; } = 0;

        public List<MediaFileInfo> MediaFiles { get; set; } = new List<MediaFileInfo>();

        public PlayList GetPlayList(params int[] treePath)
        {
            if (treePath == null || treePath.Length == 0)
                return this;

            int idx = treePath[0];
            if (idx < PlayLists.Count)
            {
                int[] subPath = new int[treePath.Length - 1];
                Array.Copy(treePath, 1, subPath, 0, treePath.Length - 1);

                return PlayLists[idx].GetPlayList(subPath);
            }

            throw new IndexOutOfRangeException("PlayList not found, idx: " + idx);
        }

        public MediaFileInfo FindFile(string subString)
        {
            foreach (MediaFileInfo item in MediaFiles)
            {
                if (item.FileName.Contains(subString))
                    return item;
            }

            foreach (PlayList list in PlayLists)
            {
                MediaFileInfo info = list.FindFile(subString);
                if (info != null)
                    return info;
            }

            return null;
        }

        public override string ToString()
        {
            return string.Format(" MediaFiles {0}, SelectedIndex: {1}, PlayLists {2}", 
                MediaFiles.Count, SelectedIndex, PlayLists.Count);
        }
    }

    public class AppConfig
    {
        private string _dataFolder;
        private string _fileName;

        [Category("Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Configuration Configuration { get; set; } = new Configuration();

        [Category("Media Database"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PlayList RootList { get; set; } = new PlayList();

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

        public void CopyFrom(AppConfig config)
        {
            RootList = config.RootList;
            Configuration.CopyFrom(config.Configuration);
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
                this.CopyFrom(appConfig);
            }
        }
    }
}
