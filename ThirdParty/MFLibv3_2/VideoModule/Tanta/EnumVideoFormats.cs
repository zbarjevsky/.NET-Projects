using CameraCapture.Interface;
using MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TantaCommon;

namespace VideoModule.Tanta
{
    public class EnumVideoFormats
    {
        public static List<TantaMFVideoFormatContainer> GetVideoFormatsForCaptureDevice(TantaMFDevice device)
        {
            IMFMediaSource mediaSource = TantaWMFUtils.GetMediaSourceFromTantaDevice(device);
            if (mediaSource == null)
            {
                throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to mediaSource == null");
            }

            // A presentation is a set of related media streams that share a common presentation time.
            // we don't need that functionality in this app but we do need to presentation descriptor
            // to find out the stream descriptors, these will give us the media types on offer
            HResult hr = mediaSource.CreatePresentationDescriptor(out IMFPresentationDescriptor sourcePresentationDescriptor);
            if (hr != HResult.S_OK)
            {
                throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to mediaSource.CreatePresentationDescriptor failed. Err=" + hr.ToString());
            }
            if (sourcePresentationDescriptor == null)
            {
                throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to mediaSource.CreatePresentationDescriptor failed. sourcePresentationDescriptor == null");
            }

            // Now we get the number of stream descriptors in the presentation. 
            // A presentation descriptor contains a list of one or more 
            // stream descriptors. 
            hr = sourcePresentationDescriptor.GetStreamDescriptorCount(out int sourceStreamCount);
            if (hr != HResult.S_OK)
            {
                throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to sourcePresentationDescriptor.GetStreamDescriptorCount failed. Err=" + hr.ToString());
            }
            if (sourceStreamCount == 0)
            {
                throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to sourcePresentationDescriptor.GetStreamDescriptorCount failed. sourceStreamCount == 0");
            }

            // look for the video stream
            List<TantaMFVideoFormatContainer> formatList = new List<TantaMFVideoFormatContainer>();

            // now loop through each media type
            for (int i = 0; i < sourceStreamCount; i++)
            {
                // we require the major type to be video
                Guid guidMajorType = TantaWMFUtils.GetMajorMediaTypeFromPresentationDescriptor(sourcePresentationDescriptor, i);
                if (guidMajorType != MFMediaType.Video) continue;

                // we also require the stream to be enabled
                hr = sourcePresentationDescriptor.GetStreamDescriptorByIndex(i, out bool streamIsSelected, out IMFStreamDescriptor videoStreamDescriptor);
                if (hr != HResult.S_OK)
                {
                    throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to sourcePresentationDescriptor.GetStreamDescriptorByIndex(v) failed. Err=" + hr.ToString());
                }
                if (videoStreamDescriptor == null)
                {
                    throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to sourcePresentationDescriptor.GetStreamDescriptorByIndex(v) failed. videoStreamDescriptor == null");
                }
                // if the stream is not selected (enabled) look for the next
                if (streamIsSelected == false)
                {
                    Marshal.ReleaseComObject(videoStreamDescriptor);
                    videoStreamDescriptor = null;
                    continue;
                }

                // Get the media type handler for the stream. IMFMediaTypeHandler 
                // interface is a standard way of looking at the media types on an stream
                hr = videoStreamDescriptor.GetMediaTypeHandler(out IMFMediaTypeHandler typeHandler);
                if (hr != HResult.S_OK)
                {
                    throw new Exception("call to videoStreamDescriptor.GetMediaTypeHandler failed. Err=" + hr.ToString());
                }
                if (typeHandler == null)
                {
                    throw new Exception("call to videoStreamDescriptor.GetMediaTypeHandler failed. typeHandler == null");
                }
                // Now we get the number of media types in the stream descriptor.
                hr = typeHandler.GetMediaTypeCount(out int mediaTypeCount);
                if (hr != HResult.S_OK)
                {
                    throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to typeHandler.GetMediaTypeCount failed. Err=" + hr.ToString());
                }
                if (mediaTypeCount == 0)
                {
                    throw new Exception("DisplayVideoFormatsForCurrentCaptureDevice call to typeHandler.GetMediaTypeCount failed. mediaTypeCount == 0");
                }

                for (int mediaTypeId = 0; mediaTypeId < mediaTypeCount; mediaTypeId++)
                {
                    // Now we have the handler, get the media type.
                    IMFMediaType workingMediaType = null;
                    hr = typeHandler.GetMediaTypeByIndex(mediaTypeId, out workingMediaType);
                    if (hr != HResult.S_OK)
                    {
                        throw new Exception("GetMediaTypeFromStreamDescriptorById call to typeHandler.GetMediaTypeByIndex failed. Err=" + hr.ToString());
                    }
                    if (workingMediaType == null)
                    {
                        throw new Exception("GetMediaTypeFromStreamDescriptorById call to typeHandler.GetMediaTypeByIndex failed. workingMediaType == null");
                    }
                    TantaMFVideoFormatContainer tmpContainer = TantaMediaTypeInfo.GetVideoFormatContainerFromMediaTypeObject(workingMediaType, device);
                    if (tmpContainer == null)
                    {
                        // we failed
                        throw new Exception("GetSupportedVideoFormatsFromSourceReaderInFormatContainers failed on call to GetVideoFormatContainerFromMediaTypeObject");
                    }
                    // now add it
                    formatList.Add(tmpContainer);
                    Marshal.ReleaseComObject(workingMediaType);
                    workingMediaType = null;
                }

                // NOTE: we only do the first enabled video stream we find.
                // it is possible to have more but our control
                // cannot cope with that
                break;
            }

            return formatList;
        }
    }
}
