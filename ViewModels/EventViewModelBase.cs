using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal abstract class EventViewModelBase : ViewModelBase
    {
        public IEventTargetProvider EventTarget
        {
            get
            {
                return _eventTargetProvider;
            }
            protected set
            {
                _eventTargetProvider = value as EventTargetViewModel;
            }
        }

        protected List<string> GetTargetKeys()
        {
            var ret = new List<string>();
            var targetKeys = _eventTargetProvider.GetKeys();
            foreach (var key in targetKeys)
            {
                ret.Add($"Target {key}");
            }
            return ret;
        }

        protected bool TryGetTargetValue(string key, out string value)
        {
            switch (key)
            {
                case "EventTarget":
                    value = "EventTarget";
                    return true;
                case "Target Type":
                    value = EventTarget.Type.ToString();
                    return true;
                case "Target Path":
                    value = EventTarget.Path;
                    return true;
                case "Target Value":
                    value = EventTarget.Value;
                    return true;
            }
            value = "";
            return false;
        }

        protected bool TrySetTargetValue(string key, string value)
        {
            switch (key)
            {
                case "Target Type":
                    EventTarget.Type = SafeCastEnum<EventTargetType>(value);
                    return true;
                case "Target Path":
                    EventTarget.Path = value;
                    return true;
                case "Target Value":
                    EventTarget.Value = value;
                    return true;
            }
            return false;
        }

        #region Private Fields
        EventTargetViewModel _eventTargetProvider = new EventTargetViewModel();
        #endregion
    }
}
