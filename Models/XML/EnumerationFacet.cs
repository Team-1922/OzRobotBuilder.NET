using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class EnumerationFacet : IFacet
    {
        private string _value;
        public EnumerationFacet(string value)
        {
            _value = value;
        }
        public string GetConstructionString()
        {
            return $"new {GetType().ToString()}(\"{_value}\")";
        }
        public string Stringify()
        {
            return $"value=={_value}";
        }
        public bool TestValue(object value)
        {
            if (value == null)
                return false;
            return value.ToString() == _value;
        }
    }
}
