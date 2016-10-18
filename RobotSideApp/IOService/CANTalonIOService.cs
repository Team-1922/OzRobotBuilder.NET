using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace RobotSideApp.IOService
{
    class CANTalonIOService : ICANTalonIOService
    {
        public static CANTalonControlMode ConvertControlMode(WPILib.Interfaces.ControlMode controlMode)
        {
            switch (controlMode)
            {
                case WPILib.Interfaces.ControlMode.PercentVbus:
                    return CANTalonControlMode.PercentVbus;
                case WPILib.Interfaces.ControlMode.Follower:
                    return CANTalonControlMode.Follower;
                case WPILib.Interfaces.ControlMode.Voltage:
                    return CANTalonControlMode.Voltage;
                case WPILib.Interfaces.ControlMode.Position:
                    return CANTalonControlMode.Position;
                case WPILib.Interfaces.ControlMode.Speed:
                    return CANTalonControlMode.Speed;
                case WPILib.Interfaces.ControlMode.Current:
                    return CANTalonControlMode.Current;
                case WPILib.Interfaces.ControlMode.MotionProfile:
                    return CANTalonControlMode.MotionProfile;
                case WPILib.Interfaces.ControlMode.Disabled:
                    return CANTalonControlMode.Disabled;
                default:
                    return CANTalonControlMode.PercentVbus;
            }
        }
        public static WPILib.Interfaces.ControlMode ConvertControlMode(CANTalonControlMode controlMode)
        {
            switch (controlMode)
            {
                case CANTalonControlMode.PercentVbus:
                    return WPILib.Interfaces.ControlMode.PercentVbus;
                case CANTalonControlMode.Position:
                    return WPILib.Interfaces.ControlMode.Position;
                case CANTalonControlMode.Speed:
                    return WPILib.Interfaces.ControlMode.Speed;
                case CANTalonControlMode.Current:
                    return WPILib.Interfaces.ControlMode.Current;
                case CANTalonControlMode.Voltage:
                    return WPILib.Interfaces.ControlMode.Voltage;
                case CANTalonControlMode.Follower:
                    return WPILib.Interfaces.ControlMode.Follower;
                case CANTalonControlMode.MotionProfile:
                    return WPILib.Interfaces.ControlMode.MotionProfile;
                case CANTalonControlMode.Disabled:
                    return WPILib.Interfaces.ControlMode.Disabled;
                default:
                    return WPILib.Interfaces.ControlMode.PercentVbus;
            }
        }

        public static CANTalonFeedbackDevice ConvertFeedbackDevice(WPILib.CANTalon.FeedbackDevice feedbackDevice)
        {
            switch (feedbackDevice)
            {
                case WPILib.CANTalon.FeedbackDevice.QuadEncoder:
                    return CANTalonFeedbackDevice.QuadEncoder;
                case WPILib.CANTalon.FeedbackDevice.AnalogPotentiometer:
                    return CANTalonFeedbackDevice.AnalogPotentiometer;
                case WPILib.CANTalon.FeedbackDevice.AnalogEncoder:
                    return CANTalonFeedbackDevice.AnalogEncoder;
                case WPILib.CANTalon.FeedbackDevice.EncoderRising:
                    return CANTalonFeedbackDevice.EncoderRising;
                case WPILib.CANTalon.FeedbackDevice.EncoderFalling:
                    return CANTalonFeedbackDevice.EncoderFalling;
                case WPILib.CANTalon.FeedbackDevice.CtreMagEncoderRelative:
                    return CANTalonFeedbackDevice.CtreMagEncoderRelative;
                case WPILib.CANTalon.FeedbackDevice.CtreMagEncoderAbsolute:
                    return CANTalonFeedbackDevice.CtreMagEncoderAbsolute;
                case WPILib.CANTalon.FeedbackDevice.PulseWidth:
                    return CANTalonFeedbackDevice.PulseWidth;
                default:
                    return CANTalonFeedbackDevice.QuadEncoder;
            }
        }
        public static WPILib.CANTalon.FeedbackDevice ConvertFeedbackDevice(CANTalonFeedbackDevice feedbackDevice)
        {
            switch (feedbackDevice)
            {
                case CANTalonFeedbackDevice.QuadEncoder:
                    return WPILib.CANTalon.FeedbackDevice.QuadEncoder;
                case CANTalonFeedbackDevice.AnalogPotentiometer:
                    return WPILib.CANTalon.FeedbackDevice.AnalogPotentiometer;
                case CANTalonFeedbackDevice.AnalogEncoder:
                    return WPILib.CANTalon.FeedbackDevice.AnalogEncoder;
                case CANTalonFeedbackDevice.EncoderRising:
                    return WPILib.CANTalon.FeedbackDevice.EncoderRising;
                case CANTalonFeedbackDevice.EncoderFalling:
                    return WPILib.CANTalon.FeedbackDevice.EncoderFalling;
                case CANTalonFeedbackDevice.CtreMagEncoderRelative:
                    return WPILib.CANTalon.FeedbackDevice.CtreMagEncoderRelative;
                case CANTalonFeedbackDevice.CtreMagEncoderAbsolute:
                    return WPILib.CANTalon.FeedbackDevice.CtreMagEncoderAbsolute;
                case CANTalonFeedbackDevice.PulseWidth:
                    return WPILib.CANTalon.FeedbackDevice.PulseWidth;
                    break;
                default:
                    return WPILib.CANTalon.FeedbackDevice.QuadEncoder;
            }
        }

        public static CANTalonDifferentiationLevel ConvertSourceType(WPILib.Interfaces.PIDSourceType sourceType)
        {
            switch (sourceType)
            {
                case WPILib.Interfaces.PIDSourceType.Displacement:
                    return CANTalonDifferentiationLevel.Displacement;
                case WPILib.Interfaces.PIDSourceType.Rate:
                    return CANTalonDifferentiationLevel.Speed;
                default:
                    return CANTalonDifferentiationLevel.Displacement;
            }
        }
        public static WPILib.Interfaces.PIDSourceType ConvertSourceType(CANTalonDifferentiationLevel sourceType)
        {
            switch (sourceType)
            {
                case CANTalonDifferentiationLevel.Displacement:
                    return WPILib.Interfaces.PIDSourceType.Displacement;
                case CANTalonDifferentiationLevel.Speed:
                    return WPILib.Interfaces.PIDSourceType.Rate;
                default:
                    return WPILib.Interfaces.PIDSourceType.Displacement;
            }
        }

        public static CANTalonNeutralMode ConvertNeutralMode(WPILib.Interfaces.NeutralMode neutralMode)
        {
            switch (neutralMode)
            {
                case WPILib.Interfaces.NeutralMode.Jumper:
                    return CANTalonNeutralMode.Jumper;
                case WPILib.Interfaces.NeutralMode.Brake:
                    return CANTalonNeutralMode.Brake;
                case WPILib.Interfaces.NeutralMode.Coast:
                    return CANTalonNeutralMode.Coast;
                default:
                    return CANTalonNeutralMode.Coast;
            }
        }
        public static WPILib.Interfaces.NeutralMode ConvertNeutralMode(CANTalonNeutralMode neutralMode)
        {
            switch (neutralMode)
            {
                case CANTalonNeutralMode.Jumper:
                    return WPILib.Interfaces.NeutralMode.Jumper;
                case CANTalonNeutralMode.Brake:
                    return WPILib.Interfaces.NeutralMode.Brake;
                case CANTalonNeutralMode.Coast:
                    return WPILib.Interfaces.NeutralMode.Coast;
                default:
                    return WPILib.Interfaces.NeutralMode.Coast;
            }
        }

        public CANTalonIOService(CANTalon canTalon)
        {
            _canTalon = new WPILib.CANTalon(canTalon.ID);

            //AnalogValue = canTalon.AnalogInput.RawValue;
            //AnalogVelocity = canTalon.AnalogInput.RawVelocity;
            ControlMode = canTalon.ControlMode;
            EnabledPIDProfile = canTalon.EnabledPIDProfile;
            //EncoderValue = canTalon.QuadEncoder.RawValue;
            //EncoderVelocity = canTalon.QuadEncoder.RawVelocity;
            FeedbackDevice = canTalon.FeedbackDevice;
            //ForwardLimitSwitch = canTalon.ForwardLimitSwitch;
            //ForwardLimitSwitchEnabled = canTalon.ForwardLimitSwitchEnabled;
            ForwardSoftLimit = canTalon.ForwardSoftLimit;
            ForwardSoftLimitEnabled = canTalon.ForwardSoftLimitEnabled;
            //ForwardSoftLimitTripped = canTalon.ForwardSoftLimitTripped;
            //ID = canTalon.ID;
            NeutralMode = canTalon.NeutralMode;
            //NominalForwardVoltage = canTalon.NominalForwardVoltage;
            //NominalReverseVoltage = canTalon.NominalReverseVoltage;
            //PeakForwardVoltage = canTalon.PeakForwardVoltage;
            //PeakReverseVoltage = canTalon.PeakReverseVoltage;
            ConfigureNominalVoltage(canTalon.NominalForwardVoltage, canTalon.NominalReverseVoltage);
            ConfigurePeakVoltage(canTalon.PeakForwardVoltage, canTalon.PeakReverseVoltage);
            //PIDConfig0 = canTalon.PIDConfig0;
            //PIDConfig1 = canTalon.PIDConfig1;
            //ReverseClosedLoopOutput = canTalon.ReverseClosedLoopOutput;
            //ReverseLimitSwitch = canTalon.ReverseLimitSwitch;
            //ReverseLimitSwitchEnabled = canTalon.ReverseLimitSwitchEnabled;
            EnableLimitSwitches(canTalon.ForwardLimitSwitchEnabled, canTalon.ReverseLimitSwitchEnabled);
            ReversePercentVBusOutput = canTalon.ReversePercentVBusOutput;
            ReverseSensor = canTalon.ReverseSensor;
            ReverseSoftLimit = canTalon.ReverseSoftLimit;
            ReverseSoftLimitEnabled = canTalon.ReverseSoftLimitEnabled;
            //ReverseSoftLimitTripped = canTalon.ReverseSoftLimitTripped;
            Value = canTalon.Value;
            //ZeroSensorPositionOnIndexEnabled = canTalon.ZeroSensorPositionOnIndexEnabled;
            //ZeroSensorPositionOnRisingEdge = canTalon.ZeroSensorPositionOnRisingEdge;
            EnableZeroSensorPositionOnIndex(canTalon.ZeroSensorPositionOnIndexEnabled, canTalon.ZeroSensorPositionOnRisingEdge);
        }

        public int AnalogValue
        {
            get
            {
                return _canTalon.GetAnalogInRaw();
            }
        }

        public double AnalogVelocity
        {
            get
            {
                return _canTalon.GetAnalogInVelocity();
            }
        }

        public CANTalonControlMode ControlMode
        {
            get
            {
                return ConvertControlMode(_canTalon.MotorControlMode);
            }

            set
            {
                _canTalon.MotorControlMode = ConvertControlMode(value);
            }
        }

        public bool EnabledPIDProfile
        {
            get
            {
                return _canTalon.Profile > 0;
            }

            set
            {
                _canTalon.Profile = value ? 1 : 0;
            }
        }

        public long EncoderValue
        {
            get
            {
                return _canTalon.GetEncoderPosition();
            }
        }

        public double EncoderVelocity
        {
            get
            {
                return _canTalon.GetEncoderVelocity();
            }
        }

        public CANTalonFeedbackDevice FeedbackDevice
        {
            get
            {
                return ConvertFeedbackDevice(_canTalon.FeedBackDevice);
            }

            set
            {
                _canTalon.FeedBackDevice = ConvertFeedbackDevice(value);
            }
        }

        public bool ForwardLimitSwitch
        {
            get
            {
                return _canTalon.IsForwardLimitSwitchClosed();
            }
        }

        public double ForwardSoftLimit
        {
            get
            {
                return _canTalon.ForwardSoftLimit;
            }

            set
            {
                _canTalon.ForwardSoftLimit = value;
            }
        }

        public bool ForwardSoftLimitEnabled
        {
            get
            {
                return _canTalon.ForwardSoftLimitEnabled;
            }

            set
            {
                _canTalon.ForwardSoftLimitEnabled = value;
            }
        }
        
        public int ID
        {
            get
            {
                return _canTalon.DeviceId;
            }
        }

        public CANTalonNeutralMode NeutralMode
        {
            get
            {
                return CANTalonNeutralMode.Coast;//TODO: broken
            }

            set
            {
                _canTalon.NeutralMode = ConvertNeutralMode(value);
            }
        }

        public void ConfigureNominalVoltage(double forward, double reverse)
        {
            _canTalon.ConfigNominalOutputVoltage(forward, reverse);
        }

        public void ConfigurePeakVoltage(double forward, double reverse)
        {
            _canTalon.ConfigPeakOutputVoltage(forward, reverse);
        }

        public bool ReverseCloseLoopOutput
        {
            set
            {
                _canTalon.ReverseOutput(value);
            }
        }

        public bool ReverseLimitSwitch
        {
            get
            {
                return _canTalon.IsReverseLimitSwitchClosed();
            }
        }

        public void EnableLimitSwitches(bool forward, bool reverse)
        {
            _canTalon.EnableLimitSwitches(forward, reverse);
        }

        public bool ReversePercentVBusOutput
        {
            get
            {
                return _canTalon.Inverted;
            }

            set
            {
                _canTalon.Inverted = value;
            }
        }

        public bool ReverseSensor
        {
            set
            {
                _canTalon.ReverseSensor(value);
            }
        }

        public double ReverseSoftLimit
        {
            get
            {
                return _canTalon.ReverseSoftLimit;
            }

            set
            {
                _canTalon.ReverseSoftLimit = value;
            }
        }

        public bool ReverseSoftLimitEnabled
        {
            get
            {
                return _canTalon.ReverseSoftLimitEnabled;
            }

            set
            {
                _canTalon.ReverseSoftLimitEnabled = value;
            }
        }

        public double Value
        {
            get
            {
                return _canTalon.Get();
            }

            set
            {
                _canTalon.Set(value);
            }
        }

        public bool ValueAsBool
        {
            get
            {
                return Value > 0;
            }

            set
            {
                Value = value ? 1 : 0;
            }
        }

        public void EnableZeroSensorPositionOnIndex(bool enable, bool risingEdge)
        {
            _canTalon.EnableZeroSensorPositionOnIndex(enable, risingEdge);
        }

        public double P
        {
            get
            {
                return _canTalon.P;
            }

            set
            {
                _canTalon.P = value;
            }
        }

        public double I
        {
            get
            {
                return _canTalon.I;
            }

            set
            {
                _canTalon.I = value;
            }
        }

        public double D
        {
            get
            {
                return _canTalon.D;
            }

            set
            {
                _canTalon.D = value;
            }
        }

        public double F
        {
            get
            {
                return _canTalon.F;
            }

            set
            {
                _canTalon.F = value;
            }
        }

        public int IZone
        {
            get
            {
                return _canTalon.IZone;
            }

            set
            {
                _canTalon.IZone = value;
            }
        }

        public double CloseLoopRampRate
        {
            get
            {
                return _canTalon.CloseLoopRampRate;
            }

            set
            {
                _canTalon.CloseLoopRampRate = value;
            }
        }

        public int AllowableCloseLoopError
        {
            set
            {
                _canTalon.SetAllowableClosedLoopErr(value);
            }
        }

        public CANTalonDifferentiationLevel SourceType
        {
            get
            {
                return ConvertSourceType(_canTalon.PIDSourceType);
            }

            set
            {
                _canTalon.PIDSourceType = ConvertSourceType(value);
            }
        }

        #region Private Fields
        private WPILib.CANTalon _canTalon;
        #endregion
    }
}
