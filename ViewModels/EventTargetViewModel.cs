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
        
        /// <summary>
        /// This is overriden to only give some values if the other values have a certain state
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> GetOverrideKeys()
        {
            var ret = new List<string>() { "Name", "Type", "Value" };
            if (Type == EventTargetType.ModifyValue)
                ret.Add("Path");
            return ret;
        }

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
                UpdateKeyValueList();
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

        public override string this[string key]
        {
            get
            {
                switch (key)
                {
                    case "Name":
                        return Name;
                    case "Path":
                        return Path;
                    case "Type":
                        return Type.ToString();
                    case "Value":
                        return Value;
                    default:
                        throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
                }
            }

            set
            {
                switch (key)
                {
                    case "Path":
                        Path = value;
                        break;
                    case "Type":
                        Type = SafeCastEnum<EventTargetType>(value);
                        break;
                    case "Value":
                        Value = value;
                        break;
                    default:
                        throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
                }
            }
        }

        public void SetEventTarget(EventTarget eventTarget)
        {
            _eventTargetModel = eventTarget;
        }
    }
}
