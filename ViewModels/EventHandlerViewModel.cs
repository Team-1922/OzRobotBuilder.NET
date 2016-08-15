using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class EventHandlerViewModel : ViewModelBase, IEventHandlerProvider
    {
        private Models.EventHandler _eventHandlerModel;
        private IExpression _conditionExpression;
        private IExpression _expression;

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
                //this throws an exception AFTER the condition variable is set, becuase it would be annoying
                //  if the text in the box gets deleted after the user types it in and it is wrong; this could be a pretty big
                //  set of text too
                _conditionExpression = ExpressionParserService.Instance.ParseExpression(value);
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
                //this throws an exception AFTER the expression variable is set, becuase it would be annoying
                //  if the text in the box gets deleted after the user types it in and it is wrong; this could be a pretty big
                //  set of text too
                _expression = ExpressionParserService.Instance.ParseExpression(value);
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

        public bool ConditionMet
        {
            get
            {
                return _conditionExpression != null ? _conditionExpression.Evaluate() > 1 : false;
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
                case "ConditionMet":
                    return ConditionMet.ToString();
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

        public override string ModelTypeName
        {
            get
            {
                var brokenName = _eventHandlerModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        public void SetEventHandler(Models.EventHandler eventHandler)
        {
            _eventHandlerModel = eventHandler;
        }
    }
}
