﻿using Microsoft.Win32;
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
        private readonly VideoPlayerControlVM _playerControlVM = new VideoPlayerControlVM();
        private readonly AnimationHelper _controlsHideAndShow;

        public Action<VideoPlayerControlVM> OnFullScreenButtonClick = (vm) => { };
        public Action<string> OnFileDropAction = (fileName) => { };

        public MediaFileInfo MediaFileInfo
        {
            get { return (MediaFileInfo)GetValue(MediaFileInfoProperty); }
            set { SetValue(MediaFileInfoProperty, value); }
        }

        public static readonly DependencyProperty MediaFileInfoProperty =
            DependencyProperty.Register(nameof(MediaFileInfo), typeof(MediaFileInfo), typeof(VideoPlayerControl), new UIPropertyMetadata(OnMediaFileInfoChanged));

        private static void OnMediaFileInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is VideoPlayerControl This)
                This.UpdateMediaInfo();
        }

        private void UpdateMediaInfo()
        {
            _playerControlVM.SetMediaInfo(MediaFileInfo);
        }

        public VideoPlayerControl()
        {
            DataContext = _playerControlVM;

            InitializeComponent();

            _controlsHideAndShow = new AnimationHelper(this, 2,
                _playControls, _testButtons, _systemButtons);

            _playerControlVM.Init(_scrollPlayerContainer);
            _playControls.DataContext = _playerControlVM;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        public void ClosePlayer()
        {
            _playerControlVM.Close();
        }

        private void ButtonFullScreen_Click(object sender, RoutedEventArgs e)
        {
            OnFullScreenButtonClick(_playerControlVM);
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                OnFileDropAction(files[0]);
            }
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && (e.OriginalSource is MediaElement))
            {
                _playerControlVM.LeftButtonDoubleClick(_playerControlVM);
                OnFullScreenButtonClick(_playerControlVM);
            }
        }

        private void UserControl_PreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if ((e.OriginalSource is MediaElement))
                    _playerControlVM.LeftButtonClick(_playerControlVM);
                if ((e.OriginalSource is ScrollViewer))
                    _playerControlVM.LeftButtonClick(_playerControlVM);
            }
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(_playerControlVM != null && _playerControlVM.IsAttached)
                _playerControlVM.FitWindow();
        }

        private void btnFitWidth_Click(object sender, RoutedEventArgs e)
        {
            _playerControlVM.FitWidth(true);
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            _playerControlVM.MaximizeAction();
        }

        private void btnOriginalSize_Click(object sender, RoutedEventArgs e)
        {
            _playerControlVM.OriginalSize(true);
        }

        private void btnFitWindow_Click(object sender, RoutedEventArgs e)
        {
            _playerControlVM.FitWindow();
        }

        private void btnFlipHorizontally_Click(object sender, RoutedEventArgs e)
        {
            _playerControlVM.IsFlipHorizontally = !_playerControlVM.IsFlipHorizontally;
        }

        private void btnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            _playerControlVM.TogglePlayPauseState();
        }
    }
}