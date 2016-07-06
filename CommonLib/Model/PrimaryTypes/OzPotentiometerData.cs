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
    /// </summary>
    public class OzPotentiometerData
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
    }
}
