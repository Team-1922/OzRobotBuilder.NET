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
        
        public void SetSubsystem(Subsystem subsystem)
        {
            //cleanup the old providers
            _pwmOutputProviders.Clear();
            _analogInputProviders.Clear();
            _quadEncoderProviders.Clear();
            _relayOutputProviders.Clear();
            _canTalonProviders.Clear();

            //setup the new providers
            _subsystemModel = subsystem;
            PIDController.SetPIDController(_subsystemModel.PIDController);

            foreach (var pwmOutput in _subsystemModel.PWMOutput)
            {
                if (pwmOutput == null)
                    continue;
                var provider = new PWMOutputViewModel();
                provider.SetPWMOutput(pwmOutput);
                _pwmOutputProviders.Add(provider);
            }
            foreach (var analogInput in _subsystemModel.AnalogInput)
            {
                if (analogInput == null)
                    continue;
                var provider = new AnalogInputViewModel();
                provider.SetAnalogInput(analogInput);
                _analogInputProviders.Add(provider);
            }
            foreach (var quadEncoder in _subsystemModel.QuadEncoder)
            {
                if (quadEncoder == null)
                    continue;
                var provider = new QuadEncoderViewModel();
                provider.SetQuadEncoder(quadEncoder);
                _quadEncoderProviders.Add(provider);
            }
            foreach (var relayOutput in _subsystemModel.RelayOutput)
            {
                if (relayOutput == null)
                    continue;
                var provider = new RelayOutputViewModel();
                provider.SetRelayOutput(relayOutput);
                _relayOutputProviders.Add(provider);
            }
            foreach (var canTalon in _subsystemModel.CANTalons)
            {
                if (canTalon == null)
                    continue;
                var provider = new CANTalonViewModel();
                provider.SetCANTalon(canTalon);
                _canTalonProviders.Add(provider);
            }
        }

        public void UpdateInputValues()
        {
            foreach(var analogInput in _analogInputProviders)
            {
                analogInput.UpdateInputValues();
            }
            foreach (var quadEncoder in _quadEncoderProviders)
            {
                quadEncoder.UpdateInputValues();
            }
            foreach (var canTalon in _canTalonProviders)
            {
                canTalon.UpdateInputValues();
            }
        }

        public IEnumerable<IPWMOutputProvider> PWMOutputs
        {
            get { return _pwmOutputProviders; }
        }
        public IEnumerable<IAnalogInputProvider> AnalogInputs
        {
            get { return _analogInputProviders; }
        }
        public IEnumerable<IQuadEncoderProvider> QuadEncoders
        {
            get { return _quadEncoderProviders; }
        }
        public IEnumerable<IRelayOutputProvider> RelayOutputs
        {
            get { return _relayOutputProviders; }
        }
        public IEnumerable<ICANTalonProvider> CANTalons
        {
            get { return _canTalonProviders; }
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

        #region Private Fields
        List<IPWMOutputProvider> _pwmOutputProviders = new List<IPWMOutputProvider>();
        List<IAnalogInputProvider> _analogInputProviders = new List<IAnalogInputProvider>();
        List<IQuadEncoderProvider> _quadEncoderProviders = new List<IQuadEncoderProvider>();
        List<IRelayOutputProvider> _relayOutputProviders = new List<IRelayOutputProvider>();
        List<ICANTalonProvider> _canTalonProviders = new List<ICANTalonProvider>();
        #endregion
    }
}
