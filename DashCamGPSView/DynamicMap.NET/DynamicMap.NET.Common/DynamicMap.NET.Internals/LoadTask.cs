﻿
using System;
using System.Collections.Generic;
namespace DynamicMap.NET.Internals
{
    /// <summary>
    /// tile load task
    /// </summary>
    public struct LoadTask : IEquatable<LoadTask>
    {
        public DynPoint Pos;
        public int Zoom;

        internal Core Core;

        public LoadTask(DynPoint pos, int zoom, Core core = null)
        {
            Pos = pos;
            Zoom = zoom;
            Core = core;
        }

        public override string ToString()
        {
            return Zoom + " - " + Pos.ToString();
        }

        #region IEquatable<LoadTask> Members

        public bool Equals(LoadTask other)
        {
            return (Zoom == other.Zoom && Pos == other.Pos);
        }

        #endregion
    }

    internal class LoadTaskComparer : IEqualityComparer<LoadTask>
    {
        public bool Equals(LoadTask x, LoadTask y)
        {
            return x.Zoom == y.Zoom && x.Pos == y.Pos;
        }

        public int GetHashCode(LoadTask obj)
        {
            return obj.Zoom ^ obj.Pos.GetHashCode();
        }
    }
}
