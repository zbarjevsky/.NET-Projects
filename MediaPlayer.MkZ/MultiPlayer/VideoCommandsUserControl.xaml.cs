using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiPlayer
{
    /// <summary>
    /// Interaction logic for VideoCommandsUserControl.xaml
    /// </summary>
    public partial class VideoCommandsUserControl : UserControl
    {
        VideoPlayerUserControl _videoPlayerUserControl;

        public VideoCommandsUserControl()
        {
            InitializeComponent();
        }

        public void Init(VideoPlayerUserControl videoPlayerUserControl)
        {
            _videoPlayerUserControl = videoPlayerUserControl;
            _videoPlayerUserControl.PropertyChanged += _videoPlayerUserControl_PropertyChanged;
        }

        private void _videoPlayerUserControl_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Position":
                    _position.Value = _videoPlayerUserControl.Position.TotalSeconds;
                    break;
                default:
                    break;
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = _videoPlayerUserControl.FileName,
                InitialDirectory = System.IO.Path.GetDirectoryName(_videoPlayerUserControl.FileName)
            };

            if (ofd.ShowDialog().Value)
            {
                _videoPlayerUserControl.Open(ofd.FileName, _videoPlayerUserControl.Volume);
                _videoPlayerUserControl.Play();

                _position.Maximum = _videoPlayerUserControl.NaturalDuration;
            }
        }

        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            _videoPlayerUserControl.TogglePlayPauseState();
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_videoPlayerUserControl != null)
                _videoPlayerUserControl.Volume = _volume.Value / 1000.0;
        }

        private void Pos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
