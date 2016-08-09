using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for Continuous Command viewmodels
    /// </summary>
    public interface IContinuousCommandProvider : IProvider
    {
        /// <summary>
        /// Executed every update cycle
        /// </summary>
        IEventTargetProvider EventTarget { get; }

        /// <summary>
        /// Set the model instance for this Continuous Command provider
        /// </summary>
        /// <param name="continuousCommand">the ContinuousCommand model instance</param>
        void SetContinuousCommand(ContinuousCommand continuousCommand);
    }
}
