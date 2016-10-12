using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    public interface IObservableCollection : INotifyCollectionChanged, IEnumerable
    {
    }
    /// <summary>
    /// This interface defines an observable collection with basic and complex access; the built-in ObservableCollection class implelments all of these interfaces
    /// </summary>
    /// <typeparam name="T">the type of this collection</typeparam>
    public interface IObservableCollection<T> : IObservableCollection, INotifyPropertyChanged, IEnumerable<T>
    {
    }
}
