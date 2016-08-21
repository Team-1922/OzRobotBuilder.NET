using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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
        /// Gives read and write access to this provider's properties
        /// </summary>
        /// <param name="key">the name of the property to update</param>
        /// <returns>the value of the property</returns>
        string this[string key] { get; set; }
        /// <summary>
        /// This is the top-level parent this class belongs to
        /// </summary>
        IHierarchialAccess TopParent { get; }
        /// <summary>
        /// The parent this provider belongs to
        /// </summary>
        IProvider Parent { get; }
        /// <summary>
        /// Used for json serialization of models
        /// </summary>
        /// <returns>the json text of the model instance</returns>
        string GetModelJson();
        /// <summary>
        /// Used for json deserialization of models
        /// </summary>
        /// <param name="text">the json text to deserialize</param>
        void SetModelJson(string text);
    }
}
