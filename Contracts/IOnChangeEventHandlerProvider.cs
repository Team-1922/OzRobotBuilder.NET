using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IOnChangeEventHandlerProvider : IProvider
    {
        IEventTargetProvider EventTarget { get; }
        string Name { get; set; }
        string WatchPath { get; set; }
        double MinDelta { get; set; }

        void SetOnChangeEventHandler(OnChangeEventHandler onChangeEventHandler);
    }
}
