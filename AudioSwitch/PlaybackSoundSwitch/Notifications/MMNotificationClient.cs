using System;
using System.Threading.Tasks;
using MarkZ.Timer;
using PlaybackSoundSwitch.ComObjects;
using PlaybackSoundSwitch.Device;
using PlaybackSoundSwitch.Interfaces;

namespace PlaybackSoundSwitch.Notifications
{
    public class MMNotificationClient : IMMNotificationClient, IDisposable
    {
        private MMDeviceEnumerator _enumerator = null;
        private readonly DebounceDispatcher _dispatcher = new DebounceDispatcher();

        public Action<MMDevice> DefaultDeviceChanged = (device) => { };
        public Action<string, object> DevicesChanged = (deviceId, param) => { };

        public MMNotificationClient(MMDeviceEnumerator enumerator)
        {
            Register(enumerator);
        }

        /// <summary>
        /// Register the notification client in the Enumerator
        /// </summary>
        private void Register(MMDeviceEnumerator enumerator)
        {
            _enumerator = enumerator;
            _enumerator.RegisterEndpointNotificationCallback(this);
        }

        /// <summary>
        /// Unregister the notification client in the Enumerator
        /// </summary>
        private void UnRegister()
        {
            if (_enumerator != null)
            {
                _enumerator.UnregisterEndpointNotificationCallback(this);
                _enumerator = null;
            }
        }

        public void OnDeviceStateChanged(string deviceId, DeviceState newState)
        {
            _dispatcher.Debounce(300, (o) => DevicesChanged(deviceId, newState));
        }

        public void OnDeviceAdded(string deviceId)
        {
            _dispatcher.Debounce(300, (o) => DevicesChanged(deviceId, "Added"));
        }

        public void OnDeviceRemoved(string deviceId)
        {
            _dispatcher.Debounce(300, (o) => DevicesChanged(deviceId, "Removed"));
        }

        public void OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId)
        {
            var device = _enumerator.GetDevice(defaultDeviceId);
            _dispatcher.Debounce(300, (o) => DefaultDeviceChanged(device));
        }

        public void OnPropertyValueChanged(string deviceId, PropertyKey key)
        {
            if (PropertyKeys.PKEY_DEVICE_INTERFACE_FRIENDLY_NAME.formatId != key.formatId
                && PropertyKeys.PKEY_AUDIO_ENDPOINT_GUID.formatId != key.formatId
                && PropertyKeys.PKEY_DEVICE_ICON.formatId != key.formatId
                && PropertyKeys.PKEY_DEVICE_FRIENDLY_NAME.formatId != key.formatId
            )
            {
                return;
            }

            _dispatcher.Debounce(300, (o) => DevicesChanged(deviceId, key.formatId));
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Called to dispose/finalize contained objects.
        /// </summary>
        /// <param name="disposing">True if disposing, false if called from a finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnRegister();
            }
        }
    }
}