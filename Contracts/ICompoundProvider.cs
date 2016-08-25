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
    }
    public interface ICompoundProvider<ProviderType> : ICompoundProvider where ProviderType : IProvider
    {
        IObservableCollection<ProviderType> Children { get; }
    }
}
