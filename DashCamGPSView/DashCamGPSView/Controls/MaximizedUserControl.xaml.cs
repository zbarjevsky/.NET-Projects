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
        public Action<VideoPlayer> CloseAction = (player) => { };

        DispatcherTimer _timer = new DispatcherTimer();
        private VideoPlayer _sourcePlayer = null;

        public MaximizedUserControl()
        {
            InitializeComponent();
            
            _timer.Interval = TimeSpan.FromSeconds(0.3);
            _timer.Tick += timer_Tick;

            Player.LeftButtonClick = TogglePlayPauseState;
            Player.LeftButtonDoubleClick = () => { };

            thumbnails.OnItemSelectedAction = (item) => 
            {
                if(item != null)
                {
                    Player.Pause();
                    sliProgress.Value = item.seconds;
                }
            };
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Player != null && Player.MediaState == MediaState.Play)
            {
                sliProgress.Value = Player.Position.TotalSeconds;
            }
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

            this.Visibility = Visibility.Visible;
            Player.FitWidth(false);

            sliProgress.Maximum = player.NaturalDuration;
            sliProgress.Value = player.Position.TotalSeconds;
            sliProgress_ValueChanged(null, null);

            thumbnails.StartCreateThumbnailsFromVideoFile(player.FileName);

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
            _sourcePlayer.CopyState(Player, Player.Volume, false);
            
            CloseAction(Player);
            
            Player.Close();
            _sourcePlayer = null;
            this.Visibility = Visibility.Collapsed;
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan tsPos = TimeSpan.FromSeconds(sliProgress.Value);
            TimeSpan tsMax = TimeSpan.FromSeconds(Player.NaturalDuration);

            Player.Position = tsPos;

            sliProgress.Minimum = 0;
            if (Player.NaturalDuration != 0)
            {
                sliProgress.Maximum = Player.NaturalDuration;
                sliProgress.Value = Player.Position.TotalSeconds;
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
