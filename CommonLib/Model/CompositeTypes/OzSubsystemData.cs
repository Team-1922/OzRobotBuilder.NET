using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib.Model.PrimaryTypes;
using CommonLib.Interfaces;

namespace CommonLib.Model.CompositeTypes
{
    public class OzSubsystemData //: ITreeNodeSerialize
    {
        public List<OzMotorControllerData> PWMMotorControllers = new List<OzMotorControllerData>();
        //
        //These are separate, becuase they represent so much more than just a motor controller
        public List<OzTalonSRXData> TalonSRXs = new List<OzTalonSRXData>();
        public List<OzAnalogInputData> AnalogInputDevices = new List<OzAnalogInputData>();
        public List<OzQuadEncoderData> QuadEncoders = new List<OzQuadEncoderData>();
        //
        //this is slightly different than the QuadEncoder, becuase it represents a binary input
        public List<OzDigitalInputData> DigitalInputs = new List<OzDigitalInputData>();

        public bool SoftwarePIDEnabled;
        public OzPIDControllerData PIDControllerConfig = new OzPIDControllerData();

        public ScriptExtensableData ScriptExtData = new ScriptExtensableData();

        public string Name = "SubsystemName";

        /*
        public DataTreeNode GetTree()
        {
            //make sure none of the children have the same name
            if(!AssertUniqueChildNames())
            {

            }

            DataTreeNode root = new DataTreeNode(Name, GetType().ToString(), true, true);
            foreach(var pwmMotor in PWMMotorControllers)
            {
                root.Add(pwmMotor.GetTree());
            }
            foreach (var srx in TalonSRXs)
            {
                root.Add(srx.GetTree());
            }
            foreach (var analogInput in AnalogInputDevices)
            {
                root.Add(analogInput.GetTree());
            }
            foreach (var quadEncoder in QuadEncoders)
            {
                root.Add(quadEncoder.GetTree());
            }
            foreach (var digitalInput in DigitalInputs)
            {
                root.Add(digitalInput.GetTree());
            }

            //add non-hierarchal attributes
            root.Add(new DataTreeNode("SoftwarePIDEnabled", SoftwarePIDEnabled.ToString()));
            var pidControllerCfgNode = PIDControllerConfig.GetTree();
            pidControllerCfgNode.Key = "SoftwarePIDControllerConfig";
            root.Add(pidControllerCfgNode);

            return root;
        }

        private bool AssertUniqueChildNames()
        {
            Dictionary<string, int> usedNames = new Dictionary<string, int>();
            try
            {
                foreach (var pwmMotor in PWMMotorControllers)
                {
                    usedNames.Add(pwmMotor.Name, 0);
                }
                foreach (var srx in TalonSRXs)
                {
                    usedNames.Add(srx.Name, 0);
                }
                foreach (var analogInput in AnalogInputDevices)
                {
                    usedNames.Add(analogInput.Name, 0);
                }
                foreach (var quadEncoder in QuadEncoders)
                {
                    usedNames.Add(quadEncoder.Name, 0);
                }
                foreach (var digitalInput in DigitalInputs)
                {
                    usedNames.Add(digitalInput.Name, 0);
                }
            }
            catch
            {
                //there were two children with the same name
                return false;
            }

            return true;
        }

        /*public bool DeserializeTree(DataTreeNode node)
        {
            if (node.Data != GetType().ToString() && node.KeyReadOnly && node.DataReadOnly)
                return false;
            if (Name != node.Key)
                return false;

            //for now just do the motor controllers, however eventually these will be all of the items
            foreach(var pwm in PWMMotorControllers)
            {
                var nextChild = node.GetChild(pwm.Name);
                if (null == nextChild)
                    return false;//TODO: log something?
            }

            return true;
        }

        public bool UpdateValue(string path, string value)
        {
            var strings = path.Split(new char[] { Path.DirectorySeparatorChar }, 2, StringSplitOptions.None);
            if (strings.Length != 2)
                return false;
            if (strings[0] != Name)
                return false;

            foreach(var pwm in PWMMotorControllers)
            {
                if (pwm.UpdateValue(strings[1], value))
                    return true;
            }
            foreach (var pwm in TalonSRXs)
            {
                if (pwm.UpdateValue(strings[1], value))
                    return true;
            }

            foreach (var pwm in AnalogInputDevices)
            {
                if (pwm.UpdateValue(strings[1], value))
                    return true;
            }

            foreach (var pwm in QuadEncoders)
            {
                if (pwm.UpdateValue(strings[1], value))
                    return true;
            }

            foreach (var pwm in DigitalInputs)
            {
                if (pwm.UpdateValue(strings[1], value))
                    return true;
            }

            return false;
        }
        */
    }
}
