using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MarkZ.Common
{
    //move window 3 points to avoid 'burn' on display
    //'rotate' location
    public class ScreenSaver
    {
        private readonly Form _owner;
        private Point _originalLocation;

        public ScreenSaver(Form owner)
        {
            _owner = owner;
            _owner.Move += _owner_Move;
            _owner.FormClosed += _owner_FormClosed;
            _originalLocation = _owner.Location;
        }

        private void _owner_FormClosed(object sender, FormClosedEventArgs e)
        {
            _owner.Move -= _owner_Move;
            _owner.FormClosed -= _owner_FormClosed;
        }

        private bool _moveFromThis = false;
        private void _owner_Move(object sender, EventArgs e)
        {
            if(!_moveFromThis)
                _originalLocation = _owner.Location;
        }

        private const int DELTA = 3;
        private static Point[] _locations = new Point[] 
        { 
            new Point(DELTA, 0), new Point(0, DELTA), new Point(-DELTA, 0), new Point(0, -DELTA) 
        };

        private int _index = -1;
        public void PerformScreenSaving()
        {
            _moveFromThis = true;

            _index++;
            if (_index >= _locations.Length)
                _index = 0;

            Point newLocation = new Point(_originalLocation.X, _originalLocation.Y);
            newLocation.Offset(_locations[_index]);
            _owner.Location = newLocation;

            _moveFromThis = false;
        }
    }
}
