using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ClipboardManager
{
	//http://www.codeproject.com/csharp/VmEasyZipUnZip.asp
	//public class JavaZip : IDisposable
	//{
	//    private java.io.FileOutputStream		_fos = null;
	//    private java.util.zip.ZipOutputStream	_zos = null;

	//    public JavaZip(string sArchivePath)
	//    {
	//        try
	//        {
	//            // Output stream 
	//            _fos = new java.io.FileOutputStream(sArchivePath);
	//            // Tie to zip stream 
	//            _zos = new java.util.zip.ZipOutputStream(_fos);
	//        }//end try
	//        catch ( Exception err )
	//        {
	//            FormClipboard.TraceLn(true, "JavaZip", "C-tor", "Error: {0}", err.Message);
	//            throw new Exception("JavaZip::Error", err);
	//        }//end catch
	//    }//end constructor

	//    ~JavaZip()
	//    {
	//        Close();
	//    }//end destructor

	//    public void AddFile(string sFileName)
	//    {
	//        // Stream with source file 
	//        java.io.FileInputStream fis = new java.io.FileInputStream(sFileName);
	//        // It's our entry in zip 
	//        java.util.zip.ZipEntry ze = new java.util.zip.ZipEntry(Path.GetFileName(sFileName));

	//        _zos.putNextEntry(ze);
			
	//        StreamUtil.CopyStream(fis, _zos);

	//        fis.close();
	//        _zos.closeEntry();
	//    }//end AddFile

	//    public void Close()
	//    {
	//        try
	//        {
	//            if (_zos != null)
	//                _zos.close();

	//            if (_fos != null)
	//                _fos.close();
	//        }//end try
	//        catch (Exception err)
	//        {
	//            FormClipboard.TraceLn(true, "JavaZip", "Close", "Exception: {0}", err.Message);
	//        }//end catch
            
	//        _zos = null;
	//        _fos = null;
	//    }//end Close

	//    #region IDisposable Members

	//    public void Dispose()
	//    {
	//        Close();
	//    }//end Dispose

	//    #endregion
	//}//end class JavaZip

	//public class JavaUnZip : IDisposable
	//{
	//    private java.io.FileInputStream			_fis = null;
	//    private java.util.zip.ZipInputStream	_zis = null;

	//    public JavaUnZip(string sArchivePath)
	//    {
	//        try
	//        {
	//            // Input stream 
	//            _fis = new java.io.FileInputStream(sArchivePath);
	//            // Tie to zip stream 
	//            _zis = new java.util.zip.ZipInputStream(_fis);
	//        }//end try
	//        catch ( Exception err )
	//        {
	//            FormClipboard.TraceLn(true, "JavaUnZip", "C-tor", "Error: {0}", err.Message);
	//            throw new Exception("JavaUnZip::Error", err);
	//        }//end catch
	//    }//end constructor

	//    ~JavaUnZip()
	//    {
	//        Close();
	//    }//end destructor

	//    public void ExtractFiles(string sOutFolder)
	//    {
	//        if ( _fis == null )
	//            return;

	//        java.util.zip.ZipEntry ze;
	//        while ( _zis.available() > 0 && (ze = _zis.getNextEntry()) != null )
	//        {
	//            if ( !ze.isDirectory() )
	//            {
	//                ExtractFile(sOutFolder, ze);
	//            }//end if
	//        }//end while
	//        Close();
	//    }//end ExtractFiles

	//    private void ExtractFile(string sOutFolder, java.util.zip.ZipEntry ze)
	//    {
	//        java.io.FileOutputStream fos = new java.io.FileOutputStream(Path.Combine(sOutFolder, ze.getName()));
	//        StreamUtil.CopyStream(_zis, fos);
	//        fos.flush();
	//        fos.close();
	//    }//end ExtractFile

	//    private void Close()
	//    {
	//        try
	//        {
	//            if (_zis != null)
	//                _zis.close();

	//            if (_fis != null)
	//                _fis.close();
	//        }//end try
	//        catch (Exception err)
	//        {
	//            FormClipboard.TraceLn(true, "JavaUnZip", "Close", "Exception: {0}", err.Message);
	//        }//end catch

	//        _zis = null;
	//        _fis = null;
	//    }//end Close

	//    #region IDisposable Members

	//    public void Dispose()
	//    {
	//        Close();
	//    }//end Dispose

	//    #endregion
	//}//end class JavaUnZip

	//class StreamUtil
	//{
	//    public static void CopyStream(java.io.InputStream source, java.io.OutputStream destination)
	//    {
	//        sbyte[] buffer = new sbyte[4096];
	//        int countBytesRead;
	//        while ( source.available() > 0 && (countBytesRead = source.read(buffer, 0, buffer.Length)) > 0 )
	//        {
	//            destination.write(buffer, 0, countBytesRead);
	//        }//end while
	//    }//end CopyStream
	//}//end class StreamUtil
}//end namespace ClipboardListener
