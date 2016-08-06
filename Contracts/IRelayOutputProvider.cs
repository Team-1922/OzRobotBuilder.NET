using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IRelayOutputProvider : IProvider
    {
        int ID { get; set; }
        string Name { get; set; }
        RelayDirection Direction { get; set; }
        RelayValue Value { get; set; }

        void SetRelayOutput(RelayOutput relayOutput);
    }
}
