using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for quadrature encoder viewmodels
    /// </summary>
    public interface IQuadEncoderProvider : IInputProvider, IProvider<QuadEncoder>
    {
        /// <summary>
        /// This quadrature encoder's first hardware ID
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// This quadrature encoder's second hardware ID
        /// </summary>
        int ID1 { get; set; }
        /// <summary>
        /// This quadrature encoder's name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// This quadrature encoder's convertion ratio (user units/native units)
        /// </summary>
        double ConversionRatio { get; set; }
        /// <summary>
        /// This quadrature encoder's raw input value (in native units)
        /// </summary>
        long RawValue { get; }
        /// <summary>
        /// This quadrature encoder's input value (in user units)
        /// </summary>
        double Value { get; }
        /// <summary>
        /// This quadrature encoder's raw velocity (in native units/second)
        /// </summary>
        double RawVelocity { get; }
        /// <summary>
        /// This quadrature encoder's velocity (in user units/second)
        /// </summary>
        double Velocity { get; }
    }
}
