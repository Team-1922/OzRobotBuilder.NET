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
        public string Name = "SubsystemName";
        /// <summary>
        /// A List of Motor Controller Datas for PWM motor controllers
        /// </summary>
        public UniqueItemList<OzMotorControllerData> PWMMotorControllers = new UniqueItemList<OzMotorControllerData>();
        /// <summary>
        /// A List of Motor Controller Datas for Talon SRX motor controllers
        /// </summary>
        public UniqueItemList<OzTalonSRXData> TalonSRXs = new UniqueItemList<OzTalonSRXData>();
        /// <summary>
        /// A List of Analog Input Datas
        /// </summary>
        public UniqueItemList<OzAnalogInputData> AnalogInputDevices = new UniqueItemList<OzAnalogInputData>();
        /// <summary>
        /// A List of Quadrature Encoders Datas
        /// </summary>
        public UniqueItemList<OzQuadEncoderData> QuadEncoders = new UniqueItemList<OzQuadEncoderData>();
        /// <summary>
        /// A List of Digital Input Datas (binary inputs)
        /// </summary>
        public UniqueItemList<OzDigitalInputData> DigitalInputs = new UniqueItemList<OzDigitalInputData>();
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
        #region INamedClass Interface
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string GetName()
        {
            return Name;
        }
        public void SetName(string name)
        {
            Name = name;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion
    }
}
