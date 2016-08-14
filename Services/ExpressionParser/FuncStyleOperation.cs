using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    internal abstract class FuncStyleOperation : IOperationDouble
    {
        public OperationPriority Priority
        {
            get
            {
                return OperationPriority.GroupingSymbols;
            }
        }
        public double Perform(List<double> param)
        {
            if (param.Count != ParamCount)
                throw new ArgumentException($"Incorrect Number of Parameters for Function: \"{Name}\"");
            return PerformInternal(param);
        }
        protected abstract double PerformInternal(List<double> param);

        #region Abstract Methods
        public abstract uint ParamCount { get; }
        public abstract string Name { get; }
        #endregion
    }
}
