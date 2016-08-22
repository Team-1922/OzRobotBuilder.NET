using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class QuadEncoderViewModel : ViewModelBase<QuadEncoder>, IQuadEncoderProvider
    {
        public QuadEncoderViewModel(ISubsystemProvider parent) : base(parent)
        {
        }

        #region IQuadEncoderProvider
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

        public int ID
        {
            get
            {
                return ModelReference.ID;
            }

            set
            {
                var temp = ModelReference.ID;
                SetProperty(ref temp, value);
                ModelReference.ID = temp;
            }
        }

        public int ID1
        {
            get
            {
                return ModelReference.ID1;
            }

            set
            {
                var temp = ModelReference.ID1;
                SetProperty(ref temp, value);
                ModelReference.ID1 = temp;
            }
        }

        public long RawValue
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
                return ModelReference.Name;
            }

            set
            {
                var temp = ModelReference.Name;
                SetProperty(ref temp, value);
                ModelReference.Name = temp;
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
                var brokenName = ModelReference.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }
        #endregion
    }
}