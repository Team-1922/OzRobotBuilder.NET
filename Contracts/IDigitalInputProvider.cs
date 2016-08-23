using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for Digital Input viewmodels
    /// </summary>
    public interface IDigitalInputProvider : IInputProvider, IProvider<DigitalInput>
    {
        /// <summary>
        /// The hardware ID of this digital input
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The boolean state value
        /// </summary>
        bool Value { get; }
    }
}
