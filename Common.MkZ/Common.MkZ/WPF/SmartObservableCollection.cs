using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZ.WPF.Utils
{
    public class SmartObservableCollection<T> : ObservableCollection<T>
    {
        public SmartObservableCollection()
            : base()
        {
        }

        public SmartObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public SmartObservableCollection(List<T> list)
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

        //replace whole collection
        public void Update(IEnumerable<T> range)
        {
            Items.Clear();
            AddRange(range);
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
