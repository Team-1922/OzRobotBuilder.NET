﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    public class MaxExclusiveFacet : DoubleNumericFacet
    {
        public MaxExclusiveFacet(string value) : base(value)
        {
        }
        public MaxExclusiveFacet(double value) : base(value)
        {
        }
        protected override string Stringify(double testAgainst)
        {
            return $"value<{testAgainst}";
        }
        protected override bool TestValue(double test, double testAgainst)
        {
            return test < testAgainst;
        }
    }
}