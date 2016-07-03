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
    public class OzAnalogInputData : ITreeNodeSerialize
    {
        public static double GlobalSampleRate;

        public string Name = "OzAnalogInputData";
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

        public virtual DataTreeNode GetTree()
        {
            var root = new DataTreeNode(Name, typeof(OzAnalogInputData).ToString(), true, true);
            root.Add(new DataTreeNode("AnalogInputId", AnalogInputId.ToString()));
            root.Add(new DataTreeNode("AccumulatorCenter", AccumulatorCenter.ToString()));
            root.Add(new DataTreeNode("AccumulatorDeadband", AccumulatorDeadband.ToString()));
            root.Add(new DataTreeNode("AccumulatorInitialValue", AccumulatorInitialValue.ToString()));
            root.Add(new DataTreeNode("AverageBits", AverageBits.ToString()));
            root.Add(new DataTreeNode("OversampleBits", OversampleBits.ToString()));
            root.Add(new DataTreeNode("ConversionRatio", ConversionRatio.ToString()));
            root.Add(new DataTreeNode("SensorOffset", SensorOffset.ToString()));
            return root;
        }
        public virtual bool DeserializeTree(DataTreeNode node)
        {
            if (typeof(OzAnalogInputData).ToString() != node.Data)
                return false;
            if (node.Key != Name)
                return false;
            if (node.Count != 8)
                return false;

            //TODO: at some point make the whole operation fail if just one of the suboperations fail and NOT overwrite the data

            //TODO: do this in some safe kind of way
            try
            {
                AnalogInputId = uint.Parse(node.GetChild("AnalogInputId").Data);
                AccumulatorCenter = int.Parse(node.GetChild("AccumulatorCenter").Data);
                AccumulatorDeadband = int.Parse(node.GetChild("AccumulatorDeadband").Data);
                AccumulatorInitialValue = long.Parse(node.GetChild("AccumulatorInitialValue").Data);
                AverageBits = int.Parse(node.GetChild("AverageBits").Data);
                OversampleBits = int.Parse(node.GetChild("OversampleBits").Data);
                ConversionRatio = double.Parse(node.GetChild("ConversionRatio").Data);
                SensorOffset = double.Parse(node.GetChild("SensorOffset").Data);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
