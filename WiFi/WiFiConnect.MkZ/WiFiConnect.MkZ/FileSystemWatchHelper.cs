using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WiFiConnect.MkZ
{
    public class FileSystemWatchHelper : IDisposable
    {
        private FileSystemWatcher _watcher { get; } = new FileSystemWatcher();
        private ILog Log { get; }

        public FileSystemWatchHelper(ILog log, string path, string filter = "*.*")
        {
            Log = log;

            if (!Directory.Exists(path))
                throw new FileNotFoundException(path);

            DelayedDeleteFolder(path, 0);

            _watcher.Path = path;
            _watcher.Filter = filter;
            _watcher.IncludeSubdirectories = true;
            // Watch for all changes specified in the NotifyFilters  
            //enumeration.  
            _watcher.NotifyFilter = NotifyFilters.Attributes |
                                    NotifyFilters.CreationTime |
                                    NotifyFilters.DirectoryName |
                                    NotifyFilters.FileName |
                                    NotifyFilters.LastAccess |
                                    NotifyFilters.LastWrite |
                                    NotifyFilters.Security |
                                    NotifyFilters.Size;

            SubscribeToEvents(true);

            _watcher.EnableRaisingEvents = true;
        }

        private void SubscribeToEvents(bool bSubscribe)
        {
            if(bSubscribe)
            {
                _watcher.Changed += watcher_Changed;
                _watcher.Created += watcher_Created;
                _watcher.Deleted += watcher_Deleted;
                _watcher.Renamed += watcher_Renamed;
                _watcher.Error += watcher_Error;
            }
            else
            {
                _watcher.Changed -= watcher_Changed;
                _watcher.Created -= watcher_Created;
                _watcher.Deleted -= watcher_Deleted;
                _watcher.Renamed -= watcher_Renamed;
                _watcher.Error   -= watcher_Error;
            }
        }

        private void watcher_Error(object sender, ErrorEventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            Debug.WriteLine(time + " - File error: " + e.GetException().Message);
        }

        private void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            Debug.WriteLine(time + " - File renamed: " + e.FullPath);
        }

        private void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            Debug.WriteLine(time + " - File deleted: " + e.FullPath);
        }

        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            Debug.WriteLine(time + " - File created: " + e.FullPath);
            DelayedDeleteFolder(@"c:\Windows\System32\drivers\CrowdStrike\", 9000);
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            Debug.WriteLine(time + " - File Changed: " + e.FullPath);
        }

        private volatile bool _isInDelete = false;
        private void DelayedDeleteFolder(string path, int delay = 5000)
        {
            if (_isInDelete)
                return;
            _isInDelete = true;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(delay);
                if (Directory.Exists(path))
                {
                    try
                    {
                        Directory.Delete(path, true);

                        //string time = DateTime.Now.ToString("HH:mm:ss.fff");
                        Log.Log("*** File deleted: " + path);
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine("Cannot delete folder: {0}, Error: {1}", path, err.Message);
                    }
                }
                _isInDelete = false;
            });
        }

        public void Dispose()
        {
            SubscribeToEvents(false);
        }
    }
}
