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

        public static Action<bool> OnVisibleChanged = (isVisible) => { };

        public static bool IsVisible { get { return _formFullScreenImage != null ? _formFullScreenImage.Visible : false; } }
 
       public static void Show(FormStopWatch parent, Image image)
        {
            if (_formFullScreenImage == null)
            {
                _formFullScreenImage = new FormFullScreenImage(parent);
                _formFullScreenImage.Location = parent.Location; //show on same screen
                _formFullScreenImage.VisibleChanged += (s, e) => { OnVisibleChanged(IsVisible); };
            }

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
