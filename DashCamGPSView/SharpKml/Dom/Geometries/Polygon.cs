﻿// Copyright (c) Samuel Cragg.
//
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.

namespace SharpKml.Dom
{
    using System.Collections.Generic;
    using System.Linq;
    using SharpKml.Base;

    /// <summary>
    /// Represents a polygon with inner and outer boundaries.
    /// </summary>
    /// <remarks>OGC KML 2.2 Section 10.8.</remarks>
    [KmlElement("Polygon")]
    public class Polygon : Geometry, IBoundsInformation
    {
        private readonly List<InnerBoundary> innerBoundaries = new List<InnerBoundary>();
        private OuterBoundary outer;

        /// <summary>
        /// Gets or sets how the altitude value should be interpreted.
        /// </summary>
        [KmlElement("altitudeMode", 3)]
        public AltitudeMode? AltitudeMode { get; set; }

        /// <summary>
        /// Gets or sets whether to connect a geometry to the ground.
        /// </summary>
        /// <remarks>
        /// Only the outer boundary should be extruded. The geometry is extruded
        /// toward the Earth's center of mass. To extrude a geometry, the
        /// <see cref="AltitudeMode"/> shall be either
        /// <see cref="Dom.AltitudeMode.RelativeToGround"/> or
        /// <see cref="Dom.AltitudeMode.Absolute"/>, and the altitude component
        /// should be greater than 0 (that is, in the air).
        /// </remarks>
        [KmlElement("extrude", 1)]
        public bool? Extrude { get; set; }

        /// <summary>
        /// Gets or sets extended altitude mode information.
        /// [Google Extension].
        /// </summary>
        [KmlElement("altitudeMode", KmlNamespaces.GX22Namespace, 5)]
        public GX.AltitudeMode? GXAltitudeMode { get; set; }

        /// <summary>
        /// Gets a collection of <see cref="InnerBoundary"/> elements.
        /// </summary>
        /// <remarks>It is advised that the rings not cross each other.</remarks>
        [KmlElement(null, 5)]
        public IReadOnlyCollection<InnerBoundary> InnerBoundary => this.innerBoundaries;

        /// <summary>
        /// Gets or sets the exterior boundary.
        /// </summary>
        [KmlElement(null, 4)]
        public OuterBoundary OuterBoundary
        {
            get => this.outer;
            set => this.UpdatePropertyChild(value, ref this.outer);
        }

        /// <summary>
        /// Gets or sets whether to drape a geometry over the terrain.
        /// </summary>
        /// <remarks>
        /// To enable tessellation, the value should be set to true and
        /// <see cref="AltitudeMode"/> shall be <see cref="Dom.AltitudeMode.ClampToGround"/>.
        /// </remarks>
        [KmlElement("tessellate", 2)]
        public bool? Tessellate { get; set; }

        /// <summary>
        /// Gets the coordinates of the bounds of this instance.
        /// </summary>
        IEnumerable<Vector> IBoundsInformation.Coordinates
        {
            get
            {
                if ((this.OuterBoundary != null) && this.OuterBoundary.LinearRing != null)
                {
                    return ((IBoundsInformation)this.OuterBoundary.LinearRing).Coordinates;
                }

                return Enumerable.Empty<Vector>();
            }
        }

        /// <summary>
        /// Adds the specified <see cref="InnerBoundary"/> to this instance.
        /// </summary>
        /// <param name="boundary">
        /// The <c>InnerBoundary</c> to add to this instance.
        /// </param>
        /// <exception cref="System.ArgumentNullException">boundary is null.</exception>
        /// <exception cref="System.InvalidOperationException">
        /// boundary belongs to another <see cref="Element"/>.
        /// </exception>
        public void AddInnerBoundary(InnerBoundary boundary)
        {
            this.AddAsChild(this.innerBoundaries, boundary);
        }
    }
}
