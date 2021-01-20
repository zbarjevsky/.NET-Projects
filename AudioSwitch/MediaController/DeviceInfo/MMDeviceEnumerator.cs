using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MarkZ.Timer;
using MkZ.Media.ComObjects;
using MkZ.Media.Device;
using MkZ.Media.Interfaces;
using MkZ.Media.Notifications;

namespace MkZ.Media
{
	public class MMDeviceEnumerator : IDisposable
	{
		IMMDeviceEnumerator _realEnumerator;
        MMNotificationClient _notificationClient;

        public Action<MMDevice> DefaultDeviceChanged
        {
            get { return _notificationClient.DefaultDeviceChanged; }
            set { _notificationClient.DefaultDeviceChanged = value; }
        }

        public Action<MMDevice, object> DevicesChanged
        {
            get { return _notificationClient.DevicesChanged; }
            set { _notificationClient.DevicesChanged = value; }
        }

        /// <summary>
        /// Creates a new MM Device Enumerator
        /// </summary>
        public MMDeviceEnumerator()
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				throw new NotSupportedException("This functionality is only supported on Windows Vista or newer.");
			}
			_realEnumerator = new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator;
            
            _notificationClient = new MMNotificationClient(this);
        }

        public static IReadOnlyCollection<DeviceFullInfo> CreateDeviceList(MMDeviceCollection collection)
        {
            var sortedDevices = new List<DeviceFullInfo>();
            foreach (var device in collection)
            {
                try
                {
                    MMDevice d = new MMDevice(device);
                    var deviceInfo = new DeviceFullInfo(d);
                    if (string.IsNullOrEmpty(deviceInfo.Name))
                    {
                        continue;
                    }

                    sortedDevices.Add(deviceInfo);
                }
                catch (Exception e)
                {
                    string id;
                    device.GetId(out id);
                    MkZ.Tools.Trace.Debug("Can't get name of device {0}, Error: {1}", id, e);
                    //throw;
                }
            }

            return sortedDevices.OrderBy(dev => dev.Name).ThenBy(dev => dev.FriendlyName).ToArray();
        }

        /// <summary>
        /// Enumerate Audio Endpoints
        /// </summary>
        /// <param name="dataFlow">Desired DataFlow</param>
        /// <param name="dwStateMask">State Mask</param>
        /// <returns>Device Collection</returns>
        public IReadOnlyCollection<DeviceFullInfo> EnumerateAudioEndPoints(EDataFlow dataFlow, DeviceState dwStateMask)
        {
            IMMDeviceCollection result;
            Marshal.ThrowExceptionForHR(_realEnumerator.EnumAudioEndpoints(dataFlow, dwStateMask, out result));
            return CreateDeviceList(new MMDeviceCollection(result));
        }

        /// <summary>
        /// Get Default Endpoint
        /// </summary>
        /// <param name="dataFlow">Data Flow</param>
        /// <param name="role">Role</param>
        /// <returns>Device</returns>
        public MMDevice GetDefaultAudioEndpoint(EDataFlow dataFlow, Role role)
        {
            IMMDevice device;
            Marshal.ThrowExceptionForHR(_realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out device));
            return new MMDevice(device);
        }

        /// <summary>
        /// Check to see if a default audio end point exists without needing an exception.
        /// </summary>
        /// <param name="dataFlow">Data Flow</param>
        /// <param name="role">Role</param>
        /// <returns>True if one exists, and false if one does not exist.</returns>
        public bool HasDefaultAudioEndpoint(EDataFlow dataFlow, Role role)
        {
            const int E_NOTFOUND = unchecked((int)0x80070490);

            IMMDevice device;
            int hresult = ((IMMDeviceEnumerator)_realEnumerator).GetDefaultAudioEndpoint(dataFlow, role, out device);
            if (hresult == 0x0)
            {
                Marshal.ReleaseComObject(device);
                return true;
            }
            if (hresult == E_NOTFOUND)
            {
                return false;
            }
            Marshal.ThrowExceptionForHR(hresult);
            return false;
        }

        /// <summary>
        /// Get device by ID
        /// </summary>
        /// <param name="id">Device ID</param>
        /// <returns>Device</returns>
        public MMDevice GetDevice(string id)
        {
            IMMDevice device;
            Marshal.ThrowExceptionForHR(_realEnumerator.GetDevice(id, out device));
            return new MMDevice(device);
        }

        /// <summary>
        /// Registers a call back for Device Events
        /// </summary>
        /// <param name="client">Object implementing IMMNotificationClient type casted as IMMNotificationClient interface</param>
        /// <returns></returns>
        public int RegisterEndpointNotificationCallback([In] [MarshalAs(UnmanagedType.Interface)] IMMNotificationClient client)
        {
            return _realEnumerator.RegisterEndpointNotificationCallback(client);
        }

        /// <summary>
        /// Unregisters a call back for Device Events
        /// </summary>
        /// <param name="client">Object implementing IMMNotificationClient type casted as IMMNotificationClient interface </param>
        /// <returns></returns>
        public int UnregisterEndpointNotificationCallback([In] [MarshalAs(UnmanagedType.Interface)] IMMNotificationClient client)
        {
            return _realEnumerator.UnregisterEndpointNotificationCallback(client);
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
                if(_notificationClient != null)
                {
                    _notificationClient.Dispose();
                    _notificationClient = null;
                }

                if (_realEnumerator != null)
                {
                    // although GC would do this for us, we want it done now
                    Marshal.ReleaseComObject(_realEnumerator);
                    _realEnumerator = null;
                }

            }
        }
    }
}
