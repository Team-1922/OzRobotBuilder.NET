using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IEventTargetProvider : IProvider
    {
        EventTargetType Type { get; set; }
        string Path { get; set; }
        string Value { get; set; }

        void SetEventTarget(EventTarget eventTarget);
    }
}
