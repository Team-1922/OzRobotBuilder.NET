using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

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

        /// <summary>
        /// This is called once at the beginning of the application for construction purposes
        /// </summary>
        /// <param name="robotModel">the <see cref="Robot"/> to construct this Service with</param>
        void SetRobot(Robot robotModel);
        /// <summary>
        /// constructs the IOService attached to the given PWMOutput
        /// </summary>
        /// <param name="pwmOutput">the model instance holding the necessary construction data</param>
        void AddPWMOutput(PWMOutput pwmOutput);
        /// <summary>
        /// constructs the IOService attached to the given AnalogInput
        /// </summary>
        /// <param name="analogInput">the model instance holding the necessary construction data</param>
        void AddAnalogInput(AnalogInput analogInput);
        /// <summary>
        /// constructs the IOService attached to the given RelayOutput
        /// </summary>
        /// <param name="relayOutput">the model instance holding the necessary construction data</param>
        void AddRelayOutput(RelayOutput relayOutput);
        /// <summary>
        /// constructs the IOService attached to the given DigitalInput
        /// </summary>
        /// <param name="digitalInput">the model instance holding the necessary construction data</param>
        void AddDigitalInput(DigitalInput digitalInput);
        /// <summary>
        /// constructs the IOService attached to the given CANTalon
        /// </summary>
        /// <param name="canTalon">the model instance holding the necessary construction data</param>
        void AddCANTalon(CANTalon canTalon);
        /// <summary>
        /// constructs the IOService attached to the given Joystick
        /// </summary>
        /// <param name="joystick">the model instance holding the necessary construction data</param>
        void AddJoystick(Joystick joystick);
    }
}
