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

        public override double Evaluate()
        {
            if (Children[0] is DataAccessExpressionNode)
            {
                _writeOperation.Perform((Children[0] as DataAccessExpressionNode).Path, Children[1].Evaluate(), _data);
                return Children[1].Evaluate();
            }
            else
                throw new Exception("First Operand of \"=\" Operation Is ReadOnly!");
        }
    }
}
