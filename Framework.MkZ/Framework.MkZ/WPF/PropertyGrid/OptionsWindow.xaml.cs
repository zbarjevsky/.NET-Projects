using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MkZ.WinForms;

namespace MkZ.WPF.PropertyGrid
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    ///http://www.java2s.com/Tutorial/CSharp/0470__Windows-Presentation-Foundation/HostingaWindowsFormsPropertyGridinWPF.htm
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        public void SetPropertiesObject(object o, params string [] expandNames)
        {
            _propertyGrid.SelectedObject = o;
            _propertyGrid.PropertySort = PropertySort.NoSort;

            foreach (string name in expandNames)
            {
                _propertyGrid.ExpandGridItem(name);
            }
        }

        public void ExpandAll()
        {
            _propertyGrid.ExpandAllGridItems();
        }
    }
}
