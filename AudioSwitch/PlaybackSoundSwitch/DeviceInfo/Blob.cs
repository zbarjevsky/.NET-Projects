using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaybackSoundSwitch.Device
{
    /// <summary>
    /// Representation of binary large object container.
    /// </summary>
    public struct Blob
    {
        /// <summary>
        /// Length of binary object.
        /// </summary>
        public int Length;
        /// <summary>
        /// Pointer to buffer storing data.
        /// </summary>
        public IntPtr Data;
    }
}
