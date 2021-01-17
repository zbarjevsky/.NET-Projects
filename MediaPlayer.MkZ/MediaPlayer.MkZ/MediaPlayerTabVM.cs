using System;
using System.Collections.Generic;
using System.IO;
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
                if (string.IsNullOrWhiteSpace(PlayerVM.SavedState.FileName))
                    return "?";

                string fileName = Path.GetFileName(PlayerVM.SavedState.FileName);
                if (fileName.Length < 32)
                    return fileName;
                return fileName.Substring(0, 32); 
            } 
        }

        public string VideoResolution
        {
            get { return PlayerVM.VideoResolution; }
        }

        public string Status
        {
            get
            { 
                if(PlayerVM.IsInitialized && PlayerVM.Position.TotalSeconds > 0)
                    return PlayerVM.Position.ToString("mm':'ss");
                return "";
            }
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
            if (e.PropertyName == nameof(VideoPlayerControlVM.Position))
                NotifyPropertyChanged(nameof(Status));
            if (e.PropertyName == nameof(VideoPlayerControlVM.FileName))
                NotifyPropertyChangedAll();
        }

        public override string ToString()
        {
            return PlayerVM.ToString();
        }
    }
}
