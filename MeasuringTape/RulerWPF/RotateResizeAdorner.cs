using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RulerWPF
{
    public class RotateResizeAdorner : Adorner
    {
        private Thumb rotateHandle;
        private Thumb moveHandle;
        private Thumb scaleHandle;
        private Thumb sizeHandle;
        private Path outline;
        private VisualCollection visualChildren;
        private Point _center;
        private TranslateTransform _translate;
        private RotateTransform rotation;
        private ScaleTransform scale;
        private TransformGroup transformGroup;
        private const int HANDLEMARGIN = 10;

        public RotateResizeAdorner(UIElement adornedElement)
            : base(adornedElement)
        {

            visualChildren = new VisualCollection(this);

            rotateHandle = new Thumb();
            rotateHandle.Cursor = Cursors.Hand;
            rotateHandle.Width = 10;
            rotateHandle.Height = 10;
            rotateHandle.Background = Brushes.Blue;

            rotateHandle.DragDelta += (rotateHandle_DragDelta);
            rotateHandle.DragCompleted += (rotateHandle_DragCompleted);

            moveHandle = new Thumb();
            moveHandle.Cursor = Cursors.SizeAll;
            moveHandle.Width = 15;
            moveHandle.Height = 15;
            moveHandle.Background = Brushes.Blue;

            moveHandle.DragDelta += (moveHandle_DragDelta);
            moveHandle.DragCompleted += (moveHandle_DragCompleted);

            scaleHandle = new Thumb();
            scaleHandle.Cursor = Cursors.Hand;
            scaleHandle.Width = 10;
            scaleHandle.Height = 10;
            scaleHandle.Background = Brushes.Green;

            scaleHandle.DragDelta += (scaleHandle_DragDelta);
            scaleHandle.DragCompleted += (scaleHandle_DragCompleted);

            sizeHandle = new Thumb();
            sizeHandle.Cursor = Cursors.SizeWE;
            sizeHandle.Width = 10;
            sizeHandle.Height = 10;
            sizeHandle.Background = Brushes.Red;

            sizeHandle.DragDelta += (sizeHandle_DragDelta);
            sizeHandle.DragCompleted += (sizeHandle_DragCompleted);

            outline = new Path();
            outline.Stroke = Brushes.Blue;
            outline.StrokeThickness = 1;

            rotation = new RotateTransform();
            _translate = new TranslateTransform();
            scale = new ScaleTransform();
            transformGroup = adornedElement.RenderTransform as TransformGroup;
            if (transformGroup == null)
            {
                transformGroup = new TransformGroup();
            }

            visualChildren.Add(outline);
            visualChildren.Add(rotateHandle);
            visualChildren.Add(moveHandle);
            visualChildren.Add(scaleHandle);
            visualChildren.Add(sizeHandle);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {

            _center = new Point(AdornedElement.RenderSize.Width/2, AdornedElement.RenderSize.Height/2);

            Rect handleRect = new Rect(-AdornedElement.RenderSize.Width/2, -AdornedElement.RenderSize.Height/2,
                                        AdornedElement.RenderSize.Width, AdornedElement.RenderSize.Height);
            rotateHandle.Arrange(handleRect);

            Rect scalehandleRect = new Rect(0, -AdornedElement.RenderSize.Height / 2, AdornedElement.RenderSize.Width, AdornedElement.RenderSize.Height);
            scaleHandle.Arrange(scalehandleRect);

            Rect sizeHandleRect = new Rect(30, 2, AdornedElement.RenderSize.Width, AdornedElement.RenderSize.Height);
            sizeHandle.Arrange(sizeHandleRect);

            Rect finalRect = new Rect(finalSize);
            moveHandle.Arrange(finalRect);

            outline.Data = new RectangleGeometry(finalRect);
            outline.Arrange(finalRect);

            return finalSize;
        }

        protected override int VisualChildrenCount
        {
            get { return visualChildren.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return visualChildren[index];
        }

        private void scaleHandle_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            MoveNewTransformToAdornedElement(scale);
        }

        private void scaleHandle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point pos = Mouse.GetPosition(this);
            double deltaY = _center.Y - pos.Y;
            double scaleRatio = deltaY / _center.Y;
            scale.ScaleX = scaleRatio;
            scale.ScaleY = scaleRatio;
            scale.CenterX = _center.X;
            scale.CenterY = _center.Y;
            outline.RenderTransform = scale;
        }

        private void sizeHandle_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            MoveNewTransformToAdornedElement(scale);
        }

        private void sizeHandle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point pos = Mouse.GetPosition(this);
            double deltaX = _center.X - pos.X;
            AdornedElement.RenderSize = new Size(AdornedElement.RenderSize.Width + deltaX, AdornedElement.RenderSize.Height);
        }

        private void moveHandle_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            MoveNewTransformToAdornedElement(_translate);
        }

        private void moveHandle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point pos = Mouse.GetPosition(this);

            double deltaX = pos.X - _center.X;
            double deltaY = pos.Y - _center.Y;

            _translate.X = deltaX;
            _translate.Y = deltaY;

            outline.RenderTransform = _translate;
        }

        private void rotateHandle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point pos = Mouse.GetPosition(this);

            double deltaX = pos.X - _center.X;
            double deltaY = pos.Y - _center.Y;

            double angle;
            if (deltaY.Equals(0))
            {
                if (!deltaX.Equals(0))
                {
                    angle = 90;
                }
                else
                {
                    return;
                }
            }
            else
            {
                double tan = deltaX/deltaY;
                angle = Math.Atan(tan);

                angle = angle*180/Math.PI;
            }

            // If the mouse crosses the vertical center, 
            // find the complementary angle.
            if (deltaY > 0)
            {
                angle = 180 - Math.Abs(angle);
            }

            // Rotate left if the mouse moves left and right
            // if the mouse moves right.
            if (deltaX < 0)
            {
                angle = -Math.Abs(angle);
            }
            else
            {
                angle = Math.Abs(angle);
            }

            if (Double.IsNaN(angle))
            {
                return;
            }

            // Adjust the offset.
            double tanOffset = AdornedElement.RenderSize.Width/AdornedElement.RenderSize.Height;
            angle += Math.Atan(tanOffset)*180/Math.PI;

            // Apply the rotation to the outline.
            rotation.Angle = angle;
            rotation.CenterX = _center.X;
            rotation.CenterY = _center.Y;
            outline.RenderTransform = rotation;
        }

        /// <summary>
        /// Rotates to the same angle as outline.
        /// </summary>
        private void rotateHandle_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            MoveNewTransformToAdornedElement(rotation);
        }

        private void MoveNewTransformToAdornedElement(Transform transform)
        {
            if (transform == null)
            {
                return;
            }

            var newTransform = transform.Clone();
            newTransform.Freeze();
            transformGroup.Children.Insert(0, newTransform);
            AdornedElement.RenderTransform = transformGroup;

            outline.RenderTransform = Transform.Identity;
            this.InvalidateArrange();
        }
    }
}
