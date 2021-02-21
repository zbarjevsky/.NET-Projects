using MkZ.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

using MkZ.Tools;
using System.Runtime.CompilerServices;
using MkZ.Windows.DwmApi;

namespace WindowColors.Utils
{
    public class SysColorIdx
    {
        public int Index = -1;
        public string Name = "";
        public string Desc = "";

        public SysColorIdx(int index, string desc, [CallerMemberName] string propertyName = null)
        {
            Index = index;
            Name = propertyName;
            Desc = desc;
        }

        public override string ToString()
        {
            return string.Format("SysColorIdx ({0}) {1}", Index, Desc);
        }
    }

    //https://www.pinvoke.net/default.aspx/Constants/COLOR_.html
    public class SysColorIdxsEnum
    {
        public static readonly SysColorIdx NONE                 = new SysColorIdx(-1, "--N/A--");
        public static readonly SysColorIdx SCROLLBAR            = new SysColorIdx(0, "Scrollbar");
        public static readonly SysColorIdx BACKGROUND           = new SysColorIdx(1, "Window");
        public static readonly SysColorIdx DESKTOP              = new SysColorIdx(1,"Window");
        public static readonly SysColorIdx ACTIVECAPTION        = new SysColorIdx(2, "ActiveTitle");
        public static readonly SysColorIdx INACTIVECAPTION      = new SysColorIdx(3, "InactiveTitle");
        public static readonly SysColorIdx MENU                 = new SysColorIdx(4, "Menu");
        public static readonly SysColorIdx MENUBAR              = new SysColorIdx(4, "MenuBar");
        public static readonly SysColorIdx WINDOW               = new SysColorIdx(5, "Window");
        public static readonly SysColorIdx WINDOWFRAME          = new SysColorIdx(6, "WindowFrame");
        public static readonly SysColorIdx MENUTEXT             = new SysColorIdx(7, "MenuText");
        public static readonly SysColorIdx WINDOWTEXT           = new SysColorIdx(8, "WindowText");
        public static readonly SysColorIdx CAPTIONTEXT          = new SysColorIdx(9, "TitleText");
        public static readonly SysColorIdx ACTIVEBORDER         = new SysColorIdx(10, "ActiveBorder");
        public static readonly SysColorIdx INACTIVEBORDER       = new SysColorIdx(11, "InactiveBorder");
        public static readonly SysColorIdx APPWORKSPACE         = new SysColorIdx(12, "AppWorkSpace");
        public static readonly SysColorIdx HIGHLIGHT            = new SysColorIdx(13, "Hilight");
        public static readonly SysColorIdx HIGHLIGHTTEXT        = new SysColorIdx(14, "HilightText");
        public static readonly SysColorIdx BTNFACE              = new SysColorIdx(15, "ButtonFace");
        public static readonly SysColorIdx THREEDFACE           = new SysColorIdx(15, "ButtonFace");
        public static readonly SysColorIdx BTNSHADOW            = new SysColorIdx(16, "ButtonShadow");
        public static readonly SysColorIdx THREEDSHADOW         = new SysColorIdx(16, "ButtonShadow");
        public static readonly SysColorIdx GRAYTEXT             = new SysColorIdx(17, "GrayText");
        public static readonly SysColorIdx BTNTEXT              = new SysColorIdx(18, "ButtonText");
        public static readonly SysColorIdx INACTIVECAPTIONTEXT  = new SysColorIdx(19, "InactiveTitleText");
        public static readonly SysColorIdx BTNHIGHLIGHT         = new SysColorIdx(20, "ButtonHiLight");
        public static readonly SysColorIdx TREEDHIGHLIGHT       = new SysColorIdx(20, "ButtonHiLight");
        public static readonly SysColorIdx THREEDHILIGHT        = new SysColorIdx(20, "ButtonHiLight");
        public static readonly SysColorIdx BTNHILIGHT           = new SysColorIdx(20, "ButtonHiLight");
        public static readonly SysColorIdx THREEDDKSHADOW       = new SysColorIdx(21, "ButtonDkShadow");
        public static readonly SysColorIdx THREEDLIGHT          = new SysColorIdx(22, "ButtonLight");
        public static readonly SysColorIdx INFOTEXT             = new SysColorIdx(23, "InfoText");
        public static readonly SysColorIdx INFOBK               = new SysColorIdx(24, "InfoWindow");

        public static Dictionary<string, SysColorIdx> GetSysColorsIdxs()
        {
            List<FieldInfo> props = typeof(SysColorIdxsEnum).GetRuntimeFields().ToList();

            Dictionary<string, SysColorIdx> colours = props.Select(c => (SysColorIdx)c.GetValue(null)).ToDictionary(i => i.Name, i => i);

            return colours;
        }

        public static string GetDescription(FieldInfo fi)
        {
            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0) 
                    return attributes[0].Description;
                else 
                    return fi.ToString();
            }
            return "";
        }
    }

    public class SysColorVM : NotifyPropertyChangedImpl
    {
        public static readonly Dictionary<string, SysColorIdx> sysColors = SysColorIdxsEnum.GetSysColorsIdxs();

        public int Index { get; }
        public string Name { get; }
        public string Desc { get; }
        public SolidColorBrush Brush { get; set; }
        public Color Color { get; set; }
        public string HEXValue { get => GetHEXValue(); }
        public string RGBValue { get => GetRGBValue(); }

        public SysColorVM(string desc, Color color)
        {
            SysColorIdx idx = FindColorIndex(desc);
            Index = idx.Index;
            Name = idx.Name;
            Desc = desc;
            Color = color;
            Brush = new SolidColorBrush(Color);
        }

        public System.Drawing.Color GetFormsColor()
        {
            return System.Drawing.Color.FromArgb(Color.A, Color.R, Color.G, Color.B);
        }

        public void SetFormsColor(System.Drawing.Color c, string registryKey, bool apply)
        {
            Color = Color.FromArgb(c.A, c.R, c.G, c.B);
            Brush = new SolidColorBrush(Color);

            if (apply)
            {
                ColorsHelper.SetSysColors(this, registryKey);
            }

            NotifyPropertyChangedAll();
        }

        private string GetHEXValue()
        {
            return Color.ToString();
        }

        public string GetRGBValue()
        {
            return string.Format("{0} {1} {2}", Color.R, Color.G, Color.B);
        }

        private SysColorIdx FindColorIndex(string desc)
        {
            foreach (string key in sysColors.Keys)
            {
                if (string.Compare(sysColors[key].Desc, desc, true) == 0)
                    return sysColors[key];
            }
            return SysColorIdxsEnum.NONE;
        }
    }

    public class ColorsHelper
    {
        public static List<SysColorVM> GetSysColorList(string subKeyPath = ColorsHelper.COLORS_REGISTRY_KEY1)
        {
            List<SysColorVM> sysColorList = new List<SysColorVM>();

            //List<COLOR> sysColors = CommonUtils.EnumToList(COLOR.ACTIVEBORDER);
            Dictionary<string, Color> sysColors = ColorsHelper.GetSysColorsFromRegistry(subKeyPath);
            foreach (string colorName in sysColors.Keys)
            {
                sysColorList.Add(new SysColorVM(colorName, sysColors[colorName]));
            }

            return sysColorList;
        }

        public const string WIN10_COLOR_REG_KEY = @"Software\Microsoft\Windows\DWM";

        public static List<SysColorVM> GetWin10Specific()
        {
            List<SysColorVM> sysColorList = new List<SysColorVM>();

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(WIN10_COLOR_REG_KEY, true);
            if (key == null)
                return sysColorList;

            string[] valueNames = key.GetValueNames();
            foreach (string valueName in valueNames)
            {
                if (valueName.EndsWith("Color"))
                {
                    object o = key.GetValue(valueName);
                    if (o != null)
                    {
                        Int32 color = Convert.ToInt32(o);
                        byte[] bytes = BitConverter.GetBytes(color);
                        sysColorList.Add(new SysColorVM(valueName, Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0])));
                    }
                }
            }

            return sysColorList;
        }

        public static Dictionary<string, Color> GetColorList()
        {
            Dictionary<string, Color> colours = typeof(Colors)
               .GetRuntimeProperties()
               .Select(c => new
               {
                   Color = (Color)c.GetValue(null),
                   Name = AddSpaceBeforeCapital(c.Name)
               }).ToDictionary(x => x.Name, x => x.Color);

            return colours;
        }

        public static Dictionary<string, Color> GetSystemColorList()
        {
            Dictionary<string, Color> colours = typeof(SystemColors)
               .GetRuntimeProperties()
               .Select(c => new
               {
                   Color = GetColor(c),
                   Name = AddSpaceBeforeCapital(c.Name)
               }).ToDictionary(x => x.Name, x => x.Color);

            return colours;
        }

        public const string COLORS_REGISTRY_KEY1 = "Control Panel\\Desktop\\Colors";
        public const string COLORS_REGISTRY_KEY2 = "Control Panel\\Colors";

        //https://www.pinvoke.net/default.aspx/user32.SetSysColors
        public static bool SetSysColors(SysColorVM sysColor, string subKeyPath = COLORS_REGISTRY_KEY1)
        {
            bool res = false;
            if (sysColor.Index >= 0)
            {
                //array of elements to change
                int[] elements = { sysColor.Index };

                //array of corresponding colors
                int[] colors = { System.Drawing.ColorTranslator.ToWin32(sysColor.GetFormsColor()) };

                //set the desktop color using p/invoke
                res = SetSysColors(elements.Length, elements, colors);
            }

            //save value in registry so that it will persist
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKeyPath, true);
            object o = key.GetValue(sysColor.Desc);
            if (o != null)
            {
                if (subKeyPath == WIN10_COLOR_REG_KEY)
                {
                    DwmApi.DwmpGetColorizationParameters(out DwmApi.DWM_COLORIZATION_PARAMS colors);

                    int color = BitConverter.ToInt32(new byte[] { sysColor.Color.B, sysColor.Color.G, sysColor.Color.R, sysColor.Color.A }, 0);
                    key.SetValue(sysColor.Desc, color);

                    colors.clrColor.Color = sysColor.GetFormsColor();
                    DwmApi.DwmpSetColorizationParameters(colors);
                }
                else
                {
                    key.SetValue(sysColor.Desc, string.Format("{0} {1} {2}", sysColor.Color.R, sysColor.Color.G, sysColor.Color.B));
                }
                ColorsHelper.ForceUpdateSystemSettings();
                res = true;
            }

            return res;
        }

        public static Color GetSysColor(string registryKey, string subKeyPath = COLORS_REGISTRY_KEY1)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKeyPath, true);
            object o = key.GetValue(registryKey);
            if (o != null)
            {
                string sColor = o.ToString();
                string[] vColor = sColor.Split(' ');
                byte r = Convert.ToByte(vColor[0]);
                byte g = Convert.ToByte(vColor[1]);
                byte b = Convert.ToByte(vColor[2]);

                return Color.FromArgb(255, r, g, b);
            }
            return Colors.Transparent;
        }

        public static Dictionary<string, Color> GetSysColorsFromRegistry(string subKeyPath = COLORS_REGISTRY_KEY1)
        {
            Dictionary<string, Color> systemColors = new Dictionary<string, Color>();

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKeyPath, true);
            if (key == null)
                return systemColors;

            string [] valueNames = key.GetValueNames();
            foreach (string valueName in valueNames)
            {
                object o = key.GetValue(valueName);
                if (o != null)
                {
                    string sColor = o.ToString();
                    string[] vColor = sColor.Split(' ');
                    byte r = Convert.ToByte(vColor[0]);
                    byte g = Convert.ToByte(vColor[1]);
                    byte b = Convert.ToByte(vColor[2]);

                    systemColors.Add(valueName, Color.FromArgb(255, r, g, b));
                }
            }

            return systemColors;
        }

        private static Color GetColor(PropertyInfo c)
        {
            if (c.GetValue(null) is Color col)
                return col;
            return Colors.Transparent;
        }

        private static string[] splitCapital(string name)
        {   
            //string.Join(" ", splitCapital(c.Name))
            return new string[] { name };
        }

        private static string AddSpaceBeforeCapital(string stringToSplit)
        {
            const string uExp = @"((?<=\p{Ll})\p{Lu}|\p{Lu}(?=\p{Ll}))";
            const string sExp = @"((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))";

            return System.Text.RegularExpressions.Regex.Replace(stringToSplit, sExp, " $1").Trim();
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetSysColors(int cElements, int[] lpaElements, int[] lpaRgbValues);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessageTimeout(IntPtr hWnd, int Msg, IntPtr wParam, string lParam, uint fuFlags, uint uTimeout, IntPtr lpdwResult);

        private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        private const int WM_SETTINGCHANGE = 0x1a;
        private const int SMTO_ABORTIFHUNG = 0x0002;

        public static void ForceUpdateSystemSettings()
        {
            SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, IntPtr.Zero, null, SMTO_ABORTIFHUNG, 100, IntPtr.Zero);
            SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, IntPtr.Zero, null, SMTO_ABORTIFHUNG, 100, IntPtr.Zero);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        //public static SysColorVM GetAccenColor()
        //{
        //    UISettings.GetColorValue
        //}
    }

}
