using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    /// <summary>
    /// Interface for classes which can be validated
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Goes through each validation setting and member 
        /// </summary>
        /// <param name="settings">the active settings for validation</param>
        /// <param name="workingPath">the path for instance; used for traversal of hierarchial data types</param>
        /// <returns>a report of the validation issues</returns>
        ValidationReport Validate(ValidationSettings settings, string workingPath);
    }
}
