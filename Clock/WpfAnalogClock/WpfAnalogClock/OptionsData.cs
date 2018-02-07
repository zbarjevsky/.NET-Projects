using System;
using System.ComponentModel;

namespace WpfAnalogClock
{
    public class OptionsData
    {
        public static OptionsData Instance = new OptionsData();

        public Action<string> OnPropertyChange = (prop) => { };

        public void ShowOptions(System.Windows.Window owner)
        {
            OptionsWindow wnd = new OptionsWindow(this);
            wnd.Owner = owner;
            wnd.OnPropertyChange = (prop) => { OnPropertyChange(prop);  };
            wnd.ShowDialog();
        }

        [Category("Colors")]
        public System.Drawing.Color DigitalClockColor { get; set; }

        [Category("Colors")]
        public System.Drawing.Color CloseButtonColor { get; set; }

        [Category("Colors")]
        public System.Drawing.Color OptionsButtonColor { get; set; }
    }
}
