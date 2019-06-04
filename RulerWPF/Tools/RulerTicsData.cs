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
                    dotsPerUnit = 1/Utils.DPPX(canvas).X; //per pixel
                    tickTextCount = 10.0;
                    tick_width = 10.0 * Utils.DPPX(canvas).X; //per pixel
                    tick_text_scale = 0.1;
                    break;
                case MeasurementUnits.Inches:
                    dotsPerUnit = Utils.DPI(canvas).X;
                    tickTextCount = 16;
                    tick_width = dotsPerUnit / 16;
                    tick_text_scale = 16;
                    tick1Height = canvas.ActualHeight / 4;
                    tickHalfHeight = canvas.ActualHeight / 3;
                    break;
                case MeasurementUnits.Centimeters:
                    dotsPerUnit = Utils.DPCM(canvas).X;
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
    }
}
