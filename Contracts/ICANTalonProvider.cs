using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface ICANTalonProvider
    {
        IAnalogInputProvider AnalogInput { get; set; }
        IQuadEncoderProvider QuadEncoder { get; set; }
        IPIDControllerSRXProvider PIDConfig0 { get; set; }
        IPIDControllerSRXProvider PIDConfig1 { get; set; }
        int ID { get; set; }
        string Name { get; set; }
        bool EnabledPIDProfile { get; set; }
        CANTalonFeedbackDevice FeedbackDevice { get; set; }
        CANTalonControlMode ControlMode { get; set; }
        CANTalonNeutralMode NeutralMode { get; set; }
        bool ZeroSensorPositionOnIndexEnabled { get; set; }
        bool ZeroSensorPositionOnRisingEdge { get; set; }
        bool ReverseSensor { get; set; }
        bool ReverseClosedLoopOutput { get; set; }
        bool ReversePercentVBusOutput { get; set; }
        bool ForwardLimitSwitchEnabled { get; set; }
        bool ReverseLimitSwitchEnabled { get; set; }
        bool ForwardSoftLimitEnabled { get; set; }
        bool ReverseSoftLimitEnabled { get; set; }
        double ForwardSoftLimit { get; set; }
        double ReverseSoftLimit { get; set; }
        double NominalForwardVoltage { get; set; }
        double NominalReverseVoltage { get; set; }
        double PeakForwardVoltage { get; set; }
        double PeakReverseVoltage { get; set; }
        double Value { get; set; }

        void SetCANTalon(CANTalon canTalon);
    }
}
