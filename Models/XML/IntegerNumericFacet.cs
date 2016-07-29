using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public abstract class IntegerNumericFacet : IFacet
    {
        int _value;
        public IntegerNumericFacet(string value)
        {
            _value = int.Parse(value);
        }
        public IntegerNumericFacet(int value)
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
        public bool TestValue(object test)
        {
            if(test == null)
                return false;
            return TestValue(test, _value);
        }
        public abstract bool TestValue(object test, int testAgainst);
        protected abstract string Stringify(int testAgainst);
    }
}
