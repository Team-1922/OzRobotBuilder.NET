using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services
{
    /// <summary>
    /// Represents the global expression parser instance.  This is mostly stateless, however additional operations can be added at a later time
    /// </summary>
    public class ExpressionParserService
    {
        private static IExpressionParser _dataAccess;
        /// <summary>
        /// The global IExpressionParser instance
        /// </summary>
        public static IExpressionParser Instance
        {
            get
            {
                if (null == _dataAccess)
                    _dataAccess = new ExpressionParser.ExpressionParser();
                return _dataAccess;
            }
        }
    }
}
