using System;
using System.Globalization;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing.Design;
using System.IO;
using System.Reflection;

namespace MeditationStopWatch
{
    [Serializable]
    [TypeConverter(typeof(PlayListTypeConverter))]
    public class PlayList
    {
        public string Name { get; set; } = "Music";

        public List<string> List { get; set; } = new List<string>();

        private ReiKi.ReikiProgressBar.ReiKiSettings _reiKi;
        public ReiKi.ReikiProgressBar.ReiKiSettings ReiKiSettings
        {
            get
            {
                if (_reiKi == null)
                    _reiKi = new ReiKi.ReikiProgressBar.ReiKiSettings();
                return _reiKi;
            }
            set
            {
                _reiKi = value;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public bool ListEquals(PlayList list)
        {
            if (!ListEquals(list.List))
                return false;

            if (Name != list.Name)
                return false;

            return true;
        }

        public bool ListEquals(List<string> files)
        {
            if (files.Count != List.Count)
                return false;

            for (int i = 0; i < files.Count; i++)
            {
                if (files[i] != List[i])
                    return false;
            }

            return true;
        }

        internal class PlayListTypeConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context,
                                             CultureInfo culture,
                                             object value,
                                             Type destinationType)
            {

                if (destinationType == typeof(string) && value != null)
                {
                    return value.ToString();
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                List<PropertyDescriptor> list = new List<PropertyDescriptor>();
                if (value is PlayList)
                {
                    PlayList play_list = value as PlayList;
                    for (int i = 0; i < play_list.List.Count; i++)
                    {
                        list.Add(new PlayListPropertyDescriptor(play_list, i));
                    }
                }

                return new PropertyDescriptorCollection(list.ToArray());
            }

            private class PlayListPropertyDescriptor : PropertyDescriptor
            {
                private PlayList _play_list = null;
                private int _idx = -1;

                public PlayListPropertyDescriptor(PlayList play_list, int idx)
                    : base("#" + idx, null)
                {
                    _play_list = play_list;
                    _idx = idx;
                }

                public override string Name
                {
                    get { return "#" + _idx + ". " + _play_list.Name; }
                }

                public override Type ComponentType { get { return null; } }

                public override string DisplayName
                {
                    get
                    {
                        return string.Format("File {0}", _idx+1);
                    }
                }

                public override string Description { get { return "Music File"; } }

                public override bool CanResetValue(object component) { return true; }

                public override object GetValue(object component) { return this._play_list.List[_idx]; }

                public override void ResetValue(object component) { }

                public override void SetValue(object component, object value) { this._play_list.List[_idx] = (string)value; }

                public override bool ShouldSerializeValue(object component) { return true; }

                public override bool IsReadOnly { get { return false; } }

                public override Type PropertyType { get { return this._play_list.List[_idx].GetType(); } }
            }
        }
    }

    [Serializable]
    public class PlayLists
    {
        public List<PlayList> Collection { get; set; } = new List<PlayList>();

        public int SelectedIndex { get; set; } = 0;

        public PlayLists()
        {
            Collection.Add(new PlayList());
        }

        public PlayList SelectedPlayList
        {
            get
            {
                if(SelectedIndex >=0 && SelectedIndex < Count)
                    return Collection[SelectedIndex];
                return null;
            }
        }

        public int Count { get { return Collection.Count; } }

        public PlayList this[int idx] { get { return Collection[idx]; } }

        public void Add(PlayList list) { Collection.Add(list); }

        public void RemoveAt(int idx) { Collection.RemoveAt(idx); }

        public override string ToString()
        {
            string txt = "O: ";
            foreach (PlayList list in Collection)
            {
                txt += list.Name + ", ";
            }
            return txt;
        }
    }

    [Serializable]
    [DefaultPropertyAttribute("ClockBackground")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AnalogClockSettings
    {
        [DisplayName("Hour Hand Color")]
        [Description("Hour Hand Color")]
        [DefaultValue(typeof(Color), "DarkGoldenrod")]
        public Color HourHandColor { get; set; } = Color.DarkGoldenrod;

        [DisplayName("Minute Hand Color")]
        [Description("Minute Hand Color")]
        [DefaultValue(typeof(Color), "Goldenrod")]
        public Color MinuteHandColor { get; set; } = Color.Goldenrod;

        [DisplayName("Hour and Minute Opacity")]
        [Description("Hour and Minute Opacity (0-255")]
        [DefaultValue(typeof(byte), "255")]
        public byte HandOpacity { get; set; } = 255;

        [DisplayName("Second Hand Color")]
        [Description("Second Hand Color")]
        [DefaultValue(typeof(Color), "Red")]
        public Color SecondHandColor { get; set; } = Color.Red;

        [DisplayName("Second Hand Circle Color")]
        [Description("Second Hand Circle Color")]
        [DefaultValue(typeof(Color), "Red")]
        public Color SecondHandCircleColor { get; set; } = Color.Red;

        [DisplayName("Ticks Color")]
        [Description("Ticks Color")]
        [DefaultValue(typeof(Color), "SaddleBrown")]
        public Color TicksColor { get; set; } = Color.SaddleBrown;

        [DisplayName("Ticks Background Color")]
        [Description("Ticks Background Color")]
        [DefaultValue(typeof(Color), "Black")]
        public Color TicksBackColor { get; set; } = Color.Black;

        [DisplayName("Background")]
        [Description("Clock Background")]
        [DefaultValue(typeof(Color), "Black")]
        public Color ClockBackground { get; set; } = Color.Black;

        public bool Draw1MinuteTicks { get; set; } = true;

        public bool Draw5MinuteTicks { get; set; } = true;

        public bool SuspendScreenSaver { get; set; } = false;

        public override string ToString()
        {
            return "Analog Clock Settings";
        }

        public AnalogClockSettings Clone()
        {
            return new AnalogClockSettings()
            {
                HourHandColor = HourHandColor,
                MinuteHandColor = MinuteHandColor,
                HandOpacity = HandOpacity,
                SecondHandColor = SecondHandColor,
                SecondHandCircleColor = SecondHandCircleColor,
                TicksColor = TicksColor,
                TicksBackColor = TicksBackColor,
                ClockBackground = ClockBackground,
                Draw1MinuteTicks = Draw1MinuteTicks,
                Draw5MinuteTicks = Draw5MinuteTicks,
                SuspendScreenSaver = SuspendScreenSaver
            };
        }
    }

    [DefaultProperty("Interface")] //will show this property as selected value
	public class Options
	{
		public Options()
		{
			LoadDefaultValues();
		}//end Constructor

		public void LoadDefaultValues()
		{
			foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
			{
                var att = prop.Attributes[typeof(DefaultValueAttribute)];
                DefaultValueAttribute attr = att as DefaultValueAttribute;
				if (attr == null) continue;
				prop.SetValue(this, attr.Value);
			}
		}

        [Category("1. Play List")]
        [DisplayName("Favorites List")]
        [Description("Favorites List")]
        [DefaultValue(new string[] { })]
        public string[] FavoritesList { get; set; }

        [Category("1. Play List")]
        [DisplayName("Play List Collection")]
        [Description("Last Play List Collections")]
        [TypeConverter(typeof(PlayListsTypeConverter))]
        public PlayLists PlayListCollection { get; set; } = new PlayLists();

        [Category("1. Play List")]
        [DisplayName("Play List Font")]
        [Description("Play List Font")]
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 12pt")]
        public Font PlayListFont { get; set; }

        [Category("2. Misc")]
		[DisplayName("Last Image")]
		[Description("Image")]
        [System.ComponentModel.Editor(
            typeof(System.Windows.Forms.Design.FileNameEditor), 
            typeof(System.Drawing.Design.UITypeEditor))]
        public string LastImageFile { get; set; }

		[Category("2. Misc")]
		[Description("Start up sound Voulume")]
		[DefaultValue(300)]
		public int Volume { get; set; }

		[Category("2. Misc")]
		[Description("Slide Show TimeOut in seconds")]
		[DefaultValue(7)]
		public int SlideShowTimeOut { get; set; }

		[Category("2. Misc")]
		[Description("Show file name over the image")]
		[DefaultValue(false)]
		public bool ShowFileName { get; set; }

		[Category("2. Misc")]
		[Description("Loop through all songs")]
		[DefaultValue(true)]
		public bool Loop { get; set; }

        #region Clock Options

        [Category("3. Clock Color Options")]
        [Description("Analog Clock Settings")]
        public AnalogClockSettings AnalogClockSettings { get; set; } = new AnalogClockSettings();

        [Category("3. Clock Color Options")]
		[DisplayName("Digital Clock Text Color")]
		[Description("Digital Clock Text Color")]
		[DefaultValue(typeof(Color), "Red")]
		public Color DigitalClockTextColor { get; set; }

        [Category("3. Clock Color Options")]
		[DisplayName("Digital Clock Back Color")]
		[Description("Digital Clock Back Color")]
		[DefaultValue(typeof(Color), "DimGray")]
		public Color DigitalClockBackColor { get; set; }

        [Category("3. Clock Color Options")]
        [Description("Digital Clock Font")]
        [DefaultValue(typeof(Font), "Imprint MT Shadow, 36pt, style=Bold")]
        public Font DigitalClockFont { get; set; }

        #endregion

        [Category("4. Position")]
		[DisplayName("Picture Size")]
		[Description("Picture Size - Splitter Position")]
		[DefaultValue(450)]
		public int PictureWidth { get; set; }

		[Category("4. Position")]
		[DisplayName("Clock Height")]
		[Description("Clock Height - Splitter Position")]
		[DefaultValue(250)]
		public int ClockHeight { get; set; }

		[Category("4. Position")]
		[DisplayName("Window State")]
		[Description("Window State")]
		[DefaultValue(FormWindowState.Normal)]
		public FormWindowState WindowState { get; set; }

        [Category("4. Position")]
        [DisplayName("Window Rectangle")]
        [Description("Window Rectangle")]
        [DefaultValue(typeof(Rectangle), "100,100,800,600")]
        public Rectangle AppRectangle { get; set; }

        [Category("4. Position")]
        [DisplayName("Soud Volume Label Bounds")]
        [Description("Diplay Volume At this Location")]
        [DefaultValue(typeof(Rectangle), "116, 26, 208, 42")]
        public Rectangle SoudVolumeLabelBounds { get; set; }

        [Category("4. Position")]
        [DisplayName("Clock Full Screen Bounds")]
        [Description("Clock Position in Full Screen Image")]
        [DefaultValue(typeof(Rectangle), "367, 33, 390, 377")]
        public Rectangle ClockFullScreenBounds { get; set; }

        [Category("5. Color Options")]
        [Description("Options Description Background color")]
        [DefaultValue(typeof(Color), "Info")]
        public Color Background { get; set; }
    }

    public class PlayListsTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is PlayList)
                {
                    PlayList list = value as PlayList;
                    return list.Name + ": " + list.List.Count;
                }

                if (value is PlayLists)
                {
                    PlayLists collection = value as PlayLists;
                    return collection.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            if (value is PlayLists)
            {
                PlayLists collection = value as PlayLists;
                for(int i = 0; i<collection.Count; i++)
                {
                    list.Add(new PlayListsPropertyDescriptor(collection, i));
                }
            }

            return new PropertyDescriptorCollection(list.ToArray());
        }

        private class PlayListsPropertyDescriptor : PropertyDescriptor
        {
            private PlayLists _collection = null;
            private int _idx = -1;

            public PlayListsPropertyDescriptor(PlayLists collection, int idx)
                : base("#" + idx, null)
            {
                _collection = collection;
                _idx = idx;
            }

            public override string Name
            {
                get { return "#" + _idx + ". " + _collection[_idx].Name; }
            }

            public override Type ComponentType { get { return null; } }

            public override string DisplayName
            {
                get
                {
                    PlayList list = this._collection[_idx];
                    return string.Format("{0} ({1})", list.Name, list.List.Count);
                }
            }

            public override string Description { get { return "mp3 File List"; } }

            public override bool CanResetValue(object component) { return true; }

            public override object GetValue(object component) { return this._collection[_idx]; }

            public override void ResetValue(object component){ }

            public override void SetValue(object component, object value) { }

            public override bool ShouldSerializeValue(object component) {  return true; }

            public override bool IsReadOnly { get { return false; } }

            public override Type PropertyType { get { return this._collection[_idx].GetType(); } }
        }
    }
    public static class PropertyGridExtensions
    {
        /// <summary>
        /// Gets the (private) PropertyGridView instance.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <returns>The PropertyGridView instance.</returns>
        private static object GetPropertyGridView(PropertyGrid propertyGrid)
        {
            //private PropertyGridView GetPropertyGridView();
            //PropertyGridView is an internal class...
            Type type = typeof(PropertyGrid);
            BindingFlags flags = BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance;
            object propertyGridView = type.InvokeMember("gridView", flags, null, propertyGrid, null);
            return propertyGridView;
        }

        /// <summary>
        /// Gets the width of the left column.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <returns>
        /// The width of the left column.
        /// </returns>
        public static int GetInternalLabelWidth(this PropertyGrid propertyGrid)
        {
            //System.Windows.Forms.PropertyGridInternal.PropertyGridView
            object gridView = GetPropertyGridView(propertyGrid);

            const BindingFlags flags = BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance;

            //protected int InternalLabelWidth
            PropertyInfo prop = gridView.GetType().GetProperty("InternalLabelWidth", flags);
            object internalLabelWidth = prop.GetValue(gridView, BindingFlags.GetProperty, null, null, null);
            return (int)internalLabelWidth;
        }

        /// <summary>
        /// Moves the splitter to the supplied horizontal position.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <param name="xpos">The horizontal position.</param>
        public static void MoveSplitterTo(this PropertyGrid propertyGrid, int xpos)
        {
            //System.Windows.Forms.PropertyGridInternal.PropertyGridView
            object gridView = GetPropertyGridView(propertyGrid);

            //private void MoveSplitterTo(int xpos);
            const BindingFlags flags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            Type type = gridView.GetType();
            MethodInfo method = type.GetMethod("MoveSplitterTo", flags);

            method.Invoke(gridView, new Object[] { xpos });
        }

        public static IEnumerable<GridItem> EnumerateAllItems(this PropertyGrid grid)
        {
            if (grid == null)
                yield break;

            // get to root item
            GridItem start = grid.SelectedGridItem;
            while (start.Parent != null)
            {
                start = start.Parent;
            }

            foreach (GridItem item in start.EnumerateAllItems())
            {
                yield return item;
            }
        }

        public static IEnumerable<GridItem> EnumerateAllItems(this GridItem item)
        {
            if (item == null)
                yield break;

            yield return item;
            foreach (GridItem child in item.GridItems)
            {
                foreach (GridItem gc in child.EnumerateAllItems())
                {
                    yield return gc;
                }
            }
        }
    }
}
