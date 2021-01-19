using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MkZ.WinForms
{
    public class WpfBrushColorUIEditor : ColorConverter
    {
    }

    //class MyColorEditor : ColorEditor
    //{
    //    public override UITypeEditorEditStyle GetEditStyle(
    //        ITypeDescriptorContext context)
    //    {
    //        return UITypeEditorEditStyle.Modal;
    //    }
    //    public override object EditValue(
    //       ITypeDescriptorContext context, IServiceProvider provider, object value)
    //    {
    //        MessageBox.Show(
    //              "We could show an editor here, but you meant Green, right?");
    //        return Color.Green;
    //    }
    //}
    class MyColorConverter : ColorConverter
    { // reference: System.Drawing.Design.dll
        public override bool GetStandardValuesSupported(
                ITypeDescriptorContext context)
        {
            return false;
        }
    }

    class TestObject
    {
        [Category("Order Colour"), Browsable(true), DisplayName("Colour")]
        [Description("The background colour for orders from this terminal")]
        //[Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(MyColorConverter))]
        public Color Colour { get; set; }
    }
}
