using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.FakeIOService
{
    public class FakeCANTalonIOService : ICANTalonIOService
    {
        public FakeCANTalonIOService(CANTalon canTalon)
        {
            //AnalogValue = canTalon.AnalogInput.RawValue;
            //AnalogVelocity = canTalon.AnalogInput.RawVelocity;
            ControlMode = canTalon.ControlMode;
            //EnabledPIDProfile = canTalon.EnabledPIDProfile;
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

        #region ICANTalonIOService
        public int AllowableCloseLoopError { private get; set; }
        public int AnalogValue { get; private set; }
        public double AnalogVelocity { get; private set; }
        public double CloseLoopRampRate { get; set; }
        public CANTalonControlMode ControlMode { get; set; }
        public double D { get; set; }
        public bool EnabledPIDProfile { get; set; }
        public long EncoderValue { get; private set; }
        public double EncoderVelocity { get; private set; }
        public double F { get; set; }
        public CANTalonFeedbackDevice FeedbackDevice { get; set; }
        public bool ForwardLimitSwitch { get; private set; }
        public double ForwardSoftLimit { get; set; }
        public bool ForwardSoftLimitEnabled { get; set; }
        public double I { get; set; }
        public int ID { get; private set; }
        public int IZone { get; set; }
        public CANTalonNeutralMode NeutralMode { get; set; }
        public double P { get; set; }
        public bool ReverseCloseLoopOutput { private get; set; }
        public bool ReverseLimitSwitch { get; private set; }
        public bool ReversePercentVBusOutput { get; set; }
        public bool ReverseSensor { private get; set; }
        public double ReverseSoftLimit { get; set; }
        public bool ReverseSoftLimitEnabled { get; set; }
        public CANTalonDifferentiationLevel SourceType { get; set; }
        public double Value { get; set; }
        public bool ValueAsBool
        {
            get
            {
                return Value > 1;
            }

            set
            {
                Value = value ? 1 : 0;
            }
        }

        public void ConfigureNominalVoltage(double forward, double reverse)
        {
            throw new NotImplementedException();
        }

        public void ConfigurePeakVoltage(double forward, double reverse)
        {
            throw new NotImplementedException();
        }

        public void EnableLimitSwitches(bool forward, bool reverse)
        {
            throw new NotImplementedException();
        }

        public void EnableZeroSensorPositionOnIndex(bool enable, bool risingEdge)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
