using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    /// <summary>
    /// The CANTalon has compound IO, therefore it needs its own type
    /// </summary>
    public interface ICANTalonIOService : IIOService
    {
        IIOService AnalogInput { get; }
        IIOService DigitalInput { get; }
        IIOService LimitSwitchFront { get; }
        IIOService LimitSwitchRear { get; }
    }
}
