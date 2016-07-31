using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The CANTalon has compound IO, therefore it needs its own type
    /// </summary>
    public interface ICANTalonIOService : IIOService
    {
        /// <summary>
        /// The Analog input for this CANTalon
        /// </summary>
        IIOService AnalogInput { get; }
        /// <summary>
        /// The Quad Encoder input for this CANTalon 
        /// </summary>
        IIOService QuadEncoder { get; }
        /// <summary>
        /// The front limit switch for this CANTalon
        /// </summary>
        IIOService LimitSwitchFront { get; }
        /// <summary>
        /// The rear limit switch for this CANTalon
        /// </summary>
        IIOService LimitSwitchRear { get; }
    }
}
