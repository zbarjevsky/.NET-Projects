using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using MkZ.Tools;
using MkZ.WPF;

namespace MkZ.RadexOneLib
{
    public class RadexOneDeviceManager : IDisposable
    {
        public const int CONNECTION_CHECK_TIMEOUT = 3000;

        private readonly RadexOneConnection _radexDevice = new RadexOneConnection();
        private List<RadexOneComPortFinder.PortInfo> _radexPorts = new List<RadexOneComPortFinder.PortInfo>();
        private readonly RadexOneDeviceInfo _radexConfig = new RadexOneDeviceInfo();
        private Task _connectionCheckTask;
        private volatile bool _cancel = false;

        public Action<RadexOneDeviceInfo, RadiationDataPoint> OnDataReceived = (dev, pt) => { };
        public Action<RadexOneDeviceInfo, string> OnDisconnect = (dev, reason) => { };
        public Action<RadexOneDeviceInfo> OnCfgReceived = (dev) => { };
        public Action<RadexOneDeviceInfo> OnVerReceived = (dev) => { };

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

                WPFUtils.ExecuteOnUiThreadInvoke(() =>
                {
                    OnDataReceived?.Invoke(_radexConfig, pt);
                });
            };

            _radexDevice.VerReceived = (cmd) =>
            {
                WPFUtils.ExecuteOnUiThreadInvoke(() =>
                {
                    _radexConfig.SetVersion(cmd);
                    OnVerReceived?.Invoke(_radexConfig);
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
                    OnDisconnect?.Invoke(_radexConfig, reason);
                });
            };

            _radexDevice.Interval = 15000; //15 sec interval, start when connected to 1st port

            _connectionCheckTask = Task.Run(() =>
            {
                while (!_cancel)
                {
                    try
                    {
                        //_radexPorts = RadexComPortDesc.RadexPortInfos();

                        if (!_radexDevice.IsOpen)
                        {
                            _radexPorts = RadexOneComPortFinder.Find();

                            bool autoConnect = true; // WPFUtils.ExecuteOnUiThreadInvoke(() => { return m_chkAutoConnect.Checked; });
                            if (autoConnect)
                                ConnectToSelectedPort(_radexPorts.FirstOrDefault());
                        }
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
                    OnDataReceived?.Invoke(_radexConfig, new RadiationDataPoint());// "Resume"));
                    _radexDevice.Pause = false;
                    break;
                case PowerModes.StatusChange:
                    break;
                case PowerModes.Suspend:
                    _radexDevice.Pause = true;
                    OnDataReceived?.Invoke(_radexConfig, new RadiationDataPoint());// "Sleep"));
                    break;
                default:
                    break;
            }
        }

        private void ConnectToSelectedPort(RadexOneComPortFinder.PortInfo selectedDevice)
        {
            WPFUtils.ExecuteOnUiThreadInvoke(() =>
            {
                if (selectedDevice != null)
                {
                    if (_radexDevice.IsOpen && selectedDevice.PortName == _radexDevice.PortName)
                        return;

                    _radexConfig.SerialNumber = selectedDevice.SerialNumber.Clone();

                    try
                    {
                        //connect to port and start sampling thread
                        _radexDevice.Open(selectedDevice.PortName);
                    }
                    catch (Exception err)
                    {
                        Log.e("RadexOneDeviceManager::ConnectToSelectedPort::exception: " + err.Message);
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
