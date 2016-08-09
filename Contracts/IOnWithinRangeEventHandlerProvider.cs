using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for OnWithinRangeEventHandler viewmodels
    /// NOTE: the name of this is the identifying property
    /// </summary>
    public interface IOnWithinRangeEventHandlerProvider : IProvider
    {
        /// <summary>
        /// What to do when this event is triggered
        /// </summary>
        IEventTargetProvider EventTarget { get; }
        /// <summary>
        /// the path of the property which needs to meet these creiteria
        /// </summary>
        string WatchPath { get; set; }
        /// <summary>
        /// The minimum value that <see cref="WatchPath"/> must reach for this event to be triggered
        /// </summary>
        double Min { get; set; }
        /// <summary>
        /// The maximum value that <see cref="WatchPath"/> must stay under for this event to be triggered
        /// </summary>
        double Max { get; set; }
        /// <summary>
        /// Whether to invert <see cref="Min"/> and <see cref="Max"/> ( x is greater than Max or x is less than Min instead of x is less than Max and x is greater than Min)
        /// </summary>
        bool Invert { get; set; }

        /// <summary>
        /// Set the model instance for this OnWithinRangeEventHandler provider
        /// </summary>
        /// <param name="onWithinRangeEventHandler">the OnWithinRangeEventHandler model instance</param>
        void SetOnWithinRangeEventHandler(OnWithinRangeEventHandler onWithinRangeEventHandler);
    }
}
