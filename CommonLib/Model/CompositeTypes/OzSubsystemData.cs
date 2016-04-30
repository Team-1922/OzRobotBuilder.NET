using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib.Model.PrimaryTypes;
using CommonLib.Interfaces;

namespace CommonLib.Model.Compositetypes
{
    public class OzSubsystemData
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
    }
}
