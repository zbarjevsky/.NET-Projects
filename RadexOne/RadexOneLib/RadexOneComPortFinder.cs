using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MkZ.RadexOneLib
{
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

        //private static string RadexPortInfo(int idx)
        //{
        //    List<RadexComPortDesc> radexPorts = RadexPortInfos();
        //    if (idx < radexPorts.Count)
        //        return radexPorts[idx].Port;
        //    return null;
        //}

        //private static bool RadexPortExists(string comPort)
        //{
        //    List<RadexComPortDesc> radexPorts = RadexPortInfos();
        //    foreach (RadexComPortDesc radexPort in radexPorts)
        //    {
        //        if (radexPort.Port == comPort)
        //            return true;
        //    }
        //    return false;
        //}

        private static List<string> PortNames(string name)
        {
            List<string> names = new List<string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_PnPEntity");
                var list = searcher.Get();
                foreach (ManagementObject queryObj in list)
                {
                    object caption = queryObj["Caption"];
                    if (caption == null)
                        continue;
                    names.Add(caption.ToString());
                }
            }
            catch (Exception ex)
            {
                string[] ports = SerialPort.GetPortNames();
                Debug.WriteLine("PortNames: " + ex);
            }
            return names.Where(n => n.Contains(name)).ToList();
        }

        #endregion
    }

    public class RadexOneComPortFinder
    {
        public class PortInfo
        {
            public string PortName;
            public RadexOneSerialNumber SerialNumber;

            public PortInfo(string portName, RadexOneSerialNumber serialNumber)
            {
                PortName = portName;
                SerialNumber = serialNumber;
            }

            public override string ToString()
            {
                return string.Format("RadexOnePortInfo - Port: {0} SerialNumber: {1}", PortName, SerialNumber);
            }
        }

        /// <summary>
        /// WMI sometimes failed to operate - needed alternate version
        /// Go through all ports - query for RadexOne Version
        /// </summary>
        /// <returns></returns>
        public static List<string> Find()
        {
            //WMI find Radex port
            List<RadexComPortDesc> radexPorts = RadexComPortDesc.RadexPortInfos();
            if(radexPorts!= null && radexPorts.Count > 0)
                return radexPorts.Select(p => p.Port).ToList();

            //WMI did not work - find Radex port by connecting to each com port
            List<PortInfo> _vers = new List<PortInfo>();
            List<RadexOneConnection> _conns = new List<RadexOneConnection>();

            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                SerialPort sp = new SerialPort(ports[i]);
                Debug.WriteLine("##### PORT: "+sp.PortName);
                if (!sp.IsOpen)
                {
                    RadexOneConnection conn = new RadexOneConnection();
                    conn.VerReceived = (ver) => { _vers.Add(new PortInfo(conn.PortName, ver.SerialNumber)); Debug.WriteLine("****** VERSION: " + ver.SerialNumber + "   PORT: " + conn.PortName); };
                    _conns.Add(conn);

                    string s = conn.Open(sp.PortName);
                    try
                    {
                        conn.SendRequestVer();
                    }
                    catch (Exception)
                    {
                        conn.Close();
                        continue;
                    }

                    Thread.Sleep(200);
                    conn.Close();
                }
            }

            return _vers.Select(p => p.PortName).ToList();
        }
    }
}
