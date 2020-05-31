using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
                ButtonShown = (OperationString == "Stop");
            });

            SnapCommand = new RelayCommand(op =>
            {
                if (SelectedDevice == null)
                    return;

                _videoModuleLogic.OnOperation("Snap");
            });

            _videoModuleLogic.ImageWrapper.OnUpdateVideoAction = (source) => { VideoImage = source; };
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
                if (_selectedDevice!=null)
                    _videoModuleLogic.OnActivate(_selectedDevice);
            }
        }

        private ImageSource _videoImage = null;
        public ImageSource VideoImage
        {
            get { return _videoImage; }
            set { _videoImage = value; OnPropertyChanged(); }
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

        private bool _buttonShown;
        public bool ButtonShown
        {
            get { return _buttonShown; }
            set { SetProperty(ref _buttonShown, value); }
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
