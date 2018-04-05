using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DUMeterMZ
{
	public static class EventLogWrap
	{
		static string sSource = "DUMeterMZ";
		static string sLogName = "DUMeterMZLog";

		static EventLogWrap()
		{
			if (!EventLog.SourceExists(sSource))
				EventLog.CreateEventSource(sSource, sLogName);

		}

		public static void Info(string msg)
		{
			EventLog.WriteEntry(sSource, msg, EventLogEntryType.Information, 12, 12);
		}

		public static void Error(string msg)
		{
			EventLog.WriteEntry(sSource, msg, EventLogEntryType.Error, 13, 13);
		}
	}
}
