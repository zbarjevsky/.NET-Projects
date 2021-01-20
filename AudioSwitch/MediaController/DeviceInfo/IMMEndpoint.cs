using MkZ.Media.ComObjects;
using MkZ.Media.Device;
using System.Runtime.InteropServices;

namespace MkZ.Media.Device
{
    [Guid(ComIIds.IMM_ENDPOINT_IID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMEndpoint
    {
        int GetDataFlow([Out] [MarshalAs(UnmanagedType.I4)] out EDataFlow dataFlow);
    }
}
