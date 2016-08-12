using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    public class Expression : IExpression
    {
        ExpressionNode _rootNode;
        string _expressionString;
        public Expression(string exp, ExpressionNode rootNode)
        {
            _expressionString = exp;
            _rootNode = rootNode;
        }

        public double Evaluate()
        {
            return _rootNode.Evaluate();
        }

        public string GetString()
        {
            return _expressionString;
        }
    }
}
