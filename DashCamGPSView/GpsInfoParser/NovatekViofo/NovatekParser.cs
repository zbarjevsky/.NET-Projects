using GPSDataParser;
using GPSDataParser.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NovatekViofoGPSParser
{
    public class NovatekParser
    {
        public static List<ViofoGpsPoint> ReadMP4FileGpsInfo(string fileName)
        {
            using (MemoryMappedFileReader reader = new MemoryMappedFileReader(fileName))
            {
                List<Box> boxes = new List<Box>(1024);

                while (reader.Position < reader.Length)
                {
                    Box x = new Box(reader);
                    if (x.Size > reader.Length)
                        return null; //corrupt file

                    if (x.Kind == "moov")
                    {
                        uint off = 0;
                        while (off < x.Size - 8)
                        {
                            Box g = new Box(x.Data, off);
                            if (g.Kind == "gps ")
                                return ParseGpsCatalog(g, reader); //gps catalog - position and size list
                            off += g.Size;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// This will parse list of location-size pairs
        /// And parse each specific location
        /// </summary>
        /// <param name="g"></param>
        /// <param name="file"></param>
        /// <returns>list of gps points</returns>
        private static List<ViofoGpsPoint> ParseGpsCatalog(Box g, MemoryMappedFileReader reader)
        {
            LocationsList list = new LocationsList(g);
            List<ViofoGpsPoint> gpsPoints = new List<ViofoGpsPoint>();
            foreach (Location loc in list.locations)
            {
                if (loc.Offset == 0 || loc.Length == 0)
                    continue;

                reader.Position = loc.Offset;
                ViofoGpsPoint i = ViofoGpsPoint.Parse(reader, loc.Length);
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
        public uint Size;
        public string Kind;

        public byte[] Data;

        public Box(MemoryMappedFileReader reader)
        {
            const uint maxSize = 1024 * 1024 * 1024;

            Size = reader.ReadUintBE();
            Kind = reader.ReadString(4);

            if (Size > reader.Length || Size > maxSize)
                return; //bad offset or size

            Data = reader.ReadBuffer((int)Size - 8);
        }

        public Box(byte[] buffer, uint start)
        {
            Size = MemoryMappedFileReader.ConvertToUintBE(buffer, start);
            Kind = Encoding.ASCII.GetString(buffer, (int)start + 4, 4);

            if (Size > buffer.Length - start)
                return; //bad offset or size

            Data = new byte[Size - 8];
            Array.Copy(buffer, start + 8, Data, 0, Size - 8);
        }
    }

    public class Location
    {
        public uint Offset, Length;

        public uint Read(byte [] data, uint pos)
        {
            Offset = MemoryMappedFileReader.ConvertToUintBE(data, pos);
            pos += 4;
            Length = MemoryMappedFileReader.ConvertToUintBE(data, pos);
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
            Version = MemoryMappedFileReader.ConvertToUintBE(gpsCatalog.Data, off);
            off += 4;
            EncodedDate = MemoryMappedFileReader.ConvertToUintBE(gpsCatalog.Data, off);
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
