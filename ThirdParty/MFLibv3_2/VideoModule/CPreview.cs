using System;
using System.Windows.Controls;
using MediaFoundation;

namespace VideoModule
{
    class CPreview : CProcess
    {
        public CPreview(ImageDisplayData image, IntPtr hEvent)
            :base(image, hEvent)
        { }

        protected override HResult OnFrame(IMFSample pSample, IMFMediaBuffer pBuffer, long llTimestamp, string ssFormat)
            => Draw.DrawFrame(pBuffer, !string.IsNullOrEmpty(ssFormat), ssFormat);
    }

}