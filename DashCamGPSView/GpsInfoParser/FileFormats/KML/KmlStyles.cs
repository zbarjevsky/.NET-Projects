using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GPSDataParser.FileFormats.KML
{
    public class StyleGroupIcon
    {
        public static void ConfigureGroup(string name, 
            ref StyleIcon styleIconNormal, ref StyleIcon styleIconHighLight, ref StyleMap styleMap)
        {
            styleIconNormal = new StyleIcon("1", "0") { id = name + "-normal" };
            styleIconHighLight = new StyleIcon("1", "1") { id = name + "-highlight" };
            styleMap = new StyleMap(styleIconNormal.id, styleIconHighLight.id) { id = name };
        }
    }

    [Serializable]
    public class StyleIcon : BaseIdTag
    {
        [XmlElement("IconStyle")]
        public IconStyle iconStyle = new IconStyle();
        [XmlElement("LabelStyle")]
        public LabelStyle labelStyle = new LabelStyle();

        public StyleIcon() { }

        public StyleIcon(string scaleIcon, string scaleLabel, string color = "ff00ff00")
        {
            iconStyle = new IconStyle(scaleIcon, color);
            labelStyle.scale = scaleLabel;
        }
    }

    public class IconStyle
    {
        public string color = "ff00ff00";
        public string scale = "1";
        public KmlIcon Icon = new KmlIcon();

        public IconStyle() { }

        public IconStyle(string scale, string color = "ff00ff00")
        {
            this.scale = scale;
            this.color = color;
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
        public static void ConfigureGroup(string name,
            ref StyleLine styleLineNormal, ref StyleLine styleLineHighLight, ref StyleMap styleMap)
        {
            styleLineNormal = new StyleLine(1) { id = name + "-normal" };
            styleLineHighLight = new StyleLine(1.5) { id = name + "-highlight" };
            styleMap = new StyleMap(styleLineNormal.id, styleLineHighLight.id) { id = name };
        }
    }

    [Serializable]
    public class StyleLine : BaseIdTag
    {
        [XmlElement("LineStyle")]
        public LineStyle lineStyle = new LineStyle();

        public StyleLine() { }

        public StyleLine(double width = 1)
        {
            lineStyle = new LineStyle(width);
        }
    }

    [Serializable]
    public class LineStyle
    {
        //System.Drawing.ColorTranslator.ToHtml
        public string color = "ffff0000";
        public double width = 4;

        public LineStyle() { }

        public LineStyle(double width = 1, string color = "ff00ff00")
        {
            this.width = width;
            this.color = color;
        }
    }

    [XmlType("StyleMap")]
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
