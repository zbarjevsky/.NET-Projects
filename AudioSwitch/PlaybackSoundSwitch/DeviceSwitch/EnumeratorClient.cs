using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class EnumeratorClient : IDisposable
    {
        private MMDeviceEnumerator _enumerator;

        public EnumeratorClient()
        {
            _enumerator = new MMDeviceEnumerator();
        }

        ~EnumeratorClient()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_enumerator != null)
            {
                _enumerator.Dispose();
                _enumerator = null;
            }
        }

        public Action<MMDevice> DefaultDeviceChanged
        {
            get { return _enumerator.DefaultDeviceChanged; }
            set { _enumerator.DefaultDeviceChanged = value; }
        }

        public Action<MMDevice, object> DevicesChanged
        {
            get { return _enumerator.DevicesChanged; }
            set { _enumerator.DevicesChanged = value; }
        }

        public bool IsDefault(string deviceId, EDataFlow flow, Role role)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return false;

            if (role == Role.All)
            {
                var result = true;
                result &= IsDefault(deviceId, flow, Role.Communications);
                result &= IsDefault(deviceId, flow, Role.Console);
                result &= IsDefault(deviceId, flow, Role.Multimedia);

                return result;
            }

            try
            {
                MMDevice defaultDevice = _enumerator.GetDefaultAudioEndpoint((EDataFlow) flow, (Role) role);
                return deviceId == defaultDevice.ID;
            }
            catch (Exception)
            {
                //Happens if there is no default device for the given Data Flow and/or role
                // See issue #401
                return false;
            }
        }

        /// <summary>
        /// Enumerate Audio Endpoints
        /// </summary>
        /// <param name="dataFlow">Desired DataFlow</param>
        /// <param name="dwStateMask">State Mask</param>
        /// <returns>Device Collection</returns>
        public IReadOnlyCollection<DeviceFullInfo> EnumerateAudioEndPoints(EDataFlow dataFlow, DeviceState dwStateMask)
        {
            return _enumerator.EnumerateAudioEndPoints(dataFlow, dwStateMask);
        }

        /// <summary>
        /// Get Default Endpoint
        /// </summary>
        /// <param name="dataFlow">Data Flow</param>
        /// <param name="role">Role</param>
        /// <returns>Device</returns>
        public MMDevice GetDefaultAudioEndpoint(EDataFlow dataFlow, Role role)
        {
            if (_enumerator.HasDefaultAudioEndpoint(dataFlow, role))
                return _enumerator.GetDefaultAudioEndpoint(dataFlow, role);
            return null;
        }

        //[ComImport, Guid(ComGuid.AUDIO_IMMDEVICE_ENUMERATOR_OBJECT_IID)]
        //private class _MMDeviceEnumerator
        //{
        //}
    }
}