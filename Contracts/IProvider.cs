using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public interface IProvider : INotifyPropertyChanged
    {
        /// <summary>
        /// The name of this provider; if this provider does have a name
        /// in the model, this does not have to conform to the xml schema "Name" restriction
        /// </summary>
        string Name { get; }
        /// <summary>
        /// This represents all of the properties the provider has so it can be easily accessable to a 
        /// key,value user-interface feature
        /// </summary>
        //IReadOnlyDictionary<string,string> Properties { get; }
        /// <summary>
        /// This essentially is the setter for the <see cref="Properties"/> property;
        /// NOTE: this is not required to work in a hierarchial mannor
        /// </summary>
        /// <param name="key">the name of the property to set</param>
        /// <param name="value">the value of the property to set</param>
        //void SetProperty(string key, string value);
    }
}
