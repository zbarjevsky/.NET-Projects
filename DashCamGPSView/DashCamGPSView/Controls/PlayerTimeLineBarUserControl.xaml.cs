using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for PlayerTimelineBarUserControl.xaml
    /// </summary>
    public partial class PlayerTimelineBarUserControl : UserControl, INotifyPropertyChanged
    {
        DispatcherTimer _timer = new DispatcherTimer();

        public Action<TimeSpan> OnVideoPositionChanged = (position) => { };

        /// <summary>
        /// Dependency property to Get/Set the Maximum Value 
        /// </summary>
        public static readonly DependencyProperty ExternalPlayerProperty =
            DependencyProperty.Register("ExternalPlayer", typeof(IVideoPlayer), typeof(PlayerTimelineBarUserControl), 
            new PropertyMetadata(OnItemsSourceChanged));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IVideoPlayer value = (IVideoPlayer)e.NewValue;
            PlayerTimelineBarUserControl thisControl = d as PlayerTimelineBarUserControl;
            thisControl.ExternalPlayerBinded();
        }

        public IVideoPlayer ExternalPlayer
        {
            get { return (IVideoPlayer)GetValue(ExternalPlayerProperty); }
            set { SetValue(ExternalPlayerProperty, value); } 
        }

        public double SpeedRatio
        {
            get
            {
                if(ExternalPlayer != null)
                    return ConvertSpeedRatio(ExternalPlayer.SpeedRatio);
                return 3;
            }

            set
            {
                ExternalPlayer.SpeedRatio = ConvertSpeedRatio((int)value); OnPropertyChanged();
            }
        }

        public PlayerTimelineBarUserControl()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(0.3);
            _timer.Tick += timer_Tick;
        }

        private void ExternalPlayerBinded()
        {
            ExternalPlayer.VideoStarted = (player) => { UpdateTimeLineSliderLimits(); };
            ExternalPlayer.PropertyChanged += (s, p) =>
            {
                if (p.PropertyName == nameof(ExternalPlayer.MediaState))
                    UpdateTimerState();
                if (p.PropertyName == nameof(ExternalPlayer.Position))
                    UpdateSliderPosition();
            };
        }

        private void UpdateSliderPosition()
        {
            _isInTimer = true;
            if (ExternalPlayer != null)
            {
                sliProgress.Value = ExternalPlayer.Position.TotalSeconds;
                OnVideoPositionChanged(ExternalPlayer.Position);
            }
            _isInTimer = false;
        }

        private bool _isInTimer = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateSliderPosition();
        }

        private void UpdateTimerState()
        {
            if (ExternalPlayer.MediaState == MediaState.Play)
                _timer.Start();
            else
                _timer.Stop();
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan tsPos = TimeSpan.FromSeconds(sliProgress.Value);
            TimeSpan tsMax = TimeSpan.FromSeconds(ExternalPlayer.NaturalDuration);

            //System.Diagnostics.Debug.WriteLine("Slider: " + tsPos);
            if (!_isInTimer)
            {
                if (ExternalPlayer.Position == tsPos)
                    return; //no update needed

                ExternalPlayer.Position = tsPos;
                //System.Diagnostics.Debug.WriteLine("Player(2): " + Player.Position);
                if (sliProgress.Value - ExternalPlayer.Position.TotalSeconds > 0.0001)
                    sliProgress.Value = ExternalPlayer.Position.TotalSeconds;
            }

            if(ExternalPlayer.NaturalDuration > 0)
                lblProgressStatus.Text = tsPos.ToString(@"hh\:mm\:ss\.fff") + "/" + tsMax.ToString(@"hh\:mm\:ss");
            else
                lblProgressStatus.Text = "--:--:--/--:--:--";
        }

        private void UpdateTimeLineSliderLimits()
        {
            sliProgress.Minimum = 0;
            if (ExternalPlayer.NaturalDuration != 0)
            {
                sliProgress.Maximum = ExternalPlayer.NaturalDuration;
                //sliProgress.Value = Player.Position.TotalSeconds;
                if (sliProgress.Maximum >= 60)
                    sliProgress.SmallChange = 1;
                else //if less than minute - have 60 tics
                    sliProgress.SmallChange = sliProgress.Maximum / 60.0;

                sliProgress.LargeChange = sliProgress.Maximum / 10.0;
            }
        }

        double[] speedMultipliers = new double [] { 0.033, 0.25, 0.5, 1.0, 2.0, 4.0, 8.0, 16.0 };
        private double ConvertSpeedRatio(int sliderPosition)
        {
            if (sliderPosition >= 0 && sliderPosition < speedMultipliers.Length)
                return speedMultipliers[sliderPosition];
            return 1;
        }
        private int ConvertSpeedRatio(double playerSpeedRatio)
        {
            for (int i = 1; i < speedMultipliers.Length; i++)
            {
                if (playerSpeedRatio >= speedMultipliers[i - 1] && playerSpeedRatio < speedMultipliers[i])
                    return i - 1;
            }

            return 3; //position of x1
        }

        private void btnNextFrame_Click(object sender, RoutedEventArgs e)
        {
            if (ExternalPlayer.MediaState == MediaState.Play)
                ExternalPlayer.Pause();

            sliProgress.Value += 0.064;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
