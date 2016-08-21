using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.FakeIOService
{
    public class FakeAnalogInputIOService : IAnalogInputIOService
    {
        public FakeAnalogInputIOService(AnalogInput analogInput)
        {
            AccumulatorCenter = analogInput.AccumulatorCenter;
            AccumulatorCount = analogInput.AccumulatorCount;
            AccumulatorDeadband = analogInput.AccumulatorDeadband;
            AccumulatorValue = analogInput.RawAccumulatorValue;
            AverageBits = analogInput.AverageBits;
            AverageValue = analogInput.RawAverageValue;
            ID = analogInput.ID;
            OversampleBits = analogInput.OversampleBits;
            Value = analogInput.RawValue;
        }

        #region IAnalogInputIOService
        public int AccumulatorCenter { get; set; }
        public long AccumulatorCount { get; private set; }
        public int AccumulatorDeadband { get; set; }
        public long AccumulatorValue { get; private set; }
        public int AverageBits { get; set; }
        public long AverageValue { get; private set; }
        public int OversampleBits { get; set; }
        public double Value { get; private set; }
        public bool ValueAsBool
        {
            get
            {
                return Value > 0;
            }
        }
        public int ID { get; private set; }
        public void ResetAccumulator()
        {
            //this probably will not do much
            AccumulatorCount = 0;
            AccumulatorValue = 0;
        }
        #endregion
    }
}
