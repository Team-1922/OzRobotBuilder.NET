using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for double solenoid viewmodels
    /// </summary>
    public interface IDoubleSolenoidProvider : IProvider<DoubleSolenoid>
    {
        /// <summary>
        /// The first ID of this double solenoid
        /// </summary>
        int ID0 { get; set; }
        /// <summary>
        /// The second ID of this double solenoid
        /// </summary>
        int ID1 { get; set; }
        /// <summary>
        /// The state of this double solenoid
        /// </summary>
        DoubleSolenoidState Value { get; set; }
    }
}
