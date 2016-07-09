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
    public class OzQuadEncoderData : INamedClass, IIdentificationNumber
    {
        /// <summary>
        /// The name of this encoder
        /// </summary>
        public string Name { get; set; } = "OzQuadEncoderData";
        /// <summary>
        /// The ratio defined as output units per encoder unit
        /// </summary>
        public double ConversionRatio { get; set; } 
        /// <summary>
        /// The first digital input used (where the three wires are plugged into)
        /// </summary>
        public uint ID { get; set; } 
        /// <summary>
        /// The second digital input used (where the one wire is plugged into)
        /// </summary>
        public uint ID1 { get; set; } 
    }
}
