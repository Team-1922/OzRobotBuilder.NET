using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    public class OzDigitalInputData : ITreeNodeSerialize
    {
        public uint DigitalInputId;

        public string Name = "OzDigitalInputData";

        public bool DeserializeTree(DataTreeNode node)
        {
            if (typeof(OzDigitalInputData).ToString() != node.Data)
                return false;
            if (node.Key != Name)
                return false;
            if (node.Count != 1)
                return false;

            //TODO: at some point make the whole operation fail if just one of the suboperations fail and NOT overwrite the data

            //TODO: do this in some safe kind of way
            try
            {
                DigitalInputId = uint.Parse(node.GetChild("DigitalInputId").Data);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public DataTreeNode GetTree()
        {
            var root = new DataTreeNode(Name, typeof(OzDigitalInputData).ToString(), true, true);
            root.Add(new DataTreeNode("DigitalInputId", DigitalInputId.ToString()));
            return root;
        }
    }
}
