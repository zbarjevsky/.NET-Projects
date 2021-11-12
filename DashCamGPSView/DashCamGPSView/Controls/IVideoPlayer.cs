using DashCamGPSView.Tools;
using MkZ.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DashCamGPSView.Controls
{
    public interface IVideoPlayer : INotifyPropertyChanged
    {
        Action<IVideoPlayer> VideoStarted { get; set; }

        MediaState MediaState { get; }
        string FileName { get; }
        double SpeedRatio { get; set; }
        double Volume { get; set; }
        TimeSpan Position { get; }
        void PositionSet(TimeSpan position, bool notify);
        Size NaturalSize { get; }
        double NaturalDuration { get; }

        void Play();
        void Pause();
        void Stop();
        void TogglePlayPauseState();

        void ZoomStateSet(eZoomState zoom, bool adjustScroll);
        eZoomState ZoomStateGet();

        bool IsFlipHorizontally { get; set; }
        double ScrollOffsetY { get; set; }
    }
}
