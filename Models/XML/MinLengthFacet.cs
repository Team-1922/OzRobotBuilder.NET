using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class MinLengthFacet : IntegerNumericFacet
    {
        public MinLengthFacet(int value) : base(value)
        {
        }

        public MinLengthFacet(string value) : base(value)
        {
        }

        public override bool TestValue(object test, int testAgainst)
        {
            return test.ToString().Length >= testAgainst;
        }

        protected override double ClampValue(double test, int testAgainst)
        {
            //this doesn't really make sense in the context of numbers

            double absTest = Math.Abs(test);
            double sign = test >= 0 ? 1 : -1;

            double tenPow = Math.Pow(10, testAgainst);
            if (absTest < tenPow)
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
            return $"Length >= {testAgainst} Characters";
        }
    }
}
