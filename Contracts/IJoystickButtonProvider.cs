using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IJoystickButtonProvider : IInputProvider
    {
        int ID { get; set; }
        bool Value { get; set; }

        void SetJoystick(JoystickButton axis, Joystick joystick);
    }
}
