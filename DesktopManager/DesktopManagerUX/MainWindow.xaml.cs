using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using DesktopManagerUX.Utils;
using MkZ.Windows.DwmApi;
using MkZ.Windows.Win32API;
using MkZ.WPF.Utils;

namespace DesktopManagerUX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Background);

        public MainWindow()
        {
            AppContext.Init(this);

            InitializeComponent();

            this.DataContext = AppContext.ViewModel;

            tabLayouts.Items.Clear();
            ReloadLayouts();

            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResizeThisProportionallyToDesktopSize();

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private int _iTimerCounter = 0;
        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_iTimerCounter++ % 6 == 0) //every 6 seconds
            {
                if (_chkAutoSaveAll.IsChecked.Value)
                    _ = SaveAllOpenWindowsAsync();
            }

            if (_chkAutoSaveAll.IsChecked.Value)
                _txtAutoSaveAll.Text += ".";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            tabLayouts.ItemsSource = null;
            AppContext.Configuration.Save();
            Properties.Settings.Default.Save();
        }

        private void ReloadLayouts(int selectTab = 0)
        {
            //copy
            ObservableCollection<LayoutConfiguration> layouts = new ObservableCollection<LayoutConfiguration>(AppContext.Configuration.Layouts);
            layouts.Add(new LayoutConfiguration(LayoutConfiguration.LayoutType.CreateNewTab, "*"));

            tabLayouts.ItemsSource = layouts;
            tabLayouts.SelectedIndex = selectTab;
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            //if display config changed - stop saving app positions
            _chkAutoSaveAll.IsChecked = false;
            _timer.Stop();

            AppContext.Configuration.SmartDisplaysUpdate();

            this.Activate();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseSelected_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveLayout_Click(object sender, RoutedEventArgs e)
        {
            if (AppContext.Configuration.Layouts.Count <= 1)
                return; //must have at least one

            if (sender is Button btn)
            {
                if (btn.DataContext is LayoutConfiguration layout)
                {
                    int idx = AppContext.Configuration.Layouts.IndexOf(layout);
                    if (idx >= 0)
                    {
                        int selected = tabLayouts.SelectedIndex;
                        AppContext.Configuration.Layouts.RemoveAt(idx);

                        //select tab that is in place of deleted - if was selected
                        if(idx == selected) //delete active
                        {
                            if (selected >= AppContext.Configuration.Layouts.Count) //was last
                                selected--;
                        }
                        else //if was not selected - retain selection
                        {
                            if (selected > idx)
                                selected--;
                        }

                        if(selected < 0)
                            selected = 0;

                        ReloadLayouts(selected); //tab before
                    }
                }
            }
        }

        //click inside TextBox does not select tab - do it here
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox txt)
            {
                if (txt.DataContext is LayoutConfiguration layout)
                {
                    if (layout.TabType == LayoutConfiguration.LayoutType.Layout)
                    {
                        int idx = AppContext.Configuration.Layouts.IndexOf(layout);
                        if (idx >= 0)
                            tabLayouts.SelectedIndex = idx;
                    }
                    else if (layout.TabType == LayoutConfiguration.LayoutType.CreateNewTab)
                    {
                        AddLayout_Click(sender, e);
                    }
                }
            }
        }

        private void TabLayouts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabLayouts.SelectedItem is LayoutConfiguration layout)
            {
                if (layout.TabType == LayoutConfiguration.LayoutType.CreateNewTab)
                {
                    AddLayout_Click(sender, e);
                }
            }
        }

        private void AddLayout_Click(object sender, RoutedEventArgs e)
        {
            string name = GenerateNewLayoutName();
            AppContext.Configuration.Layouts.Add(new LayoutConfiguration(LayoutConfiguration.LayoutType.Layout, name));
            ReloadLayouts(AppContext.Configuration.Layouts.Count - 1); //last
        }

        private string GenerateNewLayoutName()
        {
            for (int i = 0; i < 200; i++)
            {
                LayoutConfiguration found = AppContext.Configuration.Layouts.FirstOrDefault(l => l.Name.StartsWith("Layout " + (i + 1)));
                if (found == null)
                    return "Layout " + (i + 1);
            }
            return "<<>>";
        }

        private void ResizeThisProportionallyToDesktopSize()
        {
            WpfScreen screen = WpfScreen.GetScreenFrom(this);
            Rect bounds = screen.WorkingArea;

            double ratio = bounds.Width / bounds.Height;
            double height = tabLayouts.ActualHeight;
            double width = height * ratio;

            //add borders
            height += 130;
            width += 12;

            if (width > bounds.Width || width > bounds.Height)
            {
                width = bounds.Width - 12;
                width = bounds.Height - 12;
            }

            this.Width = width;
            this.Height = height;
        }

        private List<WindowStatePosition> _windowStatePositions = new List<WindowStatePosition>();

        private async Task SaveAllOpenWindowsAsync()
        {
            _timer.Stop();

            _txtAutoSaveAll.Text = "Saving...";

            int count = 0;
            await Task.Run(() =>
            {
                lock (_windowStatePositions)
                {
                    _windowStatePositions.Clear();
                    List<WindowInfo> windows = EnumOpenWindows.GetOpenWindows();
                    for (int i = 0; i < windows.Count; i++)
                    {
                        WindowInfo info = windows[i];
                        System.Diagnostics.Debug.WriteLine("Save Window: " + info);
                        WindowStatePosition w = new WindowStatePosition(info);
                        _windowStatePositions.Add(w);
                    }
                    count = windows.Count;
                }
            });

            _txtAutoSaveAll.Text = $"(Saved: {count})";

            _timer.Start();
        }

        private void SaveAll_Click(object sender, RoutedEventArgs e)
        {
            SaveAllOpenWindowsAsync();
        }

        private void RestoreAll_Click(object sender, RoutedEventArgs e)
        {
            lock (_windowStatePositions)
            {
                List<WindowInfo> windows = EnumOpenWindows.GetOpenWindows(includeInvisible:false);
                for (int i = windows.Count - 1; i >= 0; i--)
                {
                    WindowInfo info = windows[i];
                    System.Diagnostics.Debug.WriteLine("Restore Window: " + info);

                    WindowStatePosition pos = _windowStatePositions.FirstOrDefault(w => w.hWnd == info.hWnd && w.Title == info.Title);
                    if (pos != null && pos.IsValid())
                    {
                        pos.SetWindowPlacement();
                    }
                }
            }
            //_chkAutoSaveAll.IsChecked = true;
            _timer.Start();
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            _chkAutoSaveAll.IsChecked = false;
            _timer.Stop();
            this.Close();
        }

        private void AutoSaveAll_Click(object sender, RoutedEventArgs e)
        {
            if (_chkAutoSaveAll.IsChecked.Value)
            {
                _txtAutoSaveAll.Text = "Init.";
                _timer.Start();
            }
        }
    }
}
