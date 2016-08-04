using Team1922.MVVM.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using System.Windows.Input;
using System.ComponentModel;
using Team1922.MVVM.Contracts;

namespace Team1922.MVVM.ViewModels
{
    public abstract class RobotViewModelBase : ViewModelBase, IRobotProvider
    {
        protected Robot _robotModel;

        public RobotViewModelBase()
        {
            //GetBooksCommand = new DelegateCommand(OnGetBooks, CanGetBooks);
            AddSubsystemCommand = new DelegateCommand(OnAddSubsystem);
            AddContinuousCommandCommand = new DelegateCommand(OnAddContinuousCommand);
            AddOnChangeEventHandlerCommand = new DelegateCommand(OnAddOnChangeEventHandler);
            AddOnWithinRangeEventHandlerCommand = new DelegateCommand(OnAddOnWithinRangeEventHandler);
            AddJoystickCommand = new DelegateCommand(OnAddJoystick);
        }

        public void SetRobot(Robot robot)
        {
            //clear the old providers
            _subsystemProviders.Clear();
            _continuousCommandProviders.Clear();
            _onChangeEventHandlerProviders.Clear();
            _onWithinRangeEventHandlerProviders.Clear();
            _joystickProviders.Clear();

            //setup the new providers
            _robotModel = robot;
            foreach(var subsystem in _robotModel.Subsystem)
            {
                if (subsystem == null)
                    continue;
                var provider = new SubsystemViewModel();
                provider.SetSubsystem(subsystem);
                _subsystemProviders.Add(provider);
            }
            foreach (var continuousCommand in _robotModel.ContinuousCommand)
            {
                if (continuousCommand == null)
                    continue;
                var provider = new ContinuousCommandViewModel();
                provider.SetContinuousCommand(continuousCommand);
                _continuousCommandProviders.Add(provider);
            }
            foreach (var onChangeEventHandler in _robotModel.OnChangeEventHandler)
            {
                if (onChangeEventHandler == null)
                    continue;
                var provider = new OnChangeEventHandlerViewModel();
                provider.SetOnChangeEventHandler(onChangeEventHandler);
                _onChangeEventHandlerProviders.Add(provider);
            }
            foreach (var onWithinRangeEventHandler in _robotModel.OnWithinRangeEventHandler)
            {
                if (onWithinRangeEventHandler == null)
                    continue;
                var provider = new OnWithinRangeEventHandlerViewModel();
                provider.SetOnWithinRangeEventHandler(onWithinRangeEventHandler);
                _onWithinRangeEventHandlerProviders.Add(provider);
            }
            foreach (var joystick in _robotModel.Joystick)
            {
                if (joystick == null)
                    continue;
                var provider = new JoystickViewModel();
                provider.SetJoystick(joystick);
                _joystickProviders.Add(provider);
            }
        }

        private INotifyPropertyChanged _selectedElement;
        public INotifyPropertyChanged SelectedElement
        {
            get { return _selectedElement; }
            set
            {
                if (SetProperty(ref _selectedElement, value))
                {
                    EventAggregator<ItemSelectEvent>.Instance.Publish(this, new ItemSelectEvent { SelectedElement = _selectedElement });
                }
            }
        }

        public IEnumerable<ISubsystemProvider> Subsystems
        {
            get { return _subsystemProviders; }
        }
        public IEnumerable<IContinuousCommandProvider> ContinuousCommands
        {
            get { return _continuousCommandProviders; }
        }
        public IEnumerable<IOnChangeEventHandlerProvider> OnChangeEventHandlers
        {
            get { return _onChangeEventHandlerProviders; }
        }
        public IEnumerable<IOnWithinRangeEventHandlerProvider> OnWithinRangeEventHandlers
        {
            get { return _onWithinRangeEventHandlerProviders; }
        }
        public IEnumerable<IJoystickProvider> Joysticks
        {
            get { return _joystickProviders; }
        }

        private void OnAddSubsystem()
        {
            EventAggregator<AddSubsystemEvent>.Instance.Publish(this, new AddSubsystemEvent());
        }
        private void OnAddContinuousCommand()
        {
            EventAggregator<AddContinuousCommandEvent>.Instance.Publish(this, new AddContinuousCommandEvent());
        }
        private void OnAddOnChangeEventHandler()
        {
            EventAggregator<AddOnChangeEventHandler>.Instance.Publish(this, new AddOnChangeEventHandler());
        }
        private void OnAddOnWithinRangeEventHandler()
        {
            EventAggregator<AddOnWithinRangeEventHandler>.Instance.Publish(this, new AddOnWithinRangeEventHandler());
        }
        private void OnAddJoystick()
        {
            EventAggregator<AddJoystickEvent>.Instance.Publish(this, new AddJoystickEvent());
        }
        private void OnAddAnalogInput()
        {
            if (_selectedElement is Subsystem)
            {
                EventAggregator<AddAnalogInputEvent>.Instance.Publish(this, new AddAnalogInputEvent(_selectedElement as Subsystem));
            }
        }
        private void OnAddCANTalon()
        {
            if (_selectedElement is Subsystem)
            {
                EventAggregator<AddCANTalonEvent>.Instance.Publish(this, new AddCANTalonEvent(_selectedElement as Subsystem));
            }
        }
        private void OnAddDigitalInput()
        {
            if (_selectedElement is Subsystem)
            {
                EventAggregator<AddDigitalInputEvent>.Instance.Publish(this, new AddDigitalInputEvent(_selectedElement as Subsystem));
            }
        }
        private void OnAddPWMMotorController()
        {
            if (_selectedElement is Subsystem)
            {
                EventAggregator<AddPWMMotorControllerEvent>.Instance.Publish(this, new AddPWMMotorControllerEvent(_selectedElement as Subsystem));
            }
        }
        private void OnAddQuadEncoder()
        {
            if (_selectedElement is Subsystem)
            {
                EventAggregator<AddQuadEncoderEvent>.Instance.Publish(this, new AddQuadEncoderEvent(_selectedElement as Subsystem));
            }
        }

        public void UpdateInputValues()
        {
            foreach(var subsystem in _subsystemProviders)
            {
                subsystem.UpdateInputValues();
            }
        }

        public ICommand AddSubsystemCommand { get; }
        public ICommand AddContinuousCommandCommand { get; }
        public ICommand AddOnChangeEventHandlerCommand { get; }
        public ICommand AddOnWithinRangeEventHandlerCommand { get; }
        public ICommand AddJoystickCommand { get; }

        #region Abstract Methods
        public abstract int TeamNumber { get; set; }
        public abstract int AnalogInputSampleRate { get; set; }
        #endregion

        #region Private Fields
        List<ISubsystemProvider> _subsystemProviders = new List<ISubsystemProvider>();
        List<IContinuousCommandProvider> _continuousCommandProviders = new List<IContinuousCommandProvider>();
        List<IOnChangeEventHandlerProvider> _onChangeEventHandlerProviders = new List<IOnChangeEventHandlerProvider>();
        List<IOnWithinRangeEventHandlerProvider> _onWithinRangeEventHandlerProviders = new List<IOnWithinRangeEventHandlerProvider>();
        List<IJoystickProvider> _joystickProviders = new List<IJoystickProvider>();
        #endregion
    }
}
