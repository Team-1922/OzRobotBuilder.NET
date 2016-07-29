using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    internal class AndFacet : IFacet
    {
        public IFacet Facet1 { get; set; }
        public IFacet Facet2 { get; set; }
        public AndFacet(IFacet facet1, IFacet facet2)
        {
            Facet1 = facet1;
            Facet2 = facet2;
        }

        public string Stringify()
        {
            if (null == Facet1)
            {
                if (null == Facet2)
                    return "";
                else
                    return Facet2.ToString();
            }
            else if (null == Facet2)
            {
                return Facet1.ToString();
            }
            return $"{Facet1.ToString()} AND {Facet2.ToString()}";
        }

        public bool TestValue(object value)
        {
            if (null == Facet1)
            {
                if (null == Facet2)
                    return false;
                else
                    return Facet2.TestValue(value);
            }
            else if (null == Facet2)
            {
                return Facet1.TestValue(value);
            }

            return Facet1.TestValue(value) && Facet2.TestValue(value);
        }
    }
}
