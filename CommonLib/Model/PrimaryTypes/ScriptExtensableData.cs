using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    public class ScriptExtensableData : ITreeNodeSerialize
    {
        public string ScriptExtension;
        public string OverriddenMethods;

        public DataTreeNode GetTree()
        {
            var root = new DataTreeNode("ScriptExtensableData", typeof(ScriptExtensableData).ToString(), true, true);
            root.Add(new DataTreeNode("ScriptExtension", ScriptExtension));
            root.Add(new DataTreeNode("OverriddenMethods", OverriddenMethods));
            return root;
        }

        public bool DeserializeTree(DataTreeNode node)
        {
            if (typeof(OzDigitalInputData).ToString() != node.Data)
                return false;
            if (node.Count != 2)
                return false;

            //TODO: at some point make the whole operation fail if just one of the suboperations fail and NOT overwrite the data

            //TODO: do this in some safe kind of way
            try
            {
                ScriptExtension = node.GetChild("ScriptExtension").Data;
                OverriddenMethods = node.GetChild("OverriddenMethods").Data;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
