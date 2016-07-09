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
    /// </summary>
    public class ValidationReport
    {
        public ValidationReport(ValidationSettings settings)
        {
            SettingsUsed = settings;
        }
        public readonly ValidationSettings SettingsUsed;
        public List<IValidationIssue> ValidationIssues { get; } = new List<IValidationIssue>();
    }
}
