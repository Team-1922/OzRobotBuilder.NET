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
    public class OzPIDControllerData
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
    }
}
