using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class CANTalonQuadEncoderViewModel : ViewModelBase, ICANTalonQuadEncoderProvider
    {
        CANTalonQuadEncoder _quadEncoderModel;
        int _canTalonID;

        public double ConversionRatio
        {
            get
            {
                return _quadEncoderModel.ConversionRatio;
            }

            set
            {
                var temp = _quadEncoderModel.ConversionRatio;
                SetProperty(ref temp, value);
                _quadEncoderModel.ConversionRatio = temp;
            }
        }

        public long RawValue
        {
            get
            {
                return _quadEncoderModel.RawValue;
            }

            private set
            {
                var temp = _quadEncoderModel.RawValue;
                SetProperty(ref temp, value);
                _quadEncoderModel.RawValue = temp;
            }
        }

        public double RawVelocity
        {
            get
            {
                return _quadEncoderModel.RawVelocity;
            }

            private set
            {
                var temp = _quadEncoderModel.RawVelocity;
                SetProperty(ref temp, value);
                _quadEncoderModel.RawVelocity = temp;
            }
        }

        public double SensorOffset
        {
            get
            {
                return _quadEncoderModel.SensorOffset;
            }

            set
            {
                var temp = _quadEncoderModel.SensorOffset;
                SetProperty(ref temp, value);
                _quadEncoderModel.SensorOffset = temp;
            }
        }

        public double Value
        {
            get
            {
                return _quadEncoderModel.Value;
            }

            private set
            {
                var temp = _quadEncoderModel.Value;
                SetProperty(ref temp, value);
                _quadEncoderModel.Value = temp;
            }
        }

        public double Velocity
        {
            get
            {
                return _quadEncoderModel.Velocity;
            }

            private set
            {
                var temp = _quadEncoderModel.Velocity;
                SetProperty(ref temp, value);
                _quadEncoderModel.Velocity = temp;
            }
        }

        public string Name
        {
            get
            {
                return "Quadrature Encoder";
            }
        }

        protected override string GetValue(string key)
        {
            switch(key)
            {
                case "ConversionRatio":
                    return ConversionRatio.ToString();
                case "Name":
                    return Name;
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
            switch(key)
            {
                case "ConversionRatio":
                    ConversionRatio = SafeCastDouble(value);
                    break;
                case "SensorOffset":
                    SensorOffset = SafeCastDouble(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }            
        }

        public void SetCANTalon(CANTalon canTalon)
        {
            _quadEncoderModel = canTalon.QuadEncoder;
            _canTalonID = canTalon.ID;
        }

        public void UpdateInputValues()
        {
            RawVelocity = IOService.Instance.CANTalons[_canTalonID].EncoderVelocity;
            Velocity = RawVelocity * ConversionRatio + SensorOffset;

            RawValue = IOService.Instance.CANTalons[_canTalonID].EncoderValue;
            Value = RawValue * ConversionRatio + SensorOffset;
        }
    }
}