using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The contract for exposing a <see cref="CANTalonAnalogInput"/> in the model
    /// </summary>
    public interface ICANTalonAnalogInputProvider : IInputProvider
    {
        /// <summary>
        /// The raw analog input value (in SRX units (0-1023))
        /// </summary>
        int RawValue { get; }
        /// <summary>
        /// The raw analog input velocity (in SRX units/second)
        /// </summary>
        double RawVelocity { get; }
        /// <summary>
        /// The converted analog input value (in user units)
        /// </summary>
        double Value { get; }
        /// <summary>
        /// The converted analog input velocity (in user units/second)
        /// </summary>
        double Velocity { get; }
        /// <summary>
        /// The conversion ratio (in user units/SRX units)
        /// </summary>
        double ConversionRatio { get; set; }
        /// <summary>
        /// The offset applied after the conversion (in user units)
        /// </summary>
        double SensorOffset { get; set; }
        /// <summary>
        /// Sets the CANTalon instance for this provider
        /// </summary>
        /// <param name="canTalon">the canTalon to retrieve the AnalogInput from</param>
        void SetCANTalon(CANTalon canTalon);      
    }
}