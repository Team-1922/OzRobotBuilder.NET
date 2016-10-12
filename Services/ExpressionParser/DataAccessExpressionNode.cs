using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Services.ExpressionParser.Operations;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// Represents an expression node which accesses data derived from <see cref="IHierarchialAccessRoot"/>
    /// </summary>
    internal class DataAccessExpressionNode : ExpressionNodeBase
    {
        private DataAccessOperation _operation;
        IHierarchialAccessRoot _data;

        public string Path { get; }

        /// <summary>
        /// Constructs a new instance with the given data and path
        /// </summary>
        /// <param name="path">the location (path) of that data to access</param>
        public DataAccessExpressionNode(string path, DataAccessOperation operation, IHierarchialAccessRoot data)
        {
            _operation = operation;
            Path = path;
            _data = data;
        }

        /// <summary>
        /// Instead of executing an operation, this accesses a given set of data
        /// </summary>
        /// <returns>the data at <see cref="_path"/> in <see cref="_dataRoot"/></returns>
        public override async Task<double> EvaluateAsync()
        {
            return await _operation.PerformAsync(Path, _data);
        }
    }
}
