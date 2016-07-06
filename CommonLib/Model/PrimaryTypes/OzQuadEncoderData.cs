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
    public class OzQuadEncoderData : INamedClass
    {
        /// <summary>
        /// The name of this encoder
        /// </summary>
        public string Name = "OzQuadEncoderData";
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
        #region INamedClass Interface
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string GetName()
        {
            return Name;
        }
        public void SetName(string name)
        {
            Name = name;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion
    }
}
