using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    internal class FacetList : List<IFacet>, IFacet
    {
        public string Stringify()
        {
            if (Count == 0)
                return "";
            string ret = "";
            foreach(var facet in this)
            {
                if (facet == null)
                    continue;
                if (ret == "")
                    ret = $"{facet.Stringify()}\n";
                else
                    ret += $"OR {facet.Stringify()}\n";
            }
            return ret.Substring(0, ret.Length - 1);
        }
        public string GetConstructionString()
        {
            StringWriter ret = new StringWriter();
            foreach (var facet in this)
            {
                if (facet == null)
                    continue;
                ret.Write(facet.GetConstructionString());
                ret.Write(",");
            }
            string retString = ret.ToString();
            return retString.Substring(0, retString.Length > 0 ? retString.Length - 1 : 0);
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
        public T ClampValue<T>(T item)
        {
            return item;
        }
        public double ClampValue(double item)
        {
            return item;
        }
    }
}
