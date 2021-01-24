using MkZ.MediaPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for PlayerControls.xaml
    /// </summary>
    public partial class PlayerControls : UserControl
    {
        public PlayerControls()
        {
            InitializeComponent();
        }

        private bool _resume = false;
        private void Slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if(DataContext is VideoPlayerControlVM vm)
            {
                _resume = (vm.MediaState == MediaState.Play);
                if(_resume)
                    vm.Pause();
            }
        }

        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (DataContext is VideoPlayerControlVM vm)
            {
                if (_resume)
                    vm.Play();
            }
        }

        private void _sliderPosition_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Slider slider)
            {
                Point currentPos = e.GetPosition(slider);
                if (currentPos.Y < 30)
                {
                    if (!_popupSliderTooltip.IsOpen) 
                        _popupSliderTooltip.IsOpen = true;

                    Track track = slider.Template.FindName("PART_Track", slider) as Track;

                    _txtSliderTooltip.Text = SecondsToStringConverter.SeondsToString(track.ValueFromPoint(currentPos));

                    _popupSliderTooltip.HorizontalOffset = currentPos.X - (_borderSliderTooltip.ActualWidth / 2);
                    _popupSliderTooltip.VerticalOffset = -20;
                }
                else
                {
                    _popupSliderTooltip.IsOpen = false;
                }
            }
        }

        private void _sliderPosition_MouseLeave(object sender, MouseEventArgs e)
        {
            _popupSliderTooltip.IsOpen = false;
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VideoPlayerControlVM vm)
            {
                vm.Open(vm.State);
            }
        }

        private void Skip_Backward_Click(object sender, RoutedEventArgs e)
        {
            Skip(-10);
        }

        private void Skip_Forward_Click(object sender, RoutedEventArgs e)
        {
            Skip(10);
        }

        private void Skip(double seconds)
        {
            if (DataContext is VideoPlayerControlVM vm)
            {
                double pos = vm.Position.TotalSeconds;
                pos += seconds;
                if (pos >= vm.NaturalDuration)
                    pos = vm.NaturalDuration - 1.0;
                if (pos < 0)
                    pos = 0;

                vm.Position = TimeSpan.FromSeconds(pos);
            }
        }

        private void Skip_Forward_1Frame_Click(object sender, RoutedEventArgs e)
        {
            Skip(0.03);
        }
    }
}
