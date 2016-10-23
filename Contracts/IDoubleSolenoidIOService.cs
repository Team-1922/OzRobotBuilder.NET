using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The IO Service interface for Double solenoids
    /// </summary>
    public interface IDoubleSolenoidIOService : IIOService
    {
        /// <summary>
        /// the first ID of the double solenoid
        /// </summary>
        int ID0 { get; set; }
        /// <summary>
        /// The second ID of the double solenoid
        /// </summary>
        int ID1 { get; set; }
        /// <summary>
        /// The set value of the double solenoid
        /// </summary>
        DoubleSolenoidState Value { get; set; }
    }
}
