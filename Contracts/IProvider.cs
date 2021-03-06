﻿using System;
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
    public interface IProvider : INotifyPropertyChanged, IHierarchialAccess
    {
        /// <summary>
        /// The name of this provider; if this provider does have a name
        /// in the model, this does not have to conform to the xml schema "Name" restriction
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// This is the top-level parent this class belongs to
        /// </summary>
        IProviderRoot TopParent { get; }
        /// <summary>
        /// The parent this provider belongs to
        /// </summary>
        IProvider Parent { get; }
        /// <summary>
        /// The path to this object in the tree
        /// </summary>
        string FullyQualifiedName { get; }
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
        /// <summary>
        /// Expected to be exactly the same as <see cref="GetModelJson"/>
        /// </summary>
        /// <returns></returns>
        string ToString();
        /// <summary>
        /// The model instance of this class
        /// </summary>
        object ModelReference { get; set; }
    }
    /// <summary>
    /// This gives the provider a strongly-typed model instance associated with it.
    /// </summary>
    /// <typeparam name="ModelType">the type of the model</typeparam>
    public interface IProvider<ModelType> : IProvider
    {
        /// <summary>
        /// a strongly typed model reference
        /// </summary>
        ModelType ModelReference { get; set; }
    }
}
