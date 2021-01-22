using MkZ.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MkZ.WPF.PropertyGrid
{
    //[Category("Clock"), TypeConverter(typeof(ExpandableObjectConverter))]
    //[DefaultValue(typeof(System.Drawing.Color), "DarkGoldenrod")]
    //[Editor(typeof(System.Drawing.Design.ColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [Editor(typeof(SerializableBrushColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [TypeConverter(typeof(SerializableBrushToColorConverter))]
    public class SerializableBrush : NotifyPropertyChangedImpl
    {
        public const string BRUSH = "Brush: ";
        [NonSerialized]
        private readonly System.Windows.Media.BrushConverter _colorConverter = new System.Windows.Media.BrushConverter();

        public static implicit operator System.Drawing.Color(SerializableBrush br) => br.ColorW;
        public static implicit operator SerializableBrush(System.Drawing.Color color) => new SerializableBrush(color);

        private SolidColorBrush _brush = Brushes.Transparent;

        [XmlIgnore]
        [Browsable(false)]
        public SolidColorBrush B { get => _brush; set => SetProperty(ref _brush, value); }

        [XmlIgnore]
        [Browsable(false)]
        public Color C { get => B.Color; set { B.Color = value; NotifyPropertyChangedAll(); } }

        [XmlIgnore]
        [Browsable(true)]
        [DisplayName("Color")]
        [DefaultValue(typeof(System.Drawing.Color), "DarkGoldenrod")]
        public System.Drawing.Color ColorW
        {
            get => System.Drawing.Color.FromArgb(B.Color.A, B.Color.R, B.Color.G, B.Color.B);
            set
            {
                if(B.IsFrozen)
                {
                    B = new SolidColorBrush(new System.Windows.Media.Color() { A = value.A, R = value.R, G = value.G, B = value.B });
                }
                else
                {
                    B.Color = new Color() { A = value.A, R = value.R, G = value.G, B = value.B };
                }
                NotifyPropertyChangedAll();
            }
        }

        public SerializableBrush(SolidColorBrush brush)
        {
            B = brush;
        }

        public SerializableBrush(System.Drawing.Color color)
        {
            ColorW = color;
        }

        public SerializableBrush(string userDefinedValue)
        {
            if (userDefinedValue.StartsWith(BRUSH))
                userDefinedValue = userDefinedValue.Substring(BRUSH.Length);

            int pos = userDefinedValue.IndexOf(" - ");
            if (pos > 0)
                userDefinedValue = userDefinedValue.Substring(0, pos);

            //ColorW = System.Drawing.Color.FromName(userDefinedValue);
            ColorW = System.Drawing.ColorTranslator.FromHtml(userDefinedValue);
        }

        public SerializableBrush()
        {

        }

        [Browsable(false)]
        [XmlAttribute]
        public string Color
        {
            get
            {
                return _colorConverter.ConvertToString(B);
            }

            set
            {
                B = (SolidColorBrush)_colorConverter.ConvertFrom(value);
                NotifyPropertyChangedAll();
            }
        }

        public override string ToString()
        {
            string hexARGB = $"#{ColorW.A:X2}{ColorW.R:X2}{ColorW.G:X2}{ColorW.B:X2}";
            string byteARGB = $"{ColorW.A},{ColorW.R},{ColorW.G},{ColorW.B}";
            string htmlRGB = System.Drawing.ColorTranslator.ToHtml(ColorW);

            System.Drawing.Color c = KnownColorFinder.FindName(ColorW);
            if (c.IsKnownColor)
                return BRUSH + byteARGB + " - " + c.Name;
            return BRUSH + byteARGB;
        }
    }

    internal class KnownColorFinder
    {
        static ILookup<int, System.Drawing.Color> colorLookup = typeof(System.Drawing.Color)
              .GetProperties(BindingFlags.Public | BindingFlags.Static)
              .Select(f => (System.Drawing.Color)f.GetValue(null, null))
              .Where(c => c.IsNamedColor)
              .ToLookup(c => c.ToArgb());

        public static System.Drawing.Color FindName(System.Drawing.Color c)
        {
            //reset A value
            int val = (int)(0xFF000000 | c.ToArgb());
            foreach (var namedColor in colorLookup[val])
            {
                Console.WriteLine(namedColor.Name);
                return namedColor;
            }

            return c;
        }
    }

    internal class SerializableBrushToColorConverter : System.Drawing.ColorConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            Debug.WriteLine("SerializableBrush CanConvertFrom type '{0}'", sourceType);
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Debug.WriteLine("SerializableBrush ConvertFrom value: '{0}'", value);
            if (value is string s)
                return new SerializableBrush(s);
            return base.ConvertFrom(context, culture, value);
        }
    }

    internal class SerializableBrushColorEditor : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            //cd.Color = value;
            //cd.ShowDialog();
            //val = cd.Color;

            System.Drawing.Design.ColorEditor cd = new System.Drawing.Design.ColorEditor();
            if (value is SerializableBrush cl)
            {
                object val = cd.EditValue(provider, cl.ColorW);
                if(val is System.Drawing.Color color)
                return new SerializableBrush(color);
            }

            return value;
        }
        
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
            string whatImage = e.Value.ToString();
            if (e.Value is SerializableBrush br)
            {
                e.Graphics.FillRectangle(new System.Drawing.SolidBrush(br.ColorW), e.Bounds);
            }
        }
    }
}
