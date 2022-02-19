using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMaestro.Old
{
    public class Entry
    {
        private int index;
        private float v;

        public Entry(int index, float v)
        {
            this.index = index;
            this.v = v;
        }

        public float getY()
        {
            return v;
        }
    }
}
