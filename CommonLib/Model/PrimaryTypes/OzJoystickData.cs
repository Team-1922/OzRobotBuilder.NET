using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// The information needed for a simple joystick
    /// </summary>
    public class OzJoystickData : INamedClass, IIdentificationNumber, IValidatable
    {
        /// <summary>
        /// The name of this joystick
        /// </summary>
        public string Name { get; set; } = "OzJoystickData";
        /// <summary>
        /// The id of this joystick on the driver station
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
                //i think 6 is the max, but more than 6 would be dumb anyways
                if (ID > 6)
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "ID"), ID.ToString()));
                if (!ValidationUtils.CheckName(Name))
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "Name"), Name));
            }

            return ret;
        }
    }
}
