using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IJoystickProvider : IInputProvider
    {
        IEnumerable<IJoystickAxisProvider> Axes { get; set; }
        IEnumerable<IJoystickButtonProvider> Buttons { get; set; }
        int ID { get; set; }
        string Name { get; set; }

        void SetJoystick(Joystick joystick);
    }
}
