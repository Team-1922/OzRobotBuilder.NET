using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services.ExpressionParser
{
    /// <summary>
    /// Represents an expression node which accesses data derived from <see cref="IHierarchialAccess"/>
    /// </summary>
    internal class DataAccessExpressionNode : ExpressionNodeBase
    {
        IDataAccessService _dataRoot;
        string _path;

        /// <summary>
        /// Constructs a new instance with the given data and path
        /// </summary>
        /// <param name="dataRoot">that data to access</param>
        /// <param name="path">the location (path) of that data to access</param>
        public DataAccessExpressionNode(IDataAccessService dataRoot, string path)
        {
            _dataRoot = dataRoot;
            _path = path;
        }

        /// <summary>
        /// Instead of executing an operation, this accesses a given set of data
        /// </summary>
        /// <returns>the data at <see cref="_path"/> in <see cref="_dataRoot"/></returns>
        public override double Evaluate()
        {
            double ret;
            if (double.TryParse(_dataRoot.DataInstance[_path], out ret))
                return ret;
            else
                throw new Exception($"DataAccessExpressionNode({_path}) Does Not Contain Numerical Value!");
        }
    }
}
