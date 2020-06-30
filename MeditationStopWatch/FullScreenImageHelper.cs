using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeditationStopWatch
{
    public class FullScreenImageHelper
    {
        private static FormFullScreenImage _formFullScreenImage = null;

        public static EventHandler<bool> OnVisibleChanged = (sender, isVisible) => { };

        public static bool IsVisible { get { return _formFullScreenImage != null ? _formFullScreenImage.Visible : false; } }
 
       public static void Show(FormStopWatch parent, Image image)
        {
            if (_formFullScreenImage == null)
            {
                _formFullScreenImage = new FormFullScreenImage(parent);
                _formFullScreenImage.VisibleChanged += (s, e) => { OnVisibleChanged(_formFullScreenImage, IsVisible); };
            }

            _formFullScreenImage.WindowState = System.Windows.Forms.FormWindowState.Normal;

            Point pt = parent.Location;
            pt.Offset(100, 100); //show on same screen
            _formFullScreenImage.Location = pt;

            _formFullScreenImage.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            _formFullScreenImage.Picture = image;
            _formFullScreenImage.Show(parent);
        }

        public static void Hide()
        {
            if (_formFullScreenImage != null)
                _formFullScreenImage.Visible = false;
        }
    }
}
