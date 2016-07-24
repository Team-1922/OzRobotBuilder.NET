using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts.Events
{
    public class AddAnalogInputEvent : AddSubsystemComponentEvent
    {
        public AddAnalogInputEvent(Subsystem subsystem) : base(subsystem)
        {
        }
    }
}
