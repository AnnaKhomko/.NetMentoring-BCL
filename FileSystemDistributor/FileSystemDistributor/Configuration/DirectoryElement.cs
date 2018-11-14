using System.Configuration;

namespace FileSystemDistributor.Configuration
{
	/// <summary>
	///  Class describes directory element from App.config file
	/// </summary>
	/// <seealso cref="System.Configuration.ConfigurationElement" />
	public class DirectoryElement : ConfigurationElement
    {
		/// <summary>
		/// Gets the directory path.
		/// </summary>
		/// <value>
		/// The directory path.
		/// </value>
		[ConfigurationProperty("path", IsKey = true)]
        public string DirectoryPath
        {
            get
            {
                return (string)base["path"];
            }
        }
    }
}
