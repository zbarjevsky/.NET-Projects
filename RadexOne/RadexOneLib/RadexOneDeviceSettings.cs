using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.RadexOneLib
{
    public class RadexOneDeviceSettings
    {
        //beep on/off
        public bool Sound { get; set; }

        //vibration on/off
        public bool Vibrate { get; set; }

        //alert threshold
        public double Threshold { get; set; }
    }
}
