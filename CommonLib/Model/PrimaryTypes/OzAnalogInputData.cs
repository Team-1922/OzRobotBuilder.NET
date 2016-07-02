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

        public string Name;
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

       /* public virtual DataTreeNode GetTree()
        {
            var root = new DataTreeNode(Name, AnalogInputId.ToString(), typeof(OzAnalogInputData));
            root.Add(new DataTreeNode("AnalogInputId", AnalogInputId.ToString(), typeof(uint)));
            root.Add(new DataTreeNode("AccumulatorCenter", AccumulatorCenter.ToString(), typeof(int)));
            root.Add(new DataTreeNode("AccumulatorDeadband", AccumulatorDeadband.ToString(), typeof(int)));
            root.Add(new DataTreeNode("AccumulatorInitialValue", AccumulatorInitialValue.ToString(), typeof(long)));
            root.Add(new DataTreeNode("AverageBits", AverageBits.ToString(), typeof(int)));
            root.Add(new DataTreeNode("OversampleBits", OversampleBits.ToString(), typeof(int)));
            root.Add(new DataTreeNode("ConversionRatio", ConversionRatio.ToString(), typeof(double)));
            root.Add(new DataTreeNode("SensorOffset", SensorOffset.ToString(), typeof(double)));
        }
        public virtual bool DeserializeTree(DataTreeNode node)
        {
            if (node.Type == typeof(OzAnalogInputData))
                return false;
            if (node.Key != Name)
                return false;

            bool success = uint.TryParse(node.Data, out MotorId);
            return success;
        }*/
    }
}
