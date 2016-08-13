using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// parses a given string expression into a compiled expression
    /// TODO: support extensions (when System.ComponentModel.Import/Export is added to .NET core)
    /// </summary>
    public interface IExpressionParser
    {
        /// <summary>
        /// Registers the given operation to be used when parsing expressions; 
        /// NOTE: this will be removed once the Import/Export attributes are added to .NET core
        /// </summary>
        /// <param name="operation">the operation to register</param>
        void RegisterOperation(IOperation operation);
        /// <summary>
        /// attempts to parse the given string into a compiled expression
        /// </summary>
        /// <param name="expression">the string expression</param>
        /// <param name="compiledExpression">the compiled expression; null if failed</param>
        /// <returns>the success of the conversion</returns>
        bool TryParseExpression(string expression, out IExpression compiledExpression);
        /// <summary>
        /// Parses a given string into a compiled expression, throws exception upon error
        /// </summary>
        /// <param name="expression">the expression to parse</param>
        /// <returns>a compiled expression</returns>
        IExpression ParseExpression(string expression);
        /// <summary>
        /// Checks to make sure this expression is still valid if the model changed in a significant way
        /// </summary>
        /// <param name="expression">the expression to check</param>
        /// <returns>whether the given expression is can be run without throwing an exception</returns>
        bool AssertExpression(IExpression expression);
    }
}
