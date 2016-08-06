using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class CANTalonQuadEncoderViewModel : ICANTalonQuadEncoderProvider
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
                _quadEncoderModel.ConversionRatio = value;
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

        public double SensorOffset
        {
            get
            {
                return _quadEncoderModel.SensorOffset;
            }

            set
            {
                _quadEncoderModel.SensorOffset = value;
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

        public string Name
        {
            get
            {
                return "Quadrature Encoder";
            }
        }

        public void SetCANTalon(CANTalon canTalon)
        {
            _quadEncoderModel = canTalon.QuadEncoder;
            _canTalonID = canTalon.ID;
        }

        public void UpdateInputValues()
        {
            _quadEncoderModel.RawVelocity = IOService.Instance.CANTalons[_canTalonID].EncoderVelocity;
            _quadEncoderModel.Velocity = RawVelocity * ConversionRatio + SensorOffset;

            _quadEncoderModel.RawValue = IOService.Instance.CANTalons[_canTalonID].EncoderValue;
            _quadEncoderModel.Value = RawValue * ConversionRatio + SensorOffset;
        }
    }
}