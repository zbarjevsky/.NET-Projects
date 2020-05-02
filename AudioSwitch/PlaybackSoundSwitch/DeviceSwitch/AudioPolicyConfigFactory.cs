using PlaybackSoundSwitch.ComObjects;

namespace PlaybackSoundSwitch.DeviceSwitch
{
    internal sealed class AudioPolicyConfigFactory
    {
        public static IAudioPolicyConfigFactory Create()
        {
            var iid = typeof(IAudioPolicyConfigFactory).GUID;
            ComBase.RoGetActivationFactory("Windows.Media.Internal.AudioPolicyConfig", ref iid, out object factory);
            return (IAudioPolicyConfigFactory)factory;
        }
    }
}