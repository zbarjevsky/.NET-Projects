using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace MeditationStopWatch
{
	internal class CacheEventArgs : EventArgs
	{
		public volatile int Percent;
		public volatile string Status;

		public CacheEventArgs(int percent, string status)
		{
			Percent = percent;
			Status = status;
		}
	}

	internal class ImageCache
	{
		public Image image = null;
		public volatile bool delivered = false;
		public volatile bool loaded = false;

		public ImageCache(Image img)
		{
			image = img;
		}
	}

	internal class ThumbnailCache
	{
		private IList<ImageCache> m_listCache = new List<ImageCache>();
		ImageList m_imageList;
		private IList<FileInfo> AllImages;
		private Thread m_LoadingThread = null;

		private int xThumb, yThumb;
		private Image m_placeHolder;

		public ThumbnailCache(ImageList imageList)
		{
			m_imageList = imageList;
			m_imageList.Images.Clear();

			xThumb = m_imageList.ImageSize.Width;
			yThumb = m_imageList.ImageSize.Height;
			m_placeHolder = new Bitmap(xThumb, yThumb);
		}

		public event EventHandler<CacheEventArgs> ProgressChanged;
		public volatile bool CancelLoadingThumbnails;

		public void InitCache(IList<FileInfo> allImages)
		{
			AllImages = allImages;
			LoadThumbnailImages();
		}

		public int GetImageIdx(int idx)
		{
			if (!LoadThumbnailImage(idx))
				return -1;

			return idx;
		}

		public void Clear()
		{
			CancelLoadingThumbnails = true;
			if (m_LoadingThread != null)
				m_LoadingThread.Join();

			m_listCache.Clear();
		}

		private void LoadThumbnailImages()
		{
			Clear();

			CancelLoadingThumbnails = false;

			for (int i = 0; i < AllImages.Count; i++)
			{
				if (CancelLoadingThumbnails)
					break;

				if (i % 10 == 0)
				{
					OnProgressChanged(i * 100 / AllImages.Count, "Loading placeholders...");
					System.Diagnostics.Trace.WriteLine("Loaded placeholders: " + i);
				}

				m_listCache.Add(new ImageCache(m_placeHolder));
				m_imageList.Images.Add(m_placeHolder);
			}

			m_LoadingThread = new Thread(new ThreadStart(delegate()
			{
			    Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;

				for (int i = 0; i < AllImages.Count; i++)
				{
					if (CancelLoadingThumbnails)
						break;

					LoadThumbnailImage(i);

					if (i % 10 == 0)
					{
						OnProgressChanged(i * 100 / AllImages.Count, "Loading thumbs...");
						System.Diagnostics.Trace.WriteLine("Loaded thumbs: " + i);
					}
				}
				OnProgressChanged(0, "Ready");
				System.Diagnostics.Trace.WriteLine("Loaded thumbs: done");
			}));
			m_LoadingThread.Start();
		}

		private bool LoadThumbnailImage(int idx)
		{
			if (idx < 0 || idx >= m_imageList.Images.Count || idx>= m_listCache.Count)
				return false;

			if (m_listCache[idx].loaded)
				return true;

			lock (m_listCache)
			{
				Image img = Image.FromFile(AllImages[idx].FullName);

				Image thumb = img.GetThumbnailImage(xThumb, yThumb, null, IntPtr.Zero);
				img.Dispose();

				if (idx < 0 || idx >= m_imageList.Images.Count || idx >= m_listCache.Count)
					return false;

				if (m_listCache[idx].loaded)
					return true;

				m_listCache[idx].image = thumb;
				m_imageList.Images[idx] = thumb;
				m_listCache[idx].loaded = true;
			}

			return true;
		}

		private void OnProgressChanged(int percent, string status)
		{
			if (ProgressChanged != null) 
				ProgressChanged(this, new CacheEventArgs(percent, status));
			Thread.Sleep(0);
		}
	}
}
