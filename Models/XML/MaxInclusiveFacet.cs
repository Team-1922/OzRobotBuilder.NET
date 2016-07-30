using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class MaxInclusiveFacet : DoubleNumericFacet
    {
        public MaxInclusiveFacet(string value) : base(value)
        {
        }
        public MaxInclusiveFacet(double value) : base(value)
        {
        }
        protected override double ClampValue(double test, double testAgainst)
        {
            return test > testAgainst ? testAgainst : test;
        }
        protected override string Stringify(double testAgainst)
        {
            return $"value<={testAgainst}";
        }
        protected override bool TestValue(double test, double testAgainst)
        {
            return test <= testAgainst;
        }
    }
}
