using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// Represents an expression node which has an operation to execute
    /// </summary>
    internal class OperationExpressionNode : ExpressionNodeBase
    {
        public IOperation Operation { get; set; }

        /// <summary>
        /// Returns the result of the evaluated expression
        /// </summary>
        /// <returns></returns>
        public override double Evaluate()
        {
            return Operation.Perform((from child in Children select child.Evaluate()).ToList());
        }
    }
}
