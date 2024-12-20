﻿using System;
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
    //[TypeConverter(typeof(ExpandableObjectConverter))]
    [TypeConverter(typeof(TimeZoneUITypeConverter))]
    [Editor(typeof(TimeZoneUITypeEditor), typeof(UITypeEditor))]
    public class TimeZoneUI
    {
        [Browsable(false)] // don't show in the property grid 
        public string SelectedId { get; set; } = "";

        [XmlIgnore]
        public TimeZoneInfo TimeZone
        {
            get => FindTimeZoneInfo(SelectedId);
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

        private TimeZoneInfo FindTimeZoneInfo(string selectedId)
        {
            TimeZoneInfo tz = TimeZones.FirstOrDefault(t => t.Id == selectedId);
            if (tz != null)
                return tz;
            return TimeZoneInfo.Local;
        }

        public override string ToString()
        {
            return TimeZone.ToString();
        }
    }

    // this defines a custom type converter to convert from an IBenchmark to a string
    // used by the property grid to display item when non edited
    public class TimeZoneUITypeConverter : TypeConverter
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
                TimeZoneUI timeZone = value as TimeZoneUI;
                if (timeZone != null)
                    return timeZone.TimeZone.DisplayName;
            }
            return "(none)";
        }
    }

    // this defines a custom UI type editor to display a list of possible benchmarks
    // used by the property grid to display item in edit mode
    public class TimeZoneUITypeEditor : UITypeEditor
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
            lb.Height = 300;
            lb.SelectionMode = SelectionMode.One;
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;

            // use the IBenchmark.Name property for list box display
            lb.DisplayMember = "TimeZone";

            TimeZoneUI tzValue = (TimeZoneUI)value;

            // get the analytic object from context
            // this is how we get the list of possible benchmarks
            //TimeZoneUI analytic = (TimeZoneUI)context.Instance;
            foreach (TimeZoneInfo timeZone in TimeZoneUI.TimeZones)
            {
                // we store benchmarks objects directly in the listbox
                int index = lb.Items.Add(timeZone);
                if (timeZone.Equals(tzValue.TimeZone))
                {
                    lb.SelectedIndex = index;
                }
            }

            // show this model stuff
            _editorService.DropDownControl(lb);
            if (lb.SelectedItem == null) // no selection, return the passed-in value as is
                return value;

            return new TimeZoneUI() { TimeZone = (TimeZoneInfo)lb.SelectedItem };
        }

        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            // close the drop down as soon as something is clicked
            _editorService.CloseDropDown();
        }
    }
}
