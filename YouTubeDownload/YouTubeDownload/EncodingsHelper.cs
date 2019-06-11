using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDownload
{
    public class EncodingsHelper
    {
        //OEM code pages:
        //437 (US)
        //720 (Arabic)
        //737 (Greek)
        //775 (Baltic)
        //850 (Multilingual Latin I)
        //852 (Latin II)
        //855 (Cyrillic)
        //857 (Turkish)
        //858 (Multilingual Latin I + Euro)
        //862 (Hebrew)
        //866 (Russian)

        public static int CodePage()
        {
            var codePage = Console.OutputEncoding.CodePage;

            int lcid = GetSystemDefaultLCID();
            var ci = System.Globalization.CultureInfo.GetCultureInfo(lcid);
            int page = ci.TextInfo.OEMCodePage;
            return page;
        }

        public static string Convert(string s, Encoding e)
        {
            byte[] decoded = e.GetBytes(s);
            string text = Encoding.UTF8.GetString(decoded);
            string txt1 = DecodeFromUtf8(text);
            System.Diagnostics.Debug.WriteLine(" == " + txt1);
            return txt1;
        }

        public static string DecodeFromUtf8(string utf8String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetSystemDefaultLCID();
    }
}
