using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GPSDataParser.FileFormats.KML
{
    public enum ExtendedIconCode
    {
        Star = 1502,
        Drink = 1517,
        Bike = 1522,
        Camera = 1535,
        Diner = 1577,
        Question = 1594,
        Walk = 1596,
        Home = 1603,
        Parking = 1644,
        SmallDot = 1739,
        Marker = 1899
    }

    public class KmlStyles
    {
        public static String HexConverter(System.Drawing.Color c)
        {
            return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static String RGBConverter(System.Drawing.Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }
    }

    public class StyleGroupIcon
    {
        //[XmlElement("Style", Order = 1)]
        public StyleIcon style1IconNormal;
        //[XmlElement("Style", Order = 2)]
        public StyleIcon style1IconHighLight;
        //[XmlElement("StyleMap", Order = 3)]
        public StyleMap style1Map;

        public StyleGroupIcon(System.Drawing.Color color)
        {
            ConfigureGroup(ExtendedIconCode.SmallDot, color,
                ref style1IconNormal, ref style1IconHighLight, ref style1Map);
        }

        public BaseIdTag [] ToArray()
        {
            return new BaseIdTag[] { style1IconNormal, style1IconHighLight, style1Map };
        }

        public static void ConfigureGroup(ExtendedIconCode iconIndex, System.Drawing.Color c,
            ref StyleIcon styleIconNormal, ref StyleIcon styleIconHighLight, ref StyleMap styleMap)
        {
            string name = "icon-" + (int)iconIndex + "-" + KmlStyles.HexConverter(c);
            styleIconNormal = new StyleIcon("1", "0", c) { id = name + "-normal" };
            styleIconHighLight = new StyleIcon("1", "1", c) { id = name + "-highlight" };
            styleMap = new StyleMap(styleIconNormal.id, styleIconHighLight.id) { id = name };
        }
    }

    [Serializable]
    [XmlRoot("Style")]
    public class StyleIcon : BaseIdTag
    {
        [XmlElement("IconStyle")]
        public IconStyle iconStyle = new IconStyle();
        [XmlElement("LabelStyle")]
        public LabelStyle labelStyle = new LabelStyle();

        public StyleIcon() { }

        public StyleIcon(string scaleIcon, string scaleLabel, System.Drawing.Color color)
        {
            iconStyle = new IconStyle(scaleIcon, color);
            labelStyle.scale = scaleLabel;
        }
    }

    public class IconStyle
    {
        public string color = "ff000000";
        public string scale = "1";
        public KmlIcon Icon = new KmlIcon();

        public IconStyle() { }

        public IconStyle(string scale, System.Drawing.Color color)
        {
            this.scale = scale;
            this.color = KmlStyles.HexConverter(color);
        }

        public class KmlIcon
        {
            public string href = "http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png";
        }
    }

    public class LabelStyle
    {
        public string scale = "0";
    }

    public class StyleGroupLine
    {
        //[XmlElement("Style", Order = 9)]
        public StyleLine style3LineNormal;
        //[XmlElement("Style", Order = 10)]
        public StyleLine style3LineHighLight;
        //[XmlElement("StyleMap", Order = 11)]
        public StyleMap style3Map;

        public StyleGroupLine(System.Drawing.Color color)
        {
            ConfigureGroup("MZ22-style", color,
                ref style3LineNormal, ref style3LineHighLight, ref style3Map);
        }

        public BaseIdTag[] ToArray()
        {
            return new BaseIdTag[] { style3LineNormal, style3LineHighLight, style3Map };
        }

        public static void ConfigureGroup(string name, System.Drawing.Color c,
            ref StyleLine styleLineNormal, ref StyleLine styleLineHighLight, ref StyleMap styleMap)
        {
            styleLineNormal = new StyleLine(c, 4) { id = name + "-normal" };
            styleLineHighLight = new StyleLine(c, 8) { id = name + "-highlight" };
            styleMap = new StyleMap(styleLineNormal.id, styleLineHighLight.id) { id = name };
        }
    }

    [Serializable]
    public class StyleLine : BaseIdTag
    {
        [XmlElement("LineStyle")]
        public LineStyle lineStyle = new LineStyle();

        public StyleLine() { }

        public StyleLine(System.Drawing.Color color, double width = 1)
        {
            lineStyle = new LineStyle(color, width);
        }
    }

    [Serializable]
    public class LineStyle
    {
        //System.Drawing.ColorTranslator.ToHtml
        public string color = "ff000000";
        public double width = 4;

        public LineStyle() { }

        public LineStyle(System.Drawing.Color color, double width = 1)
        {
            this.color = KmlStyles.HexConverter(color);
            this.width = width;
        }
    }

    [XmlRoot("StyleMap")]
    public class StyleMap : BaseIdTag
    {
        [XmlElement("Pair")]
        public KmlPair[] styles = new KmlPair[2]
        {
            new KmlPair() { key="normal"},
            new KmlPair() { key="highlight"}
        };

        public StyleMap() { }

        public StyleMap(string styleUrlNormal = "", string styleUrlHighlight = "")
        {
            styles[0].styleUrl = "#" + styleUrlNormal;
            styles[1].styleUrl = "#" + styleUrlHighlight;
        }
    }

    public class KmlPair
    {
        public string key = "";
        public string styleUrl = "";
    }

}
