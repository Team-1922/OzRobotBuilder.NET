using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class OnChangeEventHandlerViewModel : ViewModelBase, IOnChangeEventHandlerProvider
    {
        OnChangeEventHandler _onChangeEventHandlerModel;

        public OnChangeEventHandlerViewModel()
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