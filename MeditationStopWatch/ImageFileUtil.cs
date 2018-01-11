using System;
using System.IO;
using System.Collections.Generic;

namespace MeditationStopWatch
{
	public class ImageFileUtil
	{
		public FileInfo ImageInfo { get; set; }
		public IList<FileInfo> AllImages { get; set; }

		public void OpenImageDirectory(string sFileName)
		{
			FileInfo image = new FileInfo(sFileName);

			AllImages = new List<FileInfo>(image.Directory.GetFiles("*.jpg"));
			int idx = IndexOf(image);
			if (idx >= 0) ImageInfo = AllImages[idx];
			else ImageInfo = image;
		}
		
		public int IndexOf(FileInfo image)
		{
			if (AllImages == null) return -1;

			//find this info in list
			for (int i = 0; i < AllImages.Count; i++)
			{
				if (AllImages[i].FullName == image.FullName)
					return i;
			}
			return -1;
		}

		public FileInfo Next()
		{
			if (AllImages != null && AllImages.Count > 0)
			{
				int idx = IndexOf(ImageInfo);
				idx++; if (idx >= AllImages.Count) idx = 0;
				ImageInfo = AllImages[idx];
			}
			return ImageInfo;
		}

		public FileInfo Prev()
		{
			if (AllImages != null && AllImages.Count > 0)
			{
				int idx = IndexOf(ImageInfo);
				idx--; if (idx < 0) idx = AllImages.Count - 1;
				ImageInfo = AllImages[idx];
			}
			return ImageInfo;

		}
	}
}
