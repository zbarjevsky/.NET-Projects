﻿// Copyright (c) Samuel Cragg.
//
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.

namespace SharpKml.Dom
{
    using System;
    using System.Collections.Generic;
    using SharpKml.Base;

    /// <summary>
    /// Specifies a mapping between two <see cref="Style"/>s using a key/value
    /// pair that maps a mode to the predefined <see cref="Feature.StyleUrl"/>.
    /// </summary>
    /// <remarks>
    /// <para>OGC KML 2.2 Section 12.3.</para>
    /// <para>A StyleMap may be used to provide separate normal and highlighted
    /// styles for a <see cref="Placemark"/>. StyleMap should contain two
    /// <see cref="Pair"/> objects, one with a <see cref="Pair.State"/> value of
    /// <see cref="StyleState.Normal"/> and the other with a value of
    /// <see cref="StyleState.Highlight"/>.</para>
    /// </remarks>
    [KmlElement("StyleMap")]
    public sealed class StyleMapCollection : StyleSelector, ICollection<Pair>, IReadOnlyCollection<Pair>
    {
        /// <summary>
        /// Gets the number of <see cref="Pair"/>s in this instance.
        /// </summary>
        public int Count => this.Pairs.Count;

        /// <summary>
        /// Gets a value indicating whether this instance is read-only.
        /// </summary>
        bool ICollection<Pair>.IsReadOnly => false;

        [KmlElement(null, 1)]
        private List<Pair> Pairs { get; } = new List<Pair>();

        /// <summary>
        /// Adds a <see cref="Pair"/> to this instance.
        /// </summary>
        /// <param name="item">The <c>Pair</c> to be added.</param>
        /// <exception cref="ArgumentNullException">item is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// item belongs to another <see cref="Element"/>.
        /// </exception>
        public void Add(Pair item)
        {
            this.AddAsChild(this.Pairs, item);
        }

        /// <summary>
        /// Removes all <see cref="Pair"/>s from this instance.
        /// </summary>
        public void Clear()
        {
            this.ResetParents(this.Pairs);
            this.Pairs.Clear();
        }

        /// <summary>
        /// Determines whether the specified value is contained in this instance.
        /// </summary>
        /// <param name="item">The value to locate.</param>
        /// <returns>
        /// true if item is found in this instance; otherwise, false. This
        /// method also returns false if the specified value parameter is null.
        /// </returns>
        public bool Contains(Pair item)
        {
            return this.Pairs.Contains(item);
        }

        /// <summary>
        /// Copies this instance to a compatible one-dimensional array, starting
        /// at the specified index of the target array.
        /// </summary>
        /// <param name="array">The destination one-dimensional array.</param>
        /// <param name="arrayIndex">
        /// The zero-based index in array at which copying begins.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The number of values contained in this instance is greater than the
        /// available space from arrayIndex to the end of the destination array.
        /// </exception>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// arrayIndex is less than 0.
        /// </exception>
        public void CopyTo(Pair[] array, int arrayIndex)
        {
            this.Pairs.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through this instance.
        /// </summary>
        /// <returns>An enumerator for this instance.</returns>
        public IEnumerator<Pair> GetEnumerator()
        {
            return this.Pairs.GetEnumerator();
        }

        /// <summary>
        /// Removes the first occurrence of a specific value from this instance.
        /// </summary>
        /// <param name="item">The value to remove.</param>
        /// <returns>
        /// true if the specified value parameter is successfully removed;
        /// otherwise, false. This method also returns false if the specified
        /// value parameter was not found or is null.
        /// </returns>
        public bool Remove(Pair item)
        {
            return this.RemoveChild(this.Pairs, item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through this instance.
        /// </summary>
        /// <returns>An enumerator for this instance.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
