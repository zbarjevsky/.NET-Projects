using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BarometerBT.Utils
{
    public class BMLineChart : BMChart
    {
        internal int getEntryCount()
        {
            return 1;
        }

        internal void addEntry(string v, Entry entry)
        {
        }

        internal void init(string v1, int v2)
        {
        }

        internal void setLabels(string[] vs1, Color[] vs2)
        {
        }

        internal Entry getEntry(string v, int i)
        {
            return new Entry(0, 1);
        }
    }
}
