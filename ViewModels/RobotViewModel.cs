using Team1922.MVVM.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using System.Windows.Input;

namespace Team1922.MVVM.ViewModels
{
    public class RobotViewModel : ViewModelBase
    {
        private Robot _robot;

        public RobotViewModel(Robot robot)
        {
            _robot = robot;
            //GetBooksCommand = new DelegateCommand(OnGetBooks, CanGetBooks);
            AddSubsystemCommand = new DelegateCommand(OnAddSubsystem);
            AddCommandCommand = new DelegateCommand(OnAddCommand);
            AddJoystickCommand = new DelegateCommand(OnAddJoystick);
        }

        private BindableBase _selectedElement;
        public BindableBase SelectedElement
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

        public IEnumerable<Subsystem> Subsystems => _robot.Subsystems;
        public IEnumerable<Command> Commands => _robot.Commands;
        public IEnumerable<Joystick> Joysticks => _robot.Joysticks;

        private void OnAddSubsystem()
        {
            EventAggregator<AddSubsystemEvent>.Instance.Publish(this, new AddSubsystemEvent());
        }
        private void OnAddCommand()
        {
            EventAggregator<AddCommandEvent>.Instance.Publish(this, new AddCommandEvent());
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
        public ICommand AddCommandCommand { get; }
        public ICommand AddJoystickCommand { get; }
    }
}
