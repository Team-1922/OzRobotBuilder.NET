using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public abstract class NumericComparisonFacet : IFacet
    {
        //assume that double-precision is enough
        private double _value;
        public NumericComparisonFacet(string value)
        {
            _value = double.Parse(value);
        }
        public string Stringify()
        {
            return Stringify(_value);
        }
        public bool TestValue(object value)
        {
            double tmp;
            if (value == null || !double.TryParse(value.ToString(), out tmp))
                return false;
            return TestValue(tmp, _value);
        }
        protected abstract bool TestValue(double test, double testAgainst);
        protected abstract string Stringify(double testAgainst);
    }
}
