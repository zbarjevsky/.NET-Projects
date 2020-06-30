using MediaFoundation;
using MediaFoundation.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TantaCommon;

namespace VideoModule.Tanta
{
    public class TantaWMFUtils
    {

        // not sure why these are not defined in the MF.Net library somewhere. Nonetheless We define 
        // them here. The MF.Net samples also do this.
        public const int MF_SOURCE_READER_FIRST_VIDEO_STREAM = unchecked((int)0xfffffffc);
        public const int MF_SOURCE_READER_FIRST_AUDIO_STREAM = unchecked((int)0xfffffffd);
        public const int MF_SOURCE_READER_ANY_STREAM = unchecked((int)0xfffffffe);

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Returns a media source from the contents of a TantaMFDevice
        /// </summary>
        /// <param name="sourceDevice">the source device</param>
        /// <returns>a IMFMediaSource or null for fail</returns>
        /// <history>
        ///    01 Nov 18  Cynic - Started
        /// </history>
        public static IMFMediaSource GetMediaSourceFromTantaDevice(TantaMFDevice sourceDevice)
        {
            IMFMediaSource mediaSource = null;
            HResult hr = 0;
            IMFAttributes attributeContainer = null;

            try
            {
                if (sourceDevice == null)
                {
                    // we failed
                    throw new Exception("GetMediaSourceFromTantaDevice sourceDevice == null");
                }
                if ((sourceDevice.SymbolicName == null) || (sourceDevice.SymbolicName.Length == 0))
                {
                    // we failed
                    throw new Exception("GetMediaSourceFromTantaDevice failed null or bad symbolicLinkStr");
                }
                if (sourceDevice.DeviceType == Guid.Empty)
                {
                    // we failed
                    throw new Exception("GetMediaSourceFromTantaDevice DeviceType == Guid.Empty");
                }

                // Initialize an attribute store. We will use this to 
                // specify the device parameters.
                hr = MFExtern.MFCreateAttributes(out attributeContainer, 2);
                if (hr != HResult.S_OK)
                {
                    // we failed
                    throw new Exception("GetMediaSourceFromTantaDevice failed on call to MFCreateAttributes, retVal=" + hr.ToString());
                }
                if (attributeContainer == null)
                {
                    // we failed
                    throw new Exception("GetMediaSourceFromTantaDevice failed on call to MFEnumDeviceSources, attributeContainer == null");
                }

                // setup the attribute container, it is always a MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE here
                hr = attributeContainer.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, sourceDevice.DeviceType);
                if (hr != HResult.S_OK)
                {
                    // we failed
                    throw new Exception("GetMediaSourceFromTantaDevice failed setting up the attributes, retVal=" + hr.ToString());
                }

                // set the formal (symbolic name) name of the device as an attribute.
                hr = attributeContainer.SetString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, sourceDevice.SymbolicName);
                if (hr != HResult.S_OK)
                {
                    // we failed
                    throw new Exception("GetMediaSourceFromTantaDevice failed setting up the symbolic name, retVal=" + hr.ToString());
                }

                // get the media source from the symbolic name 
                hr = MFExtern.MFCreateDeviceSource(attributeContainer, out mediaSource);
                if (hr != HResult.S_OK)
                {
                    // we failed
                    throw new Exception("GetMediaSourceFromTantaDevice failed on call to MFCreateDeviceSource, retVal=" + hr.ToString());
                }
            }
            finally
            {
                // make sure we release the attribute memory
                if (attributeContainer != null)
                {
                    Marshal.ReleaseComObject(attributeContainer);
                }
            }
            return mediaSource;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the major media type from a stream descriptor. This will be something
        /// like MFMediaType.Audio or MFMediaType.Video
        /// </summary>
        /// <param name="streamDescriptor">the source stream descriptor</param>
        /// <returns>the major media type of the stream</returns>
        /// <history>
        ///    01 Nov 18  Cynic - Originally Written
        /// </history>
        public static Guid GetMajorMediaTypeFromStreamDescriptor(IMFStreamDescriptor streamDescriptor)
        {
            HResult hr;
            IMFMediaTypeHandler pHandler = null;
            Guid guidMajorType = Guid.Empty;

            if (streamDescriptor == null)
            {
                throw new Exception("GetMajorMediaTypeFromStreamDescriptor: No source stream descriptor provided");
            }

            // Getting the media type from a stream has to be done by first fetching a IMFMediaTypeHandler 
            // from the stream descriptor and then asking that about the media type. The type handler also has
            // to be cleaned up afterwards. This is a pretty commonly required, multi-step, operation so it
            // has been factored off here as a useful bit of building block code.

            try
            {
                // Get the media type handler for the stream. IMFMediaTypeHandler interface is a standard way of getting or 
                // setting the media types on an object
                hr = streamDescriptor.GetMediaTypeHandler(out pHandler);
                if (hr != HResult.S_OK)
                {
                    throw new Exception("GetMajorMediaTypeFromStreamDescriptor call to streamDescriptor.GetMediaTypeHandler failed. Err=" + hr.ToString());
                }
                if (pHandler == null)
                {
                    throw new Exception("GetMajorMediaTypeFromStreamDescriptor call to streamDescriptor.GetMediaTypeHandler failed. pHandler == null");
                }

                // Now we have the handler, get the major media type.
                hr = pHandler.GetMajorType(out guidMajorType);
                if (hr != HResult.S_OK)
                {
                    throw new Exception("GetMajorMediaTypeFromStreamDescriptor call to pHandler.GetMajorType failed. Err=" + hr.ToString());
                }
                if (guidMajorType == null)
                {
                    throw new Exception("GetMajorMediaTypeFromStreamDescriptor call to pHandler.GetMajorType failed. guidMajorType == null");
                }

                // return this
                return guidMajorType;
            }
            finally
            {
                // Clean up.
                if (pHandler != null)
                {
                    Marshal.ReleaseComObject(pHandler);
                }
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets the major media type from a presentationDescriptor descriptor and 
        /// and stream index. This will be something like MFMediaType.Audio or MFMediaType.Video
        /// </summary>
        /// <param name="presentationDescriptor">the source presentation descriptor</param>
        /// <param name="streamIndex">the index of the stream in the presentation we are interested in</param>
        /// <returns>the major media type of the stream</returns>
        /// <history>
        ///    01 Nov 18  Cynic - Originally Written
        /// </history>
        public static Guid GetMajorMediaTypeFromPresentationDescriptor(IMFPresentationDescriptor presentationDescriptor, int streamIndex)
        {
            HResult hr;
            Guid guidMajorType = Guid.Empty;
            bool streamIsSelected = false;
            IMFStreamDescriptor streamDescriptor = null;

            if (presentationDescriptor == null)
            {
                throw new Exception("GetMajorMediaTypeFromPresentationDescriptor: No source stream descriptor provided");
            }

            try
            {
                // get the stream descriptor
                hr = presentationDescriptor.GetStreamDescriptorByIndex(streamIndex, out streamIsSelected, out streamDescriptor);
                if (hr != HResult.S_OK)
                {
                    throw new Exception("GetMajorMediaTypeFromPresentationDescriptor call to GetStreamDescriptorByIndex failed. Err=" + hr.ToString());
                }
                if (streamDescriptor == null)
                {
                    throw new Exception("GetMajorMediaTypeFromPresentationDescriptor call tosourcePresentationDescriptor.GetStreamDescriptorByIndex failed. streamDescriptor == null");
                }

                // return this
                return GetMajorMediaTypeFromStreamDescriptor(streamDescriptor);
            }
            finally
            {
                // Clean up.
                if (streamDescriptor != null)
                {
                    Marshal.ReleaseComObject(streamDescriptor);
                }
            }
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Gets a list of all attributes contained in an object implementing the IMFAttributes interface and displays
        /// them as a human readable name. 
        /// 
        /// Adapted from
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ee663602(v=vs.85).aspx
        /// </summary>
        /// <returns>S_OK for success, nz for fail</returns>
        /// <param name="attributesContainer">the attributes container</param>
        /// <param name="maxAttributes">the maximum number of attributes</param>
        /// <param name="outSb">The output string</param>
        /// <param name="attributesToIgnore">a list of the attributes we ignore and do not report, can be null</param>
        /// <history>
        ///    01 Nov 18  Cynic - Started
        /// </history>
        public static HResult EnumerateAllAttributeNamesAsText(IMFAttributes attributesContainer, List<string> attributesToIgnore, int maxAttributes, out StringBuilder outSb)
        {
            Guid guid;
            PropVariant pv = new PropVariant();

            // we always return something here
            outSb = new StringBuilder();

            // sanity check
            if (attributesContainer == null) return HResult.E_FAIL;

            // loop through all possible attributes
            for (int attrIndex = 0; attrIndex < maxAttributes; attrIndex++)
            {
                // get the attribute from the mediaType object
                HResult hr = attributesContainer.GetItemByIndex(attrIndex, out guid, pv);
                if (hr == HResult.E_INVALIDARG)
                {
                    // we are all done, outSb should be updated
                    return HResult.S_OK;
                }
                if (hr != HResult.S_OK)
                {
                    // we failed
                    return HResult.E_FAIL;
                }
                string outName = TantaWMFUtils.ConvertGuidToName(guid);
                // are we ignoring certain ones
                if ((attributesToIgnore != null) && (attributesToIgnore.Contains(outName))) continue;
                outSb.Append(outName + ",");
            }

            return HResult.S_OK;
        }

        /// +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
        /// <summary>
        /// Converts a guid to a name. Never returns null. Inspired by 
        ///   https://msdn.microsoft.com/en-us/library/windows/desktop/ee663602(v=vs.85).aspx
        /// clipped and adapted from the MF.Net source. There still appear to be 
        /// plenty in there I have not represented here.
        /// </summary>
        /// <returns>the Guid as a text name or empty string for fail</returns>
        /// <history>
        ///    01 Nov 18  Cynic - Started
        /// </history>
        public static string ConvertGuidToName(Guid guidToConvert)
        {
            if (guidToConvert == MFMediaType.Default) return "MFMediaType";
            if (guidToConvert == MFMediaType.Audio) return "MFMediaType_Audio";
            if (guidToConvert == MFMediaType.Video) return "MFMediaType_Video";
            if (guidToConvert == MFMediaType.Protected) return "MFMediaType_Protected";
            if (guidToConvert == MFMediaType.SAMI) return "MFMediaType_SAMI";
            if (guidToConvert == MFMediaType.Script) return "MFMediaType_Script";
            if (guidToConvert == MFMediaType.Image) return "MFMediaType_Image";
            if (guidToConvert == MFMediaType.HTML) return "MFMediaType_HTML";
            if (guidToConvert == MFMediaType.Binary) return "MFMediaType_Binary";
            if (guidToConvert == MFMediaType.FileTransfer) return "MFMediaType_FileTransfer";
            if (guidToConvert == MFMediaType.Stream) return "MFMediaType_Stream";

            if (guidToConvert == MFMediaType.Base) return "Base";
            if (guidToConvert == MFMediaType.PCM) return "PCM";
            if (guidToConvert == MFMediaType.Float) return "Float";
            if (guidToConvert == MFMediaType.DTS) return "DTS";
            if (guidToConvert == MFMediaType.Dolby_AC3_SPDIF) return "Dolby_AC3_SPDIF";
            if (guidToConvert == MFMediaType.DRM) return "DRM";
            if (guidToConvert == MFMediaType.WMAudioV8) return "WMAudioV8";
            if (guidToConvert == MFMediaType.WMAudioV9) return "WMAudioV9";
            if (guidToConvert == MFMediaType.WMAudio_Lossless) return "WMAudio_Lossless";
            if (guidToConvert == MFMediaType.WMASPDIF) return "WMASPDIF";
            if (guidToConvert == MFMediaType.MSP1) return "MSP1";
            if (guidToConvert == MFMediaType.MP3) return "MP3";
            if (guidToConvert == MFMediaType.MPEG) return "MPEG";
            if (guidToConvert == MFMediaType.AAC) return "AAC";
            if (guidToConvert == MFMediaType.ADTS) return "ADTS";
            if (guidToConvert == MFMediaType.AMR_NB) return "AMR_NB";
            if (guidToConvert == MFMediaType.AMR_WB) return "AMR_WB";
            if (guidToConvert == MFMediaType.AMR_WP) return "AMR_WP";

            // {00000000-767a-494d-b478-f29d25dc9037}       MFMPEG4Format_Base
            if (guidToConvert == MFMediaType.MFMPEG4Format) return "MFMPEG4Format";

            if (guidToConvert == MFMediaType.RGB32) return "RGB32";
            if (guidToConvert == MFMediaType.ARGB32) return "ARGB32";
            if (guidToConvert == MFMediaType.RGB24) return "RGB24";
            if (guidToConvert == MFMediaType.RGB555) return "RGB555";
            if (guidToConvert == MFMediaType.RGB565) return "RGB565";
            if (guidToConvert == MFMediaType.RGB8) return "RGB8";
            if (guidToConvert == MFMediaType.AI44) return "AI44";
            if (guidToConvert == MFMediaType.AYUV) return "AYUV";
            if (guidToConvert == MFMediaType.YUY2) return "YUY2";
            if (guidToConvert == MFMediaType.YVYU) return "YVYU";
            if (guidToConvert == MFMediaType.YVU9) return "YVU9";
            if (guidToConvert == MFMediaType.UYVY) return "UYVY";
            if (guidToConvert == MFMediaType.NV11) return "NV11";
            if (guidToConvert == MFMediaType.NV12) return "NV12";
            if (guidToConvert == MFMediaType.YV12) return "YV12";
            if (guidToConvert == MFMediaType.I420) return "I420";
            if (guidToConvert == MFMediaType.IYUV) return "IYUV";
            if (guidToConvert == MFMediaType.Y210) return "Y210";
            if (guidToConvert == MFMediaType.Y216) return "Y216";
            if (guidToConvert == MFMediaType.Y410) return "Y410";
            if (guidToConvert == MFMediaType.Y416) return "Y416";
            if (guidToConvert == MFMediaType.Y41P) return "Y41P";
            if (guidToConvert == MFMediaType.Y41T) return "Y41T";
            if (guidToConvert == MFMediaType.Y42T) return "Y42T";
            if (guidToConvert == MFMediaType.P210) return "P210";
            if (guidToConvert == MFMediaType.P216) return "P216";
            if (guidToConvert == MFMediaType.P010) return "P010";
            if (guidToConvert == MFMediaType.P016) return "P016";
            if (guidToConvert == MFMediaType.v210) return "v210";
            if (guidToConvert == MFMediaType.v216) return "v216";
            if (guidToConvert == MFMediaType.v410) return "v410";
            if (guidToConvert == MFMediaType.MP43) return "MP43";
            if (guidToConvert == MFMediaType.MP4S) return "MP4S";
            if (guidToConvert == MFMediaType.M4S2) return "M4S2";
            if (guidToConvert == MFMediaType.MP4V) return "MP4V";
            if (guidToConvert == MFMediaType.WMV1) return "WMV1";
            if (guidToConvert == MFMediaType.WMV2) return "WMV2";
            if (guidToConvert == MFMediaType.WMV3) return "WMV3";
            if (guidToConvert == MFMediaType.WVC1) return "WVC1";
            if (guidToConvert == MFMediaType.MSS1) return "MSS1";
            if (guidToConvert == MFMediaType.MSS2) return "MSS2";
            if (guidToConvert == MFMediaType.MPG1) return "MPG1";
            if (guidToConvert == MFMediaType.DVSL) return "DVSL";
            if (guidToConvert == MFMediaType.DVSD) return "DVSD";
            if (guidToConvert == MFMediaType.DVHD) return "DVHD";
            if (guidToConvert == MFMediaType.DV25) return "DV25";
            if (guidToConvert == MFMediaType.DV50) return "DV50";
            if (guidToConvert == MFMediaType.DVH1) return "DVH1";
            if (guidToConvert == MFMediaType.DVC) return "DVC";
            if (guidToConvert == MFMediaType.H264) return "H264";
            if (guidToConvert == MFMediaType.MJPG) return "MJPG";
            if (guidToConvert == MFMediaType.O420) return "O420";
            if (guidToConvert == MFMediaType.HEVC) return "HEVC";
            if (guidToConvert == MFMediaType.HEVC_ES) return "HEVC_ES";

            if (guidToConvert == MFMediaType.H265) return "H265";
            if (guidToConvert == MFMediaType.VP80) return "VP80";
            if (guidToConvert == MFMediaType.VP90) return "VP90";

            if (guidToConvert == MFMediaType.FLAC) return "FLAC";
            if (guidToConvert == MFMediaType.ALAC) return "ALAC";

            if (guidToConvert == MFMediaType.MPEG2) return "MPEG2";
            if (guidToConvert == MFMediaType.MFVideoFormat_H264_ES) return "MFVideoFormat_H264_ES";
            if (guidToConvert == MFMediaType.MFAudioFormat_Dolby_AC3) return "MFAudioFormat_Dolby_AC3";
            if (guidToConvert == MFMediaType.MFAudioFormat_Dolby_DDPlus) return "MFAudioFormat_Dolby_DDPlus";
            // removed by MS - if(guidToConvert == MFMediaType.MFAudioFormat_QCELP) return "MFAudioFormat_QCELP";

            if (guidToConvert == MFMediaType.MFAudioFormat_Vorbis) return "MFAudioFormat_Vorbis";
            if (guidToConvert == MFMediaType.MFAudioFormat_LPCM) return "MFAudioFormat_LPCM";
            if (guidToConvert == MFMediaType.MFAudioFormat_PCM_HDCP) return "MFAudioFormat_PCM_HDCP";
            if (guidToConvert == MFMediaType.MFAudioFormat_Dolby_AC3_HDCP) return "MFAudioFormat_Dolby_AC3_HDCP";
            if (guidToConvert == MFMediaType.MFAudioFormat_AAC_HDCP) return "MFAudioFormat_AAC_HDCP";
            if (guidToConvert == MFMediaType.MFAudioFormat_ADTS_HDCP) return "MFAudioFormat_ADTS_HDCP";
            if (guidToConvert == MFMediaType.MFAudioFormat_Base_HDCP) return "MFAudioFormat_Base_HDCP";
            if (guidToConvert == MFMediaType.MFVideoFormat_H264_HDCP) return "MFVideoFormat_H264_HDCP";
            if (guidToConvert == MFMediaType.MFVideoFormat_Base_HDCP) return "MFVideoFormat_Base_HDCP";

            if (guidToConvert == MFMediaType.MPEG2Transport) return "MPEG2Transport";
            if (guidToConvert == MFMediaType.MPEG2Program) return "MPEG2Program";

            if (guidToConvert == MFMediaType.V216_MS) return "V216_MS";
            if (guidToConvert == MFMediaType.V410_MS) return "V410_MS";

            // Audio Renderer Attributes
            if (guidToConvert == MFAttributesClsid.MF_AUDIO_RENDERER_ATTRIBUTE_ENDPOINT_ID) return "MF_AUDIO_RENDERER_ATTRIBUTE_ENDPOINT_ID";
            if (guidToConvert == MFAttributesClsid.MF_AUDIO_RENDERER_ATTRIBUTE_ENDPOINT_ROLE) return "MF_AUDIO_RENDERER_ATTRIBUTE_ENDPOINT_ROLE";
            if (guidToConvert == MFAttributesClsid.MF_AUDIO_RENDERER_ATTRIBUTE_FLAGS) return "MF_AUDIO_RENDERER_ATTRIBUTE_FLAGS";
            if (guidToConvert == MFAttributesClsid.MF_AUDIO_RENDERER_ATTRIBUTE_SESSION_ID) return "MF_AUDIO_RENDERER_ATTRIBUTE_SESSION_ID";

            // Byte Stream Attributes
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAM_ORIGIN_NAME) return "MF_BYTESTREAM_ORIGIN_NAME";
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAM_CONTENT_TYPE) return "MF_BYTESTREAM_CONTENT_TYPE";
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAM_DURATION) return "MF_BYTESTREAM_DURATION";
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAM_LAST_MODIFIED_TIME) return "MF_BYTESTREAM_LAST_MODIFIED_TIME";
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAM_IFO_FILE_URI) return "MF_BYTESTREAM_IFO_FILE_URI";
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAM_DLNA_PROFILE_ID) return "MF_BYTESTREAM_DLNA_PROFILE_ID";
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAM_EFFECTIVE_URL) return "MF_BYTESTREAM_EFFECTIVE_URL";
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAM_TRANSCODED) return "MF_BYTESTREAM_TRANSCODED";

            // Enhanced Video Renderer Attributes
            if (guidToConvert == MFAttributesClsid.MF_ACTIVATE_CUSTOM_VIDEO_MIXER_ACTIVATE) return "MF_ACTIVATE_CUSTOM_VIDEO_MIXER_ACTIVATE";
            if (guidToConvert == MFAttributesClsid.MF_ACTIVATE_CUSTOM_VIDEO_MIXER_CLSID) return "MF_ACTIVATE_CUSTOM_VIDEO_MIXER_CLSID";
            if (guidToConvert == MFAttributesClsid.MF_ACTIVATE_CUSTOM_VIDEO_MIXER_FLAGS) return "MF_ACTIVATE_CUSTOM_VIDEO_MIXER_FLAGS";
            if (guidToConvert == MFAttributesClsid.MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_ACTIVATE) return "MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_ACTIVATE";
            if (guidToConvert == MFAttributesClsid.MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_CLSID) return "MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_CLSID";
            if (guidToConvert == MFAttributesClsid.MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_FLAGS) return "MF_ACTIVATE_CUSTOM_VIDEO_PRESENTER_FLAGS";
            if (guidToConvert == MFAttributesClsid.MF_ACTIVATE_VIDEO_WINDOW) return "MF_ACTIVATE_VIDEO_WINDOW";
            if (guidToConvert == MFAttributesClsid.MF_SA_REQUIRED_SAMPLE_COUNT) return "MF_SA_REQUIRED_SAMPLE_COUNT";
            if (guidToConvert == MFAttributesClsid.MF_SA_REQUIRED_SAMPLE_COUNT_PROGRESSIVE) return "MF_SA_REQUIRED_SAMPLE_COUNT_PROGRESSIVE";
            if (guidToConvert == MFAttributesClsid.MF_SA_MINIMUM_OUTPUT_SAMPLE_COUNT) return "MF_SA_MINIMUM_OUTPUT_SAMPLE_COUNT";
            if (guidToConvert == MFAttributesClsid.MF_SA_MINIMUM_OUTPUT_SAMPLE_COUNT_PROGRESSIVE) return "MF_SA_MINIMUM_OUTPUT_SAMPLE_COUNT_PROGRESSIVE";
            if (guidToConvert == MFAttributesClsid.VIDEO_ZOOM_RECT) return "VIDEO_ZOOM_RECT";

            // Event Attributes

            // removed by MS - if(guidToConvert == MFAttributesClsid.MF_EVENT_FORMAT_CHANGE_REQUEST_SOURCE_SAR) return "MF_EVENT_FORMAT_CHANGE_REQUEST_SOURCE_SAR";

            // MF_EVENT_DO_THINNING {321EA6FB-DAD9-46e4-B31D-D2EAE7090E30}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_DO_THINNING) return "MF_EVENT_DO_THINNING";

            // MF_EVENT_OUTPUT_NODE {830f1a8b-c060-46dd-a801-1c95dec9b107}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_OUTPUT_NODE) return "MF_EVENT_OUTPUT_NODE";

            // MF_EVENT_MFT_INPUT_STREAM_ID {F29C2CCA-7AE6-42d2-B284-BF837CC874E2}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_MFT_INPUT_STREAM_ID) return "MF_EVENT_MFT_INPUT_STREAM_ID";

            // MF_EVENT_MFT_CONTEXT {B7CD31F1-899E-4b41-80C9-26A896D32977}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_MFT_CONTEXT) return "MF_EVENT_MFT_CONTEXT";

            // MF_EVENT_PRESENTATION_TIME_OFFSET {5AD914D1-9B45-4a8d-A2C0-81D1E50BFB07}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_PRESENTATION_TIME_OFFSET) return "MF_EVENT_PRESENTATION_TIME_OFFSET";

            // MF_EVENT_SCRUBSAMPLE_TIME {9AC712B3-DCB8-44d5-8D0C-37455A2782E3}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SCRUBSAMPLE_TIME) return "MF_EVENT_SCRUBSAMPLE_TIME";

            // MF_EVENT_SESSIONCAPS {7E5EBCD0-11B8-4abe-AFAD-10F6599A7F42}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SESSIONCAPS) return "MF_EVENT_SESSIONCAPS";

            // MF_EVENT_SESSIONCAPS_DELTA {7E5EBCD1-11B8-4abe-AFAD-10F6599A7F42}
            // Type: UINT32
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SESSIONCAPS_DELTA) return "MF_EVENT_SESSIONCAPS_DELTA";

            // MF_EVENT_SOURCE_ACTUAL_START {a8cc55a9-6b31-419f-845d-ffb351a2434b}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SOURCE_ACTUAL_START) return "MF_EVENT_SOURCE_ACTUAL_START";

            // MF_EVENT_SOURCE_CHARACTERISTICS {47DB8490-8B22-4f52-AFDA-9CE1B2D3CFA8}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SOURCE_CHARACTERISTICS) return "MF_EVENT_SOURCE_CHARACTERISTICS";

            // MF_EVENT_SOURCE_CHARACTERISTICS_OLD {47DB8491-8B22-4f52-AFDA-9CE1B2D3CFA8}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SOURCE_CHARACTERISTICS_OLD) return "MF_EVENT_SOURCE_CHARACTERISTICS_OLD";

            // MF_EVENT_SOURCE_FAKE_START {a8cc55a7-6b31-419f-845d-ffb351a2434b}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SOURCE_FAKE_START) return "MF_EVENT_SOURCE_FAKE_START";

            // MF_EVENT_SOURCE_PROJECTSTART {a8cc55a8-6b31-419f-845d-ffb351a2434b}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SOURCE_PROJECTSTART) return "MF_EVENT_SOURCE_PROJECTSTART";

            // MF_EVENT_SOURCE_TOPOLOGY_CANCELED {DB62F650-9A5E-4704-ACF3-563BC6A73364}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_SOURCE_TOPOLOGY_CANCELED) return "MF_EVENT_SOURCE_TOPOLOGY_CANCELED";

            // MF_EVENT_START_PRESENTATION_TIME {5AD914D0-9B45-4a8d-A2C0-81D1E50BFB07}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_START_PRESENTATION_TIME) return "MF_EVENT_START_PRESENTATION_TIME";

            // MF_EVENT_START_PRESENTATION_TIME_AT_OUTPUT {5AD914D2-9B45-4a8d-A2C0-81D1E50BFB07}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_START_PRESENTATION_TIME_AT_OUTPUT) return "MF_EVENT_START_PRESENTATION_TIME_AT_OUTPUT";

            // MF_EVENT_TOPOLOGY_STATUS {30C5018D-9A53-454b-AD9E-6D5F8FA7C43B}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_TOPOLOGY_STATUS) return "MF_EVENT_TOPOLOGY_STATUS";

            // MF_EVENT_STREAM_METADATA_KEYDATA {CD59A4A1-4A3B-4BBD-8665-72A40FBEA776}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_STREAM_METADATA_KEYDATA) return "MF_EVENT_STREAM_METADATA_KEYDATA";

            // MF_EVENT_STREAM_METADATA_CONTENT_KEYIDS {5063449D-CC29-4FC6-A75A-D247B35AF85C}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_STREAM_METADATA_CONTENT_KEYIDS) return "MF_EVENT_STREAM_METADATA_CONTENT_KEYIDS";

            // MF_EVENT_STREAM_METADATA_SYSTEMID {1EA2EF64-BA16-4A36-8719-FE7560BA32AD}
            if (guidToConvert == MFAttributesClsid.MF_EVENT_STREAM_METADATA_SYSTEMID) return "MF_EVENT_STREAM_METADATA_SYSTEMID";

            if (guidToConvert == MFAttributesClsid.MF_SESSION_APPROX_EVENT_OCCURRENCE_TIME) return "MF_SESSION_APPROX_EVENT_OCCURRENCE_TIME";

            // Media Session Attributes

            if (guidToConvert == MFAttributesClsid.MF_SESSION_CONTENT_PROTECTION_MANAGER) return "MF_SESSION_CONTENT_PROTECTION_MANAGER";
            if (guidToConvert == MFAttributesClsid.MF_SESSION_GLOBAL_TIME) return "MF_SESSION_GLOBAL_TIME";
            if (guidToConvert == MFAttributesClsid.MF_SESSION_QUALITY_MANAGER) return "MF_SESSION_QUALITY_MANAGER";
            if (guidToConvert == MFAttributesClsid.MF_SESSION_REMOTE_SOURCE_MODE) return "MF_SESSION_REMOTE_SOURCE_MODE";
            if (guidToConvert == MFAttributesClsid.MF_SESSION_SERVER_CONTEXT) return "MF_SESSION_SERVER_CONTEXT";
            if (guidToConvert == MFAttributesClsid.MF_SESSION_TOPOLOADER) return "MF_SESSION_TOPOLOADER";

            // Media Type Attributes

            // {48eba18e-f8c9-4687-bf11-0a74c9f96a8f}   MF_MT_MAJOR_TYPE                {GUID}
            if (guidToConvert == MFAttributesClsid.MF_MT_MAJOR_TYPE) return "MF_MT_MAJOR_TYPE";

            // {f7e34c9a-42e8-4714-b74b-cb29d72c35e5}   MF_MT_SUBTYPE                   {GUID}
            if (guidToConvert == MFAttributesClsid.MF_MT_SUBTYPE) return "MF_MT_SUBTYPE";

            // {c9173739-5e56-461c-b713-46fb995cb95f}   MF_MT_ALL_SAMPLES_INDEPENDENT   {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MF_MT_ALL_SAMPLES_INDEPENDENT) return "MF_MT_ALL_SAMPLES_INDEPENDENT";

            // {b8ebefaf-b718-4e04-b0a9-116775e3321b}   MF_MT_FIXED_SIZE_SAMPLES        {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MF_MT_FIXED_SIZE_SAMPLES) return "MF_MT_FIXED_SIZE_SAMPLES";

            // {3afd0cee-18f2-4ba5-a110-8bea502e1f92}   MF_MT_COMPRESSED                {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MF_MT_COMPRESSED) return "MF_MT_COMPRESSED";

            // {dad3ab78-1990-408b-bce2-eba673dacc10}   MF_MT_SAMPLE_SIZE               {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_SAMPLE_SIZE) return "MF_MT_SAMPLE_SIZE";

            // 4d3f7b23-d02f-4e6c-9bee-e4bf2c6c695d     MF_MT_WRAPPED_TYPE              {Blob}
            if (guidToConvert == MFAttributesClsid.MF_MT_WRAPPED_TYPE) return "MF_MT_WRAPPED_TYPE";

            // {37e48bf5-645e-4c5b-89de-ada9e29b696a}   MF_MT_AUDIO_NUM_CHANNELS            {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_NUM_CHANNELS) return "MF_MT_AUDIO_NUM_CHANNELS";

            // {5faeeae7-0290-4c31-9e8a-c534f68d9dba}   MF_MT_AUDIO_SAMPLES_PER_SECOND      {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_SAMPLES_PER_SECOND) return "MF_MT_AUDIO_SAMPLES_PER_SECOND";

            // {fb3b724a-cfb5-4319-aefe-6e42b2406132}   MF_MT_AUDIO_FLOAT_SAMPLES_PER_SECOND {double}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_FLOAT_SAMPLES_PER_SECOND) return "MF_MT_AUDIO_FLOAT_SAMPLES_PER_SECOND";

            // {1aab75c8-cfef-451c-ab95-ac034b8e1731}   MF_MT_AUDIO_AVG_BYTES_PER_SECOND    {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_AVG_BYTES_PER_SECOND) return "MF_MT_AUDIO_AVG_BYTES_PER_SECOND";

            // {322de230-9eeb-43bd-ab7a-ff412251541d}   MF_MT_AUDIO_BLOCK_ALIGNMENT         {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_BLOCK_ALIGNMENT) return "MF_MT_AUDIO_BLOCK_ALIGNMENT";

            // {f2deb57f-40fa-4764-aa33-ed4f2d1ff669}   MF_MT_AUDIO_BITS_PER_SAMPLE         {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_BITS_PER_SAMPLE) return "MF_MT_AUDIO_BITS_PER_SAMPLE";

            // {d9bf8d6a-9530-4b7c-9ddf-ff6fd58bbd06}   MF_MT_AUDIO_VALID_BITS_PER_SAMPLE   {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_VALID_BITS_PER_SAMPLE) return "MF_MT_AUDIO_VALID_BITS_PER_SAMPLE";

            // {aab15aac-e13a-4995-9222-501ea15c6877}   MF_MT_AUDIO_SAMPLES_PER_BLOCK       {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_SAMPLES_PER_BLOCK) return "MF_MT_AUDIO_SAMPLES_PER_BLOCK";

            // {55fb5765-644a-4caf-8479-938983bb1588}`  MF_MT_AUDIO_CHANNEL_MASK            {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_CHANNEL_MASK) return "MF_MT_AUDIO_CHANNEL_MASK";

            // {9d62927c-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_FOLDDOWN_MATRIX         {BLOB, MFFOLDDOWN_MATRIX}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_FOLDDOWN_MATRIX) return "MF_MT_AUDIO_FOLDDOWN_MATRIX";

            // {0x9d62927d-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_PEAKREF         {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_WMADRC_PEAKREF) return "MF_MT_AUDIO_WMADRC_PEAKREF";

            // {0x9d62927e-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_PEAKTARGET        {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_WMADRC_PEAKTARGET) return "MF_MT_AUDIO_WMADRC_PEAKTARGET";

            // {0x9d62927f-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_AVGREF         {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_WMADRC_AVGREF) return "MF_MT_AUDIO_WMADRC_AVGREF";

            // {0x9d629280-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_AVGTARGET      {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_WMADRC_AVGTARGET) return "MF_MT_AUDIO_WMADRC_AVGTARGET";

            // {a901aaba-e037-458a-bdf6-545be2074042}   MF_MT_AUDIO_PREFER_WAVEFORMATEX     {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MF_MT_AUDIO_PREFER_WAVEFORMATEX) return "MF_MT_AUDIO_PREFER_WAVEFORMATEX";

            // {BFBABE79-7434-4d1c-94F0-72A3B9E17188} MF_MT_AAC_PAYLOAD_TYPE       {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AAC_PAYLOAD_TYPE) return "MF_MT_AAC_PAYLOAD_TYPE";

            // {7632F0E6-9538-4d61-ACDA-EA29C8C14456} MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION       {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION) return "MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION";

            // {1652c33d-d6b2-4012-b834-72030849a37d}   MF_MT_FRAME_SIZE                {UINT64 (HI32(Width),LO32(Height))}
            if (guidToConvert == MFAttributesClsid.MF_MT_FRAME_SIZE) return "MF_MT_FRAME_SIZE";

            // {c459a2e8-3d2c-4e44-b132-fee5156c7bb0}   MF_MT_FRAME_RATE                {UINT64 (HI32(Numerator),LO32(Denominator))}
            if (guidToConvert == MFAttributesClsid.MF_MT_FRAME_RATE) return "MF_MT_FRAME_RATE";

            // {c6376a1e-8d0a-4027-be45-6d9a0ad39bb6}   MF_MT_PIXEL_ASPECT_RATIO        {UINT64 (HI32(Numerator),LO32(Denominator))}
            if (guidToConvert == MFAttributesClsid.MF_MT_PIXEL_ASPECT_RATIO) return "MF_MT_PIXEL_ASPECT_RATIO";

            // {8772f323-355a-4cc7-bb78-6d61a048ae82}   MF_MT_DRM_FLAGS                 {UINT32 (anyof MFVideoDRMFlags)}
            if (guidToConvert == MFAttributesClsid.MF_MT_DRM_FLAGS) return "MF_MT_DRM_FLAGS";

            // {24974215-1B7B-41e4-8625-AC469F2DEDAA}   MF_MT_TIMESTAMP_CAN_BE_DTS      {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MF_MT_TIMESTAMP_CAN_BE_DTS) return "MF_MT_TIMESTAMP_CAN_BE_DTS";

            // {A20AF9E8-928A-4B26-AAA9-F05C74CAC47C}   MF_MT_MPEG2_STANDARD            {UINT32 (0 for default MPEG2, 1  to use ATSC standard, 2 to use DVB standard, 3 to use ARIB standard)}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG2_STANDARD) return "MF_MT_MPEG2_STANDARD";

            // {5229BA10-E29D-4F80-A59C-DF4F180207D2}   MF_MT_MPEG2_TIMECODE            {UINT32 (0 for no timecode, 1 to append an 4 byte timecode to the front of each transport packet)}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG2_TIMECODE) return "MF_MT_MPEG2_TIMECODE";

            // {825D55E4-4F12-4197-9EB3-59B6E4710F06}   MF_MT_MPEG2_CONTENT_PACKET      {UINT32 (0 for no content packet, 1 to append a 14 byte Content Packet header according to the ARIB specification to the beginning a transport packet at 200-1000 ms intervals.)}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG2_CONTENT_PACKET) return "MF_MT_MPEG2_CONTENT_PACKET";

            //
            // VIDEO - H264 extra data
            //

            // {F5929986-4C45-4FBB-BB49-6CC534D05B9B}  {UINT32, UVC 1.5 H.264 format descriptor: bMaxCodecConfigDelay}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_MAX_CODEC_CONFIG_DELAY) return "MF_MT_H264_MAX_CODEC_CONFIG_DELAY";

            // {C8BE1937-4D64-4549-8343-A8086C0BFDA5} {UINT32, UVC 1.5 H.264 format descriptor: bmSupportedSliceModes}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_SUPPORTED_SLICE_MODES) return "MF_MT_H264_SUPPORTED_SLICE_MODES";

            // {89A52C01-F282-48D2-B522-22E6AE633199} {UINT32, UVC 1.5 H.264 format descriptor: bmSupportedSyncFrameTypes}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_SUPPORTED_SYNC_FRAME_TYPES) return "MF_MT_H264_SUPPORTED_SYNC_FRAME_TYPES";

            // {E3854272-F715-4757-BA90-1B696C773457} {UINT32, UVC 1.5 H.264 format descriptor: bResolutionScaling}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_RESOLUTION_SCALING) return "MF_MT_H264_RESOLUTION_SCALING";

            // {9EA2D63D-53F0-4A34-B94E-9DE49A078CB3} {UINT32, UVC 1.5 H.264 format descriptor: bSimulcastSupport}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_SIMULCAST_SUPPORT) return "MF_MT_H264_SIMULCAST_SUPPORT";

            // {6A8AC47E-519C-4F18-9BB3-7EEAAEA5594D} {UINT32, UVC 1.5 H.264 format descriptor: bmSupportedRateControlModes}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_SUPPORTED_RATE_CONTROL_MODES) return "MF_MT_H264_SUPPORTED_RATE_CONTROL_MODES";

            // {45256D30-7215-4576-9336-B0F1BCD59BB2}  {Blob of size 20 * sizeof(WORD), UVC 1.5 H.264 format descriptor: wMaxMBperSec*}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_MAX_MB_PER_SEC) return "MF_MT_H264_MAX_MB_PER_SEC";

            // {60B1A998-DC01-40CE-9736-ABA845A2DBDC}         {UINT32, UVC 1.5 H.264 frame descriptor: bmSupportedUsages}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_SUPPORTED_USAGES) return "MF_MT_H264_SUPPORTED_USAGES";

            // {BB3BD508-490A-11E0-99E4-1316DFD72085}         {UINT32, UVC 1.5 H.264 frame descriptor: bmCapabilities}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_CAPABILITIES) return "MF_MT_H264_CAPABILITIES";

            // {F8993ABE-D937-4A8F-BBCA-6966FE9E1152}         {UINT32, UVC 1.5 H.264 frame descriptor: bmSVCCapabilities}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_SVC_CAPABILITIES) return "MF_MT_H264_SVC_CAPABILITIES";

            // {359CE3A5-AF00-49CA-A2F4-2AC94CA82B61}         {UINT32, UVC 1.5 H.264 Probe/Commit Control: bUsage}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_USAGE) return "MF_MT_H264_USAGE";

            //{705177D8-45CB-11E0-AC7D-B91CE0D72085}          {UINT32, UVC 1.5 H.264 Probe/Commit Control: bmRateControlModes}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_RATE_CONTROL_MODES) return "MF_MT_H264_RATE_CONTROL_MODES";

            //{85E299B2-90E3-4FE8-B2F5-C067E0BFE57A}          {UINT64, UVC 1.5 H.264 Probe/Commit Control: bmLayoutPerStream}
            if (guidToConvert == MFAttributesClsid.MF_MT_H264_LAYOUT_PER_STREAM) return "MF_MT_H264_LAYOUT_PER_STREAM";

            // {4d0e73e5-80ea-4354-a9d0-1176ceb028ea}   MF_MT_PAD_CONTROL_FLAGS         {UINT32 (oneof MFVideoPadFlags)}
            if (guidToConvert == MFAttributesClsid.MF_MT_PAD_CONTROL_FLAGS) return "MF_MT_PAD_CONTROL_FLAGS";

            // {68aca3cc-22d0-44e6-85f8-28167197fa38}   MF_MT_SOURCE_CONTENT_HINT       {UINT32 (oneof MFVideoSrcContentHintFlags)}
            if (guidToConvert == MFAttributesClsid.MF_MT_SOURCE_CONTENT_HINT) return "MF_MT_SOURCE_CONTENT_HINT";

            // {65df2370-c773-4c33-aa64-843e068efb0c}   MF_MT_CHROMA_SITING             {UINT32 (anyof MFVideoChromaSubsampling)}
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_CHROMA_SITING) return "MF_MT_VIDEO_CHROMA_SITING";

            // {e2724bb8-e676-4806-b4b2-a8d6efb44ccd}   MF_MT_INTERLACE_MODE            {UINT32 (oneof MFVideoInterlaceMode)}
            if (guidToConvert == MFAttributesClsid.MF_MT_INTERLACE_MODE) return "MF_MT_INTERLACE_MODE";

            // {5fb0fce9-be5c-4935-a811-ec838f8eed93}   MF_MT_TRANSFER_FUNCTION         {UINT32 (oneof MFVideoTransferFunction)}
            if (guidToConvert == MFAttributesClsid.MF_MT_TRANSFER_FUNCTION) return "MF_MT_TRANSFER_FUNCTION";

            // {dbfbe4d7-0740-4ee0-8192-850ab0e21935}   MF_MT_VIDEO_PRIMARIES           {UINT32 (oneof MFVideoPrimaries)}
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_PRIMARIES) return "MF_MT_VIDEO_PRIMARIES";

            // {47537213-8cfb-4722-aa34-fbc9e24d77b8}   MF_MT_CUSTOM_VIDEO_PRIMARIES    {BLOB (MT_CUSTOM_VIDEO_PRIMARIES)}
            if (guidToConvert == MFAttributesClsid.MF_MT_CUSTOM_VIDEO_PRIMARIES) return "MF_MT_CUSTOM_VIDEO_PRIMARIES";

            // {3e23d450-2c75-4d25-a00e-b91670d12327}   MF_MT_YUV_MATRIX                {UINT32 (oneof MFVideoTransferMatrix)}
            if (guidToConvert == MFAttributesClsid.MF_MT_YUV_MATRIX) return "MF_MT_YUV_MATRIX";

            // {53a0529c-890b-4216-8bf9-599367ad6d20}   MF_MT_VIDEO_LIGHTING            {UINT32 (oneof MFVideoLighting)}
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_LIGHTING) return "MF_MT_VIDEO_LIGHTING";

            // {c21b8ee5-b956-4071-8daf-325edf5cab11}   MF_MT_VIDEO_NOMINAL_RANGE       {UINT32 (oneof MFNominalRange)}
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_NOMINAL_RANGE) return "MF_MT_VIDEO_NOMINAL_RANGE";

            // {66758743-7e5f-400d-980a-aa8596c85696}   MF_MT_GEOMETRIC_APERTURE        {BLOB (MFVideoArea)}
            if (guidToConvert == MFAttributesClsid.MF_MT_GEOMETRIC_APERTURE) return "MF_MT_GEOMETRIC_APERTURE";

            // {d7388766-18fe-48c6-a177-ee894867c8c4}   MF_MT_MINIMUM_DISPLAY_APERTURE  {BLOB (MFVideoArea)}
            if (guidToConvert == MFAttributesClsid.MF_MT_MINIMUM_DISPLAY_APERTURE) return "MF_MT_MINIMUM_DISPLAY_APERTURE";

            // {79614dde-9187-48fb-b8c7-4d52689de649}   MF_MT_PAN_SCAN_APERTURE         {BLOB (MFVideoArea)}
            if (guidToConvert == MFAttributesClsid.MF_MT_PAN_SCAN_APERTURE) return "MF_MT_PAN_SCAN_APERTURE";

            // {4b7f6bc3-8b13-40b2-a993-abf630b8204e}   MF_MT_PAN_SCAN_ENABLED          {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MF_MT_PAN_SCAN_ENABLED) return "MF_MT_PAN_SCAN_ENABLED";

            // {20332624-fb0d-4d9e-bd0d-cbf6786c102e}   MF_MT_AVG_BITRATE               {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AVG_BITRATE) return "MF_MT_AVG_BITRATE";

            // {799cabd6-3508-4db4-a3c7-569cd533deb1}   MF_MT_AVG_BIT_ERROR_RATE        {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_AVG_BIT_ERROR_RATE) return "MF_MT_AVG_BIT_ERROR_RATE";

            // {c16eb52b-73a1-476f-8d62-839d6a020652}   MF_MT_MAX_KEYFRAME_SPACING      {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_MAX_KEYFRAME_SPACING) return "MF_MT_MAX_KEYFRAME_SPACING";

            // {644b4e48-1e02-4516-b0eb-c01ca9d49ac6}   MF_MT_DEFAULT_STRIDE            {UINT32 (INT32)} // in bytes
            if (guidToConvert == MFAttributesClsid.MF_MT_DEFAULT_STRIDE) return "MF_MT_DEFAULT_STRIDE";

            // {6d283f42-9846-4410-afd9-654d503b1a54}   MF_MT_PALETTE                   {BLOB (array of MFPaletteEntry - usually 256)}
            if (guidToConvert == MFAttributesClsid.MF_MT_PALETTE) return "MF_MT_PALETTE";

            // {b6bc765f-4c3b-40a4-bd51-2535b66fe09d}   MF_MT_USER_DATA                 {BLOB}
            if (guidToConvert == MFAttributesClsid.MF_MT_USER_DATA) return "MF_MT_USER_DATA";

            // {73d1072d-1870-4174-a063-29ff4ff6c11e}
            if (guidToConvert == MFAttributesClsid.MF_MT_AM_FORMAT_TYPE) return "MF_MT_AM_FORMAT_TYPE";

            // {ad76a80b-2d5c-4e0b-b375-64e520137036}   MF_MT_VIDEO_PROFILE             {UINT32}    This is an alias of  MF_MT_MPEG2_PROFILE
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_PROFILE) return "MF_MT_VIDEO_PROFILE";

            // {96f66574-11c5-4015-8666-bff516436da7}   MF_MT_VIDEO_LEVEL               {UINT32}    This is an alias of  MF_MT_MPEG2_LEVEL
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_LEVEL) return "MF_MT_VIDEO_LEVEL";

            // {91f67885-4333-4280-97cd-bd5a6c03a06e}   MF_MT_MPEG_START_TIME_CODE      {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG_START_TIME_CODE) return "MF_MT_MPEG_START_TIME_CODE";

            // {ad76a80b-2d5c-4e0b-b375-64e520137036}   MF_MT_MPEG2_PROFILE             {UINT32 (oneof AM_MPEG2Profile)}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG2_PROFILE) return "MF_MT_MPEG2_PROFILE";

            // {96f66574-11c5-4015-8666-bff516436da7}   MF_MT_MPEG2_LEVEL               {UINT32 (oneof AM_MPEG2Level)}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG2_LEVEL) return "MF_MT_MPEG2_LEVEL";

            // {31e3991d-f701-4b2f-b426-8ae3bda9e04b}   MF_MT_MPEG2_FLAGS               {UINT32 (anyof AMMPEG2_xxx flags)}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG2_FLAGS) return "MF_MT_MPEG2_FLAGS";

            // {3c036de7-3ad0-4c9e-9216-ee6d6ac21cb3}   MF_MT_MPEG_SEQUENCE_HEADER      {BLOB}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG_SEQUENCE_HEADER) return "MF_MT_MPEG_SEQUENCE_HEADER";

            // {84bd5d88-0fb8-4ac8-be4b-a8848bef98f3}   MF_MT_DV_AAUX_SRC_PACK_0        {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_DV_AAUX_SRC_PACK_0) return "MF_MT_DV_AAUX_SRC_PACK_0";

            // {f731004e-1dd1-4515-aabe-f0c06aa536ac}   MF_MT_DV_AAUX_CTRL_PACK_0       {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_DV_AAUX_CTRL_PACK_0) return "MF_MT_DV_AAUX_CTRL_PACK_0";

            // {720e6544-0225-4003-a651-0196563a958e}   MF_MT_DV_AAUX_SRC_PACK_1        {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_DV_AAUX_SRC_PACK_1) return "MF_MT_DV_AAUX_SRC_PACK_1";

            // {cd1f470d-1f04-4fe0-bfb9-d07ae0386ad8}   MF_MT_DV_AAUX_CTRL_PACK_1       {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_DV_AAUX_CTRL_PACK_1) return "MF_MT_DV_AAUX_CTRL_PACK_1";

            // {41402d9d-7b57-43c6-b129-2cb997f15009}   MF_MT_DV_VAUX_SRC_PACK          {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_DV_VAUX_SRC_PACK) return "MF_MT_DV_VAUX_SRC_PACK";

            // {2f84e1c4-0da1-4788-938e-0dfbfbb34b48}   MF_MT_DV_VAUX_CTRL_PACK         {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_DV_VAUX_CTRL_PACK) return "MF_MT_DV_VAUX_CTRL_PACK";

            // {5315d8a0-87c5-4697-b793-666c67c49b}         MF_MT_VIDEO_3D_FORMAT           {UINT32 (anyof MFVideo3DFormat)}
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_3D_FORMAT) return "MF_MT_VIDEO_3D_FORMAT";

            // {BB077E8A-DCBF-42eb-AF60-418DF98AA495}       MF_MT_VIDEO_3D_NUM_VIEW         {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_3D_NUM_VIEWS) return "MF_MT_VIDEO_3D_NUM_VIEWS";

            // {6D4B7BFF-5629-4404-948C-C634F4CE26D4}       MF_MT_VIDEO_3D_LEFT_IS_BASE     {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_3D_LEFT_IS_BASE) return "MF_MT_VIDEO_3D_LEFT_IS_BASE";

            // {EC298493-0ADA-4ea1-A4FE-CBBD36CE9331}       MF_MT_VIDEO_3D_FIRST_IS_LEFT    {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_3D_FIRST_IS_LEFT) return "MF_MT_VIDEO_3D_FIRST_IS_LEFT";

            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_ROTATION) return "MF_MT_VIDEO_ROTATION";

            // Sample Attributes

            if (guidToConvert == MFAttributesClsid.MFSampleExtension_DecodeTimestamp) return "MFSampleExtension_DecodeTimestamp";

            if (guidToConvert == MFAttributesClsid.MFSampleExtension_VideoEncodeQP) return "MFSampleExtension_VideoEncodeQP";

            if (guidToConvert == MFAttributesClsid.MFSampleExtension_VideoEncodePictureType) return "MFSampleExtension_VideoEncodePictureType";

            if (guidToConvert == MFAttributesClsid.MFSampleExtension_FrameCorruption) return "MFSampleExtension_FrameCorruption";

            // {941ce0a3-6ae3-4dda-9a08-a64298340617}   MFSampleExtension_BottomFieldFirst
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_BottomFieldFirst) return "MFSampleExtension_BottomFieldFirst";

            // MFSampleExtension_CleanPoint {9cdf01d8-a0f0-43ba-b077-eaa06cbd728a}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_CleanPoint) return "MFSampleExtension_CleanPoint";

            // {6852465a-ae1c-4553-8e9b-c3420fcb1637}   MFSampleExtension_DerivedFromTopField
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_DerivedFromTopField) return "MFSampleExtension_DerivedFromTopField";

            // MFSampleExtension_MeanAbsoluteDifference {1cdbde11-08b4-4311-a6dd-0f9f371907aa}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_MeanAbsoluteDifference) return "MFSampleExtension_MeanAbsoluteDifference";

            // MFSampleExtension_LongTermReferenceFrameInfo {9154733f-e1bd-41bf-81d3-fcd918f71332}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_LongTermReferenceFrameInfo) return "MFSampleExtension_LongTermReferenceFrameInfo";

            // MFSampleExtension_ROIRectangle {3414a438-4998-4d2c-be82-be3ca0b24d43}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_ROIRectangle) return "MFSampleExtension_ROIRectangle";

            // MFSampleExtension_PhotoThumbnail {74BBC85C-C8BB-42DC-B586DA17FFD35DCC}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_PhotoThumbnail) return "MFSampleExtension_PhotoThumbnail";

            // MFSampleExtension_PhotoThumbnailMediaType {61AD5420-EBF8-4143-89AF6BF25F672DEF}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_PhotoThumbnailMediaType) return "MFSampleExtension_PhotoThumbnailMediaType";

            // MFSampleExtension_CaptureMetadata
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_CaptureMetadata) return "MFSampleExtension_CaptureMetadata";

            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_PHOTO_FRAME_FLASH) return "MF_CAPTURE_METADATA_PHOTO_FRAME_FLASH";

            // MFSampleExtension_Discontinuity {9cdf01d9-a0f0-43ba-b077-eaa06cbd728a}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Discontinuity) return "MFSampleExtension_Discontinuity";

            // {b1d5830a-deb8-40e3-90fa-389943716461}   MFSampleExtension_Interlaced
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Interlaced) return "MFSampleExtension_Interlaced";

            // {304d257c-7493-4fbd-b149-9228de8d9a99}   MFSampleExtension_RepeatFirstField
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_RepeatFirstField) return "MFSampleExtension_RepeatFirstField";

            // {9d85f816-658b-455a-bde0-9fa7e15ab8f9}   MFSampleExtension_SingleField
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_SingleField) return "MFSampleExtension_SingleField";

            // MFSampleExtension_Token {8294da66-f328-4805-b551-00deb4c57a61}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Token) return "MFSampleExtension_Token";

            // MFSampleExtension_3DVideo                    {F86F97A4-DD54-4e2e-9A5E-55FC2D74A005}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_3DVideo) return "MFSampleExtension_3DVideo";

            // MFSampleExtension_3DVideo_SampleFormat       {08671772-E36F-4cff-97B3-D72E20987A48}
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_3DVideo_SampleFormat) return "MFSampleExtension_3DVideo_SampleFormat";

            if (guidToConvert == MFAttributesClsid.MFSampleExtension_MaxDecodeFrameSize) return "MFSampleExtension_MaxDecodeFrameSize";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_AccumulatedNonRefPicPercent) return "MFSampleExtension_AccumulatedNonRefPicPercent";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_SubSample_Mapping) return "MFSampleExtension_Encryption_SubSample_Mapping";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_ClearSliceHeaderData) return "MFSampleExtension_Encryption_ClearSliceHeaderData";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_HardwareProtection_KeyInfoID) return "MFSampleExtension_Encryption_HardwareProtection_KeyInfoID";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_HardwareProtection_KeyInfo) return "MFSampleExtension_Encryption_HardwareProtection_KeyInfo";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_HardwareProtection_VideoDecryptorContext) return "MFSampleExtension_Encryption_HardwareProtection_VideoDecryptorContext";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_Opaque_Data) return "MFSampleExtension_Encryption_Opaque_Data";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_NALULengthInfo) return "MFSampleExtension_NALULengthInfo";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_NALUTypes) return "MFSampleExtension_Encryption_NALUTypes";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_SPSPPSData) return "MFSampleExtension_Encryption_SPSPPSData";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_SEIData) return "MFSampleExtension_Encryption_SEIData";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_HardwareProtection) return "MFSampleExtension_Encryption_HardwareProtection";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_ClosedCaption_CEA708) return "MFSampleExtension_ClosedCaption_CEA708";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_DirtyRects) return "MFSampleExtension_DirtyRects";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_MoveRegions) return "MFSampleExtension_MoveRegions";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_HDCP_FrameCounter) return "MFSampleExtension_HDCP_FrameCounter";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_MDLCacheCookie) return "MFSampleExtension_MDLCacheCookie";

            // Sample Grabber Sink Attributes
            if (guidToConvert == MFAttributesClsid.MF_SAMPLEGRABBERSINK_SAMPLE_TIME_OFFSET) return "MF_SAMPLEGRABBERSINK_SAMPLE_TIME_OFFSET";

            // Stream descriptor Attributes

            if (guidToConvert == MFAttributesClsid.MF_SD_LANGUAGE) return "MF_SD_LANGUAGE";
            if (guidToConvert == MFAttributesClsid.MF_SD_PROTECTED) return "MF_SD_PROTECTED";
            if (guidToConvert == MFAttributesClsid.MF_SD_SAMI_LANGUAGE) return "MF_SD_SAMI_LANGUAGE";

            // Topology Attributes
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_NO_MARKIN_MARKOUT) return "MF_TOPOLOGY_NO_MARKIN_MARKOUT";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_PROJECTSTART) return "MF_TOPOLOGY_PROJECTSTART";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_PROJECTSTOP) return "MF_TOPOLOGY_PROJECTSTOP";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_RESOLUTION_STATUS) return "MF_TOPOLOGY_RESOLUTION_STATUS";

            // Topology Node Attributes
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_CONNECT_METHOD) return "MF_TOPONODE_CONNECT_METHOD";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_D3DAWARE) return "MF_TOPONODE_D3DAWARE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_DECODER) return "MF_TOPONODE_DECODER";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_DECRYPTOR) return "MF_TOPONODE_DECRYPTOR";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_DISABLE_PREROLL) return "MF_TOPONODE_DISABLE_PREROLL";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_DISCARDABLE) return "MF_TOPONODE_DISCARDABLE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_DRAIN) return "MF_TOPONODE_DRAIN";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_ERROR_MAJORTYPE) return "MF_TOPONODE_ERROR_MAJORTYPE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_ERROR_SUBTYPE) return "MF_TOPONODE_ERROR_SUBTYPE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_ERRORCODE) return "MF_TOPONODE_ERRORCODE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_FLUSH) return "MF_TOPONODE_FLUSH";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_LOCKED) return "MF_TOPONODE_LOCKED";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_MARKIN_HERE) return "MF_TOPONODE_MARKIN_HERE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_MARKOUT_HERE) return "MF_TOPONODE_MARKOUT_HERE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_MEDIASTART) return "MF_TOPONODE_MEDIASTART";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_MEDIASTOP) return "MF_TOPONODE_MEDIASTOP";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_NOSHUTDOWN_ON_REMOVE) return "MF_TOPONODE_NOSHUTDOWN_ON_REMOVE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_PRESENTATION_DESCRIPTOR) return "MF_TOPONODE_PRESENTATION_DESCRIPTOR";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_PRIMARYOUTPUT) return "MF_TOPONODE_PRIMARYOUTPUT";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_RATELESS) return "MF_TOPONODE_RATELESS";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_SEQUENCE_ELEMENTID) return "MF_TOPONODE_SEQUENCE_ELEMENTID";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_SOURCE) return "MF_TOPONODE_SOURCE";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_STREAM_DESCRIPTOR) return "MF_TOPONODE_STREAM_DESCRIPTOR";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_STREAMID) return "MF_TOPONODE_STREAMID";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_TRANSFORM_OBJECTID) return "MF_TOPONODE_TRANSFORM_OBJECTID";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_WORKQUEUE_ID) return "MF_TOPONODE_WORKQUEUE_ID";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_WORKQUEUE_MMCSS_CLASS) return "MF_TOPONODE_WORKQUEUE_MMCSS_CLASS";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_WORKQUEUE_MMCSS_TASKID) return "MF_TOPONODE_WORKQUEUE_MMCSS_TASKID";

            // Transform Attributes
            if (guidToConvert == MFAttributesClsid.MF_ACTIVATE_MFT_LOCKED) return "MF_ACTIVATE_MFT_LOCKED";
            if (guidToConvert == MFAttributesClsid.MF_SA_D3D_AWARE) return "MF_SA_D3D_AWARE";
            if (guidToConvert == MFAttributesClsid.MFT_SUPPORT_3DVIDEO) return "MFT_SUPPORT_3DVIDEO";

            // {53476A11-3F13-49fb-AC42-EE2733C96741} MFT_SUPPORT_DYNAMIC_FORMAT_CHANGE {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MFT_SUPPORT_DYNAMIC_FORMAT_CHANGE) return "MFT_SUPPORT_DYNAMIC_FORMAT_CHANGE";

            if (guidToConvert == MFAttributesClsid.MFT_REMUX_MARK_I_PICTURE_AS_CLEAN_POINT) return "MFT_REMUX_MARK_I_PICTURE_AS_CLEAN_POINT";
            if (guidToConvert == MFAttributesClsid.MFT_ENCODER_SUPPORTS_CONFIG_EVENT) return "MFT_ENCODER_SUPPORTS_CONFIG_EVENT";

            // Presentation Descriptor Attributes

            if (guidToConvert == MFAttributesClsid.MF_PD_APP_CONTEXT) return "MF_PD_APP_CONTEXT";
            if (guidToConvert == MFAttributesClsid.MF_PD_DURATION) return "MF_PD_DURATION";
            if (guidToConvert == MFAttributesClsid.MF_PD_LAST_MODIFIED_TIME) return "MF_PD_LAST_MODIFIED_TIME";
            if (guidToConvert == MFAttributesClsid.MF_PD_MIME_TYPE) return "MF_PD_MIME_TYPE";
            if (guidToConvert == MFAttributesClsid.MF_PD_PMPHOST_CONTEXT) return "MF_PD_PMPHOST_CONTEXT";
            if (guidToConvert == MFAttributesClsid.MF_PD_SAMI_STYLELIST) return "MF_PD_SAMI_STYLELIST";
            if (guidToConvert == MFAttributesClsid.MF_PD_TOTAL_FILE_SIZE) return "MF_PD_TOTAL_FILE_SIZE";
            if (guidToConvert == MFAttributesClsid.MF_PD_AUDIO_ENCODING_BITRATE) return "MF_PD_AUDIO_ENCODING_BITRATE";
            if (guidToConvert == MFAttributesClsid.MF_PD_VIDEO_ENCODING_BITRATE) return "MF_PD_VIDEO_ENCODING_BITRATE";

            // wmcontainer.h Attributes
            if (guidToConvert == MFAttributesClsid.MFASFSampleExtension_SampleDuration) return "MFASFSampleExtension_SampleDuration";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_SampleKeyID) return "MFSampleExtension_SampleKeyID";

            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_FILE_ID) return "MF_PD_ASF_FILEPROPERTIES_FILE_ID";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_CREATION_TIME) return "MF_PD_ASF_FILEPROPERTIES_CREATION_TIME";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_PACKETS) return "MF_PD_ASF_FILEPROPERTIES_PACKETS";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_PLAY_DURATION) return "MF_PD_ASF_FILEPROPERTIES_PLAY_DURATION";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_SEND_DURATION) return "MF_PD_ASF_FILEPROPERTIES_SEND_DURATION";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_PREROLL) return "MF_PD_ASF_FILEPROPERTIES_PREROLL";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_FLAGS) return "MF_PD_ASF_FILEPROPERTIES_FLAGS";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_MIN_PACKET_SIZE) return "MF_PD_ASF_FILEPROPERTIES_MIN_PACKET_SIZE";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_MAX_PACKET_SIZE) return "MF_PD_ASF_FILEPROPERTIES_MAX_PACKET_SIZE";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_FILEPROPERTIES_MAX_BITRATE) return "MF_PD_ASF_FILEPROPERTIES_MAX_BITRATE";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_CONTENTENCRYPTION_TYPE) return "MF_PD_ASF_CONTENTENCRYPTION_TYPE";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_CONTENTENCRYPTION_KEYID) return "MF_PD_ASF_CONTENTENCRYPTION_KEYID";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_CONTENTENCRYPTION_SECRET_DATA) return "MF_PD_ASF_CONTENTENCRYPTION_SECRET_DATA";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_CONTENTENCRYPTION_LICENSE_URL) return "MF_PD_ASF_CONTENTENCRYPTION_LICENSE_URL";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_CONTENTENCRYPTIONEX_ENCRYPTION_DATA) return "MF_PD_ASF_CONTENTENCRYPTIONEX_ENCRYPTION_DATA";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_LANGLIST) return "MF_PD_ASF_LANGLIST";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_LANGLIST_LEGACYORDER) return "MF_PD_ASF_LANGLIST_LEGACYORDER";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_MARKER) return "MF_PD_ASF_MARKER";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_SCRIPT) return "MF_PD_ASF_SCRIPT";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_CODECLIST) return "MF_PD_ASF_CODECLIST";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_METADATA_IS_VBR) return "MF_PD_ASF_METADATA_IS_VBR";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_METADATA_V8_VBRPEAK) return "MF_PD_ASF_METADATA_V8_VBRPEAK";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_METADATA_V8_BUFFERAVERAGE) return "MF_PD_ASF_METADATA_V8_BUFFERAVERAGE";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_METADATA_LEAKY_BUCKET_PAIRS) return "MF_PD_ASF_METADATA_LEAKY_BUCKET_PAIRS";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_DATA_START_OFFSET) return "MF_PD_ASF_DATA_START_OFFSET";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_DATA_LENGTH) return "MF_PD_ASF_DATA_LENGTH";
            if (guidToConvert == MFAttributesClsid.MF_SD_ASF_EXTSTRMPROP_LANGUAGE_ID_INDEX) return "MF_SD_ASF_EXTSTRMPROP_LANGUAGE_ID_INDEX";
            if (guidToConvert == MFAttributesClsid.MF_SD_ASF_EXTSTRMPROP_AVG_DATA_BITRATE) return "MF_SD_ASF_EXTSTRMPROP_AVG_DATA_BITRATE";
            if (guidToConvert == MFAttributesClsid.MF_SD_ASF_EXTSTRMPROP_AVG_BUFFERSIZE) return "MF_SD_ASF_EXTSTRMPROP_AVG_BUFFERSIZE";
            if (guidToConvert == MFAttributesClsid.MF_SD_ASF_EXTSTRMPROP_MAX_DATA_BITRATE) return "MF_SD_ASF_EXTSTRMPROP_MAX_DATA_BITRATE";
            if (guidToConvert == MFAttributesClsid.MF_SD_ASF_EXTSTRMPROP_MAX_BUFFERSIZE) return "MF_SD_ASF_EXTSTRMPROP_MAX_BUFFERSIZE";
            if (guidToConvert == MFAttributesClsid.MF_SD_ASF_STREAMBITRATES_BITRATE) return "MF_SD_ASF_STREAMBITRATES_BITRATE";
            if (guidToConvert == MFAttributesClsid.MF_SD_ASF_METADATA_DEVICE_CONFORMANCE_TEMPLATE) return "MF_SD_ASF_METADATA_DEVICE_CONFORMANCE_TEMPLATE";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_INFO_HAS_AUDIO) return "MF_PD_ASF_INFO_HAS_AUDIO";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_INFO_HAS_VIDEO) return "MF_PD_ASF_INFO_HAS_VIDEO";
            if (guidToConvert == MFAttributesClsid.MF_PD_ASF_INFO_HAS_NON_AUDIO_VIDEO) return "MF_PD_ASF_INFO_HAS_NON_AUDIO_VIDEO";
            if (guidToConvert == MFAttributesClsid.MF_ASFSTREAMCONFIG_LEAKYBUCKET1) return "MF_ASFSTREAMCONFIG_LEAKYBUCKET1";
            if (guidToConvert == MFAttributesClsid.MF_ASFSTREAMCONFIG_LEAKYBUCKET2) return "MF_ASFSTREAMCONFIG_LEAKYBUCKET2";

            // Arbitrary

            // {9E6BD6F5-0109-4f95-84AC-9309153A19FC}   MF_MT_ARBITRARY_HEADER          {MT_ARBITRARY_HEADER}
            if (guidToConvert == MFAttributesClsid.MF_MT_ARBITRARY_HEADER) return "MF_MT_ARBITRARY_HEADER";

            // {5A75B249-0D7D-49a1-A1C3-E0D87F0CADE5}   MF_MT_ARBITRARY_FORMAT          {Blob}
            if (guidToConvert == MFAttributesClsid.MF_MT_ARBITRARY_FORMAT) return "MF_MT_ARBITRARY_FORMAT";

            // Image

            // {ED062CF4-E34E-4922-BE99-934032133D7C}   MF_MT_IMAGE_LOSS_TOLERANT       {UINT32 (BOOL)}
            if (guidToConvert == MFAttributesClsid.MF_MT_IMAGE_LOSS_TOLERANT) return "MF_MT_IMAGE_LOSS_TOLERANT";

            // MPEG-4 Media Type Attributes

            // {261E9D83-9529-4B8F-A111-8B9C950A81A9}   MF_MT_MPEG4_SAMPLE_DESCRIPTION   {BLOB}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG4_SAMPLE_DESCRIPTION) return "MF_MT_MPEG4_SAMPLE_DESCRIPTION";

            // {9aa7e155-b64a-4c1d-a500-455d600b6560}   MF_MT_MPEG4_CURRENT_SAMPLE_ENTRY {UINT32}
            if (guidToConvert == MFAttributesClsid.MF_MT_MPEG4_CURRENT_SAMPLE_ENTRY) return "MF_MT_MPEG4_CURRENT_SAMPLE_ENTRY";

            // Save original format information for AVI and WAV files

            // {d7be3fe0-2bc7-492d-b843-61a1919b70c3}   MF_MT_ORIGINAL_4CC               (UINT32)
            if (guidToConvert == MFAttributesClsid.MF_MT_ORIGINAL_4CC) return "MF_MT_ORIGINAL_4CC";

            // {8cbbc843-9fd9-49c2-882f-a72586c408ad}   MF_MT_ORIGINAL_WAVE_FORMAT_TAG   (UINT32)
            if (guidToConvert == MFAttributesClsid.MF_MT_ORIGINAL_WAVE_FORMAT_TAG) return "MF_MT_ORIGINAL_WAVE_FORMAT_TAG";

            // Video Capture Media Type Attributes

            // {D2E7558C-DC1F-403f-9A72-D28BB1EB3B5E}   MF_MT_FRAME_RATE_RANGE_MIN      {UINT64 (HI32(Numerator),LO32(Denominator))}
            if (guidToConvert == MFAttributesClsid.MF_MT_FRAME_RATE_RANGE_MIN) return "MF_MT_FRAME_RATE_RANGE_MIN";

            // {E3371D41-B4CF-4a05-BD4E-20B88BB2C4D6}   MF_MT_FRAME_RATE_RANGE_MAX      {UINT64 (HI32(Numerator),LO32(Denominator))}
            if (guidToConvert == MFAttributesClsid.MF_MT_FRAME_RATE_RANGE_MAX) return "MF_MT_FRAME_RATE_RANGE_MAX";

            if (guidToConvert == MFAttributesClsid.MF_LOW_LATENCY) return "MF_LOW_LATENCY";

            // {E3F2E203-D445-4B8C-9211-AE390D3BA017}  {UINT32} Maximum macroblocks per second that can be handled by MFT
            if (guidToConvert == MFAttributesClsid.MF_VIDEO_MAX_MB_PER_SEC) return "MF_VIDEO_MAX_MB_PER_SEC";
            if (guidToConvert == MFAttributesClsid.MF_VIDEO_PROCESSOR_ALGORITHM) return "MF_VIDEO_PROCESSOR_ALGORITHM";

            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_DXVA_MODE) return "MF_TOPOLOGY_DXVA_MODE";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_ENABLE_XVP_FOR_PLAYBACK) return "MF_TOPOLOGY_ENABLE_XVP_FOR_PLAYBACK";

            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_STATIC_PLAYBACK_OPTIMIZATIONS) return "MF_TOPOLOGY_STATIC_PLAYBACK_OPTIMIZATIONS";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_PLAYBACK_MAX_DIMS) return "MF_TOPOLOGY_PLAYBACK_MAX_DIMS";

            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_HARDWARE_MODE) return "MF_TOPOLOGY_HARDWARE_MODE";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_PLAYBACK_FRAMERATE) return "MF_TOPOLOGY_PLAYBACK_FRAMERATE";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_DYNAMIC_CHANGE_NOT_ALLOWED) return "MF_TOPOLOGY_DYNAMIC_CHANGE_NOT_ALLOWED";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_ENUMERATE_SOURCE_TYPES) return "MF_TOPOLOGY_ENUMERATE_SOURCE_TYPES";
            if (guidToConvert == MFAttributesClsid.MF_TOPOLOGY_START_TIME_ON_PRESENTATION_SWITCH) return "MF_TOPOLOGY_START_TIME_ON_PRESENTATION_SWITCH";

            if (guidToConvert == MFAttributesClsid.MF_PD_PLAYBACK_ELEMENT_ID) return "MF_PD_PLAYBACK_ELEMENT_ID";
            if (guidToConvert == MFAttributesClsid.MF_PD_PREFERRED_LANGUAGE) return "MF_PD_PREFERRED_LANGUAGE";
            if (guidToConvert == MFAttributesClsid.MF_PD_PLAYBACK_BOUNDARY_TIME) return "MF_PD_PLAYBACK_BOUNDARY_TIME";
            if (guidToConvert == MFAttributesClsid.MF_PD_AUDIO_ISVARIABLEBITRATE) return "MF_PD_AUDIO_ISVARIABLEBITRATE";

            if (guidToConvert == MFAttributesClsid.MF_SD_STREAM_NAME) return "MF_SD_STREAM_NAME";
            if (guidToConvert == MFAttributesClsid.MF_SD_MUTUALLY_EXCLUSIVE) return "MF_SD_MUTUALLY_EXCLUSIVE";

            if (guidToConvert == MFAttributesClsid.MF_SAMPLEGRABBERSINK_IGNORE_CLOCK) return "MF_SAMPLEGRABBERSINK_IGNORE_CLOCK";
            if (guidToConvert == MFAttributesClsid.MF_BYTESTREAMHANDLER_ACCEPTS_SHARE_WRITE) return "MF_BYTESTREAMHANDLER_ACCEPTS_SHARE_WRITE";

            if (guidToConvert == MFAttributesClsid.MF_TRANSCODE_CONTAINERTYPE) return "MF_TRANSCODE_CONTAINERTYPE";
            if (guidToConvert == MFAttributesClsid.MF_TRANSCODE_SKIP_METADATA_TRANSFER) return "MF_TRANSCODE_SKIP_METADATA_TRANSFER";
            if (guidToConvert == MFAttributesClsid.MF_TRANSCODE_TOPOLOGYMODE) return "MF_TRANSCODE_TOPOLOGYMODE";
            if (guidToConvert == MFAttributesClsid.MF_TRANSCODE_ADJUST_PROFILE) return "MF_TRANSCODE_ADJUST_PROFILE";

            if (guidToConvert == MFAttributesClsid.MF_TRANSCODE_ENCODINGPROFILE) return "MF_TRANSCODE_ENCODINGPROFILE";
            if (guidToConvert == MFAttributesClsid.MF_TRANSCODE_QUALITYVSSPEED) return "MF_TRANSCODE_QUALITYVSSPEED";
            if (guidToConvert == MFAttributesClsid.MF_TRANSCODE_DONOT_INSERT_ENCODER) return "MF_TRANSCODE_DONOT_INSERT_ENCODER";

            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE) return "MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_HW_SOURCE) return "MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_HW_SOURCE";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME) return "MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_MEDIA_TYPE) return "MF_DEVSOURCE_ATTRIBUTE_MEDIA_TYPE";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_CATEGORY) return "MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_CATEGORY";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK) return "MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_MAX_BUFFERS) return "MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_MAX_BUFFERS";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID) return "MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ROLE) return "MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ROLE";

            if (guidToConvert == MFAttributesClsid.MFSampleExtension_DeviceTimestamp) return "MFSampleExtension_DeviceTimestamp";

            if (guidToConvert == MFAttributesClsid.MF_TRANSFORM_ASYNC) return "MF_TRANSFORM_ASYNC";
            if (guidToConvert == MFAttributesClsid.MF_TRANSFORM_ASYNC_UNLOCK) return "MF_TRANSFORM_ASYNC_UNLOCK";
            if (guidToConvert == MFAttributesClsid.MF_TRANSFORM_FLAGS_Attribute) return "MF_TRANSFORM_FLAGS_Attribute";
            if (guidToConvert == MFAttributesClsid.MF_TRANSFORM_CATEGORY_Attribute) return "MF_TRANSFORM_CATEGORY_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_TRANSFORM_CLSID_Attribute) return "MFT_TRANSFORM_CLSID_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_INPUT_TYPES_Attributes) return "MFT_INPUT_TYPES_Attributes";
            if (guidToConvert == MFAttributesClsid.MFT_OUTPUT_TYPES_Attributes) return "MFT_OUTPUT_TYPES_Attributes";
            if (guidToConvert == MFAttributesClsid.MFT_ENUM_HARDWARE_URL_Attribute) return "MFT_ENUM_HARDWARE_URL_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_FRIENDLY_NAME_Attribute) return "MFT_FRIENDLY_NAME_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_CONNECTED_STREAM_ATTRIBUTE) return "MFT_CONNECTED_STREAM_ATTRIBUTE";
            if (guidToConvert == MFAttributesClsid.MFT_CONNECTED_TO_HW_STREAM) return "MFT_CONNECTED_TO_HW_STREAM";
            if (guidToConvert == MFAttributesClsid.MFT_PREFERRED_OUTPUTTYPE_Attribute) return "MFT_PREFERRED_OUTPUTTYPE_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_PROCESS_LOCAL_Attribute) return "MFT_PROCESS_LOCAL_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_PREFERRED_ENCODER_PROFILE) return "MFT_PREFERRED_ENCODER_PROFILE";
            if (guidToConvert == MFAttributesClsid.MFT_HW_TIMESTAMP_WITH_QPC_Attribute) return "MFT_HW_TIMESTAMP_WITH_QPC_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_FIELDOFUSE_UNLOCK_Attribute) return "MFT_FIELDOFUSE_UNLOCK_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_CODEC_MERIT_Attribute) return "MFT_CODEC_MERIT_Attribute";
            if (guidToConvert == MFAttributesClsid.MFT_ENUM_TRANSCODE_ONLY_ATTRIBUTE) return "MFT_ENUM_TRANSCODE_ONLY_ATTRIBUTE";

            if (guidToConvert == MFAttributesClsid.MF_MP2DLNA_USE_MMCSS) return "MF_MP2DLNA_USE_MMCSS";
            if (guidToConvert == MFAttributesClsid.MF_MP2DLNA_VIDEO_BIT_RATE) return "MF_MP2DLNA_VIDEO_BIT_RATE";
            if (guidToConvert == MFAttributesClsid.MF_MP2DLNA_AUDIO_BIT_RATE) return "MF_MP2DLNA_AUDIO_BIT_RATE";
            if (guidToConvert == MFAttributesClsid.MF_MP2DLNA_ENCODE_QUALITY) return "MF_MP2DLNA_ENCODE_QUALITY";
            if (guidToConvert == MFAttributesClsid.MF_MP2DLNA_STATISTICS) return "MF_MP2DLNA_STATISTICS";

            if (guidToConvert == MFAttributesClsid.MF_SINK_WRITER_ASYNC_CALLBACK) return "MF_SINK_WRITER_ASYNC_CALLBACK";
            if (guidToConvert == MFAttributesClsid.MF_SINK_WRITER_DISABLE_THROTTLING) return "MF_SINK_WRITER_DISABLE_THROTTLING";
            if (guidToConvert == MFAttributesClsid.MF_SINK_WRITER_D3D_MANAGER) return "MF_SINK_WRITER_D3D_MANAGER";
            if (guidToConvert == MFAttributesClsid.MF_SINK_WRITER_ENCODER_CONFIG) return "MF_SINK_WRITER_ENCODER_CONFIG";
            if (guidToConvert == MFAttributesClsid.MF_READWRITE_DISABLE_CONVERTERS) return "MF_READWRITE_DISABLE_CONVERTERS";
            if (guidToConvert == MFAttributesClsid.MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS) return "MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS";
            if (guidToConvert == MFAttributesClsid.MF_READWRITE_MMCSS_CLASS) return "MF_READWRITE_MMCSS_CLASS";
            if (guidToConvert == MFAttributesClsid.MF_READWRITE_MMCSS_PRIORITY) return "MF_READWRITE_MMCSS_PRIORITY";
            if (guidToConvert == MFAttributesClsid.MF_READWRITE_MMCSS_CLASS_AUDIO) return "MF_READWRITE_MMCSS_CLASS_AUDIO";
            if (guidToConvert == MFAttributesClsid.MF_READWRITE_MMCSS_PRIORITY_AUDIO) return "MF_READWRITE_MMCSS_PRIORITY_AUDIO";
            if (guidToConvert == MFAttributesClsid.MF_READWRITE_D3D_OPTIONAL) return "MF_READWRITE_D3D_OPTIONAL";

            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_ASYNC_CALLBACK) return "MF_SOURCE_READER_ASYNC_CALLBACK";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_D3D_MANAGER) return "MF_SOURCE_READER_D3D_MANAGER";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_DISABLE_DXVA) return "MF_SOURCE_READER_DISABLE_DXVA";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_MEDIASOURCE_CONFIG) return "MF_SOURCE_READER_MEDIASOURCE_CONFIG";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_MEDIASOURCE_CHARACTERISTICS) return "MF_SOURCE_READER_MEDIASOURCE_CHARACTERISTICS";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_ENABLE_VIDEO_PROCESSING) return "MF_SOURCE_READER_ENABLE_VIDEO_PROCESSING";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_DISCONNECT_MEDIASOURCE_ON_SHUTDOWN) return "MF_SOURCE_READER_DISCONNECT_MEDIASOURCE_ON_SHUTDOWN";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_ENABLE_ADVANCED_VIDEO_PROCESSING) return "MF_SOURCE_READER_ENABLE_ADVANCED_VIDEO_PROCESSING";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_DISABLE_CAMERA_PLUGINS) return "MF_SOURCE_READER_DISABLE_CAMERA_PLUGINS";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_ENABLE_TRANSCODE_ONLY_TRANSFORMS) return "MF_SOURCE_READER_ENABLE_TRANSCODE_ONLY_TRANSFORMS";


            // Misc W8 attributes
            if (guidToConvert == MFAttributesClsid.MF_ENABLE_3DVIDEO_OUTPUT) return "MF_ENABLE_3DVIDEO_OUTPUT";
            if (guidToConvert == MFAttributesClsid.MF_SA_D3D11_BINDFLAGS) return "MF_SA_D3D11_BINDFLAGS";
            if (guidToConvert == MFAttributesClsid.MF_SA_D3D11_USAGE) return "MF_SA_D3D11_USAGE";
            if (guidToConvert == MFAttributesClsid.MF_SA_D3D11_AWARE) return "MF_SA_D3D11_AWARE";
            if (guidToConvert == MFAttributesClsid.MF_SA_D3D11_SHARED) return "MF_SA_D3D11_SHARED";
            if (guidToConvert == MFAttributesClsid.MF_SA_D3D11_SHARED_WITHOUT_MUTEX) return "MF_SA_D3D11_SHARED_WITHOUT_MUTEX";
            if (guidToConvert == MFAttributesClsid.MF_SA_BUFFERS_PER_SAMPLE) return "MF_SA_BUFFERS_PER_SAMPLE";
            if (guidToConvert == MFAttributesClsid.MFT_DECODER_EXPOSE_OUTPUT_TYPES_IN_NATIVE_ORDER) return "MFT_DECODER_EXPOSE_OUTPUT_TYPES_IN_NATIVE_ORDER";
            if (guidToConvert == MFAttributesClsid.MFT_DECODER_FINAL_VIDEO_RESOLUTION_HINT) return "MFT_DECODER_FINAL_VIDEO_RESOLUTION_HINT";
            if (guidToConvert == MFAttributesClsid.MFT_ENUM_HARDWARE_VENDOR_ID_Attribute) return "MFT_ENUM_HARDWARE_VENDOR_ID_Attribute";
            if (guidToConvert == MFAttributesClsid.MF_WVC1_PROG_SINGLE_SLICE_CONTENT) return "MF_WVC1_PROG_SINGLE_SLICE_CONTENT";
            if (guidToConvert == MFAttributesClsid.MF_PROGRESSIVE_CODING_CONTENT) return "MF_PROGRESSIVE_CODING_CONTENT";
            if (guidToConvert == MFAttributesClsid.MF_NALU_LENGTH_SET) return "MF_NALU_LENGTH_SET";
            if (guidToConvert == MFAttributesClsid.MF_NALU_LENGTH_INFORMATION) return "MF_NALU_LENGTH_INFORMATION";
            if (guidToConvert == MFAttributesClsid.MF_USER_DATA_PAYLOAD) return "MF_USER_DATA_PAYLOAD";
            if (guidToConvert == MFAttributesClsid.MF_MPEG4SINK_SPSPPS_PASSTHROUGH) return "MF_MPEG4SINK_SPSPPS_PASSTHROUGH";
            if (guidToConvert == MFAttributesClsid.MF_MPEG4SINK_MOOV_BEFORE_MDAT) return "MF_MPEG4SINK_MOOV_BEFORE_MDAT";
            if (guidToConvert == MFAttributesClsid.MF_STREAM_SINK_SUPPORTS_HW_CONNECTION) return "MF_STREAM_SINK_SUPPORTS_HW_CONNECTION";
            if (guidToConvert == MFAttributesClsid.MF_STREAM_SINK_SUPPORTS_ROTATION) return "MF_STREAM_SINK_SUPPORTS_ROTATION";
            if (guidToConvert == MFAttributesClsid.MF_DISABLE_LOCALLY_REGISTERED_PLUGINS) return "MF_DISABLE_LOCALLY_REGISTERED_PLUGINS";
            if (guidToConvert == MFAttributesClsid.MF_LOCAL_PLUGIN_CONTROL_POLICY) return "MF_LOCAL_PLUGIN_CONTROL_POLICY";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_WORKQUEUE_MMCSS_PRIORITY) return "MF_TOPONODE_WORKQUEUE_MMCSS_PRIORITY";
            if (guidToConvert == MFAttributesClsid.MF_TOPONODE_WORKQUEUE_ITEM_PRIORITY) return "MF_TOPONODE_WORKQUEUE_ITEM_PRIORITY";
            if (guidToConvert == MFAttributesClsid.MF_AUDIO_RENDERER_ATTRIBUTE_STREAM_CATEGORY) return "MF_AUDIO_RENDERER_ATTRIBUTE_STREAM_CATEGORY";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_PROTECTED_SURFACE) return "MFPROTECTION_PROTECTED_SURFACE";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_DISABLE_SCREEN_SCRAPE) return "MFPROTECTION_DISABLE_SCREEN_SCRAPE";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_VIDEO_FRAMES) return "MFPROTECTION_VIDEO_FRAMES";
            if (guidToConvert == MFAttributesClsid.MFPROTECTIONATTRIBUTE_BEST_EFFORT) return "MFPROTECTIONATTRIBUTE_BEST_EFFORT";
            if (guidToConvert == MFAttributesClsid.MFPROTECTIONATTRIBUTE_FAIL_OVER) return "MFPROTECTIONATTRIBUTE_FAIL_OVER";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_GRAPHICS_TRANSFER_AES_ENCRYPTION) return "MFPROTECTION_GRAPHICS_TRANSFER_AES_ENCRYPTION";
            if (guidToConvert == MFAttributesClsid.MF_XVP_DISABLE_FRC) return "MF_XVP_DISABLE_FRC";
            if (guidToConvert == MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_SYMBOLIC_LINK) return "MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_SYMBOLIC_LINK";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_IMAGE_STREAM) return "MF_DEVICESTREAM_IMAGE_STREAM";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_INDEPENDENT_IMAGE_STREAM) return "MF_DEVICESTREAM_INDEPENDENT_IMAGE_STREAM";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_STREAM_ID) return "MF_DEVICESTREAM_STREAM_ID";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_STREAM_CATEGORY) return "MF_DEVICESTREAM_STREAM_CATEGORY";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_TRANSFORM_STREAM_ID) return "MF_DEVICESTREAM_TRANSFORM_STREAM_ID";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_EXTENSION_PLUGIN_CLSID) return "MF_DEVICESTREAM_EXTENSION_PLUGIN_CLSID";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_EXTENSION_PLUGIN_CONNECTION_POINT) return "MF_DEVICESTREAM_EXTENSION_PLUGIN_CONNECTION_POINT";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_TAKEPHOTO_TRIGGER) return "MF_DEVICESTREAM_TAKEPHOTO_TRIGGER";
            if (guidToConvert == MFAttributesClsid.MF_DEVICESTREAM_MAX_FRAME_BUFFERS) return "MF_DEVICESTREAM_MAX_FRAME_BUFFERS";

            // Windows X attributes
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_FRAME_RAWSTREAM) return "MF_CAPTURE_METADATA_FRAME_RAWSTREAM";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_FOCUSSTATE) return "MF_CAPTURE_METADATA_FOCUSSTATE";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_REQUESTED_FRAME_SETTING_ID) return "MF_CAPTURE_METADATA_REQUESTED_FRAME_SETTING_ID";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_EXPOSURE_TIME) return "MF_CAPTURE_METADATA_EXPOSURE_TIME";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_EXPOSURE_COMPENSATION) return "MF_CAPTURE_METADATA_EXPOSURE_COMPENSATION";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_ISO_SPEED) return "MF_CAPTURE_METADATA_ISO_SPEED";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_LENS_POSITION) return "MF_CAPTURE_METADATA_LENS_POSITION";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_SCENE_MODE) return "MF_CAPTURE_METADATA_SCENE_MODE";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_FLASH) return "MF_CAPTURE_METADATA_FLASH";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_FLASH_POWER) return "MF_CAPTURE_METADATA_FLASH_POWER";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_WHITEBALANCE) return "MF_CAPTURE_METADATA_WHITEBALANCE";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_ZOOMFACTOR) return "MF_CAPTURE_METADATA_ZOOMFACTOR";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_FACEROIS) return "MF_CAPTURE_METADATA_FACEROIS";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_FACEROITIMESTAMPS) return "MF_CAPTURE_METADATA_FACEROITIMESTAMPS";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_FACEROICHARACTERIZATIONS) return "MF_CAPTURE_METADATA_FACEROICHARACTERIZATIONS";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_ISO_GAINS) return "MF_CAPTURE_METADATA_ISO_GAINS";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_SENSORFRAMERATE) return "MF_CAPTURE_METADATA_SENSORFRAMERATE";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_WHITEBALANCE_GAINS) return "MF_CAPTURE_METADATA_WHITEBALANCE_GAINS";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_METADATA_HISTOGRAM) return "MF_CAPTURE_METADATA_HISTOGRAM";

            if (guidToConvert == MFAttributesClsid.MF_SINK_VIDEO_PTS) return "MF_SINK_VIDEO_PTS";
            if (guidToConvert == MFAttributesClsid.MF_SINK_VIDEO_NATIVE_WIDTH) return "MF_SINK_VIDEO_NATIVE_WIDTH";
            if (guidToConvert == MFAttributesClsid.MF_SINK_VIDEO_NATIVE_HEIGHT) return "MF_SINK_VIDEO_NATIVE_HEIGHT";
            if (guidToConvert == MFAttributesClsid.MF_SINK_VIDEO_DISPLAY_ASPECT_RATIO_NUMERATOR) return "MF_SINK_VIDEO_DISPLAY_ASPECT_RATIO_NUMERATOR";
            if (guidToConvert == MFAttributesClsid.MF_SINK_VIDEO_DISPLAY_ASPECT_RATIO_DENOMINATOR) return "MF_SINK_VIDEO_DISPLAY_ASPECT_RATIO_DENOMINATOR";
            if (guidToConvert == MFAttributesClsid.MF_BD_MVC_PLANE_OFFSET_METADATA) return "MF_BD_MVC_PLANE_OFFSET_METADATA";
            if (guidToConvert == MFAttributesClsid.MF_LUMA_KEY_ENABLE) return "MF_LUMA_KEY_ENABLE";
            if (guidToConvert == MFAttributesClsid.MF_LUMA_KEY_LOWER) return "MF_LUMA_KEY_LOWER";
            if (guidToConvert == MFAttributesClsid.MF_LUMA_KEY_UPPER) return "MF_LUMA_KEY_UPPER";
            if (guidToConvert == MFAttributesClsid.MF_USER_EXTENDED_ATTRIBUTES) return "MF_USER_EXTENDED_ATTRIBUTES";
            if (guidToConvert == MFAttributesClsid.MF_INDEPENDENT_STILL_IMAGE) return "MF_INDEPENDENT_STILL_IMAGE";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_PROTECTION_MANAGER_PROPERTIES) return "MF_MEDIA_PROTECTION_MANAGER_PROPERTIES";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_HARDWARE) return "MFPROTECTION_HARDWARE";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_HDCP_WITH_TYPE_ENFORCEMENT) return "MFPROTECTION_HDCP_WITH_TYPE_ENFORCEMENT";
            if (guidToConvert == MFAttributesClsid.MF_XVP_CALLER_ALLOCATES_OUTPUT) return "MF_XVP_CALLER_ALLOCATES_OUTPUT";
            if (guidToConvert == MFAttributesClsid.MF_DEVICEMFT_EXTENSION_PLUGIN_CLSID) return "MF_DEVICEMFT_EXTENSION_PLUGIN_CLSID";
            if (guidToConvert == MFAttributesClsid.MF_DEVICEMFT_CONNECTED_FILTER_KSCONTROL) return "MF_DEVICEMFT_CONNECTED_FILTER_KSCONTROL";
            if (guidToConvert == MFAttributesClsid.MF_DEVICEMFT_CONNECTED_PIN_KSCONTROL) return "MF_DEVICEMFT_CONNECTED_PIN_KSCONTROL";
            if (guidToConvert == MFAttributesClsid.MF_DEVICE_THERMAL_STATE_CHANGED) return "MF_DEVICE_THERMAL_STATE_CHANGED";
            if (guidToConvert == MFAttributesClsid.MF_ACCESS_CONTROLLED_MEDIASOURCE_SERVICE) return "MF_ACCESS_CONTROLLED_MEDIASOURCE_SERVICE";
            if (guidToConvert == MFAttributesClsid.MF_CONTENT_DECRYPTOR_SERVICE) return "MF_CONTENT_DECRYPTOR_SERVICE";
            if (guidToConvert == MFAttributesClsid.MF_CONTENT_PROTECTION_DEVICE_SERVICE) return "MF_CONTENT_PROTECTION_DEVICE_SERVICE";
            if (guidToConvert == MFAttributesClsid.MF_SD_AUDIO_ENCODER_DELAY) return "MF_SD_AUDIO_ENCODER_DELAY";
            if (guidToConvert == MFAttributesClsid.MF_SD_AUDIO_ENCODER_PADDING) return "MF_SD_AUDIO_ENCODER_PADDING";

            if (guidToConvert == MFAttributesClsid.MFT_END_STREAMING_AWARE) return "MFT_END_STREAMING_AWARE";
            if (guidToConvert == MFAttributesClsid.MF_SA_D3D11_ALLOW_DYNAMIC_YUV_TEXTURE) return "MF_SA_D3D11_ALLOW_DYNAMIC_YUV_TEXTURE";
            if (guidToConvert == MFAttributesClsid.MFT_DECODER_QUALITY_MANAGEMENT_CUSTOM_CONTROL) return "MFT_DECODER_QUALITY_MANAGEMENT_CUSTOM_CONTROL";
            if (guidToConvert == MFAttributesClsid.MFT_DECODER_QUALITY_MANAGEMENT_RECOVERY_WITHOUT_ARTIFACTS) return "MFT_DECODER_QUALITY_MANAGEMENT_RECOVERY_WITHOUT_ARTIFACTS";

            if (guidToConvert == MFAttributesClsid.MF_SOURCE_READER_D3D11_BIND_FLAGS) return "MF_SOURCE_READER_D3D11_BIND_FLAGS";
            if (guidToConvert == MFAttributesClsid.MF_MEDIASINK_AUTOFINALIZE_SUPPORTED) return "MF_MEDIASINK_AUTOFINALIZE_SUPPORTED";
            if (guidToConvert == MFAttributesClsid.MF_MEDIASINK_ENABLE_AUTOFINALIZE) return "MF_MEDIASINK_ENABLE_AUTOFINALIZE";
            if (guidToConvert == MFAttributesClsid.MF_READWRITE_ENABLE_AUTOFINALIZE) return "MF_READWRITE_ENABLE_AUTOFINALIZE";

            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_BROWSER_COMPATIBILITY_MODE_IE_EDGE) return "MF_MEDIA_ENGINE_BROWSER_COMPATIBILITY_MODE_IE_EDGE";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_TELEMETRY_APPLICATION_ID) return "MF_MEDIA_ENGINE_TELEMETRY_APPLICATION_ID";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_TIMEDTEXT) return "MF_MEDIA_ENGINE_TIMEDTEXT";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_CONTINUE_ON_CODEC_ERROR) return "MF_MEDIA_ENGINE_CONTINUE_ON_CODEC_ERROR";

            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_CAMERA_STREAM_BLOCKED) return "MF_CAPTURE_ENGINE_CAMERA_STREAM_BLOCKED";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_CAMERA_STREAM_UNBLOCKED) return "MF_CAPTURE_ENGINE_CAMERA_STREAM_UNBLOCKED";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_ENABLE_CAMERA_STREAMSTATE_NOTIFICATION) return "MF_CAPTURE_ENGINE_ENABLE_CAMERA_STREAMSTATE_NOTIFICATION";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_SELECTEDCAMERAPROFILE) return "MF_CAPTURE_ENGINE_SELECTEDCAMERAPROFILE";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_SELECTEDCAMERAPROFILE_INDEX) return "MF_CAPTURE_ENGINE_SELECTEDCAMERAPROFILE_INDEX";

            if (guidToConvert == MFAttributesClsid.EVRConfig_AllowBatching) return "EVRConfig_AllowBatching";
            if (guidToConvert == MFAttributesClsid.EVRConfig_AllowDropToBob) return "EVRConfig_AllowDropToBob";
            if (guidToConvert == MFAttributesClsid.EVRConfig_AllowDropToHalfInterlace) return "EVRConfig_AllowDropToHalfInterlace";
            if (guidToConvert == MFAttributesClsid.EVRConfig_AllowDropToThrottle) return "EVRConfig_AllowDropToThrottle";
            if (guidToConvert == MFAttributesClsid.EVRConfig_AllowScaling) return "EVRConfig_AllowScaling";
            if (guidToConvert == MFAttributesClsid.EVRConfig_ForceBatching) return "EVRConfig_ForceBatching";
            if (guidToConvert == MFAttributesClsid.EVRConfig_ForceBob) return "EVRConfig_ForceBob";
            if (guidToConvert == MFAttributesClsid.EVRConfig_ForceHalfInterlace) return "EVRConfig_ForceHalfInterlace";
            if (guidToConvert == MFAttributesClsid.EVRConfig_ForceScaling) return "EVRConfig_ForceScaling";
            if (guidToConvert == MFAttributesClsid.EVRConfig_ForceThrottle) return "EVRConfig_ForceThrottle";
            if (guidToConvert == MFAttributesClsid.MF_ASFPROFILE_MAXPACKETSIZE) return "MF_ASFPROFILE_MAXPACKETSIZE";
            if (guidToConvert == MFAttributesClsid.MF_ASFPROFILE_MINPACKETSIZE) return "MF_ASFPROFILE_MINPACKETSIZE";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_D3D_MANAGER) return "MF_CAPTURE_ENGINE_D3D_MANAGER";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_DECODER_MFT_FIELDOFUSE_UNLOCK_Attribute) return "MF_CAPTURE_ENGINE_DECODER_MFT_FIELDOFUSE_UNLOCK_Attribute";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_DISABLE_DXVA) return "MF_CAPTURE_ENGINE_DISABLE_DXVA";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_DISABLE_HARDWARE_TRANSFORMS) return "MF_CAPTURE_ENGINE_DISABLE_HARDWARE_TRANSFORMS";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_ENCODER_MFT_FIELDOFUSE_UNLOCK_Attribute) return "MF_CAPTURE_ENGINE_ENCODER_MFT_FIELDOFUSE_UNLOCK_Attribute";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_EVENT_GENERATOR_GUID) return "MF_CAPTURE_ENGINE_EVENT_GENERATOR_GUID";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_EVENT_STREAM_INDEX) return "MF_CAPTURE_ENGINE_EVENT_STREAM_INDEX";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_MEDIASOURCE_CONFIG) return "MF_CAPTURE_ENGINE_MEDIASOURCE_CONFIG";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_OUTPUT_MEDIA_TYPE_SET) return "MF_CAPTURE_ENGINE_OUTPUT_MEDIA_TYPE_SET";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_RECORD_SINK_AUDIO_MAX_PROCESSED_SAMPLES) return "MF_CAPTURE_ENGINE_RECORD_SINK_AUDIO_MAX_PROCESSED_SAMPLES";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_RECORD_SINK_AUDIO_MAX_UNPROCESSED_SAMPLES) return "MF_CAPTURE_ENGINE_RECORD_SINK_AUDIO_MAX_UNPROCESSED_SAMPLES";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_RECORD_SINK_VIDEO_MAX_PROCESSED_SAMPLES) return "MF_CAPTURE_ENGINE_RECORD_SINK_VIDEO_MAX_PROCESSED_SAMPLES";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_RECORD_SINK_VIDEO_MAX_UNPROCESSED_SAMPLES) return "MF_CAPTURE_ENGINE_RECORD_SINK_VIDEO_MAX_UNPROCESSED_SAMPLES";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_USE_AUDIO_DEVICE_ONLY) return "MF_CAPTURE_ENGINE_USE_AUDIO_DEVICE_ONLY";
            if (guidToConvert == MFAttributesClsid.MF_CAPTURE_ENGINE_USE_VIDEO_DEVICE_ONLY) return "MF_CAPTURE_ENGINE_USE_VIDEO_DEVICE_ONLY";
            if (guidToConvert == MFAttributesClsid.MF_SOURCE_STREAM_SUPPORTS_HW_CONNECTION) return "MF_SOURCE_STREAM_SUPPORTS_HW_CONNECTION";
            if (guidToConvert == MFAttributesClsid.MF_VIDEODSP_MODE) return "MF_VIDEODSP_MODE";
            if (guidToConvert == MFAttributesClsid.MFASFSPLITTER_PACKET_BOUNDARY) return "MFASFSPLITTER_PACKET_BOUNDARY";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_DeviceReferenceSystemTime) return "MFSampleExtension_DeviceReferenceSystemTime";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_VideoDSPMode) return "MFSampleExtension_VideoDSPMode";

            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_CALLBACK) return "MF_MEDIA_ENGINE_CALLBACK";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_DXGI_MANAGER) return "MF_MEDIA_ENGINE_DXGI_MANAGER";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_EXTENSION) return "MF_MEDIA_ENGINE_EXTENSION";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_PLAYBACK_HWND) return "MF_MEDIA_ENGINE_PLAYBACK_HWND";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_OPM_HWND) return "MF_MEDIA_ENGINE_OPM_HWND";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_PLAYBACK_VISUAL) return "MF_MEDIA_ENGINE_PLAYBACK_VISUAL";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_COREWINDOW) return "MF_MEDIA_ENGINE_COREWINDOW";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_VIDEO_OUTPUT_FORMAT) return "MF_MEDIA_ENGINE_VIDEO_OUTPUT_FORMAT";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_CONTENT_PROTECTION_FLAGS) return "MF_MEDIA_ENGINE_CONTENT_PROTECTION_FLAGS";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_CONTENT_PROTECTION_MANAGER) return "MF_MEDIA_ENGINE_CONTENT_PROTECTION_MANAGER";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_AUDIO_ENDPOINT_ROLE) return "MF_MEDIA_ENGINE_AUDIO_ENDPOINT_ROLE";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_AUDIO_CATEGORY) return "MF_MEDIA_ENGINE_AUDIO_CATEGORY";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_STREAM_CONTAINS_ALPHA_CHANNEL) return "MF_MEDIA_ENGINE_STREAM_CONTAINS_ALPHA_CHANNEL";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_BROWSER_COMPATIBILITY_MODE) return "MF_MEDIA_ENGINE_BROWSER_COMPATIBILITY_MODE";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_SOURCE_RESOLVER_CONFIG_STORE) return "MF_MEDIA_ENGINE_SOURCE_RESOLVER_CONFIG_STORE";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_NEEDKEY_CALLBACK) return "MF_MEDIA_ENGINE_NEEDKEY_CALLBACK";
            if (guidToConvert == MFAttributesClsid.MF_MEDIA_ENGINE_TRACK_ID) return "MF_MEDIA_ENGINE_TRACK_ID";

            if (guidToConvert == MFAttributesClsid.MFPROTECTION_ACP) return "MFPROTECTION_ACP";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_CGMSA) return "MFPROTECTION_CGMSA";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_CONSTRICTAUDIO) return "MFPROTECTION_CONSTRICTAUDIO";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_CONSTRICTVIDEO) return "MFPROTECTION_CONSTRICTVIDEO";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_CONSTRICTVIDEO_NOOPM) return "MFPROTECTION_CONSTRICTVIDEO_NOOPM";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_DISABLE) return "MFPROTECTION_DISABLE";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_FFT) return "MFPROTECTION_FFT";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_HDCP) return "MFPROTECTION_HDCP";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_TRUSTEDAUDIODRIVERS) return "MFPROTECTION_TRUSTEDAUDIODRIVERS";
            if (guidToConvert == MFAttributesClsid.MFPROTECTION_WMDRMOTA) return "MFPROTECTION_WMDRMOTA";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_SampleID) return "MFSampleExtension_Encryption_SampleID";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Encryption_SubSampleMappingSplit) return "MFSampleExtension_Encryption_SubSampleMappingSplit";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_PacketCrossOffsets) return "MFSampleExtension_PacketCrossOffsets";
            if (guidToConvert == MFAttributesClsid.MFSampleExtension_Content_KeyID) return "MFSampleExtension_Content_KeyID";
            if (guidToConvert == MFAttributesClsid.MF_MSE_ACTIVELIST_CALLBACK) return "MF_MSE_ACTIVELIST_CALLBACK";
            if (guidToConvert == MFAttributesClsid.MF_MSE_BUFFERLIST_CALLBACK) return "MF_MSE_BUFFERLIST_CALLBACK";
            if (guidToConvert == MFAttributesClsid.MF_MSE_CALLBACK) return "MF_MSE_CALLBACK";
            if (guidToConvert == MFAttributesClsid.MF_MT_VIDEO_3D) return "MF_MT_VIDEO_3D";
            return "Unknown";
        }
    }
}
