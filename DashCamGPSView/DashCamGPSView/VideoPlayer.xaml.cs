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

            mePlayer.MediaOpened += (s, e) => { FitWidth(); };
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

        public Size NaturalSize
        {
            get
            {
                if (mePlayer.Source != null)
                {
                    return new Size(mePlayer.NaturalVideoWidth, mePlayer.NaturalVideoHeight);
                }
                return new Size(1920, 1080);
            }
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

        public void Open(string fileName, double volume)
        {
            mePlayer.Source = string.IsNullOrEmpty(fileName)? null : new Uri(fileName);
            Volume = volume;
        }

        internal void Close()
        {
            mePlayer.Source = null;
        }

        public void Play() { if(mePlayer.Source != null) mePlayer.Play(); }
        public void Pause() { if (mePlayer.Source != null) mePlayer.Pause(); }
        public void Stop() { if (mePlayer.Source != null) mePlayer.Stop(); }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void mePlayer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        private void FitWidth()
        {
            mePlayer.Width = this.ActualWidth - 18;

            //proportionally change height
            Size sz = NaturalSize;
            mePlayer.Height = mePlayer.Width * sz.Height / sz.Width;

            ScrollToCenter();
        }

        internal void FitWindow()
        {
            mePlayer.Width = this.ActualWidth - 18;
            mePlayer.Height = this.ActualHeight - 18;

            ScrollToCenter();
        }

        internal void ResizeToActualSize()
        {
            Size sz = NaturalSize;
            mePlayer.Width = sz.Width;
            mePlayer.Height = sz.Height;

            ScrollToCenter();
        }
    }
}
