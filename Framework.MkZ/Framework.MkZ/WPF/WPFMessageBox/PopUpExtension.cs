using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace MZ.WPF.MessageBox
{
    public partial class PopUp
    {
        //
        // Summary:
        //     Specifies the buttons that are displayed on a message box. Used as an argument
        //     of the Overload:System.Windows.MessageBox.Show method.
        [Flags]
        public enum PopUpButtonsType : int
        {
            //
            // Summary:
            //     The message box displays an OK button.
            OK = 1,
            btn3 = 1, //0001
            //
            // Summary:
            //     The message box displays OK and Cancel buttons.
            CancelOK = 3,
            btn2btn3 = 3, //0011
            //
            // Summary:
            //     The message box displays Yes, No, and Cancel buttons.
            CancelNoYes = 7,
            btn1btn2btn3 = 7, //0111
            //
            // Summary:
            //     The message box displays Yes and No buttons.
            NoYes = 11 //1011
        }

        //
        // Summary:
        //     Specifies which message box button that a user clicks. System.Windows.MessageBoxResult
        //     is returned by the Overload:System.Windows.MessageBox.Show method.
        public enum PopUpResult
        {
            //
            // Summary:
            //     The message box returns no result.
            None = 0,
            //
            // Summary:
            //     The result value of the message box is Cancel.
            Cancel = 1,
            Btn1 = 1,
            //
            // Summary:
            //     The result value of the message box is No.
            No = 2,
            Btn2 = 2,
            //
            // Summary:
            //     The result value of the message box is OK.
            OK = 3,
            Btn3 = 3,
            //
            // Summary:
            //     The result value of the message box is Yes.
            Yes = 3,
        }

        public class PopUpButton
        {
            public string Text { get; set; } = "";
            public bool IsDefault { get; set; } = false;
            public bool IsVisible { get; set; } = false;

            public PopUpButton(string text, bool visible, bool isDefaultButton)
            {
                Text = text;
                IsVisible = visible;
                IsDefault = isDefaultButton;
            }
        }

        public class PopUpButtons
        {
            public PopUpButton btn1 { get; set; }
            public PopUpButton btn2 { get; set; }
            public PopUpButton btn3 { get; set; }

            public PopUpButtonsType ButtonsType = PopUpButtonsType.OK; 

            public PopUpButtons(PopUpButtonsType buttons)
            {
                ButtonsType = buttons;
                switch (buttons)
                {
                    case PopUpButtonsType.CancelOK:
                        btn1 = new PopUpButton("", false, false);
                        btn2 = new PopUpButton(WIN32.MB_GetString(WIN32.SystemLocStrings.Cancel), true, false);
                        btn3 = new PopUpButton(WIN32.MB_GetString(WIN32.SystemLocStrings.OK), true, true);
                        break;
                    case PopUpButtonsType.CancelNoYes:
                        btn1 = new PopUpButton(WIN32.MB_GetString(WIN32.SystemLocStrings.Cancel), true, false);
                        btn2 = new PopUpButton(WIN32.MB_GetString(WIN32.SystemLocStrings.No), true, false);
                        btn3 = new PopUpButton(WIN32.MB_GetString(WIN32.SystemLocStrings.Yes), true, true);
                        break;
                    case PopUpButtonsType.NoYes:
                        btn1 = new PopUpButton("", false, false);
                        btn2 = new PopUpButton(WIN32.MB_GetString(WIN32.SystemLocStrings.No), true, false);
                        btn3 = new PopUpButton(WIN32.MB_GetString(WIN32.SystemLocStrings.Yes), true, true);
                        break;
                    case PopUpButtonsType.OK:
                    default:
                        btn1 = new PopUpButton("", false, false);
                        btn2 = new PopUpButton("", false, false);
                        btn3 = new PopUpButton(WIN32.MB_GetString(WIN32.SystemLocStrings.OK), true, true);
                        break;
                }
            }

            public PopUpButtons(PopUpButtonsType buttons, PopUpResult defaultButton)
                : this(buttons)
            {
                switch (defaultButton)
                {
                    case PopUpResult.Btn1: //cancel
                        btn1.IsDefault = true;
                        btn2.IsDefault = false;
                        btn3.IsDefault = false;
                        break;
                    case PopUpResult.Btn2:
                        btn1.IsDefault = false;
                        btn2.IsDefault = true;
                        btn3.IsDefault = false;
                        break;
                    case PopUpResult.Btn3:
                        btn1.IsDefault = false;
                        btn2.IsDefault = false;
                        btn3.IsDefault = true;
                        break;
                    case PopUpResult.None:
                    default:
                        btn1.IsDefault = false;
                        btn2.IsDefault = false;
                        btn3.IsDefault = false;
                        break;
                }
            }

            public PopUpButtons(string btn1text, string btn2text, string btn3text, PopUpResult defaultButton = PopUpResult.Btn3)
                : this(PopUpButtonsType.CancelNoYes, defaultButton)
            {
                ButtonsType = 0;
                btn1.IsVisible = !string.IsNullOrWhiteSpace(btn1text);
                if (btn1.IsVisible)
                {
                    btn1.Text = btn1text;
                    ButtonsType = (PopUpButtonsType)((int)ButtonsType | 4);
                }

                btn2.IsVisible = !string.IsNullOrWhiteSpace(btn2text);
                if (btn2.IsVisible)
                {
                    btn2.Text = btn2text;
                    ButtonsType = (PopUpButtonsType)((int)ButtonsType | 2);
                }

                btn3.IsVisible = !string.IsNullOrWhiteSpace(btn3text);
                if (btn3.IsVisible)
                {
                    btn3.Text = btn3text;
                    ButtonsType = (PopUpButtonsType)((int)ButtonsType | 1);
                }
            }
        }
    }

    public partial class PopUp
    {
        #region Common Message Boxes

        public static void Error(string message, string title = "Error",
            TextAlignment textAlignment = TextAlignment.Center)
        {
            MessageBox(null, message, title, MessageBoxImage.Error, textAlignment);
        }

        public static void Information(string message, string title = "Information",
            TextAlignment textAlignment = TextAlignment.Center)
        {
            MessageBox(null, message, title, MessageBoxImage.Information, textAlignment);
        }

        public static void Exclamation(string message, string title = "Exclamation",
            TextAlignment textAlignment = TextAlignment.Center)
        {
            MessageBox(null, message, title, MessageBoxImage.Exclamation, textAlignment);
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
        public static PopUp.PopUpResult Question(string message, string title = "Question",
            MessageBoxImage icon = MessageBoxImage.Question,
            TextAlignment textAlignment = TextAlignment.Center,
            PopUp.PopUpButtonsType buttonsType = PopUp.PopUpButtonsType.CancelOK)
        {
            PopUp.PopUpButtons buttons = new PopUp.PopUpButtons(buttonsType);

            return MessageBox(null, message, title, icon, textAlignment, buttons);
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
        public static PopUp.PopUpResult MessageBox(string message, string title,
            MessageBoxImage icon = MessageBoxImage.Information, 
            TextAlignment textAlignment = TextAlignment.Center,
            PopUp.PopUpButtons buttons = null, int timeout = Timeout.Infinite)
        {
            if (buttons == null)
                buttons = new PopUp.PopUpButtons(PopUp.PopUpButtonsType.OK);

            return WPF_Helper.ExecuteOnUIThread(() =>
            {
                return MessageWindowExtension.MessageBox(null, ref message, title, icon, textAlignment, buttons, timeout);
            });
        }

        #endregion
    }

    public static class PopUpWpfExtension
    {

        #region WPF Extension Message Boxes

        public static void MessageInfo(this UIElement owner,
            string message, string title = "Information")
        {
            owner.MessageBox(message, title, MessageBoxImage.Information);
        }

        public static void MessageError(this UIElement owner,
            string message, string title = "Error")
        {
            owner.MessageBox(message, title, MessageBoxImage.Error);
        }

        public static void MessageWarning(this UIElement owner,
            string message, string title = "Warning")
        {
            owner.MessageBox(message, title, MessageBoxImage.Exclamation);
        }

        public static PopUp.PopUpResult MessageQuestion(this UIElement owner,
            string message, string title = "Question",
            PopUp.PopUpButtonsType buttons = PopUp.PopUpButtonsType.CancelOK)
        {
            return owner.MessageBox(message, title, MessageBoxImage.Question, TextAlignment.Center, buttons);
        }

        #endregion

        public static PopUp.PopUpResult MessageBox(this UIElement owner, string message, string title,
            MessageBoxImage icon = MessageBoxImage.Information, TextAlignment textAlignment = TextAlignment.Center,
            PopUp.PopUpButtonsType buttonsType = PopUp.PopUpButtonsType.OK, int timeout = Timeout.Infinite)
        {
            PopUp.PopUpButtons buttons = new PopUp.PopUpButtons(buttonsType);

            return WPF_Helper.ExecuteOnUIThreadWPF(() =>
            {
                return MessageWindowExtension.MessageBox(owner, ref message, title, icon, textAlignment, buttons, timeout);
            });
        }
    }

    public static class PopUpWinExtension
    {
        public static void MessageInfo(this System.Windows.Forms.Control owner, 
            string message, string title = "Information", TextAlignment textAlignment = TextAlignment.Center)
        {
            owner.MessageBox(message, title, MessageBoxImage.Information, textAlignment);
        }

        public static void MessageError(this System.Windows.Forms.Control owner, 
            string message, string title = "Error", TextAlignment textAlignment = TextAlignment.Center)
        {
            owner.MessageBox(message, title, MessageBoxImage.Error, textAlignment);
        }

        public static void MessageWarning(this System.Windows.Forms.Control owner, 
            string message, string title = "Warning", TextAlignment textAlignment = TextAlignment.Center)
        {
            owner.MessageBox(message, title, MessageBoxImage.Exclamation, textAlignment);
        }

        public static PopUp.PopUpResult MessageQuestion(this System.Windows.Forms.Control owner, 
            string message, string title = "Question",
            PopUp.PopUpButtonsType buttons = PopUp.PopUpButtonsType.CancelOK)
        {
            return owner.MessageBox(message, title, MessageBoxImage.Question, TextAlignment.Center, buttons);
        }

        public static PopUp.PopUpResult MessageBox(this System.Windows.Forms.Control owner, string message, string title,
            MessageBoxImage icon = MessageBoxImage.Information, TextAlignment textAlignment = TextAlignment.Center,
            PopUp.PopUpButtonsType buttonsType = PopUp.PopUpButtonsType.OK, int timeout = Timeout.Infinite)
        {
            PopUp.PopUpButtons buttons = new PopUp.PopUpButtons(PopUp.PopUpButtonsType.OK);

            return owner.MessageBoxEx(message, title, icon, textAlignment, buttons, timeout);
        }

        public static PopUp.PopUpResult MessageBoxEx(this System.Windows.Forms.Control owner, string message, string title,
            MessageBoxImage icon = MessageBoxImage.Information, TextAlignment textAlignment = TextAlignment.Center,
            PopUp.PopUpButtons buttons = null, int timeout = Timeout.Infinite)
        {
            if(buttons == null)
                buttons = new PopUp.PopUpButtons(PopUp.PopUpButtonsType.OK);

            return WPF_Helper.ExecuteOnUIThreadForm(() =>
            {
                return MessageWindowExtension.MessageBox(owner.Handle, ref message, title, icon, textAlignment, buttons, timeout);
            });
        }
    }
}
