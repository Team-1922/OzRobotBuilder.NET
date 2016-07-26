using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    /// <summary>
    /// Represents the state of the robot IO; separate from the robot model, but accessible to it
    /// </summary>
    public interface IRobotIOService
    {
        IDictionary<uint, IIOService> PWMOutputs { get; }
        /// <summary>
        /// In data, analog inputs and potentioemters are used differently,
        ///     however they both use the same id's & accessed similarly from hardware
        /// </summary>
        IDictionary<uint, IIOService> AnalogInputs { get; }
        IDictionary<uint, IIOService> RelayOutputs { get; }
        IDictionary<uint, IIOService> DigitalInputs { get; }
        IDictionary<uint, ICANTalonIOService> CANTalons { get; }
    }
}
