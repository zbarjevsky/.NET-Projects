using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MkZ.RadexOne;
using MkZ.WPF;
using RadexOneLib;

namespace MkZ.Weather.RadexOne
{
    public class RadexOneDeviceManager : IDisposable
    {
        private readonly RadexOneConnection _radexDevice = new RadexOneConnection();
        private List<RadexComPortDesc> _radexPorts = new List<RadexComPortDesc>();
        private readonly RadexOneConfig _radexConfig = new RadexOneConfig();
        private Task _connectionCheckTask;
        private volatile bool _cancel = false;

        private readonly RadiationLog _history = new RadiationLog();

        public Action<RadiationDataPoint> OnDataReceived = (pt) => { };
        public Action<string> OnDisconnect = (reason) => { };
        public Action<RadexOneConfig> OnCfgReceived = (config) => { };
        public Action<RadexSerialNumber> OnVerReceived = (serialNumber) => { };

        public void Init()
        {
            _radexDevice.DataReceived = (cmd) =>
            {
                if (cmd.CPM == 0 || cmd.RATE == 0)
                    return;

                RadiationDataPoint pt = new RadiationDataPoint()
                {
                    CPM = cmd.CPM,
                    RATE = cmd.RATE,
                    DOSE = cmd.DOSE,
                    Threshold = 60 // (double)m_numMaxCPM.Value
                };

                _history.Log.Add(pt);
                if (_history.Log.Count > 1024 * 1024) //max size 1M
                    _history.Log.RemoveAt(0);

                WPFUtils.ExecuteOnUiThreadInvoke(() =>
                {
                    OnDataReceived(pt);
                });
            };

            _radexDevice.VerReceived = (cmd) =>
            {
                WPFUtils.ExecuteOnUiThreadInvoke(() =>
                {
                    _radexConfig.SetVersion(cmd);
                    OnVerReceived?.Invoke(cmd.SerialNumber);
                });
            };

            _radexDevice.CfgReceived = (cmd) =>
            {
                WPFUtils.ExecuteOnUiThreadInvoke(() =>
                {
                    _radexConfig.SetSettings(cmd);
                    OnCfgReceived?.Invoke(_radexConfig);
                });
            };

            _radexDevice.DisconnectEvent = (reason) =>
            {
                WPFUtils.ExecuteOnUiThreadInvoke(() =>
                {
                    OnDisconnect?.Invoke(reason);
                });
            };

            _radexDevice.Interval = 3000; //3 sec interval, start when connected to 1st port

            _connectionCheckTask = Task.Run(() =>
            {
                while (!_cancel)
                {
                    try
                    {
                        _radexPorts = RadexComPortDesc.RadexPortInfos();

                        bool autoConnect = true; // WPFUtils.ExecuteOnUiThreadInvoke(() => { return m_chkAutoConnect.Checked; });
                        if (autoConnect)
                            ConnectToSelectedPort(_radexPorts.FirstOrDefault());
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine(err.ToString());
                    }

                    Thread.Sleep(1000);
                }
            });
        }

        public List<RadiationDataPoint> GetLog()
        {
            return _history.Log;
        }

        private void ConnectToSelectedPort(RadexComPortDesc selectedDevice)
        {
            WPFUtils.ExecuteOnUiThreadInvoke(() =>
            {
                if (selectedDevice != null)
                {
                    if (_radexDevice.IsOpen && selectedDevice.Port == _radexDevice.PortName)
                        return;

                    try
                    {
                        //connect to port and start sampling thread
                        _radexDevice.Open(selectedDevice.Port);
                    }
                    catch (Exception err)
                    {
                        Log.WriteLine("Open exception: " + err.Message);
                    }
                }
                else
                {
                    //m_lblConnectStatus.Text = "Device Not Connected";
                }
            });
        }

        public void Dispose()
        {
            _cancel = true;
            _connectionCheckTask?.Wait(2000);
            _connectionCheckTask = null;
        }
    }
}
