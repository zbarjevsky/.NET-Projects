using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopManagerUX
{
    /// <summary>
    /// Interaction logic for AppChooserUserControl.xaml
    /// </summary>
    public partial class AppChooserUserControl : UserControl
    {
        private ViewModel _VM = null;

        public AppChooserUserControl()
        {
            InitializeComponent();
        }

        public void Init(ViewModel vm, int row, int col)
        {
            _VM = vm;
            this.DataContext = vm;
            cmbApps.ItemsSource = vm.Apps;

            string selectedTitle = Logic.SettingGet(row, col);
            cmbApps.SelectedIndex = AppInfo.FindApp(selectedTitle, _VM.Apps);

            borderMain.BorderThickness = ThicknessFromRowCol(row, col);
        }

        public bool HasSelection { get { return cmbApps.SelectedIndex > 0 && cmbApps.SelectedIndex < _VM.Apps.Count; } }

        public AppInfo SelectedApp
        {
            get { if(HasSelection) return _VM.Apps[cmbApps.SelectedIndex]; return null; }
        }

        private void cmbApps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HasSelection)
            {
                imagePreview.Source = Logic.CaptureApplication(SelectedApp.HWND, _VM.DPI);
                if (imagePreview.Source != null)
                    txtInfo.Content = "Current Size: " + imagePreview.Source.Width + "x" + imagePreview.Source.Height;
                else
                    txtInfo.Content = "Select Another Application";
                
                //Logic.SettingSave()
            }
            else
            {
                imagePreview.Source = null;
                txtInfo.Content = "Select Window";
            }
        }

        private Thickness ThicknessFromRowCol(int row, int col)
        {
            int rows = _VM.AppChoosers.GetLength(0);
            int cols = _VM.AppChoosers.GetLength(1);

            const double thin = 1, thick = 3;

            double left = (col == 0) ? thick: thin;
            double right = (col == cols-1) ? thick : thin;
            double top = (row == 0) ? thick : thin;
            double bottom = (row == rows - 1) ? thick: thin;

            return new Thickness(left, top, right, bottom);
        }
    }
}
