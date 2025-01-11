using Microsoft.Win32;
using MkZ.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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

namespace MultiPlayer
{
    /// <summary>
    /// Interaction logic for VideoCommandsUserControl.xaml
    /// </summary>
    public partial class VideoCommandsUserControl : UserControl
    {
        VideoPlayerUserControl _videoPlayerUserControl;
        public bool IsPopWindowMode { get; private set; } = false;

        public void TogglePlayPauseState()
        {
            _videoPlayerUserControl.TogglePlayPauseState();
            if (_videoPlayerUserControl.MediaState == MediaState.Pause)
                _btnPlayPause.Background = Brushes.LightBlue;
            else if (_videoPlayerUserControl.MediaState == MediaState.Play)
                _btnPlayPause.Background = Brushes.LightGreen;
            else
                _btnPlayPause.Background = Brushes.LightGray;
        }

        public void Play()
        {
            _videoPlayerUserControl.Play();
            _btnPlayPause.Background = Brushes.LightGreen;
        }

        public void Pause()
        {
            _videoPlayerUserControl.Pause();
            _btnPlayPause.Background = Brushes.LightBlue;
        }

        public void Stop()
        {
            _videoPlayerUserControl.Stop();
            _btnPlayPause.Background = Brushes.LightGray;
        }

        public VideoCommandsUserControl()
        {
            InitializeComponent();
        }

        public void Init(VideoPlayerUserControl v)
        {
            _videoPlayerUserControl = v;
            _videoPlayerUserControl.PropertyChanged += _videoPlayerUserControl_PropertyChanged;

            _videoPlayerUserControl.LeftButtonClick = () => { TogglePlayPauseState(); };
            _videoPlayerUserControl.LeftButtonDoubleClick = () => { Pop_Click(this, null); };
            _videoPlayerUserControl.VideoEnded = (player) => { Stop(); Play(); }; 
        }

        bool _isInUpdate = false;
        public void Update(OnePlayerSettings s, bool pop = false)
        {
            IsPopWindowMode = pop;

            _isInUpdate = true;
            
            _volume.Value = s.Volume * 1000.0;
            _position.Maximum = s.Duration;
            _position.Value = s.Position;
            _speed.SelectedIndex = SpeedRatio(s.SpeedRatio);

            _fit.SelectedIndex = (int)s.ZoomState;
            if (s.ZoomState == eZoomState.Custom)
                _videoPlayerUserControl.Zoom = s.Zoom;
            
            _timeLbl.Text = TimeSpan.FromSeconds(s.Position).ToString("mm':'ss");
            
            AdjustMarginsForVisibleScrollBars();

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
            string fileName = _videoPlayerUserControl.FileName;
            string dir = System.IO.Path.GetDirectoryName(_videoPlayerUserControl.FileName);
            OpenFileDialog ofd = new OpenFileDialog();
            if (Directory.Exists(dir))
            {
                ofd.InitialDirectory = dir;
                ofd.FileName = fileName;
            }
 
            if (ofd.ShowDialog().Value)
            {
                Open(ofd.FileName);
            }
        }

        public void Open(string fileName)
        {
            _videoPlayerUserControl.Open(fileName, _videoPlayerUserControl.Volume);
            _position.Maximum = _videoPlayerUserControl.NaturalDuration;
            _position.Value = 0;
            Play();
        }

        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            TogglePlayPauseState();
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_videoPlayerUserControl != null)
                _videoPlayerUserControl.Volume = _volume.Value / 1000.0;
        }

        double[] _speedRatios = { 0.1, 0.2, 0.5, 1.0, 1.5 };

        private void Speed_Selected(object sender, SelectionChangedEventArgs e)
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

        private void Fit_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (_videoPlayerUserControl == null || _isInUpdate)
                return;

            _videoPlayerUserControl.ZoomState = (eZoomState)_fit.SelectedIndex;

            AdjustMarginsForVisibleScrollBars();
        }

        private void AdjustMarginsForVisibleScrollBars()
        {
            double bottom = (_videoPlayerUserControl._scrollPlayerContainer.ComputedHorizontalScrollBarVisibility == Visibility.Visible) ? 12.0 : 0.0;
            this.Margin = new Thickness(0, 0, 0, bottom);
        }

        private bool _resume = false;
        private void Pos_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if (_videoPlayerUserControl == null || _isInUpdate)
                return;

            _resume = (_videoPlayerUserControl.MediaState == MediaState.Play);
            if (_resume)
                Pause();
        }

        private void Pos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_videoPlayerUserControl == null || _isInUpdate)
                return;

            bool resume = (_videoPlayerUserControl.MediaState == MediaState.Play);
            if (resume)
                Pause();

            _position.Value = e.NewValue;
            _videoPlayerUserControl.PositionSet(TimeSpan.FromSeconds(_position.Value), false);

            _timeLbl.Text = _videoPlayerUserControl.Position.ToString("mm':'ss");

            if (resume)
                Play();

            e.Handled = true;
        }

        private void Pos_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (_videoPlayerUserControl == null || _isInUpdate)
                return;

            _videoPlayerUserControl.PositionSet(TimeSpan.FromSeconds(_position.Value), false);

            if (_resume)
                Play();

            e.Handled = true;
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            List<string> fileNames = GetFileNames(_videoPlayerUserControl.FileName, out int idx);
            if (idx > 0 && idx < fileNames.Count)
                Open(fileNames[idx-1]);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            List<string> fileNames = GetFileNames(_videoPlayerUserControl.FileName, out int idx);
            if (idx >= 0 && idx < fileNames.Count - 1)
                Open(fileNames[idx + 1]);
        }

        private List<string> GetFileNames(string fileName, out int idx)
        {
            string dir = System.IO.Path.GetDirectoryName(fileName);

            List<string> fileNames = System.IO.Directory.EnumerateFiles(dir).ToList();
            fileNames.Sort();
            idx = fileNames.IndexOf(fileName);

            return fileNames;
        }

        private void Pop_Click(object sender, RoutedEventArgs e)
        {
            if (IsPopWindowMode)
                return; //do not open additional pop windows

            PopUpWindow wnd = new PopUpWindow();
            wnd.Owner = Application.Current.MainWindow;
            wnd.Load(new OnePlayerSettings(_videoPlayerUserControl));
            Pause();
            wnd.Show();
        }

        private void Pos_MouseMove(object sender, MouseEventArgs e)
        {
            ShowTimeToolTip(sender, e);
        }

        private void ShowTimeToolTip(object sender, MouseEventArgs e)
        {
            if (sender is Slider slider)
            {
                Point currentPos = e.GetPosition(slider);
                if (currentPos.Y < 30)
                {
                    if (!_popupSliderTooltip.IsOpen)
                        _popupSliderTooltip.IsOpen = true;

                    Track track = slider.Template.FindName("PART_Track", slider) as Track;

                    _txtSliderTooltip.Text = TimeSpan.FromSeconds(track.ValueFromPoint(currentPos)).ToString("mm':'ss");
                    string dur = TimeSpan.FromSeconds(_videoPlayerUserControl.Duration).ToString("mm':'ss");
                    _txtSliderTooltip.Text += " / " + dur;

                    _popupSliderTooltip.HorizontalOffset = currentPos.X - (_borderSliderTooltip.ActualWidth / 2);
                    _popupSliderTooltip.VerticalOffset = -20;
                }
                else
                {
                    _popupSliderTooltip.IsOpen = false;
                }
            }
        }

        private void Pos_MouseLeave(object sender, MouseEventArgs e)
        {
            _popupSliderTooltip.IsOpen = false;
        }
    }
}
