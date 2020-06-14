using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZ.Media.Device
{
    /// <summary>
    /// MMDevice STGM enumeration
    /// </summary>
    public enum StorageAccessMode
    {
        /// <summary>
        /// Read-only access mode.
        /// </summary>
        Read,
        /// <summary>
        /// Write-only access mode.
        /// </summary>
        Write,
        /// <summary>
        /// Read-write access mode.
        /// </summary>
        ReadWrite
    }
}
