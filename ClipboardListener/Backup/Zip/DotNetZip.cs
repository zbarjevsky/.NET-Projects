using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Packaging;
using System.IO;
using ClipboardManager.Zip;
using System.Diagnostics;

namespace ClipboardManager.Zip
{
	public class DotNetZip : IZip
	{
		private Package _zip;

		public DotNetZip(string sZipPath, bool bOverwrite)
		{
			FileMode fileMode = bOverwrite ? FileMode.Create : FileMode.OpenOrCreate;
			_zip = ZipPackage.Open(sZipPath, fileMode, FileAccess.ReadWrite);
		}

		#region IZip Members

		public void Add(string sFileName)
		{
			Debug.Assert(_zip != null);
			AddToArchive(_zip, sFileName);
		}

		public void Close()
		{
			if (_zip != null) _zip.Close();
			_zip = null;
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			Close();
		}

		#endregion

		public static void ZipFiles(List<string> filesPath, string sZipPath, bool bOverwrite)
		{
			FileMode fileMode = bOverwrite ? FileMode.Create : FileMode.OpenOrCreate;

			//Open the zip file if it exists, else create a new one 
			using (Package zip = ZipPackage.Open(sZipPath, fileMode, FileAccess.ReadWrite))
			{
				//Add as many files as you like:
				foreach (var file in filesPath)
				{
					AddToArchive(zip, file);
				}
			}
		}//End ZipFiles

		public static void UnZipFiles(string sZipPath, string sTargetDir)
		{
			using (Package zip = ZipPackage.Open(sZipPath, FileMode.Open, FileAccess.Read))
			{
				var parts = zip.GetParts();
				foreach (ZipPackagePart part in parts)
				{
					ExtractFile(part, sTargetDir);
				}
			}
		}

		/// <summary>
		/// Method to create file at the temp folder
		/// </summary>
		/// <param name="rootFolder"></param>
		/// <param name="contentFileURI"></param>
		/// <returns></returns>
		protected static void ExtractFile(ZipPackagePart contentFile, string rootFolder)
		{
			// Initially create file under the folder specified
			string contentFilePath = string.Empty;
			contentFilePath = contentFile.Uri.OriginalString.Replace('/', Path.DirectorySeparatorChar);

			if (contentFilePath.StartsWith(Path.DirectorySeparatorChar.ToString()))
			{
				contentFilePath = contentFilePath.TrimStart(Path.DirectorySeparatorChar);
			}

			contentFilePath = Path.Combine(rootFolder, contentFilePath);

			//Check for the folder already exists. If not then create that folder
			if (!Directory.Exists(Path.GetDirectoryName(contentFilePath)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(contentFilePath));
			}

			// Create the file with the Part content
			using (FileStream fileStream = new FileStream(contentFilePath, FileMode.Create))
			{
				CopyStream(contentFile.GetStream(), fileStream);
			}// end:using(FileStream fileStream) - Close & dispose fileStream.
		}

		private static void AddToArchive(Package zip, string fileToAdd)
		{
			//Replace spaces with an underscore (_) 
			string uriFileName = fileToAdd.Replace(" ", "_");

			//A Uri always starts with a forward slash "/" 
			string zipUri = String.Concat("/", Path.GetFileName(uriFileName));

			Uri partUri = new Uri(zipUri, UriKind.Relative);
			string contentType = System.Net.Mime.MediaTypeNames.Application.Zip;

			//The PackagePart contains the information: 
			// Where to extract the file when it//s extracted (partUri) 
			// The type of content stream (MIME type):  (contentType) 
			// The type of compression:  (CompressionOption.Normal)   
			PackagePart pkgPart = zip.CreatePart(partUri, contentType, CompressionOption.NotCompressed);

			// Copy the data to the Resource Part
			using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
			{
				CopyStream(fileStream, pkgPart.GetStream());
			}// end:using(fileStream) - Close and dispose fileStream.
		}//End AddToArchive	

		/// <summary>
		/// Method to add files and folder within the zip file package
		/// </summary>
		/// <param name="parentnode"></param>
		/// <param name="zipFile"></param>
		protected void AddFileToZip(Package zip, string fileToAdd)
		{
			//Check for file existing. If file does not exists,
			//then add in the report to generate at the end of the process.
			if (!File.Exists(fileToAdd))
				return;

			Uri partURI = GetPartUri(fileToAdd);//relativePath);

			string contentType = GetContentType(fileToAdd);

			//The PackagePart contains the information: 
			// Where to extract the file when it//s extracted (partUri) 
			// The type of content stream (MIME type):  (contentType) 
			// The type of compression:  (CompressionOption.Normal)   
			PackagePart newFilePackagePart = zip.CreatePart(partURI, contentType, CompressionOption.Normal);

			// Copy the data to the Resource Part
			using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
			{
				CopyStream(fileStream, newFilePackagePart.GetStream());
			}// end:using(fileStream) - Close and dispose fileStream.
		}

		private static Uri GetPartUri(string relativePath)
		{
			// Remove the section of the path that has "root defined"
			relativePath = relativePath.Replace("./", "");

			// Incase if there is space in the file name, then this won't work.
			// So we need to remove space from the file name and replace it with "_"
			string fileName = Path.GetFileName(relativePath);

			relativePath = relativePath.Replace(fileName, fileName.Replace(" ", "_"));

			//create Packagepart that will represent file that we will add. 
			//Define URI for this file that needs to be added within the Zip file.
			//Define the added file as related to the Zip file in terms of the path.
			//This part will define the location where this file needs 
			//to be extracted while the zip file is getting exteacted.
			Uri partURI = new Uri(relativePath, UriKind.Relative);

			return partURI;
		}

		private static string GetContentType(string fileName)
		{
			string contentType = System.Net.Mime.MediaTypeNames.Application.Zip;

			//Define the Content Type of the file that we will be adding. 
			//This depends upon the file extension
			switch (Path.GetExtension(fileName).ToLower())
			{
				case (".xml"):
					contentType = System.Net.Mime.MediaTypeNames.Text.Xml;
					break;
				case (".txt"):
					contentType = System.Net.Mime.MediaTypeNames.Text.Plain;
					break;
				case (".rtf"):
					contentType = System.Net.Mime.MediaTypeNames.Application.Rtf;
					break;
				case (".gif"):
					contentType = System.Net.Mime.MediaTypeNames.Image.Gif;
					break;
				case (".jpeg"):
					contentType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
					break;
				case (".tiff"):
					contentType = System.Net.Mime.MediaTypeNames.Image.Tiff;
					break;
				case (".pdf"):
					contentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
					break;
				case (".doc"):
				case (".docx"):
				case (".ppt"):
				case (".xls"):
					contentType = System.Net.Mime.MediaTypeNames.Text.RichText;
					break;
			}
			return contentType;
		}

		//  --------------------------- CopyStream ---------------------------
		/// <summary>
		///   Copies data from a source stream to a target stream.</summary>
		/// <param name="source">
		///   The source stream to copy from.</param>
		/// <param name="target">
		///   The destination stream to copy to.</param>
		private static void CopyStream(Stream source, Stream target)
		{
			const int bufSize = 0x1000;
			byte[] buf = new byte[bufSize];
			int bytesRead = 0;
			while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
				target.Write(buf, 0, bytesRead);
		}// end:CopyStream()
	}
}
