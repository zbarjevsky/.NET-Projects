using MkZ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MkZ.WPF
{
    public static class WPFUtils
    {
        public static void ExecuteOnUIThread(Action action)
        {
            ExecuteOnUIThread(() => { action(); return 0; }); 
        }

        public static DispatcherOperation ExecuteOnUiThreadBeginInvoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (Application.Current != null && !Application.Current.Dispatcher.CheckAccess())
                return Application.Current.Dispatcher.BeginInvoke(action, priority);
            else
                action.Invoke();

            return null;
        }

        public static void ExecuteOnUiThreadInvoke(Action action,
            DispatcherPriority priority = DispatcherPriority.Normal,
            [CallerMemberName] string propertyName = null)
        {
            try
            {
                if (Application.Current != null && !Application.Current.Dispatcher.CheckAccess())
                    Application.Current.Dispatcher.Invoke(action, priority);
                else
                    action.Invoke();
            }
            catch (Exception ex)
            {
                Log.e("ExecuteOnUiThreadInvoke({0}), exeption: {1}\n", propertyName, ex);
            }
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
