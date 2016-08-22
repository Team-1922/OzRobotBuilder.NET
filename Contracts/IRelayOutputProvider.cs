using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for relay output viewmodels
    /// </summary>
    public interface IRelayOutputProvider : IProvider<RelayOutput>
    {
        /// <summary>
        /// This relay's hardware ID
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// This relay's name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The direction(s) this relay is allowed to be set to
        /// </summary>
        RelayDirection Direction { get; set; }
        /// <summary>
        /// The direction(s) this relay is set to
        /// </summary>
        RelayValue Value { get; set; }
    }
}
