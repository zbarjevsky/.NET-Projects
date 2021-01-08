using SDKTemplate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;


using BarometerBT.Utils;
using MkZ.Tools;

namespace BarometerBT.Bluetooth
{
    public class BTConnectionDirect : IDisposable
    {
        public static readonly Guid TX_POWER_UUID = new Guid("00001804-0000-1000-8000-00805f9b34fb");
        public static readonly Guid TX_POWER_LEVEL_UUID = new Guid("00002a07-0000-1000-8000-00805f9b34fb");
        public static readonly Guid CCCD = new Guid("00002902-0000-1000-8000-00805f9b34fb");
        public static readonly Guid FIRMWARE_REVISON_UUID = new Guid("00002a26-0000-1000-8000-00805f9b34fb");
        public static readonly Guid DIS_UUID = new Guid("0000180a-0000-1000-8000-00805f9b34fb");
        public static readonly Guid RX_SERVICE_UUID = new Guid("6e400001-b5a3-f393-e0a9-e50e24dcca9e");
        public static readonly Guid RX_CHAR_UUID = new Guid("6e400002-b5a3-f393-e0a9-e50e24dcca9e");
        public static readonly Guid TX_CHAR_UUID = new Guid("6e400003-b5a3-f393-e0a9-e50e24dcca9e");

        private BluetoothLEDevice bluetoothLeDevice = null;
        GattSession mBluetoothGatt = null;

        public List<GattDeviceService> _services = new List<GattDeviceService>();

        #region Error Codes
        readonly int E_BLUETOOTH_ATT_WRITE_NOT_PERMITTED = unchecked((int)0x80650003);
        readonly int E_BLUETOOTH_ATT_INVALID_PDU = unchecked((int)0x80650004);
        readonly int E_ACCESSDENIED = unchecked((int)0x80070005);
        readonly int E_DEVICE_NOT_AVAILABLE = unchecked((int)0x800710df); // HRESULT_FROM_WIN32(ERROR_DEVICE_NOT_AVAILABLE)
        #endregion

        //https://docs.microsoft.com/en-us/windows/uwp/devices-sensors/gatt-client
        //C:\Dev_Mark\Temp\BlueMaestro\android-tempo-utility-sdk-current\Android-nRF-UART-master\app\src\main\java\com\bluemaestro\tempo_utility\UartService.java
        public BTConnectionDirect()
        {
        }

        public async void Connect(string id)
        {
            try
            {
                // BT_Code: BluetoothLEDevice.FromIdAsync must be called from a UI thread because it may prompt for consent.
                bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(id);

                if (bluetoothLeDevice == null)
                {
                    Log.d("Failed to connect to device.", NotifyType.ErrorMessage);
                    return;
                }

                bluetoothLeDevice.ConnectionStatusChanged += ConnectionStatusChangedHandler;

                mBluetoothGatt = await GattSession.FromDeviceIdAsync(bluetoothLeDevice.BluetoothDeviceId);
                mBluetoothGatt.MaintainConnection = true;
            }
            catch (Exception ex) when (ex.HResult == E_DEVICE_NOT_AVAILABLE)
            {
                Log.d("Bluetooth radio is not on.", NotifyType.ErrorMessage);
                return;
            }

            if (bluetoothLeDevice != null)
            {
                // Note: BluetoothLEDevice.GattServices property will return an empty list for unpaired devices. For all uses we recommend using the GetGattServicesAsync method.
                // BT_Code: GetGattServicesAsync returns a list of all the supported services of the device (even if it's not paired to the system).
                // If the services supported by the device are expected to change during BT usage, subscribe to the GattServicesChanged event.
                GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesForUuidAsync(RX_SERVICE_UUID);

                if (result.Status == GattCommunicationStatus.Success)
                {
                    _services.Clear();
                    _services.AddRange(result.Services);

                    Log.d(String.Format("Found {0} services", _services.Count), NotifyType.StatusMessage);
                    foreach (var service in _services)
                    {
                        Log.d("SERVICE: " + DisplayHelpers.GetServiceName(service));
                        GetCharachteristics(service);
                    }
                }
                else
                {
                    Log.d("Device unreachable", NotifyType.ErrorMessage);
                }
            }
        }

        public void Disconnect()
        {
            Dispose();
        }

        private void ConnectionStatusChangedHandler(BluetoothLEDevice sender, object args)
        {
            Log.d("ConnectionStatusChangedHandler, status {0}", sender.DeviceAccessInformation.CurrentStatus);
        }

        private async void GetCharachteristics(GattDeviceService service)
        {
            List<GattCharacteristic> characteristics = null;
            try
            {
                // Ensure we have access to the device.
                var accessStatus = await service.RequestAccessAsync();
                if (accessStatus == DeviceAccessStatus.Allowed)
                {
                    var result1 = await service.GetCharacteristicsForUuidAsync(TX_CHAR_UUID);
                    if (result1.Status == GattCommunicationStatus.Success)
                    {
                        characteristics = new List<GattCharacteristic>(result1.Characteristics);
                        GattCharacteristic txChar = characteristics[0];

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                // On error, act as if there are no characteristics.
                characteristics = new List<GattCharacteristic>();
            }
        }

        public void Dispose()
        {
            if(mBluetoothGatt != null)
            {
                mBluetoothGatt.MaintainConnection = false;
                mBluetoothGatt.Dispose();
                mBluetoothGatt = null;
            }

            if (bluetoothLeDevice != null)
            {
                bluetoothLeDevice.Dispose();
                bluetoothLeDevice = null;
            }
        }
    }
}
