using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MkZ.WPF
{
    //http://csharphelper.com/blog/2017/01/convert-a-bitmap-into-a-cursor-in-c/
    public static class CursorFromControl
    {
        public static Cursor Create(UserControl control, Size desiredSize, Point ptHotSpot = default(Point))
        {
            RenderTargetBitmap bmp = CreateBitmap(control, desiredSize);

            SaveBitmap(bmp);

            Cursor cur = CreateCursor(bmp, (int)ptHotSpot.X, (int)ptHotSpot.Y);

            return cur;
        }

        //public static System.Windows.Forms.Cursor Convert(System.Windows.Input.Cursor cursor)
        //{
        //    return new System.Windows.Forms.Cursor(cursor.)
        //}

        public static RenderTargetBitmap CreateBitmap(UserControl control, Size desiredSize)
        {
            control.Width = desiredSize.Width;
            control.Height = desiredSize.Height;

            control.Measure(desiredSize);
            control.Arrange(new Rect(desiredSize));
            control.UpdateLayout();

            RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)desiredSize.Width, (int)desiredSize.Height,
                96, 96, PixelFormats.Pbgra32);

            bmp.Render(control);

            return bmp;
        }

        //https://gist.github.com/pascaljulmy/45672071e2344e38ed24bedcebdd813f
        private static Cursor CreateCursor(BitmapSource bitmapSource, int hotspotX, int hotspotY)
        {
            using (var ms1 = new MemoryStream())
            {
                var pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                pngEncoder.Save(ms1);

                var pngBytes = ms1.ToArray();
                var size = pngBytes.GetLength(0);

                using (var ms = new MemoryStream())
                {
                    //Reserved must be zero; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)0), 0, 2);

                    //image type 1 = ico 2 = cur; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)2), 0, 2);

                    //number of images; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)1), 0, 2);

                    //image width in pixels
                    ms.WriteByte(32);

                    //image height in pixels
                    ms.WriteByte(32);

                    //Number of Colors in the color palette. Should be 0 if the image doesn't use a color palette
                    ms.WriteByte(0);

                    //reserved must be 0
                    ms.WriteByte(0);

                    //2 bytes. In CUR format: Specifies the horizontal coordinates of the hotspot in number of pixels from the left.
                    ms.Write(BitConverter.GetBytes((short)hotspotX), 0, 2);
                    //2 bytes. In CUR format: Specifies the vertical coordinates of the hotspot in number of pixels from the top.
                    ms.Write(BitConverter.GetBytes((short)hotspotY), 0, 2);

                    //Specifies the size of the image's data in bytes
                    ms.Write(BitConverter.GetBytes(size), 0, 4);

                    //Specifies the offset of BMP or PNG data from the beginning of the ICO/CUR file
                    ms.Write(BitConverter.GetBytes(22), 0, 4);

                    ms.Write(pngBytes, 0, size); //write the png data.
                    ms.Seek(0, SeekOrigin.Begin);
                    return new Cursor(ms);
                }
            }
        }

        public static void SaveBitmap(RenderTargetBitmap bmp)
        {
            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bmp));

            string fileName = @"c:\Temp\test1.png";
            if (File.Exists(fileName))
                File.Delete(fileName);

            using (Stream stm = File.Create(fileName, 128000, FileOptions.WriteThrough))
                encoder.Save(stm);
        }
    }
}
