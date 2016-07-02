using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    public class OzMotorControllerData : ITreeNodeSerialize
    {
        public string Name;
        public uint MotorId;

        public virtual DataTreeNode GetTree()
        {
            return new DataTreeNode(Name, MotorId.ToString());
        }
        public virtual bool DeserializeTree(DataTreeNode node)
        {
            if (node.Key != Name)
                return false;

            bool success = uint.TryParse(node.Data, out MotorId);
            return success;
        }
    }
}
