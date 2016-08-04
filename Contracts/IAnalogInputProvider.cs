using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// A service contract for a class which represents access between an outside view and an
    /// analog input model.
    /// </summary>
    public interface IAnalogInputProvider : IInputProvider
    {
        /// <summary>
        /// The ID property of the model
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The Name property of the model
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        int AccumulatorCenter { get; set; }
        /// <summary>
        /// The AccumulatorDeadband property of the model
        /// </summary>
        int AccumulatorDeadband { get; set; }
        /// <summary>
        /// The AverageBits property of the model
        /// </summary>
        int AverageBits { get; set; }
        /// <summary>
        /// The OversampleBits property of the model
        /// </summary>
        int OversampleBits { get; set; }
        /// <summary>
        /// The ConversionRatio property of the model
        /// </summary>
        double ConversionRatio { get; set; }
        /// <summary>
        /// The SensorOffset property of the model
        /// </summary>
        double SensorOffset { get; set; }
        /// <summary>
        /// The RawValue property of the model
        /// </summary>
        int RawValue { get; }
        /// <summary>
        /// The Value property of the model
        /// </summary>
        double Value { get; }
        /// <summary>
        /// The RawAverageValue property of the model
        /// </summary>
        long RawAverageValue { get; }
        /// <summary>
        /// The AverageValue property of the model
        /// </summary>
        double AverageValue { get; }
        /// <summary>
        /// The AccumulatorCount property of the model
        /// </summary>
        long AccumulatorCount { get; }
        /// <summary>
        /// The RawAccumulatorValue property of the model
        /// </summary>
        long RawAccumulatorValue { get; }
        /// <summary>
        /// The AccumulatorValue property of the model
        /// </summary>
        double AccumulatorValue { get; }
        /// <summary>
        /// Resets the hardware analog input accumulator
        /// </summary>
        void ResetAccumulator();
        /// <summary>
        /// Sets the analog input model instance for this viewmodel
        /// </summary>
        /// <param name="analogInput">the analog input model instance</param>
        void SetAnalogInput(AnalogInput analogInput);
    }
}
