using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Globalization;
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
        [TypeConverter(typeof(FontFamilyStringConverter))]
        public string FontFamily { get => _fontFamily; set => SetFontFamily(value); }

        private double _fontSize = 50;
        [Browsable(true)]
        public double FontSize { get => _fontSize; set => SetFontSize(value); }

        public System.Drawing.FontStyle FontStyle = System.Drawing.FontStyle.Bold;

        [Browsable(true)]
        [XmlIgnore]
        [TypeConverter(typeof(FontWeightStringConverter))]
        public FontWeight FontWeight { get => WeightFromStyle(FontStyle); set => SetFontWeight(value); }

        public SerializableFontForWpf()
        {

        }

        public void SetFont(System.Drawing.Font font)
        {
            _font = font;
            _fontFamily = _font.Name;
            _fontSize = _font.Size;

            FontStyle = _font.Bold ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular;

            NotifyPropertyChangedAll();
        }

        public void SetFontFamily(string fontFamily)
        {
            System.Drawing.Font font = new System.Drawing.Font(fontFamily, (float)_fontSize, FontStyle);
            SetFont(font);
        }

        public void SetFontSize(double size)
        {
            System.Drawing.Font font = new System.Drawing.Font(_fontFamily, (float)size, FontStyle);
            SetFont(font);
        }

        public void SetFontWeight(FontWeight weight)
        {
            System.Drawing.FontStyle style = StyleFromWeight(weight);
            System.Drawing.Font font = new System.Drawing.Font(_fontFamily, (float)_fontSize, style);
            SetFont(font);
        }

        public FontWeight WeightFromStyle(System.Drawing.FontStyle style)
        {
            return style == System.Drawing.FontStyle.Bold ? FontWeights.Bold : FontWeights.Normal;
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

    public class FontFamilyStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            return new StandardValuesCollection(installedFontCollection.Families.Select(f => f.Name).ToArray());
        }
    }

    internal class FontWeightStringConverter : TypeConverter
    {
        //https://docs.microsoft.com/en-us/dotnet/api/system.windows.fontweights?view=net-5.0
        private FontWeight[] _weights = new FontWeight[] { FontWeights.Bold, FontWeights.Normal };

        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_weights);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            //Debug.WriteLine("SerializableBrush CanConvertFrom type '{0}'", sourceType);
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string selectedWeight)
            {
                foreach (FontWeight weight in _weights)
                {
                    if (selectedWeight == weight.ToString())
                        return weight;
                }
                return FontWeights.Normal;
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
