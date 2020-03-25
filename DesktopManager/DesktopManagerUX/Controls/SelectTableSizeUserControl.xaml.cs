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
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopManagerUX.Controls
{
    /// <summary>
    /// Interaction logic for SelectTableSizeUserControl.xaml
    /// </summary>
    public partial class SelectTableSizeUserControl : UserControl, INotifyPropertyChanged
    {
        private string _selectedSizeText = "Select";
        public string SelectedSizeText 
        { 
            get { return _selectedSizeText; } 
            set { _selectedSizeText = value; OnPropertyChanged(); } 
        }

        private GridSizeData _selectedSize = new GridSizeData(2, 2);
        public GridSizeData SelectedSize
        {
            get { return _selectedSize; }
            set 
            { 
                _selectedSize = value;
                SelectedSizeText = "Size: " + _selectedSize.Rows + "x" + _selectedSize.Cols;
                OnPropertyChanged(); 
            }
        }

        /// <summary>
        /// Use this action to apply new size
        /// </summary>
        public Action<GridSizeData> OnSelectedSizeChangedAction = (size) => { }; 

        public SelectTableSizeUserControl()
        {
            this.DataContext = this;

            InitializeComponent();

            ObservableCollection<GridSelection> list = new ObservableCollection<GridSelection>()
            {
                new GridSelection(1),
                new GridSelection(2),
                new GridSelection(3)
            };

            dataGrid.ItemsSource = list;
        }

        private bool _canClose = true;
        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            SetSelectCellsInGrid();
            Keyboard.Focus(dataGrid);
            _canClose = true;
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                dgDropdown.IsChecked = false;
                dgDropdown.Focus();
            }
        }

        private void dataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _canClose = false; //start selection cells
        }

        private void dataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetSelectedSize(new List<DataGridCellInfo>(dataGrid.SelectedCells));
            dgDropdown.IsChecked = false;
            OnSelectedSizeChangedAction(SelectedSize);
            dgDropdown.Focus();
        }

        private void dataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            //if there was no change in selection
            if (_canClose)
                dgDropdown.IsChecked = false;
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _canClose = false;
            GetSelectedSize(new List<DataGridCellInfo>(dataGrid.SelectedCells));
        }

        private void SetSelectCellsInGrid()
        {
            //remember BEFORE selection change
            int rows = _selectedSize.Rows;
            int cols = _selectedSize.Cols;

            dataGrid.SelectedCells.Clear();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    DataGridCell cell = dataGrid.GetCell(row, col);
                    if(cell != null)
                        cell.IsSelected = true;
                }
            }
        }

        private void GetSelectedSize(List<DataGridCellInfo> list)
        {
            Debug.WriteLine("==========================================================");

            int max_row = 0;
            int max_col = 0;
            int min_row = 3;
            int min_col = 3;
            for (int i = 0; i < list.Count; i++)
            {
                GridSelection sel = list[i].Item as GridSelection;
                int col = sel.GetRowCol(list[i].Column.SortMemberPath);
                int row = sel.Row;

                if (row > max_row) max_row = row;
                if (row < min_row) min_row = row;
                if (col > max_col) max_col = col;
                if (col < min_col) min_col = col;

                Debug.WriteLine(list[i].Column.SortMemberPath + " === " + list[i].Item);
            }

            SelectedSize = new GridSizeData((max_row - min_row + 1), (max_col - min_col + 1));

            Debug.WriteLine("c" + min_col + ", r" + min_row + ", == c" + max_col + ", r" + max_row);
            Debug.WriteLine(SelectedSizeText);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GridSelection
    {
        public string col1 { get; }
        public string col2 { get; }
        public string col3 { get; }

        public int Row = 0;

        public GridSelection(int row)
        {
            Row = row;
            col1 = "1x" + row;
            col2 = "2x" + row;
            col3 = "3x" + row;
        }

        public override string ToString()
        {
            return col1 + ", " + col2 + ", " + col3;
        }

        public int GetRowCol(string col)
        {
            if (nameof(col1) == col)
                return 1;
            if (nameof(col2) == col)
                return 2;
            if (nameof(col3) == col)
                return 3;
            throw new NotImplementedException();
        }
    }
}
