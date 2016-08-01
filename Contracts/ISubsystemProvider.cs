using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface ISubsystemProvider : IInputProvider
    {
        IEnumerable<IPWMOutputProvider> PWMOutputs { get; }
        IEnumerable<IAnalogInputProvider> AnalogInputs { get; }
        IEnumerable<IQuadEncoderProvider> QuadEncoders { get; }
        IEnumerable<IRelayOutputProvider> RelayOutputs { get; }
        IEnumerable<ICANTalonProvider> CANTalons { get; }
        IPIDControllerSoftwareProvider PIDController { get; }
        string Name { get; set; }
        bool SoftwarePIDEnabled { get; set; }

        void SetSubsystem(Subsystem subsystem);
    }
}
