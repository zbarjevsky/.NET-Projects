﻿// Copyright (c) 2007-2011 SlimDX Group
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Runtime.InteropServices;

namespace SlimDX.Generator
{
	class ParameterModel
	{
		#region Interface

		public ParameterModel(string name, TypeModel type, int indirectionLevel, ParameterModelFlags flags, string length)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("Value may not be null or empty.", "name");
			if (type == null)
				throw new ArgumentNullException("type");

			Name = name;
			Type = type;
			Flags = flags;
			IndirectionLevel = indirectionLevel;
			SizeParameter = length;
		}

		public string Name
		{
			get;
			private set;
		}

		public TypeModel Type
		{
			get;
			private set;
		}

		public ParameterModelFlags Flags
		{
			get;
			private set;
		}

		public int IndirectionLevel
		{
			get;
			private set;
		}

		public string SizeParameter
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}

		#endregion
	}
}
