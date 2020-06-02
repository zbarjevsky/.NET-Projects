using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WinIRRemote.LIRC.LIRCDefines;

namespace WinIRRemote.IRToy.Common
{
	public static class Linux
	{
		public static bool gettimeofday(ref mytimeval a, object o)
		/* only accurate to milliseconds, instead of microseconds */
		{
			DateTime dt = DateTime.Now;
			TimeSpan ts = DateTime.Now - DateTime.FromOADate(0);

			a.tv_sec = (long)ts.TotalSeconds;
			a.tv_usec = ts.Milliseconds;

			return true;
		}
	}
}
