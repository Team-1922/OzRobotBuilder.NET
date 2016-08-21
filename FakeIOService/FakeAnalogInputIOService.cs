using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.FakeIOService
{
    public class FakeAnalogInputIOService : IAnalogInputIOService
    {
        #region IAnalogInputIOService
        public int AccumulatorCenter { get; set; }
        public long AccumulatorCount { get; internal set; }
        public int AccumulatorDeadband { get; set; }
        public long AccumulatorValue { get; internal set; }
        public int AverageBits { get; set; }
        public long AverageValue { get; internal set; }
        public int OversampleBits { get; set; }
        public double Value { get; internal set; }
        public bool ValueAsBool
        {
            get
            {
                return Value > 0;
            }
        }
        public int ID { get; internal set; }
        public void ResetAccumulator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
