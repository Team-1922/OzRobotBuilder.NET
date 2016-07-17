using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// The data for a quadrature encoder
    /// </summary>
    public class OzQuadEncoderData : INamedClass, IIdentificationNumber, IValidatable
    {
        /// <summary>
        /// The name of this encoder
        /// </summary>
        public string Name { get; set; } = "OzQuadEncoderData";
        /// <summary>
        /// The ratio defined as output units per encoder unit
        /// </summary>
        public double ConversionRatio { get; set; } 
        /// <summary>
        /// The first digital input used (where the three wires are plugged into)
        /// </summary>
        public uint ID { get; set; } 
        /// <summary>
        /// The second digital input used (where the one wire is plugged into)
        /// </summary>
        public uint ID1 { get; set; }
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
                // TODO: make this configurable (some configurations actually have more than 9 digital inputs
                if (ID > 9)
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "ID"), ID.ToString()));
                if (ID1 > 9)
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "ID1"), ID1.ToString()));
            }

            return ret;
        }
    }
}
