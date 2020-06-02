using DesktopManagerUX.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopManagerUX
{
    public class ViewModel : INotifyPropertyChanged
    {
        private MainWindow _wnd;

        public SmartCollection<AppInfo> Apps { get; } = new SmartCollection<AppInfo>();

        public ViewModel(Window wnd)
        {
            Debug.Assert(wnd != null);

            _wnd = wnd as MainWindow;

            //if version changed - settings location changed - get settings from previous location/version
            if (Properties.Settings.Default.UpgradeNeeded)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeNeeded = false;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }

            ReloadApps();
        }

        public void ActivateMainWindow()
        {
            _wnd.Activate();
        }

        public void ReloadApps() 
        {
            Apps.UpdateAndAdd(Logic.GetAppsWithUI());
        }

        //public void AddAppChooser(AppChooserUserControl ctrl, int row, int col)
        //{
        //    string selectedTitle = AppChoosersGrid.GetSetting(row, col);
        //    AppChoosers[row,col] = ctrl;
        //    ctrl.SetVM(this, selectedTitle);
        //}

        public System.Windows.Point DPI
        {
            get 
            {
                PresentationSource presentationsource = PresentationSource.FromVisual(_wnd);

                if (presentationsource != null) // make sure it's connected
                {
                    return new System.Windows.Point()
                    {
                        X = presentationsource.CompositionTarget.TransformToDevice.M11,
                        Y = presentationsource.CompositionTarget.TransformToDevice.M22
                    };
                }

                return new System.Windows.Point(); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
