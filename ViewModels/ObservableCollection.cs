using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.ViewModels
{
    /// <summary>
    /// This is just a version of the observable collection which satisfies the <see cref="IObservableCollection{T}"/> interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>, IObservableCollection<T>
    {
    }
}
