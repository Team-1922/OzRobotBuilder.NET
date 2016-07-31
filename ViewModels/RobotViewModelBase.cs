using Team1922.MVVM.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using System.Windows.Input;
using System.ComponentModel;

namespace Team1922.MVVM.ViewModels
{
    public abstract class RobotViewModelBase : ViewModelBase
    {
        protected Robot _robot;

        protected List<string> ClampAttributes {get;} 

        public RobotViewModelBase(Robot robot)
        {
            _robot = robot;
            //GetBooksCommand = new DelegateCommand(OnGetBooks, CanGetBooks);
            AddSubsystemCommand = new DelegateCommand(OnAddSubsystem);
            AddContinuousCommandCommand = new DelegateCommand(OnAddContinuousCommand);
            AddOnChangeEventHandlerCommand = new DelegateCommand(OnAddOnChangeEventHandler);
            AddOnWithinRangeEventHandlerCommand = new DelegateCommand(OnAddOnWithinRangeEventHandler);
            AddJoystickCommand = new DelegateCommand(OnAddJoystick);
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

        public IEnumerable<Subsystem> Subsystems => _robot.Subsystem;
        public IEnumerable<ContinuousCommand> Commands => _robot.ContinuousCommand;
        public IEnumerable<OnChangeEventHandler> OnChangeEventHandlers => _robot.OnChangeEventHandler;
        public IEnumerable<OnWithinRangeEventHandler> OnWithinRangeEventHandlers => _robot.OnWithinRangeEventHandler;
        public IEnumerable<Joystick> Joysticks => _robot.Joystick;

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
            if(_selectedElement is Subsystem)
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

        public ICommand AddSubsystemCommand { get; }
        public ICommand AddContinuousCommandCommand { get; }
        public ICommand AddOnChangeEventHandlerCommand { get; }
        public ICommand AddOnWithinRangeEventHandlerCommand { get; }
        public ICommand AddJoystickCommand { get; }
    }
}
