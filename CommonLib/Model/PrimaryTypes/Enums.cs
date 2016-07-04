using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{       
    //Is there some way to have an equality between these below enums and the ones in the WPI lib without importing the WPIlib?
    
    /// <summary>
    /// Mode for determining how the WPILib.Interfaces.ICANSpeedController is controlled,
    /// used internally.
    /// </summary>
    public enum ControlMode
    {
        /// <summary>
        /// Percent Vbus Mode (Similar to PWM).
        /// </summary>
        PercentVbus = 0,
        /// <summary>
        /// Runs the controller in Closed Loop Position mode.
        /// </summary>
        Position = 1,
        /// <summary>
        /// Runs the controller in Closed Loop Speed mode.
        /// </summary>
        Speed = 2,
        /// <summary>
        /// Runs the controller in Closed Loop Current mode.
        /// </summary>
        Current = 3,
        /// <summary>
        /// Runs the controller by directly setting the output voltage.
        /// </summary>
        Voltage = 4,
        /// <summary>
        /// Follower Mode (sets the controller to follow another controller).
        /// </summary>
        Follower = 5,
        /// <summary>
        /// Runs the controller in Motion Profile mode.
        /// </summary>
        MotionProfile = 6,
        /// <summary>
        /// If this mode is set, the controller is disabled.
        /// </summary>
        Disabled = 15
    }
    /// <summary>
    /// Determines how the WPILib.Interfaces.ICANSpeedController behaves when sending
    /// a zero signal.
    /// </summary>
    public enum NeutralMode
    {
        /// <summary>
        /// Use the WPILib.Interfaces.NeutralMode that is set by the jumper wire on the CAN
        /// </summary>
        Jumper = 0,
        /// <summary>
        /// Stop the motor's rotation by applying a force.
        /// </summary>
        Brake = 1,
        /// <summary>
        /// Do not attempt to stop the motor.  Instead allow it to coast to a stop without
        /// applying resistance.
        /// </summary>
        Coast = 2
    }
    /// <summary>
    /// The PID source type for this PID source.
    /// </summary>
    public enum PIDSourceType
    {
        /// <summary>
        /// Use displacement as the source.
        /// </summary>
        Displacement = 0,
        /// <summary>
        /// Use rate as the source.
        /// </summary>
        Rate = 1
    }
    /// <summary>
    /// Determines which sensor to use for position reference.
    /// </summary>
    public enum LimitMode
    {
        /// <summary>
        /// Only use switchces for limits
        /// </summary>
        SwitchInputsOnly = 0,
        /// <summary>
        /// Use both switches and soft limits
        /// </summary>
        SoftPositionLimits = 1,
        /// <summary>
        /// Disable switches and disable soft limits.  Only valid for methods on WPILib.CANTalon
        /// objects.
        /// </summary>
        SrxDisableSwitchInputs = 2
    }
}
