using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel.ResourceManager.PrimaryTypes
{       
    //Is there some way to have an equality between these below enums and the ones in the WPI lib without importing the WPIlib?

    //
    // Summary:
    //     Mode for determining how the WPILib.Interfaces.ICANSpeedController is controlled,
    //     used internally.
    public enum ControlMode
    {
        //
        // Summary:
        //     Percent Vbus Mode (Similar to PWM).
        PercentVbus = 0,
        //
        // Summary:
        //     Runs the controller in Closed Loop Position mode.
        Position = 1,
        //
        // Summary:
        //     Runs the controller in Closed Loop Speed mode.
        Speed = 2,
        //
        // Summary:
        //     Runs the controller in Closed Loop Current mode.
        Current = 3,
        //
        // Summary:
        //     Runs the controller by directly setting the output voltage.
        Voltage = 4,
        //
        // Summary:
        //     Follower Mode (sets the controller to follow another controller).
        Follower = 5,
        //
        // Summary:
        //     Runs the controller in Motion Profile mode.
        MotionProfile = 6,
        //
        // Summary:
        //     If this mode is set, the controller is disabled.
        Disabled = 15
    }
    //
    // Summary:
    //     Determines how the WPILib.Interfaces.ICANSpeedController behaves when sending
    //     a zero signal.
    public enum NeutralMode
    {
        //
        // Summary:
        //     Use the WPILib.Interfaces.NeutralMode that is set by the jumper wire on the CAN
        //     device.
        Jumper = 0,
        //
        // Summary:
        //     Stop the motor's rotation by applying a force.
        Brake = 1,
        //
        // Summary:
        //     Do not attempt to stop the motor. Instead allow it to coast to a stop without
        //     applying resistance.
        Coast = 2
    }
    //
    // Summary:
    //     The PID source type for this PID source.
    public enum PIDSourceType
    {
        //
        // Summary:
        //     Use displacement as the source.
        Displacement = 0,
        //
        // Summary:
        //     Use rate as the source
        Rate = 1
    }
    //
    // Summary:
    //     Determines which sensor to use for position reference.
    public enum LimitMode
    {
        //
        // Summary:
        //     Only use switches for limits
        SwitchInputsOnly = 0,
        //
        // Summary:
        //     Use both switches and soft limits
        SoftPositionLimits = 1,
        //
        // Summary:
        //     Disable switches and disable soft limits. Only valid for methods on WPILib.CANTalon
        //     objects.
        SrxDisableSwitchInputs = 2
    }
}
