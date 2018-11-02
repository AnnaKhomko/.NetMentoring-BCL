using FileSystemDistributor.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDistributor.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class ConfigModel
	{
		public ConfigModel()
		{
			this.Directories = new List<DirectoryElement>();
			this.Rules = new List<RuleElement>();
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
		public List<DirectoryElement> Directories { get; set; }

		/// <summary>
		/// Gets or sets the rules.
		/// </summary>
		/// <value>
		/// The rules.
		/// </value>
		public List<RuleElement> Rules { get; set; }
	}
}
