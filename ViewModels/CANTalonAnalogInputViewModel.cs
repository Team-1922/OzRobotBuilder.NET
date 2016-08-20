using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class CANTalonAnalogInputViewModel : ViewModelBase, ICANTalonAnalogInputProvider
    {
        CANTalonAnalogInput _aiModel;
        int _canTalonID;

        public CANTalonAnalogInputViewModel(ICANTalonProvider parent) : base(parent)
        {
        }

        public double ConversionRatio
        {
            get
            {
                return _aiModel.ConversionRatio;
            }

            set
            {
                var temp = _aiModel.ConversionRatio;
                SetProperty(ref temp, value);
                _aiModel.ConversionRatio = temp;
            }
        }

        public int RawValue
        {
            get
            {
                return _aiModel.RawValue;
            }

            private set
            {
                var temp = _aiModel.RawValue;
                SetProperty(ref temp, value);
                _aiModel.RawValue = temp;
            }
        }

        public double RawVelocity
        {
            get
            {
                return _aiModel.RawVelocity;
            }

            private set
            {
                var temp = _aiModel.RawVelocity;
                SetProperty(ref temp, value);
                _aiModel.RawVelocity = temp;
            }
        }

        public double SensorOffset
        {
            get
            {
                return _aiModel.SensorOffset;
            }

            set
            {
                var temp = _aiModel.SensorOffset;
                SetProperty(ref temp, value);
                _aiModel.SensorOffset = temp;
            }
        }

        public double Value
        {
            get
            {
                return _aiModel.Value;
            }

            private set
            {
                var temp = _aiModel.Value;
                SetProperty(ref temp, value);
                _aiModel.Value = temp;
            }
        }

        public double Velocity
        {
            get
            {
                return _aiModel.Velocity;
            }

            private set
            {
                var temp = _aiModel.Velocity;
                SetProperty(ref temp, value);
                _aiModel.Velocity = temp;
            }
        }

        public string Name
        {
            get
            {
                return "Analog Input";
            }
        }

        protected override string GetValue(string key)
        {
                switch (key)
                {
                    case "ConversionRatio":
                        return ConversionRatio.ToString();
                    case "Name":
                        return Name.ToString();
                    case "RawValue":
                        return RawValue.ToString();
                    case "RawVelocity":
                        return RawVelocity.ToString();
                    case "SensorOffset":
                        return SensorOffset.ToString();
                    case "Value":
                        return Value.ToString();
                    case "Velocity":
                        return Velocity.ToString();
                    default:
                        throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
                }
        }

        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "ConversionRatio":
                    ConversionRatio = SafeCastDouble(value);
                    break;
                case "SensorOffset":
                    SensorOffset = SafeCastLong(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }
        }

        public override string ModelTypeName
        {
            get
            {
                var brokenName = _aiModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }


        public void SetCANTalon(CANTalon canTalon)
        {
            _aiModel = canTalon.AnalogInput;
            _canTalonID = canTalon.ID;
        }

        public void UpdateInputValues()
        {
            RawVelocity = IOService.Instance.CANTalons[_canTalonID].AnalogVelocity;
            Velocity = RawVelocity * ConversionRatio + SensorOffset;

            RawValue = IOService.Instance.CANTalons[_canTalonID].AnalogValue;
            Value = RawValue * ConversionRatio + SensorOffset;
        }
    }
}