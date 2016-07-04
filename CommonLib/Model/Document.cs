using System.IO;

namespace CommonLib.Interfaces
{
    /// <summary>
    /// The base document class in the MVC framework
    /// </summary>
    public abstract class Document
    {
        /// <summary>
        /// the path of this file in the directory structure
        /// TODO: this is kind of weird in compressed document managers
        /// </summary>
        public string Path;
    }
}
