using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BarometerBT.Utils;
using MkZ.Windows;

namespace BarometerBT
{
    public class DeviceRecordVM : NotifyPropertyChangedImpl
    {
        private string _title = "N/A";
        public string Title { get { return _title; } set { _title = value; NotifyPropertyChanged(); } }

        private string _data;
        public string Data { get { return _data; } set { _data = value; NotifyPropertyChanged(); } }


        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(); }
        }


        private ushort _companyID;
        public ushort CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; NotifyPropertyChanged(); }
        }

        private byte [] _buffer = new byte[0];
        public byte [] Buffer
        {
            get { return _buffer; }
            set { _buffer = value; }
        }

        public DeviceRecordVM(string name, ushort companyID, byte [] buffer)
        {
            _buffer = buffer;
            _companyID = companyID;

            Title = string.Format("0x{0}", companyID.ToString("X"));
            Data = BitConverter.ToString(buffer);
            Name = name;
        }
    }
}
