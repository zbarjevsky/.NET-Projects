using SDKTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.Utils
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
                    CommonTools.InfoMessage(message);
                    break;
                case NotifyType.ErrorMessage:
                default:
                    CommonTools.ErrorMessage(message);
                    break;
            }

        }
    }
}
