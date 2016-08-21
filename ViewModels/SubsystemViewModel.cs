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
        
        public SubsystemViewModel(IRobotProvider parent) : base(parent)
        {
            _pwmOutputProviders = new CompoundProviderList<IPWMOutputProvider>("PWMOutputs", this);
            _analogInputProviders = new CompoundProviderList<IAnalogInputProvider>("AnalogInputs", this);
            _quadEncoderProviders = new CompoundProviderList<IQuadEncoderProvider>("QuadEncoders", this);
            _digitalInputProviders = new CompoundProviderList<IDigitalInputProvider>("DigitalInputs", this);
            _relayOutputProviders = new CompoundProviderList<IRelayOutputProvider>("RelayOutputs", this);
            _canTalonProviders = new CompoundProviderList<ICANTalonProvider>("CANTalons", this);
            PIDController = new PIDControllerSoftwareViewModel(this);
        }

        #region ISubsystemProvider
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
        public bool SoftwarePIDEnabled
        {
            get { return _subsystemModel.SoftwarePIDEnabled; }
            set { _subsystemModel.SoftwarePIDEnabled = value; }
        }

        private void AddPWMOutput(PWMOutput pwmOutput, bool addToModel)
        {
            if (pwmOutput == null)
                throw new ArgumentNullException("pwmOutput");
            if(addToModel)
                _subsystemModel.PWMOutput.Add(pwmOutput);
            var provider = new PWMOutputViewModel(this);
            provider.SetPWMOutput(pwmOutput);
            provider.Name = _pwmOutputProviders.GetUnusedKey(provider.Name);
            _pwmOutputProviders.Items.Add(provider);
        }

        private void AddAnalogInput(AnalogInput analogInput, bool addToModel)
        {
            if (analogInput == null)
                throw new ArgumentNullException("analogInput");
            if (addToModel)
                _subsystemModel.AnalogInput.Add(analogInput);
            var provider = new AnalogInputViewModel(this);
            provider.SetAnalogInput(analogInput);
            provider.Name = _analogInputProviders.GetUnusedKey(provider.Name);
            _analogInputProviders.Items.Add(provider);
        }

        private void AddQuadEncoder(QuadEncoder quadEncoder, bool addToModel)
        {
            if (quadEncoder == null)
                throw new ArgumentNullException("quadEncoder");
            if (addToModel)
                _subsystemModel.QuadEncoder.Add(quadEncoder);

            var provider = new QuadEncoderViewModel(this);
            provider.SetQuadEncoder(quadEncoder);
            provider.Name = _quadEncoderProviders.GetUnusedKey(provider.Name);
            _quadEncoderProviders.Items.Add(provider);
        }

        private void AddDigitalInput(DigitalInput digitalInput, bool addToModel)
        {
            if (digitalInput == null)
                throw new ArgumentNullException("digitalInput");
            if (addToModel)
                _subsystemModel.DigitalInput.Add(digitalInput);
            var provider = new DigitalInputViewModel(this);
            provider.SetDigitalInput(digitalInput);
            provider.Name = _digitalInputProviders.GetUnusedKey(provider.Name);
            _digitalInputProviders.Items.Add(provider);
        }

        private void AddRelayOutput(RelayOutput relayOutput, bool addToModel)
        {
            if (relayOutput == null)
                throw new ArgumentNullException("relayOutput");
            if (addToModel)
                _subsystemModel.RelayOutput.Add(relayOutput);
            var provider = new RelayOutputViewModel(this);
            provider.SetRelayOutput(relayOutput);
            provider.Name = _relayOutputProviders.GetUnusedKey(provider.Name);
            _relayOutputProviders.Items.Add(provider);
        }

        public void AddCANTalon(CANTalon canTalon, bool addToModel)
        {
            if (canTalon == null)
                throw new ArgumentNullException("canTalon");
            if (addToModel)
                _subsystemModel.CANTalons.Add(canTalon);
            var provider = new CANTalonViewModel(this);
            provider.SetCANTalon(canTalon);
            provider.Name = _canTalonProviders.GetUnusedKey(provider.Name);
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

        public void RemovePWMOutput(string name)
        {
            for (int i = 0; i < _pwmOutputProviders.Items.Count; ++i)
            {
                if (_pwmOutputProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _pwmOutputProviders.Items.RemoveAt(i);

                    //remove the model instance
                    _subsystemModel.PWMOutput.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveDigitalInput(string name)
        {
            for (int i = 0; i < _digitalInputProviders.Items.Count; ++i)
            {
                if (_digitalInputProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _digitalInputProviders.Items.RemoveAt(i);

                    //remove the model instance
                    _subsystemModel.DigitalInput.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveAnalogInput(string name)
        {
            for (int i = 0; i < _analogInputProviders.Items.Count; ++i)
            {
                if (_analogInputProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _analogInputProviders.Items.RemoveAt(i);

                    //remove the model instance
                    _subsystemModel.AnalogInput.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveQuadEncoder(string name)
        {
            for (int i = 0; i < _quadEncoderProviders.Items.Count; ++i)
            {
                if (_quadEncoderProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _quadEncoderProviders.Items.RemoveAt(i);

                    //remove the model instance
                    _subsystemModel.QuadEncoder.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveRelayOutput(string name)
        {
            for (int i = 0; i < _relayOutputProviders.Items.Count; ++i)
            {
                if (_relayOutputProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _relayOutputProviders.Items.RemoveAt(i);

                    //remove the model instance
                    _subsystemModel.RelayOutput.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveCANTalon(string name)
        {
            for (int i = 0; i < _canTalonProviders.Items.Count; ++i)
            {
                if (_canTalonProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _canTalonProviders.Items.RemoveAt(i);

                    //remove the model instance
                    _subsystemModel.CANTalons.RemoveAt(i);
                    break;
                }
            }
        }
        public int ID
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region ICompoundProvider
        public IEnumerable<IProvider> Children
        {
            get
            {
                return _children.Values;
            }
        }
        #endregion

        #region IProvider
        public string Name
        {
            get { return _subsystemModel.Name; }
            set { _subsystemModel.Name = value; }
        }
        public string GetModelJson()
        {
            return JsonSerialize(_subsystemModel);
        }
        public void SetModelJson(string text)
        {
            SetSubsystem(JsonDeserialize<Subsystem>(text));
        }
        #endregion

        #region ViewModelBase

        protected override string GetValue(string key)
        {
            switch (key)
            {
                case "AnalogInputs":
                    return _analogInputProviders.GetModelJson();
                case "CANTalons":
                    return _canTalonProviders.GetModelJson();
                case "PIDController":
                    return PIDController.GetModelJson();
                case "DigitalInputs":
                    return _digitalInputProviders.GetModelJson();
                case "PWMOutputs":
                    return _pwmOutputProviders.GetModelJson();
                case "QuadEncoders":
                    return _quadEncoderProviders.GetModelJson();
                case "RelayOutputs":
                    return _relayOutputProviders.GetModelJson();

                case "SoftwarePIDEnabled":
                    return SoftwarePIDEnabled.ToString();
                case "ID":
                    return ID.ToString();
                case "Name":
                    return Name;
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }
        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "AnalogInputs":
                    _analogInputProviders.SetModelJson(value);
                    break;
                case "CANTalons":
                    _canTalonProviders.SetModelJson(value);
                    break;
                case "PIDController":
                    PIDController.SetModelJson(value);
                    break;
                case "DigitalInputs":
                    _digitalInputProviders.SetModelJson(value);
                    break;
                case "PWMOutputs":
                    _pwmOutputProviders.SetModelJson(value);
                    break;
                case "QuadEncoders":
                    _quadEncoderProviders.SetModelJson(value);
                    break;
                case "RelayOutputs":
                    _relayOutputProviders.SetModelJson(value);
                    break;

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
        #endregion

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
