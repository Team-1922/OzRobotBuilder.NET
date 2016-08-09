using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for EventTarget viewmodels
    /// </summary>
    public interface IEventTargetProvider : IProvider
    {
        /// <summary>
        /// the kind of event target this is
        /// </summary>
        EventTargetType Type { get; set; }
        /// <summary>
        /// The path to the value to set if <see cref="Type"/>==<see cref="EventTargetType.ModifyValue"/>
        /// </summary>
        string Path { get; set; }
        /// <summary>
        /// Either the expression to evaluate and store into <see cref="Path"/> or the name of the C# method to execute 
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Sets the model instance for this EventTarget provider
        /// </summary>
        /// <param name="eventTarget">the EventTarget model instance</param>
        void SetEventTarget(EventTarget eventTarget);
    }
}
