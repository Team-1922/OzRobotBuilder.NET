using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Services.ExpressionParser.Operations;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// Represents an expression node which accesses data derived from <see cref="IHierarchialAccess"/>
    /// </summary>
    internal class DataAccessExpressionNode : ExpressionNodeBase
    {
        private DataAccessOperation _operation;
        IHierarchialAccess _data;

        public string Path { get; }

        /// <summary>
        /// Constructs a new instance with the given data and path
        /// </summary>
        /// <param name="path">the location (path) of that data to access</param>
        public DataAccessExpressionNode(string path, DataAccessOperation operation, IHierarchialAccess data)
        {
            _operation = operation;
            Path = path;
            _data = data;
        }

        /// <summary>
        /// Instead of executing an operation, this accesses a given set of data
        /// </summary>
        /// <returns>the data at <see cref="_path"/> in <see cref="_dataRoot"/></returns>
        public override double Evaluate()
        {
            return _operation.Perform(Path, _data);
        }
    }
}
