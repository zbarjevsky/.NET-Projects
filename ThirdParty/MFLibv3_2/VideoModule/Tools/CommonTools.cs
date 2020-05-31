using MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoModule.Tools
{
    class CommonTools
    {
    }

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

        public static Task ExecuteInBacgroundThread(Action action)
        {
            var task = Task.Factory.StartNew(action);
            return task;
        }

        public static T ExecuteInBacgroundThread<T>(Func<T> func)
        {
            var task = Task<T>.Factory.StartNew(func);
            task.Wait();
            return task.Result;
        }
    }
}
