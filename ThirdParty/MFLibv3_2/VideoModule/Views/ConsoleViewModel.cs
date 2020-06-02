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
            public string Name => "Close";

            public string SymbolicName => "";
        }

        public ConsoleViewModel(VideoModuleLogic videoModuleLogic)
        {
            _videoModuleLogic = videoModuleLogic;

            IList<IDeviceInfo> deviceTable = MfDevice.GetCategoryDevices(CLSID.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
            deviceTable.Insert(0, new EmptyDevice());

            DeviceItems = new ObservableCollection<IDeviceInfo>(deviceTable);
            SelectedDevice = null;
            
            PlayCommand = new RelayCommand(o =>
            {
                if (SelectedDevice == null)
                    return;
                string op = o as string;

                _videoModuleLogic.OnOperation(op);
                    
                OperationString = op == "Start" ? "Stop" : "Start";
                RecordIconVisibility = (OperationString == "Stop") ? Visibility.Visible : Visibility.Hidden;
            });

            SnapCommand = new RelayCommand(op =>
            {
                if (SelectedDevice == null)
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

        public ObservableCollection<IDeviceInfo> DeviceItems { get; private set; }

        public ICommand PlayCommand { get; private set; }
        public ICommand SnapCommand { get; private set; }

        private IDeviceInfo _selectedDevice;
        public IDeviceInfo SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                SetProperty(ref _selectedDevice, value);
                if (_selectedDevice != null)
                {
                    WaitControlVisibility = Visibility.Visible;
                    _videoModuleLogic.OnActivate(_selectedDevice);
                }
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

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
