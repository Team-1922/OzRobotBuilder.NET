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
        private string _conditionExpressionParsingErrors = "";
        private IExpression _expression;
        private string _expressionParsingErrors = "";

        public EventHandlerViewModel(IHierarchialAccess topParent) : base(topParent)
        {
        }

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
                UpdateConditionExpression(value);
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
                UpdateExpressionExpression(value);
            }
        }

        public double ExpressionEvaluated
        {
            get
            {
                return _expression?.Evaluate() ?? double.NaN;
            }
        }

        public double ConditionEvaluated
        {
            get
            {
                return _conditionExpression?.Evaluate() ?? double.NaN;
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
                    return Expression;
                case "Condition":
                    return Condition;
                case "ConditionEvaluated":
                    return ConditionEvaluated.ToString();
                case "ExpressionEvaluated":
                    return ExpressionEvaluated.ToString();
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

        protected override string GetErrorString(string attribName)
        {
            string firstErrors = base.GetErrorString(attribName);
            if (firstErrors != "")
                return firstErrors;

            if(attribName == "Expression")
            {
                return _expressionParsingErrors ?? "";
            }
            else if (attribName == "Condition")
            {
                return _conditionExpressionParsingErrors ?? "";
            }
            return "";
        }

        public void SetEventHandler(Models.EventHandler eventHandler)
        {
            _eventHandlerModel = eventHandler;
            UpdateConditionExpression(_eventHandlerModel.Condition);
            UpdateExpressionExpression(_eventHandlerModel.Expression);

        }

        private void UpdateConditionExpression(string value)
        {
            try
            {
                _conditionExpressionParsingErrors = "";
                _conditionExpression = ExpressionParserService.Instance.ParseExpression(value, TopParent);

                //trigger the change event for the evaluated property
                string blank = "";
                SetProperty(ref blank, value, "ConditionEvaluated");
            }
            catch (Exception e)
            {
                _conditionExpressionParsingErrors = e.Message;
                if (TypeRestrictions.ThrowsExceptionsOnValidationFailure)
                    throw;
            }
        }
        private void UpdateExpressionExpression(string value)
        {
            try
            {
                _expressionParsingErrors = "";
                _expression = ExpressionParserService.Instance.ParseExpression(value, TopParent);

                //trigger the change event for the evaluated property
                string blank = "";
                SetProperty(ref blank, value, "ExpressionEvaluated");
            }
            catch (Exception e)
            {
                _expressionParsingErrors = e.Message;
                if (TypeRestrictions.ThrowsExceptionsOnValidationFailure)
                    throw;
            }
        }
    }
}
