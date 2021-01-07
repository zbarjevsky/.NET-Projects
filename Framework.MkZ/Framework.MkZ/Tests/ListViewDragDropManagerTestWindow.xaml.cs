using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MkZ.WPF.DragDrop;

namespace ListViewDragDropManagerDemo
{
	/// <summary>
	/// Demonstrates how to use the ListViewDragManager class.
	/// </summary>
	public partial class ListViewDragDropManagerTestWindow : System.Windows.Window
	{
		ListViewDragDropManager<DragDropTestChoreObject> dragMgr1;
		ListViewDragDropManager<DragDropTestChoreObject> dragMgr2;

		public ListViewDragDropManagerTestWindow()
		{
			InitializeComponent();
			this.Loaded += Window1_Loaded;
		}

		#region Window1_Loaded

		void Window1_Loaded( object sender, RoutedEventArgs e )
		{
			// Give the ListView an ObservableCollection of Task 
			// as a data source.  Note, the ListViewDragManager MUST
			// be bound to an ObservableCollection, where the collection's
			// type parameter matches the ListViewDragManager's type
			// parameter (in this case, both have a type parameter of Task).
			ObservableCollection<DragDropTestChoreObject> tasks = DragDropTestChoreObject.CreateTasks();
			this.listView1.ItemsSource = tasks;

			this.listView2.ItemsSource = new ObservableCollection<DragDropTestChoreObject>();

			// This is all that you need to do, in order to use the ListViewDragManager.
			this.dragMgr1 = new ListViewDragDropManager<DragDropTestChoreObject>( this.listView1 );
			this.dragMgr2 = new ListViewDragDropManager<DragDropTestChoreObject>( this.listView2 );

			// Turn the ListViewDragManager on and off. 
			this.chkManageDragging.Checked += delegate { this.dragMgr1.ListView = this.listView1; };
			this.chkManageDragging.Unchecked += delegate { this.dragMgr1.ListView = null; };

			// Show and hide the drag adorner.
			this.chkDragAdorner.Checked += delegate { this.dragMgr1.ShowDragAdorner = true; };
			this.chkDragAdorner.Unchecked += delegate { this.dragMgr1.ShowDragAdorner = false; };

			// Change the opacity of the drag adorner.
			this.sldDragOpacity.ValueChanged += delegate { this.dragMgr1.DragAdornerOpacity = this.sldDragOpacity.Value; };

			// Apply or remove the item container style, which responds to changes
			// in the attached properties of ListViewItemDragState.
			this.chkApplyContStyle.Checked += delegate { this.listView1.ItemContainerStyle = this.FindResource( "ItemContStyle" ) as Style; };
			this.chkApplyContStyle.Unchecked += delegate { this.listView1.ItemContainerStyle = null; };

			// Use or do not use custom drop logic.
			this.chkSwapDroppedItem.Checked += delegate { this.dragMgr1.ProcessDrop += dragMgr_ProcessDrop; };
			this.chkSwapDroppedItem.Unchecked += delegate { this.dragMgr1.ProcessDrop -= dragMgr_ProcessDrop; };

			// Show or hide the lower ListView.
			this.chkShowOtherListView.Checked += delegate { this.listView2.Visibility = Visibility.Visible; };
			this.chkShowOtherListView.Unchecked += delegate { this.listView2.Visibility = Visibility.Collapsed; };

			// Hook up events on both ListViews to that we can drag-drop
			// items between them.
			this.listView1.DragEnter += OnListViewDragEnter;
			this.listView2.DragEnter += OnListViewDragEnter;
			this.listView1.Drop += OnListViewDrop;
			this.listView2.Drop += OnListViewDrop;
		}

		#endregion // Window1_Loaded

		#region dragMgr_ProcessDrop

		// Performs custom drop logic for the top ListView.
		void dragMgr_ProcessDrop( object sender, ProcessDropEventArgs<DragDropTestChoreObject> e )
		{
			// This shows how to customize the behavior of a drop.
			// Here we perform a swap, instead of just moving the dropped item.

			int higherIdx = Math.Max( e.OldIndex, e.NewIndex );
			int lowerIdx = Math.Min( e.OldIndex, e.NewIndex );

			if( lowerIdx < 0 )
			{
				// The item came from the lower ListView
				// so just insert it.
				e.ItemsSource.Insert( higherIdx, e.DataItem );
			}
			else
			{
				// null values will cause an error when calling Move.
				// It looks like a bug in ObservableCollection to me.
				if( e.ItemsSource[lowerIdx] == null ||
					e.ItemsSource[higherIdx] == null )
					return;

				// The item came from the ListView into which
				// it was dropped, so swap it with the item
				// at the target index.
				e.ItemsSource.Move( lowerIdx, higherIdx );
				e.ItemsSource.Move( higherIdx - 1, lowerIdx );
			}

			// Set this to 'Move' so that the OnListViewDrop knows to 
			// remove the item from the other ListView.
			e.Effects = DragDropEffects.Move;
		}

		#endregion // dragMgr_ProcessDrop

		#region OnListViewDragEnter

		// Handles the DragEnter event for both ListViews.
		void OnListViewDragEnter( object sender, DragEventArgs e )
		{
			e.Effects = DragDropEffects.Move;
		}

		#endregion // OnListViewDragEnter

		#region OnListViewDrop

		// Handles the Drop event for both ListViews.
		void OnListViewDrop( object sender, DragEventArgs e )
		{
			if( e.Effects == DragDropEffects.None )
				return;

			DragDropTestChoreObject task = e.Data.GetData( typeof( DragDropTestChoreObject ) ) as DragDropTestChoreObject;
			if( sender == this.listView1 )
			{
				if( this.dragMgr1.IsDragInProgress )
					return;

				// An item was dragged from the bottom ListView into the top ListView
				// so remove that item from the bottom ListView.
				(this.listView2.ItemsSource as ObservableCollection<DragDropTestChoreObject>).Remove( task );
			}
			else
			{
				if( this.dragMgr2.IsDragInProgress )
					return;

				// An item was dragged from the top ListView into the bottom ListView
				// so remove that item from the top ListView.
				(this.listView1.ItemsSource as ObservableCollection<DragDropTestChoreObject>).Remove( task );
			}
		}

		#endregion // OnListViewDrop

	}
}