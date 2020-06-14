using MZ.Media.ComObjects;
using MZ.Media.Device;
using System.Runtime.InteropServices;

namespace MZ.Media.Device
{
    [Guid(ComIIds.IMM_ENDPOINT_IID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMEndpoint
    {
        int GetDataFlow([Out] [MarshalAs(UnmanagedType.I4)] out EDataFlow dataFlow);
    }
}
