using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeditationStopWatch
{
    public static class CursorHandler
    {
        private static int ReferenceCount = 2;

        public static bool IsCursorVisible
        {
            get { return ReferenceCount >= 2; }
            set
            {
                if(value) //show
                {
                    if (ReferenceCount >= 2)
                        return;

                    ReferenceCount++;
                    System.Windows.Forms.Cursor.Show();
                    ReferenceCount++;
                    System.Windows.Forms.Cursor.Show();
                }
                else //hide
                {
                    if (ReferenceCount <= 0)
                        return;

                    while (ReferenceCount-- > 0)
                        System.Windows.Forms.Cursor.Hide();
                }
            }
        }
    }
}
