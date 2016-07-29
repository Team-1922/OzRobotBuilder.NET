﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    //it seems like no types support this, so lets not use it
    public class FractionDigitsFacet : IntegerNumericFacet
    {
        public FractionDigitsFacet(int value) : base(value)
        {
        }

        public FractionDigitsFacet(string value) : base(value)
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

            //if it is a double, get the part after the decimal place
            var splitValue = obj.Split(new char[] { '.' }, StringSplitOptions.None);
            if (null == splitValue)
                return false;
            if (splitValue.Length == 1)//no decimal place = always goods
                return true;
            if (splitValue.Length == 2)
            {
                return splitValue[1].Length <= testAgainst;
            }
            return false;
        }

        protected override string Stringify(int testAgainst)
        {
            return $"Decimal Places <= {testAgainst}";
        }
    }
}
