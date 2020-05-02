using System;
using System.Runtime.InteropServices;
//using AudioSwitcher.AudioApi.Hooking.ComObjects;
//using NAudio.CoreAudioApi;
using PlaybackSoundSwitch;
using PlaybackSoundSwitch.ComObjects;
using PlaybackSoundSwitch.Device;
//using SoundSwitch.Audio.Manager.Interop.Enum;
using SoundSwitch.Audio.Manager.Interop.Interface;

namespace PlaybackSoundSwitch.DeviceSwitch
{
    internal class EnumeratorClient
    {
        private readonly MMDeviceEnumerator _enumerator;

        public EnumeratorClient()
        {
            _enumerator = new MMDeviceEnumerator();
        }

        ~EnumeratorClient()
        {
            _enumerator.Dispose();
        }

        public bool IsDefault(string deviceId, EDataFlow flow, Role role)
        {
            if (role == Role.Count)
            {
                var result = true;
                result &= IsDefault(deviceId, flow, Role.Communications);
                result &= IsDefault(deviceId, flow, Role.Console);
                result &= IsDefault(deviceId, flow, Role.Multimedia);

                return result;
            }

            try
            {
                var defaultDevice = _enumerator.GetDefaultAudioEndpoint((EDataFlow) flow, (Role) role);
                return deviceId == defaultDevice.ID;
            }
            catch (Exception)
            {
                //Happens if there is no default device for the given Data Flow and/or role
                // See issue #401
                return false;
            }
        }

        //[ComImport, Guid(ComGuid.AUDIO_IMMDEVICE_ENUMERATOR_OBJECT_IID)]
        //private class _MMDeviceEnumerator
        //{
        //}
    }
}