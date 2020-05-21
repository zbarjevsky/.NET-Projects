using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MZ.WPF
{
    public static class WPFUtils
    {
        public static void ExecuteOnUIThread(Action action)
        {
            ExecuteOnUIThread(() => { action(); return 0; }); 
        }

        public static T ExecuteOnUIThread<T>(Func<T> action)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                return Application.Current.Dispatcher.Invoke(() =>
                {
                    return action.Invoke();
                });
            }
            else
            {
                return action.Invoke();
            }
        }
    }
}
