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

        public void Init(VideoPlayerUserControl v)
        {
            _videoPlayerUserControl = v;
            _videoPlayerUserControl.PropertyChanged += _videoPlayerUserControl_PropertyChanged;

            _videoPlayerUserControl.LeftButtonClick = () => { _videoPlayerUserControl.TogglePlayPauseState(); };
            _videoPlayerUserControl.VideoEnded = (player) => { _videoPlayerUserControl.Stop(); _videoPlayerUserControl.Play(); }; 
        }

        bool _isInUpdate = false;
        public void Update(VideoPlayerUserControl v)
        {
            _isInUpdate = true;
            _volume.Value = v.Volume * 1000.0;
            _position.Maximum = v.NaturalDuration;
            _position.Value = v.Position.TotalSeconds;
            _speed.SelectedIndex = SpeedRatio(v.SpeedRatio);
            _isInUpdate = false;
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
                _position.Maximum = _videoPlayerUserControl.NaturalDuration;
                _position.Value = 0;
                _videoPlayerUserControl.Play();

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

        private void Pos_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if (_videoPlayerUserControl == null || _isInUpdate)
                return;

            MediaState state = _videoPlayerUserControl.MediaState;
            if (state != MediaState.Pause)
                _videoPlayerUserControl.Pause();

        }

        double[] _speedRatios = { 0.1, 0.2, 0.5, 1.0, 1.5 };

        private void Speed_Selected(object sender, RoutedEventArgs e)
        {
            if (_videoPlayerUserControl == null || _isInUpdate)
                return;

            _videoPlayerUserControl.SpeedRatio = _speedRatios[_speed.SelectedIndex];
        }

        private int SpeedRatio(double speed)
        {
            for (int i = 0; i < _speed.Items.Count; i++)
            {
                if (speed == _speedRatios[i])
                    return i;
            }
            return 3;
        }

        private void Fit_Selected(object sender, RoutedEventArgs e)
        {
            if (_videoPlayerUserControl == null || _isInUpdate)
                return;

            _videoPlayerUserControl.ZoomState = (MkZ.WPF.eZoomState)_fit.SelectedIndex;
        }

        private void Pos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void Pos_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (_videoPlayerUserControl == null || _isInUpdate)
                return;

            MediaState state = _videoPlayerUserControl.MediaState;
            if (state != MediaState.Pause)
                _videoPlayerUserControl.Pause();

            _videoPlayerUserControl.PositionSet(TimeSpan.FromSeconds(_position.Value), false);
        }
    }
}
