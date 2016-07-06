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
    public class OzMotorControllerData : INamedClass
    {
        /// <summary>
        /// The name of this motor controller
        /// </summary>
        public string Name = "OzMotorControllerData";
        /// <summary>
        /// The pwm ID of the motor on the RoboRIO
        /// </summary>
        public uint MotorId;

        #region INamedClass Interface
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string GetName()
        {
            return Name;
        }
        public void SetName(string name)
        {
            Name = name;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion
    }
}
