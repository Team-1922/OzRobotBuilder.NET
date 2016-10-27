using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for command viewmodels
    /// </summary>
    public interface ICommandProvider : IProvider<Command>
    {
        /// <summary>
        /// The expression to run when command is first called
        /// </summary>
        string OnStart { get; set; }
        /// <summary>
        /// The expression to run every update cycle
        /// </summary>
        string OnUpdate { get; set; }
        /// <summary>
        /// The expression to check if this command should stop
        /// </summary>
        string IsFinished { get; set; }
        /// <summary>
        /// The expression to run after this command is finished, or is cancelled
        /// </summary>
        string OnEnd { get; set; }
        /// <summary>
        /// A comma-separated list of names of the parameters used
        /// </summary>
        string ParamNames { get; set; }
    }
}
