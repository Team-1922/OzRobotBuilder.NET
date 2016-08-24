using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The top-level hierarchial access instance; this provies asynchronous access to the view-model tree
    /// </summary>
    public interface IHierarchialAccessRoot
    {
        /// <summary>
        /// Retrieves the value at the given path
        /// </summary>
        /// <param name="path">the path to look for the value at</param>
        /// <returns>the value at <paramref name="path"/></returns>
        Task<string> GetAsync(string path);
        /// <summary>
        /// Sets the value at the given path
        /// </summary>
        /// <param name="path">where to set <paramref name="value"/> at</param>
        /// <param name="value">the value to set at location <paramref name="path"/></param>
        /// <returns></returns>
        Task SetAsync(string path, string value);
    }
}
