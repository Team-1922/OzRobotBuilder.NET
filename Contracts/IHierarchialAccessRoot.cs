using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The top-level hierarchial access instance; this provies asynchronous access to the view-model tree
    /// </summary>
    public interface IHierarchialAccessRoot : IDisposable
    {
        /// <summary>
        /// Retrieves the value at the given key
        /// </summary>
        /// <param name="key">the key to look for the value at</param>
        /// <returns>the value at <paramref name="key"/></returns>
        Task<string> GetAsync(string key);
        /// <summary>
        /// Sets the value at the given key
        /// </summary>
        /// <param name="key">where to set <paramref name="value"/> at</param>
        /// <param name="value">the value to set at location <paramref name="key"/></param>
        /// <param name="safe">if true, then the task waits for the result and throws any exceptions that occured; if false, then just send the request and leave</param>
        /// <returns></returns>
        Task SetAsync(string key, string value, bool safe = true);
        /// <summary>
        /// Used to determine whether an item exsits at the given key
        /// </summary>
        /// <param name="key">the key to check</param>
        /// <returns>whether or not an item exists at <paramref name="key"/></returns>
        bool KeyExists(string key);
        /// <summary>
        /// This is called when the tree is updated (viewmodel objects added/deleted) to create a look-up table for the entire tree
        /// in order to optimize read/write performnace.  This is typically called once at the beginning of a non-debug application run
        /// </summary>
        void PrecomputeTree();
    }
}
