using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// This represents a class which can be accessed hierarchially using c#-style access (i.e. instance.member.subMember)
    /// </summary>
    public interface IHierarchialAccess
    {
        /// <summary>
        /// Hierarchial access to data
        /// </summary>
        /// <param name="key">the path to the value to read/write</param>
        /// <returns>the data at <paramref name="key"/></returns>
        string this[string key] { get; set; }
        /// <summary>
        /// Hierarchial exception-safe access to data
        /// </summary>
        /// <param name="key">the path to the value to read</param>
        /// <param name="value">the value read from <paramref name="key"/></param>
        /// <returns>whether or not the read was successful</returns>
        bool TryGetValue(string key, out string value);
        /// <summary>
        /// Hierarchial exception-safe access to data
        /// </summary>
        /// <param name="key">the path to the value to write</param>
        /// <param name="value">the value to write to<paramref name="key"/></param>
        /// <returns>whether or not the write was successful</returns>
        bool TrySetValue(string key, string value);
        /// <summary>
        /// Checks to see whether a given key exists
        /// </summary>
        /// <param name="key">the item to check</param>
        /// <returns>whether or not an item exists at <paramref name="key"/></returns>
        bool KeyExists(string key);
    }
}
