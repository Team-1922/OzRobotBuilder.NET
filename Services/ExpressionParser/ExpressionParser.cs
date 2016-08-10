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
        
        /// <summary>
        /// The operations which are allowed to be in the format "4+5"
        /// </summary>
        static Regex _specialOps = new Regex(@"^(\+|-|\*|\/|\%|\^)$");
        /// <summary>
        /// Converts text at the given position up to the next special operation in <see cref="_specialOps"/>
        /// </summary>
        /// <param name="expression">the expression to loop through</param>
        /// <param name="i">the current index of <paramref name="expression"/></param>
        /// <returns>a node representing the operand</returns>
        private ExpressionNode GetOperand(string expression, ref int i)
        {
            //is this a sub-group
            if (expression[i] == '(')
            {
                //if so, take this subgroup, and recurse
                int begin = ++i;
                while (expression[i] != ')') ++i;

                //increment once at the end to make sure the next character is at the index location
                i++;
                
                return Group(expression.Substring(begin, i - begin - 1));
            }
            else
            {
                //if this is not a new sub-group, just loop until the next operand
                int begin = i;
                while (i < expression.Length && !_specialOps.IsMatch($"{expression[i]}")) ++i;
                return new ExpressionToken(double.Parse(expression.Substring(begin, i - begin)));
            }
        }
        /// <summary>
        /// Retrieves a binary operation with the given text-identifier
        /// </summary>
        /// <param name="op">the name of this operation, <see cref="IOperation.Name"/></param>
        /// <returns>the operation represented with <paramref name="op"/></returns>
        private BinaryOperation GetBinaryOperation(string op)
        {
            //if there is NO space between the two elements, that means multiplication (i.e. two touching parentheses)
            if (op == "")
                return _binaryOperation[2];//this should be multiplication

            //go through each binary operation and check if the string properly represents it
            foreach(var operation in _binaryOperation)
            {
                if (operation.Name == op)
                    return operation;
            }
            return null;
        }
        /// <summary>
        /// converts string expression into expression tree
        /// </summary>
        /// <param name="expression">the expression to convert</param>
        /// <returns>the converted expression</returns>
        private ExpressionNode Group(string expression)
        {
            //create the root node
            ExpressionNode tree = new ExpressionNode();

            //whether this is the first scan-cycle
            bool first = true;
            for (int i = 0; i < expression.Length; ++i)
            {
                //On the first scan, get the left-hand operand right-out
                if(first)
                {
                    //get the left operand
                    tree.Children.Add(GetOperand(expression, ref i));

                    //this keeps groups with ONLY a single parenthetical expression from throwing exceptions
                    if (i >= expression.Length)
                        return tree.Children.First();
                }

                //get the text representation of this operation
                int opBegin = i;
                while (_specialOps.IsMatch($"{expression[i]}")) ++i;
                string op = expression.Substring(opBegin, i - opBegin);

                //Get the current operation
                var currentOperation = GetBinaryOperation(op);
                if (currentOperation == null)
                    throw new Exception($"Failed to Retrieve Valid Binary Operation For Operation \"{op}\"");

                //get the right operend
                var rightOperand = GetOperand(expression, ref i);

                if (first)
                {
                    //update the first variable
                    first = false;

                    //on the first go-through, set the current operation to the tree's operation becuase it has not yet been set
                    tree.Operation = currentOperation;

                    //on the first go-through, set the right-hand operand to the tree's right-hand operand because it has not yet been set
                    tree.Children.Add(rightOperand);
                    continue;
                }

                //create this next operation node
                var thisNode = new ExpressionNode();
                thisNode.Operation = currentOperation;

                //this priority is MORE important than the previous one
                if (currentOperation.Priority < (tree.Operation as BinaryOperation).Priority)
                {
                    //steal the previous operation's right-hand operand
                    thisNode.Children.Add(tree.Children[1]);
                    
                    //insert this node as the right-hand of the previous operator
                    tree.Children[1] = thisNode;
                }
                else//this priority is NOT more important than the previous one
                {
                    //Make the left-hand operator the previous tree
                    thisNode.Children.Add(tree);

                    //set the tree to this node, becuase it is now the last operation to execute
                    tree = thisNode;
                }

                //the next operand is the new right operand
                thisNode.Children.Add(GetOperand(expression, ref i));
            }

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
