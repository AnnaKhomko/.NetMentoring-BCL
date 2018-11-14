using System.Collections.Generic;

namespace FileSystemDistributor.Models
{
	/// <summary>
	/// Class contains config data
	/// </summary>
	public class ConfigModel
	{
		public ConfigModel()
		{
			this.Directories = new List<DirectoryModel>();
			this.Rules = new List<RuleModel>();
		}

		/// <summary>
		/// Gets or sets the defaul folder.
		/// </summary>
		/// <value>
		/// The defaul folder.
		/// </value>
		public string DefaulFolder { get; set; }

		/// <summary>
		/// Gets or sets the directories.
		/// </summary>
		/// <value>
		/// The directories.
		/// </value>
		public List<DirectoryModel> Directories { get; set; }

		/// <summary>
		/// Gets or sets the rules.
		/// </summary>
		/// <value>
		/// The rules.
		/// </value>
		public List<RuleModel> Rules { get; set; }
	}
}
