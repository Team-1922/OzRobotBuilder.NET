using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// This is more complicated than the potentiometer (and the potentiometer might be merged into this) because of polling rates, accumulation, etc.
    /// </summary>
    public class OzAnalogInputData : INamedClass, IIdentificationNumber, IValidatable
    {
        /// <summary>
        /// the polling rate for ALL analog inputs
        /// </summary>
        public static double GlobalSampleRate;
        /// <summary>
        /// The name of this particular analog input
        /// </summary>
        public string Name { get; set; } = "OzAnalogInputData";
        /// <summary>
        /// The input id
        /// </summary>
        public uint ID { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AccumulatorCenter { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AccumulatorDeadband { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public long AccumulatorInitialValue { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AverageBits { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int OversampleBits { get; set; }
        /// <summary>
        /// The conversion ratio defined as normalized analog units per user-defined units
        /// </summary>
        public double ConversionRatio { get; set; }
        /// <summary>
        /// The offset in normalized analog units
        /// </summary>
        public double SensorOffset { get; set; }
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
                if (!ValidationUtils.CheckName(Name))
                    ret.ValidationIssues.Add(new IllogicalValueValidationIssue(ValidationUtils.ExtendWorkingPath(workingPath, "Name"), Name));

                //TODO: also check the other members which I currently have no idea what they do
            }

            return ret;
        }
    }
}
