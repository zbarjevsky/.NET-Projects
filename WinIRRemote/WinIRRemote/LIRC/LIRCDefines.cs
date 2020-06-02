using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinIRRemote.LIRC
{
	using lirc_t = System.Int32;
	using ir_code = System.UInt64;

	public static class LIRCDefines
	{
		//
		// Constants
		//
		public const uint PULSE_BIT = 0x01000000;
		public const uint PULSE_MASK = 0x00FFFFFF;

		public const bool LONG_IR_CODE = true;
		public const uint PACKET_SIZE = (256);
		public const uint RBUF_SIZE = (256);
		public const uint WBUF_SIZE = (2048);
		public const uint REC_SYNC = 8;

		public const int IR_PROTOCOL_MASK = 0x07ff;

		public const uint RAW_CODES = 0x0001;       /* for internal use only */
		public const uint RC5 = 0x0002;     /* IR data follows RC5 protocol */
		public const uint SHIFT_ENC = RC5;          /* IR data is shift encoded (name obsolete) */
		public const uint RC6 = 0x0004;     /* IR data follows RC6 protocol */
		public const uint RCMM = 0x0008;    /* IR data follows RC-MM protocol */
		public const uint SPACE_ENC = 0x0010;       /* IR data is space encoded */
		public const uint SPACE_FIRST = 0x0020;     /* bits are encoded as space+pulse */
		public const uint GOLDSTAR = 0x0040;    /* encoding found on Goldstar remote */
		public const uint GRUNDIG = 0x0080;     /* encoding found on Grundig remote */
		public const uint BO = 0x0100;  /* encoding found on Bang & Olufsen remote */
		public const uint SERIAL = 0x0200;      /* serial protocol */
		public const uint XMP = 0x0400; /* XMP protocol */

		/* additinal flags: can be orred together with protocol flag */
		public const uint REVERSE = 0x0800;
		public const uint NO_HEAD_REP = 0x1000;     /* no header for key repeats */
		public const uint NO_FOOT_REP = 0x2000; /* no foot for key repeats */
		public const uint CONST_LENGTH = 0x4000;    /* signal length+gap is always constant */
		public const uint REPEAT_HEADER = 0x8000;   /* header is also sent before repeat code */

		public const uint COMPAT_REVERSE = 0x00010000;/* compatibility mode for REVERSE flag */

		public const uint REPEAT_MAX_DEFAULT = 600;

		public const uint DEFAULT_FREQ = 38000;

		public const uint IR_PARITY_NONE = 0;
		public const uint IR_PARITY_EVEN = 1;
		public const uint IR_PARITY_ODD = 2;

		public static uint LIRC_MODE2SEND(uint x) { return x; }
		public static uint LIRC_SEND2MODE(uint x) { return x; }
		public static uint LIRC_MODE2REC(uint x) { return (x) << 16; }
		public static uint LIRC_REC2MODE(uint x) { return (x) >> 16; }

		public const uint LIRC_MODE_RAW = 0x00000001;
		public const uint LIRC_MODE_PULSE = 0x00000002;
		public const uint LIRC_MODE_MODE2 = 0x00000004;
		public const uint LIRC_MODE_CODE = 0x00000008;
		public const uint LIRC_MODE_LIRCCODE = 0x00000010;
		public const uint LIRC_MODE_STRING = 0x00000020;

		public static uint LIRC_CAN_SEND_RAW { get { return LIRC_MODE2SEND(LIRC_MODE_RAW); } }
		public static uint LIRC_CAN_SEND_PULSE { get { return LIRC_MODE2SEND(LIRC_MODE_PULSE); } }
		public static uint LIRC_CAN_SEND_MODE2 { get { return LIRC_MODE2SEND(LIRC_MODE_MODE2); } }
		public static uint LIRC_CAN_SEND_CODE { get { return LIRC_MODE2SEND(LIRC_MODE_CODE); } }
		public static uint LIRC_CAN_SEND_LIRCCODE { get { return LIRC_MODE2SEND(LIRC_MODE_LIRCCODE); } }
		public static uint LIRC_CAN_SEND_STRING { get { return LIRC_MODE2SEND(LIRC_MODE_STRING); } }

		public const uint LIRC_CAN_SEND_MASK = 0x0000003f;

		public const uint LIRC_CAN_SET_SEND_CARRIER = 0x00000100;
		public const uint LIRC_CAN_SET_SEND_DUTY_CYCLE = 0x00000200;
		public const uint LIRC_CAN_SET_TRANSMITTER_MASK = 0x00000400;

		public static uint LIRC_CAN_REC_RAW { get { return LIRC_MODE2REC(LIRC_MODE_RAW); } }
		public static uint LIRC_CAN_REC_PULSE { get { return LIRC_MODE2REC(LIRC_MODE_PULSE); } }
		public static uint LIRC_CAN_REC_MODE2 { get { return LIRC_MODE2REC(LIRC_MODE_MODE2); } }
		public static uint LIRC_CAN_REC_CODE { get { return LIRC_MODE2REC(LIRC_MODE_CODE); } }
		public static uint LIRC_CAN_REC_LIRCCODE { get { return LIRC_MODE2REC(LIRC_MODE_LIRCCODE); } }
		public static uint LIRC_CAN_REC_STRING { get { return LIRC_MODE2REC(LIRC_MODE_STRING); } }

		public static uint LIRC_CAN_REC_MASK { get { return LIRC_MODE2REC(LIRC_CAN_SEND_MASK); } }

		public static uint LIRC_CAN_SET_REC_CARRIER { get { return (LIRC_CAN_SET_SEND_CARRIER << 16); } }
		public static uint LIRC_CAN_SET_REC_DUTY_CYCLE
		{
			get { return (LIRC_CAN_SET_SEND_DUTY_CYCLE << 16); }
		}

		public const uint LIRC_CAN_SET_REC_DUTY_CYCLE_RANGE = 0x40000000;
		public const uint LIRC_CAN_SET_REC_CARRIER_RANGE = 0x80000000;
		public const uint LIRC_CAN_GET_REC_RESOLUTION = 0x20000000;

		public static uint LIRC_CAN_SEND(uint x) { return ((x) & LIRC_CAN_SEND_MASK); }
		public static uint LIRC_CAN_REC(uint x) { return ((x) & LIRC_CAN_REC_MASK); }

		public const uint LIRC_CAN_NOTIFY_DECODE = 0x01000000;

		////
		//// Typedefs
		////
		//#ifdef LONG_IR_CODE
		//typedef unsigned __int64 ir_code;
		//#else
		//typedef unsigned long ir_code;
		//#endif

		//typedef int lirc_t;

		//
		//Structure definitions
		//

		public class rbuf
		{
			public lirc_t[] data = new lirc_t[RBUF_SIZE];
			public ir_code decoded;
			public int rptr;
			public int wptr;
			public int too_long;
			public int is_biphase;
			public lirc_t pendingp;
			public lirc_t pendings;
			public lirc_t sum;
		}

		public class sbuf
		{
			public lirc_t[] data { get { return _data; } }
			public lirc_t[] _data = new lirc_t[WBUF_SIZE];
			public int wptr;
			public int too_long;
			public int is_biphase;
			public lirc_t pendingp;
			public lirc_t pendings;
			public lirc_t sum;
		}

		public class ir_code_node
		{
			public ir_code code;
			public ir_code_node next;
		}

		public class ir_ncode
		{
			public string name;
			public ir_code code;
			public int length;
			public lirc_t[] signals;
			public ir_code_node next;
			public ir_code_node current;
			public ir_code_node transmit_state;
		}

		public class mytimeval
		{
			public Int64 tv_sec;
			public Int64 tv_usec;
		}

		public class ir_remote
		{
			public string name;                 /* name of remote control */
			public ir_ncode[] codes;			//list of codes
			public int bits;                   /* bits (length of code) */
			public int flags;                  /* flags */
			public int eps;                    /* eps (_relative_ tolerance) */
			public int aeps;                   /* detecing _very short_ pulses is
								   difficult with relative tolerance
								   for some remotes,
								   this is an _absolute_ tolerance
								   to solve this problem
								   usually you can say 0 here */

			/* pulse and space lengths of: */

			public lirc_t phead, shead;         /* header */
			public lirc_t pthree, sthree;       /* 3 (only used for RC-MM) */
			public lirc_t ptwo, stwo;           /* 2 (only used for RC-MM) */
			public lirc_t pone, sone;           /* 1 */
			public lirc_t pzero, szero;         /* 0 */
			public lirc_t plead;                /* leading pulse */
			public lirc_t ptrail;              /* trailing pulse */
			public lirc_t pfoot, sfoot;         /* foot */
			public lirc_t prepeat, srepeat;      /* indicate repeating */

			public int pre_data_bits;          /* length of pre_data */
			public ir_code pre_data;           /* data which the remote sends before actual keycode */
			public int post_data_bits;         /* length of post_data */
			public ir_code post_data;          /* data which the remote sends after actual keycode */
			public lirc_t pre_p, pre_s;         /* signal between pre_data and keycode */
			public lirc_t post_p, post_s;      /* signal between keycode and post_code */

			public lirc_t gap;                 /* time between signals in usecs */
			public lirc_t gap2;                /* time between signals in usecs */
			public lirc_t repeat_gap;          /* time between two repeat codes if different from gap */
			public int toggle_bit;             /* obsolete */
			public ir_code toggle_bit_mask;    /* previously only one bit called toggle_bit */
			public int min_repeat;             /* code is repeated at least x times code sent once -> min_repeat=0 */
			public uint min_code_repeat;/*meaningful only if remote sends a repeat code: in this case this value indicates how often the real code is repeated before the repeat code is being sent */
			public uint freq;          /* modulation frequency */
			public uint duty_cycle;    /* 0<duty cycle<=100 */
			public ir_code toggle_mask;        /* Sharp (?) error detection scheme */
			public ir_code rc6_mask;           /* RC-6 doubles signal length of some bits */

			/* serial protocols */
			public uint baud;          /* can be overridden by [p|s]zero, [p|s]one */
			public uint bits_in_byte;  /* default: 8 */
			public uint parity;        /* currently unsupported */
			public uint stop_bits;     /* mapping: 1->2 1.5->3 2->4 */

			public ir_code ignore_mask;        /* mask defines which bits can be ignored when matching a code */
			/* end of user editable values */

			public ir_code toggle_bit_mask_state;
			public int toggle_mask_state;
			public int repeat_countdown;
			public ir_ncode last_code; /* code received or sent last */
			public ir_ncode toggle_code;/* toggle code received or sent last */
			public int reps;
			public mytimeval last_send;   /* time last_code was received or sent */
			public lirc_t min_remaining_gap;   /* remember gap for CONST_LENGTH remotes */
			public lirc_t max_remaining_gap;   /* gap range */
			public ir_remote next;
		};

		//
		// Functions
		//
		public static ir_code get_ir_code(ir_ncode ncode, ir_code_node node)
		{
			if (ncode.next != null && node != null) return node.code;
			return ncode.code;
		}

		public static ir_code_node get_next_ir_code_node(ir_ncode ncode, ir_code_node node)
		{
			if (node == null) return ncode.next;
			return node.next;
		}

		public static int bit_count(ir_remote remote)
		{
			return remote.pre_data_bits + remote.bits +	remote.post_data_bits;
		}

		public static int bits_set(ir_code[] data)
		{
			int ret = 0;
			for (int i = 0; i < data.Length; i++)
			{
				if ((data[i] & 1) != 0) ret++;
				data[i] >>= 1;
			}
			return ret;
		}

		public static ir_code reverse(ir_code data, int bits)
		{
			ir_code c = 0;
			for (int i = 0; i < bits; i++)
			{
				c |= (data & ((ir_code)1 << i)) != 0 ? (ir_code)(1 << (bits - 1 - i)) : 0;
			}
			return (c);
		}

		public static bool is_pulse(lirc_t data)
		{
			return ((data & PULSE_BIT) != 0);
		}

		public static bool is_space(lirc_t data)
		{
			return (!is_pulse(data));
		}

		public static bool has_repeat(ir_remote remote)
		{
			if (remote.prepeat > 0 && remote.srepeat > 0) return true;
			else return false;
		}

		public static void set_protocol(ir_remote remote, int protocol)
		{
			remote.flags &= ~(IR_PROTOCOL_MASK);
			remote.flags |= protocol;
		}

		public static bool is_raw(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == RAW_CODES) return true;
			else return false;
		}

		public static bool is_space_enc(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == SPACE_ENC) return true;
			else return false;
		}

		public static bool is_space_first(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == SPACE_FIRST) return true;
			else return false;
		}

		public static bool is_rc5(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == RC5) return (true);
			else return (false);
		}

		public static bool is_rc6(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == RC6 || remote.rc6_mask != 0) return true;
			else return false;
		}

		public static bool is_biphase(ir_remote remote)
		{
			if (is_rc5(remote) || is_rc6(remote)) return true;
			else return false;
		}

		public static bool is_rcmm(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == RCMM) return true;
			else return false;
		}

		public static bool is_goldstar(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == GOLDSTAR) return true;
			else return false;
		}

		public static bool is_grundig(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == GRUNDIG) return true;
			else return false;
		}

		public static bool is_bo(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == BO) return true;
			else return false;
		}

		public static bool is_serial(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == SERIAL) return true;
			else return false;
		}

		public static bool is_xmp(ir_remote remote)
		{
			if ((remote.flags & IR_PROTOCOL_MASK) == XMP) return true;
			else return false;
		}

		public static bool is_const(ir_remote remote)
		{
			if ((remote.flags & CONST_LENGTH) != 0) return true;
			else return false;
		}

		public static bool has_repeat_gap(ir_remote remote)
		{
			if (remote.repeat_gap > 0) return true;
			else return false;
		}

		public static bool has_pre(ir_remote remote)
		{
			if (remote.pre_data_bits > 0) return true;
			else return false;
		}

		public static bool has_post(ir_remote remote)
		{
			if (remote.post_data_bits > 0) return true;
			else return false;
		}

		public static bool has_header(ir_remote remote)
		{
			if (remote.phead > 0 && remote.shead > 0) return true;
			else return false;
		}

		public static bool has_foot(ir_remote remote)
		{
			if (remote.pfoot > 0 && remote.sfoot > 0) return true;
			else return false;
		}

		public static bool has_toggle_bit_mask(ir_remote remote)
		{
			if (remote.toggle_bit_mask > 0) return true;
			else return false;
		}

		public static bool has_ignore_mask(ir_remote remote)
		{
			if (remote.ignore_mask > 0) return true;
			else return false;
		}

		public static bool has_toggle_mask(ir_remote remote)
		{
			if (remote.toggle_mask > 0) return true;
			else return false;
		}

		public static lirc_t min_gap(ir_remote remote)
		{
			if (remote.gap2 != 0 && remote.gap2 < remote.gap)
			{
				return remote.gap2;
			}
			else
			{
				return remote.gap;
			}
		}

		public static lirc_t max_gap(ir_remote remote)
		{
			if (remote.gap2 > remote.gap)
			{
				return remote.gap2;
			}
			else
			{
				return remote.gap;
			}
		}

		public static long time_elapsed(mytimeval last, mytimeval current)
		{
			Int64 secs, diff;

			secs = current.tv_sec - last.tv_sec;

			diff = 1000000 * secs + current.tv_usec - last.tv_usec;

			return (diff);
		}

		public static ir_code gen_mask(int bits)
		{
			int i;
			ir_code mask;

			mask = 0;
			for (i = 0; i < bits; i++)
			{
				mask <<= 1;
				mask |= 1;
			}
			return (mask);
		}

		public static ir_code gen_ir_code(ir_remote remote, ir_code pre, ir_code code, ir_code post)
		{
			ir_code all;

			all = (pre & gen_mask(remote.pre_data_bits));
			all <<= remote.bits;
			all |= is_raw(remote) ? code : (code & gen_mask(remote.bits));
			all <<= remote.post_data_bits;
			all |= post & gen_mask(remote.post_data_bits);

			return all;
		}

		public static bool match_ir_code(ir_remote remote, ir_code a, ir_code b)
		{
			ir_code x1 = remote.ignore_mask | a;
			ir_code x2 = remote.ignore_mask | b;
			ir_code x3 = remote.ignore_mask | (b ^ remote.toggle_bit_mask);

			return (x1 == x2 || x1 == x3);
		}

		public static bool expect(ir_remote remote, lirc_t delta, lirc_t exdelta)
		{
			int aeps = remote.aeps;

			if (Math.Abs(exdelta - delta) <= exdelta * remote.eps / 100 || Math.Abs(exdelta - delta) <= aeps)
				return true;

			return false;
		}

		public static bool expect_at_least(ir_remote remote, lirc_t delta, lirc_t exdelta)
		{
			int aeps = remote.aeps;

			if (delta + exdelta * remote.eps / 100 >= exdelta || delta + aeps >= exdelta)
			{
				return true;
			}
			return false;
		}

		public static bool expect_at_most(ir_remote remote, lirc_t delta, lirc_t exdelta)
		{
			int aeps = remote.aeps;

			if (delta <= exdelta + exdelta * remote.eps / 100 || delta <= exdelta + aeps)
			{
				return true;
			}
			return false;
		}
	}
}

