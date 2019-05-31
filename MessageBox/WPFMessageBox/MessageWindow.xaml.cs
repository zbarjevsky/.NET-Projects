using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

// <summary>
// MarkZ. 2017-08-01
// </summary>
namespace MZ.WPF.MessageBox
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// Resizable, Movable, Customazable 
    /// Enabled Copy Text
    /// Thread safe
    /// Foot pedal F5, F6
    /// sTextAlignment = TextAlignment.Center
    /// sWindowStartupLocation = WindowStartupLocation.CenterOwner
    /// 
    /// Usage: MessageWindow.Error("Hello World");
    /// 
    /// Another implementation: http://blogs.microsoft.co.il/arik/2011/05/26/a-customizable-wpf-messagebox/
    /// </summary>
    internal partial class MessageWindow : Window
    {
        //commands for foot pedal - F5 and F6
        public ICommand DefaultCommand { get; set; }
        public ICommand EscapeCommand { get; set; }
        public ICommand F5Command { get; set; }
        public ICommand F6Command { get; set; }
        public ICommand CopyCommand { get; set; }

        public static Visibility IconType1Visibility { get; set; } = Visibility.Visible;
        public static Visibility IconType2Visibility { get; set; } = Visibility.Collapsed;

        private MessageBoxResult _DialogResult = MessageBoxResult.None;
        private MessageBoxButton _buttons = MessageBoxButton.OKCancel;
        private MessageBoxResult _defaultButton = MessageBoxResult.None;

        /// <summary>
        /// Change the message text Alingment
        /// </summary>
        public static TextAlignment sTextAlignment = TextAlignment.Center;
        public static WindowStartupLocation sWindowStartupLocation = WindowStartupLocation.CenterOwner;

        public static MessageBoxResult MessageBox(UIElement owner, 
            ref string message, string title,
            MessageBoxImage icon, 
            TextAlignment textAlignment, 
            MessageBoxButton buttons, string btnF6text, string btnF5text, string btn1text, MessageBoxResult defaultButton, 
            int autoCloseTimeoutMs = Timeout.Infinite, //infinite
            bool bReadonly = true)
        {
            MessageWindow wnd = new MessageWindow(buttons, defaultButton);
            wnd.WindowStartupLocation = WindowStartupLocation.Manual; // sWindowStartupLocation;
            wnd.Owner = GetWindowImpl(owner);
            wnd.ConfigureAppearance(icon);
            wnd.Title = title; //for task bar visible tetx
            wnd.txtMessage.TextAlignment = textAlignment;
            wnd.txtMessage.Text = message;
            wnd.txtMessage.IsReadOnly = bReadonly;
            //wnd.txtMessage.ToolTip = message;
            wnd.txtTitle.Text = title;
            //wnd.txtTitle.ToolTip = title;

            wnd.btn1.Content = btn1text;
            wnd.btnF5.Content = btnF5text;
            wnd.btnF6.Content = btnF6text;

            wnd.AdjustSize();
            if (owner != null)
                wnd.CenterToUIElement(owner);
            else
                wnd.CenterToMainWindow();

            //if timeout is set and window is not closed after timeout - close it
            Thread t = wnd.CloseWindowOnTimeout(autoCloseTimeoutMs);

            wnd.ShowDialog();

            //if closed before timeout - abort thread
            if(t != null)
                t.Abort();

            //get text from input box - return value
            message = wnd.txtMessage.Text;

            return wnd._DialogResult;
        }

        public MessageWindow(MessageBoxButton buttons, MessageBoxResult defaultButton)
        {
            _buttons = buttons;
            _defaultButton = defaultButton;

            InitializeComponent();

            DataContext = this;

            DefaultCommand = new RelayCommand((o) =>
            {
                if (btn1.IsDefault)
                    btn1_Click(o, new RoutedEventArgs());
                else if (btnF5.IsDefault)
                    btnF5_Click(o, new RoutedEventArgs());
                else if (btnF6.IsDefault)
                    btnF6_Click(o, new RoutedEventArgs());
            });

            EscapeCommand = new RelayCommand((o) =>
            {
                CloseBtn_OnClick(o, new RoutedEventArgs());
            });

            //Foot pedal (F6)
            F6Command = new RelayCommand((o) =>
            {
                btnF6_Click(o, new RoutedEventArgs());
            });

            //Foot pedal (F5)
            F5Command = new RelayCommand((o) =>
            {
                btnF5_Click(o, new RoutedEventArgs());
            });

            //Foot pedal (F5)
            CopyCommand = new RelayCommand((o) =>
            {
                Copy_Click(o, new RoutedEventArgs());
            });

            imgCopy.Source = WPF_Helper.GetResourceImage("COPY.PNG");
        }

        private void MessageWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            AdjustSize();

            if (!txtMessage.IsReadOnly)
            {
                scrlMessage.Background = Brushes.White;
                txtMessage.AcceptsReturn = true;
                txtMessage.Select(0, txtMessage.Text.Length);
                FocusManager.SetFocusedElement(this, txtMessage);
            }

            SetDefaultButton();

            string closeAction = "";

            switch (_buttons)
            {
                case MessageBoxButton.YesNo:
                    btn1.Visibility = Visibility.Hidden;
                    btnF5.Visibility = Visibility.Visible;
                    if (btnF5.Content == null)
                        btnF5.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.No);
                    btnF6.Visibility = Visibility.Visible;
                    if (btnF6.Content == null)
                        btnF6.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.Yes);
                    closeAction = btnF5.Content.ToString().TryRemoveKeyboardAccellerator();
                    break;
                case MessageBoxButton.OKCancel:
                    btn1.Visibility = Visibility.Hidden;
                    btnF5.Visibility = Visibility.Visible;
                    if (btnF5.Content == null)
                        btnF5.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.Cancel);
                    btnF6.Visibility = Visibility.Visible;
                    if (btnF6.Content == null)
                        btnF6.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.OK);
                    closeAction = btnF5.Content.ToString().TryRemoveKeyboardAccellerator();
                    break;
                case MessageBoxButton.YesNoCancel:
                    btn1.Visibility = Visibility.Visible;
                    if (btn1.Content == null)
                        btn1.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.Cancel);
                    btnF5.Visibility = Visibility.Visible;
                    if (btnF5.Content == null)
                        btnF5.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.No);
                    btnF6.Visibility = Visibility.Visible;
                    if (btnF6.Content == null)
                        btnF6.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.Yes);
                    closeAction = btn1.Content.ToString().TryRemoveKeyboardAccellerator();
                    btn1.ToolTip = closeAction + "\n(Escape)";
                    break;
                case MessageBoxButton.OK:
                default:
                    btn1.Visibility = Visibility.Hidden;
                    btnF5.Visibility = Visibility.Hidden;
                    btnF5.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.Cancel);
                    btnF6.Visibility = Visibility.Visible;
                    if (btnF6.Content == null)
                        btnF6.Content = WPF_Helper.GetMessageBoxButtonText(MessageBoxResult.OK);
                    closeAction = btnF6.Content.ToString().TryRemoveKeyboardAccellerator();
                    break;
            }

            btnClose.ToolTip = "Close\nAlt+F4 / ('" + closeAction + "')";

            btnF5.ToolTip = btnF5.Content.ToString().TryRemoveKeyboardAccellerator() + "\n(Left Foot Pedal/F5)";
            btnF6.ToolTip = btnF6.Content.ToString().TryRemoveKeyboardAccellerator() + "\n(Right Foot Pedal/F6)";

            txtMessage.TextChanged += txtMessage_TextChanged;
        }

        private void SetDefaultButton()
        {
            switch (_defaultButton)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.No:
                    btnF5.IsDefault = true;
                    break;
                case MessageBoxResult.Cancel:
                    if (_buttons == MessageBoxButton.YesNoCancel)
                        btn1.IsDefault = true;
                    else
                        btnF5.IsDefault = true;
                    break;
                case MessageBoxResult.OK:
                case MessageBoxResult.Yes:
                default:
                    btnF6.IsDefault = true;
                    break;
            }
        }

        private void txtMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            Point delta = AdjustSize();
            this.Left -= delta.X / 2;
        }

        //allow window move on left click
        private void BorderMain_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_buttons == MessageBoxButton.OK)
                btnF6_Click(sender, e);
            else if (_buttons == MessageBoxButton.YesNoCancel)
                btn1_Click(sender, e);
            else //default F5
                btnF5_Click(sender, e);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            string msg = txtMessage.SelectedText;
            if (string.IsNullOrWhiteSpace(msg))
            {
                msg = "================================\n";
                msg += txtTitle.Text;
                msg += "\n---------------------------------\n";
                msg += txtMessage.Text;
                msg += "\n---------------------------------\n";
                if(btnF5.Visibility == Visibility.Visible)
                    msg += "------" + btnF5.Content + "----" + btnF6.Content + "----";
                else
                    msg += "----------------------------" + btnF6.Content + "----";
            }

            Clipboard.SetText(msg);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //btn1 - is always cancel button 
            _DialogResult = MessageBoxResult.Cancel;
            this.Close();
        }

        private void btnF5_Click(object sender, RoutedEventArgs e)
        {
            //btnF5 - is 'No' or 'Cancel' 
            _DialogResult = MessageBoxResult.Cancel;
            if (_buttons == MessageBoxButton.YesNoCancel || _buttons == MessageBoxButton.YesNo)
                _DialogResult = MessageBoxResult.No;

            this.Close();
        }

        private void btnF6_Click(object sender, RoutedEventArgs e)
        {
            _DialogResult = MessageBoxResult.OK;
            if (_buttons == MessageBoxButton.YesNoCancel || _buttons == MessageBoxButton.YesNo)
                _DialogResult = MessageBoxResult.Yes;

            this.Close();
        }

        //if timeout is set and window is not closed after timeout - click default button
        private Thread CloseWindowOnTimeout(int timeout)
        {
            if (timeout < 100)
                return null;

            Thread t = new Thread(() =>
                {
                    Thread.Sleep(timeout);
                    if (IsVisible)
                        WPF_Helper.ExecuteOnUIThread(() => { DefaultCommand.Execute(this); return 0; });
                });
            t.Name = "AutoCloseMessage";
            t.Start();
            return t;
        }

        private static Window GetWindowImpl(UIElement owner)
        {
            if (owner == null)
                return WPF_Helper.GetMainWindow();

            return GetWindow(owner);
        }

        /// <summary>
        /// Calculate size of message and adjust window size correspondently
        /// </summary>
        private Point AdjustSize()
        {
            var screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            double deltaWidth = 70;
            double deltaHeight = 140;
            double deltaWidthTitle = 80;
            
            //restrict size to 80% of display
            double maxWidth = screen.Bounds.Width * 0.8 - deltaWidth;
            double maxHeight = screen.Bounds.Height * 0.9 - deltaHeight;

            //minimal height for input box (not readonly)
            double minHeight = txtMessage.IsReadOnly ? this.MinHeight + 50 : this.MinHeight;

            //calculate message size
            Size size = MeasureString(txtMessage);
            //calculate buttons size
            Size buttonsSize = CalculateButtonsSize();
            if (buttonsSize.Width > size.Width)
                size.Width = buttonsSize.Width;

            //calculate title size
            Size sizeTitle = MeasureString(txtTitle);
            if(sizeTitle.Width + deltaWidthTitle > maxWidth)
                txtTitle.ToolTip = txtTitle.Text; //if title will be cut - add tooltip

            //if title is wider than message
            if (sizeTitle.Width < maxWidth - deltaWidthTitle && sizeTitle.Width + deltaWidthTitle > size.Width)
                size.Width = sizeTitle.Width + deltaWidthTitle;

            if (size.Width > maxWidth) size.Width = maxWidth;
            if (size.Width < this.MinWidth - deltaWidth) size.Width = this.MinWidth - deltaWidth;

            if (size.Height > maxHeight) size.Height = maxHeight;
            if (size.Height < minHeight - deltaHeight) size.Height = minHeight - deltaHeight;

            double deltaX = Math.Round(size.Width + deltaWidth - this.ActualWidth);
            double deltaY = Math.Round(size.Height + deltaHeight - this.ActualHeight);

            this.Width = size.Width + deltaWidth;
            this.Height = size.Height + deltaHeight;

            //for very long lines - scroll to the middle
            if(sTextAlignment == TextAlignment.Center)
                txtMessage.ScrollToHorizontalOffset(size.Width/2);

            return new Point(deltaX, deltaY);
        }

        private void CenterToUIElement(UIElement owner)
        {
            Rect r = new Rect(owner.PointToScreen(new Point()), owner.RenderSize);
            Point location = WPF_Helper.CenterToRectangle(this, r);
            this.Left = location.X;
            this.Top = location.Y;
        }

        private void CenterToMainWindow()
        {
            Rect r = WPF_Helper.GetMainWindowRect();
            Point location = WPF_Helper.CenterToRectangle(this, r);
            this.Left = location.X;
            this.Top = location.Y;
        }

        private Size MeasureString(TextBlock t)
        {
            var formattedText = new FormattedText(
                t.Text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(t.FontFamily, t.FontStyle, t.FontWeight, t.FontStretch),
                t.FontSize,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }

        private Size MeasureString(TextBox t)
        {
            string text = t.Text;
            if (string.IsNullOrEmpty(text))
                text = "  ";
            if (text.EndsWith(Environment.NewLine))
                text += "   "; //add spaces to calculate correct height

            var formattedText = new FormattedText(
                text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(t.FontFamily, t.FontStyle, t.FontWeight, t.FontStretch),
                t.FontSize,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }

        private Size CalculateButtonsSize()
        {
            Size sz1 = CalculateButtonSize(btn1);
            Size sz2 = CalculateButtonSize(btnF5);
            Size sz3 = CalculateButtonSize(btnF6);

            return new Size(sz1.Width + sz2.Width + sz3.Width, sz1.Height + sz2.Height + sz3.Height);
        }

        private Size CalculateButtonSize(Button btn)
        {
            if (btn.Content == null)
                return new Size();

            FormattedText formattedText = new FormattedText(
                btn.Content.ToString(),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(btn.FontFamily, btn.FontStyle, btn.FontWeight, btn.FontStretch),
                btn.FontSize,
                Brushes.Black);

            //compensate for Icon width
            return new Size(formattedText.Width + 40, formattedText.Height + 20);
        }

        public static ImageSource _imgError = System.Drawing.SystemIcons.Error.ToImageSource();
        public static ImageSource _imgQuest = System.Drawing.SystemIcons.Question.ToImageSource();
        public static ImageSource _imgExcl = System.Drawing.SystemIcons.Exclamation.ToImageSource();
        public static ImageSource _imgInfo = System.Drawing.SystemIcons.Information.ToImageSource();

        private void ConfigureAppearance(MessageBoxImage iconType)
        {
            Color messageColor = Colors.DodgerBlue;
            switch (iconType)
            {
                //case MessageBoxImage.Hand:
                case MessageBoxImage.Error:
                    img.Source = _imgError;// System.Drawing.SystemIcons.Error.ToImageSource(); // WPF_Helper.GetResourceImage("MSGBOX01.ICO");
                    img.ToolTip = "Error";
                    messageColor = Colors.Peru;// LightCoral; //Crimson;
                    System.Media.SystemSounds.Hand.Play();
                    break;
                case MessageBoxImage.Question:
                    img.Source = _imgQuest; // System.Drawing.SystemIcons.Question.ToImageSource(); // WPF_Helper.GetResourceImage("MSGBOX02.ICO"); 
                    img.ToolTip = "Question";
                    messageColor = Colors.SteelBlue; //DodgerBlue;
                    System.Media.SystemSounds.Question.Play();
                    break;
                case MessageBoxImage.Exclamation:
                    img.Source = _imgExcl; // System.Drawing.SystemIcons.Exclamation.ToImageSource(); // WPF_Helper.GetResourceImage("MSGBOX03.ICO"); 
                    img.ToolTip = "Exclamation";
                    messageColor = Colors.DarkGoldenrod; //Colors.Goldenrod; //DarkGoldenrod;
                    System.Media.SystemSounds.Exclamation.Play();
                    break;
                case MessageBoxImage.Information:
                //case MessageBoxImage.Asterisk:
                    img.Source = _imgInfo; // System.Drawing.SystemIcons.Information.ToImageSource(); // WPF_Helper.GetResourceImage("MSGBOX04.ICO"); 
                    img.ToolTip = "Information";
                    messageColor = Colors.Gray; //CornflowerBlue;
                    System.Media.SystemSounds.Beep.Play();
                    break;
                case MessageBoxImage.None:
                default:
                    img.Source = null;
                    img.ToolTip = "";
                    messageColor = Colors.DodgerBlue;
                    System.Media.SystemSounds.Beep.Play();
                    break;
            }

            colorTitle.Color = messageColor;
            borderImage.BorderBrush = new SolidColorBrush(colorTitle.Color);
            borderMsg.BorderBrush = borderImage.BorderBrush;
            borderMain.BorderBrush = borderImage.BorderBrush;
            txtMessage.SelectionBrush = borderImage.BorderBrush;

            this.Icon = img.Source; //for task bar visible image
        }
    }
}
