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
        #region ICANTalonIOService
        public int AnalogValue { get; internal set; }
        public double AnalogVelocity { get; internal set; }
        public CANTalonControlMode ControlMode { get; set; }
        public bool EnabledPIDProfile { get; set; }
        public long EncoderValue { get; internal set; }
        public double EncoderVelocity { get; internal set; }
        public CANTalonFeedbackDevice FeedbackDevice { get; set; }
        public bool ForwardLimitSwitch { get; internal set; }
        public bool ForwardLimitSwitchEnabled { get; set; }
        public double ForwardSoftLimit { get; set; }
        public bool ForwardSoftLimitEnabled { get; set; }
        public bool ForwardSoftLimitTripped { get; internal set; }
        public int ID { get; set; }
        public CANTalonNeutralMode NeutralMode { get; set; }
        public double NominalForwardVoltage { get; set; }
        public double NominalReverseVoltage { get; set; }
        public double PeakForwardVoltage { get; set; }
        public double PeakReverseVoltage { get; set; }
        public PIDControllerSRX PIDConfig0 { get; internal set; }
        public PIDControllerSRX PIDConfig1 { get; internal set; }
        public bool ReverseClosedLoopOutput { get; set; }
        public bool ReverseLimitSwitch { get; internal set; }
        public bool ReverseLimitSwitchEnabled { get; set; }
        public bool ReversePercentVBusOutput { get; set; }
        public bool ReverseSensor { get; set; }
        public double ReverseSoftLimit { get; set; }
        public bool ReverseSoftLimitEnabled { get; set; }
        public bool ReverseSoftLimitTripped { get; internal set; }
        public double Value { get; set; }
        public bool ValueAsBool { get; set; }
        public bool ZeroSensorPositionOnIndexEnabled { get; set; }
        public bool ZeroSensorPositionOnRisingEdge { get; set; }
        public int ID { get; internal set; }
        #endregion
    }
}
