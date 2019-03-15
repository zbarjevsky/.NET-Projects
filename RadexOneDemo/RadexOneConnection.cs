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
        private readonly RadexComPort _radexPort = new RadexComPort();
        private readonly AlertManager _alertManager = new AlertManager();

        public Action<CommandGetData> DataReceived = (cmd) => { };
        public Action<CommandGetVersion> VerReceived = (cmd) => { };
        public Action<CommandGetSettings> CfgReceived = (cmd) => { };

        public Action LightOn = () => { };
        public Action LightOff = () => { };
        public bool IsAlertOn { get { return _alertManager.Alert; } }

        public bool IsOpen { get { return _radexPort.IsOpen; } }

        public Action<string> DisconnectEvent = (reason) => { };

        public int AlertCPM {
            get { return _alertManager.AlertCPM; }
            set { _alertManager.AlertCPM = value; }
        }

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
                _alertManager.AnalyseSignal(data);

                DataReceived(data);
            };

            _commands.VerReceived = (cmd) => { VerReceived(cmd); };

            _commands.CfgReceived = (cfg) => { CfgReceived(cfg); };

            _alertManager.LightOn = () => { LightOn(); };

            _alertManager.LightOff = () => { LightOff(); };

            _radexPort.Port.DataReceived += ComPort_DataReceived;
        }

        public string RadexPort
        {
            get { return _radexPort.RadexPortName; }
        }

        public string Open(string comPort)
        {
            if (_radexPort.IsOpen && _radexPort.RadexPortName == comPort)
                return _radexPort.RadexPortName;

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

        internal void SendRequestSetSettings(bool snd, bool vbr, double max)
        {
            SendRequest(new CommandConfigure(snd, vbr, max));
        }

        internal void RequestResetDose()
        {
            SendRequest(new CommandAAFF());
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
            try
            {
                byte[] data = _radexPort.GetReceivedData();
                if(data != null)
                    _commands.ProcessResponce(data);
            }
            catch (Exception err)
            {
                OnDisconnected("Response error: "+err.Message);
            }
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
    }

    public class RadexComPort
    {
        public readonly SerialPort Port = new SerialPort();

        public string RadexPortName { get; private set; }

        public bool IsOpen { get { return Port.IsOpen; } }

        public string OpenFirstConnected()
        {
            return Open(RadexPortInfo(0));
        }

        public string Open(string comPort)
        {
            lock (this)
            {
                try
                {
                    if (!RadexPortExists(comPort))
                        throw new Exception("Device not connected: RADEX ONE");

                    RadexPortName = comPort;
                    if (Port.IsOpen)
                    {
                        if (Port.PortName != RadexPortName)
                            Port.Close();
                        else
                            return RadexPortName; //already open
                    }

                    Port.PortName = RadexPortName;
                    Port.BaudRate = 9600;
                    Port.DataBits = 8;
                    Port.StopBits = StopBits.One;
                    Port.Handshake = Handshake.None;
                    Port.Parity = Parity.None;

                    Port.Open();

                    return RadexPortName;
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }

        public void Close()
        {
            if (Port.IsOpen)
            {
                //should be closed in thread to avoid dead lock
                Task.Run(() =>
                {
                    lock (this)
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
            lock (this)
            {
                Port.Write(data, offset, count);
            }
        }

        public byte[] GetReceivedData()
        {
            Thread.Sleep(10);
            lock (this)
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

        //private static List<string> GetPortDescriptions()
        //{
        //    using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
        //    {
        //        string[] portnames = SerialPort.GetPortNames();
        //        SerialPort p1 = new SerialPort(portnames[3]);
        //        var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
        //        var tList = (from comX in portnames join p in ports on comX equals p["DeviceID"].ToString() select comX + " - " + p["Caption"]).ToList();
        //        return tList;
        //    }
        //}

        public static List<string> PortNames(string name)
        {
            string[] portNames = SerialPort.GetPortNames();
            string sInstanceName = string.Empty;
            string sPortName = string.Empty;

            List<string> names = new List<string>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_PnPEntity");
            var list = searcher.Get();
            foreach (ManagementObject queryObj in list)
            {
                sInstanceName = queryObj["Caption"].ToString();
                names.Add(sInstanceName);
            }

            return names.Where(n => n.Contains(name)).ToList();
        }

        #endregion
    }

    public class AlertManager
    {
        public Action LightOn = () => { };
        public Action LightOff = () => { };

        public int AlertCPM = 60;

        private bool _lightOn = false;
        private double _lastDose = 1000.0;
        private uint _lastCPM = 10;

        public bool Alert { get { return _lightOn; } }

        public void AnalyseSignal(CommandGetData cmd)
        {
            if (_lightOn == false)
            {
                if (cmd.CPM > AlertCPM && cmd.CPM >= (_lastCPM + (AlertCPM/10))) //CPM increasing significally
                {
                    _lightOn = true;
                    LightOn();
                }
            }
            else if (_lightOn == true)
            {
                //if DOSE decreasing OR CPM cross threshold
                if (cmd.DOSE < (_lastDose-(_lastDose/10.0)) || cmd.CPM < (AlertCPM-5))
                {
                    _lightOn = false;
                    LightOff();
                }
            }
            _lastCPM = cmd.CPM;
            _lastDose = cmd.DOSE;
        }
    }
}
