using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Services.ExpressionParser.Operations;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// represents an expression node which houses a write operator.  NOTE: Chlidren[0] must be a <see cref="DataAccessExpressionNode"/>
    /// </summary>
    internal class DataWriteExpressionNode : ExpressionNodeBase
    {
        DataAccessWriteOperation _writeOperation;
        IHierarchialAccessRoot _data;

        public DataWriteExpressionNode(DataAccessWriteOperation operation, IHierarchialAccessRoot data)
        {
            _writeOperation = operation;
            _data = data;
        }

        public override async Task<double> EvaluateAsync()
        {
            if (Children[0] is DataAccessExpressionNode)
            {
                await _writeOperation.PerformAsync((Children[0] as DataAccessExpressionNode).Path, await Children[1].EvaluateAsync(), _data);
                return await Children[1].EvaluateAsync();
            }
            else
                throw new Exception("First Operand of \"=\" Operation Is ReadOnly!");
        }
    }
}
