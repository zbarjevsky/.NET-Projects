using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MkZ.WPF.DragDrop
{
    public class ListViewDragDropHelper<ItemType> where ItemType : class
    {
        ListView _listView;
        ListViewDragDropManager<ItemType> dragMgr = new ListViewDragDropManager<ItemType>();

        public void AllowDragReorder(ListView list, bool enablerDragReorder)
        {
            if(enablerDragReorder)
            {
                _listView = list;

                // This is all that you need to do, in order to use the ListViewDragManager.
                this.dragMgr.ListView = list;
                this.dragMgr.ShowDragAdorner = true;
                this.dragMgr.DragAdornerOpacity = 0.5;

                list.DragEnter += OnListView_DragEnter;
                list.Drop += OnListView_Drop;
            }
            else
            {
                list.DragEnter -= OnListView_DragEnter;
                list.Drop -= OnListView_Drop;

                _listView = null;
                this.dragMgr.ListView = null;
            }
        }

        private void OnListView_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        private void OnListView_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Effects == DragDropEffects.None)
                return;

            ItemType item = e.Data.GetData(typeof(ItemType)) as ItemType;
            if (sender == this._listView) //drag from list2 to list one
            {
                if (this.dragMgr.IsDragInProgress)
                    return;

                // An item was dragged from the bottom ListView into the top ListView
                // so remove that item from the bottom ListView.
                // ??(this.listView2.ItemsSource as ObservableCollection<ItemType>).Remove(item);
            }
            else if(sender is ListView list2) //move from list1 to list 2
            {
                //??if (this.dragMgr2.IsDragInProgress)
                //??    return;

                // An item was dragged from the top ListView into the bottom ListView
                // so remove that item from the top ListView.
                (this._listView.ItemsSource as ObservableCollection<ItemType>).Remove(item);
            }
        }
    }
}
