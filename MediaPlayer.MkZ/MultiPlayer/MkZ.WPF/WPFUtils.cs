using MkZ.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Application = System.Windows.Application;
using Pen = System.Windows.Media.Pen;

namespace MkZ.WPF
{
    public static class WPFUtils
    {
        public static bool GetInDesignMode()
        {
            if (Application.Current == null) 
                return false;
            
            if (Application.Current.Properties != null && Application.Current.Properties.Contains("InDesignMode"))
            {
                bool inDesignMode = (bool)Application.Current.Properties["InDesignMode"];
                Debug.WriteLine("DesignModeCheck1: " + inDesignMode);
                return inDesignMode;
            }

            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                Debug.WriteLine("DesignModeCheck2: true");
                return true;
            }

            return false;
        }

        public static void UpdateGridLinesStyle(System.Windows.Media.Brush color)
        {
            var tGridLinesRenderer = Type.GetType(
                "System.Windows.Controls.Grid+GridLinesRenderer," +
                " PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

            var gridLinesRenderer = Activator.CreateInstance(tGridLinesRenderer);
            Type t = gridLinesRenderer.GetType();

            Pen penOdd = new Pen(color, 0.5);
            //penOdd.DashStyle = DashStyles.Dot;
            t.GetField("s_oddDashPen", BindingFlags.Static | BindingFlags.NonPublic).SetValue(gridLinesRenderer, penOdd);

            Pen penEven = new Pen(color, 0.5);
            //penEven.DashStyle = DashStyles.Dash;
            t.GetField("s_evenDashPen", BindingFlags.Static | BindingFlags.NonPublic).SetValue(gridLinesRenderer, penEven);
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
