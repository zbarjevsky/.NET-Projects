using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace HotPixelTest
{
	public partial class PictureControl : PictureBox
	{
		private int _x, _y, _zoom;
		private Bitmap _bmp;
		private bool _bShowPixel;

		public PictureControl()
		{
			InitializeComponent();
		}

		public void SetPicture(Bitmap bmp, int xPixel, int yPixel, int zoom, bool bShowPixel)
		{
			_bmp = bmp;
			_x = xPixel;
			_y = yPixel;
			_zoom = zoom;
			_bShowPixel = bShowPixel;

			Invalidate();
		}
		public void Clear()
		{
			_bmp = null;
			Invalidate();
		}

		private void PictureControl_Paint(object sender, PaintEventArgs e)
		{
			Graphics gControl = e.Graphics;
			gControl.SmoothingMode = SmoothingMode.HighQuality;

			// Create a Bitmap image in memory and set its CompositingMode
			Point ptHotPixel = new Point();
			Bitmap bmp = CreateBitmap(_x, _y, ClientRectangle.Width, ClientRectangle.Height, ref ptHotPixel);
			if (bmp == null)
			{
				gControl.FillRectangle(Brushes.Gray, ClientRectangle);
				return;
			}

			Graphics gBmp = Graphics.FromImage(bmp);
			gBmp.CompositingMode = CompositingMode.SourceCopy;

			// draw a red circle to the bitmap in memory
			Color red = Color.FromArgb(0x60, 0xff, 0, 0);
			Pen redPen = new Pen(red, 1);
			if (_bShowPixel)
			{
				int radius = 5 * _zoom;
				if (radius < 15) radius = 15;
				Rectangle rectHotPixel = new Rectangle(ptHotPixel.X * _zoom, ptHotPixel.Y * _zoom,
					radius, radius);

				int offset = -radius / 2 + _zoom /2;
				rectHotPixel.Offset(offset, offset);

				gBmp.DrawEllipse(redPen, rectHotPixel);
				gBmp.DrawEllipse(redPen, ptHotPixel.X * _zoom + _zoom / 2, ptHotPixel.Y * _zoom + _zoom / 2, 2, 2);
			}

			// draw the bitmap on our window
			gControl.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);

			bmp.Dispose();
			gBmp.Dispose();
			redPen.Dispose();
		}

		private Bitmap CreateBitmap(int xPixel, int yPixel, int width, int height, ref Point ptHotPixel)
		{
			if (_bmp == null)
				return null;

			width += _zoom;
			height += _zoom;
			Bitmap tmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			width /= _zoom;
			height /= _zoom;

			int x = GetStartOffset(xPixel, width, _bmp.Width);
			int xTmpPixel = xPixel - x; //pixel coordinats relative to new sumbnail bitmap
			int y = GetStartOffset(yPixel, height, _bmp.Height);
			int yTmpPixel = yPixel - y;

			ptHotPixel = new Point(xTmpPixel, yTmpPixel);

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					Color c = _bmp.GetPixel(x + i, y + j);
					for (int tmpX = 0; tmpX < _zoom; tmpX++)
					{
						for (int tmpY = 0; tmpY < _zoom; tmpY++)
						{
							tmp.SetPixel(i * _zoom + tmpX, j * _zoom + tmpY, c);
						}
					}

				}
			}

			return tmp;
		}

		private int GetStartOffset(int pos, int width, int big_width)
		{
			int left = pos - width / 2;
			if (left < 0) { left = 0; }
			if (left > big_width - width) left = big_width - width;
			return left;
		}
	}
}
