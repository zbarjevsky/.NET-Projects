using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using MediaFoundation;
using MediaFoundation.Alt;
using MediaFoundation.Misc;
using MediaFoundation.ReadWrite;
using MediaFoundation.Transform;

namespace VideoModule
{
    internal class CCapture : CProcess, ICapture
    {
        protected IMFSinkWriter PWriter;
        protected bool BFirstSample;
        protected long LlBaseTime;

        public CCapture(ImageDisplayData notify, IntPtr hEvent)
            : base(notify, hEvent)
        {
            PWriter = null;
            BFirstSample = false;
            LlBaseTime = 0;
        }

        public HResult StartCapture(string pwszFileName, Guid subType)
        {
            HResult hr = 0;

            lock (LockSync)
            {
                if (PReader == null)
                    return hr;

                // Create the sink writer 
                if (Succeeded(hr))
                {
                    hr = MFExtern.MFCreateSinkWriterFromURL(pwszFileName, null, null, out PWriter);
                }

                // Set up the encoding parameters.
                if (Succeeded(hr))
                {
                    var param = new EncodingParameters {Subtype = subType, Bitrate = 18000};
                    hr = ConfigureCapture(param);
                }

                if (Succeeded(hr))
                {
                    BFirstSample = true;
                    LlBaseTime = 0;
                }
            }

            return hr;
        }

        public HResult StopCapture()
        {
            HResult hr = HResult.S_OK;

            lock (LockSync)
            {
                if (PWriter != null)
                {
                    hr = PWriter.Finalize_();
                    SafeRelease(PWriter);
                    PWriter = null;
                }
            }

            return hr;
        }

        public bool IsCapturing()
        {
            bool bIsCapturing;

            lock (LockSync)
            {
                bIsCapturing = (PWriter != null);
            }

            return bIsCapturing;
        }

        private static HResult ConfigureEncoder(EncodingParameters eparams, IMFMediaType videoInStreamType, IMFSinkWriter pWriter,
            out int pdwStreamIndex)
        {
            IMFMediaType outStreamType;

            var hr = MFExtern.MFCreateMediaType(out outStreamType);

            if (Succeeded(hr))
            {
                hr = outStreamType.SetGUID(MFAttributesClsid.MF_MT_MAJOR_TYPE, MFMediaType.Video);
            }

            if (Succeeded(hr))
            {
                hr = outStreamType.SetGUID(MFAttributesClsid.MF_MT_SUBTYPE, eparams.Subtype);
            }

            if (Succeeded(hr))
            {
                hr = outStreamType.SetUINT32(MFAttributesClsid.MF_MT_AVG_BITRATE, eparams.Bitrate);
            }

            if (Succeeded(hr))
            {
                hr = CopyAttribute(videoInStreamType, outStreamType, MFAttributesClsid.MF_MT_FRAME_SIZE);
            }

            if (Succeeded(hr))
            {
                hr = CopyAttribute(videoInStreamType, outStreamType, MFAttributesClsid.MF_MT_FRAME_RATE);
            }

            if (Succeeded(hr))
            {
                hr = CopyAttribute(videoInStreamType, outStreamType, MFAttributesClsid.MF_MT_PIXEL_ASPECT_RATIO);
            }

            if (Succeeded(hr))
            {
                hr = CopyAttribute(videoInStreamType, outStreamType, MFAttributesClsid.MF_MT_INTERLACE_MODE);
            }

            pdwStreamIndex = 0;
            if (Succeeded(hr))
            {
                hr = pWriter.AddStream(outStreamType, out pdwStreamIndex);
            }

            SafeRelease(outStreamType);

            return hr;
        }

        private HResult ConfigureCapture(EncodingParameters eparam)
        {
            IMFMediaType videoStreamType = null;

            //var hr = ConfigureSourceReader(PReader);

            var hr = PReader.GetCurrentMediaType((int)MF_SOURCE_READER.FirstVideoStream, out videoStreamType);
            MfGetAttributeSize(videoStreamType, out int width, out int height);

            double compressionFactor = 50;
            double fps = 30;
            int bpp = 4; //bytes per pixel

            eparam.Bitrate = (int)(fps * width * height * bpp / compressionFactor);

            var sinkStream = 0;
            if (Succeeded(hr))
            {
                hr = ConfigureEncoder(eparam, videoStreamType, PWriter, out sinkStream);
            }

            if (Succeeded(hr))
            {
                // Register the color converter DSP for this process, in the video 
                // processor category. This will enable the sink writer to enumerate
                // the color converter when the sink writer attempts to match the
                // media types.

                hr = MFExtern.MFTRegisterLocalByCLSID(
                    typeof(CColorConvertDMO).GUID,
                    MFTransformCategory.MFT_CATEGORY_VIDEO_PROCESSOR,
                    "",
                    MFT_EnumFlag.SyncMFT,
                    0,
                    null,
                    0,
                    null
                    );
            }

            if (Succeeded(hr))
            {
                hr = PWriter.SetInputMediaType(sinkStream, videoStreamType, null);
            }

            if (Succeeded(hr))
            {
                hr = PWriter.BeginWriting();
            }

            SafeRelease(videoStreamType);

            return hr;
        }

        private static HResult CopyAttribute(IMFAttributes pSrc, IMFAttributes pDest, Guid key)
        {
            var variant = new PropVariant();

            var hr = pSrc.GetItem(key, variant);
            if (Succeeded(hr))
                hr = pDest.SetItem(key, variant);
            return hr;
        }

        public override HResult CloseDevice(Color backColor)
        {
            StopCapture();
            return base.CloseDevice(backColor);
        }

        protected override HResult OnFrame(IMFSample pSample, IMFMediaBuffer pBuffer, long llTimestamp, string ssFormat)
        {
            HResult hr;
            if (IsCapturing())
            {
                if (BFirstSample)
                {
                    LlBaseTime = llTimestamp;
                    BFirstSample = false;
                }

                // re-base the time stamp
                llTimestamp -= LlBaseTime;

                hr = pSample.SetSampleTime(llTimestamp);

                //if (Succeeded(hr))
                //{
                //    var displayTask = Task<int>.Factory.StartNew(() => Draw.DrawFrame(pBuffer));
                //    var recordTask = Task<int>.Factory.StartNew(() => PWriter.WriteSample(0, pSample));
                //    Task.WaitAll(displayTask, recordTask);
                //    hr = displayTask.Result;
                //    if (Succeeded(hr))
                //        hr = recordTask.Result;
                //}
                //Parallel.Invoke(()=>Draw.DrawFrame(pBuffer),()=> PWriter.WriteSample(0, pSample));
                if (Succeeded(hr))
                    hr = Draw.DrawFrame(pBuffer, !string.IsNullOrEmpty(ssFormat), ssFormat);
                if (Succeeded(hr))
                    hr = PWriter.WriteSample(0, pSample);
            }
            else
                hr = Draw.DrawFrame(pBuffer, !string.IsNullOrEmpty(ssFormat), ssFormat);
            return hr;
        }
    }
}
