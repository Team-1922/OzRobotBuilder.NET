using CommonLib.Interfaces;
using CommonLib.Model.CompositeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.Documents
{
    public class RobotDocument : Document//, ITreeNodeSerialize
    {
        public string Name { get; private set; } = "RobotDocument";
        public List<OzSubsystemData> Subsystems = new List<OzSubsystemData>();
        public List<OzCommandData> Commands = new List<OzCommandData>();

        /*public bool DeserializeTree(DataTreeNode node)
        {
            //this should be the parent of the whole tree
            if (node.Key != "RobotDocument" || node.Data != "RobotDocument")
                return false;

            //deserialize the children
            foreach(var subsystem in Subsystems)
            {
                var subsystemNode = node.GetChild(subsystem.Name);
                if(null == subsystemNode)
                {
                    //well this means the tree is bad
                    Console.WriteLine("Attempt to Load Tree Node into Robot Data, But The Tree was Broken");
                    return false;
                }
            }

            return true;
        }

        public DataTreeNode GetTree()
        {
            var root = new DataTreeNode("RobotDocument", "RobotDocument", true, true);

            //add the subsystems
            var subsystemRoot = new DataTreeNode("Subsystems", "", true, true);
            foreach (var subsystem in Subsystems)
            {
                subsystemRoot.Add(subsystem.GetTree());
            }
            root.Add(subsystemRoot);

            //add the commands (eventually)

            return root;
        }*/
    }
}
