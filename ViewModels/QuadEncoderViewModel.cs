using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class QuadEncoderViewModel : ViewModelBase, IQuadEncoderProvider
    {
        QuadEncoder _quadEncoderModel;

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
    }
}