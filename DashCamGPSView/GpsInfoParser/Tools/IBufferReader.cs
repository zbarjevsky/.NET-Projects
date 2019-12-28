using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSDataParser.Tools
{
    public abstract class IBufferReader : IDisposable
    {
        public long Length { get; protected set; } = 0;

        public long Position { get; set; } = -1;

        public abstract byte ReadByte();

        public abstract byte[] ReadBuffer(long length);

        public uint ReadUintLE()
        {
            byte[] number = ReadBuffer(4);
            if (number != null)
            {
                uint u = ConvertToUintLE(number, 0);
                return u;
            }
            return 0;
        }

        public uint ReadUintBE()
        {
            byte[] number = ReadBuffer(4);
            if (number != null)
            {
                uint u = ConvertToUintBE(number, 0);
                return u;
            }
            return 0;
        }

        public float ReadFloatLE()
        {
            byte[] number = ReadBuffer(4);
            if (number != null)
            {
                float f = ConvertToFloatLE(number, 0);
                return f;
            }
            return 0;
        }

        public string ReadString(int characterCount)
        {
            byte[] str = ReadBuffer(characterCount);
            if (str != null)
                return Encoding.ASCII.GetString(str, 0, characterCount);
            return "";
        }

        public abstract void Dispose();

        #region Static Methods

        public static uint ConvertToUintBE(byte[] buff, long offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToUInt32(number, 0);
        }

        public static uint ConvertToUintLE(byte[] buff, long offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (!BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToUInt32(number, 0);
        }

        public static float ConvertToFloatLE(byte[] buff, long offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (!BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToSingle(number, 0);
        }

        #endregion    
    }
}
