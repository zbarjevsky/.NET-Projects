using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SPUD
{
	public class SpeedCam
	{
		private Int32 _spLong;
		private Int32 _spLat;
		private byte _spLimit;         //speed limit, may be mph or kmh?
		private byte _spType;          //see ct_ codes
		private int _spAngle;
		private byte _spDirection;     //low bit contains hi bit of Angle (0-359 deg)
		private byte _spFlag;          //record status, see rt_ codes
		private int _index;


		private static int s_index = 0;
		private const double epsilon = 0.00000011920929;  //conversion factor to lat/long

		public SpeedCam(int stam)
		{
			_spLong = 0;
			_spLat = 0;
			_spLimit = 90;
			_spType = (byte)CameraTypes.FixedSpeedcam;
			_spAngle = 90;
			_spDirection = (byte)CameraDirection.BiDirectional;
			_spFlag = (byte)RecordTypes.New;
			_index = ++s_index;
		}//end constructor

		public SpeedCam(SpeedCam c)
		{
			_spLong = c._spLong;
			_spLat = c._spLat;
			_spLimit = c._spLimit;
			_spType = c._spType;
			_spAngle = c._spAngle;
			_spDirection = c._spDirection;
			_spFlag = c._spFlag;
			_index = ++s_index;
		}//end constructor

		public override bool Equals(object o)
		{
			SpeedCam c = o as SpeedCam;
			if(c==null) return false;
			return _spLong == c._spLong 
				&& _spLat == c._spLat
				&& _spType == c._spType;
		}

		internal static void ResetIndex()
		{
			s_index = 0;
		}

		public enum RecordTypes
		{
			New = 0,
			Deleted = 1,
			Edited = 2
		}

		public enum CameraTypes
		{
			FixedSpeedcam = 0,
			MobileSpeedcam = 1,
			TrafficLight = 2,
			SectionSpeedcam = 3,
			RedLightCam = 4
		}
		public enum CameraDirection
		{
			Unidirectional = 0,
			BiDirectional = 1,
			AllDirections = 2,
			Unknown = 3
		}

		public int Index { get { return _index; } set { _index = value; } }
		public byte Flag { get { return _spFlag; } set { _spFlag = value; } }          //record status, see rt_ codes
		public RecordTypes sFlag { get { return (RecordTypes)_spFlag; } set { _spFlag = (byte)value; } }
		public double Longtitude { get { return Angular(_spLong); } set { _spLong = Angular(value); } }
		public double Latitude { get { return Angular(_spLat); } set { _spLat = Angular(value); } }
		public byte Kph_SpeedLimit { get { return _spLimit; } set { _spLimit = value; } } //speed limit, may be mph or kmh?
		public byte Mph_SpeedLimit { get { return Kph2Mph(_spLimit); } set { _spLimit = Mph2Kph(value); } }                 //speed limit, may be mph or kmh?
		public byte Type { get { return _spType; } set { _spType = value; } }          //see ct_ codes
		public CameraTypes sType { get { return (CameraTypes)_spType; } set { _spType = (byte)value; } }
		public int Angle { get { return _spAngle; } set { _spAngle = value; } }
		public int iAngle { get { return _spAngle + ((_spDirection & 1) * 0x100); } set { SetAngle(value); } }
		public byte Direction { get { return _spDirection; } set { _spDirection = value; } }     //low bit contains hi bit of Angle (0-359 deg)
		public CameraDirection sDirection { get { return (CameraDirection)(_spDirection / 2); } set { SetDirection(value); } }  //low bit contains hi bit of Angle (0-359 deg)

		public IComparable GetColumnValue(int col)
		{
			switch (col)
			{
				case 0: return Index;
				case 1: return Flag;
				case 2: return sFlag;
				case 3: return Longtitude;
				case 4: return Latitude;
				case 5: return Kph_SpeedLimit;
				case 6: return Mph_SpeedLimit;
				case 7: return Type;
				case 8: return sType;
				case 9: return Angle;
				case 10: return iAngle;
				case 11: return Direction;
				case 12: return sDirection;
				default: return Index;
			}
		}

		public static int GetColumnWidth(int col)
		{
			switch (col)
			{
				case 0: return 50;
				case 1: return 30;
				case 2: return 50;
				case 3: return 120;
				case 4: return 120;
				case 5: return 50;
				case 6: return 50;
				case 7: return 30;
				case 8: return 100;
				case 9: return 50;
				case 10: return 50;
				case 11: return 30;
				case 12: return 100;
				default: return 100;
			}
		}

		private static byte Kph2Mph(byte speed) { return (byte)(speed / 1.609 + 0.5); }
		private static byte Mph2Kph(byte speed) { return (byte)(speed * 1.609 + 0.5); }
		private static double Angular(int pos) { return pos * epsilon; }
		private static int Angular(double pos) { return (int)(pos / epsilon); }

		//low bit contains hi bit of Angle (0-359 deg)
		private void SetDirection(CameraDirection dir)
		{
			_spDirection = (byte)(((byte)dir * 2) | (_spDirection & 0x01)); //preserve hi bit
		}

		private void SetAngle(int angle)
		{
			if (angle > 256) //has hi bit
			{
				_spDirection = (byte)(_spDirection | 0x01); //hi bit on
				_spAngle = angle - 256;
			}
			else
			{
				_spDirection = (byte)(_spDirection & 0x10); //hi bit off
				_spAngle = angle - 256;
			}
		}

		public static SpeedCam Read(BinaryReader r)
		{
            //if ((r as FileStream).Position >= (r as FileStream).Length)
            //    return null;

            SpeedCam sp = new SpeedCam(-1);

			sp._spLong = r.ReadInt32();
			sp._spLat = r.ReadInt32();
			sp._spLimit = r.ReadByte();
			sp._spType = r.ReadByte();
			sp._spAngle = r.ReadByte();
			sp._spDirection = r.ReadByte();
			sp._spFlag = r.ReadByte();
			sp._index = s_index;

			return sp;
		}

		public enum CSVOrderFields
		{
			X = 0, Y = 1, TYPE = 2, SPEED = 3, DIRTYPE = 4, DIRECTION = 5
		}

		public static SpeedCam ReadCSV(string sLine)
		{
			SpeedCam sp = new SpeedCam(-1);
			string[] svValues = sLine.Split(',');

			sp._spLong = Angular(Convert.ToDouble(svValues[(int)CSVOrderFields.X]));
			sp._spLat = Angular(Convert.ToDouble(svValues[(int)CSVOrderFields.Y]));
			sp._spType = Convert.ToByte(svValues[(int)CSVOrderFields.TYPE]);
			sp._spType--; //adjust from CSV to enum
			int speed = Convert.ToInt32(svValues[(int)CSVOrderFields.SPEED]);
			if (speed > 200) MessageBox.Show("Speed: " + speed);
			sp._spLimit = (byte)speed;
			sp._spAngle = Convert.ToInt32(svValues[(int)CSVOrderFields.DIRECTION]);
			sp._spDirection = Convert.ToByte(svValues[(int)CSVOrderFields.DIRTYPE]);
			sp._spFlag = (byte)RecordTypes.New;
			sp._index = s_index;

			return sp;
		}

		public static void Write(BinaryWriter w, SpeedCam sp)
		{
			w.Write(sp._spLong);
			w.Write(sp._spLat);
			w.Write(sp._spLimit);
			w.Write(sp._spType);
			w.Write((byte)sp._spAngle);
			w.Write(sp._spDirection);
			w.Write(sp._spFlag);
		}
	}
}
