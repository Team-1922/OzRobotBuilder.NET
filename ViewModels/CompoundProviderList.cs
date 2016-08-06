using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.ViewModels
{

    class CompoundProviderList<T> : ObservableCollection<T>, ICompoundProvider where T : IProvider
    {
        public CompoundProviderList(string name)
        {
            Name = name;
        }

        public IEnumerable<IProvider> Children
        {
            get
            {
                return this.Cast<IProvider>();
            }
        }

        public string Name
        {
            get;
        }

        public IReadOnlyDictionary<string, string> Properties
        {
            get
            {
                return (from child in Children select child).ToDictionary(child => child.Name, child => (child.ToString() ?? "null"));
            }
        }
    }
}
