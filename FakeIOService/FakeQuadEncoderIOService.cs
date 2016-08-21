using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.FakeIOService
{
    public class FakeQuadEncoderIOService : IQuadEncoderIOService
    {
        public FakeQuadEncoderIOService(QuadEncoder quadEncoder)
        {
            ID = quadEncoder.ID;
            ID1 = quadEncoder.ID1;
            Value = quadEncoder.RawValue;
            Velocity = quadEncoder.RawVelocity;
        }
        #region IQuadEncoderIOService
        public double Value { get; private set; }
        public bool ValueAsBool
        {
            get
            {
                return Value > 1.0 ? true : false;
            }
        }
        public double Velocity { get; private set; }
        public int ID { get; private set; }
        public int ID1 { get; private set; }
        #endregion
    }
}
