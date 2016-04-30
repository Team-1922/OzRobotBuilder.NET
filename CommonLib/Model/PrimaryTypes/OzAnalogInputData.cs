using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /*
     * 
     * This is more complicated than the potentiometer (and the potentiometer might be merged into this) because of polling rates, accumulation, etc.
     * 
     */
    public class OzAnalogInputData
    {
        public static double GlobalSampleRate;

        public uint AnalogInputId;
        public int AccumulatorCenter;
        public int AccumulatorDeadband;
        public long AccumulatorInitialValue;
        public int AverageBits;
        public int OversampleBits;

        //defined as normalized analog units per user-defined units
        public double ConversionRatio;
        //in normalized analog units
        public double SensorOffset;
    }
}
