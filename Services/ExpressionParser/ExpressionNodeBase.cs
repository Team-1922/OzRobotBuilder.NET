using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// represents a node of a tree of expressions
    /// </summary>
    internal abstract class ExpressionNodeBase
    {
        /// <summary>
        /// one child branch of the binary tree (left-hand operand)
        /// </summary>
        public List<ExpressionNodeBase> Children { get; } = new List<ExpressionNodeBase>();

        /// <summary>
        /// Returns the result of the evaluated expression
        /// </summary>
        /// <returns></returns>
        public abstract double Evaluate();
    }
}
