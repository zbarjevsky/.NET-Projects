using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MkZ.MediaPlayer.Utils
{
    public static class CustomCursor
    {
        public static void InsreaseCursor(this Window wnd, Visual vis)
        {
            Mouse.OverrideCursor = CreateCursor(50, 50, Brushes.Gold, vis);
        }

        public static Cursor CreateCursor(double rx, double ry, SolidColorBrush brush, Visual visual)
        {
            var vis = new DrawingVisual();
            using (var dc = vis.RenderOpen())
            {
                dc.DrawRectangle(brush, new Pen(Brushes.Black, 0.1), new Rect(0, 0, rx, ry));
                dc.Close();
            }
            var rtb = new RenderTargetBitmap(64, 64, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(vis);

            using (var ms1 = new MemoryStream())
            {
                var penc = new PngBitmapEncoder();
                penc.Frames.Add(BitmapFrame.Create(rtb));
                penc.Save(ms1);

                var pngBytes = ms1.ToArray();
                var size = pngBytes.GetLength(0);

                //.cur format spec http://en.wikipedia.org/wiki/ICO_(file_format)
                using (var ms = new MemoryStream())
                {
                    {//ICONDIR Structure
                        ms.Write(BitConverter.GetBytes((Int16)0), 0, 2);//Reserved must be zero; 2 bytes
                        ms.Write(BitConverter.GetBytes((Int16)2), 0, 2);//image type 1 = ico 2 = cur; 2 bytes
                        ms.Write(BitConverter.GetBytes((Int16)1), 0, 2);//number of images; 2 bytes
                    }

                    {//ICONDIRENTRY structure
                        ms.WriteByte(32); //image width in pixels
                        ms.WriteByte(32); //image height in pixels

                        ms.WriteByte(0); //Number of Colors in the color palette. Should be 0 if the image doesn't use a color palette
                        ms.WriteByte(0); //reserved must be 0

                        //ms.Write(BitConverter.GetBytes((Int16)(rx / 2.0)), 0, 2);//2 bytes. In CUR format: Specifies the horizontal coordinates of the hotspot in number of pixels from the left.
                        //ms.Write(BitConverter.GetBytes((Int16)(ry / 2.0)), 0, 2);//2 bytes. In CUR format: Specifies the vertical coordinates of the hotspot in number of pixels from the top.
                        ms.Write(BitConverter.GetBytes((Int16)(0)), 0, 2);//2 bytes. In CUR format: Specifies the horizontal coordinates of the hotspot in number of pixels from the left.
                        ms.Write(BitConverter.GetBytes((Int16)(0)), 0, 2);//2 bytes. In CUR format: Specifies the vertical coordinates of the hotspot in number of pixels from the top.

                        ms.Write(BitConverter.GetBytes(size), 0, 4);//Specifies the size of the image's data in bytes
                        ms.Write(BitConverter.GetBytes((Int32)22), 0, 4);//Specifies the offset of BMP or PNG data from the beginning of the ICO/CUR file
                    }

                    ms.Write(pngBytes, 0, size);//write the png data.
                    ms.Seek(0, SeekOrigin.Begin);
                    return new Cursor(ms);
                }
            }
        }

        private static Cursor CreateCursorObject(
            int size, double xHotPointRatio, double yHotPointRatio, BitmapSource rtb)
        {
            using (var ms1 = new MemoryStream())
            {
                var penc = new PngBitmapEncoder();
                penc.Frames.Add(BitmapFrame.Create(rtb));
                penc.Save(ms1);

                var pngBytes = ms1.ToArray();
                var byteCount = pngBytes.GetLength(0);

                //.cur format spec <a href="http://en.wikipedia.org/wiki/ICO_(file_format"><font color="#0066cc">http://en.wikipedia.org/wiki/ICO_(file_format</font></a>)
                using (var stream = new MemoryStream())
                {
                    //ICONDIR Structure
                    stream.Write(BitConverter.GetBytes((Int16)0), 0, 2); //Reserved must be zero; 2 bytes
                    stream.Write(BitConverter.GetBytes((Int16)2), 0, 2); //image type 1 = ico 2 = cur; 2 bytes
                    stream.Write(BitConverter.GetBytes((Int16)1), 0, 2); //number of images; 2 bytes
                                                                         //ICONDIRENTRY structure
                    stream.WriteByte(32); //image width in pixels
                    stream.WriteByte(32); //image height in pixels

                    stream.WriteByte(0); //Number of Colors. Should be 0 if the image doesn't use a color palette
                    stream.WriteByte(0); //reserved must be 0

                    stream.Write(BitConverter.GetBytes((Int16)(size * xHotPointRatio)), 0, 2);
                    //2 bytes. In CUR format: Specifies the number of pixels from the left.
                    stream.Write(BitConverter.GetBytes((Int16)(size * yHotPointRatio)), 0, 2);
                    //2 bytes. In CUR format: Specifies the number of pixels from the top.

                    //Specifies the size of the image's data in bytes
                    stream.Write(BitConverter.GetBytes(byteCount), 0, 4);
                    stream.Write(BitConverter.GetBytes((Int32)22), 0, 4);
                    //Specifies the offset of BMP or PNG data from the beginning of the ICO/CUR file
                    stream.Write(pngBytes, 0, byteCount); //write the png data.
                    stream.Seek(0, SeekOrigin.Begin);

                    return new System.Windows.Input.Cursor(stream);
                }
            }
        }
    }
}
