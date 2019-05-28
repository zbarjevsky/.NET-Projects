using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeditationStopWatch.Tools
{
    public class GlobalMessageFilter : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public Action<Point> MouseMovedAction = (point) => { };

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                Point point = new Point(m.LParam.ToInt32());
                MouseMovedAction(point);
            }

            // Always allow message to continue to the next filter control
            return false;
        }

        #endregion
    }
}
