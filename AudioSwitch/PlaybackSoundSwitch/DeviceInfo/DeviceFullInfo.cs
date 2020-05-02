//using NAudio.CoreAudioApi;
using PlaybackSoundSwitch.Device;
//using SoundSwitch.Common.Framework.Audio.Icon;

namespace PlaybackSoundSwitch.Device
{
    public class DeviceFullInfo : DeviceInfo
    {
        public string IconPath { get; }
        public EDeviceState State { get; }

        public System.Drawing.Icon LargeIcon => AudioDeviceIconExtractor.ExtractIconFromPath(IconPath, Type, true);
        public System.Drawing.Icon SmallIcon => AudioDeviceIconExtractor.ExtractIconFromPath(IconPath, Type, false);

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