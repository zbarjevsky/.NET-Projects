﻿
namespace DynamicMap.NET.Projections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// PROJCS["SWEREF99 TM",GEOGCS["SWEREF99",DATUM["SWEREF99",SPHEROID["GRS 1980",6378137,298.257222101,AUTHORITY["EPSG","7019"]],TOWGS84[0,0,0,0,0,0,0],AUTHORITY["EPSG","6619"]],PRIMEM["Greenwich",0,AUTHORITY["EPSG","8901"]],UNIT["degree",0.01745329251994328,AUTHORITY["EPSG","9122"]],AUTHORITY["EPSG","4619"]],UNIT["metre",1,AUTHORITY["EPSG","9001"]],
    /// PROJECTION["Transverse_Mercator"],PARAMETER["latitude_of_origin",0],PARAMETER["central_meridian",15],PARAMETER["scale_factor",0.9996],PARAMETER["false_easting",500000],PARAMETER["false_northing",0],AUTHORITY["EPSG","3006"],AXIS["y",EAST],AXIS["x",NORTH]]
    /// </summary>
    public class SWEREF99_TMProjection : PureProjection
    {
        public static readonly SWEREF99_TMProjection Instance = new SWEREF99_TMProjection();

        static readonly double MinLatitude = 54.96;
        static readonly double MaxLatitude = 69.07;
        static readonly double MinLongitude = 10.0;
        static readonly double MaxLongitude = 24.5;

        static readonly double orignX = -1200000;
        static readonly double orignY = 8500000;

        static readonly double scaleFactor = 0.9996;                    // scale factor				
        static readonly double centralMeridian = DegreesToRadians(15);// Center longitude (projection center) 
        static readonly double latOrigin = 0.0;                    // center latitude			
        static readonly double falseNorthing = 0.0;           // y offset in meters			
        static readonly double falseEasting = 500000.0;        // x offset in meters			
        static readonly double semiMajor = 6378137.0;           // major axis
        static readonly double semiMinor = 6356752.3141403561; // minor axis
        static readonly double semiMinor2 = 6356752.3142451793;     // minor axis
        static readonly double metersPerUnit = 1.0;
        static readonly double COS_67P5 = 0.38268343236508977; // cosine of 67.5 degrees
        static readonly double AD_C = 1.0026000;               // Toms region 1 constant

        public override RectLatLng Bounds
        {
            get
            {
                return RectLatLng.FromLTRB(MinLongitude, MaxLatitude, MaxLongitude, MinLatitude);
            }
        }

        DynSize tileSize = new DynSize(256, 256);
        public override DynSize TileSize
        {
            get
            {
                return tileSize;
            }
        }

        public override double Axis
        {
            get
            {
                return 6378137;
            }
        }

        public override double Flattening
        {
            get
            {
                return (1.0 / 298.257222101);
            }
        }

        public override DynPoint FromLatLngToPixel(double lat, double lng, int zoom)
        {
            DynPoint ret = DynPoint.Empty;

            lat = Clip(lat, MinLatitude, MaxLatitude);
            lng = Clip(lng, MinLongitude, MaxLongitude);

            double[] lks = new double[] { lng, lat };
            lks = DTM10(lks);
            lks = MTD10(lks);
            lks = DTM00(lks);

            double res = GetTileMatrixResolution(zoom);
            return LksToPixel(lks, res);
        }

        static DynPoint LksToPixel(double[] lks, double res)
        {
            return new DynPoint((long)Math.Floor((lks[0] - orignX) / res), (long)Math.Floor((orignY - lks[1]) / res));
        }

        public override PointLatLng FromPixelToLatLng(long x, long y, int zoom)
        {
            PointLatLng ret = PointLatLng.Empty;

            double res = GetTileMatrixResolution(zoom);

            double[] lks = new double[] { (x * res) + orignX, orignY - (y * res) };
            lks = MTD11(lks);
            lks = DTM10(lks);
            lks = MTD10(lks);

            ret.Lat = Clip(lks[1], MinLatitude, MaxLatitude);
            ret.Lng = Clip(lks[0], MinLongitude, MaxLongitude);

            return ret;
        }

        double[] DTM10(double[] lonlat)
        {
            // Eccentricity squared : (a^2 - b^2)/a^2
            double es = 1.0 - (semiMinor2 * semiMinor2) / (semiMajor * semiMajor); // e^2

            // Second eccentricity squared : (a^2 - b^2)/b^2
            double ses = (Math.Pow(semiMajor, 2) - Math.Pow(semiMinor2, 2)) / Math.Pow(semiMinor2, 2);

            double ba = semiMinor2 / semiMajor;
            double ab = semiMajor / semiMinor2;

            double lon = DegreesToRadians(lonlat[0]);
            double lat = DegreesToRadians(lonlat[1]);
            double h = lonlat.Length < 3 ? 0 : lonlat[2].Equals(Double.NaN) ? 0 : lonlat[2];
            double v = semiMajor / Math.Sqrt(1 - es * Math.Pow(Math.Sin(lat), 2));
            double x = (v + h) * Math.Cos(lat) * Math.Cos(lon);
            double y = (v + h) * Math.Cos(lat) * Math.Sin(lon);
            double z = ((1 - es) * v + h) * Math.Sin(lat);
            return new double[] { x, y, z, };
        }

        double[] MTD10(double[] pnt)
        {
            // Eccentricity squared : (a^2 - b^2)/a^2
            double es = 1.0 - (semiMinor * semiMinor) / (semiMajor * semiMajor); // e^2

            // Second eccentricity squared : (a^2 - b^2)/b^2
            double ses = (Math.Pow(semiMajor, 2) - Math.Pow(semiMinor, 2)) / Math.Pow(semiMinor, 2);

            double ba = semiMinor / semiMajor;
            double ab = semiMajor / semiMinor;

            bool AtPole = false; // is location in polar region
            double Z = pnt.Length < 3 ? 0 : pnt[2].Equals(Double.NaN) ? 0 : pnt[2];

            double lon = 0;
            double lat = 0;
            double Height = 0;
            if (pnt[0] != 0.0)
            {
                lon = Math.Atan2(pnt[1], pnt[0]);
            }
            else
            {
                if (pnt[1] > 0)
                {
                    lon = Math.PI / 2;
                }
                else
                   if (pnt[1] < 0)
                {
                    lon = -Math.PI * 0.5;
                }
                else
                {
                    AtPole = true;
                    lon = 0.0;
                    if (Z > 0.0) // north pole
                    {
                        lat = Math.PI * 0.5;
                    }
                    else
                       if (Z < 0.0) // south pole
                    {
                        lat = -Math.PI * 0.5;
                    }
                    else // center of earth
                    {
                        return new double[] { RadiansToDegrees(lon), RadiansToDegrees(Math.PI * 0.5), -semiMinor, };
                    }
                }
            }
            double W2 = pnt[0] * pnt[0] + pnt[1] * pnt[1]; // Square of distance from Z axis
            double W = Math.Sqrt(W2); // distance from Z axis
            double T0 = Z * AD_C; // initial estimate of vertical component
            double S0 = Math.Sqrt(T0 * T0 + W2); // initial estimate of horizontal component
            double Sin_B0 = T0 / S0; // sin(B0), B0 is estimate of Bowring aux variable
            double Cos_B0 = W / S0; // cos(B0)
            double Sin3_B0 = Math.Pow(Sin_B0, 3);
            double T1 = Z + semiMinor * ses * Sin3_B0; // corrected estimate of vertical component
            double Sum = W - semiMajor * es * Cos_B0 * Cos_B0 * Cos_B0; // numerator of cos(phi1)
            double S1 = Math.Sqrt(T1 * T1 + Sum * Sum); // corrected estimate of horizontal component
            double Sin_p1 = T1 / S1; // sin(phi1), phi1 is estimated latitude
            double Cos_p1 = Sum / S1; // cos(phi1)
            double Rn = semiMajor / Math.Sqrt(1.0 - es * Sin_p1 * Sin_p1); // Earth radius at location
            if (Cos_p1 >= COS_67P5)
            {
                Height = W / Cos_p1 - Rn;
            }
            else
               if (Cos_p1 <= -COS_67P5)
            {
                Height = W / -Cos_p1 - Rn;
            }
            else
            {
                Height = Z / Sin_p1 + Rn * (es - 1.0);
            }

            if (!AtPole)
            {
                lat = Math.Atan(Sin_p1 / Cos_p1);
            }
            return new double[] { RadiansToDegrees(lon), RadiansToDegrees(lat), Height, };
        }

        double[] DTM00(double[] lonlat)
        {
            double e0, e1, e2, e3;  // eccentricity constants		
            double e, es, esp;      // eccentricity constants		
            double ml0;              // small value m			

            es = 1.0 - Math.Pow(semiMinor / semiMajor, 2);
            e = Math.Sqrt(es);
            e0 = e0fn(es);
            e1 = e1fn(es);
            e2 = e2fn(es);
            e3 = e3fn(es);
            ml0 = semiMajor * mlfn(e0, e1, e2, e3, latOrigin);
            esp = es / (1.0 - es);

            // ...		

            double lon = DegreesToRadians(lonlat[0]);
            double lat = DegreesToRadians(lonlat[1]);

            double delta_lon = 0.0;  // Delta longitude (Given longitude - center)
            double sin_phi, cos_phi; // sin and cos value				
            double al, als;         // temporary values				
            double c, t, tq;           // temporary values				
            double con, n, ml;      // cone constant, small m			

            delta_lon = AdjustLongitude(lon - centralMeridian);
            SinCos(lat, out sin_phi, out cos_phi);

            al = cos_phi * delta_lon;
            als = Math.Pow(al, 2);
            c = esp * Math.Pow(cos_phi, 2);
            tq = Math.Tan(lat);
            t = Math.Pow(tq, 2);
            con = 1.0 - es * Math.Pow(sin_phi, 2);
            n = semiMajor / Math.Sqrt(con);
            ml = semiMajor * mlfn(e0, e1, e2, e3, lat);

            double x = scaleFactor * n * al * (1.0 + als / 6.0 * (1.0 - t + c + als / 20.0 *
                (5.0 - 18.0 * t + Math.Pow(t, 2) + 72.0 * c - 58.0 * esp))) + falseEasting;

            double y = scaleFactor * (ml - ml0 + n * tq * (als * (0.5 + als / 24.0 *
                (5.0 - t + 9.0 * c + 4.0 * Math.Pow(c, 2) + als / 30.0 * (61.0 - 58.0 * t
                + Math.Pow(t, 2) + 600.0 * c - 330.0 * esp))))) + falseNorthing;

            if (lonlat.Length < 3)
                return new double[] { x / metersPerUnit, y / metersPerUnit };
            else
                return new double[] { x / metersPerUnit, y / metersPerUnit, lonlat[2] };
        }

        double[] DTM01(double[] lonlat)
        {
            // Eccentricity squared : (a^2 - b^2)/a^2
            double es = 1.0 - (semiMinor * semiMinor) / (semiMajor * semiMajor);

            // Second eccentricity squared : (a^2 - b^2)/b^2
            double ses = (Math.Pow(semiMajor, 2) - Math.Pow(semiMinor, 2)) / Math.Pow(semiMinor, 2);

            double ba = semiMinor / semiMajor;
            double ab = semiMajor / semiMinor;

            double lon = DegreesToRadians(lonlat[0]);
            double lat = DegreesToRadians(lonlat[1]);
            double h = lonlat.Length < 3 ? 0 : lonlat[2].Equals(Double.NaN) ? 0 : lonlat[2];
            double v = semiMajor / Math.Sqrt(1 - es * Math.Pow(Math.Sin(lat), 2));
            double x = (v + h) * Math.Cos(lat) * Math.Cos(lon);
            double y = (v + h) * Math.Cos(lat) * Math.Sin(lon);
            double z = ((1 - es) * v + h) * Math.Sin(lat);
            return new double[] { x, y, z, };
        }

        double[] MTD01(double[] pnt)
        {
            // Eccentricity squared : (a^2 - b^2)/a^2
            double es = 1.0 - (semiMinor2 * semiMinor2) / (semiMajor * semiMajor);

            // Second eccentricity squared : (a^2 - b^2)/b^2
            double ses = (Math.Pow(semiMajor, 2) - Math.Pow(semiMinor2, 2)) / Math.Pow(semiMinor2, 2);

            double ba = semiMinor2 / semiMajor;
            double ab = semiMajor / semiMinor2;

            bool At_Pole = false; // is location in polar region
            double Z = pnt.Length < 3 ? 0 : pnt[2].Equals(Double.NaN) ? 0 : pnt[2];

            double lon = 0;
            double lat = 0;
            double Height = 0;
            if (pnt[0] != 0.0)
            {
                lon = Math.Atan2(pnt[1], pnt[0]);
            }
            else
            {
                if (pnt[1] > 0)
                {
                    lon = Math.PI / 2;
                }
                else
                   if (pnt[1] < 0)
                {
                    lon = -Math.PI * 0.5;
                }
                else
                {
                    At_Pole = true;
                    lon = 0.0;
                    if (Z > 0.0) // north pole
                    {
                        lat = Math.PI * 0.5;
                    }
                    else
                       if (Z < 0.0) // south pole
                    {
                        lat = -Math.PI * 0.5;
                    }
                    else // center of earth
                    {
                        return new double[] { RadiansToDegrees(lon), RadiansToDegrees(Math.PI * 0.5), -semiMinor2, };
                    }
                }
            }

            double W2 = pnt[0] * pnt[0] + pnt[1] * pnt[1]; // Square of distance from Z axis
            double W = Math.Sqrt(W2);                      // distance from Z axis
            double T0 = Z * AD_C;                // initial estimate of vertical component
            double S0 = Math.Sqrt(T0 * T0 + W2); //initial estimate of horizontal component
            double Sin_B0 = T0 / S0;             // sin(B0), B0 is estimate of Bowring aux variable
            double Cos_B0 = W / S0;              // cos(B0)
            double Sin3_B0 = Math.Pow(Sin_B0, 3);
            double T1 = Z + semiMinor2 * ses * Sin3_B0; //corrected estimate of vertical component
            double Sum = W - semiMajor * es * Cos_B0 * Cos_B0 * Cos_B0; // numerator of cos(phi1)
            double S1 = Math.Sqrt(T1 * T1 + Sum * Sum); // corrected estimate of horizontal component
            double Sin_p1 = T1 / S1;  // sin(phi1), phi1 is estimated latitude
            double Cos_p1 = Sum / S1; // cos(phi1)
            double Rn = semiMajor / Math.Sqrt(1.0 - es * Sin_p1 * Sin_p1); // Earth radius at location

            if (Cos_p1 >= COS_67P5)
            {
                Height = W / Cos_p1 - Rn;
            }
            else
               if (Cos_p1 <= -COS_67P5)
            {
                Height = W / -Cos_p1 - Rn;
            }
            else
            {
                Height = Z / Sin_p1 + Rn * (es - 1.0);
            }

            if (!At_Pole)
            {
                lat = Math.Atan(Sin_p1 / Cos_p1);
            }
            return new double[] { RadiansToDegrees(lon), RadiansToDegrees(lat), Height, };
        }

        double[] MTD11(double[] p)
        {
            double e0, e1, e2, e3;  // eccentricity constants		
            double e, es, esp;      // eccentricity constants		
            double ml0;         // small value m

            es = 1.0 - Math.Pow(semiMinor / semiMajor, 2);
            e = Math.Sqrt(es);
            e0 = e0fn(es);
            e1 = e1fn(es);
            e2 = e2fn(es);
            e3 = e3fn(es);
            ml0 = semiMajor * mlfn(e0, e1, e2, e3, latOrigin);
            esp = es / (1.0 - es);

            // ...

            double con, phi;
            double delta_phi;
            long i;
            double sin_phi, cos_phi, tan_phi;
            double c, cs, t, ts, n, r, d, ds;
            long max_iter = 6;

            double x = p[0] * metersPerUnit - falseEasting;
            double y = p[1] * metersPerUnit - falseNorthing;

            con = (ml0 + y / scaleFactor) / semiMajor;
            phi = con;
            for (i = 0; ; i++)
            {
                delta_phi = ((con + e1 * Math.Sin(2.0 * phi) - e2 * Math.Sin(4.0 * phi) + e3 * Math.Sin(6.0 * phi)) / e0) - phi;
                phi += delta_phi;

                if (Math.Abs(delta_phi) <= EPSLoN)
                    break;

                if (i >= max_iter)
                    throw new ArgumentException("Latitude failed to converge");
            }

            if (Math.Abs(phi) < HALF_PI)
            {
                SinCos(phi, out sin_phi, out cos_phi);
                tan_phi = Math.Tan(phi);
                c = esp * Math.Pow(cos_phi, 2);
                cs = Math.Pow(c, 2);
                t = Math.Pow(tan_phi, 2);
                ts = Math.Pow(t, 2);
                con = 1.0 - es * Math.Pow(sin_phi, 2);
                n = semiMajor / Math.Sqrt(con);
                r = n * (1.0 - es) / con;
                d = x / (n * scaleFactor);
                ds = Math.Pow(d, 2);

                double lat = phi - (n * tan_phi * ds / r) * (0.5 - ds / 24.0 * (5.0 + 3.0 * t +
                    10.0 * c - 4.0 * cs - 9.0 * esp - ds / 30.0 * (61.0 + 90.0 * t +
                    298.0 * c + 45.0 * ts - 252.0 * esp - 3.0 * cs)));

                double lon = AdjustLongitude(centralMeridian + (d * (1.0 - ds / 6.0 * (1.0 + 2.0 * t +
                    c - ds / 20.0 * (5.0 - 2.0 * c + 28.0 * t - 3.0 * cs + 8.0 * esp +
                    24.0 * ts))) / cos_phi));

                if (p.Length < 3)
                    return new double[] { RadiansToDegrees(lon), RadiansToDegrees(lat) };
                else
                    return new double[] { RadiansToDegrees(lon), RadiansToDegrees(lat), p[2] };
            }
            else
            {
                if (p.Length < 3)
                    return new double[] { RadiansToDegrees(HALF_PI * Sign(y)), RadiansToDegrees(centralMeridian) };
                else
                    return new double[] { RadiansToDegrees(HALF_PI * Sign(y)), RadiansToDegrees(centralMeridian), p[2] };
            }
        }

        #region -- levels info --
        //	"defaultLayer" : "topowebbwmts",
        //	"extent" : {
        //		"left" : -1200000,
        //		"bottom" : 4700000,
        //		"right" : 2600000,
        //		"top" : 8500000
        //	},
        //	"projection" : "EPSG:3006",
        //	"units" : "m",
        //	"allOverlays" : true,
        //	"resolutions" : [4096.0, 2048.0, 1024.0, 512.0, 256.0, 128.0, 64.0, 32.0, 16.0, 8.0, 4.0, 2.0, 1.0, 0.5, 0.25, 0.15, 0.1, 0.05, 0.01],
        //	"initPosition" : {
        //		"n" : 6607899,
        //		"e" : 564931,
        //		"zoom" : 2
        //	},
        #endregion

        static double[] resolutions = new double[] { 4096.0, 2048.0, 1024.0, 512.0, 256.0, 128.0, 64.0, 32.0, 16.0, 8.0, 4.0, 2.0, 1.0, 0.5, 0.25, 0.15, 0.1, 0.05, 0.01 };
        public static double GetTileMatrixResolution(int zoom)
        {
            double ret = 0;

            if (zoom < resolutions.Length)
            {
                ret = resolutions[zoom];
            }

            return ret;
        }

        public override double GetGroundResolution(int zoom, double latitude)
        {
            return GetTileMatrixResolution(zoom);
        }

        Dictionary<int, DynSize> extentMatrixMin;
        Dictionary<int, DynSize> extentMatrixMax;

        public override DynSize GetTileMatrixMinXY(int zoom)
        {
            if (extentMatrixMin == null)
            {
                GenerateExtents();
            }
            return extentMatrixMin[zoom];
        }

        public override DynSize GetTileMatrixMaxXY(int zoom)
        {
            if (extentMatrixMax == null)
            {
                GenerateExtents();
            }
            return extentMatrixMax[zoom];
        }

        void GenerateExtents()
        {
            extentMatrixMin = new Dictionary<int, DynSize>();
            extentMatrixMax = new Dictionary<int, DynSize>();

            for (int i = 0; i <= resolutions.Length; i++)
            {
                double res = GetTileMatrixResolution(i);

                extentMatrixMin.Add(i, new DynSize(FromPixelToTileXY(FromLatLngToPixel(Bounds.LocationTopLeft, i))));
                extentMatrixMax.Add(i, new DynSize(FromPixelToTileXY(FromLatLngToPixel(Bounds.LocationRightBottom, i))));
            }
        }
    }
}
