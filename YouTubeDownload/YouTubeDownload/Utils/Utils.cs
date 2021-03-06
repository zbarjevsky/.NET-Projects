using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MkZ.Framework.Tools;

namespace YouTubeDownload.Utils
{
    public static class Utils
    {
        //delay indirect load Framework
        public static bool UpdateAssemblyInfoVersion(string[] args)
        {
            return (AssemblyTools.UpdateAssemblyInfoVersion(args));
        }
    }
}
