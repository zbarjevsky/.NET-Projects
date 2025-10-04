using MkZ.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace MultiPlayer
{

    public class BookmarkColors
    {
        public static SolidColorBrush BookmarkBrushA { get; } = System.Windows.Media.Brushes.PaleGreen;
        public static SolidColorBrush BookmarkBrush1 { get; } = System.Windows.Media.Brushes.MistyRose;
        public static SolidColorBrush BookmarkBrush2 { get; } = System.Windows.Media.Brushes.LightYellow;
        public static SolidColorBrush BookmarkBrush3 { get; } = System.Windows.Media.Brushes.Plum;
        public static SolidColorBrush BookmarkBrush4 { get; } = System.Windows.Media.Brushes.DeepSkyBlue;
        public static SolidColorBrush BookmarkBrush5 { get; } = System.Windows.Media.Brushes.RosyBrown;
        public static SolidColorBrush BookmarkBrush6 { get; } = System.Windows.Media.Brushes.Olive;
        public static SolidColorBrush BookmarkBrush7 { get; } = System.Windows.Media.Brushes.Coral;
        public static SolidColorBrush BookmarkBrush8 { get; } = System.Windows.Media.Brushes.Khaki;
        public static SolidColorBrush BookmarkBrush9 { get; } = System.Windows.Media.Brushes.Peru;

        public static readonly SolidColorBrush[] Brushes =
        {
            BookmarkBrushA, BookmarkBrush1, BookmarkBrush2, BookmarkBrush3, BookmarkBrush4, BookmarkBrush5, BookmarkBrush6, BookmarkBrush7, BookmarkBrush8, BookmarkBrush9,
        };
    }

    /// <summary>
    /// Interaction logic for BookmarkButtonControl.xaml
    /// </summary>
    public partial class BookmarkButtonControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GotoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(BookmarkButtonControl), new PropertyMetadata(false));

        public double Position
        {
            get { return (double)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GotoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(double), typeof(BookmarkButtonControl), new PropertyMetadata(0.0, OnPositionChanged));

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (BookmarkButtonControl)d;
            control?.PropertyChanged?.Invoke(control, new PropertyChangedEventArgs(nameof(TooltipText_GO)));
            control?.PropertyChanged?.Invoke(control, new PropertyChangedEventArgs(nameof(TooltipText_SET)));
        }

        public ICommand GotoCommand
        {
            get { return (ICommand)GetValue(GotoCommandProperty); }
            set { SetValue(GotoCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GotoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GotoCommandProperty =
            DependencyProperty.Register(nameof(GotoCommand), typeof(ICommand), typeof(BookmarkButtonControl), new PropertyMetadata());

        public string CommandParameter
        {
            get { return (string)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GotoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(string), typeof(BookmarkButtonControl), new PropertyMetadata());

        public ICommand SetCommand
        {
            get { return (ICommand)GetValue(SetCommandProperty); }
            set { SetValue(SetCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GotoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetCommandProperty =
            DependencyProperty.Register(nameof(SetCommand), typeof(ICommand), typeof(BookmarkButtonControl), new PropertyMetadata());

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GotoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(BookmarkButtonControl), new PropertyMetadata("Q", OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (BookmarkButtonControl)d;
            control?.PropertyChanged?.Invoke(control, new PropertyChangedEventArgs(nameof(TooltipText_GO)));
            control?.PropertyChanged?.Invoke(control, new PropertyChangedEventArgs(nameof(TooltipText_SET)));
        }

        public SolidColorBrush ActiveBrush
        {
            get { return (SolidColorBrush)GetValue(ActiveBrushProperty); }
            set { SetValue(ActiveBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GotoCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveBrushProperty =
            DependencyProperty.Register(nameof(ActiveBrush), typeof(SolidColorBrush), typeof(BookmarkButtonControl), new PropertyMetadata(BookmarkColors.BookmarkBrushA));

        public string TooltipText_GO
        {
            get 
            {
                if (Position > 0)
                    return $"Go To Bookmark ({Text}) -> {VideoCommandsVM.SecondsToString(Position)}";
                return $"Go To Bookmark ({Text})"; 
            }
        }

        public string TooltipText_SET
        {
            get { return $"Set Bookmark ({Text})"; }
        }

        public BookmarkButtonControl()
        {
            InitializeComponent();
        }

        private void ReplayClear_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
