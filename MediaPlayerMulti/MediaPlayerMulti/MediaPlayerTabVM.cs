using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MZ.Windows;

namespace MkZ.MediaPlayer
{
    public class MediaPlayerTabVM : NotifyPropertyChangedImpl
    {
        public string Title 
        { 
            get 
            {
                if (string.IsNullOrWhiteSpace(PlayerVM.Title))
                    return "?";
                if (PlayerVM.Title.Length < 32)
                    return PlayerVM.Title;
                return PlayerVM.Title.Substring(0, 32); 
            } 
        }

        public string VideoResolution
        {
            get { return PlayerVM.VideoResolution; }
        }

        public string FileName { get { return PlayerVM.FileName; } }
        public TimeSpan Position { get { return PlayerVM.Position; } set { PlayerVM.Position = value; } }

        public VideoPlayerControlVM PlayerVM { get; } = new VideoPlayerControlVM();

        public MediaPlayerTabVM()
        {
            PlayerVM.PropertyChanged += PlayerVM_PropertyChanged;
        }

        private void PlayerVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyPropertyChangedAll();
        }
    }
}
