using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinIRRemote.IRToy.Common;
using static WinIRRemote.LIRC.LIRCDefines;

namespace WinIRRemote.IRToy.Common
{
	using lirc_t = System.Int32;
	using size_t = System.Int32;
	using ir_code = System.UInt64;

	public class IRRemote
    {
		public ir_remote decoding		= null;
		public ir_remote last_remote	= null;
		public ir_remote repeat_remote	= null;
		public ir_ncode repeat_code	= null;

		public bool map_code(ir_remote remote, ref ir_code prep, ref ir_code codep, ref ir_code postp,

		 int pre_bits, ir_code pre,
	     int bits, ir_code code,
	     int post_bits, ir_code post)
{
	ir_code all;
	
	if(pre_bits+bits+post_bits!= remote.pre_data_bits+remote.bits+remote.post_data_bits)
	{
		return(false);
	}
	all=(pre&gen_mask(pre_bits));
	all<<=bits;
	all|=(code&gen_mask(bits));
	all<<=post_bits;
	all|=(post&gen_mask(post_bits));


	postp = (all & gen_mask(remote.post_data_bits));
	all>>=remote.post_data_bits;
	codep = (all & gen_mask(remote.bits));
	all>>=remote.bits;
	prep = (all & gen_mask(remote.pre_data_bits));
		
	return(true);
}

		public void map_gap(ir_remote remote,

		 mytimeval start, mytimeval last,
		 lirc_t signal_length,
		 ref int repeat_flagp,
		 ref lirc_t min_remaining_gapp,
		 ref lirc_t max_remaining_gapp)
		{
			// Time gap (us) between a keypress on the remote control and
			// the next one.
			lirc_t gap;

			// Check the time gap between the last keypress and this one.
			if (start.tv_sec - last.tv_sec >= 2)
			{
				// Gap of 2 or more seconds: this is not a repeated keypress.
				repeat_flagp = 0;
				gap = 0;
			} 
			else 
			{
				// Calculate the time gap in microseconds.
				gap = (lirc_t)time_elapsed(last, start);
				if (expect_at_most(remote, gap, remote.max_remaining_gap))
				{
					// The gap is shorter than a standard gap
					// (with relative or aboslute tolerance): this
					// is a repeated keypress.
					repeat_flagp = 1;
				}
				else
				{
					// Standard gap: this is a new keypress.
					repeat_flagp = 0;
				}
			}

			// Calculate extimated time gap remaining for the next code.
			if (is_const(remote)) {
				// The sum (signal_length + gap) is always constant
				// so the gap is shorter when the code is longer.
				if (min_gap(remote) > signal_length) {
					min_remaining_gapp = min_gap(remote) - signal_length;
					max_remaining_gapp = max_gap(remote) - signal_length;
				} else {
					min_remaining_gapp = 0;
					if (max_gap(remote) > signal_length)
					{
						max_remaining_gapp = max_gap(remote) - signal_length;
					}
					else
					{
						max_remaining_gapp = 0;
					}
				}
			} else {
				// The gap after the signal is always constant.
				// This is the case of Kanam Accent serial remote.
				min_remaining_gapp = min_gap(remote);
				max_remaining_gapp = max_gap(remote);
			}
		}

		public ir_ncode get_code(ir_remote remote,
					   ir_code pre, ir_code code, ir_code post,
					  ref ir_code toggle_bit_mask_statep)
		{
			ir_code pre_mask, code_mask, post_mask, toggle_bit_mask_state, all;
			bool found_code, have_code;
			ir_ncode[] codes;
			ir_ncode found;

			pre_mask = code_mask = post_mask = 0;

			if (has_toggle_bit_mask(remote))
			{
				pre_mask = remote.toggle_bit_mask >> (remote.bits + remote.post_data_bits);
				post_mask = remote.toggle_bit_mask & gen_mask(remote.post_data_bits);
			}

			if (has_ignore_mask(remote))
			{
				pre_mask |= remote.ignore_mask >> (remote.bits + remote.post_data_bits);
				post_mask |= remote.ignore_mask & gen_mask(remote.post_data_bits);
			}

			if (has_toggle_mask(remote) && (remote.toggle_mask_state % 2) != 0)
			{
				ir_code affected, mask, mask_bit;
				int bit, current_bit;

				affected = post;
				mask = remote.toggle_mask;
				for (bit = current_bit = 0; bit < bit_count(remote); bit++, current_bit++)
				{
					if (bit == remote.post_data_bits)
					{
						affected = code;
						current_bit = 0;
					}
					if (bit == remote.post_data_bits + remote.bits)
					{
						affected = pre;
						current_bit = 0;
					}
					mask_bit = mask & 1;
					(affected) ^= (mask_bit << current_bit);
					mask >>= 1;
				}
			}
			if (has_pre(remote))
			{
				if ((pre | pre_mask) != (remote.pre_data | pre_mask))
				{
					return null;
				}
			}

			if (has_post(remote))
			{
				if ((post | post_mask) != (remote.post_data | post_mask))
				{
					return null;
				}
			}

			all = gen_ir_code(remote, pre, code, post);

			toggle_bit_mask_state = all & remote.toggle_bit_mask;

			found = null;
			found_code = false;
			have_code = false;
			codes = remote.codes;
			if (codes != null)
			{
				for (int idx = 0; idx < codes.Length; idx++)
				{
					if (string.IsNullOrWhiteSpace(codes[idx].name))
						break;
				
					ir_code next_all;

					next_all = gen_ir_code(remote, remote.pre_data, get_ir_code(codes[idx], codes[idx].current),  remote.post_data);
					if (match_ir_code(remote, next_all, all))
					{
						found_code = true;
						if (codes[idx].next != null)
						{
							if (codes[idx].current == null)
							{
								codes[idx].current = codes[idx].next;
							}
							else
							{
								codes[idx].current = codes[idx].current.next;
							}
						}
						if (!have_code)
						{
							found = codes[idx];
							if (codes[idx].current == null)
							{
								have_code = true;
							}
						}
					}
					else
					{
						/* find longest matching sequence */
						ir_code_node search = codes[idx].next;
						if (search == null || (codes[idx].next != null && codes[idx].current == null))
						{
							codes[idx].current = null;
						}
						else
						{
							bool sequence_match = false;
							while (search != codes[idx].current.next)
							{
								ir_code_node prev, next;
								bool flag = true;

								prev = null; /* means codes.code */
								next = search;
								while (next != codes[idx].current)
								{
									if (get_ir_code(codes[idx], prev) != get_ir_code(codes[idx], next))
									{
										flag = false;
										break;
									}
									prev = get_next_ir_code_node(codes[idx], prev);
									next = get_next_ir_code_node(codes[idx], next);
								}
								if (flag == true)
								{
									next_all = gen_ir_code(remote, remote.pre_data,
												   get_ir_code(codes[idx], prev),
												   remote.post_data);
									if (match_ir_code(remote, next_all, all))
									{
										codes[idx].current = get_next_ir_code_node(codes[idx], prev);
										sequence_match = true;
										found_code = true;
										if (!have_code)
										{
											found = codes[idx];
										}
										break;
									}
								}
								search = search.next;
							}
							if (!sequence_match) codes[idx].current = null;
						}
					}
					//codes = codes.next;
					//idx++
				}
			}

			if (found_code && found != null && has_toggle_mask(remote))
			{
				if ((remote.toggle_mask_state % 2) == 0)
				{
					remote.toggle_code = found;
				}
				else
				{
					if (found != remote.toggle_code)
					{
						remote.toggle_code = null;
						return (null);
					}
					remote.toggle_code = null;
				}
			}
			toggle_bit_mask_statep = toggle_bit_mask_state;
			return (found);
		}

public ulong set_code(ir_remote  remote, ir_ncode found,
				 ir_code toggle_bit_mask_state,bool repeat_flag,
				 lirc_t min_remaining_gap, lirc_t max_remaining_gap)
{
	ulong code;
 mytimeval current = new mytimeval();
	 ir_remote  last_decoded = null;

Linux.gettimeofday(ref current, null);

	if(remote==last_decoded && (found==remote.last_code || (found.next!=null && found.current!=null)) &&
	   repeat_flag &&
	   time_elapsed(remote.last_send, current)<1000000 &&
	   (!has_toggle_bit_mask(remote) || toggle_bit_mask_state==remote.toggle_bit_mask_state))
	{
		if(has_toggle_mask(remote))
		{
			remote.toggle_mask_state++;
			if(remote.toggle_mask_state==4)
			{
				remote.reps++;
				remote.toggle_mask_state=2;
			}
		}
		else if(found.current==null)
		{
			remote.reps++;
		}
	}
	else
	{
		if(found.next!=null && found.current==null)
		{
			remote.reps=1;
		}
		else
		{
			remote.reps=0;
		}
		if(has_toggle_mask(remote))
		{
			remote.toggle_mask_state=1;
			remote.toggle_code=found;
		}
		if(has_toggle_bit_mask(remote))
		{
			remote.toggle_bit_mask_state=toggle_bit_mask_state;
		}
	}
	last_remote=remote;
	last_decoded=remote;
	if(found.current==null) remote.last_code=found;
	remote.last_send=current;
	remote.min_remaining_gap=min_remaining_gap;
	remote.max_remaining_gap=max_remaining_gap;
	
	code=0;
	if(has_pre(remote))
	{
		code|=remote.pre_data;
		code=code<<remote.bits;
	}
	code|=found.code;
	if(has_post(remote))
	{
		code=code<<remote.post_data_bits;
		code|=remote.post_data;
	}
	if((remote.flags&COMPAT_REVERSE) != 0)
	{
		/* actually this is wrong: pre, code and post should
		   be rotated separately but we have to stay
		   compatible with older software
		 */
		code=reverse(code, bit_count(remote));
	}
	return(code);
}

public string write_message(string remote_name,
		  string button_name, string button_suffix,
		  ir_code code, int reps)
{
			//"%016llx %02x %s%s %s\n"
			return string.Format("{0:16} {1:00} {2}{3} {4}\n",
			 code,
			 reps,
			 button_name, button_suffix,
			 remote_name);
}

public bool decodeCommand(ir_remote[]  remotes, ref string cmd)
{
	ir_remote  remote;
ir_code pre = 0, code = 0, post = 0;
ir_ncode ncode;
bool repeat_flag = true;
ir_code toggle_bit_mask_state = 0;
lirc_t min_remaining_gap = 0, max_remaining_gap = 0;
 ir_remote  scan;
ir_ncode scan_ncode;

/* use remotes carefully, it may be changed on SIGHUP */
decoding=remote=remotes[0];
	while(remote != null)
	{
		//LOGPRINTF(1,"trying \"%s\" remote",remote.name);
		
		if(Hardware.Instance.decode_func(remote, ref pre, ref code, ref post, ref repeat_flag, ref min_remaining_gap, ref max_remaining_gap) &&
		   (ncode=get_code(remote, pre, code, post, ref toggle_bit_mask_state)) != null)
		{
			int len;

code=set_code(remote, ncode, toggle_bit_mask_state,
		  repeat_flag,
		  min_remaining_gap,
		  max_remaining_gap);
			if((has_toggle_mask(remote) && remote.toggle_mask_state%2 != 0) || ncode.current!=null)
			{
				decoding=null;
				return false;
			}

			for(scan = decoding; scan != null; scan = scan.next)
			{
				for (int i = 0; i < scan.codes.Length; i++)
				{
					scan.codes[i].current = null;
				}
			}
			if(is_xmp(remote))
			{
				remote.last_code.current = remote.last_code.next;
			}
			
			cmd = write_message(
						remote.name,
						remote.last_code.name, "", code,
						remote.reps-(ncode.next != null? 1:0));
			decoding=null;
			if(cmd.Length>=PACKET_SIZE+1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else
		{
			//LOGPRINTF(1,"failed \"%s\" remote",remote.name);
		}
		remote.toggle_mask_state=0;
		remote=remote.next;
	}
	decoding=null;
	last_remote=null;

	return false;
}
	}
}
