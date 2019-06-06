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
        private double dotsPerTextLabel { get; set; }
        public double tickHalfCount { get; private set; }
        public double ticksPerTextLabelCount { get; private set; }

        public double tick_count { get; private set; }

        public double tick0Height { get; private set; }
        public double tick1Height { get; private set; }
        public double tickHalfHeight { get; private set; }
        public double tickTextHeight { get; private set; }

        public double tick_width { get; private set; }

        public double tick_text_scale { get; private set; }

        public double tick_to_device_scale { get; private set; }

        //use DPI aware calculations
        //https://stackoverflow.com/questions/3286175/how-do-i-convert-a-wpf-size-to-physical-pixels
        public RulerTicsData(MeasurementUnits units, FrameworkElement canvas)
        {
            tickTextHeight = canvas.ActualHeight / 3; //every tick with number
            tick0Height = 0.4 * tickTextHeight; //every tick
            tick1Height = 0.4 * tickTextHeight; //eveery second tick
            tickHalfHeight = 0.75 * tickTextHeight; //half

            switch (units)
            {
                case MeasurementUnits.Pixels:
                    dotsPerTextLabel = 100.0; //per 100 pixel
                    ticksPerTextLabelCount = 10.0; //10 ticks per 100 pixel
                    tick_text_scale = 0.1;
                    break;
                case MeasurementUnits.Inches:
                    dotsPerTextLabel = Utils.DPI.X;
                    ticksPerTextLabelCount = 16; //16 ticks per INCH
                    tick_text_scale = 16;
                    tick1Height = 0.75 * tickTextHeight; //eveery second tick
                    tickHalfHeight = tickTextHeight; //half
                    break;
                case MeasurementUnits.Centimeters:
                    dotsPerTextLabel = Utils.DPCM.X;
                    ticksPerTextLabelCount = 10; //10 ticks per CM
                    tick_text_scale = 10;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("MeasurementUnits", "Unknown unit");
            }

            tick_width = dotsPerTextLabel / ticksPerTextLabelCount;
            tickHalfCount = ticksPerTextLabelCount / 2;

            tick_to_device_scale = Utils.ScaleFromGraphics();
            double width_in_pixels = canvas.ActualWidth * tick_to_device_scale;
            tick_count = width_in_pixels / tick_width;
        }

        public double WidthInSelectedUnits(double widthInPixels)
        {
            return widthInPixels / (tick_width * tick_text_scale);
        }
    }
}
