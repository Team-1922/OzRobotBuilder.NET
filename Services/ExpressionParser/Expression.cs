using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    internal class Expression : IExpression
    {
        ExpressionNodeBase _rootNode;
        string _expressionString;
        public Expression(string exp, ExpressionNodeBase rootNode)
        {
            _expressionString = exp;
            _rootNode = rootNode;
        }

        public async Task<double> EvaluateAsync()
        {
            if (null == _rootNode)
                return 0.0;
            return await _rootNode.EvaluateAsync();
        }

        public string GetString()
        {
            return _expressionString;
        }
    }
}
