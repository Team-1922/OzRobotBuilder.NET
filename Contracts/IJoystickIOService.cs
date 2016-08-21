using System.Collections.Generic;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for joystick IO services
    /// </summary>
    public interface IJoystickIOService : IIOService
    {
        /// <summary>
        /// The axis states
        /// </summary>
        IReadOnlyDictionary<uint, double> Axes { get; }
        /// <summary>
        /// the button states
        /// </summary>
        IReadOnlyDictionary<uint, bool> Buttons { get; }
    }
}