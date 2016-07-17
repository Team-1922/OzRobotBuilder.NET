using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// Data for a simple PWM motor controller
    /// </summary>
    public class OzMotorControllerData : INamedClass, IIdentificationNumber, IValidatable
    {
        /// <summary>
        /// The name of this motor controller
        /// </summary>
        public string Name { get; set; } = "OzMotorControllerData";
        /// <summary>
        /// The pwm ID of the motor on the RoboRIO
        /// </summary>
        public uint ID { get; set; }
        /// <summary>
        /// Goes through each validation setting and member 
        /// </summary>
        /// <param name="settings">the active settings for validation</param>
        /// <param name="workingPath">the path for instance; used for traversal of hierarchial data types</param>
        /// <returns>a report of the validation issues</returns>
        public ValidationReport Validate(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, Name);

            if (settings.Contains(ValidationSetting.IllogicalValues))
            {
                // TODO: make this configurable (some configurations actually have more than 9 PWM outputs
                if (ID > 9)
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "ID"), ID.ToString()));
                if (!ValidationUtils.CheckName(Name))
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "Name"), Name));
            }
            return ret;
        }
    }
}
