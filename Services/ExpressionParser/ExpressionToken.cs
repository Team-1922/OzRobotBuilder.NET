using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// This represents a single number instead of an operation to perform
    /// </summary>
    public class ExpressionToken : ExpressionNode
    {
        /// <summary>
        /// this token's value
        /// </summary>
        public double Value
        {
            get
            {
                return (double)_value;
            }

            set
            {
                _value = (decimal)value;
            }
        }
        private decimal _value;

        public ExpressionToken(double value)
        {
            Value = value;
        }

        public override decimal Evaluate()
        {
            return _value;
        }
    }
}
