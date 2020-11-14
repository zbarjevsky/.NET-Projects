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
                CommonTools.ExecuteOnUiThreadBeginInvoke(() => PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

    }
}
