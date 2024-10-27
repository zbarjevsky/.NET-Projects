using GPSDataParser;
using GPSDataParser.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovatekViofoGPSParser
{
    public class ViofoParser
    {
        public static List<ViofoGpsPoint> ReadMP4FileGpsInfo(string fileName)
        {
            const uint maxSize = 1024 * 1024 * 1024;

            FileInfo info = new FileInfo(fileName);
            if (!info.Exists || info.Length > maxSize) //more than 1 GB
                return null;

            byte[] buff = File.ReadAllBytes(fileName);
            using (IBufferReader reader = new BufferReader(buff))
            //using (IBufferReader reader = new MemoryMappedFileReader(fileName))
            {
                while (reader.Position < reader.Length)
                {
                    uint size = reader.ReadUintBE();
                    string kind = reader.ReadString(4);
                    if (size > reader.Length)
                        return null; //corrupt file

                    Debug.WriteLine($"Kind: {kind}, Size: {size}");
                    if (kind == "moov")
                    {
                        long end = reader.Position + size - 8;
                        while (reader.Position < end)
                        {
                            uint size1 = reader.ReadUintBE();
                            string kind1 = reader.ReadString(4);
                            Debug.WriteLine($"Kind1: {kind1}, Size1: {size1}");
                            if (kind1 == "gps ")
                                return ParseGpsCatalog(reader, size1 - 8); //gps catalog - position and size list

                            reader.Position += (size1 - 8);
                        }
                    }
                    else
                    {
                        reader.Position += (size - 8);
                    }
                }
            }

            return null;
        }

        private static List<ViofoGpsPoint> ParseMovieHeaderCatalog(IBufferReader reader, long size)
        {
            List<ViofoGpsPoint> gpsPoints = new List<ViofoGpsPoint>();
            LocationsList list = new LocationsList(reader, size);
            return gpsPoints;
        }

        /// <summary>
        /// This will parse list of location-size pairs
        /// And parse each specific location
        /// </summary>
        /// <param name="g"></param>
        /// <param name="file"></param>
        /// <returns>list of gps points</returns>
        private static List<ViofoGpsPoint> ParseGpsCatalog(IBufferReader reader, long size)
        {
            LocationsList list = new LocationsList(reader, size);
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

        public class Location
        {
            public uint Offset, Length;

            public override string ToString()
            {
                return "Offset: " + Offset.ToString("###,###,###") + ", Len: " + Length.ToString("###,###");
            }
        }

        public class LocationsList
        {
            public uint Version, EncodedDate;
            public List<Location> locations = new List<Location>();

            public LocationsList(IBufferReader reader, long size)
            {
                long last = reader.Position + size;

                Version = reader.ReadUintBE();
                EncodedDate = reader.ReadUintBE();

                while (reader.Position < last)
                {
                    locations.Add(new Location()
                    {
                        Offset = reader.ReadUintBE(),
                        Length = reader.ReadUintBE()
                    });
                }
            }
        }
    }
}
