using FileSystemDistributor.Configuration;
using FileSystemDistributor.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Strings = FileSystemDistributor.Resources.Strings;
using FileSystemDistributor.EventArgs;
using FileSystemDistributor.Models;
using FileSystemDistributor.Utils;

namespace FileSystemDistributor
{
	public class FileSystemWatcherService
	{
		private List<FileSystemWatcher> fileSystemWatchers;
		private ILogger log;

		/// <summary>
		/// Occurs when [on file created].
		/// </summary>
		public event EventHandler<FileCreatedEventArgs> OnFileCreated;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemWatcherService"/> class.
		/// </summary>
		public FileSystemWatcherService()
		{
			this.log = new Logger();
			this.fileSystemWatchers = new List<FileSystemWatcher>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemWatcherService"/> class.
		/// </summary>
		/// <param name="log">The log.</param>
		public FileSystemWatcherService(ILogger log)
		{
			this.log = log;
			this.fileSystemWatchers = new List<FileSystemWatcher>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemWatcherService"/> class.
		/// </summary>
		/// <param name="watchers">The watchers.</param>
		public FileSystemWatcherService(List<FileSystemWatcher> watchers)
		{
			this.fileSystemWatchers = watchers;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemWatcherService"/> class.
		/// </summary>
		/// <param name="watchers">The watchers.</param>
		public FileSystemWatcherService(List<FileSystemWatcher> watchers, ILogger log)
		{
			this.log = log;
			this.fileSystemWatchers = watchers;
		}

		/// <summary>
		/// Initializes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		public void Init(FileSystemWatcherConfigurationModel context)
		{
			if (context != null && context.Directories != null)
			{
				foreach (var directory in context.Directories)
				{
					var watcher = new FileSystemWatcher(directory)
					{
						NotifyFilter = context.NotifyFilter,
						EnableRaisingEvents = context.EnableRaisingEvents,
						IncludeSubdirectories = context.IncludeSubdirectories
					};

					watcher.Created += (s, args) => { this.OnCreated(args.Name, args.FullPath); };
					fileSystemWatchers.Add(watcher);
				}
			}
		}

		/// <summary>
		/// Called when [created].
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="path">The path.</param>
		private void OnCreated(string name, string path)
		{
			log.Log(string.Format(Strings.FileFound, name, File.GetCreationTime(path)));

			var temp = this.OnFileCreated;

			if (temp != null)
			{
				this.OnFileCreated(this, new FileCreatedEventArgs { Name = name, FilePath = path });
			}
		}
	}
}
