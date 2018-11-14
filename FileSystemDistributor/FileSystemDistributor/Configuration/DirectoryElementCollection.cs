using System.Configuration;

namespace FileSystemDistributor.Configuration
{
	/// <summary>
	///  Class describes Directory element collection
	/// </summary>
	/// <seealso cref="System.Configuration.ConfigurationElementCollection" />
	public class DirectoryElementCollection : ConfigurationElementCollection
    {
		/// <summary>
		/// Creates new element.
		/// </summary>
		/// <returns>
		/// A newly created Configuration element />.
		/// </returns>
		protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryElement();
        }

		/// <summary>
		/// Gets the element key for a specified configuration element when overridden in a derived class.
		/// </summary>
		/// <param name="element">Configuration Element to return the key for.</param>
		/// <returns>
		/// An object that acts as the key for the specified Configuration element  />.
		/// </returns>
		protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryElement)element).DirectoryPath;
        }

    }
}
