﻿using MZ.Media.ComObjects;

namespace MZ.Media.DeviceSwitch
{
    internal sealed class AudioPolicyConfigFactory
    {
        public static IAudioPolicyConfigFactory Create()
        {
            var iid = typeof(IAudioPolicyConfigFactory).GUID;
            object factory;
            ComBase.RoGetActivationFactory("Windows.Media.Internal.AudioPolicyConfig", ref iid, out factory);
            return (IAudioPolicyConfigFactory)factory;
        }
    }
}