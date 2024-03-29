﻿// Copyright (c) Samuel Cragg.
//
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.

namespace SharpKml.Dom
{
    using System;
    using SharpKml.Base;

    /// <summary>
    /// This should be the root element of a KML document instance.
    /// </summary>
    [KmlElement("kml")]
    public sealed class Kml : Element
    {
        private Feature feature;
        private NetworkLinkControl link;

        /// <summary>
        /// Gets or sets the associated <see cref="Feature"/> of this instance.
        /// </summary>
        [KmlElement(null, 2)]
        public Feature Feature
        {
            get => this.feature;
            set => this.UpdatePropertyChild(value, ref this.feature);
        }

        /// <summary>
        /// Gets or sets information on how to process the KML document instance.
        /// </summary>
        [KmlAttribute("hint")]
        public string Hint { get; set; }

        /// <summary>
        /// Gets or sets the associated <see cref="NetworkLinkControl"/>
        /// of this instance.
        /// </summary>
        [KmlElement(null, 1)]
        public NetworkLinkControl NetworkLinkControl
        {
            get => this.link;
            set => this.UpdatePropertyChild(value, ref this.link);
        }

        /// <summary>
        /// Registers the specified prefix with the specified namespace.
        /// </summary>
        /// <param name="prefix">
        /// The prefix to associate with the namespace being added.
        /// </param>
        /// <param name="ns">The namespace to add.</param>
        /// <exception cref="ArgumentException">
        /// The value for prefix is an empty string, "xml" or "xmlns".
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// prefix/uri is null.
        /// </exception>
        public void AddNamespacePrefix(string prefix, string ns)
        {
            Check.IsNotNull(prefix, nameof(prefix));
            Check.IsNotNull(ns, nameof(ns));

            if ((prefix.Length == 0) ||
                (prefix == "xml") ||
                (prefix == "xmlns"))
            {
                throw new ArgumentException("Invalid prefix.", nameof(prefix));
            }

            this.AddNamespace(prefix, ns);
        }
    }
}
