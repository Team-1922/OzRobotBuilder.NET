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
        public ValidationReport(ValidationSettings settings)
        {
            SettingsUsed = settings;
        }
        public readonly ValidationSettings SettingsUsed;
        public List<IValidationIssue> ValidationIssues { get; } = new List<IValidationIssue>();
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
