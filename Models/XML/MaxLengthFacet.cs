using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class MaxLengthFacet : IntegerNumericFacet
    {
        public MaxLengthFacet(int value) : base(value)
        {
        }
        public MaxLengthFacet(string value) : base(value)
        {
        }
        protected override double ClampValue(double test, int testAgainst)
        {
            //this doesn't really make sense in the context of numbers

            double absTest = Math.Abs(test);
            double sign = test >= 0 ? 1 : -1;

            double tenPowP1 = Math.Pow(10, testAgainst + 1);
            if (absTest >= tenPowP1)
            {
                double result;
                string sTest = absTest.ToString();
                bool success = double.TryParse(sTest.Substring(sTest.Length - testAgainst - 1 + (sTest.Contains('.') ? -1 : 0), testAgainst), out result);
                if (success)
                    return result * sign;
            }
            return tenPowP1 - 1;//this point should never be hit
        }
        public override bool TestValue(object test, int testAgainst)
        {
            return test.ToString().Length <= testAgainst;
        }
        protected override string Stringify(int testAgainst)
        {
            return $"Length <= {testAgainst} Characters";
        }
    }
}
