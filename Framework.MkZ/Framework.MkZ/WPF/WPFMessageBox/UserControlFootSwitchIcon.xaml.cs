using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MkZ.WPF.MessageBox
{
    /// <summary>
    /// Interaction logic for UserControlFootSwitchIcon.xaml
    /// </summary>
    internal partial class UserControlFootSwitchIcon : UserControl, INotifyPropertyChanged
    {
        private Brush _nextBrush = new SolidColorBrush(Color.FromRgb(0x77, 0xBB, 0x1F));
        private Brush _prevBrush = new SolidColorBrush(Color.FromRgb(0xE2, 0x52, 0x05));
        private Brush _disabledBrush = Brushes.LightGray;

        public UserControlFootSwitchIcon()
        {
            InitializeComponent();

            GreenIconVisibility = Visibility.Hidden;
            OrangeIconVisibility = Visibility.Hidden;

            DataContext = this;

            this.IsEnabledChanged += UserControlFootSwitchIcon_IsEnabledChanged;
        }

        private void UserControlFootSwitchIcon_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OnPropertyChanged("NextColor");
            OnPropertyChanged("PrevColor");
        }

        private bool _isGreenVisible = true;

        public Visibility GreenIconVisibility
        {
            get { return _isGreenVisible ? Visibility.Visible : Visibility.Hidden; } 
            set { _isGreenVisible = (value == Visibility.Visible); OnPropertyChanged(); }
        }

        public Visibility OrangeIconVisibility
        {
            get { return _isGreenVisible ? Visibility.Hidden : Visibility.Visible; }
            set { _isGreenVisible = value != Visibility.Visible; OnPropertyChanged(); }
        }

        public Brush NextColor
        {
            get { return this.IsEnabled ? _nextBrush : _disabledBrush; }
            set { OnPropertyChanged(); }
        }

        public Brush PrevColor
        {
            get { return this.IsEnabled ? _prevBrush : _disabledBrush;  }
            set { OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
