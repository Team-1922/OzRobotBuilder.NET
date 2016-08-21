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
            AnalogValue = canTalon.AnalogInput.RawValue;
            AnalogVelocity = canTalon.AnalogInput.RawVelocity;
            ControlMode = canTalon.ControlMode;
            EnabledPIDProfile = canTalon.EnabledPIDProfile;
            EncoderValue = canTalon.QuadEncoder.RawValue;
            EncoderVelocity = canTalon.QuadEncoder.RawVelocity;
            FeedbackDevice = canTalon.FeedbackDevice;
            ForwardLimitSwitch = canTalon.ForwardLimitSwitch;
            ForwardLimitSwitchEnabled = canTalon.ForwardLimitSwitchEnabled;
            ForwardSoftLimit = canTalon.ForwardSoftLimit;
            ForwardSoftLimitEnabled = canTalon.ForwardSoftLimitEnabled;
            ForwardSoftLimitTripped = canTalon.ForwardSoftLimitTripped;
            ID = canTalon.ID;
            NeutralMode = canTalon.NeutralMode;
            NominalForwardVoltage = canTalon.NominalForwardVoltage;
            NominalReverseVoltage = canTalon.NominalReverseVoltage;
            PeakForwardVoltage = canTalon.PeakForwardVoltage;
            PeakReverseVoltage = canTalon.PeakReverseVoltage;
            PIDConfig0 = canTalon.PIDConfig0;
            PIDConfig1 = canTalon.PIDConfig1;
            ReverseClosedLoopOutput = canTalon.ReverseClosedLoopOutput;
            ReverseLimitSwitch = canTalon.ReverseLimitSwitch;
            ReverseLimitSwitchEnabled = canTalon.ReverseLimitSwitchEnabled;
            ReversePercentVBusOutput = canTalon.ReversePercentVBusOutput;
            ReverseSensor = canTalon.ReverseSensor;
            ReverseSoftLimit = canTalon.ReverseSoftLimit;
            ReverseSoftLimitEnabled = canTalon.ReverseSoftLimitEnabled;
            ReverseSoftLimitTripped = canTalon.ReverseSoftLimitTripped;
            Value = canTalon.Value;
            ZeroSensorPositionOnIndexEnabled = canTalon.ZeroSensorPositionOnIndexEnabled;
            ZeroSensorPositionOnRisingEdge = canTalon.ZeroSensorPositionOnRisingEdge;
        }
        #region ICANTalonIOService
        public int AnalogValue { get; private set; }
        public double AnalogVelocity { get; private set; }
        public CANTalonControlMode ControlMode { get; set; }
        public bool EnabledPIDProfile { get; set; }
        public long EncoderValue { get; private set; }
        public double EncoderVelocity { get; private set; }
        public CANTalonFeedbackDevice FeedbackDevice { get; set; }
        public bool ForwardLimitSwitch { get; private set; }
        public bool ForwardLimitSwitchEnabled { get; set; }
        public double ForwardSoftLimit { get; set; }
        public bool ForwardSoftLimitEnabled { get; set; }
        public bool ForwardSoftLimitTripped { get; private set; }
        public int ID { get; private set; }
        public CANTalonNeutralMode NeutralMode { get; set; }
        public double NominalForwardVoltage { get; set; }
        public double NominalReverseVoltage { get; set; }
        public double PeakForwardVoltage { get; set; }
        public double PeakReverseVoltage { get; set; }
        public PIDControllerSRX PIDConfig0 { get; private set; }
        public PIDControllerSRX PIDConfig1 { get; private set; }
        public bool ReverseClosedLoopOutput { get; set; }
        public bool ReverseLimitSwitch { get; private set; }
        public bool ReverseLimitSwitchEnabled { get; set; }
        public bool ReversePercentVBusOutput { get; set; }
        public bool ReverseSensor { get; set; }
        public double ReverseSoftLimit { get; set; }
        public bool ReverseSoftLimitEnabled { get; set; }
        public bool ReverseSoftLimitTripped { get; private set; }
        public double Value { get; set; }
        public bool ValueAsBool { get; set; }
        public bool ZeroSensorPositionOnIndexEnabled { get; set; }
        public bool ZeroSensorPositionOnRisingEdge { get; set; }
        #endregion
    }
}
