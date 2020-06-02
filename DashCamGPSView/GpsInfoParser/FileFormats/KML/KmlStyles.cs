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

    public class StyleGroupIcon : BaseIdTag
    {
        public StyleIcon styleIconNormal;
        public StyleIcon styleIconHighLight;
        public StyleMap styleMap;

        public StyleGroupIcon(ExtendedIconCode iconIndex, System.Drawing.Color color)
        {
            id = "icon-" + (int)iconIndex + "-" + KmlStyles.HexConverter(color);
            styleIconNormal = new StyleIcon("1", "0", color) { id = id + "-normal" };
            styleIconHighLight = new StyleIcon("1", "1", color) { id = id + "-highlight" };
            styleMap = new StyleMap(styleIconNormal.id, styleIconHighLight.id) { id = id };
        }

        public BaseIdTag [] ToArray()
        {
            return new BaseIdTag[] { styleIconNormal, styleIconHighLight, styleMap };
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

    public class StyleGroupLine : BaseIdTag
    {
        public StyleLine styleLineNormal;
        public StyleLine styleLineHighLight;
        public StyleMap styleMap;

        public StyleGroupLine(System.Drawing.Color color)
        {
            this.id = "MZ22-style";
            styleLineNormal = new StyleLine(color, 4) { id = this.id + "-normal" };
            styleLineHighLight = new StyleLine(color, 8) { id = this.id + "-highlight" };
            styleMap = new StyleMap(styleLineNormal.id, styleLineHighLight.id) { id = this.id };
        }

        public BaseIdTag[] ToArray()
        {
            return new BaseIdTag[] { styleLineNormal, styleLineHighLight, styleMap };
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
