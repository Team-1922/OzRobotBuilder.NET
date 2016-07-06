using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// This is more complicated than the potentiometer (and the potentiometer might be merged into this) because of polling rates, accumulation, etc.
    /// </summary>
    public class OzAnalogInputData : INamedClass
    {
        /// <summary>
        /// the polling rate for ALL analog inputs
        /// </summary>
        public static double GlobalSampleRate;
        /// <summary>
        /// The name of this particular analog input
        /// </summary>
        public string Name = "OzAnalogInputData";
        /// <summary>
        /// The input id
        /// </summary>
        public uint AnalogInputId;
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AccumulatorCenter;
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AccumulatorDeadband;
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public long AccumulatorInitialValue;
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AverageBits;
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int OversampleBits;
        /// <summary>
        /// The conversion ratio defined as normalized analog units per user-defined units
        /// </summary>
        public double ConversionRatio;
        /// <summary>
        /// The offset in normalized analog units
        /// </summary>
        public double SensorOffset;
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
