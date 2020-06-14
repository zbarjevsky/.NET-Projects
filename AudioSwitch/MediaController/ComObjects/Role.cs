using System;
using System.Runtime.InteropServices;

namespace MZ.Media.ComObjects
{
    /// <summary>
    ///     The ERole enumeration defines constants that indicate the role
    ///     that the system has assigned to an audio endpoint device
    /// </summary>
    [Flags]
    [ComVisible(true)]
    public enum Role
    {
        /// <summary>
        ///     Games, system notification sounds, and voice commands.
        /// </summary>
        Console = 0,

        /// <summary>
        ///     Music, movies, narration, and live music recording
        /// </summary>
        Multimedia = Console + 1,

        /// <summary>
        ///     Voice communications (talking to another person).
        /// </summary>
        Communications = Multimedia + 1,

        /// <summary>
        ///  for all combined
        /// </summary>
        All = Communications + 1
    }
}