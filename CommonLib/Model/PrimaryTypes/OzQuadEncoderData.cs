using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
	/*
	 * 
	 * 
	 * 
	 */
    public class OzQuadEncoderData //: ITreeNodeSerialize
    {
		//while having a 'UnitsPerRotation' value is nice, this piece of data is usually up to emprical testing, therefore
		//		a second attribute for 'UnitsPerRotation' would be pointless
        public double ConversionRatio;

		//This value could exist, but in the context encoders are usually used, it would be pointless
        //public double InitialOffset;

		//the digital in location of the first three encoder wires (this is -1 on TalonSRX)
        public uint DigitalIn0;

		//the digital in location of the final encoder wire (this is -1 on TalonSRX)
        public uint DigitalIn1;

        public string Name = "OzQuadEncoderData";

        /*public bool DeserializeTree(DataTreeNode node)
        {
            if (typeof(OzQuadEncoderData).ToString() != node.Data)
                return false;
            if (node.Key != Name)
                return false;
            if (node.Count != 3)
                return false;

            //TODO: at some point make the whole operation fail if just one of the suboperations fail and NOT overwrite the data

            //TODO: do this in some safe kind of way
            try
            {
                ConversionRatio = double.Parse(node.GetChild("ConversionRatio").Data);
                DigitalIn0 = uint.Parse(node.GetChild("DigitalIn0").Data);
                DigitalIn1 = uint.Parse(node.GetChild("DigitalIn1").Data);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public DataTreeNode GetTree()
        {
            var root = new DataTreeNode(Name, typeof(OzQuadEncoderData).ToString(), true, true);
            root.Add(new DataTreeNode("ConversionRatio", ConversionRatio.ToString()));
            root.Add(new DataTreeNode("DigitalIn0", DigitalIn0.ToString()));
            root.Add(new DataTreeNode("DigitalIn1", DigitalIn1.ToString()));
            return root;
        }*/
    }
}
