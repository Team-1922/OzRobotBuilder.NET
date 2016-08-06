using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class CANTalonAnalogInputViewModel : ICANTalonAnalogInputProvider
    {
        CANTalonAnalogInput _aiModel;
        int _canTalonID;

        public double ConversionRatio
        {
            get
            {
                return _aiModel.ConversionRatio;
            }

            set
            {
                _aiModel.ConversionRatio = value;
            }
        }

        public int RawValue
        {
            get
            {
                return _aiModel.RawValue;
            }
        }

        public double RawVelocity
        {
            get
            {
                return _aiModel.RawVelocity;
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
                _aiModel.SensorOffset = value;
            }
        }

        public double Value
        {
            get
            {
                return _aiModel.Value;
            }
        }

        public double Velocity
        {
            get
            {
                return _aiModel.Velocity;
            }
        }

        public void SetCANTalon(CANTalon canTalon)
        {
            _aiModel = canTalon.AnalogInput;
            _canTalonID = canTalon.ID;
        }

        public string Name
        {
            get
            {
                return "Analog Input";
            }
        }

        public void UpdateInputValues()
        {
            _aiModel.RawVelocity = IOService.Instance.CANTalons[_canTalonID].AnalogVelocity;
            _aiModel.Velocity = RawVelocity * ConversionRatio + SensorOffset;

            _aiModel.RawValue = IOService.Instance.CANTalons[_canTalonID].AnalogValue;
            _aiModel.Value = RawValue * ConversionRatio + SensorOffset;
        }
    }
}