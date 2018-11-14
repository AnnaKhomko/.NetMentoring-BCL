
namespace FileSystemDistributor.Models
{
	/// <summary>
	/// Class contains rule data.
	/// </summary>
	public class RuleModel
	{
		/// <summary>
		/// Gets or sets the file name template.
		/// </summary>
		/// <value>
		/// The file name template.
		/// </value>
		public string FileNameTemplate { get; set; }

		/// <summary>
		/// Gets or sets the destination directory path.
		/// </summary>
		/// <value>
		/// The destination directory path.
		/// </value>
		public string DestinationDirectoryPath { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is order required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is order required; otherwise, <c>false</c>.
		/// </value>
		public bool IsOrderRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is date required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is date required; otherwise, <c>false</c>.
		/// </value>
		public bool IsDateRequired { get; set; }
	}
}
