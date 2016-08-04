using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The contract for exposing a <see cref="CANTalonQuadEncoder"/> in the model
    /// </summary>
    public interface ICANTalonQuadEncoderProvider : IInputProvider
    {
        /// <summary>
        /// The raw encoder value (in encoder untis)
        /// </summary>
        long RawValue { get; }
        /// <summary>
        /// The raw encoder velocity (in encoder units/second)
        /// </summary>
        double RawVelocity { get; }
        /// <summary>
        /// The converted encoder value (in user units)
        /// </summary>
        double Value { get; }
        /// <summary>
        /// The converted encoder velocity (in user units/second)
        /// </summary>
        double Velocity { get; }
        /// <summary>
        /// The conversion ratio (in user units/encoder units)
        /// </summary>
        double ConversionRatio { get; set; }
        /// <summary>
        /// The offset applied after the conversion (in user units)
        /// </summary>
        double SensorOffset { get; set; }
        /// <summary>
        /// Sets the CANTalon instance for this provider
        /// </summary>
        /// <param name="canTalon">the canTalon to retrieve the QuadEncoder from</param>
        void SetCANTalon(CANTalon canTalon);
    }
}