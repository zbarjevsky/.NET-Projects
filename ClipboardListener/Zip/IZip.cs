using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClipboardManager.Zip
{
	public interface IZip : IDisposable
	{
		void Add(string sFileName);
		void Close();
	}
}
