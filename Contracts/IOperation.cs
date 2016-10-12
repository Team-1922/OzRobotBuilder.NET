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
        /// Which order of operations is this operation?
        /// </summary>
        OperationPriority Priority { get; }
    }
    /// <summary>
    /// represents a function-style operation: Name(op1,op2,op3,...) with double parameters/return type
    /// </summary>
    public interface IOperationDouble : IOperation
    {
        /// <summary>
        /// executes this operation
        /// </summary>
        /// <param name="param">the params passed to this operation</param>
        /// <returns>the result of this operation</returns>
        Task<double> PerformAsync(List<double> param);
    }
    /// <summary>
    /// represents a function-style operation: Name(op1,op2,op3,...) with boolean parameters/return type
    /// </summary>
    public interface IOperationBool : IOperation
    {
        /// <summary>
        /// executes this operation
        /// </summary>
        /// <param name="param">the params passed to this operation</param>
        /// <returns>the result of this operation</returns>
        Task<bool> PerformAsync(List<bool> param);
    }
    public enum OperationPriority
    {
        GroupingSymbols = 0,
        Exponent = 1,
        MultDiv = 2,
        AddSub = 3,
        Boolean = 4,
        Lowest = Boolean + 1//These always get evaluated LAST
    }
}
