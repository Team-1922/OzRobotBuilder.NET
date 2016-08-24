using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models.XML
{
    /// <summary>
    /// The type of exceptions which are thrown after facet validations fail.  
    /// Note: this is for use by the <see cref="TypeRestrictions"/> class and not thrown internally from the facet classes
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
