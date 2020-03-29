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

		public abstract bool decode_func(ir_remote remote,
			ref ir_code prep, ref ir_code codep, ref ir_code postp,
			ref bool repeat_flag,
			ref lirc_t min_remaining_gapp,
			ref lirc_t max_remaining_gapp);

		public abstract lirc_t readdata(lirc_t timeout);
		public abstract void wait_for_data(lirc_t timeout);
		public abstract int data_ready();
		public abstract ir_code get_ir_code();
	}

	public class Hardware : IHardware
	{
		static Hardware()
		{
			Instance = new Hardware();
			initHardwareStruct();
		}

		public static IHardware Instance { get; }

		public lirc_t readData(lirc_t timeout)
		{

			//==========
			lirc_t data;
			//==========

			data = 0;

			if (!sendReceiveData) return 0;

			sendReceiveData->waitTillDataIsReady(timeout);

			sendReceiveData->getData(&data);

			return data;
		}

		public void wait_for_data(lirc_t timeout)
		{

			if (!sendReceiveData) return;

			sendReceiveData->waitTillDataIsReady(timeout);
		}

		int data_ready()
		{

			if (!sendReceiveData) return 0;

			if (sendReceiveData->dataReady()) return 1;

			return 0;
		}

		//struct hardware hw;

		public static void initHardwareStruct()
		{
			Instance.decode_func = &receive_decode;
			Instance.readdata = &readData;
			Instance.wait_for_data = &wait_for_data;
			Instance.data_ready = &data_ready;
			Instance.get_ir_code = NULL;

			Instance.features = LIRC_CAN_REC_MODE2;
			Instance.send_mode = 0;
			Instance.rec_mode = LIRC_MODE_MODE2;
			Instance.code_length = 0;
			Instance.resolution = 0;

			strcpy(hw.device, "hw");
			strcpy(hw.name, "IRToy");
		}
	}
}
