using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace ClipboardManager
{
	class ImagesUtil
	{
		public static Image get(int idx1, int idx2, ImageList list)
		{
			Image ico1 = list.Images[idx1];
			Image ico2 = list.Images[idx2];

			return Combine(ico1, ico2);
		}//end Combine

		public static Image Combine(Image ico1, Image ico2)
		{
			Bitmap NewBitmap = new Bitmap(33, 16);
			using (Graphics oGraphics = Graphics.FromImage(NewBitmap))
			{
				oGraphics.DrawImage(ico1, 0, 0, 16, 16);
				oGraphics.DrawImage(ico2, 17, 0, 16, 16);
			}//end using

			return NewBitmap;
		}//end Combine
	}//end class ImagesUtil
}//end namespace ClipboardListener
