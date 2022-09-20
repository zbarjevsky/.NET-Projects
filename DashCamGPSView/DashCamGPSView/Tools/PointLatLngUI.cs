
using DynamicMap.NET;
using System;
using System.Globalization;

namespace DashCam.Tools
{
    /// <summary>
    /// the point of coordinates
    /// </summary>
    [Serializable]
    public struct PointLatLngUI
    {
        public static readonly PointLatLngUI Empty = new PointLatLngUI();
        
        private double lat;
        private double lng;

        bool NotEmpty;

        public PointLatLngUI(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
            NotEmpty = true;
        }

        /// <summary>
        /// returns true if coordinates wasn't assigned
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !NotEmpty;
            }
        }

        public double Lat
        {
            get
            {
                return this.lat;
            }
            set
            {
                this.lat = value;
                NotEmpty = true;
            }
        }

        public double Lng
        {
            get
            {
                return this.lng;
            }
            set
            {
                this.lng = value;
                NotEmpty = true;
            }
        }

        public static PointLatLngUI operator +(PointLatLngUI pt, SizeLatLng sz)
        {
            return Add(pt, sz);
        }

        public static PointLatLngUI operator -(PointLatLngUI pt, SizeLatLng sz)
        {
            return Subtract(pt, sz);
        }

        public static SizeLatLng operator -(PointLatLngUI pt1, PointLatLngUI pt2)
        {
            return new SizeLatLng(pt1.Lat - pt2.Lat, pt2.Lng - pt1.Lng);
        }

        public static bool operator ==(PointLatLngUI left, PointLatLngUI right)
        {
            return ((left.Lng == right.Lng) && (left.Lat == right.Lat));
        }

        public static bool operator !=(PointLatLngUI left, PointLatLngUI right)
        {
            return !(left == right);
        }

        public static PointLatLngUI Add(PointLatLngUI pt, SizeLatLng sz)
        {
            return new PointLatLngUI(pt.Lat - sz.HeightLat, pt.Lng + sz.WidthLng);
        }

        public static PointLatLngUI Subtract(PointLatLngUI pt, SizeLatLng sz)
        {
            return new PointLatLngUI(pt.Lat + sz.HeightLat, pt.Lng - sz.WidthLng);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PointLatLngUI))
            {
                return false;
            }
            PointLatLngUI tf = (PointLatLngUI)obj;
            return (((tf.Lng == this.Lng) && (tf.Lat == this.Lat)) && tf.GetType().Equals(base.GetType()));
        }

        public void Offset(PointLatLngUI pos)
        {
            this.Offset(pos.Lat, pos.Lng);
        }

        public void Offset(double lat, double lng)
        {
            this.Lng += lng;
            this.Lat -= lat;
        }

        public override int GetHashCode()
        {
            return (this.Lng.GetHashCode() ^ this.Lat.GetHashCode());
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Lat={0:0.000000}, Lng={1:0.000000}}}", this.Lat, this.Lng);
        }
    }
}