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
    public class PopUp
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
        public static MessageBoxResult Question(string message, string title = "Question",
            MessageBoxImage icon = MessageBoxImage.Question, 
            TextAlignment textAlignment = TextAlignment.Center,
            MessageBoxButton buttons = MessageBoxButton.OKCancel)
        {
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
        /// <param name="btnF6text">right most button, uses right foot pedal (F6), MessageBoxResult.Yes, OK; if is null will assign text by 'buttons'</param>
        /// <param name="btnF5text">left/middle button, uses left foot pedal (F5), MessageBoxResult.Cancel, No; if is null will assign text by 'buttons'</param>
        /// <param name="btn1text">left most button, MessageBoxResult.Cancel; if is null will assign text by 'buttons'</param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        public static MessageBoxResult MessageBox(string message, string title,
            MessageBoxImage icon = MessageBoxImage.Information, TextAlignment textAlignment = TextAlignment.Center,
            MessageBoxButton buttons = MessageBoxButton.OK, 
            string btnF6text = null, string btnF5text = null, string btn1text = null, 
            MessageBoxResult defaultButton = MessageBoxResult.OK)
        {
            return WPF_Helper.ExecuteOnUIThread(() =>
            {
                return MessageWindow.MessageBox(null, ref message, title, icon, textAlignment, buttons, btnF6text, btnF5text, btn1text, defaultButton);
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
        /// <param name="btnF6text">right most button, uses right foot pedal (F6), MessageBoxResult.Yes, OK; if is null will assign text by 'buttons'</param>
        /// <param name="btnF5text">left/middle button, uses left foot pedal (F5), MessageBoxResult.Cancel, No; if is null will assign text by 'buttons'</param>
        /// <param name="btn1text">left most button, MessageBoxResult.Cancel; if is null will assign text by 'buttons'</param>
        /// <param name="defaultButton">MessageBoxResult - will set as default action on 'Enter'</param>
        /// <param name="timeout">execute default action after timeout if more than 100 milliseconds</param>
        /// <returns></returns>
        public static MessageBoxResult MessageBox(UIElement owner, string message, string title,
            MessageBoxImage icon, TextAlignment textAlignment = TextAlignment.Center,
            MessageBoxButton buttons = MessageBoxButton.OK,
            string btnF6text = null, string btnF5text = null, string btn1text = null,
            MessageBoxResult defaultButton = MessageBoxResult.OK, int timeout = Timeout.Infinite)
        {
            return MessageBox(() => owner,
                    message, title,
                    icon, textAlignment,
                    buttons, btnF6text, btnF5text, btn1text, defaultButton, timeout);
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
        /// <param name="btnF6text">right most button, uses right foot pedal (F6), MessageBoxResult.Yes, OK; if is null will assign text by 'buttons'</param>
        /// <param name="btnF5text">left/middle button, uses left foot pedal (F5), MessageBoxResult.Cancel, No; if is null will assign text by 'buttons'</param>
        /// <param name="btn1text">left most button, MessageBoxResult.Cancel; if is null will assign text by 'buttons'</param>
        /// <param name="defaultButton">MessageBoxResult - will set as default action on 'Enter'</param>
        /// <param name="timeout">execute default action after timeout if more than 100 milliseconds</param>
        /// <returns></returns>
        public static MessageBoxResult MessageBox(Func<UIElement> GetOwnerOnUIThread, string message, string title,
            MessageBoxImage icon, TextAlignment textAlignment = TextAlignment.Center,
            MessageBoxButton buttons = MessageBoxButton.OK,
            string btnF6text = null, string btnF5text = null, string btn1text = null,
            MessageBoxResult defaultButton = MessageBoxResult.OK, int timeout = Timeout.Infinite)
        {
            return WPF_Helper.ExecuteOnUIThread(() =>
            {
                UIElement owner = null;
                if(GetOwnerOnUIThread != null)
                    owner = GetOwnerOnUIThread();

                return MessageWindow.MessageBox(owner,
                    ref message, title,
                    icon, textAlignment,
                    buttons, btnF6text, btnF5text, btn1text, defaultButton, timeout);
            });
        }

        public static MessageBoxResult InputBox(ref string userInput, string title, 
            MessageBoxImage icon = MessageBoxImage.Information, TextAlignment textAlignment = TextAlignment.Justify,
            MessageBoxButton buttons = MessageBoxButton.OKCancel, 
            string btnF6text = null, string btnF5text = null, string btn1text = null,
            MessageBoxResult defaultButton = MessageBoxResult.OK, int timeout = Timeout.Infinite)
        {
            string message = userInput;
            MessageBoxResult res = WPF_Helper.ExecuteOnUIThread(() =>
            {
                return MessageWindow.MessageBox(null, ref message, title, icon, textAlignment, 
                    buttons, btnF6text, btnF5text, btn1text, defaultButton, timeout,
                    false);
            });
            userInput = message;
            return res;
        }
    }
}
