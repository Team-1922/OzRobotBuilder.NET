using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The special parts needed for quadrature encoders
    /// NOTE: <see cref="IInputService.Value"/> returns native encoder units
    /// </summary>
    public interface IQuadEncoderIOService : IInputService
    {
        /// <summary>
        /// simple velocity of the encoder (encoder units/second)
        /// </summary>
        double Velocity { get; }
    }
}
