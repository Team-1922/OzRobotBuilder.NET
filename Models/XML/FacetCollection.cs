using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class FacetCollection : IFacet
    {
        private FacetList _orFacets = new FacetList();
        private AndFacetBase<AndFacet,AndFacet> _andFacet = 
            new AndFacetBase<AndFacet, AndFacet>(
                new AndFacet(null, null), 
                new AndFacet(null, null));

        public FacetCollection(IEnumerable<IFacet> facets)
        {
            foreach (var facet in facets)
                AddFacet(facet);
        }
        public FacetCollection() { }
        public void AddFacet(IFacet facet)
        {
            if (facet == null)
                return;
            if (facet is EnumerationFacet)
                _orFacets.Add(facet);
            else if (facet is PatternFacet)
                _orFacets.Add(facet);
            else if (facet is MaxExclusiveFacet || facet is MaxInclusiveFacet)
                _andFacet.Facet2.Facet2 = facet;
            else if (facet is MinExclusiveFacet || facet is MinInclusiveFacet)
                _andFacet.Facet2.Facet1 = facet;
            else if (facet is FractionDigitsFacet)
                return;
            else if (facet is TotalDigitsFacet || facet is LengthFacet)
            {
                _andFacet.Facet1.Facet1 = facet;
                _andFacet.Facet1.Facet2 = null;
            }
            else if (facet is MinLengthFacet)
                _andFacet.Facet1.Facet1 = facet;
            else if (facet is MaxLengthFacet)
                _andFacet.Facet1.Facet2 = facet;
        }
        public string GetConstructionString()
        {
            StringWriter ret = new StringWriter();
            ret.Write("new FacetCollection(");
            string orFacetText = _orFacets.GetConstructionString();
            string andFacetText = _andFacet.GetConstructionString();

            if (orFacetText != "")
            {
                ret.Write($"{orFacetText}");
                if (andFacetText != "")
                    ret.Write($",{andFacetText}");
            }
            else
            {
                if (andFacetText != "")
                    ret.Write($"{andFacetText}");
            }
            ret.Write(")");
            return ret.ToString();
        }
        public string Stringify()
        {
            string andFacetString = _andFacet.Stringify();
            string orFacetString = _orFacets.Stringify();
            if (andFacetString != "")
            {                
                andFacetString = $"{andFacetString}\n";
                orFacetString = $"OR {orFacetString}";
            }
            return $"Invalid Value!  Must Satisfy:\n{andFacetString}{_orFacets.Stringify()}\n";
        }
        public bool TestValue(object value)
        {
            //if the length part of the andFacet fails, the whole thing fails
            try
            {
                if (!_andFacet.Facet1.TestValue(value))
                    return false;
            }
            catch(NullReferenceException)
            {
                //this just means both facets are null and therefore no length testing exists
            }


            try
            {
                //if the value part of the andFacet succeeds, the whole thing succeeds
                if (_andFacet.Facet2.TestValue(value))
                    return true;
            }
            catch(NullReferenceException)
            {
                //this means that no value testing exists, and we can just continue
            }

            //go through each other facet and if any of them succeed, the whole thing succeeds
            if (_orFacets.TestValue(value))
                return true;
            return false;
        }
    }
}
