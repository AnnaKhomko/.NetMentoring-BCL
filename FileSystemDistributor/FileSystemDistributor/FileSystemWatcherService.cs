using FileSystemDistributor.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDistributor
{
    public class FileSystemWatcherService
    {
        List<DirectoryElement> directories;
        List<FileSystemWatcher> fileSystemWatchers;

        public FileSystemWatcherService(List<DirectoryElement> directories)
        {
            this.directories = directories;
            MonitorDirectories();
        }

        private  void MonitorDirectories()
        {
            foreach (var directory in directories)
            {
                // Create a new FileSystemWatcher and set its properties.
                FileSystemWatcher watcher = new FileSystemWatcher(directory.DirectoryPath);
                /* Watch for changes in LastAccess and LastWrite times, and
                   the renaming of files or directories. */
                watcher.NotifyFilter = NotifyFilters.FileName;

                // Add event handlers.
                watcher.Created += new FileSystemEventHandler(OnChanged);

                // Begin watching.
                watcher.EnableRaisingEvents = true;
            }
            
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }
    }
}
