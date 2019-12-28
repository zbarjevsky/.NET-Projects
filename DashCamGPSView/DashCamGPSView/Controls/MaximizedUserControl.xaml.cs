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

        DispatcherTimer _timer = new DispatcherTimer();
        private VideoPlayer _sourcePlayer = null;

        public MaximizedUserControl()
        {
            InitializeComponent();
            
            _timer.Interval = TimeSpan.FromSeconds(0.3);
            _timer.Tick += timer_Tick;

            Player.LeftButtonClick = TogglePlayPauseState;
            Player.LeftButtonDoubleClick = () => btnClose_Click(this, null);
            Player.VideoEnded = () => { Player.Pause(); };

            thumbnails.OnItemSelectedAction = (item) => 
            {
                if(item != null)
                {
                    Player.Pause();
                    sliProgress.Value = item.start;
                }
            };
        }

        private bool _isInTimer = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            _isInTimer = true;
            if (Player != null && Player.MediaState == MediaState.Play)
            {
                sliProgress.Value = Player.Position.TotalSeconds;
            }
            _isInTimer = false;
        }

        public void TogglePlayPauseState()
        {
            Player.TogglePlayPauseState();
        }

        public void ShowWithControl(VideoPlayer player, double volume)
        {
            if (_sourcePlayer != null)
                throw new Exception("Invalid call");

            _sourcePlayer = player;

            Player.CopyState(player, volume, true);
            Player.RestoreMediaState(player.MediaState, player.Position);

            this.Visibility = Visibility.Visible;
            Player.FitWidth(false);

            thumbnails.StartCreateThumbnailsFromVideoFile(player);

            sliProgress.Maximum = player.NaturalDuration;
            sliProgress.Value = player.Position.TotalSeconds;
            sliProgress_ValueChanged(null, null);

            _timer.Start();

            Player.btnMaximize.IsEnabled = false;
        }

        private void btnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
            //Tools.Tools.Screenshot(Tools.GpsFileFormat.Unkn, Player.FileName, Player.Position, Application.Current.MainWindow);
            Tools.Tools.Snapshot(Tools.GpsFileFormat.Unkn, Player.FileName, Player.Position, Player.VideoPlayerElement);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MediaState state = Player.MediaState;
            Player.Pause();

            double pos1 = sliProgress.Value;
            double position = Player.Position.TotalSeconds;
            double volume = Player.Volume;

            Player.Close();
            _sourcePlayer = null;
            this.Visibility = Visibility.Collapsed;

            CloseAction(pos1, state, volume);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan tsPos = TimeSpan.FromSeconds(sliProgress.Value);
            TimeSpan tsMax = TimeSpan.FromSeconds(Player.NaturalDuration);

            if(!_isInTimer)
                Player.Position = tsPos;

            sliProgress.Minimum = 0;
            if (Player.NaturalDuration != 0)
            {
                sliProgress.Maximum = Player.NaturalDuration;
                //sliProgress.Value = Player.Position.TotalSeconds;
                if (sliProgress.Maximum >= 60)
                    sliProgress.SmallChange = 1;
                else //if less than minute - have 60 tics
                    sliProgress.SmallChange = sliProgress.Maximum / 60.0;

                sliProgress.LargeChange = sliProgress.Maximum / 10.0;
            }

            thumbnails.SelectItem(sliProgress.Value);
            lblProgressStatus.Text = tsPos.ToString(@"hh\:mm\:ss") + "/" + tsMax.ToString(@"hh\:mm\:ss");
        }
    }
}
