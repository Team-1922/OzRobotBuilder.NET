using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /*
     * 
     * This is the big kahuna of all of the primary data types;
     * 
     */
    class OzTalonSRXData : OzMotorControllerData
    {
        public OzAnalogInputData AnalogInput;
        public OzQuadEncoderData DigitalInput;
        public OzSRXPIDControllerData PIDConfig0;
        public OzSRXPIDControllerData PIDConfig1;
        public int EnabledPIDProfile;
        public FeedbackDevice SRXFeedbackDevice;
        public ControlMode SRXControlMode;
        public NeutralMode SRXNeutralMode;
        public bool ZeroSensorPositionOnIndexEnabled;
        public bool ReverseSensor;
        public bool ReverseClosedLoopOutput;
        public bool ReversePercentVBusOutput;
        public bool ForwardLimitSwitchEnabled;
        public bool ReverseLimitSwitchEnabled;
        public bool ForwardSoftLimitEnabled;
        public bool ReverseSoftLimitEnabled;
        public double ForwardSoftLimit;
        public double ReverseSoftLimit;
        public double NominalForwardVoltage;
        public double NominalReverseVoltage;
        public double PeakForwardVoltage;
        public double PeakReverseVoltage;

        //
        // Summary:
        //     Feedback type for CAN Talon
        public enum FeedbackDevice
        {
            //
            // Summary:
            //     A quadrature encoder.
            QuadEncoder = 0,
            //
            // Summary:
            //     An analog potentiometer.
            AnalogPotentiometer = 2,
            //
            // Summary:
            //     An analog encoder.
            AnalogEncoder = 3,
            //
            // Summary:
            //     An encoder that only reports when it hits a rising edge.
            EncoderRising = 4,
            //
            // Summary:
            //     An encoder that only reports when it hits a falling edge.
            EncoderFalling = 5,
            //
            // Summary:
            //     Relative magnetic encoder.
            CtreMagEncoderRelative = 6,
            //
            // Summary:
            //     Absolute magnetic encoder
            CtreMagEncoderAbsolute = 7,
            //
            // Summary:
            //     Encoder is a pulse width sensor.
            PulseWidth = 8
        }

        public class OzSRXPIDControllerData : OzPIDControllerData
        {
            public int IZone;
            public double CloseLoopRampRate;
            //this is like the 'tolerance' value for software PID controllers
            //
            //This is in native SRX units (0 to 1023 for analog devices)
            public int AllowableCloseLoopError;
            public PIDSourceType SourceType;
        }

    }
}
