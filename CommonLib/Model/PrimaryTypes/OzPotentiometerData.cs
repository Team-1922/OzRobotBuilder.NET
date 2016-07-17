using CommonLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// The data for a potentiometer
    /// All potentiometers will return a value between 0 and 1 (normalized potentiometer units)
    /// TODO: merge with analog input
    /// </summary>
    public class OzPotentiometerData : IValidatable
    {
        /// <summary>
        /// The ratio defined as output units per normalized potentiometer unit
        /// </summary>
        public double ConversionRatio { get; set; }
        /// <summary>
        /// The sensor offset (in normalized potentiometer units); the new "zero point" of the sensor
        /// </summary>
        public double OffsetValue { get; set; }
        /// <summary>
        /// The analog input this potentiometer is plugged into (this is always -1 if plugged into a TalonSRX)
        /// </summary>
        public int AnalogInID { get; set; }
        /// <summary>
        /// Goes through each validation setting and member 
        /// </summary>
        /// <param name="settings">the active settings for validation</param>
        /// <param name="workingPath">the path for instance; used for traversal of hierarchial data types</param>
        /// <returns>a report of the validation issues</returns>
        public ValidationReport Validate(ValidationSettings settings, string workingPath)
        {
            throw new NotImplementedException();
        }
    }
}
