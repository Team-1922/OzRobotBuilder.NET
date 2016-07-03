using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /*
     * 
     * PID Configuration for software PID Controller (use OzSRXPIDControllerData for SRX PID configuration)
     * 
     */
    public class OzPIDControllerData : ITreeNodeSerialize
    {
        public double P;
        public double I;
        public double D;
        public double F;

        //the time in seconds between update cycles (this is irrelevent on the SRX)
        public double CycleDuration;
        //for use with continuous rotation potentiometers        
        public bool Continuous;

        public DataTreeNode GetTree()
        {
            var root = new DataTreeNode("OzPIDControllerData", typeof(OzPIDControllerData).ToString(), true, true);
            root.Add(new DataTreeNode("P", P.ToString()));
            root.Add(new DataTreeNode("I", I.ToString()));
            root.Add(new DataTreeNode("D", D.ToString()));
            root.Add(new DataTreeNode("F", F.ToString()));
            root.Add(new DataTreeNode("CycleDuration", CycleDuration.ToString()));
            root.Add(new DataTreeNode("Continuous", Continuous.ToString()));
            return root;
        }

        public bool DeserializeTree(DataTreeNode node)
        {
            if (typeof(OzPIDControllerData).ToString() != node.Data)
                return false;
            if (node.Count != 6)
                return false;

            //TODO: at some point make the whole operation fail if just one of the suboperations fail and NOT overwrite the data

            //TODO: do this in some safe kind of way
            try
            {
                P = double.Parse(node.GetChild("P").Data);
                I = double.Parse(node.GetChild("I").Data);
                D = double.Parse(node.GetChild("D").Data);
                F = double.Parse(node.GetChild("F").Data);
                CycleDuration = double.Parse(node.GetChild("CycleDuration").Data);
                Continuous = bool.Parse(node.GetChild("Continuous").Data);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
