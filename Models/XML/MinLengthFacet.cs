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

        protected override string Stringify(int testAgainst)
        {
            return $"Length >= {testAgainst} Characters";
        }
    }
}
