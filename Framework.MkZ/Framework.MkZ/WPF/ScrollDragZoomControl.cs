using MkZ.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MkZ.WPF
{
    public class OffsetAndZoom
    {
        public double Zoom { get; set; } = 1.0;

        //location change from center
        public Vector Offset { get; set; } = new Vector();

        public override string ToString()
        {
            return string.Format("Offset: X:{0:0.0}, Y:{1:0.0}, Zoom:{2:0.00}", Offset.X, Offset.Y, Zoom);
        }
    }

    public class BoundsSettings : NotifyPropertyChangedImpl
    {
        private bool _isVisible = false;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        public OffsetAndZoom FullScreen { get; set; } = new OffsetAndZoom();
        public OffsetAndZoom Normal { get; set; } = new OffsetAndZoom();

        public override string ToString()
        {
            return string.Format("BoundsSettings - IsVisible={0}, Normal: {1}, Full: {2}", IsVisible, Normal, FullScreen);
        }
    }

    /// <summary>
    /// Common functionality for Scroll Drag and Zoom
    /// </summary>
    public class ScrollDragZoomControl
    {
        private ScrollDragZoom _scrollDragZoom;
        private Control _control;
        private string Name = "N/A";

        public ScrollDragZoomControl(Control control, ScrollViewer scrollViewer, bool bEnableDrag, 
            [CallerMemberName] string propertyName = null)
        {
            Name = propertyName;

            _control = control;
            EnableZoom(scrollViewer);

            _control.Draggable(bEnableDrag);
            if (bEnableDrag)
            {
                _control.DraggableSubscribeForChange(() =>
                {
                    if (_attachedState != null)
                        _attachedState.Offset = _control.GetDraggableOffset();
                });
            }
        }

        public void EnableZoom(ScrollViewer scrollViewer)
        {
            if (_scrollDragZoom != null)
            {
                _scrollDragZoom.SizeChangedAction = () => { };
                _scrollDragZoom.Dispose();
            }
            _scrollDragZoom = null;

            if (scrollViewer != null)
            {
                _scrollDragZoom = new ScrollDragZoom(_control, scrollViewer);
                _scrollDragZoom.SizeChangedAction = () =>
                {
                    if (_attachedState != null)
                        _attachedState.Zoom = _scrollDragZoom.Zoom;
                };
            }
        }

        private OffsetAndZoom _attachedState = null;
        public void BoundsAttach(OffsetAndZoom loc)
        {
            _attachedState = null; //pause update
            
            if (loc == null)
                loc = new OffsetAndZoom();

            Debug.WriteLine("Restore {0} Zoom from: {1:0.00} to {2:0.00}, Original Size: {3}",
                Name, _scrollDragZoom.Zoom, loc.Zoom, _scrollDragZoom.NaturalSize);

            if (loc.Zoom > 10) loc.Zoom = 10;
            _scrollDragZoom.Zoom = loc.Zoom;

            Debug.WriteLine("Restore {0} Offset to: {1:0.00}", Name, loc.Offset);

            _control.SetDraggableOffset(loc.Offset, bAbsoluteOffset: true);

            _attachedState = loc; //continue update
        }

        public void BoundsGet(OffsetAndZoom loc)
        {
            loc.Zoom = _scrollDragZoom.Zoom;
            loc.Offset = _control.GetDraggableOffset();
        }
    }
}
