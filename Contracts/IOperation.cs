using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// represents a function-style operation: Name(op1,op2,op3,...)
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// The name of this operation
        /// </summary>
        string Name { get; }
        /// <summary>
        /// the number of parameters this method takes
        /// </summary>
        uint ParamCount { get; }
        /// <summary>
        /// executes this operation
        /// </summary>
        /// <param name="param">the params passed to this operation</param>
        /// <returns>the result of this operation</returns>
        decimal Perform(List<decimal> param);
    }
}
