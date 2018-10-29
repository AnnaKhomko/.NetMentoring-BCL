using System.Configuration;
using System.Globalization;
using System.IO;

namespace FileSystemDistributor.Configuration
{
    public class FileSystemDistributorSection : ConfigurationSection
    {
        [ConfigurationProperty("culture", DefaultValue = "en-US")]
        public CultureInfo Culture
        {
            get
            {
                return (CultureInfo)this["culture"];
            }
        }

        [ConfigurationProperty("directories")]
        [ConfigurationCollection(typeof(DirectoryElement), AddItemName = "directory")]
        public DirectoryElementCollection Directories
        {
            get
            {
                return (DirectoryElementCollection)this["directories"];
            }
        }

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
