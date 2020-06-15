using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace ClipboardManager
{
	public static class ImagesUtil
	{
		public static Image GetTwinImage(this ImageList list, int idx1, int idx2)
		{
			Image ico1 = list.Images[idx1];
			Image ico2 = list.Images[idx2];

			return Combine(ico1, ico2);
		}//end Combine

		public static Image Combine(Image ico1, Image ico2)
		{
			Bitmap bmp = new Bitmap(33, 16); //16 + 1 + 16
			using (Graphics oGraphics = Graphics.FromImage(bmp))
			{
				oGraphics.DrawImage(ico1, 0, 0, 16, 16);
				oGraphics.DrawImage(ico2, 17, 0, 16, 16);
			}//end using

			return bmp;
		}//end Combine
	}//end class ImagesUtil
}//end namespace ClipboardListener
