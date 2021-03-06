﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// This represents a single number instead of an operation to perform
    /// </summary>
    internal class ExpressionToken : ExpressionNodeBase
    {
        /// <summary>
        /// this token's value
        /// </summary>
        public double Value { get; set; }

        public ExpressionToken(double value)
        {
            Value = value;
        }

        public override async Task<double> EvaluateAsync()
        {
            return Value;
        }
    }
}
