using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    /// <summary>
    /// Contains all of the information from a completed validation process
    /// TODO: make this be converted to a readonly equivalent somehow
    /// TODO: make this support JSON serialization somehow
    /// </summary>
    public class ValidationReport
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="settings">the settings used in this validation report</param>
        public ValidationReport(ValidationSettings settings)
        {
            SettingsUsed = settings;
        }
        /// <summary>
        /// the settings used in this validation report
        /// </summary>
        public readonly ValidationSettings SettingsUsed;
        /// <summary>
        /// A running list of validation issues
        /// </summary>
        public List<IValidationIssue> ValidationIssues { get; } = new List<IValidationIssue>();
        /// <summary>
        /// Adds all of the validation issues from another report but also all of the settings
        /// </summary>
        /// <param name="other">the report to merge from</param>
        public void Augment(ValidationReport other)
        {
            if (null == other)
                return;// TODO: throw exception

            foreach(var setting in other.SettingsUsed)
            {
                if (!SettingsUsed.Contains(setting))
                    SettingsUsed.Add(setting);
            }

            ValidationIssues.AddRange(other.ValidationIssues);
        }
    }
}
