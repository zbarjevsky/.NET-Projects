using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CameraCapture.Interface;
using MediaFoundation;
using MZ.Utils;
using MZ.WPF;
using VideoModule;
using VideoModule.Tools;

namespace ControlModule
{
    public class ConsoleViewModel : NotifyPropertyChangedImpl
    {
        private VideoModuleLogic _videoModuleLogic;

        private class EmptyDevice : IDeviceInfo
        {
            public string Name { get; }

            public string SymbolicName => "";

            public EmptyDevice(string name)
            {
                Name = name;
            }
        }

        private IDeviceInfo _emptyDevice = new EmptyDevice("Close");

        public ConsoleViewModel(VideoModuleLogic videoModuleLogic)
        {
            _videoModuleLogic = videoModuleLogic;

            IList<IDeviceInfo> vidCapList = MfDevice.GetCategoryDevices(CLSID.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
            vidCapList.Insert(0, _emptyDevice);

            VideoDeviceItems = new ObservableCollection<IDeviceInfo>(vidCapList);
            SelectedVideoDevice = _emptyDevice;

            VideoResolutionItems = new ObservableCollection<VideoResolutionInfo>();

            IList<IDeviceInfo> audCapList = MfDevice.GetCategoryDevices(CLSID.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_GUID);
            audCapList.Insert(0, new EmptyDevice("Master Volume"));
            audCapList.Insert(0, _emptyDevice);

            AudioDeviceItems = new ObservableCollection<IDeviceInfo>(audCapList);
            SelectedAudioDevice = _emptyDevice;

            PlayCommand = new RelayCommand(o =>
            {
                if (SelectedVideoDevice == _emptyDevice)
                    return;
                string op = o as string;

                _videoModuleLogic.OnOperation(op);
                    
                OperationString = op == "Start" ? "Stop" : "Start";
                RecordIconVisibility = (OperationString == "Stop") ? Visibility.Visible : Visibility.Hidden;
            });

            SnapCommand = new RelayCommand(op =>
            {
                if (SelectedVideoDevice == _emptyDevice)
                    return;

                _videoModuleLogic.OnOperation("Snap");
            });

            _videoModuleLogic.ImageWrapper.OnUpdateVideoAction = (imageSource, isLive) => 
            {
                if (WaitControlVisibility == Visibility.Visible && isLive)
                    WaitControlVisibility = Visibility.Hidden;

                VideoImage = imageSource; 
            };
        }

        public ObservableCollection<IDeviceInfo> VideoDeviceItems { get; private set; }

        public ObservableCollection<VideoResolutionInfo> VideoResolutionItems { get; private set; }

        public ObservableCollection<IDeviceInfo> AudioDeviceItems { get; private set; }

        public ICommand PlayCommand { get; private set; }
        public ICommand SnapCommand { get; private set; }

        private IDeviceInfo _selectedVideoDevice;
        public IDeviceInfo SelectedVideoDevice
        {
            get { return _selectedVideoDevice; }
            set
            {
                SetProperty(ref _selectedVideoDevice, value);
                if (_selectedVideoDevice != _emptyDevice)
                {
                    WaitControlVisibility = Visibility.Visible;
                    _videoModuleLogic.OnActivate(_selectedVideoDevice);
                    UpdateResolutions(_selectedVideoDevice);
                }
                else
                {
                    WaitControlVisibility = Visibility.Hidden;
                    _videoModuleLogic.OnClose(_selectedVideoDevice);
                }
            }
        }

        private IDeviceInfo _selectedAudioDevice;
        public IDeviceInfo SelectedAudioDevice
        {
            get { return _selectedAudioDevice; }
            set
            {
                SetProperty(ref _selectedAudioDevice, value);
            }
        }

        private VideoResolutionInfo _selectedVideoResolution;
        public VideoResolutionInfo SelectedVideoResolution
        {
            get { return _selectedVideoResolution; }
            set
            {
                SetProperty(ref _selectedVideoResolution, value);
            }
        }

        private ImageSource _videoImage = null;
        public ImageSource VideoImage
        {
            get { return _videoImage; }
            set { _videoImage = value; OnPropertyChanged(); }
        }

        private bool _isFlipHorizontally = true;
        public bool IsFlipHorizontally
        {
            get { return _isFlipHorizontally; }
            set 
            { 
                SetProperty(ref _isFlipHorizontally, value); 
                _videoModuleLogic.SetFlipHorizontally(_isFlipHorizontally); 
            }
        }

        private Visibility _waitControlVisibility = Visibility.Hidden;
        public Visibility WaitControlVisibility
        {
            get { return _waitControlVisibility; }
            set { SetProperty(ref _waitControlVisibility, value); }
        }

        private string _operationString = "Start";
        public string OperationString
        {
            get { return _operationString; }
            set { SetProperty(ref _operationString, value); }
        }

        private string _formatString = string.Empty;
        public string FormatString
        {
            get { return _formatString; }
            set { SetProperty(ref _formatString, value); }
        }

        private Visibility _recordIconVisibility = Visibility.Hidden;
        public Visibility RecordIconVisibility
        {
            get { return _recordIconVisibility; }
            set { SetProperty(ref _recordIconVisibility, value); }
        }

        private void UpdateResolutions(IDeviceInfo device)
        {
            List<VideoResolutionInfo> list = VideoModuleLogic.GetVideoFormats(device);
            VideoResolutionItems.Clear();
            foreach (var item in list)
            {
                VideoResolutionItems.Add(item);
            }
            if(list.Count > 0)
                SelectedVideoResolution = VideoResolutionItems[0];
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
