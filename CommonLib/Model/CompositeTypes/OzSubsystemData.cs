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
    public class OzSubsystemData : INamedClass
    {
        /// <summary>
        /// The Name of the subsystem
        /// </summary>
        public string Name { get; set; } = "SubsystemName";
        /// <summary>
        /// A List of Motor Controller Datas for PWM motor controllers
        /// </summary>
        public UniqueItemList<OzMotorControllerData> PWMMotorControllers { get; set; } = new UniqueItemList<OzMotorControllerData>();
        /// <summary>
        /// A List of Motor Controller Datas for Talon SRX motor controllers
        /// </summary>
        public UniqueItemList<OzTalonSRXData> TalonSRXs { get; set; } = new UniqueItemList<OzTalonSRXData>();
        /// <summary>
        /// A List of Analog Input Datas
        /// </summary>
        public UniqueItemList<OzAnalogInputData> AnalogInputDevices { get; set; } = new UniqueItemList<OzAnalogInputData>();
        /// <summary>
        /// A List of Quadrature Encoders Datas
        /// </summary>
        public UniqueItemList<OzQuadEncoderData> QuadEncoders { get; set; } = new UniqueItemList<OzQuadEncoderData>();
        /// <summary>
        /// A List of Digital Input Datas (binary inputs)
        /// </summary>
        public UniqueItemList<OzDigitalInputData> DigitalInputs { get; set; } = new UniqueItemList<OzDigitalInputData>();
        /// <summary>
        /// Whether or not the software PID controller is enabled
        /// </summary>
        public bool SoftwarePIDEnabled { get; set; } = false;
        /// <summary>
        /// The Data for the software pid controller
        /// </summary>
        public OzPIDControllerData PIDControllerConfig { get; set; } = new OzPIDControllerData();
        /// <summary>
        /// The data for overriding default methods of a subsystem and adding new ones
        /// </summary>
        public ScriptExtensableData ScriptExtData { get; set; } = new ScriptExtensableData();
    }
}
