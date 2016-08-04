using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class OnChangeEventHandlerViewModel : IOnChangeEventHandlerProvider
    {
        OnChangeEventHandler _onChangeEventHandlerModel;

        public IEventTargetProvider EventTarget
        {
            get
            {
                return _eventTargetProvider;
            }
        }

        public double MinDelta
        {
            get
            {
                return _onChangeEventHandlerModel.MinDelta;
            }

            set
            {
                _onChangeEventHandlerModel.MinDelta = value;
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
                _onChangeEventHandlerModel.Name = value;
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
                _onChangeEventHandlerModel.WatchPath = value;
            }
        }

        public void SetOnChangeEventHandler(OnChangeEventHandler onChangeEventHandler)
        {
            _onChangeEventHandlerModel = onChangeEventHandler;
            _eventTargetProvider.SetEventTarget(_onChangeEventHandlerModel.EventTarget);
        }

        #region Private Fields
        IEventTargetProvider _eventTargetProvider = new EventTargetViewModel();
        #endregion
    }
}