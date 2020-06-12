using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.WinForms
{
    public class InPlaceTextBox : TextBox
    {
        public static void ShowTextBox(string text, Font font, Button button, Action<string> OkAction)
        {
            Point location = FindTextLocation(button);
            ShowTextBox(text, font, button, location, OkAction);
        }

        public static void ShowTextBox(string text, Font font, Label lbl, Action<string> OkAction)
        {
            Point location = FindTextLocation(lbl);
            ShowTextBox(text, font, lbl, location, OkAction);
        }

        public static void ShowTextBox(string text, Font font, Control owner, Point location, Action<string> OkAction)
        {
            InPlaceTextBox txt = new InPlaceTextBox(text, font, owner, location, OkAction);
            txt.Show();
            txt.Focus();
        }

        #region Implementation
        private Action<string> OkAction = (text) => { };
        private Action<string> CancelAction = (text) => { };

        private Control _owner;

        private InPlaceTextBox() { } 

        private InPlaceTextBox(string text, Font font, Control parent, Point location, Action<string> OkAction)
        {
            this.Text = text;
            this.Font = font;

            _owner = parent;

            this.Location = location;
            this.OkAction = OkAction;

            _owner.Controls.Add(this);
            //_owner.Controls.SetChildIndex(this, 100);

            UpdateWidthCorrespondingToTextLength(this, _owner);

            this.TextChanged += InPlaceTextBox_TextChanged;
            this.KeyPress += InPlaceTextBox_KeyPress;
            this.LostFocus += InPlaceTextBox_LostFocus;
        }

        private void InPlaceTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateWidthCorrespondingToTextLength(this, _owner);
        }

        private void InPlaceTextBox_LostFocus(object sender, EventArgs e)
        {
            CloseEditBox(OkAction);
        }

        private void InPlaceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CloseEditBox(OkAction);
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                CloseEditBox(CancelAction);
            }
        }

        private void CloseEditBox(Action<string> closeAction)
        {
            if (this.Visible)
            {
                _owner.Controls.Remove(this);
                closeAction(this.Text);
                this.Visible = false;
            }
        }

        private static void UpdateWidthCorrespondingToTextLength(TextBox ctrl, Control owner)
        {
            const int BORDER = 4;
            Size size = TextRenderer.MeasureText(ctrl.Text, ctrl.Font);
            int delta = size.Width - ctrl.Width + BORDER;

            const int MIN_WIDTH = 30;
            if (ctrl.Location.X + ctrl.Width + delta < owner.Width)
            {
                if (ctrl.Width + delta < MIN_WIDTH)
                    ctrl.Width = MIN_WIDTH;
                else
                    ctrl.Width += delta;
            }
        }

        private static Point FindTextLocation(Button button)
        {
            return FindTextLocation(button, button.TextAlign);
        }

        private static Point FindTextLocation(Label lbl)
        {
            return FindTextLocation(lbl, lbl.TextAlign);
        }

        private static Point FindTextLocation(Control ctrl, ContentAlignment contentAlignment)
        {
            const int BORDER = 6;
            Size sizeText = TextRenderer.MeasureText(ctrl.Text, ctrl.Font);

            int width = ctrl.Width - sizeText.Width - BORDER;
            int height = ctrl.Height - sizeText.Height - BORDER;
            int centerX = (int)(width / 2.0);
            int centerY = (int)(height / 2.0);

            switch (contentAlignment)
            {
                case ContentAlignment.TopLeft:
                    return new Point();
                case ContentAlignment.TopCenter:
                    return new Point(centerX, 0);
                case ContentAlignment.TopRight:
                    return new Point(width, 0);
                case ContentAlignment.MiddleLeft:
                    return new Point(0, centerY);
                case ContentAlignment.MiddleCenter:
                    return new Point(centerX, centerY);
                case ContentAlignment.MiddleRight:
                    return new Point(width, centerY);
                case ContentAlignment.BottomLeft:
                    return new Point(0, height);
                case ContentAlignment.BottomCenter:
                    return new Point(centerX, height);
                case ContentAlignment.BottomRight:
                    return new Point(width, height);
                default:
                    return new Point(centerX, centerY);
            }
        }
    }
#endregion

}
