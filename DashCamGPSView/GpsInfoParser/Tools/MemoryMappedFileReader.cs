using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSDataParser
{
    public class MemoryMappedFileReader : IDisposable
    {
        private MemoryMappedFile _mmf = null;
        private MemoryMappedViewAccessor _mma = null;

        private long _index = -1;

        public long Length { get; private set; }

        public long Position { get { return _index; } set { _index = value; } }

        public MemoryMappedFileReader(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            Length = info.Length;

            _mmf = MemoryMappedFile.CreateFromFile(fileName, FileMode.Open);
            _mma = _mmf.CreateViewAccessor();

            _index = 0;
        }

        public byte ReadByte()
        {
            byte b = _mma.ReadByte(_index);
            _index++;
            return b;
        }

        public byte [] ReadBuffer(int length)
        {
            byte[] buffer = new byte[length];
            int count = _mma.ReadArray<byte>(_index, buffer, 0, length);
            _index += count;
            if (count == length)
                return buffer;
            return null;
        }

        public uint ReadUintLE()
        {
            uint i = _mma.ReadUInt32(_index);
            _index += 4;
            return i;
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

        public string ReadString(int characterCount)
        {
            byte[] str = ReadBuffer(characterCount);
            if(str != null)
                return Encoding.ASCII.GetString(str, 0, characterCount);
            return "";
        }

        public float ReadFloatLE()
        {
            float f = _mma.ReadSingle(_index);
            _index += 4;
            return f;
        }

        public static uint ConvertToUintBE(byte[] buff, uint offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToUInt32(number, 0);
        }

        public static uint ConvertToUintLE(byte[] buff, uint offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (!BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToUInt32(number, 0);
        }

        public static float ConvertToFloatLE(byte[] buff, uint offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (!BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToSingle(number, 0);
        }

        public void Dispose()
        {
            if (_mma != null)
                _mma.Dispose();
            _mma = null;

            if (_mmf != null)
                _mmf.Dispose();
            _mmf = null;
        }
    }
}
