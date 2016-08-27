using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922
{
    /// <summary>
    /// The type of exceptions which are thrown after facet validations fail.  
    /// </summary>
    public class FacetValidationException : Exception
    {
        /// <summary>
        /// Initialize this exception with a message
        /// </summary>
        /// <param name="message">the exception message</param>
        public FacetValidationException(string message) : base(message) { }
    }
}
