using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Tools
{
    public static class Shell32_Icons
    {
        public static ImageList SmallImageList = new ImageList();
        public static ImageList LargeImageList = new ImageList();

        public static List<Icon> SmallIconsList = new List<Icon>();
        public static List<Icon> LargeIconsList = new List<Icon>();

        static Shell32_Icons()
        {
            SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            SmallImageList.ImageSize = new Size(16, 16);
            
            LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            LargeImageList.ImageSize = new Size(32, 32);

            IntPtr large;
            IntPtr small;
            int count = ExtractIconEx("shell32.dll", -1, out large, out small, 1);

            for (int i = 0; i < count; i++)
            {
                try
                {
                    int res = ExtractIconEx("shell32.dll", i, out large, out small, 1);

                    SmallIconsList.Add(Icon.FromHandle(small));
                    LargeIconsList.Add(Icon.FromHandle(large));

                    SmallImageList.Images.Add(SmallIconsList[i]);
                    LargeImageList.Images.Add(LargeIconsList[i]);
                }
                catch (Exception err)
                {
                    Debug.WriteLine("Load Icons Error: " + err.Message);
                    break;
                }
            }
        }

        public static void SaveImages(string folder, List<Icon> icons)
        {
            if (icons == null || icons.Count == 0)
                return;

            Directory.CreateDirectory(folder);
            Directory.CreateDirectory(Path.Combine(folder, "Png"));
            Directory.CreateDirectory(Path.Combine(folder, "Ico"));

            int size = icons[0].Width;

            for (int i = 0; i < icons.Count; i++)
            {
                
                string fileNamePng = Path.Combine(folder, "Png", string.Format("Icon_{0:000}_{1}.png", i, size));
                icons[i].ToBitmap().Save(fileNamePng, ImageFormat.Png);

                string fileNameIco = Path.Combine(folder, "Ico", string.Format("Icon_{0:000}_{1}.ico", i, size));
                icons[i].ToBitmap().Save(fileNameIco, ImageFormat.Icon);
                //using (FileStream fs = new FileStream(fileNameIco, FileMode.Create))
                //    icons[i].Save(fs);
            }
        }

        public static Icon Extract(string file, int number, bool largeIcon)
        {
            IntPtr large;
            IntPtr small;
            ExtractIconEx(file, number, out large, out small, 1);
            try
            {
                return Icon.FromHandle(largeIcon ? large : small);
            }
            catch
            {
                return null;
            }
        }

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);
    }
}
