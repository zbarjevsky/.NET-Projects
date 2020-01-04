using CircularGauge;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpeedGauge
{
    /// <summary>
    /// Interaction logic for Gauge180UserControl.xaml
    /// </summary>
    public partial class Gauge180UserControl : UserControl, INotifyPropertyChanged
    {
        private Gauge180ViewModel _viewModel = new Gauge180ViewModel();

        public Gauge180UserControl()
        {
            this.DataContext = _viewModel;
            InitializeComponent();
        }

        public string Speed
        {
            get { return _viewModel.SpeedValue; }
            set { _viewModel.SpeedValue = value; OnPropertyChanged(); }
        }

        public string SpeedUnits
        {
            get { return _viewModel.SpeedUnits; }
            set { _viewModel.SpeedUnits = value; OnPropertyChanged(); }
        }

        public HighlightBrush.HighlightColor SpeedColor
        {
            get { return _viewModel.SpeedHighlightBrush; }
            set { _viewModel.SpeedHighlightBrush = value; OnPropertyChanged(); }
        }

        public double MaxSpeed
        {
            get { return _viewModel.MaxValue; }
            set { _viewModel.MaxValue = value; OnPropertyChanged(); }
        }

        public bool ShowTestSlider
        {
            get { return sliTest.Visibility == System.Windows.Visibility.Visible; }
            set { sliTest.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
