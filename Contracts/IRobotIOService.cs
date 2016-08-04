using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// Represents the state of the robot IO; separate from the robot model, but accessible to it
    /// </summary>
    public interface IRobotIOService
    {
        /// <summary>
        /// A list of PWM outputs
        /// </summary>
        IReadOnlyDictionary<int, IPWMOutputIOService> PWMOutputs { get; }
        /// <summary>
        /// In data, analog inputs and potentioemters are used differently,
        /// however they both use the same id's & accessed similarly from hardware
        /// </summary>
        IReadOnlyDictionary<int, IAnalogInputIOService> AnalogInputs { get; }
        /// <summary>
        /// A list of Relay outputs
        /// </summary>
        IReadOnlyDictionary<int, IRelayOutputIOService> RelayOutputs { get; }
        /// <summary>
        /// A list of Digital inputs (quad encoders included)
        /// </summary>
        IReadOnlyDictionary<int, IInputService> DigitalInputs { get; }
        /// <summary>
        /// A list of CAN Talons
        /// </summary>
        IReadOnlyDictionary<int, ICANTalonIOService> CANTalons { get; }
        /// <summary>
        /// A list of Joysticks
        /// </summary>
        IReadOnlyDictionary<int, IJoystickIOService> Joysticks { get; }
    }
}
