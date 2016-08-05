using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class OnWithinRangeEventHandlerViewModel : IOnWithinRangeEventHandlerProvider
    {
        OnWithinRangeEventHandler _onWithinRangeEventHandlerModel;

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
                _onWithinRangeEventHandlerModel.Invert = value;
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
                _onWithinRangeEventHandlerModel.Max = value;
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
                _onWithinRangeEventHandlerModel.Min = value;
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
                _onWithinRangeEventHandlerModel.Name = value;
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
                _onWithinRangeEventHandlerModel.WatchPath = value;
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