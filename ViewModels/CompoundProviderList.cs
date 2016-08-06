using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.ViewModels
{

    class CompoundProviderList<T> : List<T>, ICompoundProvider where T : IProvider
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
    }
}
