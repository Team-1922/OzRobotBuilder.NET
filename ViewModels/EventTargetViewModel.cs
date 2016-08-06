using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class EventTargetViewModel : ViewModelBase, IEventTargetProvider
    {
        EventTarget _eventTargetModel;

        public string Name
        {
            get
            {
                return "Event Target";
            }
        }

        public string Path
        {
            get
            {
                return _eventTargetModel.Path;
            }

            set
            {
                var temp = _eventTargetModel.Path;
                SetProperty(ref temp, value);
                _eventTargetModel.Path = temp;
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
                var temp = _eventTargetModel.Type;
                SetProperty(ref temp, value);
                _eventTargetModel.Type = temp;
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
                var temp = _eventTargetModel.Value;
                SetProperty(ref temp, value);
                _eventTargetModel.Value = temp;
            }
        }

        public void SetEventTarget(EventTarget eventTarget)
        {
            _eventTargetModel = eventTarget;
        }
    }
}
