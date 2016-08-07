using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class ContinuousCommandViewModel : EventViewModelBase, IContinuousCommandProvider
    {
        ContinuousCommand _commandModel;

        public ContinuousCommandViewModel()
        {
            EventTarget.PropertyChanged += _eventTargetProvider_PropertyChanged;
        }

        private void _eventTargetProvider_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Type")
                UpdateKeys();
            OnPropertyChanged($"EventTarget.{e.PropertyName}");
        }

        protected override List<string> GetOverrideKeys()
        {
            var ret = new List<string>() { "Name" };
            ret.AddRange(GetTargetKeys());
            return ret;
        }

        public string Name
        {
            get
            {
                return _commandModel.Name;
            }

            set
            {
                var temp = _commandModel.Name;
                SetProperty(ref temp, value);
                _commandModel.Name = temp;
            }
        }

        public override string this[string key]
        {
            get
            {
                string ret;
                if (TryGetTargetValue(key, out ret))
                    return ret;

                switch(key)
                {
                    case "Name":
                        return Name;
                    default:
                        throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
                }
            }

            set
            {
                if (TrySetTargetValue(key, value))
                    return;

                switch(key)
                {
                    case "Name":
                        Name = value;
                        break;
                    default:
                        throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
                }
            }
        }

        public void SetContinuousCommand(ContinuousCommand continuousCommand)
        {
            _commandModel = continuousCommand;
            EventTarget.SetEventTarget(_commandModel.EventTarget);
        }
    }
}