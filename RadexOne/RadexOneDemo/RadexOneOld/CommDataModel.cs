using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RadexOneDemo.Old
{
    public class CommDataModel
    {
        public static byte[] Serialize<T>(T data) where T : struct
        {
            int size = Marshal.SizeOf(data);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(data, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        public static T Deserialize<T>(byte[] array) where T : struct
        {
            T str = new T();

            int size = Marshal.SizeOf(str);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            //set zeroes
            byte [] zeros = new byte[size];
            Marshal.Copy(zeros, 0, ptr, size);

            //copy data
            int len = Math.Min(size, array.Length);
            Marshal.Copy(array, 0, ptr, len);

            str = (T)Marshal.PtrToStructure(ptr, str.GetType());
            Marshal.FreeHGlobal(ptr);

            return str;
        }

        public static string StrructToString<T>(T data) where T : struct
        {
            byte[] bytes = Serialize<T>(data);
            string result = "";
            foreach (byte b in bytes)
            {
                result += b.ToString("X2") + " ";
            }
            return result;
        }

        public static string BytesToString(byte [] bytes) 
        {
            string result = "";
            foreach (byte b in bytes)
            {
                result += b.ToString("X2") + " ";
            }
            return result;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Request
    {
        public UInt16 o00_header;
        public byte o01_b1, o02_b2, o03_b3, o04_b4;
        public UInt32 o05_index0;
        public UInt16 o06_index1;
        public UInt16 o07_chk2;
        public UInt16 o08_chk3;
        public UInt16 o09_footer;
        public byte o10_b1;
        public byte o10_b2;
        public byte o11_b1;
        public byte o11_b2;
        public byte o12_b1;
        public byte o12_b2;
    }

    public class RequestBase
    {
        public const uint MAX_GET = 94;
        public const uint MAX_SET = 88;

        public Request request;

        public RequestBase(uint cmdId, uint max) 
        {
            //"7BFF 20 00 06 00 07000000 5700 0008 0C00F3F7";
            request.o00_header = 65403; //7BFF
            request.o01_b1 = 0x20;
            request.o02_b2 = 0x00;
            request.o03_b3 = 0x06;
            request.o04_b4 = 0x00;
            request.o05_index0 = cmdId;
            request.o06_index1 = (ushort)(max - cmdId);
            if (cmdId > max)//overflow, skip FF = -1
                request.o06_index1--;

            Size = 18;
        }

        protected ushort Checksum()
        {
            return request.o09_footer = (ushort)(0xFFFF - (request.o07_chk2 + request.o08_chk3));
        }

        public byte [] ToByteArray()
        {
            return CommDataModel.Serialize<Request>(request);
        }

        public override string ToString()
        {
            return CommDataModel.StrructToString<Request>(request).Substring(0, Size*3);
        }

        public int Size { get; protected set; }
    }

    public class RequestVersion : RequestBase
    {
        public RequestVersion(uint cmdId) //F2FF - get version
             : base(cmdId, MAX_GET)
        {
            //"7BFF 20 00 06 00 08000000 5600 0100 
            //0C00 F2FF";
            request.o07_chk2 = 0x0001;          //0100
            request.o08_chk3 = 0x000C;          //0C00
            request.o09_footer = Checksum();    //F2FF = 65522
        }
    }

    public class RequestData : RequestBase
    {
        public RequestData(uint cmdId) //get data
            : base(cmdId, MAX_GET)
        {
            //"7BFF 20 00 06 00 07000000 5700 0008
            //0C00 F3F7";
            request.o07_chk2    = 0x0800;         //0008
            request.o08_chk3    = 0x000C;         //0C00
            request.o09_footer  = Checksum();     //F3F7
        }
    }

    public class RequestSettings : RequestBase
    {
        public RequestSettings(uint cmdId) //get settings
             : base(cmdId, MAX_GET)
        {
            //"7BFF 20 00 06 00 03000000 5B00 0108 0C00 F2F7";
            request.o07_chk2    = 0x0801;        //0108
            request.o08_chk3    = 0x000C;        //0C00
            request.o09_footer  = Checksum();    //F2F7
        }
    }

    public class RequestResetDose : RequestBase //FBF7 - reset dose
    {
        public RequestResetDose(uint cmdId) //FBF7 - reset dose
             : base(cmdId, MAX_GET)
        {
            //7B FF 20 00 06 00 13000000 4B00 
            //0308 0100 FB F7
            request.o07_chk2    = 0x0803;  //0308
            request.o08_chk3    = 0x0001;  //0100
            request.o09_footer  = Checksum(); //FBF7
        }
    }

    public class RequestConfigure : RequestBase
    {
        public RequestConfigure(uint cmdId, bool snd, bool vbr, double threshold) //EB9D - set settings
             : base(cmdId, MAX_SET)
        {
            if (threshold < 0.1)
                threshold = 0.1;
            threshold *= 100;

            //"7BFF 20 00 0C 00 1C000000 3C00 
            request.o03_b3 = 0x0C;
            //0208 0E00 0200 0000 025A EB9D";
            request.o07_chk2 = 2050;//0208
            request.o08_chk3 = 14;  //0E00
            request.o09_footer = 2; //0200
            request.o10_b1 = 0;
            request.o10_b2 = 0;
            request.o11_b1 = Get(snd, vbr); //02
            request.o11_b2 = (byte) threshold; 
            request.o12_b1 = (byte) (237 - request.o11_b1); //0xED - o11_b1
            request.o12_b2 = (byte) (247 - request.o11_b2); //0xF7 - o11_b2

            Size = 24;
        }

        private byte Get(bool snd, bool vbr)
        {
            byte b = 0;

            if (snd)
                b |= 0x02;
            if (vbr)
                b |= 0x01;

            return b;
        }
    }

    #region Test

    public class RequestBad2 : RequestBase //request last buffer(data or ver)
    {
        public RequestBad2(uint cmdId)
            : base(cmdId, MAX_GET)
        {
            request.o07_chk2 = 0x0802;      //0208
            request.o08_chk3 = 0x000C;      //0C00?
            request.o09_footer = Checksum();
        }
    }

    public class RequestBad51 : RequestBase
    {
        public RequestBad51(uint cmdId) //returns last data
            : base(cmdId, MAX_GET)
        {
            request.o07_chk2 = 0x0805;
            request.o08_chk3 = 0x0001;
            request.o09_footer = Checksum();
        }
    }

    public class RequestBad4 : RequestBase
    {
        public RequestBad4(uint cmdId) //get version2
            : base(cmdId, MAX_GET)
        {
            request.o07_chk2 = 0x0804;  //0408?
            request.o08_chk3 = 0x000C;  //0100, 0C00?
            request.o09_footer = Checksum();
        }
    }

    public class RequestBad5 : RequestBase
    {
        public RequestBad5(uint cmdId)
            : base(cmdId, MAX_GET)
        {
            request.o07_chk2 = 0x0805;
            request.o08_chk3 = 0x000C;
            request.o09_footer = Checksum();
        }
    }

    public class RequestTest : RequestBase
    {
        public RequestTest(uint cmdId)
            : base(cmdId, MAX_GET)
        {
            request.o07_chk2 = 0x0805;
            request.o08_chk3 = 0x0001;
            request.o09_footer = Checksum();
        }
    }

    #endregion

    public class ResponceBase
    {
        public byte[] _data = new byte[42];

        public uint cmdId { get { return GetUInt32(6); } }

        public uint o01_h1 { get { return GetUInt16(0); } }
        public uint o02_b1 { get { return GetUInt8(2); } }
        public uint o03_b2 { get { return GetUInt8(3); } }
        public uint o04_b3 { get { return GetUInt8(4); } }
        public uint o05_b4 { get { return GetUInt8(5); } }

        public ResponceBase(byte [] data, int size = 42)
        {
            Size = Math.Min(data.Length, size);
            Buffer.BlockCopy(data, 0, _data, 0, Size); //_data = data;
        }

        public int Size { get; }

        public bool IsValid
        {
            get
            {
                return o02_b1 == 32 && o03_b2 == 128;
            }
        }

        public byte GetUInt8(int offset)
        {
            return _data[offset];
        }

        public ushort GetUInt16(int offset)
        {
            byte[] buf16 = { _data[offset], _data[offset + 1] };
            UInt16 t16 = 0;
            t16 = (UInt16)(t16 | (buf16[1] & 0x00FF));
            t16 = (UInt16)(t16 << 8);
            t16 = (UInt16)(t16 | (buf16[0] & 0x00FF));
            return t16;
        }

        public uint GetUInt32(int offset)
        {
            byte[] buf32 = { _data[offset], _data[offset+1], _data[offset+2], _data[offset+3] };
            UInt32 t32 = 0;
            for (int i = 3; i > 0; i--)
            {
                t32 = t32 | ((UInt32)buf32[i] & 0x000000FF);
                t32 = (t32 << 8);
            }
            t32 = t32 | ((UInt32)buf32[0] & 0x000000FF);
            return t32;
        }

        public override string ToString()
        {
            return "-> " + cmdId.ToString("0000") + " -- ===>> -- " + CommDataModel.BytesToString(_data).Substring(0, Size*3);
        }
    }


    public class RadexSerialNumber
    {
        public uint year { get; set; }
        public uint month { get; set; }
        public uint day { get; set; }

        public uint verMajor { get; set; }
        public uint verMinor { get; set; }

        public uint snPart1 { get; set; }
        public uint snPart2 { get; set; }

        public static RadexSerialNumber ParseResponse(ResponceBase responce)
        {
            RadexSerialNumber sn = new RadexSerialNumber()
            {
                year = responce.GetUInt16(28),
                month = responce.GetUInt8(30),
                day = responce.GetUInt8(31),

                verMajor = responce.GetUInt8(32),
                verMinor = responce.GetUInt8(33),

                snPart1 = responce.GetUInt16(34),
                snPart2 = responce.GetUInt32(24)
            };
            return sn;
        }

        public static bool operator !=(RadexSerialNumber sn1, RadexSerialNumber sn2)
        {
            return !(sn1 == sn2);
        }

        public static bool operator ==(RadexSerialNumber sn1, RadexSerialNumber sn2)
        {
            if (IsNull(sn1) && IsNull(sn2))
                return true;
            if (IsNull(sn1) || IsNull(sn2))
                return false;

            return sn1.Equals(sn2);
        }

        public override string ToString()
        {
            string date = string.Format("{0:D2}{1:D2}{2:D2}", day, month, year);
            return string.Format("S/N {0}-{1:D4}-{2:D6}  v. {3}.{4}", date, snPart1, snPart2, verMajor, verMinor);
        }

        public override bool Equals(object obj)
        {
            RadexSerialNumber sn2 = obj as RadexSerialNumber;
            if (IsNull(sn2))
                return false;

            return
                year     == sn2.year     &&
                month    == sn2.month    &&
                day      == sn2.day      &&
                verMajor == sn2.verMajor &&
                verMinor == sn2.verMinor &&
                snPart1  == sn2.snPart1  &&
                snPart2  == sn2.snPart2;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private static bool IsNull(object obj)
        {
            return (obj == null);
        }
    }

}
