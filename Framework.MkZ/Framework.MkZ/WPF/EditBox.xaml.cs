using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MkZ.WPF
{
    /// <summary>
    /// Interaction logic for EditBox.xaml
    /// </summary>
    public partial class EditBox : UserControl
    {
        public event Action<EditBox, string> TextChanged = (editBox, newText) => { };

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(EditBox),
                new PropertyMetadata(string.Empty, OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EditBox txt = d as EditBox;
            if (txt != null && txt.TextChanged != null)
                txt.TextChanged(txt, txt.Text);
        }

        public EditBox()
        {
            InitializeComponent();

            _clickCount = 0;
            _txtBox.Visibility = Visibility.Collapsed;
            _txtBlock.Visibility = Visibility.Visible;
        }

        private int _clickCount = 0;
        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _clickCount++;
            Debug.WriteLine("UserControl_MouseLeftButtonUp count: {0}, Focus: {1} Text: {2}", _clickCount, _txtBox.IsFocused, Text);
            if (_clickCount == 2)
            {
                _txtBlock.Visibility = Visibility.Collapsed;
                _txtBox.Visibility = Visibility.Visible;
                Keyboard.Focus(_txtBox);
            }
        }

        private void _txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged(this, _txtBox.Text);
        }

        private void _txtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            OnLostFocus();
        }

        private void _txtBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void _txtBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
                OnLostFocus();
        }

        private void OnLostFocus()
        {
            Debug.WriteLine("OnLostFocus: {0}, Text: {1}", _clickCount, Text);

            //_txtBox.SelectedText = "";
            _txtBox.Visibility = Visibility.Collapsed;
            _txtBlock.Visibility = Visibility.Visible;
            _clickCount = 0;
        }
    }
}
