using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// The data needed to have a script-extensable class
    /// </summary>
    public class ScriptExtensableData : IValidatable
    {
        /// <summary>
        /// The additional methods added to this object
        /// </summary>
        public string ScriptExtension { get; set; }
        /// <summary>
        /// The existing methods which are overidden 
        /// TODO: get a good example of this
        /// </summary>
        public string OverriddenMethods { get; set; }

        /// <summary>
        /// TODO: validates the script extention or overridden methods somehow
        /// </summary>
        /// <param name="settings">the active settings for validation</param>
        /// <param name="workingPath">the path for instance; used for traversal of hierarchial data types</param>
        /// <returns>a report of the validation issues</returns>
        public ValidationReport Validate(ValidationSettings settings, string workingPath)
        {
            return new ValidationReport(settings);
        }
    }
}
