using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    public class OzMotorControllerData //: ITreeNodeSerialize
    {
        public string Name = "OzMotorControllerData";
        public uint MotorId;

        /*public virtual DataTreeNode GetTree()
        {
            var root = new DataTreeNode(Name, GetType().ToString());
            root.Add(new DataTreeNode("MotorId", MotorId.ToString()));
            return root;
        }

        public virtual bool UpdateValue(string path, string value)
        {
            var strings = path.Split(new char[] { Path.DirectorySeparatorChar }, 2, StringSplitOptions.None);
            if (strings.Length != 2)
                return false;
            if (strings[0] != Name)
                return false;
            if (path != "MotorId")
                return false;

            return uint.TryParse(value, out MotorId);
        }*/
    }
}
