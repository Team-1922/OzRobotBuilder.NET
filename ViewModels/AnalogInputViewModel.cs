using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    /// <summary>
    /// The viewmodel for all analog inputs
    /// </summary>
    internal class AnalogInputViewModel : BindableBase, IAnalogInputProvider
    {
        /// <summary>
        /// the reference to the model object
        /// </summary>
        protected AnalogInput _analogInputModel;

        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        public int AccumulatorCenter
        {
            get
            {
                return _analogInputModel.AccumulatorCenter;
            }

            set
            {
                var temp = _analogInputModel.AccumulatorCenter;
                SetProperty(ref temp, IOService.Instance.AnalogInputs[ID].AccumulatorCenter = value);
                _analogInputModel.AccumulatorCenter = temp;
            }
        }

        /// <summary>
        /// The AccumulatorCount property of the model
        /// </summary>
        public long AccumulatorCount
        {
            get
            {
                return _analogInputModel.AccumulatorCount;
            }

            private set
            {
                var temp = _analogInputModel.AccumulatorCount;
                SetProperty(ref temp, value);
                _analogInputModel.AccumulatorCount = temp;
            }
        }

        /// <summary>
        /// The AccumulatorDeadband property of the model
        /// </summary>
        public int AccumulatorDeadband
        {
            get
            {
                return _analogInputModel.AccumulatorDeadband;
            }

            set
            {
                var temp = _analogInputModel.AccumulatorDeadband;
                SetProperty(ref temp, IOService.Instance.AnalogInputs[ID].AccumulatorDeadband = value);
                _analogInputModel.AccumulatorDeadband = temp;
            }
        }

        /// <summary>
        /// The AccumulatorValue property of the model
        /// </summary>
        public double AccumulatorValue
        {
            get
            {
                return _analogInputModel.AccumulatorValue;
            }

            private set
            {
                var temp = _analogInputModel.AccumulatorValue;
                SetProperty(ref temp, value);
                _analogInputModel.AccumulatorValue = temp;
            }
        }

        /// <summary>
        /// The AverageBits property of the model
        /// </summary>
        public int AverageBits
        {
            get
            {
                return _analogInputModel.AverageBits;
            }

            set
            {
                var temp = _analogInputModel.AverageBits;
                SetProperty(ref temp, IOService.Instance.AnalogInputs[ID].AverageBits = value);
                _analogInputModel.AverageBits = temp;
            }
        }

        /// <summary>
        /// The ConversionRatio property of the model
        /// </summary>
        public double ConversionRatio
        {
            get
            {
                return _analogInputModel.ConversionRatio;
            }

            set
            {
                var temp = _analogInputModel.ConversionRatio;
                SetProperty(ref temp, value);
                _analogInputModel.ConversionRatio = temp;
            }
        }

        /// <summary>
        /// The ID property of the model
        /// </summary>
        public int ID
        {
            get
            {
                return _analogInputModel.ID;
            }

            set
            {
                var temp = _analogInputModel.ID;
                SetProperty(ref temp, value);
                _analogInputModel.ID = temp;
            }
        }

        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        public string Name
        {
            get
            {
                return _analogInputModel.Name;
            }

            set
            {
                var temp = _analogInputModel.Name;
                SetProperty(ref temp, value);
                _analogInputModel.Name = temp;
            }
        }

        /// <summary>
        /// The OversampleBits property of the model
        /// </summary>
        public int OversampleBits
        {
            get
            {
                return _analogInputModel.OversampleBits;
            }

            set
            {
                var temp = _analogInputModel.OversampleBits;
                SetProperty(ref temp, IOService.Instance.AnalogInputs[ID].OversampleBits = value);
                _analogInputModel.OversampleBits = temp;
            }
        }

        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        public int RawValue
        {
            get
            {
                return _analogInputModel.RawValue;
            }

            private set
            {
                var temp = _analogInputModel.RawValue;
                SetProperty(ref temp, value);
                _analogInputModel.RawValue = temp;
            }
        }

        /// <summary>
        /// The RawAccumulatorValue property of the model
        /// </summary>
        public long RawAccumulatorValue
        {
            get
            {
                return _analogInputModel.RawAccumulatorValue;
            }

            private set
            {
                var temp = _analogInputModel.AccumulatorValue;
                SetProperty(ref temp, value);
                _analogInputModel.AccumulatorValue = temp;
            }
        }

        /// <summary>
        /// The RawAverageValue property of the model
        /// </summary>
        public long RawAverageValue
        {
            get
            {
                return _analogInputModel.RawAverageValue;
            }

            private set
            {
                var temp = _analogInputModel.AverageValue;
                SetProperty(ref temp, value);
                _analogInputModel.AverageValue = temp;
            }
        }

        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        public double SensorOffset
        {
            get
            {
                return _analogInputModel.SensorOffset;
            }

            set
            {
                var temp = _analogInputModel.SensorOffset;
                SetProperty(ref temp, value);
                _analogInputModel.SensorOffset = temp;
            }
        }

        /// <summary>
        /// The Value property of the model
        /// </summary>
        public double Value
        {
            get
            {
                return _analogInputModel.Value;
            }

            private set
            {
                var temp = _analogInputModel.Value;
                SetProperty(ref temp, value);
                _analogInputModel.Value = temp;
            }
        }

        /// <summary>
        /// The AverageValue property of the model
        /// </summary>
        public double AverageValue
        {
            get
            {
                return _analogInputModel.AverageValue;
            }

            private set
            {
                var temp = _analogInputModel.AverageValue;
                SetProperty(ref temp, value);
                _analogInputModel.AverageValue = temp;
            }
        }

        /// <summary>
        /// Resets the hardware analog input accumulator
        /// </summary>
        public void ResetAccumulator()
        {
            IOService.Instance.AnalogInputs[ID].ResetAccumulator();
        }
        
        /// <summary>
        /// Sets the analog input model instance for this viewmodel
        /// </summary>
        /// <param name="analogInput">the analog input model instance</param>
        public void SetAnalogInput(AnalogInput analogInput)
        {
            _analogInputModel = analogInput;
        }

        /// <summary>
        /// Updates the attached analog input model instance with the IO service
        /// </summary>
        public void UpdateInputValues()
        {
            IAnalogInputIOService thisIOService = IOService.Instance.AnalogInputs[ID];

            RawAverageValue = thisIOService.AverageValue;
            AverageValue = RawAverageValue * ConversionRatio + SensorOffset;

            RawAccumulatorValue = thisIOService.AccumulatorValue;
            AccumulatorValue = RawAccumulatorValue * ConversionRatio + SensorOffset;

            RawValue = (int)thisIOService.Value;
            Value = RawValue * ConversionRatio + SensorOffset;

            AccumulatorCount = thisIOService.AccumulatorCount;
        }
    }
}