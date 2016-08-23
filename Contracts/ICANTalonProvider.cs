using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for CANTalon viewmodels
    /// </summary>
    public interface ICANTalonProvider : IInputProvider, ICompoundProvider, IProvider<CANTalon>
    {
        /// <summary>
        /// The quadrature encoder
        /// </summary>
        ICANTalonQuadEncoderProvider QuadEncoder { get; }
        /// <summary>
        /// The analog input
        /// </summary>
        ICANTalonAnalogInputProvider AnalogInput { get; }
        /// <summary>
        /// The 0th PID config 
        /// </summary>
        IPIDControllerSRXProvider PIDConfig0 { get; }
        /// <summary>
        /// The 1st PID config
        /// </summary>
        IPIDControllerSRXProvider PIDConfig1 { get; }
        /// <summary>
        /// The CAN ID
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The enabled PID profile (0=false;1=true)
        /// </summary>
        bool EnabledPIDProfile { get; set; }
        /// <summary>
        /// The closed-loop feedback device
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
        /// Enables Talon SRX to automatically zero the Sensor Position whenever an edge
        /// is detected on the index signal.
        /// </summary>
        bool ZeroSensorPositionOnIndexEnabled { get; set; }
        /// <summary>
        /// if <see cref="ZeroSensorPositionOnIndexEnabled"/> is true, then this is whether or not to zero the sensor on the rising edge or the falling edge
        /// </summary>
        bool ZeroSensorPositionOnRisingEdge { get; set; }
        /// <summary>
        /// whether or not to reverse the input sensor (effectively multiplies the sensor input by -1)
        /// </summary>
        bool ReverseSensor { get; set; }
        /// <summary>
        /// Whether or not to reverse the output in closed-loop mode
        /// </summary>
        bool ReverseClosedLoopOutput { get; set; }
        /// <summary>
        /// Whether or not to reverse the ouput in Percent VBus mode
        /// </summary>
        bool ReversePercentVBusOutput { get; set; }
        /// <summary>
        /// Whether the forward limit switch (hardware) is enabled
        /// </summary>
        bool ForwardLimitSwitchEnabled { get; set; }
        /// <summary>
        /// Whether to reverse limit switch (hardware) is enabled
        /// </summary>
        bool ReverseLimitSwitchEnabled { get; set; }
        /// <summary>
        /// Whether the forward limit (software) is enabled
        /// </summary>
        bool ForwardSoftLimitEnabled { get; set; }
        /// <summary>
        /// Whether the reverse limit (software) is enabled
        /// </summary>
        bool ReverseSoftLimitEnabled { get; set; }
        /// <summary>
        /// The forward soft limit value (in user units)
        /// </summary>
        double ForwardSoftLimit { get; set; }
        /// <summary>
        /// The reverse soft limit value (in user units)
        /// </summary>
        double ReverseSoftLimit { get; set; }
        /// <summary>
        /// The nominal forward voltage (typically 0V)
        /// </summary>
        double NominalForwardVoltage { get; set; }
        /// <summary>
        /// The nominal reverse voltage (typicall 0V)
        /// </summary>
        double NominalReverseVoltage { get; set; }
        /// <summary>
        /// The peak forward voltage (typically 12V)
        /// </summary>
        double PeakForwardVoltage { get; set; }
        /// <summary>
        /// The peak reverse voltage (typically -12V)
        /// </summary>
        double PeakReverseVoltage { get; set; }

        /// <summary>
        /// The value this output is set to;
        /// In PercentVBus mode, this is -1 to 1;
        /// In Follower mode, this is the CAN ID of the controller to mirror
        /// In All other modes, this is in user-defined units
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// The state of the forward limit switch (hardware)
        /// </summary>
        bool ForwardLimitSwitch { get; }
        /// <summary>
        /// The state of the reverse limit switch (hardware)
        /// </summary>
        bool ReverseLimitSwitch { get; }

        /// <summary>
        /// The state of the forward soft limit (software)
        /// </summary>
        bool ForwardSoftLimitTripped { get; }
        /// <summary>
        /// The state of the reverse soft limit (software)
        /// </summary>
        bool ReverseSoftLimitTripped { get; }
        
    }
}
