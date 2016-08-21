using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.FakeIOService
{
    public class FakeQuadEncoderIOService : IQuadEncoderIOService
    {
        #region IQuadEncoderIOService
        public double Value { get; internal set; }
        public bool ValueAsBool
        {
            get
            {
                return Value > 1.0 ? true : false;
            }
        }
        public double Velocity { get; internal set; }
        public int ID { get; internal set; }
        public int ID1 { get; internal set; }
        #endregion
    }
}
