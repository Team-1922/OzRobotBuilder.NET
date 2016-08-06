using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class OnWithinRangeEventHandlerViewModel : ViewModelBase, IOnWithinRangeEventHandlerProvider
    {
        OnWithinRangeEventHandler _onWithinRangeEventHandlerModel;

        public OnWithinRangeEventHandlerViewModel()
        {
            _eventTargetProvider.PropertyChanged += _eventTargetProvider_PropertyChanged;
        }

        private void _eventTargetProvider_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged($"EventTarget.{e.PropertyName}");
        }

        public IEventTargetProvider EventTarget
        {
            get
            {
                return _eventTargetProvider;
            }
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

        public void SetOnWithinRangeEventHandler(OnWithinRangeEventHandler onWithinRangeEventHandler)
        {
            _onWithinRangeEventHandlerModel = onWithinRangeEventHandler;
            _eventTargetProvider.SetEventTarget(_onWithinRangeEventHandlerModel.EventTarget);
        }

        #region Private Fields
        IEventTargetProvider _eventTargetProvider = new EventTargetViewModel();
        #endregion
    }
}