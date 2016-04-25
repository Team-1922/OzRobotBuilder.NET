using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RobotModel.ResourceManager.PrimaryTypes;

namespace RobotModel.ResourceManager.LoadableTypes
{
    class OzSubsystemResData
    {
        public List<OzMotorControllerData> PWMMotorControllers;
        //
        //These are separate, becuase they represent so much more than just a motor controller
        public List<OzTalonSRXData> TalonSRXs;
        public List<OzAnalogInputData> AnalogInputDevices;
        public List<OzQuadEncoderData> QuadEncoders;
        //
        //this is slightly different than the QuadEncoder, becuase it represents a binary input
        public List<OzDigitalInputData> DigitalInputs;

        public bool SoftwarePIDEnabled;
        public OzPIDControllerData PIDControllerConfig;

        
    }
}
