using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinIRRemote.IRToy
{
	using lirc_t = System.Int32;
	using ir_code = System.UInt64;
    using static WinIRRemote.LIRC.LIRCDefines;

	//
	// this hardware struct differs somewhat from the LIRC project
	// but the functions we need for IR record should be there
	// the rest are exported normally from the DLL
	// C:\Dev_Mark\Temp\winlirc-code-r315-trunk\winlirc-code-r315-trunk\DLL\Common\Hardware.h

	public abstract class IHardware
	{
		public string device { get; set; }
		public string name { get; set; }

		public ulong features { get; set; }
		public ulong send_mode { get; set; }
		public ulong rec_mode { get; set; }
		public ulong code_length { get; set; }
		public uint resolution { get; set; }

		public delegate bool decode_func_delegate(ir_remote remote,
			ref ir_code prep, ref ir_code codep, ref ir_code postp,
			ref bool repeat_flag,
			ref lirc_t min_remaining_gapp,
			ref lirc_t max_remaining_gapp);
		public decode_func_delegate decode_func;

		public delegate lirc_t readdata_delegate(lirc_t timeout);
		public readdata_delegate readdata;

		public delegate void wait_for_data_delegate(lirc_t timeout);
		public wait_for_data_delegate wait_for_data;

		public delegate int data_ready_delegate();
		public data_ready_delegate data_ready;

		public delegate ir_code get_ir_code_delegate();
		public get_ir_code_delegate get_ir_code;
	}

	public class Hardware : IHardware
	{
		public static SendReceiveData sendReceiveData { get; set; }

		static Hardware()
		{
			Instance = new Hardware();
			initHardwareStruct();
		}

		public static IHardware Instance { get; }

		public static lirc_t readdata_impl(int timeout)
		{

			//==========
			lirc_t data = 0;
			//==========

			if (sendReceiveData == null) return 0;

			sendReceiveData.waitTillDataIsReady(timeout);

			sendReceiveData.getData(out data);

			return data;
		}

		public static void wait_for_data_impl(int timeout)
		{

			if (sendReceiveData == null) return;

			sendReceiveData.waitTillDataIsReady(timeout);
		}

	 public static int data_ready_impl()
		{

			if (sendReceiveData == null) return 0;

			if (sendReceiveData.dataReady()) return 1;

			return 0;
		}

		//struct hardware hw;

		public static void initHardwareStruct()
		{
			Instance.decode_func = null; // &receive_decode;
			Instance.readdata = readdata_impl;
			Instance.wait_for_data = wait_for_data_impl;
			Instance.data_ready = data_ready_impl;
			Instance.get_ir_code = null;

			Instance.features = LIRC_CAN_REC_MODE2;
			Instance.send_mode = 0;
			Instance.rec_mode = LIRC_MODE_MODE2;
			Instance.code_length = 0;
			Instance.resolution = 0;

			Instance.device = "hw";
			Instance.name = "IRToy";
		}
	}
}
