using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class SubsystemViewModel : ViewModelBase, ISubsystemProvider
    {
        protected Subsystem _subsystemModel;
        
        public SubsystemViewModel()
        {
            _pwmOutputProviders = new CompoundProviderList<IPWMOutputProvider>("PWMOutputs");
            _analogInputProviders = new CompoundProviderList<IAnalogInputProvider>("AnalogInputs");
            _quadEncoderProviders = new CompoundProviderList<IQuadEncoderProvider>("QuadEncoders");
            _relayOutputProviders = new CompoundProviderList<IRelayOutputProvider>("RelayOutputs");
            _canTalonProviders = new CompoundProviderList<ICANTalonProvider>("CANTalons");
        }

        public void SetSubsystem(Subsystem subsystem)
        {
            //cleanup the old providers
            _pwmOutputProviders.Items.Clear();
            _analogInputProviders.Items.Clear();
            _quadEncoderProviders.Items.Clear();
            _relayOutputProviders.Items.Clear();
            _canTalonProviders.Items.Clear();

            //setup the new providers
            _subsystemModel = subsystem;
            PIDController.SetPIDController(_subsystemModel.PIDController);

            if (null != _subsystemModel.PWMOutput)
            {
                foreach (var pwmOutput in _subsystemModel.PWMOutput)
                {
                    if (pwmOutput == null)
                        continue;
                    var provider = new PWMOutputViewModel();
                    provider.SetPWMOutput(pwmOutput);
                    _pwmOutputProviders.Items.Add(provider);
                }
            }
            if (null != _subsystemModel.AnalogInput)
            {
                foreach (var analogInput in _subsystemModel.AnalogInput)
                {
                    if (analogInput == null)
                        continue;
                    var provider = new AnalogInputViewModel();
                    provider.SetAnalogInput(analogInput);
                    _analogInputProviders.Items.Add(provider);
                }
            }
            if (null != _subsystemModel.QuadEncoder)
            {
                foreach (var quadEncoder in _subsystemModel.QuadEncoder)
                {
                    if (quadEncoder == null)
                        continue;
                    var provider = new QuadEncoderViewModel();
                    provider.SetQuadEncoder(quadEncoder);
                    _quadEncoderProviders.Items.Add(provider);
                }
            }
            if (null != _subsystemModel.RelayOutput)
            {
                foreach (var relayOutput in _subsystemModel.RelayOutput)
                {
                    if (relayOutput == null)
                        continue;
                    var provider = new RelayOutputViewModel();
                    provider.SetRelayOutput(relayOutput);
                    _relayOutputProviders.Items.Add(provider);
                }
            }
            if (null != _subsystemModel.CANTalons)
            {
                foreach (var canTalon in _subsystemModel.CANTalons)
                {
                    if (canTalon == null)
                        continue;
                    var provider = new CANTalonViewModel();
                    provider.SetCANTalon(canTalon);
                    _canTalonProviders.Items.Add(provider);
                }
            }
        }

        public void UpdateInputValues()
        {
            foreach(var analogInput in _analogInputProviders.Items)
            {
                analogInput.UpdateInputValues();
            }
            foreach (var quadEncoder in _quadEncoderProviders.Items)
            {
                quadEncoder.UpdateInputValues();
            }
            foreach (var canTalon in _canTalonProviders.Items)
            {
                canTalon.UpdateInputValues();
            }
        }

        public IEnumerable<IPWMOutputProvider> PWMOutputs
        {
            get { return _pwmOutputProviders.Items; }
        }
        public IEnumerable<IAnalogInputProvider> AnalogInputs
        {
            get { return _analogInputProviders.Items; }
        }
        public IEnumerable<IQuadEncoderProvider> QuadEncoders
        {
            get { return _quadEncoderProviders.Items; }
        }
        public IEnumerable<IRelayOutputProvider> RelayOutputs
        {
            get { return _relayOutputProviders.Items; }
        }
        public IEnumerable<ICANTalonProvider> CANTalons
        {
            get { return _canTalonProviders.Items; }
        }
        public IPIDControllerSoftwareProvider PIDController { get; } = new PIDControllerSoftwareViewModel();
        public string Name
        {
            get { return _subsystemModel.Name; }
            set { _subsystemModel.Name = value; }
        }
        public bool SoftwarePIDEnabled
        {
            get { return _subsystemModel.SoftwarePIDEnabled; }
            set { _subsystemModel.SoftwarePIDEnabled = value; }
        }

        public IEnumerable<IProvider> Children
        {
            get
            {
                return _children.Values;
            }
        }

        public int ID
        {
            get
            {
                return _subsystemModel.ID;
            }
        }

        public override string this[string key]
        {
            get
            {
                switch(key)
                {
                    case "AnalogInputs":
                        return AnalogInputs.ToString();
                    case "CANTalons":
                        return CANTalons.ToString();
                    case "ID":
                        return ID.ToString();
                    case "Name":
                        return Name;
                    case "PIDController":
                        return PIDController.ToString();
                    case "PWMOutputs":
                        return PWMOutputs.ToString();
                    case "QuadEncoders":
                        return QuadEncoders.ToString();
                    case "RelayOutputs":
                        return RelayOutputs.ToString();
                    case "SoftwarePIDEnabled":
                        return SoftwarePIDEnabled.ToString();
                    default:
                        throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
                }
            }

            set
            {
                switch(key)
                {
                    case "Name":
                        Name = value;
                        break;
                    case "SoftwarePIDEnabled":
                        SoftwarePIDEnabled = SafeCastBool(value);
                        break;
                    default:
                        throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
                }
            }
        }

        #region Private Fields
        Dictionary<string, IProvider> _children = new Dictionary<string, IProvider>();
        CompoundProviderList<IPWMOutputProvider> _pwmOutputProviders
        {
            get
            {
                return _children["_pwmOutputProviders"] as CompoundProviderList<IPWMOutputProvider>;
            }

            set
            {
                _children["_pwmOutputProviders"] = value;
            }
        }
        CompoundProviderList<IAnalogInputProvider> _analogInputProviders
        {
            get
            {
                return _children["_analogInputProviders"] as CompoundProviderList<IAnalogInputProvider>;
            }

            set
            {
                _children["_analogInputProviders"] = value;
            }
        }
        CompoundProviderList<IQuadEncoderProvider> _quadEncoderProviders
        {
            get
            {
                return _children["_quadEncoderProviders"] as CompoundProviderList<IQuadEncoderProvider>;
            }

            set
            {
                _children["_quadEncoderProviders"] = value;
            }
        }
        CompoundProviderList<IRelayOutputProvider> _relayOutputProviders
        {
            get
            {
                return _children["_relayOutputProviders"] as CompoundProviderList<IRelayOutputProvider>;
            }

            set
            {
                _children["_relayOutputProviders"] = value;
            }
        }
        CompoundProviderList<ICANTalonProvider> _canTalonProviders
        {
            get
            {
                return _children["_canTalonProviders"] as CompoundProviderList<ICANTalonProvider>;
            }

            set
            {
                _children["_canTalonProviders"] = value;
            }
        }
        #endregion
    }
}
