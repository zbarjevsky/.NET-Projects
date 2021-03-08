using System.Linq;


using MkZ.Media.ComObjects;
using MkZ.Media.Device;
using MkZ.Windows.Win32API;

namespace MkZ.Media.DeviceSwitch
{
    public class AudioSwitcher
    {
        private static AudioSwitcher _instance;
        private readonly PolicyClient _policyClient = new PolicyClient();
        private readonly EnumeratorClient _enumerator = new EnumeratorClient();

        private ExtendedPolicyClient _extendedPolicyClient;

        private ExtendedPolicyClient ExtendPolicyClient
        {
            get
            {
                if (_extendedPolicyClient != null)
                {
                    return _extendedPolicyClient;
                }

                return _extendedPolicyClient = ComThread.Invoke(() => new ExtendedPolicyClient());
            }
        }

        private AudioSwitcher()
        {
        }

        public static AudioSwitcher Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                return _instance = ComThread.Invoke(() => new AudioSwitcher());
            }
        }

        /// <summary>
        /// Switch the default audio device to the one given
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="role"></param>
        public void SwitchTo(string deviceId, Role role)
        {
            if (role != Role.All)
            {
                ComThread.Invoke((() =>
                {
                    if (_enumerator.IsDefault(deviceId, EDataFlow.Render, role) || _enumerator.IsDefault(deviceId, EDataFlow.Capture, role))
                    {
                        System.Diagnostics.Trace.WriteLine($"Default endpoint already {deviceId}");
                        return;
                    }

                    _policyClient.SetDefaultEndpoint(deviceId, role);
                }));

                return;
            }

            SwitchTo(deviceId, Role.Console);
            SwitchTo(deviceId, Role.Multimedia);
            SwitchTo(deviceId, Role.Communications);
        }

        /// <summary>
        /// Switch the audio endpoint of the given process
        /// </summary>
        /// <param name="deviceId">Id of the device</param>
        /// <param name="role">Which role to switch</param>
        /// <param name="flow">Which flow to switch</param>
        /// <param name="processId">ProcessID of the process</param>
        public void SwitchProcessTo(string deviceId, Role role, EDataFlow flow, uint processId)
        {
            var roles = new Role[]
            {
                Role.Console,
                Role.Communications,
                Role.Multimedia
            };

            if (role != Role.All)
            {
                roles = new Role[]
                {
                    role
                };
            }

            ComThread.Invoke((() =>
            {
                var currentEndpoint = roles.Select(eRole => ExtendPolicyClient.GetDefaultEndPoint(flow, eRole, processId)).FirstOrDefault(endpoint => !string.IsNullOrEmpty(endpoint));
                if (deviceId.Equals(currentEndpoint))
                {
                    System.Diagnostics.Trace.WriteLine($"Default endpoint for {processId} already {deviceId}");
                    return;
                }

                ExtendPolicyClient.SetDefaultEndPoint(deviceId, flow, roles, processId);
            }));
        }

        /// <summary>
        /// Switch the audio device of the Foreground Process
        /// </summary>
        /// <param name="deviceId">Id of the device</param>
        /// <param name="role">Which role to switch</param>
        /// <param name="flow">Which flow to switch</param>
        public void SwitchProcessTo(string deviceId, Role role, EDataFlow flow)
        {
            var processId = ComThread.Invoke(() => User32.ForegroundProcessId);
            SwitchProcessTo(deviceId, role, flow, processId);
        }

        /// <summary>
        /// Is the given deviceId the default audio device in the system
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="flow"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsDefault(string deviceId, EDataFlow flow, Role role)
        {
            return ComThread.Invoke(() => _enumerator.IsDefault(deviceId, flow, role));
        }

        /// <summary>
        /// Get the device used by the given process
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="role"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public string GetUsedDevice(EDataFlow flow, Role role, uint processId)
        {
            return ComThread.Invoke(() => ExtendPolicyClient.GetDefaultEndPoint(flow, role, processId));
        }

        /// <summary>
        /// Reset Windows configuration for the process that had their audio device changed
        /// </summary>
        public void ResetProcessDeviceConfiguration()
        {
            ComThread.Invoke(() => ExtendPolicyClient.ResetAllSetEndpoint());
        }
    }
}