using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Tools
{
    public enum AlternateColorPalette
    {
        Cold,
        Warm
    }

    //alternate colors if name changes significantly
    public class AlternateColorTool
    {
        public AlternateColorPalette AlternateColorPalette { get; set; }

        public AlternateColorTool(AlternateColorPalette colorSet = AlternateColorPalette.Cold)
        {
            AlternateColorPalette = colorSet;
        }

        public Color GetColor(string name)
        {
            _colorIdx = AlternateColor(name, _name, _colorIdx);
            _name = name;
            if(AlternateColorPalette == AlternateColorPalette.Cold)
                return _colorSet1[_colorIdx];
            return _colorSet2[_colorIdx];
        }

        private int _colorIdx = 0;
        private string _name = "";
        private static Color[] _colorSet1 = new Color[] { Color.AliceBlue, Color.LightYellow };
        private static Color[] _colorSet2 = new Color[] { Color.LavenderBlush, Color.Honeydew };

        private static int AlternateColor(string name, string prevName, int colorIdx)
        {
            if (string.IsNullOrWhiteSpace(prevName))
                return colorIdx;

            string[] words = prevName.Split(' ');
            string start = words[0];
            if (words.Length > 1)
                start += " " + words[1];

            if (name.StartsWith(start))
                return colorIdx;
            
            colorIdx++;
            if (colorIdx >= _colorSet1.Length)
                colorIdx = 0;
            
            return colorIdx;
        }
    }
}
