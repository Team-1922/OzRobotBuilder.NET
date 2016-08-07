using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class OnChangeEventHandlerViewModel : EventViewModelBase, IOnChangeEventHandlerProvider
    {
        OnChangeEventHandler _onChangeEventHandlerModel;

        public OnChangeEventHandlerViewModel()
        {
            EventTarget.PropertyChanged += _eventTargetProvider_PropertyChanged;
        }

        private void _eventTargetProvider_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Type")
                UpdateKeyValueList();
            OnPropertyChanged($"EventTarget.{e.PropertyName}");
        }

        protected override List<string> GetOverrideKeys()
        {
            var ret = base.GetOverrideKeys();
            ret.AddRange(GetTargetKeys());
            return ret;
        }

        public double MinDelta
        {
            get
            {
                return _onChangeEventHandlerModel.MinDelta;
            }

            set
            {
                var temp = _onChangeEventHandlerModel.MinDelta;
                SetProperty(ref temp, value);
                _onChangeEventHandlerModel.MinDelta = temp;
            }
        }

        public string Name
        {
            get
            {
                return _onChangeEventHandlerModel.Name;
            }

            set
            {
                var temp = _onChangeEventHandlerModel.Name;
                SetProperty(ref temp, value);
                _onChangeEventHandlerModel.Name = temp;
            }
        }

        public string WatchPath
        {
            get
            {
                return _onChangeEventHandlerModel.WatchPath;
            }

            set
            {
                var temp = _onChangeEventHandlerModel.WatchPath;
                SetProperty(ref temp, value);
                _onChangeEventHandlerModel.WatchPath = temp;
            }
        }

        public override string this[string key]
        {
            get
            {
                string ret;
                if (TryGetTargetValue(key, out ret))
                    return ret;

                switch (key)
                {
                    case "Name":
                        return Name;
                    case "MinDelta":
                        return MinDelta.ToString();
                    case "WatchPath":
                        return WatchPath;
                    default:
                        throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
                }
            }

            set
            {
                if (TrySetTargetValue(key, value))
                    return;

                switch (key)
                {
                    case "Name":
                        Name = value;
                        break;
                    case "MinDelta":
                        MinDelta = SafeCastDouble(value);
                        break;
                    case "WatchPath":
                        WatchPath = value;
                        break;
                    default:
                        throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
                }
            }
        }

        public void SetOnChangeEventHandler(OnChangeEventHandler onChangeEventHandler)
        {
            _onChangeEventHandlerModel = onChangeEventHandler;
            EventTarget.SetEventTarget(_onChangeEventHandlerModel.EventTarget);
        }
    }
}