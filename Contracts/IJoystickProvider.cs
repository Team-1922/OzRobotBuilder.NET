using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for joystick viewmodels
    /// </summary>
    public interface IJoystickProvider : IInputProvider, IProvider<Joystick>
    {
        /// <summary>
        /// The state of the joystick axes
        /// </summary>
        IReadOnlyDictionary<int, double> Axes { get; }
        /// <summary>
        /// The state of the joystick buttons
        /// </summary>
        IReadOnlyDictionary<int, bool> Buttons { get; }
        /// <summary>
        /// The id of the this joystick in the driver station
        /// </summary>
        int ID { get; set; }
    }
}
