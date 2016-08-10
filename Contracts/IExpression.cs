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

    /// <summary>
    /// extends the compiled expression with a given return type
    /// </summary>
    /// <typeparam name="RetType">the return type of this expression</typeparam>
    public interface IExpression<RetType> : IExpression
    {
        /// <summary>
        /// Executes this expression
        /// </summary>
        /// <returns>the result of the expression</returns>
        RetType Evaluate();
    }

}
