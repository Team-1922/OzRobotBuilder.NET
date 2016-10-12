using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// This is an add-in class that allows all of the Implementation of IOperationDouble/Bool to be done through inheritance instead of 
    /// a full class for each
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class BinaryOperation<T> : IOperation
    {
        public uint ParamCount
        {
            get
            {
                return 2;
            }
        }

        public async Task<T> PerformAsync(List<T> param)
        {
            if(param.Count != ParamCount)
                throw new ArgumentException("param count not equal to 2");
            return await PerformAsync(param[0], param[1]);
        }

        #region Abstract Methods
        public abstract OperationPriority Priority { get; }
        public abstract Task<T> PerformAsync(T input1, T input2);
        public abstract string Name { get; }
        #endregion
    }
    internal abstract class BinaryOperationDouble : BinaryOperation<double>, IBinaryOperationDouble
    {
    }
    internal abstract class BinaryOperationBool : BinaryOperation<bool>, IBinaryOperationBool
    {
        public override OperationPriority Priority
        {
            get
            {
                return OperationPriority.Boolean;
            }
        }
    }

}
