using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.Utils
{
    public class NotifyPropertyChangedImpl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChangedAll()
        {
            NotifyPropertyChanged("");
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                CommonTools.ExecuteOnUiThreadInvoke(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)),
                    System.Windows.Threading.DispatcherPriority.Normal, propertyName);
            }
        }

        public bool SetProperty<T>(ref T prop, T val, [CallerMemberName] string propertyName = null)
        {
            if (prop.Equals(val))
                return false;

            prop = val;
            NotifyPropertyChanged(propertyName);

            return true;
        }
    }
}
