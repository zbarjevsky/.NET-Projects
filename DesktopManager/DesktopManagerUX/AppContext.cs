using MkZ.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopManagerUX
{
    public class AppContext
    {
        public static void Init(MainWindow wnd)
        {
            if(!CommonUtils.IsAdministrator())
            {
                CommonUtils.ErrorMessage("Desktop Manager need to run as Administrator!");
                throw new UnauthorizedAccessException();
            }

            Configuration = Configuration.Load();
            ViewModel = new ViewModel(wnd);
            Logic = new Logic();
        }

        public static volatile bool Sync = false;

        public static ViewModel ViewModel { get; private set; } 

        public static Logic Logic { get; private set; }

        public static Configuration Configuration { get; private set; }
    }
}
