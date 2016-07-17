using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// PID Configuration for software PID Controller (use OzSRXPIDControllerData for SRX PID configuration)
    /// </summary>
    public class OzPIDControllerData : IValidatable
    {
        /// <summary>
        /// The Proportional Constant
        /// </summary>
        public double P { get; set; }
        /// <summary>
        /// The Integration Constant
        /// </summary>
        public double I { get; set; }
        /// <summary>
        /// The Differential Constant
        /// </summary>
        public double D { get; set; }
        /// <summary>
        /// The Fixed Constant
        /// </summary>
        public double F { get; set; }
        /// <summary>
        /// The allowable tolerance in the PID input (replaced with <see cref="OzTalonSRXData.OzSRXPIDControllerData.AllowableCloseLoopError"/> on Talon SRXs)
        /// </summary>
        public double Tolerance { get; set; }
        /// <summary>
        /// The time in seconds between update cycles (this is irrelevent on the SRX)
        /// </summary>
        public double CycleDuration { get; set; }
        /// <summary>
        /// Whether or not the input is like a continuous rotation potentiometer (usually no)
        /// </summary>
        public bool Continuous { get; set; }
        /// <summary>
        /// Goes through each validation setting and member 
        /// </summary>
        /// <param name="settings">the active settings for validation</param>
        /// <param name="workingPath">the path for instance; used for traversal of hierarchial data types</param>
        /// <returns>a report of the validation issues</returns>
        public ValidationReport Validate(ValidationSettings settings, string workingPath)
        {
            ValidationReport ret = new ValidationReport(settings);
            workingPath = ValidationUtils.ExtendWorkingPath(workingPath, "OzPIDControllerData");

            if (settings.Contains(ValidationSetting.IllogicalValues))
            {
                // TODO: make this do safety checks (i.e. large I values)
            }

            return ret;
        }
    }
}
