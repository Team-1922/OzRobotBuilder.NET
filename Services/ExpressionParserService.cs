using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.Services
{
    /// <summary>
    /// Represents the global expression parser instance.  This is mostly stateless, however additional operations can be added at a later time
    /// </summary>
    public class ExpressionParserService
    {
        private static IExpressionParser _instance;
        /// <summary>
        /// The global IExpressionParser instance
        /// </summary>
        public static IExpressionParser Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new ExpressionParser.ExpressionParser();
                return _instance;
            }
        }
        /// <summary>
        /// Whether or not the the expression parser should be able to access the <see cref="DataAccessService"/>
        /// </summary>
        public static bool DataAccessEnabled { get; set; } = true;
    }
}
