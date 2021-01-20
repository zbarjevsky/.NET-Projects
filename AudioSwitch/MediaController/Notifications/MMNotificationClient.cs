using System;
using System.Threading.Tasks;
using MarkZ.Timer;
using MkZ.Media.ComObjects;
using MkZ.Media.Device;
using MkZ.Media.Interfaces;

namespace MkZ.Media.Notifications
{
    public class MMNotificationClient : IMMNotificationClient, IDisposable
    {
        private MMDeviceEnumerator _enumerator = null;
        private readonly DebounceDispatcher _dispatcher = new DebounceDispatcher();

        public Action<MMDevice> DefaultDeviceChanged = (device) => { };
        public Action<MMDevice, object> DevicesChanged = (device, param) => { };

        public MMNotificationClient(MMDeviceEnumerator enumerator)
        {
            Register(enumerator);
        }

        /// <summary>
        /// Register the notification client in the Enumerator
        /// </summary>
        private void Register(MMDeviceEnumerator enumerator)
        {
            UnRegister();
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
            if (string.IsNullOrWhiteSpace(deviceId))
                return;

            var device = _enumerator.GetDevice(deviceId);
            _dispatcher.Debounce(300, (o) => DevicesChanged(device, newState));
        }

        public void OnDeviceAdded(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return;

            var device = _enumerator.GetDevice(deviceId);
            _dispatcher.Debounce(300, (o) => DevicesChanged(device, "Added"));
        }

        public void OnDeviceRemoved(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return;

            var device = _enumerator.GetDevice(deviceId);
            _dispatcher.Debounce(300, (o) => DevicesChanged(device, "Removed"));
        }

        public void OnDefaultDeviceChanged(DataFlow flow, Role role, string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return;

            MMDevice device = _enumerator.GetDevice(deviceId);
            _dispatcher.Debounce(300, (o) => DefaultDeviceChanged(device));
        }

        public void OnPropertyValueChanged(string deviceId, PropertyKey key)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return;

            if (PropertyKeys.PKEY_DEVICE_INTERFACE_FRIENDLY_NAME.formatId != key.formatId
                && PropertyKeys.PKEY_AUDIO_ENDPOINT_GUID.formatId != key.formatId
                && PropertyKeys.PKEY_DEVICE_ICON.formatId != key.formatId
                && PropertyKeys.PKEY_DEVICE_FRIENDLY_NAME.formatId != key.formatId
            )
            {
                return;
            }

            var device = _enumerator.GetDevice(deviceId);
            _dispatcher.Debounce(300, (o) => DevicesChanged(device, key.formatId));
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