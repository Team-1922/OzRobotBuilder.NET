using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using System.Windows.Input;
using System.ComponentModel;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Contracts.Events;

namespace Team1922.MVVM.ViewModels
{
    public abstract class RobotViewModelBase : ViewModelBase, IRobotProvider
    {
        protected Robot _robotModel;

        public RobotViewModelBase()
        {
            _subsystemProviders = new CompoundProviderList<ISubsystemProvider>("Subsystems");
            _eventHandlerProviders = new CompoundProviderList<IEventHandlerProvider>("EventHandlers");
            _joystickProviders = new CompoundProviderList<IJoystickProvider>("Joysticks");
        }

        public void SetRobot(Robot robot)
        {
            //clear the old providers
            _subsystemProviders.Items.Clear();
            _eventHandlerProviders.Items.Clear();
            _joystickProviders.Items.Clear();

            //setup the new providers
            _robotModel = robot;
            if (null != _robotModel.Subsystem)
            {
                foreach (var subsystem in _robotModel.Subsystem)
                {
                    AddSubsystem(subsystem, false);
                }
            }
            if (null != _robotModel.EventHandler)
            {
                foreach (var eventHandler in _robotModel.EventHandler)
                {
                    AddEventHandler(eventHandler, false);
                }
            }
            if (null != _robotModel.Joystick)
            {
                foreach (var joystick in _robotModel.Joystick)
                {
                    AddJoystick(joystick, false);
                }
            }
        }

        protected override List<string> GetOverrideKeys()
        {
            return _keys;
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

        public IObservableCollection<ISubsystemProvider> Subsystems
        {
            get { return _subsystemProviders.Items; }
        }
        public IObservableCollection<IEventHandlerProvider> EventHandlers
        {
            get { return _eventHandlerProviders.Items; }
        }
        public IObservableCollection<IJoystickProvider> Joysticks
        {
            get { return _joystickProviders.Items; }
        }

        public void UpdateInputValues()
        {
            foreach(var subsystem in _subsystemProviders.Items)
            {
                subsystem.UpdateInputValues();
            }
        }

        public int TeamNumber
        {
            get
            {
                return _robotModel.TeamNumber;
            }

            set
            {
                _robotModel.TeamNumber = value;
            }
        }

        public int AnalogInputSampleRate
        {
            get
            {
                return _robotModel.AnalogInputSampleRate;
            }

            set
            {
                _robotModel.AnalogInputSampleRate = value;
            }
        }

        public IEnumerable<IProvider> Children
        {
            get
            {
                return _children.Values;
            }
        }

        public string Name
        {
            get
            {
                return "Robot";
            }
        }

        protected override string GetValue(string key)
        {
            switch(key)
            {
                case "AnalogInputSampleRate":
                    return AnalogInputSampleRate.ToString();
                case "Joysticks":
                    return Joysticks.ToString();
                case "Name":
                    return Name;
                case "EventHandlers":
                    return EventHandlers.ToString();
                case "Subsystems":
                    return Subsystems.ToString();
                case "TeamNumber":
                    return TeamNumber.ToString();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }
        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "AnalogInputSampleRate":
                    AnalogInputSampleRate = SafeCastInt(value);
                    break;
                case "TeamNumber":
                    TeamNumber = SafeCastInt(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }
            
        }
        public override string ModelTypeName
        {
            get
            {
                var brokenName = _robotModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        private void AddSubsystem(Subsystem subsystem, bool addToModel)
        {
            if (subsystem == null)
                throw new ArgumentNullException("subsystem");
            if (addToModel)
                _robotModel.Subsystem.Add(subsystem);

            var provider = new SubsystemViewModel();
            provider.SetSubsystem(subsystem);
            _subsystemProviders.Items.Add(provider);
        }
        private void AddJoystick(Joystick joystick, bool addToModel)
        {
            if (joystick == null)
                throw new ArgumentNullException("joystick");
            if (addToModel)
                _robotModel.Joystick.Add(joystick);

            var provider = new JoystickViewModel();
            provider.SetJoystick(joystick);
            _joystickProviders.Items.Add(provider);
        }
        private void AddEventHandler(Models.EventHandler eventHandler, bool addToModel)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("subsystem");
            if (addToModel)
                _robotModel.EventHandler.Add(eventHandler);

            var provider = new EventHandlerViewModel();
            provider.SetEventHandler(eventHandler);
            _eventHandlerProviders.Items.Add(provider);
        }
        public void AddSubsystem(Subsystem subsystem)
        {
            AddSubsystem(subsystem, true);
        }
        public void AddJoystick(Joystick joystick)
        {
            AddJoystick(joystick, true);
        }
        public void AddEventHandler(Models.EventHandler eventHandler)
        {
            AddEventHandler(eventHandler, true);
        }

        #region Private Fields
        Dictionary<string, IProvider> _children = new Dictionary<string, IProvider>();
        readonly List<string> _keys = new List<string>(){ "AnalogInputSampleRate", "ContinuousCommands", "Joysticks", "Name", "OnChangeEventHandlers","OnWithinRangeEventHandlers","Subsystems","TeamNumber" };
        CompoundProviderList<ISubsystemProvider> _subsystemProviders
        {
            get
            {
                return _children["_subsystemProviders"] as CompoundProviderList<ISubsystemProvider>;
            }

            set
            {
                _children["_subsystemProviders"] = value;
            }
        }
        CompoundProviderList<IEventHandlerProvider> _eventHandlerProviders
        {
            get
            {
                return _children["_eventHandlerProviders"] as CompoundProviderList<IEventHandlerProvider>;
            }

            set
            {
                _children["_eventHandlerProviders"] = value;
            }
        }
        CompoundProviderList<IJoystickProvider> _joystickProviders
        {
            get
            {
                return _children["_joystickProviders"] as CompoundProviderList<IJoystickProvider>;
            }

            set
            {
                _children["_joystickProviders"] = value;
            }
        }
        #endregion
    }
}
