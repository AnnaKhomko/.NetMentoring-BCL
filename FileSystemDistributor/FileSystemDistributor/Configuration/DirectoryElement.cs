using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDistributor.Configuration
{
    public class DirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = true)]
        public string DirectoryPath
        {
            get
            {
                return (string)this["path"];
            }
        }
    }
}
