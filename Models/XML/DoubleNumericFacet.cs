using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public abstract class DoubleNumericFacet : IFacet
    {
        //assume that double-precision is enough
        private double _value;
        public DoubleNumericFacet(string value)
        {
            _value = double.Parse(value);
        }
        public DoubleNumericFacet(double value)
        {
            _value = value;
        }
        public string Stringify()
        {
            return Stringify(_value);
        }
        public string GetConstructionString()
        {
            return $"new {GetType().ToString()}({_value.ToString()})";
        }
        public bool TestValue(object value)
        {
            double tmp;
            if (value == null || !double.TryParse(value.ToString(), out tmp))
                return false;
            return TestValue(tmp, _value);
        }
        public double ClampValue(double input)
        {
            return ClampValue(input, _value);
        }
        protected abstract bool TestValue(double test, double testAgainst);
        protected abstract string Stringify(double testAgainst);
        protected abstract double ClampValue(double test, double testAgainst);
    }
}
