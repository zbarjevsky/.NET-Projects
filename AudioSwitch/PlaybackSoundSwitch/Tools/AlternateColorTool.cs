using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaybackSoundSwitch.Tools
{
    //alternate colors if name changes significantly
    public class AlternateColorTool
    {
        public Color GetColor(string name)
        {
            _colorIdx = AlternateColor(name, _name, _colorIdx);
            _name = name;
            return _colors[_colorIdx];
        }

        private int _colorIdx = 0;
        private string _name = "";
        private static Color[] _colors = new Color[] { Color.AliceBlue, Color.LightYellow };

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
            if (colorIdx >= _colors.Length)
                colorIdx = 0;
            
            return colorIdx;
        }
    }
}
