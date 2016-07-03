using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    /// <summary>
    /// This is a helper class to give lists a name for 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NamedList<T> : List<T>
    {
        public string Name { get; set; }
        public NamedList(string name)
        {
            Name = name;
        }
    }
}
