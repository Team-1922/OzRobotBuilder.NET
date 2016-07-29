using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class FacetCollection : IFacet
    {
        private FacetList _enumFacets = new FacetList();
        private FacetList _patternFacets = new FacetList();
        private AndFacet _andFacet = new AndFacet(null, null);

        public void AddFacet(IFacet facet)
        {
            if (facet is EnumerationFacet)
                _enumFacets.Add(facet);
            else if (facet is PatternFacet)
                _patternFacets.Add(facet);
            else if (facet is MaxExclusiveFacet || facet is MaxInclusiveFacet)
                _andFacet.Facet2 = facet;
            else if (facet is MinExclusiveFacet || facet is MinInclusiveFacet)
                _andFacet.Facet1 = facet;
        }
        public string Stringify()
        {
            string andFacetString = _andFacet.Stringify();
            if (andFacetString != "")
                andFacetString = $"OR {andFacetString}\n";
            else
                andFacetString = "";
            return $"Invalid Value!  Must Satisfy:\n{andFacetString}{_enumFacets.Stringify()}\n{_patternFacets.Stringify()}\n";
        }
        public bool TestValue(object value)
        {
            //if the AndFacet succeeds, the whole thing succeeds
            if (null != _andFacet)
                if (_andFacet.TestValue(value))
                    return true;

            //go through each other facet and if any of them succeed, the whole thing succeeds
            if (_enumFacets.TestValue(value))
                return true;
            if (_patternFacets.TestValue(value))
                return true;
            return false;
        }
    }
}
