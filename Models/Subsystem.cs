using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models.Deprecated.BaseTypes;

namespace Team1922.MVVM.Models.Deprecated
{
    /// <summary>
    /// A subsystem with all of its possible inputs and outputs
    /// TODO: is it possible to have the collections be readonly, however still be loaded by json/xml serialization?
    /// </summary>
    public class Subsystem : BindableBase, INamedClass
    {
        /// <summary>
        /// The Name of the subsystem
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// A List of Motor Controller Datas for PWM motor controllers
        /// </summary>
        public ObservableCollection<PWMMotorController> PWMMotorControllers
        {
            get { return _pwmMotorControllers; }
            set { SetProperty(ref _pwmMotorControllers, value); }
        }
        /// <summary>
        /// A List of Motor Controller Datas for Talon SRX motor controllers
        /// </summary>
        public ObservableCollection<CANTalon> CANTalons
        {
            get { return _canTalons;  }
            set { SetProperty(ref _canTalons, value); }
        }
        /// <summary>
        /// A List of Analog Input Datas
        /// </summary>
        public ObservableCollection<AnalogInput> AnalogInputs
        {
            get { return _analogInputs; }
            set { SetProperty(ref _analogInputs, value); }
        }
        /// <summary>
        /// A List of Quadrature Encoders Datas
        /// </summary>
        public ObservableCollection<QuadEncoder> QuadEncoders
        {
            get { return _quadEncoders; }
            set { SetProperty(ref _quadEncoders, value); }
        }
        /// <summary>
        /// A List of Digital Input Datas (binary inputs)
        /// </summary>
        public ObservableCollection<DigitalInput> DigitalInputs
        {
            get { return _digitalInputs; }
            set { SetProperty(ref _digitalInputs, value); }
        }
        /// <summary>
        /// Whether or not the software PID controller is enabled
        /// </summary>
        public bool SoftwarePIDEnabled
        {
            get { return _softwarePIDEnabled; }
            set { SetProperty(ref _softwarePIDEnabled, value); }
        }
        /// <summary>
        /// The Data for the software pid controller
        /// </summary>
        public PIDController PIDControllerConfig
        {
            get { return _pidControllerConfig; }
            set { SetProperty(ref _pidControllerConfig, value); }
        }

        #region Private Fields
        private string _name = "SubsystemName";
        private ObservableCollection<PWMMotorController> _pwmMotorControllers = new ObservableCollection<PWMMotorController>();
        private ObservableCollection<CANTalon> _canTalons = new ObservableCollection<CANTalon>();
        private ObservableCollection<AnalogInput> _analogInputs = new ObservableCollection<AnalogInput>();
        private ObservableCollection<QuadEncoder> _quadEncoders = new ObservableCollection<QuadEncoder>();
        private ObservableCollection<DigitalInput> _digitalInputs = new ObservableCollection<DigitalInput>();
        private bool _softwarePIDEnabled = false;
        private PIDController _pidControllerConfig = new PIDController();
        #endregion
    }
}
