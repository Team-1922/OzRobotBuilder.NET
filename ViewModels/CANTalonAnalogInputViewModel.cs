using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class CANTalonAnalogInputViewModel : ViewModelBase<CANTalonAnalogInput>, ICANTalonAnalogInputProvider
    {
        int _canTalonID;

        public CANTalonAnalogInputViewModel(ICANTalonProvider parent) : base(parent)
        {
        }

        #region ICANTalonAnalogInputProvider
        public double ConversionRatio
        {
            get
            {
                return ModelReference.ConversionRatio;
            }

            set
            {
                var temp = ModelReference.ConversionRatio;
                SetProperty(ref temp, value);
                ModelReference.ConversionRatio = temp;
            }
        }

        public int RawValue
        {
            get
            {
                return ModelReference.RawValue;
            }

            private set
            {
                var temp = ModelReference.RawValue;
                SetProperty(ref temp, value);
                ModelReference.RawValue = temp;
            }
        }

        public double RawVelocity
        {
            get
            {
                return ModelReference.RawVelocity;
            }

            private set
            {
                var temp = ModelReference.RawVelocity;
                SetProperty(ref temp, value);
                ModelReference.RawVelocity = temp;
            }
        }

        public double SensorOffset
        {
            get
            {
                return ModelReference.SensorOffset;
            }

            set
            {
                var temp = ModelReference.SensorOffset;
                SetProperty(ref temp, value);
                ModelReference.SensorOffset = temp;
            }
        }

        public double Value
        {
            get
            {
                return ModelReference.Value;
            }

            private set
            {
                var temp = ModelReference.Value;
                SetProperty(ref temp, value);
                ModelReference.Value = temp;
            }
        }

        public double Velocity
        {
            get
            {
                return ModelReference.Velocity;
            }

            private set
            {
                var temp = ModelReference.Velocity;
                SetProperty(ref temp, value);
                ModelReference.Velocity = temp;
            }
        }
        #endregion

        #region IInputProvider
        public void UpdateInputValues()
        {
            RawVelocity = IOService.Instance.CANTalons[_canTalonID].AnalogVelocity;
            Velocity = RawVelocity * ConversionRatio + SensorOffset;

            RawValue = IOService.Instance.CANTalons[_canTalonID].AnalogValue;
            Value = RawValue * ConversionRatio + SensorOffset;
        }
        #endregion

        #region IProvider
        public string Name
        {
            get
            {
                return "Analog Input";
            }
        }
        #endregion

        #region ViewModelBase
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
                var brokenName = ModelReference.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        protected override void OnModelChange()
        {
            _canTalonID = (Parent as CANTalonViewModel).ID;
        }
        #endregion
    }
}