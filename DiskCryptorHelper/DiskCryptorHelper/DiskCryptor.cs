using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskCryptorHelper
{
    public class DiskCryptor
    {
        public const string DiskCryptorPath = @"c:\Program Files\dcrypt\dccon.exe";

        /* operation status codes */
        public enum ErrorCode
        {
            [Description("operation completed successful")]
            ST_OK = 0, //	    operation completed successful
            [Description("unknown error")]
            ST_ERROR = 1, //	    unknown error
            [Description("device not found")]
            ST_NF_DEVICE = 2, //	    device not found
            [Description("read / write error")]
            ST_RW_ERR = 3, //	    read / write error
            [Description("invalid password")]
            ST_PASS_ERR = 4, //	    invalid password
            [Description("invalid password")]
            ST_ALR_MOUNT = 5, //	    device has already mounted
            [Description("device not mounted")]
            ST_NO_MOUNT = 6, //	    device not mounted
            [Description("error on volume locking")]
            ST_LOCK_ERR = 7, //	    error on volume locking
            [Description("device is unmountable")]
            ST_UNMOUNTABLE = 8, //	    device is unmountable
            [Description("not enought memory")]
            ST_NOMEM = 9, //	    not enought memory
            [Description("error on creating system thread")]
            ST_ERR_THREAD = 10, //	error on creating system thread
            [Description("invalid data wipe mode")]
            ST_INV_WIPE_MODE = 11, //	invalid data wipe mode
            [Description("invalid data size")]
            ST_INV_DATA_SIZE = 12, //	invalid data size
            [Description("access denied")]
            ST_ACCESS_DENIED = 13, //	access denied
            [Description("file not found")]
            ST_NF_FILE = 14, //	file not found
            [Description("disk I/O error")]
            ST_IO_ERROR = 15, //	disk I/O error
            [Description("unsupported file system")]
            ST_UNK_FS = 16, //	unsupported file system
            [Description("invalid FS bootsector, please format partition")]
            ST_ERR_BOOT = 17, //	invalid FS bootsector, please format partition
            [Description("MBR is corrupted")]
            ST_MBR_ERR = 18, //	MBR is corrupted
            [Description("bootloader is already installed")]
            ST_BLDR_INSTALLED = 19, //	bootloader is already installed
            [Description("not enough space after partitions to install bootloader")]
            ST_NF_SPACE = 20, //	not enough space after partitions to install bootloader
            [Description("bootloader is not installed")]
            ST_BLDR_NOTINST = 21, //	bootloader is not installed
            [Description("invalid bootloader size")]
            ST_INV_BLDR_SIZE = 22, //	invalid bootloader size
            [Description("bootloader corrupted, config not found")]
            ST_BLDR_NO_CONF = 23, //	bootloader corrupted, config not found
            [Description("old bootloader can not be configured")]
            ST_BLDR_OLD_VER = 24, //	old bootloader can not be configured
            [Description("ST_AUTORUNNED")]
            ST_AUTORUNNED = 25, //	
            [Description("ST_NEED_EXIT")]
            ST_NEED_EXIT = 26, //	
            [Description("user not have admin privilegies")]
            ST_NO_ADMIN = 27, //	user not have admin privilegies
            [Description("boot device not found")]
            ST_NF_BOOT_DEV = 28, //	boot device not found
            [Description("can not open registry key")]
            ST_REG_ERROR = 29, //	can not open registry key
            [Description("registry key not found")]
            ST_NF_REG_KEY = 30, //	registry key not found
            [Description("can not open SCM database")]
            ST_SCM_ERROR = 31, //	can not open SCM database
            [Description("encryption finished")]
            ST_FINISHED = 32, //	encryption finished
            [Description("driver already installed")]
            ST_INSTALLED = 32, //	driver already installed
            [Description("device has unsupported sector size")]
            ST_INV_SECT = 34, //	device has unsupported sector size
            [Description("shrinking error, last clusters are used")]
            ST_CLUS_USED = 35, //	shrinking error, last clusters are used
            [Description("not enough free space in partition to continue encrypting")]
            ST_NF_PT_SPACE = 36, //	not enough free space in partition to continue encrypting
            [Description("removable media changed")]
            ST_MEDIA_CHANGED = 37, //	removable media changed
            [Description("no removable media in device")]
            ST_NO_MEDIA = 38, //	no removable media in device
            [Description("device is busy")]
            ST_DEVICE_BUSY = 39, //	device is busy
            [Description("media type not supported")]
            ST_INV_MEDIA_TYPE = 40, //	media type not supported
            [Description("ST_FORMAT_NEEDED")]
            ST_FORMAT_NEEDED = 41, //	
            [Description("ST_CANCEL")]
            ST_CANCEL = 42, //	
            [Description("invalid volume version")]
            ST_INV_VOL_VER = 43, //	invalid volume version
            [Description("keyfiles not found")]
            ST_EMPTY_KEYFILES = 44, //	keyfiles not found
            [Description("this is a not backup file")]
            ST_NOT_BACKUP = 45, //	this is a not backup file
            [Description("can not open file")]
            ST_NO_OPEN_FILE = 46, //	can not open file
            [Description("can not create file")]
            ST_NO_CREATE_FILE = 47, //	can not create file
            [Description("invalid volume header")]
            ST_INV_VOLUME = 48, //	invalid volume header
            [Description("ST_OLD_VERSION")]
            ST_OLD_VERSION = 49, //	
            [Description("ST_NEW_VERSION")]
            ST_NEW_VERSION = 50, //	
            [Description("ST_ENCRYPTED")]
            ST_ENCRYPTED = 51, //	
            [Description("ST_INCOMPATIBLE")]
            ST_INCOMPATIBLE = 52, //	
            [Description("ST_LOADED")]
            ST_LOADED = 53, //	
            [Description("ST_VOLUME_TOO_NEW")]
            ST_VOLUME_TOO_NEW = 54, //	    
        }

        public class Error
        {
            public ErrorCode ErrorCode = ErrorCode.ST_OK;
            public string ErrorDesc { get { return ErrorCode.Description(); } }

            public Error(ErrorCode code = ErrorCode.ST_OK)
            {
                ErrorCode = code;
            }

            public Error(string report_line)
            {
                if (report_line.StartsWith("Error:"))
                {
                    int err;
                    if (int.TryParse(report_line.Substring(6), out err))
                    {
                        ErrorCode = ((DiskCryptor.ErrorCode)err);
                    }
                }
            }
        }

        public class DriveInfo
        {
            public string MountPoint = "";
            public string DriveLetter = "";
            public string size = "";
            public string status = "";

            public bool IsValidInfo { get; }
            public bool HasInfo { get; private set; }

            public DriveInfo(string report_line)
            {
                string[] line = report_line.Split('|');
                if (line.Length == 4)
                {
                    MountPoint = (line[0].Trim());
                    DriveLetter = (line[1].Trim());
                    size = (line[2].Trim());
                    status = (line[3].Trim());
                }

                HasInfo = line.Length == 4 || report_line.Contains("-------");
                IsValidInfo = MountPoint.StartsWith("pt");
            }

            public override string ToString()
            {
                return MountPoint + " | " + DriveLetter + " | " + status;
            }
        }

        public Error LastError = new Error();
        public List<DriveInfo> DriveList = new List<DriveInfo>(10);
        public StringBuilder Log = new StringBuilder(4096);

        public Action OnDataReceived = () => { };

        public Action<DriveInfo> OnDisksAdded = (driveInfo) => { Debug.WriteLine("Disk Added: "+driveInfo.Description()); };

        public void ExecuteEnum()
        {
            ExecuteDiskCryptorCommanLine("-enum");
        }

        public void ExecuteVersion()
        {
            ExecuteDiskCryptorCommanLine("-version");
        }

        public void ExecuteMount(DriveInfo drive, string driveLetter, string pwd)
        {
            Log.AppendLine("Mount: " + drive.MountPoint);
            ExecuteDiskCryptorCommanLine("-mount " + drive.MountPoint + " -mp " + driveLetter + " -p " + pwd);
        }

        public void ExecuteUnMount(DriveInfo drive)
        {
            if(!drive.status.StartsWith("mounted"))
            {
                Log.AppendLine("UnMount: " + drive.MountPoint + ", device is not mounted");
                return;
            }

            Log.AppendLine("UnMount: " + drive.MountPoint);
            ExecuteDiskCryptorCommanLine("-unmount " + drive.MountPoint + " -f -dp");
        }

        public void ExecuteUnMount(string driveLetter)
        {
            DriveInfo drive = FindInfoForDriveLetter(driveLetter);
            if(drive == null)
            {
                Log.AppendLine("Drive letter not found: " + driveLetter);
                return;
            }
            ExecuteUnMount(drive);
        }

        public void ExecuteMountAll(string pwd)
        {
            Log.AppendLine("Mount All...");
            ExecuteDiskCryptorCommanLine("-mountall -p " + pwd);
        }

        public void ExecuteUnMountAll()
        {
            Log.AppendLine("UnMount All...");
            ExecuteDiskCryptorCommanLine("-unmountall -f -dp");
        }

        public void ExecuteBSOD()
        {
            Log.AppendLine("BSOD!");
            ExecuteDiskCryptorCommanLine("-bsod");  //Erase all keys in memory and generate BSOD
        }

        public DriveInfo FindInfoForDriveLetter(string driveLetter)
        {
            foreach (DriveInfo info in DriveList)
            {
                if (info.DriveLetter == driveLetter)
                    return info;
            }
            return null;
        }

        private void ExecuteDiskCryptorCommanLine(string args)
        {
            if(args == "-enum") //if enum command
                DriveList.Clear();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            Process p = new Process();

            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;

            startInfo.UseShellExecute = false;
            startInfo.Arguments = args;
            startInfo.FileName = DiskCryptorPath;

            p.StartInfo = startInfo;

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += OutputDataReceived;

            p.Start();
            p.BeginOutputReadLine();
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e != null && e.Data != null)
            {
                Debug.WriteLine("Console data: "+e.Data);
                DiskCryptor.Error error = new DiskCryptor.Error(e.Data);
                if (error.ErrorCode != DiskCryptor.ErrorCode.ST_OK)
                {
                    Log.AppendLine(" (" + error.ErrorDesc + ")");
                }
                else
                {
                    DiskCryptor.DriveInfo newDrive = new DiskCryptor.DriveInfo(e.Data);
                    if (newDrive.HasInfo)
                    {
                        if (newDrive.IsValidInfo && DriveList.FirstOrDefault((drv) => drv.MountPoint == newDrive.MountPoint) == null)
                        {
                            DriveList.Add(newDrive);
                            OnDisksAdded(newDrive);
                        }
                    }
                    else
                    {
                        Log.AppendLine(e.Data);
                    }
                }
                OnDataReceived();
            }
        }
    }
}
