using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RulerWPF
{
    public class RulerTicsData
    {
        //interval in pixels
        public double dotsPerUnit { get; private set; }
        public double tickHalfCount { get; private set; }
        public double tickTextCount { get; private set; }

        public double tick_count { get; private set; }

        public double tick1Height { get; private set; }
        public double tickHalfHeight { get; private set; }
        public double tickTextHeight { get; private set; }

        public double tick_offset { get; private set; }

        public double tick_text_div { get; private set; }

        public RulerTicsData(MeasurementUnits units, FrameworkElement canvas)
        {
            switch (units)
            {
                case MeasurementUnits.Pixels:
                    dotsPerUnit = 1;
                    tickTextCount = 10;
                    tick_offset = 10;
                    tick_text_div = 0.1;
                    break;
                case MeasurementUnits.Inches:
                    dotsPerUnit = DPI(canvas).X;
                    tickTextCount = 16;
                    tick_offset = dotsPerUnit / 16;
                    tick_text_div = 16;
                    break;
                case MeasurementUnits.Centimeters:
                    dotsPerUnit = 10 * DPMM(canvas).X;
                    tickTextCount = 10;
                    tick_offset = dotsPerUnit / 10;
                    tick_text_div = 10;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("MeasurementUnits", "Unknown unit");
            }

            tickHalfCount = tickTextCount / 2;

            tick1Height = canvas.ActualHeight / 4;
            tickHalfHeight = canvas.ActualHeight / 3;
            tickTextHeight = canvas.ActualHeight / 3;

            tick_count = canvas.ActualWidth / tick_offset;
        }

        public double WidthInSelectedUnits(double widthInPixels)
        {
            return widthInPixels / dotsPerUnit;
        }

        //dot per mm
        private Point DPMM(Visual visual)
        {
            const double inch2mm = 25.4;

            Point dpi = DPI(visual);
            Point dpmm = new Point(dpi.X / inch2mm, dpi.Y / inch2mm);
            return dpmm;
        }

        private Point DPI(Visual visual)
        {
            PresentationSource source = PresentationSource.FromVisual(visual);
            Point dpi = new Point(96, 96);
            if (source != null)
            {
                dpi.X = 96.0 * source.CompositionTarget.TransformToDevice.M11;
                dpi.Y = 96.0 * source.CompositionTarget.TransformToDevice.M22;
            }
            return dpi;
        }
    }
}
