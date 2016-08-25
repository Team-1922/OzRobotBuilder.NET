using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class PatternFacet : IFacet
    {
        private Regex _pattern;
        public PatternFacet(string value)
        {
            _pattern = new Regex(value);
        }
        public string GetConstructionString()
        {
            return $"new {GetType().ToString()}(@\"{_pattern.ToString()}\")";
        }
        public string Stringify()
        {
            return $"matches pattern \"{_pattern.ToString()}\"";
        }
        public bool TestValue(object value)
        {
            if (value == null)
                return false;
            return _pattern.IsMatch(value.ToString());
        }
        public double ClampValue(double item)
        {
            return item;
        }
    }
}
