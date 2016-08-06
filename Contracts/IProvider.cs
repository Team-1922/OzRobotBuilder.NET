using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// While this interface might have some limited use beyond WPF, this
    /// is mostly a helper interface for the WPF designer so the TreeView can be used on 
    /// all of the viewmodels easily
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// The name of this provider; if this provider does have a name
        /// in the model, this does not have to conform to the xml schema "Name" restriction
        /// </summary>
        string Name { get; }
    }
}
