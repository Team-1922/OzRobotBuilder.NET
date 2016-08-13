using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{

    /// <summary>
    /// represents a compiled expression that can be executed
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// executes this expression
        /// </summary>
        /// <returns>the result of the expression</returns>
        double Evaluate();
        /// <summary>
        /// Gets the string representation of this expression
        /// </summary>
        /// <returns>the string representation of this expression</returns>
        string GetString();
    }
}
