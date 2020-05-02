using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaybackSoundSwitch.ComObjects;
using PlaybackSoundSwitch.Device;

namespace PlaybackSoundSwitch.DeviceSwitch
{
    /// <summary>
    /// https://github.com/cdhunt/AudioSwitcher
    /// https://github.com/sirWest/AudioSwitch
    /// https://github.com/Belphemur/SoundSwitch
    /// </summary>
    public class SoundDeviceManager
    {
        public static bool IsSwitchDefaultDevice { get; set; } = true;
        public static bool SwitchForegroundProgram { get; set; } = false;

        /// <summary>
        /// Attempts to set active device to the specified name
        /// </summary>
        /// <param name="device"></param>
        public static bool SetActiveDevice(DeviceFullInfo device)
        {
            AudioSwitcher switch1 = AudioSwitcher.Instance;

            //Log.Information("Set Default device: {Device}", device);
            if (!IsSwitchDefaultDevice)
            {
                switch1.SwitchTo(device.Id, Role.Console);
                switch1.SwitchTo(device.Id, Role.Multimedia);
                if (SwitchForegroundProgram)
                {
                    switch1.ResetProcessDeviceConfiguration();
                    switch1.SwitchProcessTo(device.Id, Role.Console, device.DeviceType);
                    switch1.SwitchProcessTo(device.Id, Role.Multimedia, device.DeviceType);
                }
            }
            else
            {
                //Log.Information("Set Default Communication device: {Device}", device);
                switch1.SwitchTo(device.Id, Role.All);
                if (SwitchForegroundProgram)
                {
                    switch1.ResetProcessDeviceConfiguration();
                    switch1.SwitchProcessTo(device.Id, Role.All, device.DeviceType);
                }
            }
            return true;
        }
    }
}
