/* Ping.cs 
 */
namespace PingImplementation
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Threading;
	using System.Collections;
	using System.Diagnostics;
	using System.Runtime.InteropServices;
	using System.Net.NetworkInformation;
	using System.Collections.Generic;
	using System.Text;

	class PingWrapper
	{
		[Flags]
		enum ConnectionState : int
		{
			INTERNET_CONNECTION_MODEM		= 0x1,
			INTERNET_CONNECTION_LAN			= 0x2,
			INTERNET_CONNECTION_PROXY		= 0x4,
			INTERNET_RAS_INSTALLED			= 0x10,
			INTERNET_CONNECTION_OFFLINE		= 0x20,
			INTERNET_CONNECTION_CONFIGURED	= 0x40
		}//end enum ConnectionState

		[DllImport("wininet", CharSet = CharSet.Auto)]
		static extern bool InternetGetConnectedState(ref ConnectionState lpdwFlags, int dwReserved);

		static bool IsOffline()
		{
			ConnectionState state = 0;
			InternetGetConnectedState(ref state, 0);
			if (((int)ConnectionState.INTERNET_CONNECTION_OFFLINE & (int)state) != 0)
			{
				return true;
			}//end if

			return false;
		}//end IsOffline

		public static long Ping(string address)
		{
			IPAddress ip = null;
			try
			{
				ip = Dns.GetHostEntry(address).AddressList[0];
				return Ping(ip);
			}//end try
			catch (System.Net.Sockets.SocketException err)
			{
				throw new Exception("DNS Error: "+ err.Message, err);
			}//end catch
		}//end Ping

		private static long Ping(IPAddress ip)
		{
			//set options ttl=128 and no fragmentation
			PingOptions options = new PingOptions(128, true);

			//create a Ping object
			Ping ping = new Ping();

			//32 empty bytes buffer
			byte[] data = new byte[32];
			int timeout = 3000;
			PingReply reply = ping.Send(ip, timeout, data, options);

			if (reply != null)
			{
				switch (reply.Status)
				{
					case IPStatus.Success:
						System.Diagnostics.Trace.WriteLine(
							string.Format("Reply from {0}: bytes={1} time={2}ms TTL={3}",
							reply.Address, reply.Buffer.Length, reply.RoundtripTime, reply.Options?.Ttl));
						return reply.RoundtripTime;
					case IPStatus.TimedOut:
						throw new Exception(string.Format("Request timed out ({0} ms).", timeout));
					default:
						throw new Exception(string.Format("Ping failed {0}", reply.Status.ToString()));
				}//end switch
			}//end if
			else
			{
				throw new Exception("Ping failed for an unknown reason");
			}//end else
		}//end Ping
	}//end class PingWrapper
}//end namespace PingImplementation