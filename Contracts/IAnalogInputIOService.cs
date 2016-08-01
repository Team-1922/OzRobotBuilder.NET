using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// this is slightly more complex than the regular <see cref="IIOService"/> because
    /// it has more hardware-bound values
    /// </summary>
    public interface IAnalogInputIOService : IInputService
    {
        /// <summary>
        /// The result of post average and oversample algorithms
        /// </summary>
        int AverageValue { get; }
        /// <summary>
        /// The number of samples that have been added to the accumulator since the last reset
        /// </summary>
        long AccumulatorCount { get; }
        /// <summary>
        /// The value currently in the accumulator
        /// </summary>
        long AccumulatorValue { get; }
        /// <summary>
        /// The the power of 2 number of samples which are averaged together
        /// </summary>
        int AverageBits { get; set; }
        /// <summary>
        /// The power of 2 number of samples which are added together
        /// </summary>
        int OversampleBits { get; set; }
        /// <summary>
        /// This resets the analog accumulator
        /// </summary>
        void ResetAccumulator();
    }
}
