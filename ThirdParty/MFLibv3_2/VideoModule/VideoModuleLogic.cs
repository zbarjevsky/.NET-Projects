using CameraCapture.Interface;
using MediaFoundation;
using MediaFoundation.Misc;
using MZ.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VideoModule
{
    public class VideoModuleLogic : IDisposable
    {
        // Category for capture devices
        private readonly Guid KSCATEGORY_CAPTURE = new Guid("65E8773D-8F56-11D0-A3B9-00A0C9223196");

        private RegisterDeviceNotifications rdn;
        private CProcess camProcess;

        public Action<string, string> OnErrorAction = (msg, title) => { };
        public Action<string> OnFormatAction = (format) => { };

        public VideoModuleLogic()
        {
            //Need use thread expect UI to dispose COM objects
            Application.Current.MainWindow.Closing += (o, args) =>
            {
                var task = Task.Factory.StartNew(Dispose);
                task.Wait();
            };
        }

        private MfDevice _activeDevice;

        public void OnActivate(IDeviceInfo moniker)
        {
            _activeDevice = moniker as MfDevice;
            var format = string.Empty;
            var hr = camProcess.SetDevice(_activeDevice, ref format);
            if (!string.IsNullOrEmpty(format))
                OnFormatAction(format);
            
            MFError.ThrowExceptionForHR(hr);
        }

        public void OnOperation(string op)
        {
            var capture = camProcess as ICapture;
            switch (op)
            {
                case "Start":
                    var filename = FileHelper.SavePath + "Video " + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".mp4";
                    capture?.StartCapture(filename, MFMediaType.H264);
                    break;
                case "Stop":
                    capture?.StopCapture();
                    break;
                case "Snap":
                    capture?.SnapShot(ToTitleCase(ConfigurationManager.AppSettings["snapformat"]));
                    break;
                default:
                    throw new InvalidOperationException(op);
            }
        }

        public void InitDisplay(Image image, IntPtr hEvent)
        {
            if (rdn == null)
                rdn = new RegisterDeviceNotifications(hEvent, KSCATEGORY_CAPTURE);
            if (camProcess == null)
                //camProcess = new CPreview(image, hEvent);
                camProcess = new CCapture(image, hEvent);
        }

        private void NotifyError(string sErrorMessage, int hrErr)
        {
            var sErrMsg = MFError.GetErrorText(hrErr);
            string sMsg = $"{sErrorMessage} (HRESULT = 0x{hrErr:x}:{sErrMsg})";
            camProcess.CloseDevice();
            OnErrorAction(sMsg, "Error");
        }

        private static string ToTitleCase(string str)
        {
            return string.IsNullOrEmpty(str) ?
                str : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public void Dispose()
        {
            rdn?.Dispose();
            rdn = null;

            camProcess?.Dispose();
            camProcess = null;

            // Shut down MF
            MFExtern.MFShutdown();
        }
    }
}
