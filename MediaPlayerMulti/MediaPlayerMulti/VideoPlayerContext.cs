using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MkZ.MediaPlayer.Utils;

namespace MkZ.MediaPlayer
{
    public class VideoPlayerContext
    {
        public static VideoPlayerContext Instance { get; } = new VideoPlayerContext() { InDesignMode = GetInDesignMode() };

        public AppConfig Config { get; } = new AppConfig();

        public VideoPlayerControlVM PlayerVM { get; set; } = null;
        public bool InDesignMode { get; set; }

        public void AddNewMediaFiles(PlayList playList, string[] fileNames, double volume)
        {
            List<string> unsupported = new List<string>();
            int index = -1, count = 0;
            foreach (string fileName in fileNames)
            {
                if (Config.Configuration.IsSupportedImageFile(fileName))
                {
                    Config.Configuration.BackgroundImageFileName = fileName;
                }
                else if (Config.Configuration.IsSupportedMediaFile(fileName))
                {
                    count++;
                    index = playList.AddNewMediaFile(fileName, volume);
                }
                else
                {
                    unsupported.Add(fileName);
                }
            }

            if (unsupported.Count > 0)
                MessageBox.Show("File type is not supported.\n" + unsupported[0], "AddNewMediaFiles");
        }


        private static bool GetInDesignMode()
        {
            if (Application.Current == null) return false;
            if (Application.Current.Properties != null && Application.Current.Properties.Contains("InDesignMode"))
            {
                return (bool)Application.Current.Properties["InDesignMode"];
            }
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return true;
            }
            return false;
        }
    }
}
