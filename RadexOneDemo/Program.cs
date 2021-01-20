﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


using MkZ.Tools;

namespace RadexOneDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!SingleInstance())
                return; //already running

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        private static bool SingleInstance()
        {
            return !(SingleInstanceHelper.GlobalShowWindow(FormMain.TITLE));
        }//end SingleInstance
    }
}
