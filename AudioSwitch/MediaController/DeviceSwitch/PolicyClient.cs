using System;
using System.Runtime.InteropServices;
using PlaybackSoundSwitch.ComObjects;
using SoundSwitch.Audio.Manager.Interop.Interface;
using SoundSwitch.Audio.Manager.Interop.Interface.Policy;

namespace PlaybackSoundSwitch.DeviceSwitch
{
    internal class PolicyClient
    {
        private readonly IPolicyConfig _configVII;
        private readonly IPolicyConfigVista _configVista;
        private readonly IPolicyConfigX _configX;
        private readonly _PolicyConfigClient _policyConfig;

        public PolicyClient()
        {
            _policyConfig = new _PolicyConfigClient();

            _configX = _policyConfig as IPolicyConfigX;
            _configVII = _policyConfig as IPolicyConfig;
            _configVista = _policyConfig as IPolicyConfigVista;
        }

        ~PolicyClient()
        {
            if (_policyConfig != null && Marshal.IsComObject(_policyConfig))
                Marshal.FinalReleaseComObject(_policyConfig);
        }

        public void SetDefaultEndpoint(string devId, Role eRole)
        {
            if (_configX != null)
            {
                Marshal.ThrowExceptionForHR(_configX.SetDefaultEndpoint(devId, eRole));
            }
            else if (_configVII != null)
            {
                Marshal.ThrowExceptionForHR(_configVII.SetDefaultEndpoint(devId, eRole));
            }
            else if (_configVista != null)
            {
                Marshal.ThrowExceptionForHR(_configVista.SetDefaultEndpoint(devId, eRole));
            }
        }

        [ComImport, Guid(ComIIds.POLICY_CONFIG_CLIENT_IID)]
        private class _PolicyConfigClient
        {
        }
    }
}