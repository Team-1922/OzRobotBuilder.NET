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
    public class OzSubsystemData : ITreeNodeSerialize
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
        
        public DataTreeNode GetTree()
        {
            DataTreeNode root = new DataTreeNode(Name, typeof(OzSubsystemData).ToString(), true, true);
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

        public bool DeserializeTree(DataTreeNode node)
        {
            if (node.Data != typeof(OzSubsystemData).ToString())
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
    }
}
