using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

using MZ.WPF;
using System.Windows.Media.Animation;
using MkZ.MediaPlayer.Controls;

namespace MkZ.MediaPlayer
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayerControl : UserControl
    {
        private VideoPlayerControlVM _vm = null;
        private readonly AnimationHelper _controlsHideAndShow;

        public VideoPlayerControl()
        {
            InitializeComponent();

            _controlsHideAndShow = new AnimationHelper(this, _playControls);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_vm != null && _vm.IsAttached)
            {
                _vm.Detach();
            }

            if (this.DataContext is VideoPlayerControlVM vm)
            {
                if(vm != null && !vm.IsInitialized)
                {
                    vm.Init(_scrollPlayerContainer);
                }

                _vm = vm;

                if (_vm != null)
                {
                    _playControls.DataContext = _vm;

                    _vm.Attach(_scrollPlayerContainer);
                    _vm.NotifyPropertyChangedAll();
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                _vm.OpenAndPlay(files[0]);
            }
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _vm.LeftButtonDoubleClick();
        }

        private void UserControl_PreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && (e.OriginalSource is MediaElement))
                _vm.LeftButtonClick();
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void btnFitWidth_Click(object sender, RoutedEventArgs e)
        {
            _vm.FitWidth(true);
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            _vm.MaximizeAction();
        }

        private void btnOriginalSize_Click(object sender, RoutedEventArgs e)
        {
            _vm.OriginalSize(true);
        }

        private void btnFitWindow_Click(object sender, RoutedEventArgs e)
        {
            _vm.FitWindow();
        }

        private void btnFlipHorizontally_Click(object sender, RoutedEventArgs e)
        {
            _vm.IsFlipHorizontally = !_vm.IsFlipHorizontally;
        }

        private void btnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            _vm.TogglePlayPauseState();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _vm.FitWindow();
        }
    }
}
