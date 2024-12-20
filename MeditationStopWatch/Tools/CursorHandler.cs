﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

                    for (int i = 0; i < 2; i++)
                    {
                        System.Windows.Forms.Cursor.Show();
                        ReferenceCount++;
                    }
                }
                else //hide
                {
                    if (ReferenceCount <= 0)
                        return;

                    while (ReferenceCount > 0)
                    {
                        System.Windows.Forms.Cursor.Hide();
                        ReferenceCount--;
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(System.Drawing.Point pt);

    }
}
