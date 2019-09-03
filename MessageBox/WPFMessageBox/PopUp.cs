using System;
using System.Threading;
using System.Windows;

// <summary>
// MarkZ. 2017-08-01
// 
// Show Message Box, using main app window(WinForms or WPF) as owner,
// If btn*text is null will assign text by 'buttons'
// </summary>
namespace MZ.WPF.MessageBox
{
    public partial class PopUp
    {
        public enum IconStyle
        {
            FootPedalImages,
            RegularImages,
            NoImages
        }

        public static IconStyle IconType
        {

            set
            {
                switch (value)
                {
                    case IconStyle.FootPedalImages:
                        MessageWindow.IconType1Visibility = Visibility.Collapsed;
                        MessageWindow.IconType2Visibility = Visibility.Visible;
                        break;
                    case IconStyle.RegularImages:
                        MessageWindow.IconType1Visibility = Visibility.Visible;
                        MessageWindow.IconType2Visibility = Visibility.Collapsed;
                        break;
                    case IconStyle.NoImages:
                    default:
                        MessageWindow.IconType1Visibility = Visibility.Collapsed;
                        MessageWindow.IconType2Visibility = Visibility.Collapsed;
                        break;
                }
            }

            get
            {
                if (MessageWindow.IconType1Visibility == Visibility.Collapsed && MessageWindow.IconType2Visibility == Visibility.Collapsed)
                    return IconStyle.NoImages;
                if (MessageWindow.IconType1Visibility == Visibility.Collapsed && MessageWindow.IconType2Visibility == Visibility.Visible)
                    return IconStyle.FootPedalImages;
                return IconStyle.RegularImages;
            }
        }

        public static void Error(string message, string title = "Error",
          MessageBoxImage icon = MessageBoxImage.Error,
          TextAlignment textAlignment = TextAlignment.Center)
        {
            MessageBox(message, title, icon, textAlignment);
        }

        public static void Information(string message, string title = "Information",
            MessageBoxImage icon = MessageBoxImage.Information,
            TextAlignment textAlignment = TextAlignment.Center)
        {
            MessageBox(message, title, icon, textAlignment);
        }

        public static void Exclamation(string message, string title = "Exclamation",
            MessageBoxImage icon = MessageBoxImage.Exclamation, 
            TextAlignment textAlignment = TextAlignment.Center)
        {
            MessageBox(message, title, icon, textAlignment);
        }

        /// <summary>
        /// return True on Ok(F6), False on Cancel(F5)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <param name="textAlignment"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static PopUpResult Question(string message, string title = "Question",
            MessageBoxImage icon = MessageBoxImage.Question, 
            TextAlignment textAlignment = TextAlignment.Center,
            PopUpButtonsType buttonsType = PopUpButtonsType.CancelOK)
        {
            PopUpButtons buttons = new PopUpButtons(buttonsType);

            return MessageBox(message, title, icon, textAlignment, buttons);
        }

        /// <summary>
        /// Show Message Box, using main app window(WinForms or WPF) as owner,
        /// If btn*text is null will assign text by 'buttons'
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <param name="textAlignment"></param>
        /// <param name="buttons"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static PopUpResult MessageBox(string message, string title,
            MessageBoxImage icon = MessageBoxImage.Information, TextAlignment textAlignment = TextAlignment.Center,
            PopUpButtons buttons = null, int timeout = Timeout.Infinite)
        {
            if (buttons == null)
                buttons = new PopUpButtons(PopUpButtonsType.OK);

            return WPF_Helper.ExecuteOnUIThread(() =>
            {
                return MessageWindow.MessageBox(null, ref message, title, icon, textAlignment, buttons, timeout);
            });
        }

        /// <summary>
        /// Show Message Box, using main app window(WinForms or WPF) as owner,
        /// If btn*text is null will assign text by 'buttons'
        /// </summary>
        /// <param name="owner">WPF UIElement to center on, can be null</param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <param name="textAlignment"></param>
        /// <param name="buttons"></param>
        /// <param name="timeout">execute default action after timeout if more than 100 milliseconds</param>
        /// <returns></returns>
        public static PopUpResult MessageBox(UIElement owner, string message, string title,
            MessageBoxImage icon, TextAlignment textAlignment = TextAlignment.Center,
            PopUpButtons buttons = null, int timeout = Timeout.Infinite)
        {
            if (buttons == null)
                buttons = new PopUpButtons(PopUpButtonsType.OK);

            return MessageBox(() => owner, message, title, icon, textAlignment, buttons, timeout);
        }

        /// <summary>
        /// Show Message Box, using main app window(WinForms or WPF) as owner,
        /// If btn*text is null will assign text by 'buttons'
        /// </summary>
        /// <param name="GetOwnerOnUIThread">function that returns WPF UIElement to center on, wull be executed on UI thread, can be null</param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <param name="textAlignment"></param>
        /// <param name="buttons"></param>
        /// <param name="timeout">execute default action after timeout if more than 100 milliseconds</param>
        /// <returns></returns>
        public static PopUpResult MessageBox(Func<UIElement> GetOwnerOnUIThread, string message, string title,
            MessageBoxImage icon, TextAlignment textAlignment = TextAlignment.Center,
            PopUpButtons buttons = null, int timeout = Timeout.Infinite)
        {
            if (buttons == null)
                buttons = new PopUpButtons(PopUpButtonsType.OK);

            return WPF_Helper.ExecuteOnUIThread(() =>
            {
                UIElement owner = null;
                if(GetOwnerOnUIThread != null)
                    owner = GetOwnerOnUIThread();

                return MessageWindow.MessageBox(owner,
                    ref message, title,
                    icon, textAlignment,
                    buttons, timeout);
            });
        }

        public static PopUpResult InputBox(ref string userInput, string title, 
            MessageBoxImage icon = MessageBoxImage.Information, TextAlignment textAlignment = TextAlignment.Justify,
            PopUpButtonsType buttonsType = PopUpButtonsType.CancelOK, int timeout = Timeout.Infinite)
        {
            PopUpButtons buttons = new PopUpButtons(buttonsType);

            string message = userInput;
            PopUpResult res = WPF_Helper.ExecuteOnUIThread(() =>
            {
                return MessageWindow.MessageBox(null, ref message, title, icon, textAlignment, 
                    buttons, timeout,
                    false);
            });
            userInput = message;
            return res;
        }
    }
}
