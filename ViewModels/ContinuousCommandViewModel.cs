using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class ContinuousCommandViewModel : IContinuousCommandProvider
    {
        ContinuousCommand _commandModel;

        public IEventTargetProvider EventTarget
        {
            get
            {
                return _eventTargetProvider;
            }
        }

        public string Name
        {
            get
            {
                return _commandModel.Name;
            }

            set
            {
                _commandModel.Name = value;
            }
        }

        public void SetContinuousCommand(ContinuousCommand continuousCommand)
        {
            _commandModel = continuousCommand;
            _eventTargetProvider.SetEventTarget(_commandModel.EventTarget);
        }

        #region Private Fields
        IEventTargetProvider _eventTargetProvider = new EventTargetViewModel();
        #endregion
    }
}