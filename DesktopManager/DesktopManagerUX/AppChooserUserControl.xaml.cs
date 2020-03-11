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
            cmbApps.SelectedIndex = FindApp(selectedTitle);

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
                imagePreview.Source = Logic.CaptureApplication(SelectedApp.Process, _VM.DPI);
                txtInfo.Content = imagePreview.Source.Width + "x" + imagePreview.Source.Height;
            }
            else
            {
                imagePreview.Source = null;
                txtInfo.Content = "Select Application";
            }
        }

        private int FindApp(string title)
        {
            int pos = title.LastIndexOf("(");
            int end = title.LastIndexOf(")");
            if (pos < 0 || end < 0)
                return 0;

            string processInTitle = title.Substring(pos + 1, end - pos - 1);

            for (int i = 1; i < _VM.Apps.Count; i++)
            {
                string processName = _VM.Apps[i].Process.ProcessName;
                if (processInTitle == processName)
                    return i;
            }
            return 0;
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
