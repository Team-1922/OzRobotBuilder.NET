using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for solenoid viewmodels
    /// </summary>
    public interface ISolenoidProvider : IProvider<Solenoid>
    {
        /// <summary>
        /// The ID of this solenoid
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The state of this solenoid
        /// </summary>
        bool Value { get; set; }
    }
}
