using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSDataParser.Tools
{
    public class BufferReader : IBufferReader
    {
        private byte[] _buffer = null;

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

        public override byte ReadByte()
        {
            byte b = _buffer[Position];
            Position++;
            return b;
        }

        public override byte[] ReadBuffer(long length)
        {
            byte[] buffer = new byte[length];
            Array.Copy(_buffer, Position, buffer, 0, length);
            Position += length;
            return buffer;
        }

        public override void Dispose()
        {
            _buffer = null;
        }
    }
}
