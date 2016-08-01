using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The CANTalon has compound IO, therefore it needs its own type
    /// </summary>
    public interface ICANTalonIOService : IOutputService
    {
        /// <summary>
        /// The Analog input for this CANTalon
        /// NOTE: this is significantly more simple on the CANTalon and does not support accumulation, oversampling, or averaging configuration
        /// </summary>
        IAnalogInputIOService AnalogInput { get; }
        /// <summary>
        /// The Quad Encoder input for this CANTalon 
        /// </summary>
        IQuadEncoderIOService QuadEncoder { get; }
        /// <summary>
        /// The front limit switch for this CANTalon
        /// </summary>
        IDigitalInputIOService LimitSwitchFront { get; }
        /// <summary>
        /// The rear limit switch for this CANTalon
        /// </summary>
        IDigitalInputIOService LimitSwitchRear { get; }
    }
}
