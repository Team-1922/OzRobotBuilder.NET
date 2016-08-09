using System.Collections.Generic;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for joystick IO services
    /// </summary>
    public interface IJoystickIOService
    {
        /// <summary>
        /// The axis states
        /// </summary>
        IReadOnlyDictionary<uint, double> Axes { get; }
        /// <summary>
        /// the button states
        /// </summary>
        IReadOnlyDictionary<uint, bool> Buttons { get; }
        /// <summary>
        /// the id of this in the driver station
        /// </summary>
        int ID { get; }
    }
}