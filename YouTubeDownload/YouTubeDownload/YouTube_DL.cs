using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouTubeDownload.Extensions;

namespace YouTubeDownload
{
    //https://github.com/ytdl-org/youtube-dl/blob/master/README.md#readme
    public class YouTube_DL
    {
        public const string DL = @"Dependencies/youtube-dl.exe";

        internal static async Task Update()
        {
            await new Process
            {
                StartInfo = new ProcessStartInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DL), "-U")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            }.RunAndWaitForExitAsync();
        }

        public static Process Create(string arguments)
        {
            return new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(DL, arguments)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
        }

        public static string GetVersion()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DL);
                //Version ver = AssemblyName.GetAssemblyName(path).Version;
                var versionInfo = FileVersionInfo.GetVersionInfo(path);
                return versionInfo.ProductName + " ver: " + versionInfo.ProductVersion;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
