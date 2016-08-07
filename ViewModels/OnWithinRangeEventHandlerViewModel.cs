using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class OnWithinRangeEventHandlerViewModel : EventViewModelBase, IOnWithinRangeEventHandlerProvider
    {
        OnWithinRangeEventHandler _onWithinRangeEventHandlerModel;

        public OnWithinRangeEventHandlerViewModel()
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

        public bool Invert
        {
            get
            {
                return _onWithinRangeEventHandlerModel.Invert;
            }

            set
            {
                var temp = _onWithinRangeEventHandlerModel.Invert;
                SetProperty(ref temp, value);
                _onWithinRangeEventHandlerModel.Invert = temp;
            }
        }

        public double Max
        {
            get
            {
                return _onWithinRangeEventHandlerModel.Max;
            }

            set
            {
                var temp = _onWithinRangeEventHandlerModel.Max;
                SetProperty(ref temp, value);
                _onWithinRangeEventHandlerModel.Max = temp;
            }
        }

        public double Min
        {
            get
            {
                return _onWithinRangeEventHandlerModel.Min;
            }

            set
            {
                var temp = _onWithinRangeEventHandlerModel.Min;
                SetProperty(ref temp, value);
                _onWithinRangeEventHandlerModel.Min = temp;
            }
        }

        public string Name
        {
            get
            {
                return _onWithinRangeEventHandlerModel.Name;
            }

            set
            {
                var temp = _onWithinRangeEventHandlerModel.Name;
                SetProperty(ref temp, value);
                _onWithinRangeEventHandlerModel.Name = temp;
            }
        }

        public string WatchPath
        {
            get
            {
                return _onWithinRangeEventHandlerModel.WatchPath;
            }

            set
            {
                var temp = _onWithinRangeEventHandlerModel.WatchPath;
                SetProperty(ref temp, value);
                _onWithinRangeEventHandlerModel.WatchPath = temp;
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
                    case "Min":
                        return Min.ToString();
                    case "Max":
                        return Max.ToString();
                    case "Invert":
                        return Invert.ToString();
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
                    case "Min":
                        Min = SafeCastDouble(value);
                        break;
                    case "Max":
                        Max = SafeCastDouble(value);
                        break;
                    case "Invert":
                        Invert = SafeCastBool(value);
                        break;
                    case "WatchPath":
                        WatchPath = value;
                        break;
                    default:
                        throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
                }
            }
        }

        public void SetOnWithinRangeEventHandler(OnWithinRangeEventHandler onWithinRangeEventHandler)
        {
            _onWithinRangeEventHandlerModel = onWithinRangeEventHandler;
            EventTarget.SetEventTarget(_onWithinRangeEventHandlerModel.EventTarget);
        }
        
    }
}