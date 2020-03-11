using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopManagerUX
{
    public class ViewModel : INotifyPropertyChanged
    {
        private MainWindow _wnd;

        public List<AppInfo> Apps { get; set; } = Logic.ListTasks();

        public AppChooserUserControl[,] AppChoosers = new AppChooserUserControl[2,2];

        public ViewModel(MainWindow wnd)
        {
            _wnd = wnd;
        }

        public void AddAppChooser(AppChooserUserControl ctrl, int row, int col, string selectedTitle)
        {
            AppChoosers[row,col] = ctrl;
            ctrl.SetVM(this, selectedTitle);
        }

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
