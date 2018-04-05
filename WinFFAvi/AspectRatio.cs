using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFFAvi
{
	public class AspectRatio
	{
		public const string ASPECT_4x3 = "4:3";
		public const string ASPECT_16x9 = "16:9";
		public const string ASPECT_16x10 = "16:10";

		public  static  string ConvertResolutionToAspect(string sResolution)
		{
			string[] sv = sResolution.Split('x');
			try
			{
				if (sv.Length == 2 && sv[0].Length > 0 && sv[1].Length > 0)
				{
					int x = Convert.ToInt32(sv[0]);
					int y = Convert.ToInt32(sv[1]);
					if (Is4x3(x, y))
						return ASPECT_4x3;
					else if (Is16x9(x, y))
						return ASPECT_16x9;
				}
			}//end try
			catch
			{
			}//end catch

			return ASPECT_16x9;
		}

		public static bool Is4x3(int x, int y)
		{
			return (x / 4 == y / 3);
		}

		public static bool Is16x9(int x, int y)
		{
			return (x / 16 == y / 9);
		}
	}//end class AspectRatio
}//end namespace WinFFAvi
