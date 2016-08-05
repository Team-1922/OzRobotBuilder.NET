using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class QuadEncoderViewModel : IQuadEncoderProvider
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
                _quadEncoderModel.ConversionRatio = value;
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
                _quadEncoderModel.ID = value;
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
                _quadEncoderModel.ID1 = value;
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
                _quadEncoderModel.Name = value;
            }
        }

        public long RawValue
        {
            get
            {
                return _quadEncoderModel.RawValue;
            }
        }

        public double RawVelocity
        {
            get
            {
                return _quadEncoderModel.RawVelocity;
            }
        }

        public double Value
        {
            get
            {
                return _quadEncoderModel.Value;
            }
        }

        public double Velocity
        {
            get
            {
                return _quadEncoderModel.Velocity;
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

            _quadEncoderModel.RawValue = (long)thisInput.Value;
            _quadEncoderModel.Value = RawValue * ConversionRatio;

            _quadEncoderModel.RawVelocity = (long)thisInput.Velocity;
            _quadEncoderModel.Velocity = RawVelocity * ConversionRatio;
        }
    }
}