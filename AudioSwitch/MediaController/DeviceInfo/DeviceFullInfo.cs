﻿//using NAudio.CoreAudioApi;
using MkZ.Media.Device;
//using SoundSwitch.Common.Framework.Audio.Icon;

namespace MkZ.Media.Device
{
    public class DeviceFullInfo : DeviceInfo
    {
        public string IconPath { get; }
        public EDeviceState State { get; }

        public System.Drawing.Icon LargeIcon => AudioDeviceIconExtractor.ExtractIconFromPath(IconPath, DeviceType, true);
        public System.Drawing.Icon SmallIcon => AudioDeviceIconExtractor.ExtractIconFromPath(IconPath, DeviceType, false);

        public DeviceFullInfo(string name, string id, EDataFlow type, string iconPath, EDeviceState state) : base(name, id, type)
        {
            IconPath = iconPath;
            State = state;
        }

        public DeviceFullInfo(MMDevice device) : base(device)
        {
            IconPath = device.IconPath;
            State = device.State;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}