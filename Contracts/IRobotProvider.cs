using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IRobotProvider : IInputProvider
    {
        IEnumerable<ISubsystemProvider> Subsystems { get; }
        IEnumerable<IJoystickProvider> Joysticks { get; }
        IEnumerable<IOnChangeEventHandlerProvider> OnChangeEventHandlers { get; }
        IEnumerable<IOnWithinRangeEventHandlerProvider> OnWithinRangeEventHandlers { get; }
        IEnumerable<IContinuousCommandProvider> ContinuousCommands { get; }
        int TeamNumber { get; set; }
        int AnalogInputSampleRate { get; set; }

        void SetRobot(Robot robot);
    }
}
