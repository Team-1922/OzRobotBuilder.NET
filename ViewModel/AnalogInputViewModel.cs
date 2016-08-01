using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    /// <summary>
    /// The viewmodel for all analog inputs
    /// </summary>
    public class AnalogInputViewModel : IAnalogInputProvider
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
                return _analogInputModel.AccumulatorCenter = IOService.Instance.AnalogInputs[ID].AccumulatorCenter;
            }

            set
            {
                _analogInputModel.AccumulatorCenter = IOService.Instance.AnalogInputs[ID].AccumulatorCenter = value;
            }
        }

        /// <summary>
        /// The AccumulatorCount property of the model
        /// </summary>
        public long AccumulatorCount
        {
            get
            {
                return _analogInputModel.AccumulatorCount = IOService.Instance.AnalogInputs[ID].AccumulatorCount;
            }
        }

        /// <summary>
        /// The AccumulatorDeadband property of the model
        /// </summary>
        public int AccumulatorDeadband
        {
            get
            {
                return _analogInputModel.AccumulatorDeadband = IOService.Instance.AnalogInputs[ID].AccumulatorDeadband;
            }

            set
            {
                _analogInputModel.AccumulatorDeadband = IOService.Instance.AnalogInputs[ID].AccumulatorDeadband = value;
            }
        }

        /// <summary>
        /// The AccumulatorInitialValue property of the model
        /// </summary>
        public int AccumulatorInitialValue
        {
            get
            {
                return _analogInputModel.AccumulatorInitialValue;
            }

            set
            {
                _analogInputModel.AccumulatorInitialValue = value;
            }
        }

        /// <summary>
        /// The AccumulatorValue property of the model
        /// </summary>
        public long AccumulatorValue
        {
            get
            {
                return _analogInputModel.AccumulatorValue = IOService.Instance.AnalogInputs[ID].AccumulatorValue;
            }
        }

        /// <summary>
        /// The AverageBits property of the model
        /// </summary>
        public int AverageBits
        {
            get
            {
                return _analogInputModel.AverageBits = IOService.Instance.AnalogInputs[ID].AverageBits;
            }

            set
            {
                _analogInputModel.AverageBits = IOService.Instance.AnalogInputs[ID].AverageBits = value;
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
                _analogInputModel.ConversionRatio = value;
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
                _analogInputModel.ID = value;
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
                _analogInputModel.Name = value;
            }
        }

        /// <summary>
        /// The OversampleBits property of the model
        /// </summary>
        public int OversampleBits
        {
            get
            {
                return _analogInputModel.OversampleBits = IOService.Instance.AnalogInputs[ID].OversampleBits;
            }

            set
            {
                _analogInputModel.OversampleBits = IOService.Instance.AnalogInputs[ID].OversampleBits = value;
            }
        }

        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        public int RawValue
        {
            get
            {
                return _analogInputModel.RawValue = (int)IOService.Instance.AnalogInputs[ID].Value;
            }
        }

        /// <summary>
        /// The RawAverageValue property of the model
        /// </summary>
        public long RawAverageValue
        {
            get { return _analogInputModel.RawAverageValue = IOService.Instance.AnalogInputs[ID].AverageValue; }
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
                _analogInputModel.SensorOffset = value;
            }
        }

        /// <summary>
        /// The Value property of the model
        /// </summary>
        public double Value
        {
            get
            {
                return _analogInputModel.Value = RawValue * ConversionRatio;
            }
        }

        /// <summary>
        /// The AverageValue property of the model
        /// </summary>
        public double AverageValue
        {
            get { return _analogInputModel.AverageValue = RawAverageValue * ConversionRatio; }
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
        /// Updates the attached analot input model instance with the IO service
        /// </summary>
        public void UpdateInputValues()
        {
            var w = AverageValue;//NOTE: this updates both AverageValue and RawAverageValue
            var x = Value;//NOTE: this updates both Value and RawValue
            var y = AccumulatorCount;
            var z = AccumulatorValue;
        }
    }
}