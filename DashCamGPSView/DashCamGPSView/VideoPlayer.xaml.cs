using DashCamGPSView.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DashCamGPSView
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl
    {
        private ScrollDragZoom _scrollDragger;

        public VideoPlayer()
        {
            InitializeComponent();

            _scrollDragger = new ScrollDragZoom(mePlayer, scrollPlayer);

            //refresh view when change position
            mePlayer.ScrubbingEnabled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        public bool Play_CanExecute 
        { 
            get
            {
                return (mePlayer != null) && (mePlayer.Source != null);
            } 
        }

        public double Volume
        {
            get { return mePlayer.Volume; }
            set { mePlayer.Volume = value; }
        }

        public TimeSpan Position 
        { 
            get { return mePlayer.Position; }
            set { mePlayer.Position = value; } 
        }

        public double NaturalDuration
        {
            get
            {
                if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan))
                {
                    return mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                }
                return 0;
            }
        }

        public void ScrollToCenter()
        {
            _scrollDragger.ScrollToCenter();
        }

        public void Open(string fileName)
        {
            mePlayer.Source = new Uri(fileName);
        }

        public void Play() { mePlayer.Play(); }
        public void Pause() { mePlayer.Pause(); }
        public void Stop() { mePlayer.Stop(); }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void mePlayer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        internal void UpdateVideoSize()
        {
            mePlayer.Width = this.ActualWidth - 8;
            ScrollToCenter();
        }
    }
}
