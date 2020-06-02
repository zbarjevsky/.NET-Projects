using MediaFoundation;
using System;

namespace VideoModule
{
    public class EncodingParameters
    {
        public Guid Subtype { get; set; }
        public int Bitrate { get; set; }
    }

    internal interface ICapture
    {
        HResult StartCapture(string pwszFileName, Guid subType);
        HResult StopCapture();
        bool IsCapturing();
        void SnapShot(string format);
    }
}
