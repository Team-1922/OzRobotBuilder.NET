using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class PWMOutputViewModel : ViewModelBase, IPWMOutputProvider
    {
        protected PWMOutput _pwmOutputModel;

        public void SetPWMOutput(PWMOutput pwmOutput)
        {
            _pwmOutputModel = pwmOutput;
        }

        public int ID
        {
            get
            {
                return _pwmOutputModel.ID;
            }

            set
            {
                var temp = _pwmOutputModel.ID;
                SetProperty(ref temp, value);
                _pwmOutputModel.ID = temp;
            }
        }
        public string Name
        {
            get
            {
                return _pwmOutputModel.Name;
            }

            set
            {
                var temp = _pwmOutputModel.Name;
                SetProperty(ref temp, value);
                _pwmOutputModel.Name = temp;
            }
        }
        public double Value
        {
            get
            {
                return _pwmOutputModel.Value;
            }

            set
            {
                var temp = _pwmOutputModel.Value;
                SetProperty(ref temp, IOService.Instance.PWMOutputs[ID].Value = TypeRestrictions.Clamp("PWMOutput.Value", value));
                _pwmOutputModel.Value = temp;
            }
        }

        protected override string GetValue(string key)
        {
            switch(key)
            {
                case "Name":
                    return Name;
                case "Value":
                    return Value.ToString();
                case "ID":
                    return ID.ToString();
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
                case "Value":
                    Value = SafeCastDouble(value);
                    break;
                case "ID":
                    ID = SafeCastInt(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
            
        }
    }
}