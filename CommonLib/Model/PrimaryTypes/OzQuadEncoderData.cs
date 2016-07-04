using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// The data for a quadrature encoder
    /// </summary>
    public class OzQuadEncoderData
    {
        /// <summary>
        /// The ratio defined as output units per encoder unit
        /// </summary>
        public double ConversionRatio;
        /// <summary>
        /// The first digital input used (where the three wires are plugged into; -1 on TalonSRX)
        /// </summary>
        public uint DigitalIn0;
        /// <summary>
        /// The second digital input used (where the one wire is plugged into; -1 on TalonSRX)
        /// </summary>
        public uint DigitalIn1;
        /// <summary>
        /// The name of this encoder
        /// </summary>
        public string Name = "OzQuadEncoderData";
    }
}
