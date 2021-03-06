﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class SubsystemViewModel : CompoundViewModelBase<Subsystem>, ISubsystemProvider
    {        
        public SubsystemViewModel(IProvider parent) : base(parent)
        {
            _pwmOutputProviders = new CompoundProviderList<IPWMOutputProvider, PWMOutput>("PWMOutputs", this, (PWMOutput modelReference) => { return new PWMOutputViewModel(_pwmOutputProviders) { ModelReference = modelReference }; });
            _analogInputProviders = new CompoundProviderList<IAnalogInputProvider, AnalogInput>("AnalogInputs", this, (AnalogInput modelReference) => { return new AnalogInputViewModel(_analogInputProviders) { ModelReference = modelReference }; });
            _quadEncoderProviders = new CompoundProviderList<IQuadEncoderProvider, QuadEncoder>("QuadEncoders", this, (QuadEncoder modelReference) => { return new QuadEncoderViewModel(_quadEncoderProviders) { ModelReference = modelReference }; });
            _digitalInputProviders = new CompoundProviderList<IDigitalInputProvider, DigitalInput>("DigitalInputs", this, (DigitalInput modelReference) => { return new DigitalInputViewModel(_digitalInputProviders) { ModelReference = modelReference }; });
            _relayOutputProviders = new CompoundProviderList<IRelayOutputProvider, RelayOutput>("RelayOutputs", this, (RelayOutput modelReference) => { return new RelayOutputViewModel(this) { ModelReference = modelReference }; });
            _canTalonProviders = new CompoundProviderList<ICANTalonProvider, CANTalon>("CANTalons", this, (CANTalon modelReference) => { return new CANTalonViewModel(_canTalonProviders) { ModelReference = modelReference }; });
            PIDController = new PIDControllerSoftwareViewModel(this);
        }

        #region ISubsystemProvider
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
            get { return ModelReference.SoftwarePIDEnabled; }
            set { ModelReference.SoftwarePIDEnabled = value; }
        }

        private void AddPWMOutput(PWMOutput pwmOutput, bool addToModel)
        {
            if (pwmOutput == null)
                throw new ArgumentNullException("pwmOutput");
            if (addToModel)
            {
                TopParent.SetAsync($"{FullyQualifiedName}.{_pwmOutputProviders.Name}.{pwmOutput.Name}", JsonSerialize(pwmOutput));
                return;
                //ModelReference.PWMOutput.Add(pwmOutput);
            }
            //var provider = new PWMOutputViewModel(this);
            //provider.ModelReference = pwmOutput;
            //provider.Name = _pwmOutputProviders.GetUnusedKey(provider.Name);
            //_pwmOutputProviders.Items.Add(provider);
            _pwmOutputProviders.AddOrUpdate(pwmOutput);
        }

        private void AddAnalogInput(AnalogInput analogInput, bool addToModel)
        {
            if (analogInput == null)
                throw new ArgumentNullException("analogInput");
            if (addToModel)
            {
                TopParent.SetAsync($"{FullyQualifiedName}.{_analogInputProviders.Name}.{analogInput.Name}", JsonSerialize(analogInput));
                return;
                //ModelReference.AnalogInput.Add(analogInput);
            }
            //var provider = new AnalogInputViewModel(this);
            //provider.ModelReference = analogInput;
            //provider.Name = _analogInputProviders.GetUnusedKey(provider.Name);
            //_analogInputProviders.Items.Add(provider);
            _analogInputProviders.AddOrUpdate(analogInput);
        }

        private void AddQuadEncoder(QuadEncoder quadEncoder, bool addToModel)
        {
            if (quadEncoder == null)
                throw new ArgumentNullException("quadEncoder");
            if (addToModel)
            {
                TopParent.SetAsync($"{FullyQualifiedName}.{_quadEncoderProviders.Name}.{quadEncoder.Name}", JsonSerialize(quadEncoder));
                return;
                //ModelReference.QuadEncoder.Add(quadEncoder);
            }

            //var provider = new QuadEncoderViewModel(this);
            //provider.ModelReference = quadEncoder;
            //provider.Name = _quadEncoderProviders.GetUnusedKey(provider.Name);
            //_quadEncoderProviders.Items.Add(provider);
            _quadEncoderProviders.AddOrUpdate(quadEncoder);
        }

        private void AddDigitalInput(DigitalInput digitalInput, bool addToModel)
        {
            if (digitalInput == null)
                throw new ArgumentNullException("digitalInput");
            if (addToModel)
            {
                TopParent.SetAsync($"{FullyQualifiedName}.{_digitalInputProviders.Name}.{digitalInput.Name}", JsonSerialize(digitalInput));
                return;
                //ModelReference.DigitalInput.Add(digitalInput);
            }
            //var provider = new DigitalInputViewModel(this);
            //provider.ModelReference = digitalInput;
            //provider.Name = _digitalInputProviders.GetUnusedKey(provider.Name);
            //_digitalInputProviders.Items.Add(provider);
            _digitalInputProviders.AddOrUpdate(digitalInput);
        }

        private void AddRelayOutput(RelayOutput relayOutput, bool addToModel)
        {
            if (relayOutput == null)
                throw new ArgumentNullException("relayOutput");
            if (addToModel)
            {
                TopParent.SetAsync($"{FullyQualifiedName}.{_relayOutputProviders.Name}.{relayOutput.Name}", JsonSerialize(relayOutput));
                return;
                //ModelReference.RelayOutput.Add(relayOutput);
            }
            //var provider = new RelayOutputViewModel(this);
            //provider.ModelReference = relayOutput;
            //provider.Name = _relayOutputProviders.GetUnusedKey(provider.Name);
            //_relayOutputProviders.Items.Add(provider);
            _relayOutputProviders.AddOrUpdate(relayOutput);
        }

        private void AddCANTalon(CANTalon canTalon, bool addToModel)
        {
            if (canTalon == null)
                throw new ArgumentNullException("canTalon");
            if (addToModel)
            {
                TopParent.SetAsync($"{FullyQualifiedName}.{_canTalonProviders.Name}.{canTalon.Name}", JsonSerialize(canTalon));
                return;
                //ModelReference.CANTalons.Add(canTalon);
            }
            //var provider = new CANTalonViewModel(this);
            //provider.SetCANTalon(canTalon);
            //provider.Name = _canTalonProviders.GetUnusedKey(provider.Name);
            //_canTalonProviders.Items.Add(provider);
            _canTalonProviders.AddOrUpdate(canTalon);
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
            TopParent.SetAsync($"{FullyQualifiedName}.{_pwmOutputProviders.Name}.name", null).Wait();
            //_pwmOutputProviders.Remove(name);
        }

        public void RemoveDigitalInput(string name)
        {
            TopParent.SetAsync($"{FullyQualifiedName}.{_digitalInputProviders.Name}.name", null).Wait();
            //_digitalInputProviders.Remove(name);
        }

        public void RemoveAnalogInput(string name)
        {
            TopParent.SetAsync($"{FullyQualifiedName}.{_analogInputProviders.Name}.name", null).Wait();
            //_analogInputProviders.Remove(name);
        }

        public void RemoveQuadEncoder(string name)
        {
            TopParent.SetAsync($"{FullyQualifiedName}.{_quadEncoderProviders.Name}.name", null).Wait();
            //_quadEncoderProviders.Remove(name);
        }

        public void RemoveRelayOutput(string name)
        {
            TopParent.SetAsync($"{FullyQualifiedName}.{_relayOutputProviders.Name}.name", null).Wait();
            //_canTalonProviders.Remove(name);
        }

        public void RemoveCANTalon(string name)
        {
            TopParent.SetAsync($"{FullyQualifiedName}.{_canTalonProviders.Name}.name", null).Wait();
            //_canTalonProviders.Remove(name);
        }
        #endregion

        #region CompoundViewModelBase
        public override IObservableCollection Children
        {
            get
            {
                return _children;
            }
        }
        #endregion

        #region IProvider
        public override string Name
        {
            get { return ModelReference.Name; }
            set { ModelReference.Name = value; }
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
        protected override void OnModelChange()
        {
            //setup the new providers
            PIDController.ModelReference = ModelReference.PIDController;

            if (null != ModelReference.PWMOutput)
            {
                _pwmOutputProviders.ModelReference = ModelReference.PWMOutput;
            }
            if (null != ModelReference.AnalogInput)
            {
                _analogInputProviders.ModelReference = ModelReference.AnalogInput;
            }
            if(null != ModelReference.DigitalInput)
            {
                _digitalInputProviders.ModelReference = ModelReference.DigitalInput;
            }
            if (null != ModelReference.QuadEncoder)
            {
                _quadEncoderProviders.ModelReference = ModelReference.QuadEncoder;
            }
            if (null != ModelReference.RelayOutput)
            {
                _relayOutputProviders.ModelReference = ModelReference.RelayOutput;
            }
            if (null != ModelReference.CANTalons)
            {
                _canTalonProviders.ModelReference = ModelReference.CANTalons;
            }
        }
        #endregion

        #region Private Fields
        ObservableCollection<IProvider> _children = new ObservableCollection<IProvider>() { null, null, null, null, null, null };
        CompoundProviderList<IPWMOutputProvider, PWMOutput> _pwmOutputProviders
        {
            get
            {
                return _children[0] as CompoundProviderList<IPWMOutputProvider, PWMOutput>;
            }

            set
            {
                _children[0] = value;
            }
        }
        CompoundProviderList<IAnalogInputProvider, AnalogInput> _analogInputProviders
        {
            get
            {
                return _children[1] as CompoundProviderList<IAnalogInputProvider, AnalogInput>;
            }

            set
            {
                _children[1] = value;
            }
        }
        CompoundProviderList<IQuadEncoderProvider, QuadEncoder> _quadEncoderProviders
        {
            get
            {
                return _children[2] as CompoundProviderList<IQuadEncoderProvider, QuadEncoder>;
            }

            set
            {
                _children[2] = value;
            }
        }
        CompoundProviderList<IDigitalInputProvider, DigitalInput> _digitalInputProviders
        {
            get
            {
                return _children[3] as CompoundProviderList<IDigitalInputProvider, DigitalInput>;
            }

            set
            {
                _children[3] = value;
            }
        }
        CompoundProviderList<IRelayOutputProvider, RelayOutput> _relayOutputProviders
        {
            get
            {
                return _children[4] as CompoundProviderList<IRelayOutputProvider, RelayOutput>;
            }

            set
            {
                _children[4] = value;
            }
        }
        CompoundProviderList<ICANTalonProvider, CANTalon> _canTalonProviders
        {
            get
            {
                return _children[5] as CompoundProviderList<ICANTalonProvider, CANTalon>;
            }

            set
            {
                _children[5] = value;
            }
        }
        #endregion
    }
}
