using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.MVVM.Framework
{
    /// <summary>
    /// This is an observable dictionary that can be converted into a readonly dictionary
    /// </summary>
    /// <typeparam name="TKey">Dictionary Key Type</typeparam>
    /// <typeparam name="TValue">Dictionary Value Type</typeparam>
    public class ReadonlyObservableDictionary<TKey, TValue> : ObservableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }
    }
}
