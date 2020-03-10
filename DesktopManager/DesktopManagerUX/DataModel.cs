using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DesktopManagerUX
{
    public class DataModel
    {
    }

    public class AppInfo
    {
        private Process p;

        public AppInfo(Process p)
        {
            this.p = p;
            Name = p.ProcessName;
        }

        public Process Process { get { return p; } set { p = value; } }
        public string Name { get; set;}
        public SolidColorBrush Color { get; set; } = Brushes.AliceBlue;
    }
}
