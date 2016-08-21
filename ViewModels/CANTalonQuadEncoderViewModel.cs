using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class CANTalonQuadEncoderViewModel : ViewModelBase<CANTalonQuadEncoder>, ICANTalonQuadEncoderProvider
    {
        CANTalonQuadEncoder _quadEncoderModel;
        int _canTalonID;

        public CANTalonQuadEncoderViewModel(ICANTalonProvider parent) : base(parent)
        {
        }

        #region ICANTalonQuadEncoderProvider
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
        public void SetCANTalon(CANTalon canTalon)
        {
            _quadEncoderModel = canTalon.QuadEncoder;
            _canTalonID = canTalon.ID;
        }
        #endregion

        #region IProvider
        public string Name
        {
            get
            {
                return "Quadrature Encoder";
            }
        }
        public string GetModelJson()
        {
            return JsonSerialize(_quadEncoderModel);
        }
        public void SetModelJson(string text)
        {
            _quadEncoderModel = JsonDeserialize<CANTalonQuadEncoder>(text);
        }
        #endregion

        #region ViewModelBase
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

        public override string ModelTypeName
        {
            get
            {
                var brokenName = _quadEncoderModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        protected override CANTalonQuadEncoder ModelInstance
        {
            get
            {
                return _quadEncoderModel;
            }

            set
            {
                _quadEncoderModel = value;
            }
        }
        #endregion

        #region IInputProvider
        public void UpdateInputValues()
        {
            RawVelocity = IOService.Instance.CANTalons[_canTalonID].EncoderVelocity;
            Velocity = RawVelocity * ConversionRatio + SensorOffset;

            RawValue = IOService.Instance.CANTalons[_canTalonID].EncoderValue;
            Value = RawValue * ConversionRatio + SensorOffset;
        }
        #endregion
    }
}