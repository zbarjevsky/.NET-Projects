using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopManagerUX.Utils
{
    public class SmartCollection<T> : ObservableCollection<T>
    {
        public SmartCollection()
            : base()
        {
        }

        public SmartCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public SmartCollection(List<T> list)
            : base(list)
        {
        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
                Items.Add(item);

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        //add new, replace existing
        //grow only
        public void UpdateAndAdd(IEnumerable<T> range)
        {
            foreach (T item in range)
            {
                int idx = FindItem(item);
                if (idx < 0)
                    Items.Add(item);
                else
                    Items[idx] = item;
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public int FindItem(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(Items[i]))
                    return i;
            }
            return -1;
        }
    }
}
