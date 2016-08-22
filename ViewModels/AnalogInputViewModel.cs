using System;
using System.Collections.Generic;
using System.Linq;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    /// <summary>
    /// The viewmodel for all analog inputs
    /// </summary>
    internal class AnalogInputViewModel : ViewModelBase<AnalogInput>, IAnalogInputProvider
    {
        public AnalogInputViewModel(ISubsystemProvider parent) : base(parent)
        {
        }
        
        #region IAnalogInputProvider
        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        public int AccumulatorCenter
        {
            get
            {
                return ModelReference.AccumulatorCenter;
            }

            set
            {
                var temp = ModelReference.AccumulatorCenter;
                SetProperty(ref temp, IOService.Instance.AnalogInputs[ID].AccumulatorCenter = value);
                ModelReference.AccumulatorCenter = temp;
            }
        }

        /// <summary>
        /// The AccumulatorCount property of the model
        /// </summary>
        public long AccumulatorCount
        {
            get
            {
                return ModelReference.AccumulatorCount;
            }

            private set
            {
                var temp = ModelReference.AccumulatorCount;
                SetProperty(ref temp, value);
                ModelReference.AccumulatorCount = temp;
            }
        }

        /// <summary>
        /// The AccumulatorDeadband property of the model
        /// </summary>
        public int AccumulatorDeadband
        {
            get
            {
                return ModelReference.AccumulatorDeadband;
            }

            set
            {
                var temp = ModelReference.AccumulatorDeadband;
                SetProperty(ref temp, IOService.Instance.AnalogInputs[ID].AccumulatorDeadband = value);
                ModelReference.AccumulatorDeadband = temp;
            }
        }

        /// <summary>
        /// The AccumulatorValue property of the model
        /// </summary>
        public double AccumulatorValue
        {
            get
            {
                return ModelReference.AccumulatorValue;
            }

            private set
            {
                var temp = ModelReference.AccumulatorValue;
                SetProperty(ref temp, value);
                ModelReference.AccumulatorValue = temp;
            }
        }

        /// <summary>
        /// The AverageBits property of the model
        /// </summary>
        public int AverageBits
        {
            get
            {
                return ModelReference.AverageBits;
            }

            set
            {
                var temp = ModelReference.AverageBits;
                SetProperty(ref temp, IOService.Instance.AnalogInputs[ID].AverageBits = value);
                ModelReference.AverageBits = temp;
            }
        }

        /// <summary>
        /// The ConversionRatio property of the model
        /// </summary>
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

        /// <summary>
        /// The ID property of the model
        /// </summary>
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

        /// <summary>
        /// The OversampleBits property of the model
        /// </summary>
        public int OversampleBits
        {
            get
            {
                return ModelReference.OversampleBits;
            }

            set
            {
                var temp = ModelReference.OversampleBits;
                SetProperty(ref temp, IOService.Instance.AnalogInputs[ID].OversampleBits = value);
                ModelReference.OversampleBits = temp;
            }
        }

        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        public int RawValue
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

        /// <summary>
        /// The RawAccumulatorValue property of the model
        /// </summary>
        public long RawAccumulatorValue
        {
            get
            {
                return ModelReference.RawAccumulatorValue;
            }

            private set
            {
                var temp = ModelReference.AccumulatorValue;
                SetProperty(ref temp, value);
                ModelReference.AccumulatorValue = temp;
            }
        }

        /// <summary>
        /// The RawAverageValue property of the model
        /// </summary>
        public long RawAverageValue
        {
            get
            {
                return ModelReference.RawAverageValue;
            }

            private set
            {
                var temp = ModelReference.AverageValue;
                SetProperty(ref temp, value);
                ModelReference.AverageValue = temp;
            }
        }

        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
        public double SensorOffset
        {
            get
            {
                return ModelReference.SensorOffset;
            }

            set
            {
                var temp = ModelReference.SensorOffset;
                SetProperty(ref temp, value);
                ModelReference.SensorOffset = temp;
            }
        }

        /// <summary>
        /// The Value property of the model
        /// </summary>
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

        /// <summary>
        /// The AverageValue property of the model
        /// </summary>
        public double AverageValue
        {
            get
            {
                return ModelReference.AverageValue;
            }

            private set
            {
                var temp = ModelReference.AverageValue;
                SetProperty(ref temp, value);
                ModelReference.AverageValue = temp;
            }
        }

        /// <summary>
        /// Resets the hardware analog input accumulator
        /// </summary>
        public void ResetAccumulator()
        {
            IOService.Instance.AnalogInputs[ID].ResetAccumulator();
        }
        #endregion

        #region IInputProvider
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
        #endregion

        #region IProvider

        /// <summary>
        /// The AccumulatorCenter property of the model
        /// </summary>
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
                case "AccumulatorCenter":
                    return AccumulatorCenter.ToString();
                case "AccumulatorCount":
                    return AccumulatorCount.ToString();
                case "AccumulatorDeadband":
                    return AccumulatorDeadband.ToString();
                case "AccumulatorValue":
                    return AccumulatorValue.ToString();
                case "AverageBits":
                    return AverageBits.ToString();
                case "AverageValue":
                    return AverageValue.ToString();
                case "ConversionRatio":
                    return ConversionRatio.ToString();
                case "ID":
                    return ID.ToString();
                case "Name":
                    return Name.ToString();
                case "OversampleBits":
                    return OversampleBits.ToString();
                case "RawAccumulatorValue":
                    return RawAccumulatorValue.ToString();
                case "RawAverageValue":
                    return RawAverageValue.ToString();
                case "RawValue":
                    return RawValue.ToString();
                case "SensorOffset":
                    return SensorOffset.ToString();
                case "Value":
                    return Value.ToString();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }
        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "AccumulatorCenter":
                    AccumulatorCenter = SafeCastInt(value);
                    break;
                case "AccumulatorDeadband":
                    AccumulatorDeadband = SafeCastInt(value);
                    break;
                case "AverageBits":
                    AverageBits = SafeCastInt(value);
                    break;
                case "ConversionRatio":
                    ConversionRatio = SafeCastDouble(value);
                    break;
                case "ID":
                    ID = SafeCastInt(value);
                    break;
                case "Name":
                    Name = value;
                    break;
                case "OversampleBits":
                    OversampleBits = SafeCastInt(value);
                    break;
                case "SensorOffset":
                    SensorOffset = SafeCastDouble(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" is Read-Only or Does Not Exist");
            }

        }
        protected override void OnModelChange()
        {
            //TODO: update the IO Service
        }
        #endregion
    }
}