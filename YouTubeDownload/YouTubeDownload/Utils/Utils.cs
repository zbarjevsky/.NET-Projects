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

        public static void SetDoubleBuffered(this Control c, bool value)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, value, null);
        }
    }
}
