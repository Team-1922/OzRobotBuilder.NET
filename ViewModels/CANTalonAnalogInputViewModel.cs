using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    internal class CANTalonAnalogInputViewModel : BindableBase, ICANTalonAnalogInputProvider
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