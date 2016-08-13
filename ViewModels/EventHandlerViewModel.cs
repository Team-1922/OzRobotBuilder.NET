using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    internal class EventHandlerViewModel : ViewModelBase, IEventHandlerProvider
    {
        private Models.EventHandler _eventHandlerModel;

        public string Condition
        {
            get
            {
                return _eventHandlerModel.Condition;
            }

            set
            {
                var temp = _eventHandlerModel.Condition;
                SetProperty(ref temp, value);
                _eventHandlerModel.Condition = temp;
            }
        }

        public string Expression
        {
            get
            {
                return _eventHandlerModel.Expression;
            }

            set
            {
                var temp = _eventHandlerModel.Expression;
                SetProperty(ref temp, value);
                _eventHandlerModel.Expression = temp;
            }
        }

        public string Name
        {
            get
            {
                return _eventHandlerModel.Name;
            }

            set
            {
                var temp = _eventHandlerModel.Name;
                SetProperty(ref temp, value);
                _eventHandlerModel.Expression = temp;
            }
        }

        protected override string GetValue(string key)
        {
            switch (key)
            {
                case "Name":
                    return Name;
                case "Expression":
                    return Expression.ToString();
                case "Condition":
                    return Condition.ToString();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }

        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "Name":
                    Name = value;
                    break;
                case "Expression":
                    Expression = value;
                    break;
                case "Condition":
                    Condition = value;
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }
        }
    }
}
