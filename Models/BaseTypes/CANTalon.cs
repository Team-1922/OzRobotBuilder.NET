using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models.BaseTypes
{
    /// <summary>
    /// All of the information which a Talon SRX can have
    /// This is the big kahuna of all of the primary data types
    /// </summary>
    public class CANTalon : PWMMotorController
    {
        /// <summary>
        /// Analog input data
        /// </summary>
        public AnalogInput AnalogInput
        {
            get { return _analogInput; }
            set { SetProperty(ref _analogInput, value); }
        }
        /// <summary>
        /// Digital input data
        /// </summary>
        public QuadEncoder DigitalInput
        {
            get { return _digitalInput; }
            set { SetProperty(ref _digitalInput, value); }
        }
        /// <summary>
        /// PID Controller Configuration 0
        /// </summary>
        public CANTalonPIDController PIDConfig0
        {
            get { return _pidConfig0; }
            set { SetProperty(ref _pidConfig0, value); }
        }
        /// <summary>
        /// PID Controller Configuration 1
        /// </summary>
        public CANTalonPIDController PIDConfig1
        {
            get { return _pidConfig1; }
            set { SetProperty(ref _pidConfig1, value); }
        }
        /// <summary>
        /// The Enabled PID Configuration profile; 0 = <see cref="PIDConfig0"/>; >0 = <see cref="PIDConfig1"/>
        /// </summary>
        public int EnabledPIDProfile
        {
            get { return _enabledPIDProfile; }
            set { SetProperty(ref _enabledPIDProfile, value); }
        }
        /// <summary>
        /// The Device which is being used in closed loop PID control
        /// </summary>
        public CANTalonFeedbackDevice FeedbackDevice
        {
            get { return _FeedbackDevice; }
            set { SetProperty(ref _FeedbackDevice, value); }
        }
        /// <summary>
        /// Control mode of the talon srx
        /// </summary>
        public ControlMode ControlMode
        {
            get { return _controlMode; }
            set { SetProperty(ref _controlMode, value); }
        }
        /// <summary>
        /// Sets what the controller does to the H-Bridge when neutral (not driving the output).
        /// </summary>
        public NeutralMode NeutralMode
        {
            get { return _neutralMode; }
            set { SetProperty(ref _neutralMode, value); }
        }
        /// <summary>
        /// Enables Talon SRX to automatically zero the Sensor Position whenever an edge
        ///     is detected on the index signal.
        /// </summary>
        public bool ZeroSensorPositionOnIndexEnabled
        {
            get { return _zeroSensorPositionOnIndexEnabled; }
            set { SetProperty(ref _zeroSensorPositionOnIndexEnabled, value); }
        }
        /// <summary>
        /// if <see cref="ZeroSensorPositionOnIndexEnabled"/> is true, then this is whether or not to zero the sensor on the rising edge or the falling edge
        /// </summary>
        public bool ZeroSensorPositionOnRisingEdge
        {
            get { return _zeroSensorPositionOnRisingEdge; }
            set { SetProperty(ref _zeroSensorPositionOnRisingEdge, value); }
        }
        /// <summary>
        /// Whether to reverse the input sensor or not (inputValue * -1)
        /// </summary>
        public bool ReverseSensor
        {
            get { return _reverseSensor; }
            set { SetProperty(ref _reverseSensor, value); }
        }
        /// <summary>
        /// Whether to reverse the output or not in closed loop mode (outputValue * -1)
        /// </summary>
        public bool ReverseClosedLoopOutput
        {
            get { return _reverseClosedLoopOutput; }
            set { SetProperty(ref _reverseClosedLoopOutput, value); }
        }
        /// <summary>
        /// Whether to reverse the output in <see cref="ControlMode.PercentVbus"/> or not (outputValue * -1)
        /// </summary>
        public bool ReversePercentVBusOutput
        {
            get { return _reversePercentVBusOutput; }
            set { SetProperty(ref _reversePercentVBusOutput, value); }
        }
        /// <summary>
        /// Whether the forward limit switch is enabled or not
        /// </summary>
        public bool ForwardLimitSwitchEnabled
        {
            get { return _forwardLimitSwitchEnabled; }
            set { SetProperty(ref _forwardLimitSwitchEnabled, value); }
        }
        /// <summary>
        /// Whether the reverse limit switch is enabled or not
        /// </summary>
        public bool ReverseLimitSwitchEnabled
        {
            get { return _reverseLimitSwitchEnabled; }
            set { SetProperty(ref _reverseLimitSwitchEnabled, value); }
        }
        /// <summary>
        /// Whether the forward soft limit is enabled or not (<see cref="ForwardSoftLimit"/>)
        /// </summary>
        public bool ForwardSoftLimitEnabled
        {
            get { return _forwardSoftLimitEnabled; }
            set { SetProperty(ref _forwardSoftLimitEnabled, value); }
        }
        /// <summary>
        /// Whether the reverse soft limit is enabled or not (<see cref="ReverseSoftLimit"/>)
        /// </summary>
        public bool ReverseSoftLimitEnabled
        {
            get { return _reverseSoftLimitEnabled; }
            set { SetProperty(ref _reverseSoftLimitEnabled, value); }
        }
        /// <summary>
        /// The forward soft limit
        /// </summary>
        public double ForwardSoftLimit
        {
            get { return _forwardSoftLimit; }
            set { SetProperty(ref _forwardSoftLimit, value); }
        }
        /// <summary>
        /// The reverse soft limit
        /// </summary>
        public double ReverseSoftLimit
        {
            get { return _reverseSoftLimit; }
            set { SetProperty(ref _reverseSoftLimit, value); }
        }
        /// <summary>
        /// The nominal forward voltage
        /// </summary>
        public double NominalForwardVoltage
        {
            get { return _nominalForwardVoltage; }
            set { SetProperty(ref _nominalForwardVoltage, value); }
        }
        /// <summary>
        /// The nominal reverse voltage
        /// </summary>
        public double NominalReverseVoltage
        {
            get { return _nominalReverseVoltage; }
            set { SetProperty(ref _nominalReverseVoltage, value); }
        }
        /// <summary>
        /// The maximum forward output voltage
        /// </summary>
        public double PeakForwardVoltage
        {
            get { return _peakForwardVoltage; }
            set { SetProperty(ref _peakForwardVoltage, value); }
        }
        /// <summary>
        /// The maximum reverse output voltage
        /// </summary>
        public double PeakReverseVoltage
        {
            get { return _peakReverseVoltage; }
            set { SetProperty(ref _peakReverseVoltage, value); }
        }

        /// <summary>
        /// Feedback type for the CAN Talon
        /// </summary>
        public enum CANTalonFeedbackDevice
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
        public class CANTalonPIDController : PIDController
        {
            /// <summary>
            /// When an error is larger than this, the accumulated intration error is cleared so that high
            ///     errors aren't racked up when at high errors
            /// </summary>
            public int IZone
            {
                get { return _iZone; }
                set { SetProperty(ref _iZone, value); }
            }
            /// <summary>
            /// Gets or sets the closed loop ramp rate for the current profile. In Volts/Sec
            /// </summary>
            public double CloseLoopRampRate
            {
                get { return _closedLoopRampRate; }
                set { SetProperty(ref _closedLoopRampRate, value); }
            }
            /// <summary>
            /// The max allowable closed loop error for the selected profile.
            /// This is like the 'tolerance' value for software PID controllers
            /// In native SRX units (0 to 1023)
            /// </summary>
            public int AllowableCloseLoopError
            {
                get { return _allowableCloseLoopError; }
                set { SetProperty(ref _allowableCloseLoopError, value); }
            }
            /// <summary>
            /// The PID source type (position or velocity)
            /// </summary>
            public PIDSourceType SourceType
            {
                get { return _pidSourceType; }
                set { SetProperty(ref _pidSourceType, value); }
            }

            #region Private Fields
            private int _iZone;
            private double _closedLoopRampRate;
            private int _allowableCloseLoopError;
            private PIDSourceType _pidSourceType;
            #endregion
        }

        #region Private Fields
        private AnalogInput _analogInput = new AnalogInput();
        private QuadEncoder _digitalInput = new QuadEncoder();
        private CANTalonPIDController _pidConfig0 = new CANTalonPIDController();
        private CANTalonPIDController _pidConfig1 = new CANTalonPIDController();
        private int _enabledPIDProfile;
        private CANTalonFeedbackDevice _FeedbackDevice;
        private ControlMode _controlMode;
        private NeutralMode _neutralMode;
        private bool _zeroSensorPositionOnIndexEnabled;
        private bool _zeroSensorPositionOnRisingEdge;
        private bool _reverseSensor;
        private bool _reverseClosedLoopOutput;
        private bool _reversePercentVBusOutput;
        private bool _forwardLimitSwitchEnabled;
        private bool _reverseLimitSwitchEnabled;
        private bool _forwardSoftLimitEnabled;
        private bool _reverseSoftLimitEnabled;
        private double _forwardSoftLimit;
        private double _reverseSoftLimit;
        private double _nominalForwardVoltage;
        private double _nominalReverseVoltage;
        private double _peakForwardVoltage;
        private double _peakReverseVoltage;
        #endregion
    }
}
