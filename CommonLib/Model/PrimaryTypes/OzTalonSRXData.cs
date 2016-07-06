using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// All of the information which a Talon SRX can have
    /// This is the big kahuna of all of the primary data types
    /// </summary>
    public class OzTalonSRXData : OzMotorControllerData
    {
        /// <summary>
        /// Analog input data
        /// </summary>
        public OzAnalogInputData AnalogInput { get; set; } = new OzAnalogInputData();
        /// <summary>
        /// Digital input data
        /// </summary>
        public OzQuadEncoderData DigitalInput { get; set; } = new OzQuadEncoderData();
        /// <summary>
        /// PID Controller Configuration 0
        /// </summary>
        public OzSRXPIDControllerData PIDConfig0 { get; set; } = new OzSRXPIDControllerData();
        /// <summary>
        /// PID Controller Configuration 1
        /// </summary>
        public OzSRXPIDControllerData PIDConfig1 { get; set; } = new OzSRXPIDControllerData();
        /// <summary>
        /// The Enabled PID Configuration profile; 0 = <see cref="OzTalonSRXData.PIDConfig0"/>; >0 = <see cref="OzTalonSRXData.PIDConfig0"/>
        /// </summary>
        public int EnabledPIDProfile { get; set; } 
        /// <summary>
        /// The Device which is being used in closed loop PID control
        /// </summary>
        public FeedbackDevice SRXFeedbackDevice { get; set; } 
        /// <summary>
        /// Control mode of the talon srx
        /// </summary>
        public ControlMode SRXControlMode { get; set; } 
        /// <summary>
        /// Sets what the controller does to the H-Bridge when neutral (not driving the output).
        /// </summary>
        public NeutralMode SRXNeutralMode { get; set; } 
        /// <summary>
        /// Enables Talon SRX to automatically zero the Sensor Position whenever an edge
        ///     is detected on the index signal.
        /// </summary>
        public bool ZeroSensorPositionOnIndexEnabled { get; set; } 
        /// <summary>
        /// if <see cref="ZeroSensorPositionOnIndexEnabled"/> is true, then this is whether or not to zero the sensor on the rising edge or the falling edge
        /// </summary>
        public bool ZeroSensorPositionOnRisingEdge { get; set; } 
        /// <summary>
        /// Whether to reverse the input sensor or not (inputValue * -1)
        /// </summary>
        public bool ReverseSensor { get; set; } 
        /// <summary>
        /// Whether to reverse the output or not in closed loop mode (outputValue * -1)
        /// </summary>
        public bool ReverseClosedLoopOutput { get; set; } 
        /// <summary>
        /// Whether to reverse the output in <see cref="ControlMode.PercentVbus"/> or not (outputValue * -1)
        /// </summary>
        public bool ReversePercentVBusOutput { get; set; } 
        /// <summary>
        /// Whether the forward limit switch is enabled or not
        /// </summary>
        public bool ForwardLimitSwitchEnabled { get; set; } 
        /// <summary>
        /// Whether the reverse limit switch is enabled or not
        /// </summary>
        public bool ReverseLimitSwitchEnabled { get; set; } 
        /// <summary>
        /// Whether the forward soft limit is enabled or not (<see cref="ForwardSoftLimit"/>)
        /// </summary>
        public bool ForwardSoftLimitEnabled { get; set; } 
        /// <summary>
        /// Whether the reverse soft limit is enabled or not (<see cref="ReverseSoftLimit"/>)
        /// </summary>
        public bool ReverseSoftLimitEnabled { get; set; } 
        /// <summary>
        /// The forward soft limit
        /// </summary>
        public double ForwardSoftLimit { get; set; } 
        /// <summary>
        /// The reverse soft limit
        /// </summary>
        public double ReverseSoftLimit { get; set; } 
        /// <summary>
        /// The nominal forward voltage
        /// </summary>
        public double NominalForwardVoltage { get; set; } 
        /// <summary>
        /// The nominal reverse voltage
        /// </summary>
        public double NominalReverseVoltage { get; set; }
        /// <summary>
        /// The maximum forward output voltage
        /// </summary>
        public double PeakForwardVoltage { get; set; }
        /// <summary>
        /// The maximum reverse output voltage
        /// </summary>
        public double PeakReverseVoltage { get; set; }

        /// <summary>
        /// Feedback type for the CAN Talon
        /// </summary>
        public enum FeedbackDevice
        {
            /// <summary>
            /// A quadrature encoder.
            /// </summary>
            QuadEncoder = 0,
            /// <summary>
            /// An analog potentiometer.
            /// </summary>
            AnalogPotentiometer = 2,
            /// <summary>
            /// An analog encoder.
            /// </summary>
            AnalogEncoder = 3,
            /// <summary>
            /// An encoder that only reports when it hits a rising edge.
            /// </summary>
            EncoderRising = 4,
            /// <summary>
            /// An encoder that only reports when it hits a falling edge.
            /// </summary>
            EncoderFalling = 5,
            /// <summary>
            /// Relative magnetic encoder.
            /// </summary>
            CtreMagEncoderRelative = 6,
            /// <summary>
            /// Absolute magnetic encoder
            /// </summary>
            CtreMagEncoderAbsolute = 7,
            /// <summary>
            /// Encoder is a pulse width sensor
            /// </summary>
            PulseWidth = 8
        }

        /// <summary>
        /// The additional PID controller data specific for the CAN Talon
        /// </summary>
        public class OzSRXPIDControllerData : OzPIDControllerData
        {
            /// <summary>
            /// The integration accumulation zone
            /// TODO: what is this?
            /// </summary>
            public int IZone { get; set; }
            /// <summary>
            /// Gets or sets the closed loop ramp rate for the current profile. In Volts/Sec
            /// </summary>
            public double CloseLoopRampRate { get; set; }
            /// <summary>
            /// The max allowable closed loop error for the selected profile.
            /// This is like the 'tolerance' value for software PID controllers
            /// In native SRX units (0 to 1023)
            /// </summary>
            public int AllowableCloseLoopError { get; set; }
            /// <summary>
            /// The PID source type (position or velocity)
            /// </summary>
            public PIDSourceType SourceType { get; set; }
        }
        
    }
}
