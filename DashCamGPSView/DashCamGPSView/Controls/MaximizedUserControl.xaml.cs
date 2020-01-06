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
using System.Windows.Threading;

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for MaximizedUserControl.xaml
    /// </summary>
    public partial class MaximizedUserControl : UserControl
    {
        public Action<double, MediaState, double> CloseAction = (position, state, volume) => { };

        private VideoPlayerControl _sourcePlayer = null;

        public void Play() { _player.Play(); }
        public void Pause() { _player.Pause(); }

        public VideoPlayerControl Player { get { return _player; } }

        public Slider sliProgress { get { return statusBar.sliProgress; } }

        public MaximizedUserControl()
        {
            InitializeComponent();
            
            _player.LeftButtonClick = TogglePlayPauseState;
            _player.LeftButtonDoubleClick = () => btnClose_Click(this, null);
            _player.VideoEnded = () => { _player.Pause(); };
            //Player.VideoStarted = (player) => { /*UpdateSliderLimits();*/ };

            //statusBar.SetPlayer(Player);

            thumbnails.OnItemSelectedAction = (item) => 
            {
                if(item != null)
                {
                    Pause();
                    _player.Position = TimeSpan.FromSeconds(item.start);
                }
            };
        }

        public void TogglePlayPauseState()
        {
            _player.TogglePlayPauseState();
        }

        public void ShowWithControl(VideoPlayerControl player, double volume)
        {
            if (_sourcePlayer != null)
                throw new Exception("Invalid call");

            _sourcePlayer = player;

            _player.CopyState(player, volume, true);
            _player.RestoreMediaState(player.MediaState, player.Position);

            this.Visibility = Visibility.Visible;
            _player.FitWidth(false);

            thumbnails.StartCreateThumbnailsFromVideoFile(player);

            _player.btnMaximize.IsEnabled = false;
        }

        private void btnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            Pause();
            //Tools.Tools.Screenshot(Tools.GpsFileFormat.Unkn, Player.FileName, Player.Position, Application.Current.MainWindow);
            Tools.Tools.Snapshot(Tools.GpsFileFormat.Unkn, _player.FileName, _player.Position, _player.VideoPlayerElement);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MediaState state = _player.MediaState;
            Pause();

            double position = _player.Position.TotalSeconds;
            double volume = _player.Volume;

            _player.Close();
            _sourcePlayer = null;
            this.Visibility = Visibility.Collapsed;

            CloseAction(position, state, volume);
        }

        //private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    TimeSpan tsPos = TimeSpan.FromSeconds(sliProgress.Value);
        //    TimeSpan tsMax = TimeSpan.FromSeconds(Player.NaturalDuration);

        //    //System.Diagnostics.Debug.WriteLine("Slider: " + tsPos);
        //    if (!_isInTimer)
        //    {
        //        if (Player.Position == tsPos)
        //            return; //no update needed

        //        Player.Position = tsPos;
        //        //System.Diagnostics.Debug.WriteLine("Player(2): " + Player.Position);
        //        if (sliProgress.Value - Player.Position.TotalSeconds > 0.0001)
        //            sliProgress.Value = Player.Position.TotalSeconds;
        //    }

        //    thumbnails.SelectItem(sliProgress.Value);
        //    lblProgressStatus.Text = tsPos.ToString(@"hh\:mm\:ss\.fff") + "/" + tsMax.ToString(@"hh\:mm\:ss");
        //}

        //private void UpdateSliderLimits()
        //{
        //    sliProgress.Minimum = 0;
        //    if (Player.NaturalDuration != 0)
        //    {
        //        sliProgress.Maximum = Player.NaturalDuration;
        //        //sliProgress.Value = Player.Position.TotalSeconds;
        //        if (sliProgress.Maximum >= 60)
        //            sliProgress.SmallChange = 1;
        //        else //if less than minute - have 60 tics
        //            sliProgress.SmallChange = sliProgress.Maximum / 60.0;

        //        sliProgress.LargeChange = sliProgress.Maximum / 10.0;
        //    }
        //}

        //private void btnNextFrame_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Player.MediaState == MediaState.Play)
        //        Pause();

        //    sliProgress.Value += 0.064;
        //}
    }
}
