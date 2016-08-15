using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for the robot viewmodel
    /// </summary>
    public interface IRobotProvider : IInputProvider, ICompoundProvider
    {
        /// <summary>
        /// This robot's subsystems
        /// </summary>
        IEnumerable<ISubsystemProvider> Subsystems { get; }
        /// <summary>
        /// This robot's joysticks
        /// </summary>
        IEnumerable<IJoystickProvider> Joysticks { get; }
        /// <summary>
        /// This robot's event handlers
        /// </summary>
        IEnumerable<IEventHandlerProvider> EventHandlers { get; }
        /// <summary>
        /// This robot's Team Number
        /// </summary>
        int TeamNumber { get; set; }
        /// <summary>
        /// This robot's Analog Input sample rate
        /// </summary>
        int AnalogInputSampleRate { get; set; }

        /// <summary>
        /// Sets the model instance of this robot provider
        /// </summary>
        /// <param name="robot">the robot model instance</param>
        void SetRobot(Robot robot);

        /// <summary>
        /// Adds a model instance of a Subsystem
        /// </summary>
        /// <param name="subsystem">the Subsystem model instance</param>
        void AddSubsystem(Subsystem subsystem);
        /// <summary>
        /// Adds a model instance of a Joystick
        /// </summary>
        /// <param name="joystick">the Joystick model instance</param>
        void AddJoystick(Joystick joystick);
        /// <summary>
        /// Adds a model instance of a EventHandler
        /// </summary>
        /// <param name="eventHandler">the EventHandler model instance</param>
        void AddEventHandler(Models.EventHandler eventHandler);
    }
}
