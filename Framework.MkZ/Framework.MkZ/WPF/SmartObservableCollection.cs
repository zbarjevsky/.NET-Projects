using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MkZ.WPF.Utils
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

            this.NotifyCollectionChanged();
        }

        //replace whole collection
        public void Update(IList<T> newItems)
        {
            //remove missing
            for (int i = Items.Count -1; i >= 0; i--)
            {
                int idx = FindItem(Items[i], newItems);
                if (idx < 0) //not found
                    Items.Remove(Items[i]);
            }

            //add new
            for (int i = 0; i < newItems.Count; i++)
            {
                int idx = FindItem(newItems[i], Items);
                if (idx < 0)
                    Items.Add(newItems[i]);
            }

            this.NotifyCollectionChanged();
        }

        //add new, replace existing
        //grow only
        public void UpdateAndAdd(IEnumerable<T> range)
        {
            foreach (T item in range)
            {
                int idx = FindItem(item, Items);
                if (idx < 0)
                    Items.Add(item);
                else
                    Items[idx] = item;
            }

            this.NotifyCollectionChanged();
        }

        private void NotifyCollectionChanged()
        {
            //System.Windows.Application.Current.Dispatcher.BeginInvoke(new MethodInvoker(() =>
            //{
                this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            //}));
        }

        public static int FindItem(T item, IList<T> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (item.Equals(collection[i]))
                    return i;
            }
            return -1;
        }
    }
}
