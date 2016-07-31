using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts.Events
{
    public class AddSubsystemComponentEvent : EventArgs
    {
        public Subsystem Subsystem { get; set; }
        public AddSubsystemComponentEvent(Subsystem subsystem)
        {
            Subsystem = subsystem;
        }
    }
}
