using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PlaybackSoundSwitch.Device
{
    /// <summary>
    /// Property Store class, only supports reading properties at the moment.
    /// </summary>
    public class PropertyStore
    {
        private readonly IPropertyStore storeInterface;

        /// <summary>
        /// Property Count
        /// </summary>
        public int Count
        {
            get
            {
                Marshal.ThrowExceptionForHR(storeInterface.GetCount(out var result));
                return (int)result;
            }
        }

        /// <summary>
        /// Gets property by index
        /// </summary>
        /// <param name="index">Property index</param>
        /// <returns>The property</returns>
        public PropertyStoreProperty this[uint index]
        {
            get
            {
                PropertyKey key = Get(index);
                Marshal.ThrowExceptionForHR(storeInterface.GetValue(ref key, out var result));
                return new PropertyStoreProperty(key, result);
            }
        }

        /// <summary>
        /// Contains property guid
        /// </summary>
        /// <param name="key">Looks for a specific key</param>
        /// <returns>True if found</returns>
        public bool Contains(PropertyKey key)
        {
            for (uint i = 0; i < Count; i++)
            {
                PropertyKey ikey = Get(i);
                if ((ikey.formatId == key.formatId) && (ikey.propertyId == key.propertyId))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Indexer by guid
        /// </summary>
        /// <param name="key">Property Key</param>
        /// <returns>Property or null if not found</returns>
        public PropertyStoreProperty this[PropertyKey key]
        {
            get
            {
                for (uint i = 0; i < Count; i++)
                {
                    PropertyKey ikey = Get(i);
                    if ((ikey.formatId == key.formatId) && (ikey.propertyId == key.propertyId))
                    {
                        Marshal.ThrowExceptionForHR(storeInterface.GetValue(ref ikey, out var result));
                        return new PropertyStoreProperty(ikey, result);
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets property key at sepecified index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Property key</returns>
        public PropertyKey Get(uint index)
        {
            Marshal.ThrowExceptionForHR(storeInterface.GetAt(index, out var key));
            return key;
        }

        /// <summary>
        /// Gets property value at specified index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Property value</returns>
        public PropVariant GetValue(uint index)
        {
            PropertyKey key = Get(index);
            Marshal.ThrowExceptionForHR(storeInterface.GetValue(ref key, out var result));
            return result;
        }

        /// <summary>
        /// Sets property value at specified key.
        /// </summary>
        /// <param name="key">Key of property to set.</param>
        /// <param name="value">Value to write.</param>
        public void SetValue(PropertyKey key, PropVariant value)
        {
            Marshal.ThrowExceptionForHR(storeInterface.SetValue(ref key, ref value));
        }

        /// <summary>
        /// Saves a property change.
        /// </summary>
        public void Commit()
        {
            Marshal.ThrowExceptionForHR(storeInterface.Commit());
        }

        /// <summary>
        /// Creates a new property store
        /// </summary>
        /// <param name="store">IPropertyStore COM interface</param>
        internal PropertyStore(IPropertyStore store)
        {
            storeInterface = store;
        }
    }
}
