using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for OnChangeEventHandler viewmodels
    /// </summary>
    public interface IOnChangeEventHandlerProvider : IProvider
    {
        /// <summary>
        /// What to do when this event is triggered
        /// </summary>
        IEventTargetProvider EventTarget { get; }
        /// <summary>
        /// The target is run when this value is meets the creiteria
        /// </summary>
        string WatchPath { get; set; }
        /// <summary>
        /// The minimum change <see cref="WatchPath"/> must go through in order for <see cref="EventTarget"/> to run
        /// </summary>
        double MinDelta { get; set; }

        /// <summary>
        /// Set the model instance of this OnChangeEventHandler provider
        /// </summary>
        /// <param name="onChangeEventHandler">the OnChangeEventHandler provider</param>
        void SetOnChangeEventHandler(OnChangeEventHandler onChangeEventHandler);
    }
}
