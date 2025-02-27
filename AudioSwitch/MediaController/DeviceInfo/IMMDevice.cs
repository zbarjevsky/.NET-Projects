﻿using MkZ.Media.ComObjects;
using MkZ.Media.Device;
using System;
using System.Runtime.InteropServices;

namespace MkZ.Media.Device
{
    [Guid(ComIIds.IMM_DEVICE_IID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice
    {
        [PreserveSig]
        int Activate(
            [In] ref Guid interfaceId,
            [In] [MarshalAs(UnmanagedType.U4)] ClsCtx classContext,
            [In, Optional] IntPtr activationParams,
            [Out] [MarshalAs(UnmanagedType.IUnknown)] out object instancePtr);

        [PreserveSig]
        int OpenPropertyStore(
            [In] [MarshalAs(UnmanagedType.U4)] StorageAccessMode accessMode,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IPropertyStore properties);

        [PreserveSig]
        int GetId(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string strId);

        [PreserveSig]
        int GetState(
            [Out] [MarshalAs(UnmanagedType.U4)] out EDeviceState deviceState);
    }
}
