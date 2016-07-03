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
        public OzAnalogInputData AnalogInput = new OzAnalogInputData();
        /// <summary>
        /// Digital input data
        /// </summary>
        public OzQuadEncoderData DigitalInput = new OzQuadEncoderData();
        /// <summary>
        /// PID Controller Configuration 0
        /// </summary>
        public OzSRXPIDControllerData PIDConfig0 = new OzSRXPIDControllerData();
        /// <summary>
        /// PID Controller Configuration 1
        /// </summary>
        public OzSRXPIDControllerData PIDConfig1 = new OzSRXPIDControllerData();
        /// <summary>
        /// The Enabled PID Configuration profile; 0 = <see cref="OzTalonSRXData.PIDConfig0"/>; >0 = <see cref="OzTalonSRXData.PIDConfig0"/>
        /// </summary>
        public int EnabledPIDProfile;
        /// <summary>
        /// The Device which is being used in closed loop PID control
        /// </summary>
        public FeedbackDevice SRXFeedbackDevice;
        /// <summary>
        /// Control mode of the talon srx
        /// </summary>
        public ControlMode SRXControlMode;
        /// <summary>
        /// Sets what the controller does to the H-Bridge when neutral (not driving the output).
        /// </summary>
        public NeutralMode SRXNeutralMode;
        /// <summary>
        /// Enables Talon SRX to automatically zero the Sensor Position whenever an edge
        ///     is detected on the index signal.
        /// </summary>
        public bool ZeroSensorPositionOnIndexEnabled;
        /// <summary>
        /// if <see cref="ZeroSensorPositionOnIndexEnabled"/> is true, then this is whether or not to zero the sensor on the rising edge or the falling edge
        /// </summary>
        public bool ZeroSensorPositionOnRisingEdge;
        /// <summary>
        /// Whether to reverse the input sensor or not (inputValue * -1)
        /// </summary>
        public bool ReverseSensor;
        /// <summary>
        /// Whether to reverse the output or not in closed loop mode (outputValue * -1)
        /// </summary>
        public bool ReverseClosedLoopOutput;
        /// <summary>
        /// Whether to reverse the output in <see cref="ControlMode.PercentVbus"/> or not (outputValue * -1)
        /// </summary>
        public bool ReversePercentVBusOutput;
        /// <summary>
        /// Whether the forward limit switch is enabled or not
        /// </summary>
        public bool ForwardLimitSwitchEnabled;
        /// <summary>
        /// Whether the reverse limit switch is enabled or not
        /// </summary>
        public bool ReverseLimitSwitchEnabled;
        /// <summary>
        /// Whether the forward soft limit is enabled or not (<see cref="ForwardSoftLimit"/>)
        /// </summary>
        public bool ForwardSoftLimitEnabled;
        /// <summary>
        /// Whether the reverse soft limit is enabled or not (<see cref="ReverseSoftLimit"/>)
        /// </summary>
        public bool ReverseSoftLimitEnabled;
        /// <summary>
        /// The forward soft limit
        /// </summary>
        public double ForwardSoftLimit;
        /// <summary>
        /// The reverse soft limit
        /// </summary>
        public double ReverseSoftLimit;
        /// <summary>
        /// The nominal forward voltage
        /// </summary>
        public double NominalForwardVoltage;
        /// <summary>
        /// The nominal reverse voltage
        /// </summary>
        public double NominalReverseVoltage;
        /// <summary>
        /// The maximum forward output voltage
        /// </summary>
        public double PeakForwardVoltage;
        /// <summary>
        /// The maximum reverse output voltage
        /// </summary>
        public double PeakReverseVoltage;
        
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
            public int IZone;
            /// <summary>
            /// Gets or sets the closed loop ramp rate for the current profile. In Volts/Sec
            /// </summary>
            public double CloseLoopRampRate;
            /// <summary>
            /// The max allowable closed loop error for the selected profile.
            /// This is like the 'tolerance' value for software PID controllers
            /// In native SRX units (0 to 1023)
            /// </summary>
            public int AllowableCloseLoopError;
            /// <summary>
            /// The PID source type (position or velocity)
            /// </summary>
            public PIDSourceType SourceType;
        }
        
    }
}
