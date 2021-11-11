using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;

namespace DashCamGPSView.Tools
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TimeZoneUI
    {
        [Browsable(false)] // don't show in the property grid 
        public string SelectedId { get; set; } = "";

        [XmlIgnore]
        [TypeConverter(typeof(TimeZoneTypeConverter))]
        [Editor(typeof(TimeZoneTypeEditor), typeof(UITypeEditor))]
        public TimeZoneInfo TimeZone
        {
            get => GetTimeZoneInfo(SelectedId);
            set => SelectedId = value.Id;
        }

        private static List<TimeZoneInfo> _timeZones = null;
        [XmlIgnore]
        [Browsable(false)] // don't show in the property grid 
        public static List<TimeZoneInfo> TimeZones
        {
            get
            {
                if(_timeZones == null)
                {
                    _timeZones = TimeZoneInfo.GetSystemTimeZones().ToList();
                }
                return _timeZones;
            }
        }

        private TimeZoneInfo GetTimeZoneInfo(string selectedId)
        {
            TimeZoneInfo tz = TimeZones.FirstOrDefault(t => t.Id == selectedId);
            if (tz != null)
                return tz;
            return TimeZoneInfo.Local;
        }

        public override string ToString()
        {
            return "TimeZone " + TimeZone.ToString();
        }
    }

    // this defines a custom type converter to convert from an IBenchmark to a string
    // used by the property grid to display item when non edited
    public class TimeZoneTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            // we only know how to convert from to a string
            return typeof(string) == destinationType;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (typeof(string) == destinationType)
            {
                // just use the benchmark name
                TimeZoneInfo timeZone = value as TimeZoneInfo;
                if (timeZone != null)
                    return timeZone.DisplayName;
            }
            return "(none)";
        }
    }

    // this defines a custom UI type editor to display a list of possible benchmarks
    // used by the property grid to display item in edit mode
    public class TimeZoneTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // drop down mode (we'll host a listbox in the drop down)
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // use a list box
            ListBox lb = new ListBox();
            lb.SelectionMode = SelectionMode.One;
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;

            // use the IBenchmark.Name property for list box display
            lb.DisplayMember = "DisplayName";

            // get the analytic object from context
            // this is how we get the list of possible benchmarks
            //TimeZoneUI analytic = (TimeZoneUI)context.Instance;
            foreach (TimeZoneInfo timeZone in TimeZoneUI.TimeZones)
            {
                // we store benchmarks objects directly in the listbox
                int index = lb.Items.Add(timeZone);
                if (timeZone.Equals(value))
                {
                    lb.SelectedIndex = index;
                }
            }

            // show this model stuff
            _editorService.DropDownControl(lb);
            if (lb.SelectedItem == null) // no selection, return the passed-in value as is
                return value;

            return lb.SelectedItem;
        }

        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            // close the drop down as soon as something is clicked
            _editorService.CloseDropDown();
        }
    }
}
