using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    public interface IObjectChecker<T>
    {
        /// <summary>
        /// checks whether the given object is valid and returns any issues
        /// </summary>
        /// <param name="obj">the object to validate</param>
        /// <param name="settings">which issues should be reported?</param>
        /// <param name="workingPath">the current path level of this validation (helps logging)</param>
        /// <returns>The validation report; guaranteed NOT to be null... ever</returns>
        ValidationReport ValidateObject(T obj, ValidationSettings settings, string workingPath);
    }
}
