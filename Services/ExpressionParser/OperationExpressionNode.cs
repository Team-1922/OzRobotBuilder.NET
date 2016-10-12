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
        public override async Task<double> EvaluateAsync()
        {
            if (Operation is IOperationDouble)
                return await EvaluateDoubleAsync();
            else if (Operation is IOperationBool)
                return await EvaluateBoolAsync();
            else
                throw new Exception("Unknown Operation Type");
        }
        private async Task<double> EvaluateDoubleAsync()
        {
            List<double> param = new List<double>();
            foreach(var child in Children)
            {
                param.Add(await child.EvaluateAsync());
            }
            return await (Operation as IOperationDouble).PerformAsync(param);
        }
        private async Task<double> EvaluateBoolAsync()
        {
            List<bool> param = new List<bool>();
            foreach (var child in Children)
            {
                param.Add((await child.EvaluateAsync()) >= 1.0);
            }
            return (await (Operation as IOperationBool).PerformAsync(param)) ? 1 : 0;
        }
    }
}
