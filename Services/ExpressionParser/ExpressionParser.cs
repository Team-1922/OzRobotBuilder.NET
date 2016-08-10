using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    public class ExpressionParser : IExpressionParser
    {
        private List<IOperation> _operations = new List<IOperation>();
        private List<BinaryOperation> _binaryOperation = new List<BinaryOperation>();

        public ExpressionParser()
        {
            RegisterOperations();
        }

        private void RegisterOperations()
        {
            foreach(var operation in ArithmeticOperations.Operations)
            {
                RegisterOperation(operation);
                _binaryOperation.Add(operation);
            }
        }

        //static Regex _numbers = new Regex("^[0-9]$");
        static Regex _specialOps = new Regex(@"^(\+|-|\*|\/|\%|\^)$");
        private ExpressionNode GetOperand(string expression, ref int i)
        {
            //is this a group?
            if (expression[i] == '(')
            {
                int begin = ++i;
                while (expression[i] != ')') ++i;
                return Group(expression.Substring(begin, i - begin));
            }
            else
            {
                //keep going until the next operation
                int begin = i;
                while (i < expression.Length && !_specialOps.IsMatch($"{expression[i]}")) ++i;
                return new ExpressionToken(double.Parse(expression.Substring(begin, i - begin)));
            }
        }
        private BinaryOperation GetBinaryOperation(string op)
        {
            foreach(var operation in _binaryOperation)
            {
                if (operation.Name == op)
                    return operation;
            }
            return null;
        }
        /// <summary>
        /// converts expression into LISP-style expression
        /// </summary>
        /// <param name="expression">the expression to convert</param>
        /// <returns>the converted expression</returns>
        private ExpressionNode Group(string expression)
        {
            ExpressionNode tree = new ExpressionNode();
            string ret = "";
            //TODO: it might be better to directly convert this into a tree instead of frankensteining the LISP-style insto string first
            //go through the string and convert into LISP-style operations
            bool first = true;
            for (int i = 0; i < expression.Length; ++i)
            {
                if(first)
                {
                    //get the left operand
                    tree.Children.Add(GetOperand(expression, ref i));

                    //this keeps groups with ONLY a single parenthetical expression from making problems
                    if (i >= expression.Length || expression[i] == ')')
                        return tree.Children.First();
                }

                //get the text representation of this operation
                int opBegin = i;
                while (_specialOps.IsMatch($"{expression[i]}")) ++i;
                string op = expression.Substring(opBegin, i - opBegin);


                if (first)
                {
                    first = false;
                    tree.Operation = GetBinaryOperation(op);
                    if (null == tree.Operation)
                        throw new Exception();//TODO: make this more elegant

                    //get the right operend
                    tree.Children.Add(GetOperand(expression, ref i));
                    continue;
                }

                var thisNode = new ExpressionNode();
                thisNode.Operation = GetBinaryOperation(op);
                //this priority is MORE important than the previous one
                if ((thisNode.Operation as BinaryOperation).Priority < (tree.Operation as BinaryOperation).Priority)
                {
                    //store the previous right operation as the left operand of the current operation
                    thisNode.Children.Add(tree.Children[1]);
                    tree.Children[1] = thisNode;
                }
                else//this priority is NOT more important than the previous one
                {
                    thisNode.Children.Add(tree);
                    tree = thisNode;
                }
                //the next operand is the new right operand
                thisNode.Children.Add(GetOperand(expression, ref i));
            }

            //now that all of the non-numbers are received, add grouping symbols around the important ones


            //this is a bit of a special case; add multiplication wherever a close and open parenthesis meet

            return tree;
        }

        private ExpressionNode ParseExpression(string expression)
        {
            //start by removing all of the spaces
            string condensed = new string((from ch in expression.Trim() where ch != ' ' select ch).ToArray());

            //then remove the parenthesis at the beginning and end which do not do anything (this keep
            //int parenthesesCount = 0;
            //for(int i = 0; i < condensed.Length - parenthesesCount; ++i)
            //    if (condensed[i] == '(' && condensed[condensed.Length - i - 1] == ')')
            //        parenthesesCount++;
            //return Group(condensed.Substring(parenthesesCount, condensed.Length - 2*parenthesesCount));
            return Group(condensed);
        }

        #region IExpressionParser Methods
        public bool AssertExpression(IExpression expression)
        {
            throw new NotImplementedException();
        }

        public bool IsValidExpression(string expression)
        {
            throw new NotImplementedException();
        }

        public void RegisterOperation(IOperation operation)
        {
            _operations.Add(operation);
        }

        public bool TryParseExpression(string expression, out IExpression compiledExpression)
        {
            try
            {
                compiledExpression = new Expression(expression, ParseExpression(expression));
                return true;
            }
            catch(Exception)
            {
                compiledExpression = null;
                return false;
            }
        }

        public bool TryParseExpression<T>(string expression, out IExpression<T> compiledExpression)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
