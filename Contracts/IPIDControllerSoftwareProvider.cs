using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for software pid controller viewmodels
    /// </summary>
    public interface IPIDControllerSoftwareProvider : IProvider
    {
        /// <summary>
        /// The proportional constant
        /// </summary>
        double P { get; set; }
        /// <summary>
        /// The integration constant
        /// </summary>
        double I { get; set; }
        /// <summary>
        /// The differential constant
        /// </summary>
        double D { get; set; }
        /// <summary>
        /// The Feed-Forward constant
        /// </summary>
        double F { get; set; }
        /// <summary>
        /// This is equivalent to <see cref="IPIDControllerSRXProvider.IZone"/>
        /// </summary>
        double Tolerance { get; set; }
        /// <summary>
        /// The time between controller calculations
        /// </summary>
        double CycleDuration { get; set; }
        /// <summary>
        /// Whether the input's end loops back to its beginning (i.e. continuous potentiometer)
        /// </summary>
        bool Continuous { get; set; }

        /// <summary>
        /// Set the model instance for this PIDControllerSoftware provider
        /// </summary>
        /// <param name="pidController">the PIDControllerSoftware model instance</param>
        void SetPIDController(PIDControllerSoftware pidController);
    }
}
