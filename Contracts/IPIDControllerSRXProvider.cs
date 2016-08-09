using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for CANTalon pid controller config viewmodels
    /// </summary>
    public interface IPIDControllerSRXProvider : IProvider
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
        /// When closed-loop error is outside of this, the integral accumulation is reset
        /// </summary>
        int IZone { get; set; }
        /// <summary>
        /// The closed-loop output ramp rate (in Volts/second)
        /// </summary>
        double CloseLoopRampRate { get; set; }
        /// <summary>When the Closed-Loop Error is within the Allowable Closed-Loop Error: P, I, D terms are zeroed.In other words, the math that uses P, I, and D gains is
        /// disabled. However, F term is still in effect;  Integral Accumulator is cleared.
        /// </summary>
        int AllowableCloseLoopError { get; set; }
        /// <summary>
        /// The differentiation level (i.e. position, speed) of this controller
        /// </summary>
        CANTalonDifferentiationLevel SourceType { get; set; }

        /// <summary>
        /// Sets the model instance for this PIDControllerSRX provider
        /// </summary>
        /// <param name="pidController">the PIDControllerSRX model instance</param>
        void SetPIDController(PIDControllerSRX pidController);
    }
}
