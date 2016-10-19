using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The CANTalon has compound IO, therefore it needs its own type
    /// </summary>
    public interface ICANTalonIOService : IOutputService
    {
        /// <summary>
        /// The Proportinal constant
        /// </summary>
        double P { get; set; }
        /// <summary>
        /// The Integration constant
        /// </summary>
        double I { get; set; }
        /// <summary>
        /// The Differential Constant
        /// </summary>
        double D { get; set; }
        /// <summary>
        /// The feed-forward constant
        /// </summary>
        double F { get; set; }
        /// <summary>
        /// The integration zone
        /// </summary>
        int IZone { get; set; }
        /// <summary>
        /// The closed loop ramp in V/S
        /// </summary>
        double CloseLoopRampRate { get; set; }
        /// <summary>
        /// Similar to the <see cref="PIDControllerSoftware.Tolerance"/>
        /// </summary>
        int AllowableCloseLoopError { set; }
        /// <summary>
        /// The source type of the controller (i.e. speed or displacement)
        /// </summary>
        CANTalonDifferentiationLevel SourceType { get; set; }
        /// <summary>
        /// The enabled PID profile (0=false;1=true)
        /// </summary>
        bool EnabledPIDProfile { get; set; }
        /// <summary>
        /// The close-loop feedback device
        /// </summary>
        CANTalonFeedbackDevice FeedbackDevice { get; set; }
        /// <summary>
        /// The control mode
        /// </summary>
        CANTalonControlMode ControlMode { get; set; }
        /// <summary>
        /// The neutral mode
        /// </summary>
        CANTalonNeutralMode NeutralMode { get; set; }
        /// <summary>
        /// whether or not to reverse the input sensor (effectively multiplies the sensor input by -1)
        /// </summary>
        bool ReverseSensor { set; }
        /// <summary>
        /// Whether or not to reverse the output in close-loop mode
        /// </summary>
        bool ReverseCloseLoopOutput { set; }
        /// <summary>
        /// Whether or not to reverse the ouput in Percent VBus mode
        /// </summary>
        bool ReversePercentVBusOutput { get; set; }
        /// <summary>
        /// Whether the limit switches (hardware) are enabled
        /// </summary>
        void EnableLimitSwitches(bool forward, bool reverse);
        /// <summary>
        /// Whether the forward limit (software) is enabled
        /// </summary>
        bool ForwardSoftLimitEnabled { get; set; }
        /// <summary>
        /// Whether the reverse limit (software) is enabled
        /// </summary>
        bool ReverseSoftLimitEnabled { get; set; }
        /// <summary>
        /// The forward soft limit value (in native units)
        /// </summary>
        double ForwardSoftLimit { get; set; }
        /// <summary>
        /// The reverse soft limit value (in native units)
        /// </summary>
        double ReverseSoftLimit { get; set; }
        /// <summary>
        /// Sets the nominal voltages
        /// </summary>
        /// <param name="forward">the nominal forward voltage (typically +0)</param>
        /// <param name="reverse">the nominal reverse voltage (typically +0)</param>
        void ConfigureNominalVoltage(double forward, double reverse);
        /// <summary>
        /// Sets the peak voltages
        /// </summary>
        /// <param name="forward">the peak forward voltage (typically +12)</param>
        /// <param name="reverse">the peak reverse voltage (typically -12)</param>
        void ConfigurePeakVoltage(double forward, double reverse);

        /// <summary>
        /// Enables Talon SRX to automatically zero the Sensor Position whenever an edge
        /// is detected on the index signal.
        /// </summary>
        /// <param name="enable">whether to enable this feature</param>
        /// <param name="risingEdge">whether or not to zero the sensor on the rising edge or the falling edge</param>
        void EnableZeroSensorPositionOnIndex(bool enable, bool risingEdge);

        /// <summary>
        /// The state of the forward limit switch (hardware)
        /// </summary>
        bool ForwardLimitSwitch { get; }
        /// <summary>
        /// The state of the reverse limit switch (hardware)
        /// </summary>
        bool ReverseLimitSwitch { get; }

        /// <summary>
        /// The encoder value (in encoder units)
        /// </summary>
        long EncoderValue { get; }
        /// <summary>
        /// The encoder velocity (in encoder units/second)
        /// </summary>
        double EncoderVelocity { get; }

        /// <summary>
        /// The analog input value (in native talon units (0-1023)
        /// </summary>
        int AnalogValue { get; }
        /// <summary>
        /// The analog input velocity (in native talon units (0-1023)/second)
        /// </summary>
        double AnalogVelocity { get; }
    }
}
