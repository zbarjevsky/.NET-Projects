using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MkZ.WPF.WpfAnalogClock
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public Action<string> OnPropertyChange = (prop) => { };

        public OptionsWindow(object settings)
        {
            InitializeComponent();

            propertyGrid.SelectedObject = settings;

            propertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;
        }

        private void PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            OnPropertyChange(e.ChangedItem.Label);
        }
    }
}
