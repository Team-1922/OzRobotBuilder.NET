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
        public string Name { get; set; } = "OzAnalogInputData";
        /// <summary>
        /// The input id
        /// </summary>
        public uint AnalogInputId { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AccumulatorCenter { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AccumulatorDeadband { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public long AccumulatorInitialValue { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AverageBits { get; set; }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int OversampleBits { get; set; }
        /// <summary>
        /// The conversion ratio defined as normalized analog units per user-defined units
        /// </summary>
        public double ConversionRatio { get; set; }
        /// <summary>
        /// The offset in normalized analog units
        /// </summary>
        public double SensorOffset { get; set; }
    }
}
