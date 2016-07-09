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
    public class OzMotorControllerData : INamedClass, IIdentificationNumber
    {
        /// <summary>
        /// The name of this motor controller
        /// </summary>
        public string Name { get; set; } = "OzMotorControllerData";
        /// <summary>
        /// The pwm ID of the motor on the RoboRIO
        /// </summary>
        public uint ID { get; set; }
    }
}
