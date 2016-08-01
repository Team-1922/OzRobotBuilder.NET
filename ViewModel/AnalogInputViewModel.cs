using System;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    public class AnalogInputViewModel : IAnalogInputProvider
    {
        protected AnalogInput _analogInputModel;
        public int AccumulatorCenter
        {
            get
            {
                return _analogInputModel.AccumulatorCenter = IOService.Instance.AnalogInputs[ID].
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public long AccumulatorCount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int AccumulatorDeadBand
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int AccumulatorInitialValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public long AccumulatorValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int AverageBitsField
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double ConversionRatio
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ID
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int OversampleBits
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int RawValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double SensorOffset
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double Value
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void SetAnalogInput(AnalogInput analogInput)
        {
            throw new NotImplementedException();
        }
    }
}