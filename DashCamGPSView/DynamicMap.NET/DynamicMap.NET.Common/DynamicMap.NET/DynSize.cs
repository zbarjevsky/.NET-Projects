
namespace DynamicMap.NET
{
   using System.Globalization;

   /// <summary>
   /// the size
   /// </summary>
   public struct DynSize
   {
      public static readonly DynSize Empty = new DynSize();

      private long width;
      private long height;

      public DynSize(DynPoint pt)
      {
         width = pt.X;
         height = pt.Y;
      }

      public DynSize(long width, long height)
      {
         this.width = width;
         this.height = height;
      }

      public static DynSize operator +(DynSize sz1, DynSize sz2)
      {
         return Add(sz1, sz2);
      }

      public static DynSize operator -(DynSize sz1, DynSize sz2)
      {
         return Subtract(sz1, sz2);
      }

      public static bool operator ==(DynSize sz1, DynSize sz2)
      {
         return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
      }

      public static bool operator !=(DynSize sz1, DynSize sz2)
      {
         return !(sz1 == sz2);
      }

      public static explicit operator DynPoint(DynSize size)
      {
         return new DynPoint(size.Width, size.Height);
      }

      public bool IsEmpty
      {
         get
         {
            return width == 0 && height == 0;
         }
      }

      public long Width
      {
         get
         {
            return width;
         }
         set
         {
            width = value;
         }
      }

      public long Height
      {
         get
         {
            return height;
         }
         set
         {
            height = value;
         }
      }

      public static DynSize Add(DynSize sz1, DynSize sz2)
      {
         return new DynSize(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
      }

      public static DynSize Subtract(DynSize sz1, DynSize sz2)
      {
         return new DynSize(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
      }

      public override bool Equals(object obj)
      {
         if(!(obj is DynSize))
            return false;

         DynSize comp = (DynSize)obj;
         // Note value types can't have derived classes, so we don't need to
         //
         return (comp.width == this.width) &&
                   (comp.height == this.height);
      }

      public override int GetHashCode()
      {
         if(this.IsEmpty)
         {
            return 0;
         }
         return (Width.GetHashCode() ^ Height.GetHashCode());
      }

      public override string ToString()
      {
         return "{Width=" + width.ToString(CultureInfo.CurrentCulture) + ", Height=" + height.ToString(CultureInfo.CurrentCulture) + "}";
      }
   }
}
