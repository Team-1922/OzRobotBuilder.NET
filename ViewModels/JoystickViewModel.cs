using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{

    internal class JoystickViewModel : ViewModelBase, IJoystickProvider
    {
        Joystick _joystickModel;

        public JoystickViewModel()
        {
            _axes.CollectionChanged += _axes_CollectionChanged;
            _buttons.CollectionChanged += _buttons_CollectionChanged;
        }

        private void _buttons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e != null && e.Action == NotifyCollectionChangedAction.Replace)
            {
                OnPropertyChanged($"Buttons[{e.NewStartingIndex}]");
            }
        }

        private void _axes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e != null && e.Action == NotifyCollectionChangedAction.Replace)
            {
                OnPropertyChanged($"Axes[{e.NewStartingIndex}]");
            }
        }

        protected override List<string> GetOverrideKeys()
        {
            var ret = new List<string>(){ "Name", "ID" };
            for(int i = 1; i <= 12; ++i)
            {
                ret.Add($"Buttons[{i}]");
            }
            for (int i = 1; i <= 6; ++i)
            {
                ret.Add($"Axes[{i}]");
            }
            return ret;
        }

        public IReadOnlyDictionary<uint, double> Axes
        {
            get
            {
                return _axes;
            }
        }

        public IReadOnlyDictionary<uint, bool> Buttons
        {
            get
            {
                return _buttons;
            }
        }

        public int ID
        {
            get
            {
                return _joystickModel.ID;
            }

            set
            {
                _joystickModel.ID = value;
            }
        }

        public string Name
        {
            get
            {
                return _joystickModel.Name;
            }

            set
            {
                _joystickModel.Name = value;
            }
        }

        private Regex axisTester = new Regex("^Axes\\[([1-9]|1[0-2])\\]$");
        private Regex buttonTester = new Regex("^Buttons\\[([1-9]|1[0-2])\\]$");
        private bool GetAxis(string key, out string output)
        {
            //special situation where the axes/buttons can be referenced by index
            if (axisTester.IsMatch(key))
            {
                //get the second to last character
                uint index;
                bool success = uint.TryParse(key.Substring(key.Length - 2, 1), out index);
                if (success)
                {
                    output = _axes[index].ToString();
                }
            }
            output = "";
            return false;
        }
        private bool GetButton(string key, out string output)
        {
            //special situation where the axes/buttons can be referenced by index
            if (buttonTester.IsMatch(key))
            {
                //get the second to last character
                uint index;
                bool success = uint.TryParse(key.Substring(key.Length - 2, 1), out index);
                if (success)
                {
                    output = _buttons[index].ToString();
                }
            }
            output = "";
            return false;
        }

        protected override string GetValue(string key)
        {
            string ret;
            if (GetAxis(key, out ret))
                return ret;
            if (GetButton(key, out ret))
                return ret;

            switch (key)
            {
                case "Name":
                    return Name;
                case "ID":
                    return ID.ToString();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }

        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "Name":
                    Name = value;
                    break;
                case "ID":
                    ID = SafeCastInt(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }

        }

        public override string ModelTypeName
        {
            get
            {
                var brokenName = _joystickModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        public void SetJoystick(Joystick joystick)
        {
            _joystickModel = joystick;
        }

        public void UpdateInputValues()
        {
            var thisJoystickIOService = IOService.Instance.Joysticks[ID];
            for(uint i = 1; i <= 12; ++i)
            {
                _buttons[i] = thisJoystickIOService.Buttons[i];
            }
            for(uint i = 1; i <= 5; ++i)
            {
                _axes[i] = thisJoystickIOService.Axes[i];
            }
        }

        #region Private Fields
        ReadonlyObservableDictionary<uint, double> _axes = new ReadonlyObservableDictionary<uint, double>();
        ReadonlyObservableDictionary<uint, bool> _buttons = new ReadonlyObservableDictionary<uint, bool>();
        #endregion
    }
}