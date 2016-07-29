using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    internal class FacetList : List<IFacet>, IFacet
    {
        public string Stringify()
        {
            string ret = "";
            foreach(var facet in this)
            {
                if (facet == null)
                    continue;
                ret += $"OR {facet.Stringify()}\n";
            }
            return ret.Substring(0, ret.Length - 1);
        }
        public bool TestValue(object value)
        {
            foreach (var facet in this)
            {
                if (facet == null)
                    continue;
                if (facet.TestValue(value))
                    return true;
            }
            return false;
        }
    }
}
