using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZ.Media.ComObjects;
using MZ.Media.Device;

namespace MZ.Media.DeviceSwitch
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
        public static bool SetActiveDevice(string deviceId, EDataFlow deviceType)
        {
            AudioSwitcher switch1 = AudioSwitcher.Instance;

            //Log.Information("Set Default device: {Device}", device);
            if (!IsSwitchDefaultDevice)
            {
                switch1.SwitchTo(deviceId, Role.Console);
                switch1.SwitchTo(deviceId, Role.Multimedia);
                if (SwitchForegroundProgram)
                {
                    switch1.ResetProcessDeviceConfiguration();
                    switch1.SwitchProcessTo(deviceId, Role.Console, deviceType);
                    switch1.SwitchProcessTo(deviceId, Role.Multimedia, deviceType);
                }
            }
            else
            {
                //Log.Information("Set Default Communication device: {Device}", device);
                switch1.SwitchTo(deviceId, Role.All);
                if (SwitchForegroundProgram)
                {
                    switch1.ResetProcessDeviceConfiguration();
                    switch1.SwitchProcessTo(deviceId, Role.All, deviceType);
                }
            }
            return true;
        }
    }
}
