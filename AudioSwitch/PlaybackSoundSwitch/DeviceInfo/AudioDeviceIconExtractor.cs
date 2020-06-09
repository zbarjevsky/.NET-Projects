/********************************************************************
* Copyright (C) 2015-2017 Antoine Aflalo
*
* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
********************************************************************/

using System;
using System.Drawing;
using System.Runtime.Caching;
//using System.Runtime.Caching;
//using NAudio.CoreAudioApi;
using PlaybackSoundSwitch.Device;
using PlaybackSoundSwitch.Properties;
//using SoundSwitch.Common.Framework.Icon;
//using Serilog;
//using SoundSwitch.Common.Framework.Icon;
//using SoundSwitch.Common.Properties;

namespace PlaybackSoundSwitch.Device
{
    public class AudioDeviceIconExtractor
    {

        private static readonly System.Drawing.Icon DefaultSpeakers = Icon.FromHandle(Resources.defaultSpeakers.GetHicon());
        private static readonly System.Drawing.Icon DefaultMicrophone = Icon.FromHandle(Resources.defaultMicrophone.GetHicon());

        private static readonly MemoryCache IconCache = new MemoryCache("_iconCache");
        private static readonly CacheItemPolicy CacheItemPolicy = new CacheItemPolicy
        {
            RemovedCallback = CleanupIcon,
            SlidingExpiration = TimeSpan.FromMinutes(30)
        };

        private static void CleanupIcon(CacheEntryRemovedArguments arg)
        {
            IDisposable item = arg.CacheItem.Value as IDisposable;
            if (item == null) 
                return;

            try
            {
                item.Dispose();
            }
            catch (ObjectDisposedException)
            {

            }
        }

        private static string GetKey(MMDevice audioDevice, bool largeIcon)
        {
            return $"{audioDevice.IconPath}-${largeIcon}";
        }

        /// <summary>
        /// Extract an Icon form a given path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dataFlow"></param>
        /// <param name="largeIcon"></param>
        /// <returns></returns>
        public static System.Drawing.Icon ExtractIconFromPath(string path, EDataFlow dataFlow, bool largeIcon)
        {
            System.Drawing.Icon ico = null;
            var key = $"{path}-${largeIcon}";

            if (IconCache.Contains(key))
            {
                return (System.Drawing.Icon)IconCache.Get(key);
            }

            try
            {
                if (path.EndsWith(".ico"))
                {
                    ico = System.Drawing.Icon.ExtractAssociatedIcon(path);
                }
                else
                {
                    var iconInfo = path.Split(',');
                    var dllPath = iconInfo[0];
                    var iconIndex = int.Parse(iconInfo[1]);
                    ico = IconExtractor.Extract(dllPath, iconIndex, largeIcon);
                }
            }
            catch (Exception e)
            {
                //Log.Error(e, "Can't extract icon from {path}", path);
                MZ.Tools.Trace.Debug("Can't extract icon from {0}, Error: {1}", path, e);
                switch (dataFlow)
                {
                    case EDataFlow.Capture:
                        return DefaultMicrophone;
                    case EDataFlow.Render:
                        return DefaultSpeakers;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            IconCache.Add(key, ico, CacheItemPolicy);
            
            return ico;
        }

        /// <summary>
        ///     Extract the Icon out of an AudioDevice
        /// </summary>
        /// <param name="audioDevice"></param>
        /// <param name="largeIcon"></param>
        /// <returns></returns>
        public static System.Drawing.Icon ExtractIconFromAudioDevice(MMDevice audioDevice, bool largeIcon)
        {
            return ExtractIconFromPath(audioDevice.IconPath, audioDevice.DataFlow, largeIcon);
        }
    }
    }