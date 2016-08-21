using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class QuadEncoderViewModel : ViewModelBase<QuadEncoder>, IQuadEncoderProvider
    {
        QuadEncoder _quadEncoderModel;

        public QuadEncoderViewModel(ISubsystemProvider parent) : base(parent)
        {
        }

        #region IQuadEncoderProvider
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

        public int ID
        {
            get
            {
                return _quadEncoderModel.ID;
            }

            set
            {
                var temp = _quadEncoderModel.ID;
                SetProperty(ref temp, value);
                _quadEncoderModel.ID = temp;
            }
        }

        public int ID1
        {
            get
            {
                return _quadEncoderModel.ID1;
            }

            set
            {
                var temp = _quadEncoderModel.ID1;
                SetProperty(ref temp, value);
                _quadEncoderModel.ID1 = temp;
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

        public void SetQuadEncoder(QuadEncoder quadEncoder)
        {
            _quadEncoderModel = quadEncoder;
        }
        #endregion

        #region IInputProvider
        public void UpdateInputValues()
        {
            var thisInput = IOService.Instance.DigitalInputs[ID] as IQuadEncoderIOService;
            if (null == thisInput)
                //this means this digital input was not constructed with the quad encoder IO Service
                return;//TODO: throw an exception or log

            RawValue = (long)thisInput.Value;
            Value = RawValue * ConversionRatio;

            RawVelocity = thisInput.Velocity;
            Velocity = RawVelocity * ConversionRatio;
        }
        #endregion

        #region IProvider
        public string Name
        {
            get
            {
                return _quadEncoderModel.Name;
            }

            set
            {
                var temp = _quadEncoderModel.Name;
                SetProperty(ref temp, value);
                _quadEncoderModel.Name = temp;
            }
        }
        #endregion

        #region ViewModelBase
        protected override string GetValue(string key)
        {
            switch (key)
            {
                case "Name":
                    return Name;
                case "ID":
                    return ID.ToString();
                case "ID1":
                    return ID1.ToString();
                case "ConversionRatio":
                    return ConversionRatio.ToString();
                case "RawValue":
                    return RawValue.ToString();
                case "RawVelocity":
                    return RawVelocity.ToString();
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
                case "Name":
                    Name = value;
                    break;
                case "ID":
                    ID = SafeCastInt(value);
                    break;
                case "ID1":
                    ID1 = SafeCastInt(value);
                    break;
                case "ConversionRatio":
                    ConversionRatio = SafeCastDouble(value);
                    break;
                case "RawValue":
                    RawValue = SafeCastLong(value);
                    break;
                case "RawVelocity":
                    RawVelocity = SafeCastDouble(value);
                    break;
                case "Value":
                    Value = SafeCastDouble(value);
                    break;
                case "Velocity":
                    Velocity = SafeCastDouble(value);
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

        protected override QuadEncoder ModelInstance
        {
            get
            {
                return _quadEncoderModel;
            }

            set
            {
                SetQuadEncoder(value);
            }
        }
        #endregion
    }
}