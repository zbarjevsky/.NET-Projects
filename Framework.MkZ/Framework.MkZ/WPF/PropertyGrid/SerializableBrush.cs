using MkZ.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

        public SerializableBrush()
        {

        }

        [Browsable(false)]
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
            return "SerializableBrush: " + ColorW.Name;
        }
    }

    //public class WinFormsColorConverter : System.Drawing.ColorConverter
    //{
    //    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    //    {
    //        return false;
    //    }

    //    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    //    {
    //        return true;
    //    }

    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        Debug.WriteLine("CanConvertFrom type " + sourceType);
    //        if (sourceType == typeof(string))
    //            return true;
    //        return base.CanConvertFrom(context, sourceType);
    //    }

    //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    //    {
    //        Debug.WriteLine("ConvertFrom type " + value);
    //        if (value is System.Drawing.Color c)
    //            return new SerializableBrush() { ColorW = c };
    //        return base.ConvertFrom(context, culture, value);
    //    }

    //    //public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    //    //{
    //    //    Debug.WriteLine("CanConvertTo type " + destinationType);
    //    //    return destinationType == typeof(System.Drawing.Color);
    //    //}

    //    //public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    //    //{
    //    //    Debug.WriteLine("ConvertTo Value: " + value + ", Type: " + destinationType);
    //    //    if (value is SerializableBrush br && destinationType == typeof(string))
    //    //        return br.ColorW.Name;
    //    //    return System.Drawing.Color.Black;
    //    //}
    //}

    [Editor(typeof(SerializableBrushColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
    //[TypeConverter(typeof(MyTypeConverter))]
    public class MyClass
    {
        #region Properties
        [Editor(typeof(SerializableBrushColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(SerializableBrushToColorConverter))]
        public System.Drawing.Color Color { get; set; }
        #endregion

        public override string ToString()
        {
            return Color.Name;
        }
    }

    internal class SerializableBrushToColorConverter : System.Drawing.ColorConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }

    internal class SerializableBrushColorEditor : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
        }
        
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            System.Drawing.Design.ColorEditor cd = new System.Drawing.Design.ColorEditor();
            if (value is SerializableBrush cl)
            {
                object val = cd.EditValue(provider, cl.ColorW);
                if(val is System.Drawing.Color color)
                return new SerializableBrush(color);
            }

            return value;
        }
    }
}
