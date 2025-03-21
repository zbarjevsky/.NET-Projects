﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Media.Device
{
    /// <summary>
    /// PROPERTYKEY is defined in wtypes.h
    /// </summary>
    public struct PropertyKey
    {
        /// <summary>
        /// Format ID
        /// </summary>
        public Guid formatId;
        /// <summary>
        /// Property ID
        /// </summary>
        public int propertyId;
        /// <summary>
        /// <param name="formatId"></param>
        /// <param name="propertyId"></param>
        /// </summary>
        public PropertyKey(Guid formatId, int propertyId)
        {
            this.formatId = formatId;
            this.propertyId = propertyId;
        }
    }
}
