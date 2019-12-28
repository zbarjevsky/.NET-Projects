using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using GPSDataParser.Tools;

namespace GPSDataParser.Tools
{
    public class MemoryMappedFileReader :  IBufferReader
    {
        private MemoryMappedFile _mmf = null;
        private MemoryMappedViewAccessor _mma = null;

        public MemoryMappedFileReader(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            Length = info.Length;

            _mmf = MemoryMappedFile.CreateFromFile(fileName, FileMode.Open);
            _mma = _mmf.CreateViewAccessor();

            Position = 0;
        }

        public override byte ReadByte()
        {
            byte b = _mma.ReadByte(Position);
            Position++;
            return b;
        }

        public override byte [] ReadBuffer(long length)
        {
            byte[] buffer = new byte[length];
            int count = _mma.ReadArray<byte>(Position, buffer, 0, (int)length);
            Position += count;
            if (count == length)
                return buffer;
            return null;
        }

        public override void Dispose()
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
