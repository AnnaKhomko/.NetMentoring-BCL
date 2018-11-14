using System.Collections.Generic;
using System.IO;

namespace FileSystemDistributor.Models
{
	/// <summary>
	/// Class contains properties for File System Watcher 
	/// </summary>
	public class FileSystemWatcherConfigurationModel
	{
		/// <summary>
		/// Gets or sets a value indicating whether [notify filter].
		/// </summary>
		/// <value>
		///   <c>true</c> if [notify filter]; otherwise, <c>false</c>.
		/// </value>
		public NotifyFilters NotifyFilter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [enable raising events].
		/// </summary>
		/// <value>
		///   <c>true</c> if [enable raising events]; otherwise, <c>false</c>.
		/// </value>
		public bool EnableRaisingEvents { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [include subdirectories].
		/// </summary>
		/// <value>
		///   <c>true</c> if [include subdirectories]; otherwise, <c>false</c>.
		/// </value>
		public bool IncludeSubdirectories { get; set; }

		/// <summary>
		/// Gets or sets the directories.
		/// </summary>
		/// <value>
		/// The directories.
		/// </value>
		public List<string> Directories { get; set; }
	}
}