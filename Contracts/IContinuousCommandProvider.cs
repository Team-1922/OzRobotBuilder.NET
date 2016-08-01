using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IContinuousCommandProvider
    {
        IEventTargetProvider EventTarget { get; set; }
        string Name { get; set; }

        void SetContinuousCommand(ContinuousCommand continuousCommand);
    }
}
