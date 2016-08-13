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
        private List<IOperation> _binaryOperations = new List<IOperation>();
        private UnaryMinus _unaryMinusOperation = new UnaryMinus();

        public ExpressionParser()
        {
            RegisterOperations();
        }

        private void RegisterOperations()
        {
            foreach(var operation in Operations.DoubleOperations)
            {
                _binaryOperations.Add(operation);
            }
        }

        #region Private Helper Methods

        /// <summary>
        /// The operations which are allowed to be in the format "4+5"
        /// </summary>
        static Regex _specialOps = new Regex(@"^(\+|-|\*|\/|\%|\^)$");
        /// <summary>
        /// The regular expression for valid operation names
        /// </summary>
        static Regex _validOpName = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
        /// <summary>
        /// This is used to test operation characters against these signs to make sure the are recognized as such
        /// </summary>
        static Regex _validSigns = new Regex(@"^(\+|-)$");
        /// <summary>
        /// Converts text at the given position up to the next special operation in <see cref="_specialOps"/>
        /// </summary>
        /// <param name="expression">the expression to loop through</param>
        /// <param name="i">the current index of <paramref name="expression"/></param>
        /// <returns>a node representing the operand</returns>
        private ExpressionNodeBase GetOperand(string expression, ref int i)
        {
            //if this starts with unary "-" operator
            if(expression[i] == '-')
            {
                //make sure to move to the next character
                i++;

                //then create an intermediary layer where the multiply-by-negative-one occurs
                OperationExpressionNode node = new OperationExpressionNode();
                node.Operation = _unaryMinusOperation;
                node.Children.Add(new ExpressionToken(0));//this is to get the BinaryOperation class not to complain
                node.Children.Add(GetOperand(expression, ref i));
                return node;
            }

            //is this a sub-group
            if (expression[i] == '(')
            {
                //if so, take this subgroup, and recurse
                var returnExpression = GetGroupStatement(expression, ref i);
                
                return Group(returnExpression);
            }
            //is this data-access?
            else if(expression[i] == '[')
            {
                if(!ExpressionParserService.DataAccessEnabled)
                {
                    throw new Exception("Attempt to Access Data From Expression Parser when Data Access is Not Enabled; Set \"ExpressionParserService.DataAccessEnabled\" to \"true\"!");
                }

                //get the path within the brackets
                var path = GetGroupStatement(expression, ref i, '[', ']');

                return new DataAccessExpressionNode(DataAccessService.Instance, path);
            }
            else if(_validOpName.IsMatch($"{expression[i]}"))
            {
                //this means it is a non-binary operation (user added or otherwise)
                int begin = i;
                
                //continue until we reach the open parenthesis
                while (expression[i] != '(') ++i;

                //get the operation from this string
                var operation = GetOperation(expression.Substring(begin, i - begin));

                //get the list of parameters
                string parameters = GetGroupStatement(expression, ref i);

                //split the string based on TOP LEVEL COMMAS ONLY
                //  This means the commas inside other parenthetical expressions do not count
                var paramList = GetParamList(parameters);

                //make sure the correct number of parameters were passed
                if(paramList.Count != operation.ParamCount)
                    throw new Exception($"Improper Number of Parameters Passed to \"{operation.Name}\"");

                //construct the expression node
                var node = new OperationExpressionNode();
                node.Operation = operation;

                //get the children by recursing this method
                int j = 0;
                foreach (var param in paramList)
                {
                    j = 0;
                    node.Children.Add(GetOperand(param, ref j));
                    Console.WriteLine(param);
                }
                return node;
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
        /// Gets the next group statement in a string
        /// </summary>
        /// <param name="statement">the string to look through</param>
        /// <param name="open">the open-group character (i.e. '(')</param>
        /// <param name="close">the close-group character (i.e. ')')</param>
        /// <param name="i">the index of the string to start at</param>
        /// <returns>the next parenthetical statement</returns>
        private string GetGroupStatement(string statement, ref int i, char open = '(', char close = ')')
        {
            //increment once at the beginning to remove the open parenthesis
            int begin = ++i;
            int level = 0;
            try
            {
                //since there will be parentheses within this operation, make sure to get the whole string
                //  once "level" reaches -1, this means the closing parenthesis is reached
                while (level != -1)
                {
                    var ch = statement[i];
                    if (ch == open)
                        level++;
                    else if (ch == close)
                        level--;
                    
                    //increment at the end to go past the end parenthesis
                    ++i;
                }
            }
            catch (Exception)
            {
                //this occurs if a parenthesis was opened but not closed properly
                throw new Exception("Parenthetical Statement Not Properly Closed");
            }
            return statement.Substring(begin, i - begin - 1);
        }
        /// <summary>
        /// Converts string representation of parameters into a list of the parameters
        /// </summary>
        /// <param name="param">the comma-delimited parameters</param>
        /// <returns>a list representation of these parameters</returns>
        private List<string> GetParamList(string param)
        {
            List<string> ret = new List<string>();
            int level = 0;
            int begin = 0;
            for(int i = 0; i < param.Length; ++i)
            {
                var ch = param[i];

                //prevent the commas inside parentheses from counting
                if (ch == '(')
                    level++;
                else if (ch == ')')
                    level--;
                if(level == 0)
                {
                    if (ch == ',')
                    {
                        //surround the parameter with parentheses so the GetOperand method knows that it is part of one group
                        ret.Add($"({param.Substring(begin, i - begin)})");
                        begin = i + 1;
                    }
                }
            }

            //add the last parameter
            if(param.Length > 1)
            {
                //surround the parameter with parentheses so the GetOperand method knows that it is part of one group
                ret.Add($"({param.Substring(begin, param.Length - begin)})");
            }

            return ret;
        }
        /// <summary>
        /// Looks up the given operation name in the registered list
        /// </summary>
        /// <param name="opName">the name of the operation to find</param>
        /// <returns>the operation with the given name; null if it does not exist</returns>
        private IOperation GetOperation(string opName)
        {
            foreach (var operation in _operations)
            {
                if (operation.Name.ToLowerInvariant() == opName.ToLowerInvariant())
                    return operation;
            }
            throw new ArgumentException($"Operation: \"{opName}\" Is Invalid");
        }
        /// <summary>
        /// Retrieves a binary operation with the given text-identifier
        /// </summary>
        /// <param name="opName">the name of this operation, <see cref="IOperation.Name"/></param>
        /// <returns>the operation represented with <paramref name="op"/></returns>
        private IOperation GetBinaryOperation(string opName)
        {
            //if there is NO space between the two elements, that means multiplication (i.e. two touching parentheses)
            if (opName == "")
                return _binaryOperations[2];//this should be multiplication

            //go through each binary operation and check if the string properly represents it
            foreach(var operation in _binaryOperations)
            {
                if (operation.Name == opName)
                    return operation;
            }

            throw new ArgumentException($"Binary Operation: \"{opName}\" Is Invalid");
        }
        /// <summary>
        /// converts string expression into expression tree
        /// </summary>
        /// <param name="expression">the expression to convert</param>
        /// <returns>the converted expression</returns>
        private ExpressionNodeBase Group(string expression)
        {
            //create the root node
            ExpressionNodeBase tree = null;

            //whether this is the first scan-cycle
            bool first = true;
            for (int i = 0; i < expression.Length;)
            {
                //create this next operation node
                var thisNode = new OperationExpressionNode();

                //On the first scan, get the left-hand operand right-out
                if (first)
                {
                    //get the left operand
                    thisNode.Children.Add(GetOperand(expression, ref i));

                    //this keeps groups with ONLY a single parenthetical expression from throwing exceptions
                    if (i >= expression.Length)
                        return thisNode.Children[0];
                }

                //get the text representation of this operation
                int opBegin = i;
                while (_specialOps.IsMatch($"{expression[i]}") && (i > opBegin ? !_validSigns.IsMatch($"{expression[i]}") : true)) ++i;
                string op = expression.Substring(opBegin, i - opBegin);

                //Get the current operation
                var currentOperation = GetBinaryOperation(op);

                //get the right operend
                var rightOperand = GetOperand(expression, ref i);
                
                //set the operation
                thisNode.Operation = currentOperation;
                
                if (first)
                {
                    //update the first variable
                    first = false;

                    //the first time through with a unary operator is a little bit different, becuase order of operations still takes affect
                    if (thisNode.Children[0] is OperationExpressionNode && ((thisNode.Children[0] as OperationExpressionNode)?.Operation is UnaryMinus) && currentOperation.Priority < OperationPriority.MultDiv)
                    {
                        //first set the top-level equal to the unary minus operation          
                        tree = thisNode.Children[0];

                        //steal the unary operation's operand
                        thisNode.Children[0] = tree.Children[1];

                        //then insert this operation as the right-hand operand
                        tree.Children[1] = thisNode;
                    }
                    else
                    {
                        //simply set this node to the tree node, because it has not been set yet
                        tree = thisNode;
                    }
                }
                else
                {
                    //this priority is MORE important than the previous one
                    if (currentOperation.Priority < (((tree as OperationExpressionNode)?.Operation)?.Priority ?? ThrowNodeException<OperationPriority>()))
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
                }

                //the next operand is the new right operand
                thisNode.Children.Add(rightOperand);
            }

            return tree;
        }
        /// <summary>
        /// this is called after using nullpropogation with an invalid cast
        /// </summary>
        /// <typeparam name="T">this just makes this method compatable with any context</typeparam>
        /// <returns>nothing, only throws an exception</returns>
        private T ThrowNodeException<T>()
        {
            throw new Exception("Unsuccessful ExpressionNode Cast!");
        }
        #endregion

        #region IExpressionParser Methods
        public bool AssertExpression(IExpression expression)
        {
            try
            {
                expression.Evaluate();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public void RegisterOperation(IOperation operation)
        {
            if (!_validOpName.IsMatch(operation.Name))
                throw new ArgumentException($"Invalid Operation Name: \"{operation.Name}\"");
            _operations.Add(operation);
        }

        public bool TryParseExpression(string expression, out IExpression compiledExpression)
        {
            try
            {
                compiledExpression = ParseExpression(expression);
                return true;
            }
            catch(Exception)
            {
                compiledExpression = null;
                return false;
            }
        }
        
        public IExpression ParseExpression(string expression)
        {
            //start by removing all of the spaces
            string condensed = new string((from ch in expression.Trim() where ch != ' ' select ch).ToArray());
            
            //return the grouped expression
            try
            {
                var ret =  new Expression(expression, Group(condensed));
                return ret;
            }
            catch(FormatException)
            {
                throw new ArgumentException("Expression Contained Improperly Formatted Numbers");
            }
            catch(IndexOutOfRangeException)
            {
                throw new ArgumentException("Expression Contained Malformed Syntax");
            }
        }

        #endregion
    }
}
