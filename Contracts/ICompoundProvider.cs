using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// This is mostly a helper for the WPF treeview
    /// to properly show the hierarchy
    /// </summary>
    public interface ICompoundProvider : IProvider
    {
        /// <summary>
        /// The children of this provider; usually not conveniently a list to being with
        /// </summary>
        IObservableCollection Children { get; }
        /// <summary>
        /// Used for determining whether a particular provider descendent exists in this branch
        /// </summary>
        /// <remarks>
        /// One big thing to note here is that this will not return true if it is a property which is named <paramref name="descendentPath"/>
        /// </remarks>
        /// <param name="descendentPath">the name of the descendent to check</param>
        /// <returns>whether or not a descendent with the given path exists in this branch</returns>
        bool ContainsDescendentNamed(string descendentPath);
    }
    public interface ICompoundProvider<ProviderType> : ICompoundProvider where ProviderType : IProvider
    {
        IObservableCollection<ProviderType> Children { get; }
    }
}
