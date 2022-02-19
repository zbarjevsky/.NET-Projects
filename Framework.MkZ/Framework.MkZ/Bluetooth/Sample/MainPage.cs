using MkZ.WPF.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Bluetooth.Sample
{
    public class MainPage
    {
        public string SelectedBleDeviceName { get; internal set; }
        public string SelectedBleDeviceId { get; internal set; }

        public void NotifyUser(string message, NotifyType messageType)
        {
            switch (messageType)
            {
                case NotifyType.StatusMessage:
                    PopUp.Information(message);
                    break;
                case NotifyType.ErrorMessage:
                default:
                    PopUp.Error(message);
                    break;
            }

        }
    }
}
