using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MkZ.WPF
{
    public interface IMediaPlayer : INotifyPropertyChanged
    {
        Action<IMediaPlayer> MediaStartedAction { get; set; }
        Action<IMediaPlayer> MediaEndedAction { get; set; }
        Func<object, ExceptionRoutedEventArgs, bool> MediaFailedAction { get; set; }

        MediaState MediaState { get; }
        string FileName { get; }
        double SpeedRatio { get; set; }
        double Volume { get; set; }
        TimeSpan Position { get; set; }
        Size NaturalSize { get; }
        double NaturalDuration { get; }

        void Play();
        void Pause();
        void Stop();
        void TogglePlayPauseState();

        void FitWidth(bool adjustScroll);
    }
}
