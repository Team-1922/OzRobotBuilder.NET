using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for PWM output viewmodels
    /// </summary>
    public interface IPWMOutputProvider : IProvider
    {
        /// <summary>
        /// This PWM output's hardware ID
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// This PWM output's name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// This PWM output's output value (-1 to 1)
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Sets the model instance of this PWM output provider
        /// </summary>
        /// <param name="pwmOutput">the PWM output model instance</param>
        void SetPWMOutput(PWMOutput pwmOutput);
    }
}
