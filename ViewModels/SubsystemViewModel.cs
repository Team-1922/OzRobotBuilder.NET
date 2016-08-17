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
        
        public SubsystemViewModel(IHierarchialAccess topParent) : base(topParent)
        {
            _pwmOutputProviders = new CompoundProviderList<IPWMOutputProvider>("PWMOutputs", topParent);
            _analogInputProviders = new CompoundProviderList<IAnalogInputProvider>("AnalogInputs", topParent);
            _quadEncoderProviders = new CompoundProviderList<IQuadEncoderProvider>("QuadEncoders", topParent);
            _digitalInputProviders = new CompoundProviderList<IDigitalInputProvider>("DigitalInputs", topParent);
            _relayOutputProviders = new CompoundProviderList<IRelayOutputProvider>("RelayOutputs", topParent);
            _canTalonProviders = new CompoundProviderList<ICANTalonProvider>("CANTalons", topParent);
            PIDController = new PIDControllerSoftwareViewModel(topParent);
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
                    AddPWMOutput(pwmOutput, false);
                }
            }
            if (null != _subsystemModel.AnalogInput)
            {
                foreach (var analogInput in _subsystemModel.AnalogInput)
                {
                    AddAnalogInput(analogInput, false);
                }
            }
            if (null != _subsystemModel.QuadEncoder)
            {
                foreach (var quadEncoder in _subsystemModel.QuadEncoder)
                {
                    AddQuadEncoder(quadEncoder, false);
                }
            }
            if (null != _subsystemModel.RelayOutput)
            {
                foreach (var relayOutput in _subsystemModel.RelayOutput)
                {
                    AddRelayOutput(relayOutput, false);
                }
            }
            if (null != _subsystemModel.CANTalons)
            {
                foreach (var canTalon in _subsystemModel.CANTalons)
                {
                    AddCANTalon(canTalon, false);
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

        public IObservableCollection<IPWMOutputProvider> PWMOutputs
        {
            get { return _pwmOutputProviders.Items; }
        }
        public IObservableCollection<IAnalogInputProvider> AnalogInputs
        {
            get { return _analogInputProviders.Items; }
        }
        public IObservableCollection<IQuadEncoderProvider> QuadEncoders
        {
            get { return _quadEncoderProviders.Items; }
        }
        public IObservableCollection<IDigitalInputProvider> DigitalInputs
        {
            get
            {
                return _digitalInputProviders.Items;
            }
        }
        public IObservableCollection<IRelayOutputProvider> RelayOutputs
        {
            get { return _relayOutputProviders.Items; }
        }
        public IObservableCollection<ICANTalonProvider> CANTalons
        {
            get { return _canTalonProviders.Items; }
        }
        public IPIDControllerSoftwareProvider PIDController { get; }
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

        protected override string GetValue(string key)
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
        protected override void SetValue(string key, string value)
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
        public override string ModelTypeName
        {
            get
            {
                var brokenName = _subsystemModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        private void AddPWMOutput(PWMOutput pwmOutput, bool addToModel)
        {
            if (pwmOutput == null)
                throw new ArgumentNullException("pwmOutput");
            if(addToModel)
                _subsystemModel.PWMOutput.Add(pwmOutput);
            var provider = new PWMOutputViewModel(TopParent);
            provider.SetPWMOutput(pwmOutput);
            _pwmOutputProviders.Items.Add(provider);
        }

        private void AddAnalogInput(AnalogInput analogInput, bool addToModel)
        {
            if (analogInput == null)
                throw new ArgumentNullException("analogInput");
            if (addToModel)
                _subsystemModel.AnalogInput.Add(analogInput);
            var provider = new AnalogInputViewModel(TopParent);
            provider.SetAnalogInput(analogInput);
            _analogInputProviders.Items.Add(provider);
        }

        private void AddQuadEncoder(QuadEncoder quadEncoder, bool addToModel)
        {
            if (quadEncoder == null)
                throw new ArgumentNullException("quadEncoder");
            if (addToModel)
                _subsystemModel.QuadEncoder.Add(quadEncoder);

            var provider = new QuadEncoderViewModel(TopParent);
            provider.SetQuadEncoder(quadEncoder);
            _quadEncoderProviders.Items.Add(provider);
        }

        private void AddDigitalInput(DigitalInput digitalInput, bool addToModel)
        {
            if (digitalInput == null)
                throw new ArgumentNullException("digitalInput");
            if (addToModel)
                _subsystemModel.DigitalInput.Add(digitalInput);
            var provider = new DigitalInputViewModel(TopParent);
            provider.SetDigitalInput(digitalInput);
            _digitalInputProviders.Items.Add(provider);
        }

        private void AddRelayOutput(RelayOutput relayOutput, bool addToModel)
        {
            if (relayOutput == null)
                throw new ArgumentNullException("relayOutput");
            if (addToModel)
                _subsystemModel.RelayOutput.Add(relayOutput);
            var provider = new RelayOutputViewModel(TopParent);
            provider.SetRelayOutput(relayOutput);
            _relayOutputProviders.Items.Add(provider);
        }

        public void AddCANTalon(CANTalon canTalon, bool addToModel)
        {
            if (canTalon == null)
                throw new ArgumentNullException("canTalon");
            if (addToModel)
                _subsystemModel.CANTalons.Add(canTalon);
            var provider = new CANTalonViewModel(TopParent);
            provider.SetCANTalon(canTalon);
            _canTalonProviders.Items.Add(provider);
        }
        public void AddPWMOutput(PWMOutput pwmOutput)
        {
            AddPWMOutput(pwmOutput, true);
        }

        public void AddDigitalInput(DigitalInput digitalInput)
        {
            AddDigitalInput(digitalInput, true);
        }

        public void AddAnalogInput(AnalogInput analogInput)
        {
            AddAnalogInput(analogInput, true);
        }

        public void AddQuadEncoder(QuadEncoder quadEncoder)
        {
            AddQuadEncoder(quadEncoder, true);
        }

        public void AddRelayOutput(RelayOutput relayOutput)
        {
            AddRelayOutput(relayOutput, true);
        }

        public void AddCANTalon(CANTalon canTalon)
        {
            AddCANTalon(canTalon, true);
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
        CompoundProviderList<IDigitalInputProvider> _digitalInputProviders
        {
            get
            {
                return _children["_digitalInputProviders"] as CompoundProviderList<IDigitalInputProvider>;
            }

            set
            {
                _children["_digitalInputProviders"] = value;
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
