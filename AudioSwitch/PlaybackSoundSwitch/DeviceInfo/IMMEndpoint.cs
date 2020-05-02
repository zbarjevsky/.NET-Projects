using PlaybackSoundSwitch.ComObjects;
using PlaybackSoundSwitch.Device;
using System.Runtime.InteropServices;

namespace PlaybackSoundSwitch.Device
{
    [Guid(ComIIds.IMM_ENDPOINT_IID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMEndpoint
    {
        int GetDataFlow([Out] [MarshalAs(UnmanagedType.I4)] out EDataFlow dataFlow);
    }
}
