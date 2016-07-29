using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    internal class AndFacetBase<FacetType1, FacetType2> : IFacet where FacetType1 : IFacet where FacetType2 :IFacet
    {
        public FacetType1 Facet1 { get; set; }
        public FacetType2 Facet2 { get; set; }
        public AndFacetBase(FacetType1 facet1, FacetType2 facet2)
        {
            Facet1 = facet1;
            Facet2 = facet2;
        }

        public string Stringify()
        {
            string facet1String = Facet1 == null ? "" : Facet1.GetConstructionString();
            string facet2String = Facet2 == null ? "" : Facet2.GetConstructionString();
            if (facet1String == "")
            {
                if (facet2String == "")
                    return "";
                else
                    return facet2String;
            }
            else
            {
                if (facet2String == "")
                    return facet1String;
                else
                {
                    //neither are null AND neither are blank
                    return $"{facet1String} AND {facet2String}";
                }
            }
        }

        public string GetConstructionString()
        {
            string facet1String = Facet1 == null ? "" : Facet1.GetConstructionString();
            string facet2String = Facet2 == null ? "" : Facet2.GetConstructionString();
            if (facet1String == "")
            {
                if (facet2String == "")
                    return "";
                else
                    return facet2String;
            }
            else
            {
                if (facet2String == "")
                    return facet1String;
                else
                {
                    //neither are null AND neither are blank
                    return $"{facet1String},{facet2String}";
                }
            }
        }

        public bool TestValue(object value)
        {
            if (null == Facet1)
            {
                if (null == Facet2)
                    throw new NullReferenceException("Facets");
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
    internal class AndFacet : AndFacetBase<IFacet, IFacet>
    {
        public AndFacet(IFacet facet1, IFacet facet2) : base(facet1, facet2) { }
    }
}
