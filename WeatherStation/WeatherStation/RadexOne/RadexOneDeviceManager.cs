using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using MkZ.RadexOne;
using MkZ.WPF;
using RadexOneLib;

namespace MkZ.Weather.RadexOne
{
    public class RadexOneDeviceManager : IDisposable
    {
        public const int CONNECTION_CHECK_TIMEOUT = 3000;

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

                AddPoint(pt);

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

            _radexDevice.Interval = 15000; //15 sec interval, start when connected to 1st port

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

                    int count = CONNECTION_CHECK_TIMEOUT / 100;
                    for (int i = 0; i < count; i++)
                    {
                        if (_cancel)
                            break;
                        Thread.Sleep(100); //wait 3 sec to check connection
                    }
                }
            });

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    AddPoint(new RadiationDataPoint());// "Resume"));
                    _radexDevice.Pause = false;
                    break;
                case PowerModes.StatusChange:
                    break;
                case PowerModes.Suspend:
                    _radexDevice.Pause = true;
                    AddPoint(new RadiationDataPoint());// "Sleep"));
                    break;
                default:
                    break;
            }
        }

        public void AddPoint(RadiationDataPoint pt)
        {
            lock (_history)
            {
                _history.Log.Add(pt);
                if (_history.Log.Count > 1024 * 1024) //max size 1M
                    _history.Log.RemoveAt(0);
            }
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
            if (_connectionCheckTask == null)
                return;

            _radexDevice.Close();

            _cancel = true;
            _connectionCheckTask?.Wait(CONNECTION_CHECK_TIMEOUT);
            _connectionCheckTask = null;
        }
    }
}
