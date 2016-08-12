using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// represents a node of a tree of expressions
    /// </summary>
    public class ExpressionNode
    {
        /// <summary>
        /// one child branch of the binary tree (left-hand operand)
        /// </summary>
        public List<ExpressionNode> Children { get; } = new List<ExpressionNode>();

        public IOperation Operation { get; set; }

        /// <summary>
        /// Returns the result of the evaluated expression
        /// </summary>
        /// <returns></returns>
        public virtual decimal Evaluate()
        {
            return Operation.Perform((from child in Children select child.Evaluate()).ToList());
        }
    }
}
