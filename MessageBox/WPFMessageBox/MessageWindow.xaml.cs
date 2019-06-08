using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
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
    internal partial class MessageWindow : Window, INotifyPropertyChanged
    {
        //commands for foot pedal - F5 and F6
        public ICommand DefaultCommand { get; set; }
        public ICommand EscapeCommand { get; set; }
        public ICommand F5Command { get; set; }
        public ICommand F6Command { get; set; }
        public ICommand CopyCommand { get; set; }

        private double _width = 600;
        public double AdjustedWidth { get { return _width; } set { _width = value; NotifyPropertyChanged(); } }

        public static Visibility IconType1Visibility { get; set; } = Visibility.Visible;
        public static Visibility IconType2Visibility { get; set; } = Visibility.Collapsed;

        private PopUp.PopUpResult _DialogResult = PopUp.PopUpResult.None;
        private PopUp.PopUpButtons _buttons = new PopUp.PopUpButtons(PopUp.PopUpButtonsType.CancelOK);
        //private PopUp.PopUpResult _defaultButton = PopUp.PopUpResult.None;

        /// <summary>
        /// Change the message text Alingment
        /// </summary>
        public static TextAlignment sTextAlignment = TextAlignment.Center;
        public static WindowStartupLocation sWindowStartupLocation = WindowStartupLocation.CenterOwner;

        public static PopUp.PopUpResult MessageBox(UIElement owner, 
            ref string message, string title,
            MessageBoxImage icon, 
            TextAlignment textAlignment,
            PopUp.PopUpButtons buttons, 
            int autoCloseTimeoutMs = Timeout.Infinite, //infinite
            bool bReadonly = true)
        {
            MessageWindow wnd = new MessageWindow(buttons);
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

            wnd.btn1.Content = buttons.btn1.Text;
            wnd.btn2.Content = buttons.btn2.Text;
            wnd.btn3.Content = buttons.btn3.Text;

            wnd.AdjustSize(message, true);
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

        public MessageWindow(PopUp.PopUpButtons buttons)
        {
            _buttons = buttons;
            //_defaultButton = defaultButton;

            InitializeComponent();

            DataContext = this;

            DefaultCommand = new RelayCommand((o) =>
            {
                if (btn1.IsDefault)
                    btn1_Click(o, new RoutedEventArgs());
                else if (btn2.IsDefault)
                    btn2_Click(o, new RoutedEventArgs());
                else if (btn3.IsDefault)
                    btn3_Click(o, new RoutedEventArgs());
            });

            EscapeCommand = new RelayCommand((o) =>
            {
                CloseBtn_OnClick(o, new RoutedEventArgs());
            });

            //Foot pedal (F6)
            F6Command = new RelayCommand((o) =>
            {
                btn3_Click(o, new RoutedEventArgs());
            });

            //Foot pedal (F5)
            F5Command = new RelayCommand((o) =>
            {
                btn2_Click(o, new RoutedEventArgs());
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

            if (!txtMessage.IsReadOnly)
            {
                scrlMessage.Background = Brushes.White;
                txtMessage.AcceptsReturn = true;
                txtMessage.Select(0, txtMessage.Text.Length);
                FocusManager.SetFocusedElement(this, txtMessage);
                DataObject.AddPastingHandler(txtMessage, PasteHandler);
            }

            AdjustSize(txtMessage.Text, true);

            string closeAction = "";

            UpdateButton(btn1, _buttons.btn1);
            UpdateButton(btn2, _buttons.btn2);
            UpdateButton(btn3, _buttons.btn3);

            switch (_buttons.ButtonsType)
            {
                case PopUp.PopUpButtonsType.NoYes:
                    closeAction = btn2.Content.ToString().TryRemoveKeyboardAccellerator();
                    break;
                case PopUp.PopUpButtonsType.CancelOK:
                    closeAction = btn2.Content.ToString().TryRemoveKeyboardAccellerator();
                    break;
                case PopUp.PopUpButtonsType.CancelNoYes:
                    closeAction = btn1.Content.ToString().TryRemoveKeyboardAccellerator();
                    btn1.ToolTip = closeAction + "\n(Escape)";
                    break;
                case PopUp.PopUpButtonsType.OK:
                default:
                    closeAction = btn3.Content.ToString().TryRemoveKeyboardAccellerator();
                    break;
            }

            btnClose.ToolTip = "Close\nAlt+F4 / ('" + closeAction + "')";

            btn2.ToolTip = btn2.Content.ToString().TryRemoveKeyboardAccellerator() + "\n(Left Foot Pedal/F5)";
            btn3.ToolTip = btn3.Content.ToString().TryRemoveKeyboardAccellerator() + "\n(Right Foot Pedal/F6)";

            //txtMessage.TextChanged += txtMessage_TextChanged;
        }

        private static void UpdateButton(Button btn, PopUp.PopUpButton info)
        {
            btn.Content = info.Text;
            btn.IsDefault = info.IsDefault;
            btn.Visibility = info.IsVisible ? Visibility.Visible : Visibility.Hidden;
            btn.ToolTip = btn.Content.ToString().TryRemoveKeyboardAccellerator();
        }

        private void txtMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            Point delta = AdjustSize(txtMessage.Text, false);
        }

        private void PasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pasteText = e.DataObject.GetData(typeof(string)) as string;
                if (txtMessage.SelectedText.Length == 0)
                    pasteText += txtMessage.Text;
                else //text will be replaced - adjust length
                    pasteText += txtMessage.Text.Substring(txtMessage.SelectedText.Length);
                AdjustSize(pasteText, false); //grow only
            }
            else
            {
                e.CancelCommand();
            }
        }

        //allow window move on left click
        private void BorderMain_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_buttons.ButtonsType == PopUp.PopUpButtonsType.OK)
                btn3_Click(sender, e);
            else if (_buttons.ButtonsType == PopUp.PopUpButtonsType.CancelNoYes)
                btn1_Click(sender, e);
            else //default F5
                btn2_Click(sender, e);
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
                if(btn2.Visibility == Visibility.Visible)
                    msg += "------" + btn2.Content + "----" + btn3.Content + "----";
                else
                    msg += "----------------------------" + btn3.Content + "----";
            }

            Clipboard.SetText(msg);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //btn1 - is always cancel button 
            _DialogResult = PopUp.PopUpResult.Cancel;
            this.Close();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            //btnF5 - is 'No' or 'Cancel' 
            _DialogResult = PopUp.PopUpResult.Cancel;
            if (_buttons.ButtonsType == PopUp.PopUpButtonsType.CancelNoYes || _buttons.ButtonsType == PopUp.PopUpButtonsType.NoYes)
                _DialogResult = PopUp.PopUpResult.No;

            this.Close();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            _DialogResult = PopUp.PopUpResult.OK;
            if (_buttons.ButtonsType == PopUp.PopUpButtonsType.CancelNoYes || _buttons.ButtonsType == PopUp.PopUpButtonsType.NoYes)
                _DialogResult = PopUp.PopUpResult.Yes;

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
        private Point AdjustSize(string newText, bool growAndShrink)
        {
            WPF_Helper.UpdateScaleWPF(txtMessage);

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
            Size size = MeasureString(newText, txtMessage);
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

            //grow only
            if (growAndShrink || size.Width + deltaWidth > this.Width)
            {
                this.Width = size.Width + deltaWidth;
                this.Left -= deltaX / 2;
            }

            if (growAndShrink || size.Height + deltaHeight > this.Height)
                this.Height = size.Height + deltaHeight;

            //for very long lines - scroll to the middle
            if (sTextAlignment == TextAlignment.Center)
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

        private Size MeasureString(string text, Control t)
        {
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
            Size sz2 = CalculateButtonSize(btn2);
            Size sz3 = CalculateButtonSize(btn3);

            return new Size(sz1.Width + sz2.Width + sz3.Width, sz1.Height + sz2.Height + sz3.Height);
        }

        private Size CalculateButtonSize(Button btn)
        {
            if (btn == null || btn.Content == null)
                return new Size();

            Size sz = MeasureString(btn.Content.ToString(), btn);

            //compensate for Icon width
            return new Size(sz.Width + 40, sz.Height + 20);
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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChangedAll()
        {
            NotifyPropertyChanged("");
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
