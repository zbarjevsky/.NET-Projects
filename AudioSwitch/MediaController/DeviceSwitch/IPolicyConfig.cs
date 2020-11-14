﻿using System;
using System.Runtime.InteropServices;
//using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
//using AudioSwitcher.AudioApi.Hooking.ComObjects;
using MZ.Media.ComObjects;
//using SoundSwitch.Audio.Manager.Interop.Enum;

namespace MZ.Media.DeviceSwitch
{
    [Guid(ComIIds.POLICY_CONFIG_IID)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPolicyConfig
    {
        [PreserveSig]
        int GetMixFormat(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] IntPtr ppFormat);

        [PreserveSig]
        int GetDeviceFormat(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] [MarshalAs(UnmanagedType.Bool)] bool bDefault,
            [In] IntPtr ppFormat);

        [PreserveSig]
        int ResetDeviceFormat([In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName);

        [PreserveSig]
        int SetDeviceFormat(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] IntPtr pEndpointFormat,
            [In] IntPtr mixFormat);

        [PreserveSig]
        int GetProcessingPeriod(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] [MarshalAs(UnmanagedType.Bool)] bool bDefault,
            [In] IntPtr pmftDefaultPeriod,
            [In] IntPtr pmftMinimumPeriod);

        [PreserveSig]
        int SetProcessingPeriod(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] IntPtr pmftPeriod);

        [PreserveSig]
        int GetShareMode(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] IntPtr pMode);

        [PreserveSig]
        int SetShareMode(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] IntPtr mode);

        [PreserveSig]
        int GetPropertyValue(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] [MarshalAs(UnmanagedType.Bool)] bool bFxStore,
            [In] IntPtr key,
            [In] IntPtr pv);

        [PreserveSig]
        int SetPropertyValue(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] [MarshalAs(UnmanagedType.Bool)] bool bFxStore,
            [In] IntPtr key,
            [In] IntPtr pv);

        [PreserveSig]
        int SetDefaultEndpoint(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] [MarshalAs(UnmanagedType.U4)] Role role);

        [PreserveSig]
        int SetEndpointVisibility(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] [MarshalAs(UnmanagedType.Bool)] bool bVisible);
    }
}