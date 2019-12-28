using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSDataParser.Tools
{
    public class BufferReader
    {
        private byte[] _buffer = null;

        public long Length { get; private set; } = 0;

        public long Position { get; set; } = -1;

        public BufferReader(byte [] buffer, long start = 0, long length = 0)
        {
            if (length == 0)
                length = buffer.Length;

            if (start >= buffer.Length)
                return;

            if (start + length >= buffer.Length)
                length = buffer.Length - start;

            _buffer = new byte[length];
            Array.Copy(buffer, start, _buffer, 0, length);

            Position = 0;
            Length = length;
        }

        public byte ReadByte()
        {
            byte b = _buffer[Position];
            Position++;
            return b;
        }

        public byte[] ReadBuffer(long length)
        {
            byte[] buffer = new byte[length];
            Array.Copy(_buffer, Position, buffer, 0, length);
            Position += length;
            return buffer;
        }

        public uint ReadUintLE()
        {
            uint n = ConvertToUintLE(_buffer, Position);
            Position += 4;
            return n;
        }

        public uint ReadUintBE()
        {
            uint n = ConvertToUintBE(_buffer, Position);
            Position += 4;
            return n;
        }

        public string ReadString(int characterCount)
        {
            byte[] str = ReadBuffer(characterCount);
            if (str != null)
                return Encoding.ASCII.GetString(str, 0, characterCount);
            return "";
        }

        public float ReadFloatLE()
        {
            float f = ConvertToFloatLE(_buffer, Position);
            Position += 4;
            return f;
        }

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
    }
}
