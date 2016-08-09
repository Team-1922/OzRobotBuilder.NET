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
        /// This robot's on-change EventHandlers; all of the event handlers and commands might be merged
        /// </summary>
        IEnumerable<IOnChangeEventHandlerProvider> OnChangeEventHandlers { get; }
        /// <summary>
        /// This robot's on-within-range EventHandlers; all of the event handlers and commands might be merged
        /// </summary>
        IEnumerable<IOnWithinRangeEventHandlerProvider> OnWithinRangeEventHandlers { get; }
        /// <summary>
        /// This robot's continuous Commands; all of the event handlers and commands might be merged
        /// </summary>
        IEnumerable<IContinuousCommandProvider> ContinuousCommands { get; }
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
    }
}
