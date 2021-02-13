using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MkZ.WPF
{
    public class AdornerContentPresenter : Adorner
    {
        private VisualCollection _Visuals;
        private ContentPresenter _ContentPresenter;

        public AdornerContentPresenter(UIElement adornedElement)
          : base(adornedElement)
        {
            _Visuals = new VisualCollection(this);
            _ContentPresenter = new ContentPresenter();
            _Visuals.Add(_ContentPresenter);
        }

        public AdornerContentPresenter(UIElement adornedElement, Visual content)
          : this(adornedElement)
        { 
            Content = content; 
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _ContentPresenter.Measure(constraint);
            return _ContentPresenter.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _ContentPresenter.Arrange(new Rect(0, 0,
                 finalSize.Width, finalSize.Height));
            return _ContentPresenter.RenderSize;
        }

        protected override Visual GetVisualChild(int index)
        { return _Visuals[index]; }

        protected override int VisualChildrenCount
        { get { return _Visuals.Count; } }

        public object Content
        {
            get { return _ContentPresenter.Content; }
            set { _ContentPresenter.Content = value; }
        }
    }
}
