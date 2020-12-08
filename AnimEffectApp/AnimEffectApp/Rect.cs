namespace System.Drawing
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>Stores a set of four integers that represent the location and size of a rectangle. For more advanced region functions, use a <see cref="T:System.Drawing.Region"></see> object.</summary>
    /// <filterpriority>1</filterpriority>
    [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true), TypeConverter(typeof(RectangleConverter))]
    public struct Rect
    {
        /// <summary>Represents a <see cref="T:System.Drawing.Rectangle"></see> structure with its properties left uninitialized.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly Rect Empty;
        private int x;
        private int y;
        private int width;
        private int height;
        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle"></see> class with the specified location and size.</summary>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="width">The width of the rectangle. </param>
        /// <param name="height">The height of the rectangle. </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
        public Rect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle"></see> class with the specified location and size.</summary>
        /// <param name="size">A <see cref="T:System.Drawing.Size"></see> that represents the width and height of the rectangular region. </param>
        /// <param name="location">A <see cref="T:System.Drawing.Point"></see> that represents the upper-left corner of the rectangular region. </param>
        public Rect(Point location, System.Drawing.Size size)
        {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.Width;
            this.height = size.Height;
        }

		public Rectangle Rectangle
		{
			get { return new Rectangle(x, y, Width, Height); }
			set { x = value.X; y = value.Y; width = value.Width; height = value.Height; }
		}//end Rectangle

        /// <summary>Creates a <see cref="T:System.Drawing.Rectangle"></see> structure with the specified edge locations.</summary>
        /// <returns>The new <see cref="T:System.Drawing.Rectangle"></see> that this method creates.</returns>
        /// <param name="right">The x-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle"></see> structure. </param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle"></see> structure. </param>
        /// <param name="left">The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle"></see> structure. </param>
        /// <param name="top">The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle"></see> structure. </param>
        /// <filterpriority>1</filterpriority>
        public static Rect FromLTRB(int left, int top, int right, int bottom)
        {
            return new Rect(left, top, right - left, bottom - top);
        }

        /// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>A <see cref="T:System.Drawing.Point"></see> that represents the upper-left corner of this <see cref="T:System.Drawing.Rectangle"></see> structure.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }
        /// <summary>Gets or sets the size of this <see cref="T:System.Drawing.Rectangle"></see>.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> that represents the width and height of this <see cref="T:System.Drawing.Rectangle"></see> structure.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(this.Width, this.Height);
            }
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }
        /// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle"></see> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        /// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle"></see> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        /// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>The width of this <see cref="T:System.Drawing.Rectangle"></see> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }
        /// <summary>Gets or sets the height of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>The height of this <see cref="T:System.Drawing.Rectangle"></see> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }
        /// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle"></see> structure.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Left
        {
            get
            {
                return this.X;
            }
        }
        /// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle"></see> structure.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Top
        {
            get
            {
                return this.Y;
            }
        }
        /// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X"></see> and <see cref="P:System.Drawing.Rectangle.Width"></see> property values of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X"></see> and <see cref="P:System.Drawing.Rectangle.Width"></see> of this <see cref="T:System.Drawing.Rectangle"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Right
        {
            get
            {
                return (this.X + this.Width);
            }
        }
        /// <summary>Gets the y-coordinate that is the sum of the <see cref="P:System.Drawing.Rectangle.Y"></see> and <see cref="P:System.Drawing.Rectangle.Height"></see> property values of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.Y"></see> and <see cref="P:System.Drawing.Rectangle.Height"></see> of this <see cref="T:System.Drawing.Rectangle"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Bottom
        {
            get
            {
                return (this.Y + this.Height);
            }
        }
        /// <summary>Tests whether all numeric properties of this <see cref="T:System.Drawing.Rectangle"></see> have values of zero.</summary>
        /// <returns>This property returns true if the <see cref="P:System.Drawing.Rectangle.Width"></see>, <see cref="P:System.Drawing.Rectangle.Height"></see>, <see cref="P:System.Drawing.Rectangle.X"></see>, and <see cref="P:System.Drawing.Rectangle.Y"></see> properties of this <see cref="T:System.Drawing.Rectangle"></see> all have values of zero; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                if (((this.height == 0) && (this.width == 0)) && (this.x == 0))
                {
                    return (this.y == 0);
                }
                return false;
            }
        }
        /// <summary>Tests whether obj is a <see cref="T:System.Drawing.Rectangle"></see> structure with the same location and size of this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>This method returns true if obj is a <see cref="T:System.Drawing.Rectangle"></see> structure and its <see cref="P:System.Drawing.Rectangle.X"></see>, <see cref="P:System.Drawing.Rectangle.Y"></see>, <see cref="P:System.Drawing.Rectangle.Width"></see>, and <see cref="P:System.Drawing.Rectangle.Height"></see> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.Rectangle"></see> structure; otherwise, false.</returns>
        /// <param name="obj">The <see cref="T:System.Object"></see> to test. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj is Rectangle)
            {
                Rectangle rectangle1 = (Rectangle) obj;
                if (((rectangle1.X == this.X) && (rectangle1.Y == this.Y)) && (rectangle1.Width == this.Width))
                {
                    return (rectangle1.Height == this.Height);
                }
            }
            return false;
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle"></see> structures have equal location and size.</summary>
        /// <returns>This operator returns true if the two <see cref="T:System.Drawing.Rectangle"></see> structures have equal <see cref="P:System.Drawing.Rectangle.X"></see>, <see cref="P:System.Drawing.Rectangle.Y"></see>, <see cref="P:System.Drawing.Rectangle.Width"></see>, and <see cref="P:System.Drawing.Rectangle.Height"></see> properties.</returns>
        /// <param name="right">The <see cref="T:System.Drawing.Rectangle"></see> structure that is to the right of the equality operator. </param>
        /// <param name="left">The <see cref="T:System.Drawing.Rectangle"></see> structure that is to the left of the equality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(Rect left, Rect right)
        {
            if (((left.X == right.X) && (left.Y == right.Y)) && (left.Width == right.Width))
            {
                return (left.Height == right.Height);
            }
            return false;
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle"></see> structures differ in location or size.</summary>
        /// <returns>This operator returns true if any of the <see cref="P:System.Drawing.Rectangle.X"></see>, <see cref="P:System.Drawing.Rectangle.Y"></see>, <see cref="P:System.Drawing.Rectangle.Width"></see> or <see cref="P:System.Drawing.Rectangle.Height"></see> properties of the two <see cref="T:System.Drawing.Rectangle"></see> structures are unequal; otherwise false.</returns>
        /// <param name="right">The <see cref="T:System.Drawing.Rectangle"></see> structure that is to the right of the inequality operator. </param>
        /// <param name="left">The <see cref="T:System.Drawing.Rectangle"></see> structure that is to the left of the inequality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(Rect left, Rect right)
        {
            return !(left == right);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF"></see> structure to a <see cref="T:System.Drawing.Rectangle"></see> structure by rounding the <see cref="T:System.Drawing.RectangleF"></see> values to the next higher integer values.</summary>
        /// <returns>Returns a <see cref="T:System.Drawing.Rectangle"></see>.</returns>
        /// <param name="value">The <see cref="T:System.Drawing.RectangleF"></see> structure to be converted. </param>
        /// <filterpriority>1</filterpriority>
        public static Rect Ceiling(RectangleF value)
        {
            return new Rect((int) Math.Ceiling((double) value.X), (int) Math.Ceiling((double) value.Y), (int) Math.Ceiling((double) value.Width), (int) Math.Ceiling((double) value.Height));
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF"></see> to a <see cref="T:System.Drawing.Rectangle"></see> by truncating the <see cref="T:System.Drawing.RectangleF"></see> values.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see>.</returns>
        /// <param name="value">The <see cref="T:System.Drawing.RectangleF"></see> to be converted. </param>
        /// <filterpriority>1</filterpriority>
        public static Rect Truncate(RectangleF value)
        {
            return new Rect((int) value.X, (int) value.Y, (int) value.Width, (int) value.Height);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF"></see> to a <see cref="T:System.Drawing.Rectangle"></see> by rounding the <see cref="T:System.Drawing.RectangleF"></see> values to the nearest integer values.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see>.</returns>
        /// <param name="value">The <see cref="T:System.Drawing.RectangleF"></see> to be converted. </param>
        /// <filterpriority>1</filterpriority>
        public static Rect Round(RectangleF value)
        {
            return new Rect((int) Math.Round((double) value.X), (int) Math.Round((double) value.Y), (int) Math.Round((double) value.Width), (int) Math.Round((double) value.Height));
        }

        /// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>This method returns true if the point defined by x and y is contained within this <see cref="T:System.Drawing.Rectangle"></see> structure; otherwise false.</returns>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool Contains(int x, int y)
        {
            if (((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y))
            {
                return (y < (this.Y + this.Height));
            }
            return false;
        }

        /// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>This method returns true if the point represented by pt is contained within this <see cref="T:System.Drawing.Rectangle"></see> structure; otherwise false.</returns>
        /// <param name="pt">The <see cref="T:System.Drawing.Point"></see> to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool Contains(Point pt)
        {
            return this.Contains(pt.X, pt.Y);
        }

		public Point CenterPoint()
		{
			int x = this.Left + this.Width / 2;
			int y = this.Top + this.Height / 2;
			return new Point(x, y);
		}//end CenterPoint

        /// <summary>Determines if the rectangular region represented by rect is entirely contained within this <see cref="T:System.Drawing.Rectangle"></see> structure.</summary>
        /// <returns>This method returns true if the rectangular region represented by rect is entirely contained within this <see cref="T:System.Drawing.Rectangle"></see> structure; otherwise false.</returns>
        /// <param name="rect">The <see cref="T:System.Drawing.Rectangle"></see> to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool Contains(Rect rect)
        {
            if (((this.X <= rect.X) && ((rect.X + rect.Width) <= (this.X + this.Width))) && (this.Y <= rect.Y))
            {
                return ((rect.Y + rect.Height) <= (this.Y + this.Height));
            }
            return false;
        }

        /// <summary>Returns the hash code for this <see cref="T:System.Drawing.Rectangle"></see> structure. For information about the use of hash codes, see <see cref="M:System.Object.GetHashCode"></see> .</summary>
        /// <returns>An integer that represents the hash code for this rectangle.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return (((this.X ^ ((this.Y << 13) | (this.Y >> 0x13))) ^ ((this.Width << 0x1a) | (this.Width >> 6))) ^ ((this.Height << 7) | (this.Height >> 0x19)));
        }

        /// <summary>Inflates this <see cref="T:System.Drawing.Rectangle"></see> by the specified amount.</summary>
        /// <param name="width">The amount to inflate this <see cref="T:System.Drawing.Rectangle"></see> horizontally. </param>
        /// <param name="height">The amount to inflate this <see cref="T:System.Drawing.Rectangle"></see> vertically. </param>
        /// <filterpriority>1</filterpriority>
        public void Inflate(int width, int height)
        {
            this.X -= width;
            this.Y -= height;
            this.Width += 2 * width;
            this.Height += 2 * height;
        }

        /// <summary>Inflates this <see cref="T:System.Drawing.Rectangle"></see> by the specified amount.</summary>
        /// <param name="size">The amount to inflate this rectangle. </param>
        /// <filterpriority>1</filterpriority>
        public void Inflate(System.Drawing.Size size)
        {
            this.Inflate(size.Width, size.Height);
        }

        /// <summary>Creates and returns an inflated copy of the specified <see cref="T:System.Drawing.Rectangle"></see> structure. The copy is inflated by the specified amount. The original <see cref="T:System.Drawing.Rectangle"></see> structure remains unmodified.</summary>
        /// <returns>The inflated <see cref="T:System.Drawing.Rectangle"></see>.</returns>
        /// <param name="rect">The <see cref="T:System.Drawing.Rectangle"></see> with which to start. This rectangle is not modified. </param>
        /// <param name="y">The amount to inflate this <see cref="T:System.Drawing.Rectangle"></see> vertically. </param>
        /// <param name="x">The amount to inflate this <see cref="T:System.Drawing.Rectangle"></see> horizontally. </param>
        /// <filterpriority>1</filterpriority>
        public static Rect Inflate(Rect rect, int x, int y)
        {
            Rect rectangle1 = rect;
            rectangle1.Inflate(x, y);
            return rectangle1;
        }

        /// <summary>Replaces this <see cref="T:System.Drawing.Rectangle"></see> with the intersection of itself and the specified <see cref="T:System.Drawing.Rectangle"></see>.</summary>
        /// <param name="rect">The <see cref="T:System.Drawing.Rectangle"></see> with which to intersect. </param>
        /// <filterpriority>1</filterpriority>
        public void Intersect(Rect rect)
        {
            Rect rectangle1 = Rect.Intersect(rect, this);
            this.X = rectangle1.X;
            this.Y = rectangle1.Y;
            this.Width = rectangle1.Width;
            this.Height = rectangle1.Height;
        }

        /// <summary>Returns a third <see cref="T:System.Drawing.Rectangle"></see> structure that represents the intersection of two other <see cref="T:System.Drawing.Rectangle"></see> structures. If there is no intersection, an empty <see cref="T:System.Drawing.Rectangle"></see> is returned.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the intersection of a and b.</returns>
        /// <param name="a">A rectangle to intersect. </param>
        /// <param name="b">A rectangle to intersect. </param>
        /// <filterpriority>1</filterpriority>
        public static Rect Intersect(Rect a, Rect b)
        {
            int num1 = Math.Max(a.X, b.X);
            int num2 = Math.Min((int) (a.X + a.Width), (int) (b.X + b.Width));
            int num3 = Math.Max(a.Y, b.Y);
            int num4 = Math.Min((int) (a.Y + a.Height), (int) (b.Y + b.Height));
            if ((num2 >= num1) && (num4 >= num3))
            {
                return new Rect(num1, num3, num2 - num1, num4 - num3);
            }
            return Rect.Empty;
        }

        /// <summary>Determines if this rectangle intersects with rect.</summary>
        /// <returns>This method returns true if there is any intersection, otherwise false.</returns>
        /// <param name="rect">The rectangle to test. </param>
        /// <filterpriority>1</filterpriority>
        public bool IntersectsWith(Rect rect)
        {
            if (((rect.X < (this.X + this.Width)) && (this.X < (rect.X + rect.Width))) && (rect.Y < (this.Y + this.Height)))
            {
                return (this.Y < (rect.Y + rect.Height));
            }
            return false;
        }

        /// <summary>Gets a <see cref="T:System.Drawing.Rectangle"></see> structure that contains the union of two <see cref="T:System.Drawing.Rectangle"></see> structures.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> structure that bounds the union of the two <see cref="T:System.Drawing.Rectangle"></see> structures.</returns>
        /// <param name="a">A rectangle to union. </param>
        /// <param name="b">A rectangle to union. </param>
        /// <filterpriority>1</filterpriority>
        public static Rect Union(Rect a, Rect b)
        {
            int num1 = Math.Min(a.X, b.X);
            int num2 = Math.Max((int) (a.X + a.Width), (int) (b.X + b.Width));
            int num3 = Math.Min(a.Y, b.Y);
            int num4 = Math.Max((int) (a.Y + a.Height), (int) (b.Y + b.Height));
            return new Rect(num1, num3, num2 - num1, num4 - num3);
        }

        /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
        /// <param name="pos">Amount to offset the location. </param>
        /// <filterpriority>1</filterpriority>
        public void Offset(Point pos)
        {
            this.Offset(pos.X, pos.Y);
        }

        /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
        /// <param name="y">The vertical offset. </param>
        /// <param name="x">The horizontal offset. </param>
        /// <filterpriority>1</filterpriority>
        public void Offset(int x, int y)
        {
            this.X += x;
            this.Y += y;
        }

        /// <summary>Converts the attributes of this <see cref="T:System.Drawing.Rectangle"></see> to a human-readable string.</summary>
        /// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.Rectangle"></see> structure ¾ for example, {X=20, Y=20, Width=100, Height=50} </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        public override string ToString()
        {
            return ("{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + ",Width=" + this.Width.ToString(CultureInfo.CurrentCulture) + ",Height=" + this.Height.ToString(CultureInfo.CurrentCulture) + "}");
        }

        static Rect()
        {
            Rect.Empty = new Rect();
        }

    }
}

