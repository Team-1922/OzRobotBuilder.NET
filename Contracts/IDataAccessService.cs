using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// This is a holding class for a data-access class
    /// </summary>
    public interface IDataAccessService
    {
        /// <summary>
        /// The instace of the data to access
        /// </summary>
        IHierarchialAccess DataInstance { get; set; }
        /// <summary>
        /// This throws an exception if the given path is not valid
        /// </summary>
        /// <param name="path">the path to check</param>
        void AssertPath(string path);
        /// <summary>
        /// Whether or not exceptions should be thrown when improperly setting a value
        /// </summary>
        bool ThrowsExceptions { get; set; }
        /// <summary>
        /// Whether clampable values clamp
        /// </summary>
        bool ClampingValues { get; set; }
    }
}
