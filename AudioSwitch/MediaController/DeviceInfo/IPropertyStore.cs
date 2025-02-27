﻿using MkZ.Media.ComObjects;
using MkZ.Media.Device;
using System.Runtime.InteropServices;

namespace MkZ.Media.Device
{
    [Guid(ComIIds.PROPERTY_STORE_IID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyStore
    {
        [PreserveSig]
        int GetCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out uint propertyCount);

        [PreserveSig]
        int GetAt(
            [In] [MarshalAs(UnmanagedType.U4)] uint propertyIndex,
            [Out] out PropertyKey propertyKey);

        [PreserveSig]
        int GetValue(
            [In] ref PropertyKey propertyKey,
            [Out] out PropVariant value);

        [PreserveSig]
        int SetValue(
            [In] ref PropertyKey propertyKey,
            [In] ref PropVariant value);

        [PreserveSig]
        int Commit();
    }
}
