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
using System.Windows.Media;
using TantaCommon;
using VideoModule.Tanta;
using VideoModule.Tools;

namespace VideoModule
{
    public class VideoResolutionInfo
    {
        public string Name { get; }
        public TantaMFVideoFormatContainer Data { get; }

        public VideoResolutionInfo(TantaMFVideoFormatContainer data)
        {
            Name = data.DisplayString() + " " + data.FrameRateAsString + " fps";
            Data = data;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class VideoModuleLogic : IDisposable
    {
        // Category for capture devices
        private readonly Guid KSCATEGORY_CAPTURE = new Guid("65E8773D-8F56-11D0-A3B9-00A0C9223196");

        private RegisterDeviceNotifications rdn;
        private CProcess camProcess;

        public Action<string, string> OnErrorAction = (msg, title) => { };
        public Action<string> OnFormatAction = (format) => { };

        public ImageDisplayData ImageWrapper { get; }

        public VideoModuleLogic()
        {
            ImageWrapper = new ImageDisplayData();

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
            HResult hr = WPFUtils.ExecuteInBacgroundThread<HResult>(() =>
            {
                _activeDevice = moniker as MfDevice;
                string format = string.Empty;
                HResult hr1 = camProcess.SetDevice(_activeDevice, ref format);
                if (!string.IsNullOrEmpty(format))
                    OnFormatAction(format);
                return hr1;
            });

            MFError.ThrowExceptionForHR(hr);
        }

        public void OnClose(IDeviceInfo moniker)
        {
            HResult hr = WPFUtils.ExecuteInBacgroundThread<HResult>(() =>
            {
                _activeDevice = moniker as MfDevice;
                return camProcess.CloseDevice(Colors.AliceBlue);
            });

            MFError.ThrowExceptionForHR(hr);
        }

        public void OnOperation(string op)
        {
            HResult hr = WPFUtils.ExecuteInBacgroundThread<HResult>(() =>
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
                        return HResult.MF_E_CAPTURE_ENGINE_INVALID_OP;
                }
                return HResult.S_OK;
            });

            MFError.ThrowExceptionForHR(hr);
        }

        public void SetFlipHorizontally(bool isFlipHorizontally)
        {
            ImageWrapper.IsFlipHorizontally = isFlipHorizontally;
        }

        public void InitDisplay(IntPtr hEvent)
        {
            if (rdn == null)
                rdn = new RegisterDeviceNotifications(hEvent, KSCATEGORY_CAPTURE);
            if (camProcess == null)
                //camProcess = new CPreview(image, hEvent);
                camProcess = new CCapture(ImageWrapper, hEvent);
        }

        public static List<VideoResolutionInfo> GetVideoFormats(IDeviceInfo device)
        {
            TantaMFDevice tantaDevice = new TantaMFDevice(device.Name, device.SymbolicName, CLSID.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
            List<TantaMFVideoFormatContainer> list = EnumVideoFormats.GetVideoFormatsForCaptureDevice(tantaDevice);

            List<VideoResolutionInfo> res = list.Select(d => new VideoResolutionInfo(d)).ToList();

            return res;
        }

        private void NotifyError(string sErrorMessage, int hrErr)
        {
            var sErrMsg = MFError.GetErrorText(hrErr);
            string sMsg = $"{sErrorMessage} (HRESULT = 0x{hrErr:x}:{sErrMsg})";
            camProcess.CloseDevice(Colors.DarkRed);
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
