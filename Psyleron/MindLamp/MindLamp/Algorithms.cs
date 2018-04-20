using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindLamp
{
    public class Algorithms
    {
        public List<byte> m_queue = new List<byte>(300000);
        public int m_absolute = 0;
        public double m_relative = 0;

        public void AddRange(byte [] data)
        {
            //restrict size
            if (m_queue.Count > 800000)
                m_queue.RemoveRange(0, 400000);

            m_queue.AddRange(data);
        }

        public void CalculateDeviation(int countFromEnd)
        {
            int odd = 0;
            int even = 0;
            StringBuilder sb = new StringBuilder(3 * countFromEnd);
            for (int i = m_queue.Count - 1; i >= 0 && i > m_queue.Count - countFromEnd; i--)
            {
                byte item = m_queue[i];
                sb.Append(item);
                sb.Append("\t");

                if (item % 2 == 0)
                    even++;
                else
                    odd++;
            }

            m_absolute = (even - odd);
            m_relative = (double)(even - odd) / (double)(even + odd);
        }
    }
}
