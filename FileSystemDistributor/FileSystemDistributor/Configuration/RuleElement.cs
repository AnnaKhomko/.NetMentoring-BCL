using System.Configuration;

namespace FileSystemDistributor.Configuration
{
	/// <summary>
	///  Class describes rule element from App.config file
	/// </summary>
	/// <seealso cref="System.Configuration.ConfigurationElement" />
	public class RuleElement : ConfigurationElement
    {
		/// <summary>
		/// Gets the file name template.
		/// </summary>
		/// <value>
		/// The file name template.
		/// </value>
		[ConfigurationProperty("fileNameTemplate", IsKey = true)]
        public string FileNameTemplate
        {
            get
            {
                return (string)this["fileNameTemplate"];
            }
        }

		/// <summary>
		/// Gets the destination directory path.
		/// </summary>
		/// <value>
		/// The destination directory path.
		/// </value>
		[ConfigurationProperty("destinationDirectoryPath")]
        public string DestinationDirectoryPath
        {
            get
            {
                return (string)this["destinationDirectoryPath"];
            }
        }

		/// <summary>
		/// Gets a value indicating whether this instance is order required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is order required; otherwise, <c>false</c>.
		/// </value>
		[ConfigurationProperty("isOrderRequired")]
        public bool IsOrderRequired
        {
            get
            {
                return (bool)this["isOrderRequired"];
            }
        }

		/// <summary>
		/// Gets a value indicating whether this instance is date required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is date required; otherwise, <c>false</c>.
		/// </value>
		[ConfigurationProperty("isDateRequired")]
        public bool IsDateRequired
        {
            get
            {
                return (bool)this["isDateRequired"];
            }
        }
    }
}
