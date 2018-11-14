
namespace FileSystemDistributor.EventArgs
{
	/// <summary>
	/// Class describes arguments to be used for events
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class FileCreatedEventArgs : System.EventArgs
    {
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the file path.
		/// </summary>
		/// <value>
		/// The file path.
		/// </value>
		public string FilePath { get; set; }
    }
}
