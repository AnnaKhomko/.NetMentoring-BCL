using System.Configuration;
using System.Globalization;

namespace FileSystemDistributor.Configuration
{
    /// <summary>
    /// Class describes configuration section elements from App.config file
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationSection" />
    public class FileSystemDistributorSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        [ConfigurationProperty("culture", DefaultValue = "en-US")]
        public CultureInfo Culture
        {
            get
            {
                return (CultureInfo)this["culture"];
            }
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <value>
        /// The directories.
        /// </value>
        [ConfigurationProperty("directories")]
        [ConfigurationCollection(typeof(DirectoryElement), AddItemName = "directory")]
        public DirectoryElementCollection Directories
        {
            get
            {
                return (DirectoryElementCollection)this["directories"];
            }
        }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        [ConfigurationProperty("rules")]
        [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
        public RuleElementCollection Rules
        {
            get
            {
                return (RuleElementCollection)this["rules"];
            }
        }
    }
}
