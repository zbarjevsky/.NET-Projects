using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscillator_DETA
{
    public class FreqBuffer
    {
        public double base_freq = 10.0;
        public double modulate_freq = 15000.0;
        public double duration_ms = 1000.0;

        public double[] buffer;
    }

    public class FreqSequence
    {
        public FreqBuffer[] sequence;
    }
}
