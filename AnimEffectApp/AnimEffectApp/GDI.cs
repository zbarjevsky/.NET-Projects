using System;
using System.Runtime.InteropServices;
using System.Drawing;


namespace AnimEffectApp
{
	/// <summary>
	/// Class with GDI functions and constants.
	/// </summary>
	internal class GDI
	{
		// Set screen's hwnd constant
		private IntPtr ScreenHWND = (IntPtr.Zero);

		private IntPtr hDC;
		private IntPtr hPEN;
		private PenStyles CPenStyle;
		private DrawingModes CDrawMode;
		private int CPenWidth;
		private int CPenColor;
		private bool PenCreated;

		private Int32 OldROP;

		public GDI()
		{
			// Get screen's device context
			hDC = GetDC(ScreenHWND);

			// Indicates if pen object created and
			// attached to screen's device context
			PenCreated = false;

			CPenWidth = 1;
			CPenColor = 0;
			CPenStyle = PenStyles.PS_SOLID;
		}

		public GDI(DrawingModes DM, PenStyles PS, int PW, int PC)
		{
			// Get screen's device context
			hDC = GetDC(ScreenHWND);

			// Indicates if pen object created and
			// attached to screen's device context
			PenCreated = true;

			// Apply new drawing modes, colors e.t.c.
			this.CPenWidth = PW;
			this.CPenColor = PC;
			this.CPenStyle = PS;
			this.CDrawMode = DM;

			// Obtain system resources
			hPEN = CreatePen(PS, PW, PC);
			OldROP = SetROP2(hDC, DM);
			SelectObject(hDC, hPEN);
		}

		public PenStyles PenStyle
		{
			set
			{
				CPenStyle = value;

				UpdatePen();
			}
			get
			{
				return (this.CPenStyle);
			}
		}

		public int PenWidth
		{
			set
			{
				CPenWidth = value;

				UpdatePen();
			}
			get
			{
				return (this.CPenWidth);
			}
		}

		public int PenColor
		{
			set
			{
				CPenColor = value;

				UpdatePen();
			}
			get
			{
				return (this.CPenColor);
			}
		}

		public DrawingModes DrawMode
		{
			set
			{
				if (value != this.CDrawMode)
				{
					this.CDrawMode = value;
					SetROP2(hDC, CDrawMode);
				}
			}
			get
			{
				return (this.CDrawMode);
			}
		}

		public void MoveTo(Point pt)
		{
			MoveToEx(hDC, pt.X, pt.Y, 0);
		}//end MoveTo

		public void PolylineTo(Point[] pts)
		{
			PolylineTo(hDC, pts, pts.Length);
		}//end PolylineTo

		public void DrawLine(int X1, int Y1, int X2, int Y2)
		{
			MoveToEx(hDC, X1, Y1, 0);
			LineTo(hDC, X2, Y2);
		}

		public void DrawLine(Point P1, Point P2)
		{
			DrawLine(P1.X, P1.Y, P1.X, P1.Y);
		}

		public void DrawLineStrip(Point[] LineStrip)
		{
			MoveToEx(hDC, LineStrip[0].X, LineStrip[0].Y, 0);

			for (int Idx = 1; Idx < LineStrip.Length; Idx++)
			{
				LineTo(hDC, LineStrip[Idx].X, LineStrip[Idx].Y);
			}

			LineTo(hDC, LineStrip[0].X, LineStrip[0].Y);

			/// foreach (Point P in LineStrip)
			/// {
			///        LineTo(hDC, P.X, P.Y);
			/// }
		}

		public void DrawEllipse(int X1, int Y1, int X2, int Y2)
		{
		}

		public void DrawEllipse(Point Location, Point Size)
		{
		}

		public void DrawEllipse(Rectangle Rect)
		{
		}

		public static int RGB(int R, int G, int B)
		{
			return (R | (G << 8) | (B << 16));
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		~GDI()
		{
			// Delete pen, if exists
			if (PenCreated)
				DeleteObject(hPEN);

			// Release screen's device context
			ReleaseDC(hDC, ScreenHWND);
		}

		private void UpdatePen()
		{
			if (PenCreated)
				DeleteObject(hPEN);

			hPEN = CreatePen(CPenStyle, CPenWidth, CPenColor);
			SelectObject(hDC, hPEN);
			PenCreated = true;
		}

		/*private struct POINTAPI 
		{
			public int x;
			public int y;
		}*/


		[DllImport("gdi32.dll")]
		private static extern Int32 SetROP2(IntPtr hDC,
			DrawingModes nDrawMode);

		[DllImport("gdi32.dll")]
		private static extern bool MoveToEx(IntPtr hDC,
			int x,
			int y,
			int OldPoint);
		//POINTAPI lpPoint);

		[DllImport("gdi32.dll")]
		public static extern IntPtr GdiFlush();

		[DllImport("gdi32.dll")]
		private static extern bool LineTo(IntPtr hDC,
			int x,
			int y);

		[DllImport("gdi32.dll")]
		private static extern IntPtr PolylineTo(IntPtr hDC,
			Point[] pts,
			int count);

		[DllImport("gdi32.dll")]
		private static extern IntPtr CreatePen(PenStyles nPenStyle,
			int nWidth,
			int crColor);

		[DllImport("gdi32.dll")]
		private static extern IntPtr SelectObject(IntPtr hDC,
			IntPtr hObject);

		[DllImport("gdi32.dll")]
		private static extern bool DeleteObject(IntPtr hObject);

		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport("user32.dll")]
		private static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

		/* Binary raster ops */
		public enum DrawingModes : int
		{
			R2_BLACK = 1,        /* Pixel is always 0.*/
			R2_NOTMERGEPEN = 2,  /* Pixel is the inverse of the R2_MERGEPEN color. */
			R2_MASKNOTPEN = 3,   /* Pixel is a combination of the colors common to 
                                  * both the screen and the inverse of the pen.*/
			R2_NOTCOPYPEN = 4,   /* Pixel is the inverse of the pen color.*/
			R2_MASKPENNOT = 5,   /* Pixel is a combination of the colors common to
                                  * both the pen and the inverse of the screen.*/
			R2_NOT = 6,          /* Pixel is the inverse of the screen color.*/
			R2_XORPEN = 7,       /* Pixel is a combination of the colors in the pen
                                  * and in the screen, but not in both.*/
			R2_NOTMASKPEN = 8,   /* Pixel is the inverse of the R2_MASKPEN color.*/
			R2_MASKPEN = 9,      /* Pixel is a combination of the colors common to
                                  * both the pen and the screen.*/
			R2_NOTXORPEN = 10,   /* Pixel is the inverse of the R2_XORPEN color.*/
			R2_NOP = 11,         /* Pixel remains unchanged.*/
			R2_MERGENOTPEN = 12, /* Pixel is a combination of the screen color and
                                  * the inverse of the pen color.*/
			R2_COPYPEN = 13,     /* Pixel is the pen color.*/
			R2_MERGEPENNOT = 14, /* Pixel is a combination of the pen color and the
                                  * inverse of the screen color. */
			R2_MERGEPEN = 15,    /* Pixel is a combination of the pen color and the
                                  * screen color.*/
			R2_WHITE = 16,       /* Pixel is always 1.*/
			R2_LAST = 16         /* Use old pen.*/
		}

		/* Pen Styles */
		public enum PenStyles : int
		{
			PS_SOLID = 0,        /* The pen is solid. */
			PS_DASH = 1,       /* -------  The pen is dashed. This style is valid only 
                                 * when the pen width is one or less in device units.*/
			PS_DOT = 2,         /* .......  The pen is dotted. This style is valid only
                                 * when the pen width is one or less in device units.*/
			PS_DASHDOT = 3,     /* _._._._  The pen has alternating dashes and dots.
                                 * This style is valid only when the pen width is one or
                                 * less in device units.*/
			PS_DASHDOTDOT = 4,  /* _.._.._  The pen has alternating dashes and double
                                 * dots. This style is valid only when the pen width is
                                 * one or less in device units.*/
			PS_NULL = 5,        /* The pen is invisible. */
			PS_INSIDEFRAME = 6, /* The pen is solid. When this pen is used in any GDI
                                 * drawing function that takes a bounding rectangle, the
                                 * dimensions of the figure are shrunk so that it fits
                                 * entirely in the bounding rectangle, taking into account
                                 * the width of the pen. This applies only to geometric
                                 * pens. */
			PS_USERSTYLE = 7,
			PS_ALTERNATE = 8,
			PS_STYLE_MASK = 0x0000000F
		}
	}
}