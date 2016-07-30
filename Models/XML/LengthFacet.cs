using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class LengthFacet : IntegerNumericFacet
    {
        public LengthFacet(int value) : base(value)
        {
        }

        public LengthFacet(string value) : base(value)
        {
        }

        public override bool TestValue(object test, int testAgainst)
        {
            return test.ToString().Length == testAgainst;
        }

        protected override double ClampValue(double test, int testAgainst)
        {
            //this is completely illogical, however just for sanity's sake it behaves just like TotalDigits
            double absTest = Math.Abs(test);
            double sign = test >= 0 ? 1 : -1;

            double tenPow = Math.Pow(10, testAgainst);
            double tenPowP1 = tenPow * 10;
            if (absTest >= tenPowP1)
            {
                double result;
                string sTest = absTest.ToString();
                bool success = double.TryParse(sTest.Substring(sTest.Length - testAgainst - 1 + (sTest.Contains('.') ? -1 : 0), testAgainst), out result);
                if (success)
                    return result * sign;
            }
            else if (absTest < tenPow)
            {
                do
                {
                    absTest *= 10;
                } while (absTest < tenPow);
                return absTest * sign;
            }
            return tenPow;//this point should never be hit
        }

        protected override string Stringify(int testAgainst)
        {
            return $"Length == {testAgainst} Characters";
        }
    }
}
