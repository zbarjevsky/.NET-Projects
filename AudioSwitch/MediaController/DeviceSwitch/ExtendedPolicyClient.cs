﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using MkZ.Media.ComObjects;
using MkZ.Media.Device;

namespace MkZ.Media.DeviceSwitch
{
    internal class ExtendedPolicyClient
    {
        private const string DEVINTERFACE_AUDIO_RENDER = "#{e6327cad-dcec-4949-ae8a-991e976a79d2}";
        private const string DEVINTERFACE_AUDIO_CAPTURE = "#{2eef81be-33fa-4800-9670-1cd474972c3f}";
        private const string MMDEVAPI_TOKEN = @"\\?\SWD#MMDEVAPI#";

        private IAudioPolicyConfigFactory _sharedPolicyConfig;

        private IAudioPolicyConfigFactory PolicyConfig
        {
            get
            {
                if (_sharedPolicyConfig != null)
                {
                    return _sharedPolicyConfig;
                }

                return _sharedPolicyConfig = AudioPolicyConfigFactory.Create();
            }
        }

        private static string GenerateDeviceId(string deviceId, EDataFlow flow)
        {
            return $"{MMDEVAPI_TOKEN}{deviceId}{(flow == EDataFlow.Render ? DEVINTERFACE_AUDIO_RENDER : DEVINTERFACE_AUDIO_CAPTURE)}";
        }

        private static string UnpackDeviceId(string deviceId)
        {
            if (deviceId.StartsWith(MMDEVAPI_TOKEN)) deviceId = deviceId.Remove(0, MMDEVAPI_TOKEN.Length);
            if (deviceId.EndsWith(DEVINTERFACE_AUDIO_RENDER)) deviceId = deviceId.Remove(deviceId.Length - DEVINTERFACE_AUDIO_RENDER.Length);
            if (deviceId.EndsWith(DEVINTERFACE_AUDIO_CAPTURE)) deviceId = deviceId.Remove(deviceId.Length - DEVINTERFACE_AUDIO_CAPTURE.Length);
            return deviceId;
        }

        public void SetDefaultEndPoint(string deviceId, EDataFlow flow, IEnumerable<Role> roles, uint processId)
        {
            Trace.WriteLine($"ExtendedPolicyClient SetDefaultEndPoint {deviceId} [{flow}] {processId}");
            try
            {
                var stringPtrDeviceId = IntPtr.Zero;

                if (!string.IsNullOrWhiteSpace(deviceId))
                {
                    var str = GenerateDeviceId(deviceId, flow);
                    ComBase.WindowsCreateString(str, (uint) str.Length, out stringPtrDeviceId);
                }

                foreach (var eRole in roles)
                {
                    PolicyConfig.SetPersistedDefaultAudioEndpoint(processId, flow, eRole, stringPtrDeviceId);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"{ex}");
            }
        }

        /// <summary>
        /// Get the deviceId of the current DefaultEndpoint
        /// </summary>
        public string GetDefaultEndPoint(EDataFlow flow, Role role, uint processId)
        {
            try
            {
                string deviceId;
                PolicyConfig.GetPersistedDefaultAudioEndpoint(processId, flow, role, out deviceId);
                return UnpackDeviceId(deviceId);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"{ex}");
            }

            return null;
        }

        public void ResetAllSetEndpoint()
        {
            try
            {
                PolicyConfig.ClearAllPersistedApplicationDefaultEndpoints();
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"{ex}");
            }
        }
    }
}