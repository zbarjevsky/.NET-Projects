using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadexOneDemo
{
    public class RadexCommands
    {
        public const int MAX_CMD = 0x200; 

        private RadexCommandBase [] commands = new RadexCommandBase[MAX_CMD];

        public Action<CommandGetData> DataReceived = (data) => { };
        public Action<CommandGetVersion> VerReceived = (ver) => { };
        public Action<CommandGetSettings> CfgReceived = (set) => { };

        private string _logFileName = string.Format(@"C:\Temp\Radex{0}.log", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

        public void AddCommand(RadexCommandBase cmd)
        {
            commands[cmd.cmdId] = cmd;
            Log.WriteLine("?? " + cmd);
        }

        private byte [] responce = new byte[512];
        public void ProcessResponce(byte[] data)
        {
            int offset = 0;
            while (offset < data.Length)
            {
                int size = Math.Min(42, data.Length - offset);
                Array.ConstrainedCopy(data, offset, responce, 0, size);

                ResponceBase r = new ResponceBase(responce);
                if (!r.IsValid)
                {
                    Log.WriteLine("Error responce: "+r);
                    //Debug.Assert(r.IsValid, "Error in responce");
                    break;
                }

                if (r.cmdId > 0 && r.cmdId < MAX_CMD)
                {
                    RadexCommandBase cmd = commands[r.cmdId];
                    offset += cmd.ResponseSizeExpected;
                    cmd.SetResponce(responce);
                    Log.WriteLine(cmd.responce.ToString());

                    if (cmd is CommandGetData)
                    {
                        DataReceived(cmd as CommandGetData);
                    }
                    if (cmd is CommandGetVersion)
                    {
                        VerReceived(cmd as CommandGetVersion);
                    }
                    if (cmd is CommandGetSettings)
                    {
                        CfgReceived(cmd as CommandGetSettings);
                    }
                }
                else //wrong input
                {
                    break;
                }
            }
        }
    }

    public abstract class RadexCommandBase
    {
        public RequestBase request;
        public ResponceBase responce;

        public static uint Id = 1;
        public uint cmdId = GetNextId();

        private static uint GetNextId()
        {
            if (Id >= RadexCommands.MAX_CMD)
                Id = 1;

            return Id++;
        }

        public virtual void SetResponce(byte[] data)
        {
            responce = new ResponceBase(data, ResponseSizeExpected);
        }

        public int RequestSize { get { return request.Size; } }

        //public int ResponseSize { get { return responce.Size; } }

        public abstract int ResponseSizeExpected { get; }

        public override string ToString()
        {
            return cmdId.ToString("0000") + " -- Base  -- " + request;
        }
    }

    public class CommandGetData : RadexCommandBase //request get data
    {
        public CommandGetData()
        {
            request = new RequestData(cmdId);
        }

        public override int ResponseSizeExpected { get { return 34; } }

        public override string ToString()
        {
            return cmdId.ToString("0000") + " -- Data  -- " + request;
        }

        public double RATE { get { return responce.GetUInt32(20) / 100.0; } }
        public double DOSE { get { return responce.GetUInt32(24) / 100.0; } }
        public uint CPM {  get { return responce.GetUInt32(28); } }
    }

    public class CommandGetVersion : RadexCommandBase //request get version == ping
    {
        public CommandGetVersion()
        {
            request = new RequestVersion(cmdId);
        }

        public override int ResponseSizeExpected { get { return 42; } }

        public override string ToString()
        {
            return cmdId.ToString("0000") + " --Version-- " + request.ToString();
        }

        public RadexSerialNumber SerialNumber
        {
            get
            {
                //uint year = responce.GetUInt16(28);
                //uint month = responce.GetUInt8(30);
                //uint day = responce.GetUInt8(31);

                //uint verMajor = responce.GetUInt8(32);
                //uint verMinor = responce.GetUInt8(33);

                //string sn1 = string.Format("{0:D2}{1:D2}{2:D2}", day, month, year);

                //uint sn2 = responce.GetUInt16(34);
                //uint sn3 = responce.GetUInt32(24);

                //return string.Format("S/N {0}-{1:D4}-{2:D6}  v. {3}.{4}", sn1, sn2, sn3, verMajor, verMinor);
                RadexSerialNumber sn = RadexSerialNumber.ParseResponse(responce);
                return sn;
            }
        }
    }

    public class CommandGetSettings : RadexCommandBase //request get config
    {
        public CommandGetSettings()
        {
            request = new RequestSettings(cmdId);
        }

        public override int ResponseSizeExpected
        {
            get { return 24; }
        }

        public override void SetResponce(byte[] data)
        {
            base.SetResponce(data);

            byte s1 = data[20];
            byte s2 = data[21];

            Sound = (s1 & 2) != 0;
            Vibrate = (s1 & 1) != 0;

            Threshold = s2/100.0;
            if (Threshold < 0.1)
                Threshold = 0.1;
        }

        public override string ToString()
        {
            return cmdId.ToString("0000") + " --ConfigG-- " + request;
        }

        public bool Sound { get; private set; }
        public bool Vibrate { get; private set; }
        public double Threshold { get; private set; }
    }

    public class CommandConfigure : RadexCommandBase //request set config
    {
        public CommandConfigure(bool snd, bool vbr, double threshold)
        {
            request = new RequestConfigure(cmdId, snd, vbr, threshold);
        }

        public override int ResponseSizeExpected { get { return 18; } }

        public override string ToString()
        {
            return cmdId.ToString("0000") + " --ConfigS-- " + request;
        }
    }

    public class CommandResetDose : RadexCommandBase //request reset dose
    {
        public CommandResetDose()
        {
            request = new RequestResetDose(cmdId);
        }

        public override int ResponseSizeExpected { get { return 18; } }

        public override string ToString()
        {
            return cmdId.ToString("0000") + " -- Reset -- " + request;
        }
    }

    public class Command0x0802 : RadexCommandBase //request ??
    {
        public Command0x0802()
        {
            request = new RequestBad2(cmdId);
        }

        public override int ResponseSizeExpected { get { return 42; } }

        public override string ToString()
        {
            return cmdId.ToString("0000") + " --0x0802 -- " + request;
        }
    }

    public class CommandTest : RadexCommandBase //request ??
    {
        public CommandTest()
        {
            request = new RequestTest(cmdId);
        }

        public override int ResponseSizeExpected { get { return 42; } }

        public override string ToString()
        {
            return cmdId.ToString("0000") + " -- Test> -- " + request;
        }
    }
}
