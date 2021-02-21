using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


using WindowColors.Utils;
using MkZ.Windows;
using Microsoft.Win32;
using System.Diagnostics;

namespace WindowColors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://stackoverflow.com/questions/5094447/how-do-i-use-the-correct-windows-system-colors
    /// https://blogs.windows.com/windowsdeveloper/2018/10/10/fluent-xaml-theme-editor-preview-released/
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            Debug.WriteLine("SystemEvents_UserPreferenceChanged: " + e.Category);
            ComboBox_SelectionChanged(sender, null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox_SelectionChanged(sender, null);
        }

        private void ButtonEditColor_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                if(btn.DataContext is SysColorVM col)
                {
                    string key = (_cmbRegistryKeys.SelectedItem as ComboBoxItem).Content.ToString();

                    System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
                    dlg.Color = System.Drawing.Color.FromArgb(col.Brush.Color.A, col.Brush.Color.R, col.Brush.Color.G, col.Brush.Color.B);
                    dlg.AllowFullOpen = true;
                    dlg.FullOpen = true;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        col.SetFormsColor(dlg.Color, key, apply: true);
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listColors == null || _cmbRegistryKeys == null || _cmbRegistryKeys.SelectedItem == null)
                return;

            if (_cmbRegistryKeys.SelectedItem is ComboBoxItem item)
            {
                string key = item.Content.ToString();
                if (key == ColorsHelper.WIN10_COLOR_REG_KEY)
                {
                    _listColors.ItemsSource = ColorsHelper.GetWin10Specific();
                }
                else
                {
                    _listColors.ItemsSource = ColorsHelper.GetSysColorList(key);

                }
            }
        }

        private void ListColorsHeader_Click(object sender, RoutedEventArgs e)
        {
            if(e.OriginalSource is GridViewColumnHeader col)
            {
                List<SysColorVM> sysColorList = _listColors.ItemsSource as List<SysColorVM>;
                if (col.Content.ToString() == "ID")
                {
                    _listColors.ItemsSource = null;
                    sysColorList.Sort((c1, c2) => c1.Index - c2.Index);
                    _listColors.ItemsSource = sysColorList;
                }

                if (col.Content.ToString() == "Name")
                {
                    _listColors.ItemsSource = null;
                    sysColorList.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                    _listColors.ItemsSource = sysColorList;
                }
            }
        }
    }
}
