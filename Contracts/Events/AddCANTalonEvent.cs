using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts.Events
{
    [Obsolete]
    public class AddCANTalonEvent : AddSubsystemComponentEvent
    {
        public AddCANTalonEvent(Subsystem subsystem) : base(subsystem)
        {
        }
    }
}
