using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MkZ.RadexOneLib
{
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
        public static List<PortInfo> Find()
        {
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

            return _vers;
        }
    }
}
