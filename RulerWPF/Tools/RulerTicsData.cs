using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
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

        public double tick0Height { get; private set; }
        public double tick1Height { get; private set; }
        public double tickHalfHeight { get; private set; }
        public double tickTextHeight { get; private set; }

        public double tick_width { get; private set; }

        public double tick_text_scale { get; private set; }

        public RulerTicsData(MeasurementUnits units, FrameworkElement canvas)
        {
            tick0Height = canvas.ActualHeight / 8; //every tick
            tick1Height = canvas.ActualHeight / 8; //eveery second tick
            tickHalfHeight = canvas.ActualHeight / 4; //half
            tickTextHeight = canvas.ActualHeight / 3; //every tick with number

            switch (units)
            {
                case MeasurementUnits.Pixels:
                    dotsPerUnit = 1; //per pixel
                    tickTextCount = 10;
                    tick_width = 10;
                    tick_text_scale = 0.1;
                    break;
                case MeasurementUnits.Inches:
                    dotsPerUnit = DPI(canvas).X;
                    tickTextCount = 16;
                    tick_width = dotsPerUnit / 16;
                    tick_text_scale = 16;
                    tick1Height = canvas.ActualHeight / 4;
                    tickHalfHeight = canvas.ActualHeight / 3;
                    break;
                case MeasurementUnits.Centimeters:
                    dotsPerUnit = DPCM(canvas).X;
                    tickTextCount = 10;
                    tick_width = dotsPerUnit / 10;
                    tick_text_scale = 10;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("MeasurementUnits", "Unknown unit");
            }

            tickHalfCount = tickTextCount / 2;
            tick_count = canvas.ActualWidth / tick_width;
        }

        public double WidthInSelectedUnits(double widthInPixels)
        {
            return widthInPixels / dotsPerUnit;
        }

        //dot per mm
        private Point DPCM(Visual visual)
        {
            const double inch2cm = 2.54;

            Point dpi = DPI(visual);
            Point dpcm = new Point(dpi.X / inch2cm, dpi.Y / inch2cm);
            return dpcm;
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

            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?redirectedfrom=MSDN&view=netframework-4.8#System_Windows_FrameworkElement_Height
            //px(default) is device-independent units(1/96th inch per unit)
            //in is inches; 1in==96px
            //cm is centimeters; 1cm==(96/2.54) px
            //pt is points; 1pt==(96/72) px
            const double scale = (96.0 / 72.0);

            dpi.X *= scale;
            dpi.Y *= scale;

            return dpi;
        }
    }
}
