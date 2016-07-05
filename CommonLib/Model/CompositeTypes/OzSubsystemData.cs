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
    /// <summary>
    /// A subsystem with all of its possible inputs and outputs
    /// </summary>
    public class OzSubsystemData
    {
        /// <summary>
        /// The Name of the subsystem
        /// </summary>
        public string Name = "SubsystemName";
        /// <summary>
        /// A List of Motor Controller Datas for PWM motor controllers
        /// </summary>
        public List<OzMotorControllerData> PWMMotorControllers = new List<OzMotorControllerData>();
        /// <summary>
        /// A List of Motor Controller Datas for Talon SRX motor controllers
        /// </summary>
        public List<OzTalonSRXData> TalonSRXs = new List<OzTalonSRXData>();
        /// <summary>
        /// A List of Analog Input Datas
        /// </summary>
        public List<OzAnalogInputData> AnalogInputDevices = new List<OzAnalogInputData>();
        /// <summary>
        /// A List of Quadrature Encoders Datas
        /// </summary>
        public List<OzQuadEncoderData> QuadEncoders = new List<OzQuadEncoderData>();
        /// <summary>
        /// A List of Digital Input Datas (binary inputs)
        /// </summary>
        public List<OzDigitalInputData> DigitalInputs = new List<OzDigitalInputData>();
        /// <summary>
        /// Whether or not the software PID controller is enabled
        /// </summary>
        public bool SoftwarePIDEnabled = false;
        /// <summary>
        /// The Data for the software pid controller
        /// </summary>
        public OzPIDControllerData PIDControllerConfig = new OzPIDControllerData();
        /// <summary>
        /// The data for overriding default methods of a subsystem and adding new ones
        /// </summary>
        public ScriptExtensableData ScriptExtData = new ScriptExtensableData();
        
    }
}
