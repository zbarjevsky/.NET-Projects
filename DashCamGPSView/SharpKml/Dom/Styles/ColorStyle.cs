﻿// Copyright (c) Samuel Cragg.
//
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.

namespace SharpKml.Dom
{
    using SharpKml.Base;

    /// <summary>
    /// Provides elements for specifying the color and color mode
    /// of style types that derive from it.
    /// </summary>
    /// <remarks>OGC KML 2.2 Section 12.7.</remarks>
    public abstract class ColorStyle : SubStyle
    {
        /// <summary>
        /// The default value that should be used for <see cref="Color"/>.
        /// </summary>
        public static readonly Color32 DefaultColor = new Color32(255, 255, 255, 255);

        /// <summary>
        /// Gets or sets the color of the graphic element.
        /// </summary>
        [KmlElement("color", 1)]
        public Color32? Color { get; set; }

        /// <summary>
        /// Gets or sets the color mode of the graphic element.
        /// </summary>
        [KmlElement("colorMode", 2)]
        public ColorMode? ColorMode { get; set; }
    }
}
