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
    public class RobotViewModelBase : ViewModelBase<Robot>, IRobotProvider
    {
        public RobotViewModelBase() : base(null)
        {
            _subsystemProviders = new CompoundProviderList<ISubsystemProvider, Subsystem>("Subsystems", this);
            _eventHandlerProviders = new CompoundProviderList<IEventHandlerProvider, Models.EventHandler>("EventHandlers", this);
            _joystickProviders = new CompoundProviderList<IJoystickProvider, Joystick>("Joysticks", this);
        }

        #region IRobotProvider
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
        public int TeamNumber
        {
            get
            {
                return ModelReference.TeamNumber;
            }

            set
            {
                ModelReference.TeamNumber = value;
            }
        }
        public int AnalogInputSampleRate
        {
            get
            {
                return ModelReference.AnalogInputSampleRate;
            }

            set
            {
                ModelReference.AnalogInputSampleRate = value;
            }
        }
        public void SetRobot(Robot robot)
        {
            //clear the old providers
            _subsystemProviders.Items.Clear();
            _eventHandlerProviders.Items.Clear();
            _joystickProviders.Items.Clear();

            //setup the new providers
            ModelReference = robot;
            if (null != ModelReference.Subsystem)
            {
                foreach (var subsystem in ModelReference.Subsystem)
                {
                    AddSubsystem(subsystem, false);
                }
            }
            if (null != ModelReference.EventHandler)
            {
                foreach (var eventHandler in ModelReference.EventHandler)
                {
                    AddEventHandler(eventHandler, false);
                }
            }
            if (null != ModelReference.Joystick)
            {
                foreach (var joystick in ModelReference.Joystick)
                {
                    AddJoystick(joystick, false);
                }
            }
            if (null != ModelReference.RobotMap)
            {
                _robotMapProvider = new RobotMapViewModel(this);
                _robotMapProvider.SetRobot(ModelReference);
            }
        }
        public void RemoveSubsystem(string name)
        {
            for (int i = 0; i < _subsystemProviders.Items.Count; ++i)
            {
                if (_subsystemProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _subsystemProviders.Items.RemoveAt(i);

                    //remove the model instance
                    ModelReference.Subsystem.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveJoystick(string name)
        {
            for (int i = 0; i < _joystickProviders.Items.Count; ++i)
            {
                if (_joystickProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _joystickProviders.Items.RemoveAt(i);

                    //remove the model instance
                    ModelReference.Joystick.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveEventHandler(string name)
        {
            for (int i = 0; i < _eventHandlerProviders.Items.Count; ++i)
            {
                if (_eventHandlerProviders.Items[i].Name == name)
                {
                    //remove the provider
                    _eventHandlerProviders.Items.RemoveAt(i);

                    //remove the model instance
                    ModelReference.EventHandler.RemoveAt(i);
                    break;
                }
            }
        }
        #endregion
        
        #region IInputProvider
        public void UpdateInputValues()
        {
            foreach(var subsystem in _subsystemProviders.Items)
            {
                subsystem.UpdateInputValues();
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
            get
            {
                return "Robot";
            }
        }
        #endregion

        #region ViewModelBase
        protected override List<string> GetOverrideKeys()
        {
            return _keys;
        }
        protected override string GetValue(string key)
        {
            switch(key)
            {
                case "AnalogInputSampleRate":
                    return AnalogInputSampleRate.ToString();
                case "TeamNumber":
                    return TeamNumber.ToString();

                case "Joysticks":
                    return _joystickProviders.GetModelJson();
                case "Name":
                    return Name;
                case "EventHandlers":
                    return _eventHandlerProviders.GetModelJson();
                case "Subsystems":
                    return _subsystemProviders.GetModelJson();
                case "RobotMap":
                    return _robotMapProvider.GetModelJson();
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

                case "Joysticks":
                    _joystickProviders.SetModelJson(value);
                    break;
                case "EventHandlers":
                    _eventHandlerProviders.SetModelJson(value);
                    break;
                case "Subsystems":
                    _subsystemProviders.SetModelJson(value);
                    break;
                case "RobotMap":
                    _robotMapProvider.SetModelJson(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }
            
        }
        #endregion

        #region Private Methods
        private void AddSubsystem(Subsystem subsystem, bool addToModel)
        {
            if (subsystem == null)
                throw new ArgumentNullException("subsystem");
            if (addToModel)
                ModelReference.Subsystem.Add(subsystem);

            var provider = new SubsystemViewModel(this);
            provider.SetSubsystem(subsystem);
            provider.Name = _subsystemProviders.GetUnusedKey(provider.Name);
            _subsystemProviders.Items.Add(provider);
        }
        private void AddJoystick(Joystick joystick, bool addToModel)
        {
            if (joystick == null)
                throw new ArgumentNullException("joystick");
            if (addToModel)
                ModelReference.Joystick.Add(joystick);

            var provider = new JoystickViewModel(this);
            provider.SetJoystick(joystick);
            provider.Name = _joystickProviders.GetUnusedKey(provider.Name);
            _joystickProviders.Items.Add(provider);
        }
        private void AddEventHandler(Models.EventHandler eventHandler, bool addToModel)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("subsystem");
            if (addToModel)
                ModelReference.EventHandler.Add(eventHandler);

            var provider = new EventHandlerViewModel(this);
            provider.SetEventHandler(eventHandler);
            provider.Name = _eventHandlerProviders.GetUnusedKey(provider.Name);
            _eventHandlerProviders.Items.Add(provider);
        }
        #endregion

        #region Private Fields
        Dictionary<string, IProvider> _children = new Dictionary<string, IProvider>();
        readonly List<string> _keys = new List<string>(){ "AnalogInputSampleRate", "EventHandlers", "Joysticks", "RobotMap", "Name", "OnChangeEventHandlers","OnWithinRangeEventHandlers","Subsystems","TeamNumber" };
        IRobotMapProvider _robotMapProvider
        {
            get
            {
                return _children["_robotMapProvider"] as IRobotMapProvider;
            }
            
            set
            {
                _children["_robotMapProvider"] = value;
            }
        }
        CompoundProviderList<ISubsystemProvider, Subsystem> _subsystemProviders
        {
            get
            {
                return _children["_subsystemProviders"] as CompoundProviderList<ISubsystemProvider, Subsystem>;
            }

            set
            {
                _children["_subsystemProviders"] = value;
            }
        }
        CompoundProviderList<IEventHandlerProvider, Models.EventHandler> _eventHandlerProviders
        {
            get
            {
                return _children["_eventHandlerProviders"] as CompoundProviderList<IEventHandlerProvider, Models.EventHandler>;
            }

            set
            {
                _children["_eventHandlerProviders"] = value;
            }
        }
        CompoundProviderList<IJoystickProvider, Joystick> _joystickProviders
        {
            get
            {
                return _children["_joystickProviders"] as CompoundProviderList<IJoystickProvider, Joystick>;
            }

            set
            {
                _children["_joystickProviders"] = value;
            }
        }
        #endregion
    }
}
