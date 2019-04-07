using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RadexOneDemo;

namespace sD
{
    public class RadexOneConnection
    {
        private readonly RadexCommands _commands = new RadexCommands();
        private readonly ComPortHelper _radexPort = new ComPortHelper();

        public Action<CommandGetData> DataReceived = (cmd) => { };
        public Action<CommandGetVersion> VerReceived = (cmd) => { };
        public Action<CommandGetSettings> CfgReceived = (cmd) => { };

        public bool IsOpen { get { return _radexPort.IsOpen; } }

        public Action<string> DisconnectEvent = (reason) => { };

        public bool Pause = false;

        private int _interval = 500;
        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                if (value > 30000)
                    _interval = 30000;
                if (value < 100)
                    _interval = 100;
                else
                    _interval = value;
            }
        }

        public RadexOneConnection()
        {
            _commands.DataReceived = (data) =>
            {
                DataReceived(data);
            };

            _commands.VerReceived = (cmd) => { VerReceived(cmd); };

            _commands.CfgReceived = (cfg) => { CfgReceived(cfg); };

            _radexPort.Port.DataReceived += ComPort_DataReceived;
        }

        public string PortName
        {
            get { return _radexPort.Port.PortName; }
        }

        public string Open(string comPort)
        {
            if (_radexPort.IsOpen && _radexPort.Port.PortName == comPort)
                return _radexPort.Port.PortName;

            string port = _radexPort.Open(comPort);
            StartConnectionThread(true);
            return port;
        }

        public void Close()
        {
            if (!_radexPort.IsOpen)
                return;

            OnDisconnected("");
        }

        public void SendRequestData()
        {
            SendRequest(new CommandGetData());
        }

        internal void SendRequestVer()
        {
            SendRequest(new CommandGetVersion());
        }

        internal void SendRequestGetSettings()
        {
            SendRequest(new CommandGetSettings());
        }

        internal void SendRequestSetSettings(bool snd, bool vbr, double threshold)
        {
            SendRequest(new CommandConfigure(snd, vbr, threshold));
        }

        internal void RequestResetDose()
        {
            SendRequest(new CommandRestDose());
        }

        internal void RequestTestCmd()
        {
            SendRequest(new CommandTest());
        }

        private void SendRequest(RadexCommandBase cmd)
        {
            _commands.AddCommand(cmd);

            try
            {
                byte[] req = cmd.request.ToByteArray();
                _radexPort.Write(req, 0, cmd.RequestSize);
            }
            catch (Exception err)
            {
                OnDisconnected("Request error: " + err.Message);
                throw;
            }
        }

        private bool _cancel = false;
        private void StartConnectionThread(bool bStart)
        {
            if (bStart)
            {
                _cancel = false;
                Thread t = new Thread(new ThreadStart(() =>
                {
                    while (!_cancel)
                    {
                        if (!Pause)
                        {
                            try
                            {
                                SendRequestData();
                            }
                            catch (Exception err)
                            {
                                Log.WriteLine("Exception in StartConnectionThread: "+err.Message);
                                _cancel = true;
                                break;
                            }
                        }
                        Thread.Sleep(_interval);
                    }
                }));
                t.Name = "RadexConnectionThread";
                t.IsBackground = true;
                t.Start();
            }
            else
            {
                _cancel = true;
            }
        }

        private void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    byte[] data = _radexPort.GetReceivedData();
                    if (data != null)
                        _commands.ProcessResponce(data);
                }
                catch (Exception err)
                {
                    OnDisconnected("Response error: " + err.Message);
                }
            });
        }

        private void OnDisconnected(string message)
        {
            StartConnectionThread(false);
            _radexPort.Close();
            Log.WriteLine("Disconnected: " + message);
            DisconnectEvent(message);
        }
    }

    public class RadexComPortDesc
    {
        public const string RADEX_ONE = "RADEX ONE";

        public string Desc;
        public string Port;

        public RadexComPortDesc(string OS_desc)
        {
            Desc = OS_desc;
            Port = OS_desc.Substring(RADEX_ONE.Length).Trim(')', '(', ' ');
        }

        public override string ToString()
        {
            return Desc;
        }

        #region Connected Port Info

        public static List<RadexComPortDesc> RadexPortInfos()
        {
            List<string> descriptions = PortNames(RadexComPortDesc.RADEX_ONE);
            List<RadexComPortDesc> radex_names = new List<RadexComPortDesc>();
            foreach (string desc in descriptions)
            {
                radex_names.Add(new RadexComPortDesc(desc));
            }
            return radex_names;
        }

        private static string RadexPortInfo(int idx)
        {
            List<RadexComPortDesc> radexPorts = RadexPortInfos();
            if (idx < radexPorts.Count)
                return radexPorts[idx].Port;
            return null;
        }

        private static bool RadexPortExists(string comPort)
        {
            List<RadexComPortDesc> radexPorts = RadexPortInfos();
            foreach (RadexComPortDesc radexPort in radexPorts)
            {
                if (radexPort.Port == comPort)
                    return true;
            }
            return false;
        }

        private static List<string> PortNames(string name)
        {
            List<string> names = new List<string>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_PnPEntity");
            var list = searcher.Get();
            foreach (ManagementObject queryObj in list)
            {
                object caption = queryObj["Caption"];
                if(caption == null)
                    continue;
                names.Add(caption.ToString());
            }

            return names.Where(n => n.Contains(name)).ToList();
        }

        #endregion
    }

    public class ComPortHelper
    {
        public readonly SerialPort Port = new SerialPort();

        //public string PortName { get; private set; }

        public volatile bool IsOpen = false;

        public string Open(string comPortName)
        {
            lock (Port)
            {
                try
                {
                    //if (!RadexComPortDesc.RadexPortExists(comPort))
                    //    throw new Exception("Device not connected: RADEX ONE");

                    //PortName = comPort;
                    if (IsOpen)
                    {
                        if (Port.PortName == comPortName)
                            return Port.PortName; //already open

                        Close();
                    }

                    Port.PortName = comPortName;
                    Port.BaudRate = 9600;
                    Port.DataBits = 8;
                    Port.StopBits = StopBits.One;
                    Port.Handshake = Handshake.None;
                    Port.Parity = Parity.None;

                    Port.Open();

                    IsOpen = true;
                    return comPortName;
                }
                catch (Exception err)
                {
                    //possible need to repair driver
                    Debug.WriteLine(err.ToString());
                    throw;
                }
            }
        }

        public void Close()
        {
            if (IsOpen)
            {
                IsOpen = false;

                //should be closed in thread to avoid dead lock
                Task.Run(() =>
                {
                    lock (Port)
                    {
                        try
                        {
                            if (Port.IsOpen)
                                Port.Close();
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine("Error closing port: "+err.Message);
                        }
                    }
                });
            }
        }

        public void Write(byte [] data, int offset, int count)
        {
            lock (Port)
            {
                Port.Write(data, offset, count);
            }
        }

        public byte[] GetReceivedData()
        {
            Thread.Sleep(10);
            lock (Port)
            {
                int offset = 0;
                byte[] recv = new byte[512];
                while (Port.BytesToRead != 0)
                {
                    offset += Port.Read(recv, offset, Port.BytesToRead);
                    Thread.Sleep(10);
                }

                if(offset > 0 && offset < 10) //if offset is too small
                {
                    Thread.Sleep(15);
                    while (Port.BytesToRead != 0)
                    {
                        offset += Port.Read(recv, offset, Port.BytesToRead);
                        Thread.Sleep(10);
                    }
                }

                if (offset == 0)
                    return null;

                //copy 
                byte[] ret = new byte[offset];
                Buffer.BlockCopy(recv, 0, ret, 0, offset);

                return ret;
            }
        }
    }

    public class AlertManager
    {
        public enum AlertState
        {
            CoolingDown = 0,
            Good = 1,
            Warning = 2,
            Alert = 3
        }

        public Action<AlertState> OnStateChanged = (state) => { };

        public int AlertCPM = 60;

        private AlertState _alertState = AlertState.Good;
        private double _lastDose = 1000.0;
        private uint _lastCPM = 10;

        public AlertState AlertInfo { get { return _alertState; } }

        public void AnalyseSignal(CommandGetData cmd)
        {
            if (_alertState == AlertState.Alert)
            {
                //if DOSE decreasing OR CPM cross threshold
                if (cmd.RATE < (_lastDose - (_lastDose/10.0)) || cmd.CPM < AlertCPM)
                {
                    _alertState = AlertState.CoolingDown;
                    if (cmd.CPM < AlertCPM)
                        _alertState = AlertState.Good;

                    OnStateChanged(_alertState);
                }
            }

            if (_alertState == AlertState.Good)
            {
                if (cmd.CPM >= AlertCPM * 0.75)
                {
                    _alertState = AlertState.Warning;
                    OnStateChanged(_alertState);
                }
            }

            if (_alertState == AlertState.CoolingDown)
            {
                if(cmd.RATE >= (_lastDose * 0.9))
                {
                    _alertState = AlertState.Warning;
                    OnStateChanged(_alertState);
                }
            }

            if (_alertState == AlertState.Warning) //just changed or was
            {
                if (cmd.CPM >= AlertCPM)
                {
                    _alertState = AlertState.Alert;
                    OnStateChanged(_alertState);
                }
                if (cmd.CPM < AlertCPM * 0.75)
                {
                    _alertState = AlertState.Good;
                    OnStateChanged(_alertState);
                }
            }

            _lastCPM = cmd.CPM;
            _lastDose = cmd.RATE;
        }
    }
}
