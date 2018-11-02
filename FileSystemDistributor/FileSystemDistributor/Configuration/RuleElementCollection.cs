using System.Configuration;

namespace FileSystemDistributor.Configuration
{
    public class RuleElementCollection : ConfigurationElementCollection
    {

        [ConfigurationProperty("defaultFolder")]
        public string DefaultFolder
        {
            get
            {
                return (string)this["defaultFolder"];
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).FileNameTemplate;
        }
    }
}
