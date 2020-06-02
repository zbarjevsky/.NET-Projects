using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MeditationStopWatch.Tools
{
    public sealed class SoundUtils
    {
        public static List<string> GetOutDevices()
        {
            return new List<string>(DirectSoundOut.Devices.Select(dev => dev.Description));
        }

        public static void SetActiveOutDevice(string name)
        {

        }
    }
}
