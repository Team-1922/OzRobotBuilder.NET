using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class TotalDigitsFacet : IntegerNumericFacet
    {
        public TotalDigitsFacet(int value) : base(value)
        {
        }

        public TotalDigitsFacet(string value) : base(value)
        {
        }

        public override bool TestValue(object test, int testAgainst)
        {
            //first make sure it can be a double
            double tmp;
            string obj = test.ToString();
            bool success = double.TryParse(obj, out tmp);
            if (!success)
                return false;

            //if it is a double, remove the decimal place
            var splitValue = obj.Split(new char[] { '.' }, StringSplitOptions.None);
            if (null == splitValue)
                return false;
            if (splitValue.Length == 1)
                return splitValue[0].Length == testAgainst;
            if (splitValue.Length == 2)
            {
                return (splitValue[0].Length + splitValue[1].Length) == testAgainst;
            }
            return false;
        }

        protected override double ClampValue(double test, int testAgainst)
        {
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
            else if(absTest < tenPow)
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
            return $"digit count == {testAgainst}";
        }
    }
}
