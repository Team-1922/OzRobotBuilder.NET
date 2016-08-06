using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Framework
{
    public class NamedList<T> : List<T>, INamedEnumerable<T>
    {
        public NamedList(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
