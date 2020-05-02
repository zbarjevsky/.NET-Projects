using PlaybackSoundSwitch.ComObjects;
using System.Runtime.InteropServices;

namespace PlaybackSoundSwitch.Device
{
    [Guid(ComIIds.IMM_DEVICE_COLLECTION_IID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceCollection
    {
        [PreserveSig]
        int GetCount([Out] [MarshalAs(UnmanagedType.U4)] out uint count);

        [PreserveSig]
        int Item(
            [In] [MarshalAs(UnmanagedType.U4)] uint index,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IMMDevice device);
    }
}
