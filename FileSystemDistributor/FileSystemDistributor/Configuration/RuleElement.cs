using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDistributor.Configuration
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("fileNameTemplate", IsKey = true)]
        public string FileNameTemplate
        {
            get
            {
                return (string)this["fileNameTemplate"];
            }
        }

        [ConfigurationProperty("destinationDirectoryPath")]
        public string DestinationDirectoryPath
        {
            get
            {
                return (string)this["destinationDirectoryPath"];
            }
        }

        [ConfigurationProperty("isOrderRequired")]
        public bool IsOrderRequired
        {
            get
            {
                return (bool)this["isOrderRequired"];
            }
        }

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
