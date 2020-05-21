using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZ.WPF.MessageBox
{
    public partial class PopUp
    {
        //
        // Summary:
        //     Specifies the buttons that are displayed on a message box. Used as an argument
        //     of the Overload:System.Windows.MessageBox.Show method.
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
}
