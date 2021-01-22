using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;


using MkZ.Windows;

namespace MkZ.WPF.PropertyGrid
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SerializableFontForWpf : NotifyPropertyChangedImpl
    {
        private System.Drawing.Font _font = new System.Drawing.Font("Jokerman", 50);

        [XmlIgnore]
        [DefaultValue(typeof(System.Drawing.Font), "Jokerman")]
        public System.Drawing.Font Font { get => _font; set => SetFont(value); }

        private string _fontFamily = "Jokerman";
        [Browsable(true)]
        public string FontFamily { get => _fontFamily; set => SetFontFamily(value); }

        private double _fontSize = 50;
        [Browsable(true)]
        public double FontSize { get => _fontSize; set => SetFontSize(value); }

        private FontWeight _fontWeight = FontWeights.Normal;
        [Browsable(true)]
        public FontWeight FontWeight { get => _fontWeight; set => SetFontWeight(value); }

        public SerializableFontForWpf()
        {

        }

        public void SetFont(System.Drawing.Font font)
        {
            _font = font;
            _fontFamily = _font.Name;
            _fontSize = _font.Size;
            _fontWeight = _font.Bold ? FontWeights.Bold : FontWeights.Normal;

            NotifyPropertyChangedAll();
        }

        public void SetFontFamily(string fontFamily)
        {
            System.Drawing.FontStyle style = StyleFromWeight(_fontWeight);
            System.Drawing.Font font = new System.Drawing.Font(fontFamily, (float)_fontSize, style);
            SetFont(font);
        }

        public void SetFontSize(double size)
        {
            System.Drawing.FontStyle style = StyleFromWeight(_fontWeight);
            System.Drawing.Font font = new System.Drawing.Font(_fontFamily, (float)size, style);
            SetFont(font);
        }

        public void SetFontWeight(FontWeight weight)
        {
            System.Drawing.FontStyle style = StyleFromWeight(weight);
            System.Drawing.Font font = new System.Drawing.Font(_fontFamily, (float)_fontSize, style);
            SetFont(font);
        }

        public System.Drawing.FontStyle StyleFromWeight(FontWeight weight)
        {
            return weight == FontWeights.Bold ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular;
        }

        public override string ToString()
        {
            return "FontData: " + _font.Name;
        }
    }
}
