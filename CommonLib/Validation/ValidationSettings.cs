using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    /// <summary>
    /// each of the different settings for the validation process
    /// </summary>
    public enum ValidationSetting
    {
        /// <summary>
        /// elements in the document that are never referenced
        /// </summary>
        UnusedElements,
        /// <summary>
        /// are any of the values null? i.e. uninitialized string
        /// NOTE: this is always enabled 
        /// </summary>
        NullValues,
        /// <summary>
        /// IDs being used more than once
        /// </summary>
        ReusedIds,
        /// <summary>
        /// Names being used more than once for the same object type
        /// </summary>
        ReusedNames,
        /// <summary>
        /// i.e. negative PWM id, Nonexistant command name in trigger, etc.
        /// </summary>
        IllogicalValues,
        /// <summary>
        /// Only checks if all values are deafults
        /// TODO: do we even need this?
        /// </summary>
        DefaultValues
    }
    public class ValidationSettings : HashSet<ValidationSetting>
    { }
}
