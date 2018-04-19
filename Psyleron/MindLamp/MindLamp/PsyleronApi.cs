using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MindLamp
{
	using BOOL = System.Int32;
	using bs_status = System.UInt32;
	using DataSource = System.Int32;

    //http://www.psyleron.com/reg1_sdk_vbsample.aspx
    public class PsyleronApi
	{
        public const string PsyREG = "PsyREG.dll";

		[DllImport(PsyREG)]
		public static extern int PsyREGAPIVersion();

		[DllImport(PsyREG)]
		public static extern uint PsyREGAPIBuild();

		[DllImport(PsyREG)]
		public static extern uint PsyREGEnumerateSources();

		[DllImport(PsyREG)]
		public static extern uint PsyREGGetSourceCount();

		[DllImport(PsyREG)]
		public static extern void PsyREGClearSources ();

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetSource")]
		public static extern DataSource PsyREGGetSource (int uiIndex);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGReleaseSource")]
		public static extern void PsyREGReleaseSource (DataSource source);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGOpen")]
		public static extern BOOL PsyREGOpen (DataSource source);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGClose")]
		public static extern void PsyREGClose (DataSource source);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGReset")]
		public static extern void PsyREGReset (DataSource source);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGOpened")]
		public static extern BOOL PsyREGOpened (DataSource source);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetStatus")]
		public static extern bs_status PsyREGGetStatus (DataSource source);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetDeviceType")]
		public static extern string PsyREGGetDeviceType (DataSource source, StringBuilder szBuf);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetDeviceTypeBSTR")]
		public static extern string PsyREGGetDeviceTypeBSTR (DataSource source);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetDeviceId")]
		public static extern string PsyREGGetDeviceId (DataSource source, StringBuilder szBuf);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetDeviceIdBSTR")]
		public static extern string PsyREGGetDeviceIdBSTR(DataSource source);

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetBit")]
		public static extern BOOL PsyREGGetBit(DataSource source, /*[MarshalAs(UnmanagedType.LPArray)]*/ byte[] pucBuf);

		public static byte PsyREGGetBit(DataSource source)
		{
			byte[] pucBuf = new byte[10];
			int size = PsyleronApi.PsyREGGetBit(0, pucBuf);
			return pucBuf[0];
		}

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetByte")]
		public static extern BOOL PsyREGGetByte (DataSource source, /*[MarshalAs(UnmanagedType.LPArray)]*/ byte[] pucBuf);

		public static byte PsyREGGetByte(DataSource source)
		{
			byte[] pucBuf = new byte[10];
			int size = PsyleronApi.PsyREGGetByte(0, pucBuf);
			return pucBuf[0];
		}

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetBits")]
		public static extern int  PsyREGGetBits (DataSource source, /*[MarshalAs(UnmanagedType.LPArray)]*/ byte[] pucBuf, int iMaxBits, BOOL bBlock);

		public static byte[] PsyREGGetBits(DataSource source)
		{
			byte[] pucBuf = new byte[10];
			int size = PsyleronApi.PsyREGGetBits(0, pucBuf, pucBuf.Length, 0);
			return pucBuf;
		}

		[DllImport(PsyREG, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PsyREGGetBytes")]
		public static extern int PsyREGGetBytes(DataSource source, /*[MarshalAs(UnmanagedType.LPArray)]*/ byte[]  pucBuf, int iMaxBytes, BOOL bBlock);

		public static byte[] PsyREGGetBytes(DataSource source)
		{
			byte[] pucBuf = new byte[10];
			int size = PsyleronApi.PsyREGGetBytes(0, pucBuf, pucBuf.Length, 0);
			return pucBuf;
		}

		public const int TRUE = 1;
		public const int FALSE = 0;

		public const int 	PSYREG_API_VERSION = 1;
		public const DataSource INVALID_DATASOURCE = -1;
		public enum bs_status {
			BSS_GOOD = 0x0000,
			BSS_CONNECTING = 0x0001,
			BSS_WAITING = 0x0002,
			BSS_BUSY = 0x0004,
			BSS_NODEVICE = 0x0008,
			BSS_READERROR = 0x0010,
			BSS_BADCFG = 0x0020,
			BSS_CANTPROCESS = 0x0040,
			BSS_INITERROR = 0x0080,
			BSS_TIMEOUT = 0x0100,
			BSS_GENERALERROR = 0x8000,
			BSS_INVALID = 0x0200
		}
	}
}
