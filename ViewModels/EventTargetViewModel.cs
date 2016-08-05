using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class EventTargetViewModel : IEventTargetProvider
    {
        EventTarget _eventTargetModel;

        public string Path
        {
            get
            {
                return _eventTargetModel.Path;
            }

            set
            {
                _eventTargetModel.Path = value;
            }
        }

        public EventTargetType Type
        {
            get
            {
                return _eventTargetModel.Type;
            }

            set
            {
                _eventTargetModel.Type = value;
            }
        }

        public string Value
        {
            get
            {
                return _eventTargetModel.Value;
            }

            set
            {
                _eventTargetModel.Value = value;
            }
        }

        public void SetEventTarget(EventTarget eventTarget)
        {
            _eventTargetModel = eventTarget;
        }
    }
}
