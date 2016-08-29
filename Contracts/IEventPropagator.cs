using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts.Events;

namespace Team1922.MVVM.Contracts
{
    public interface IEventPropagator
    {
        event EventPropagationEventHandler Propagated;
    }
}
