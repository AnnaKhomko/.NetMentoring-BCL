using System;
using System.Collections.Generic;
using System.Configuration;

namespace FileSystemDistributor.Configuration
{
    public class DirectoryElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryElement)element).DirectoryPath;
        }

        internal IEnumerable<DirectoryElement> ToList()
        {
            throw new NotImplementedException();
        }
    }
}
