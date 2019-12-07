using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovatekViofoGPSParser
{
    public class Parser
    {
        public static List<ViofoGpsPoint> ReadMP4FileGpsInfo(string fileName)
        {
            byte[] buff = File.ReadAllBytes(fileName);
            List<Box> boxes = new List<Box>(1024);

            uint offset = 0;
            while(offset < buff.Length)
            {
                Box x = new Box(buff, offset);
                if (x.Size > buff.Length)
                    return null; //corrupt file

                offset += x.Size;

                if (x.Kind == "moov")
                {
                    uint off = 0;
                    while(off < x.Size - 8)
                    {
                        Box g = new Box(x.Data, off);
                        if (g.Kind == "gps ")
                            return ParseGpsCatalog(g, buff); //gps catalog - position and size list
                        off += g.Size;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// This will parse list of location-size pairs
        /// And parse each specific location
        /// </summary>
        /// <param name="g"></param>
        /// <param name="file"></param>
        /// <returns>list of gps points</returns>
        private static List<ViofoGpsPoint> ParseGpsCatalog(Box g, byte [] file)
        {
            LocationsList list = new LocationsList(g);
            List<ViofoGpsPoint> gpsPoints = new List<ViofoGpsPoint>();
            foreach (Location loc in list.locations)
            {
                if (loc.Offset == 0 || loc.Length == 0)
                    continue;
                ViofoGpsPoint i = ViofoGpsPoint.Parse(loc.Offset, loc.Length, file);
                if (i == null)
                    continue;
                gpsPoints.Add(i);
            }
            return gpsPoints;
        }
    }

    //Each box starts with 8 byte “header” (including the beginning of the file). 
    //The first 4 bytes is the size of the box (big endian unsigned int), 
    //the second 4 bytes contains 4 character string name/type of the box. 
    //The size includes itself (so valid size is >= 0x0008 unless the special type 
    //of large box which I will conveniently omit in this post ;)). 
    //Basically MP4 container can be treated as some rudimentary file system.
    public class Box
    {
        public uint Start, Size;
        public string Kind;

        public byte[] Data;

        public Box(byte[] file, uint start)
        {
            Start = start;
            Size = ReadUintBE(file, start);
            Kind = Encoding.ASCII.GetString(file, (int)start + 4, 4);

            if (Size > file.Length - Start)
                return; //bad offset or size

            Data = new byte[Size - 8];
            Array.Copy(file, start + 8, Data, 0, Size - 8);
        }

        public static uint ReadUintBE(byte[] buff, uint offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToUInt32(number, 0);
        }

        public static uint ReadUintLE(byte[] buff, uint offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (!BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToUInt32(number, 0);
        }

        public static float ReadFloatLE(byte[] buff, uint offset)
        {
            byte[] number = new byte[4];
            Array.Copy(buff, offset, number, 0, 4);
            if (!BitConverter.IsLittleEndian)
                number = number.Reverse().ToArray();
            return BitConverter.ToSingle(number, 0);
        }
    }

    public class Location
    {
        public uint Offset, Length;

        public uint Read(byte [] data, uint pos)
        {
            Offset = Box.ReadUintBE(data, pos);
            pos += 4;
            Length = Box.ReadUintBE(data, pos);
            pos += 4;

            return pos;
        }

        public override string ToString()
        {
            return "Offset: " + Offset.ToString("###,###,###") + ", Len: " + Length.ToString("###,###");
        }
    }

    public class LocationsList
    {
        public uint Version, EncodedDate;
        public List<Location> locations = new List<Location>();

        public LocationsList(Box gpsCatalog)
        {
            uint off = 0;
            Version = Box.ReadUintBE(gpsCatalog.Data, off);
            off += 4;
            EncodedDate = Box.ReadUintBE(gpsCatalog.Data, off);
            off += 4;

            while (off < gpsCatalog.Data.Length)
            {
                Location l = new Location();
                off = l.Read(gpsCatalog.Data, off);
                locations.Add(l);
            }
        }
    }
}
