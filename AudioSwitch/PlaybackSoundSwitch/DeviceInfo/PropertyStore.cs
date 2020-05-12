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
        private readonly IPropertyStore _storeInterface;
        private Dictionary<uint, PropertyKey> _propertyKeys = new Dictionary<uint, PropertyKey>();

        /// <summary>
        /// Property Count
        /// </summary>
        public int Count
        {
            get
            {
                uint count;
                Marshal.ThrowExceptionForHR(_storeInterface.GetCount(out count));
                return (int)count;
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
                PropertyKey key = _propertyKeys[index];
                PropVariant result;
                Marshal.ThrowExceptionForHR(_storeInterface.GetValue(ref key, out result));
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
            int count = Count; 
            for (uint i = 0; i < count; i++)
            {
                PropertyKey ikey = _propertyKeys[i];
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
                int count = Count;
                for (uint i = 0; i < count; i++)
                {
                    PropertyKey ikey = _propertyKeys[i];
                    if ((ikey.formatId == key.formatId) && (ikey.propertyId == key.propertyId))
                    {
                        PropVariant result;
                        Marshal.ThrowExceptionForHR(_storeInterface.GetValue(ref ikey, out result));
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
        private PropertyKey Get(uint index)
        {
            PropertyKey key;
            Marshal.ThrowExceptionForHR(_storeInterface.GetAt(index, out key));
            return key;
        }

        /// <summary>
        /// Gets property value at specified index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Property value</returns>
        public PropVariant GetValue(uint index)
        {
            PropertyKey key = _propertyKeys[index];
            PropVariant result;
            Marshal.ThrowExceptionForHR(_storeInterface.GetValue(ref key, out result));
            return result;
        }

        /// <summary>
        /// Sets property value at specified key.
        /// </summary>
        /// <param name="key">Key of property to set.</param>
        /// <param name="value">Value to write.</param>
        public void SetValue(PropertyKey key, PropVariant value)
        {
            Marshal.ThrowExceptionForHR(_storeInterface.SetValue(ref key, ref value));
        }

        /// <summary>
        /// Saves a property change.
        /// </summary>
        public void Commit()
        {
            Marshal.ThrowExceptionForHR(_storeInterface.Commit());
        }

        /// <summary>
        /// Creates a new property store
        /// </summary>
        /// <param name="store">IPropertyStore COM interface</param>
        internal PropertyStore(IPropertyStore store)
        {
            _storeInterface = store;

            int count = Count;
            for (uint i = 0; i < count; i++)
                _propertyKeys.Add(i, Get(i));
        }
    }
}
