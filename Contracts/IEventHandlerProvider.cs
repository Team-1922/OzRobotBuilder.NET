using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface which represents an event handler
    /// </summary>
    public interface IEventHandlerProvider : IProvider
    {
        /// <summary>
        /// What the event handler runs when <see cref="Condition"/> is true
        /// </summary>
        string Expression { get; set; }
        /// <summary>
        /// Determines when the event handler runs
        /// </summary>
        string Condition { get; set; }
        /// <summary>
        /// The name of this command
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Whether or not the condition is currently met
        /// </summary>
        bool ConditionMet { get; }

        /// <summary>
        /// Sets the model instance for this event handler provider
        /// </summary>
        /// <param name="eventHandler">the event handler model instance</param>
        void SetEventHandler(Models.EventHandler eventHandler);
    }
}
