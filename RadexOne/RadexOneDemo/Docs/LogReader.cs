using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadexOneDemo.Docs
{
    public class LogReader
    {
        private static Dictionary<string, byte> mapBytes = new Dictionary<string, byte>(256);

        static LogReader()
        {
            for (int i = 0; i <= 0xFF; i++)
            {
                string key = i.ToString("X2");
                mapBytes[key] = (byte)i;
            }
        }

        public static void Test()
        {
            //Read(@"C:\Dev_Mark\Integration\Source\iLogic2\Algorithms\FResearch\FluoroCT\RadexOneDemo\Docs\SendRcv.log.GetAndSave32-16.txt");
        }

        public static void Read(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            for (int i = 0; i < lines.Length; i++)
            {

                if (lines[i].StartsWith("7BFF")) //request
                {
                    //Debug.Write((idx++) + ". ");
                    Debug.WriteLine(lines[i]);
                    byte[] buff = ReadBytes(lines[i]);
                    int[] bitsCount = new int[] { 16, 8, 8, 8, 8, 32, 16, 16, 16, 16, 16, 16, 16 };
                    ParseReceivedDataVariable(bitsCount, buff);
                    Debug.WriteLine("");
                }
                else if (lines[i].StartsWith("7AFF")) //response
                {
                    //Debug.Write((idx++) + ". ");
                    Debug.WriteLine(lines[i]);
                    byte[] buff = ReadBytes(lines[i]);
                    int[] bitsCount = new int[] { 16, 8, 8, 8, 8, 32, 8, 8, 16, 16, 32, 32, 32, 16, 16, 16, 16, 16, 16, 16 };
                    ParseReceivedDataVariable(bitsCount, buff);
                    Debug.WriteLine("");
                }
                else if (lines[i].StartsWith("="))
                {
                    //Debug.Write("== "+(idx++) + ". ");
                    Debug.WriteLine(lines[i]);
                }
            }
        }

        public static byte[] ReadBytes(string line)
        {
            byte [] buff = new byte[line.Length/2];
            line = line.Replace(" ", "").Replace("-", "").Replace("-", "");
            for (int i = 0; i < line.Length; i+=2)
            {
                string sb = line.Substring(i, 2);
                byte b = mapBytes[sb];
                buff[i/2] = b;
            }
            //string s = Encoding.ASCII.GetString(buff);
            //Debug.WriteLine(s);
            return buff;
        }

        private static void ParseReceivedDataVariable(int[] bitsCount, byte[] data)
        {
            int offset = 0, idx = 0;
            while(offset<data.Length-2 && idx<bitsCount.Length)
            {
                ReadNum(bitsCount[idx++], data, ref offset);
            }
        }

        private static void ReadNum(int bitsCount, byte [] data, ref int offset)
        {
            const string FMT32 = "0000000000";
            const string FMT16 = "00000";
            const string FMT8 = "000";

            if (bitsCount == 16)
            {
                UInt16 a1 = ReadUINT16(data, ref offset);
                Debug.Write(a1.ToString(FMT16) + ",");
            }
            else if (bitsCount == 32)
            {
                UInt32 a2 = ReadUINT32(data, ref offset);
                Debug.Write(a2.ToString(FMT32) + ",");
            }
            else if (bitsCount == 8)
            {
                byte a2 = data[offset++];
                Debug.Write(a2.ToString(FMT8) + ",");
            }
        }

        private static UInt32[] ParseReceivedData32(byte[] data)
        {
            UInt32[] buff = new uint[data.Length / 4];

            int offset = 0;
            while (offset < data.Length - 4)
            {
                buff[offset / 4] = ReadUINT32(data, ref offset);
            }

            for (int i = 0; i < offset / 4; i++)
            {
                Debug.Write(buff[i].ToString("0000000000") + ",");
            }
            Debug.WriteLine("");

            return buff;
        }

        private static UInt16[] ParseReceivedData16(byte[] data)
        {
            UInt16[] buff = new ushort[data.Length / 2];

            int offset = 0;
            while (offset < data.Length - 2)
            {
                buff[offset / 2] = ReadUINT16(data, ref offset);
            }

            for (int i = 0; i < offset / 2; i++)
            {
                Debug.Write(buff[i].ToString("000000") + ",");
            }
            Debug.WriteLine("");

            return buff;
        }

        public static uint ReadUINT32(byte[] buffer, ref int idxFrom)
        {
            byte[] temp_buf = new byte[4];

            for (int ii = 0; ii < 4; ii++)
                temp_buf[ii] = buffer[idxFrom + ii];

            idxFrom += 4;

            return byte_array_to_UINT32_LE(temp_buf);
        }

        public static ushort ReadUINT16(byte[] buffer, ref int idxFrom)
        {
            byte[] temp_buf = new byte[2];

            for (int ii = 0; ii < 2; ii++)
                temp_buf[ii] = buffer[idxFrom + ii];

            idxFrom += 2;

            return byte_array_to_UINT16_LE(temp_buf);
        }

        public static UInt32 byte_array_to_UINT32_LE(byte[] temp_array)  // [0x34,0x33,0x32,0x31]   >> 0x31323334/825373492
        {
            byte[] buf32 = temp_array;
            UInt32 t32 = 0;
            for (int i = 3; i > 0; i--)
            {
                t32 = t32 | ((UInt32)buf32[i] & 0x000000FF);
                t32 = (t32 << 8);
            }
            t32 = t32 | ((UInt32)buf32[0] & 0x000000FF);
            return t32;
        }

        public static UInt16 byte_array_to_UINT16_LE(byte[] temp_array) // [0x32,0x31]   >> 0x3132/12594
        {
            byte[] buf16 = temp_array;
            UInt16 t16 = 0;
            t16 = (UInt16)(t16 | (buf16[1] & 0x00FF));
            t16 = (UInt16)(t16 << 8);
            t16 = (UInt16)(t16 | (buf16[0] & 0x00FF));
            return t16;
        }

    }
}
